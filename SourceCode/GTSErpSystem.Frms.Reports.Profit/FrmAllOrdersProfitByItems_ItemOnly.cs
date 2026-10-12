using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items.FrmSearch;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Profit;

public class FrmAllOrdersProfitByItems_ItemOnly : XtraForm
{
	private Class_Items Items = new Class_Items();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Orders ORD = new Class_Orders();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private Class_Stores LGN = new Class_Stores();

	private IContainer components = null;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private GroupBox groupBox1;

	private DateEdit TxtDate2;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label21;

	public ButtonEdit TxtItemCode;

	private Label label16;

	private Label lblItemID;

	private Label lblCode;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	public FrmAllOrdersProfitByItems_ItemOnly()
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
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
		TxtDate1.Focus();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 68))
		{
			if (TxtItemCode.Text != "")
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["TableOrderProfitByitem"].Clear();
				SqlCommand sqlCommand = new SqlCommand("GetReport_OrderProfitByItem_ItemOnly", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlCommand.Parameters.AddWithValue("@Date1", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
				sqlCommand.Parameters.AddWithValue("@Date2", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
				sqlCommand.Parameters.AddWithValue("@ItemID", Convert.ToInt32(lblItemID.Text));
				sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
				sqlDataAdapter.Fill(dataSetALL.TableOrderProfitByitem);
				sqlConnection.Close();
				if (dataSetALL.Tables["TableOrderProfitByitem"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\ReportOrderProfitByItems.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportOrderProfitByItems.mrt");
					}
					stiReport.RegData(dataSetALL.TableOrderProfitByitem);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text);
					stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text);
					stiReport.Render();
					stiReport.Show(dialogForm: true);
					FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
					frmReportGetOrder.stiViewerControl1.Report = stiReport;
				}
				else
				{
					MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("فضلا، اختر صنف ثم حاول مرة اخري", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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

	private void TxtItemCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		LoginDetails.ISCheckSearchItem = true;
		FrmSearchItems1 frmSearchItems = new FrmSearchItems1();
		frmSearchItems.ShowDialog();
		if (LoginDetails.ItemSearchID <= 0)
		{
			return;
		}
		foreach (Item_Items item in Items.GetItemsByitemid(LoginDetails.ItemSearchID))
		{
			lblCode.Text = item.Item_code;
			lblItemID.Text = item.ItemId.ToString();
			TxtItemCode.Text = item.item_Name;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Profit.FrmAllOrdersProfitByItems_ItemOnly));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtItemCode = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.label21 = new System.Windows.Forms.Label();
		this.lblItemID = new System.Windows.Forms.Label();
		this.lblCode = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		this.BtnContinue.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.groupBox1.Controls.Add(this.TxtTime2);
		this.groupBox1.Controls.Add(this.TxtItemCode);
		this.groupBox1.Controls.Add(this.TxtTime1);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.TxtDate2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtDate1);
		this.groupBox1.Controls.Add(this.label21);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.TxtTime2, "TxtTime2");
		this.TxtTime2.Name = "TxtTime2";
		this.TxtTime2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime2.Properties.Appearance.Font");
		this.TxtTime2.Properties.Appearance.Options.UseFont = true;
		this.TxtTime2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime2.Properties.Buttons"))
		});
		this.TxtTime2.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		resources.ApplyResources(this.TxtItemCode, "TxtItemCode");
		this.TxtItemCode.Name = "TxtItemCode";
		this.TxtItemCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
		this.TxtItemCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtItemCode.Properties.Appearance.Font");
		this.TxtItemCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtItemCode.Properties.Appearance.Options.UseFont = true;
		this.TxtItemCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtItemCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtItemCode.Properties.Buttons"), resources.GetString("TxtItemCode.Properties.Buttons1"), (int)resources.GetObject("TxtItemCode.Properties.Buttons2"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons3"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons4"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtItemCode.Properties.Buttons6"), resources.GetObject("TxtItemCode.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtItemCode.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtItemCode.Properties.Buttons9"))
		});
		this.TxtItemCode.Properties.ReadOnly = true;
		this.TxtItemCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		resources.ApplyResources(this.TxtTime1, "TxtTime1");
		this.TxtTime1.Name = "TxtTime1";
		this.TxtTime1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtTime1.Properties.Appearance.Font");
		this.TxtTime1.Properties.Appearance.Options.UseFont = true;
		this.TxtTime1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtTime1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtTime1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtTime1.Properties.Buttons"))
		});
		this.TxtTime1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.TxtDate2, "TxtDate2");
		this.TxtDate2.Name = "TxtDate2";
		this.TxtDate2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate2.Properties.Appearance.Font");
		this.TxtDate2.Properties.Appearance.Options.UseFont = true;
		this.TxtDate2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.Buttons"))
		});
		this.TxtDate2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate2.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate2.Properties.Mask.EditMask = resources.GetString("TxtDate2.Properties.Mask.EditMask");
		this.TxtDate2.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate2.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate2.Properties.TodayDate = new System.DateTime(2019, 6, 17, 21, 19, 56, 0);
		this.TxtDate2.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtDate2_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtDate1, "TxtDate1");
		this.TxtDate1.Name = "TxtDate1";
		this.TxtDate1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtDate1.Properties.Appearance.Font");
		this.TxtDate1.Properties.Appearance.Options.UseFont = true;
		this.TxtDate1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.Buttons"))
		});
		this.TxtDate1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtDate1.Properties.CalendarTimeProperties.Buttons"))
		});
		this.TxtDate1.Properties.Mask.EditMask = resources.GetString("TxtDate1.Properties.Mask.EditMask");
		this.TxtDate1.Properties.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("TxtDate1.Properties.Mask.UseMaskAsDisplayFormat");
		this.TxtDate1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.lblItemID, "lblItemID");
		this.lblItemID.Name = "lblItemID";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.Name = "lblCode";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.lblItemID);
		base.Controls.Add(this.lblCode);
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmAllOrdersProfitByItems_ItemOnly";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


