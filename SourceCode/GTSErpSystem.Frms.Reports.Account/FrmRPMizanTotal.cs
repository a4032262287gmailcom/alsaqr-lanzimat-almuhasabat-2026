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
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.FrmsMain.Search;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Reports.Account;

public class FrmRPMizanTotal : XtraForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateEdit TxtDate2;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label21;

	private Label label2;

	private ButtonEdit TxtFromAccount_No;

	private Label label9;

	private ButtonEdit TxtToAccount_No;

	private Label label35;

	private NumericUpDown TxtAccount_Level;

	private Label LblToAccount_No;

	private Label LblFromAccount_No;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	private TimeEdit TxtTime2;

	private TimeEdit TxtTime1;

	private CheckBox CkHide;

	public FrmRPMizanTotal()
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
		TxtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/dd");
		TxtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
		TxtTime1.Text = "00:00:00";
		TxtTime2.Text = "23:59:59";
		TxtDate1.Focus();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void Print()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 63))
		{
			if (Convert.ToDateTime(TxtDate1.Text) <= Convert.ToDateTime(TxtDate2.Text))
			{
				SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
				DataSetALL dataSetALL = new DataSetALL();
				dataSetALL.Tables["View_TrialBalanceByTotal"].Clear();
				SqlCommand sqlCommand = new SqlCommand("AddTempTrialBalanceByTotal", sqlConnection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				if (TxtFromAccount_No.Text != "" && TxtToAccount_No.Text != "")
				{
					sqlCommand.Parameters.AddWithValue("@pflag", 1);
					sqlCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
					sqlCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
					sqlCommand.Parameters.AddWithValue("@FromAccount_No", Convert.ToInt32(TxtFromAccount_No.Text));
					sqlCommand.Parameters.AddWithValue("@ToAccount_No", Convert.ToInt32(TxtToAccount_No.Text));
					sqlCommand.Parameters.AddWithValue("@Account_Level", Convert.ToInt32(TxtAccount_Level.Text));
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlCommand.Parameters.AddWithValue("@UserID", LoginDetails.UserID);
					sqlCommand.Parameters.AddWithValue("@HideBalance", CkHide.Checked);
				}
				if (TxtFromAccount_No.Text != "" && TxtToAccount_No.Text == "")
				{
					sqlCommand.Parameters.AddWithValue("@pflag", 2);
					sqlCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
					sqlCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
					sqlCommand.Parameters.AddWithValue("@FromAccount_No", Convert.ToInt32(TxtFromAccount_No.Text));
					sqlCommand.Parameters.AddWithValue("@Account_Level", Convert.ToInt32(TxtAccount_Level.Text));
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlCommand.Parameters.AddWithValue("@UserID", LoginDetails.UserID);
					sqlCommand.Parameters.AddWithValue("@HideBalance", CkHide.Checked);
				}
				if (TxtFromAccount_No.Text == "" && TxtToAccount_No.Text != "")
				{
					sqlCommand.Parameters.AddWithValue("@pflag", 3);
					sqlCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
					sqlCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
					sqlCommand.Parameters.AddWithValue("@ToAccount_No", Convert.ToInt32(TxtToAccount_No.Text));
					sqlCommand.Parameters.AddWithValue("@Account_Level", Convert.ToInt32(TxtAccount_Level.Text));
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlCommand.Parameters.AddWithValue("@UserID", LoginDetails.UserID);
					sqlCommand.Parameters.AddWithValue("@HideBalance", CkHide.Checked);
				}
				if (TxtFromAccount_No.Text == "" && TxtToAccount_No.Text == "")
				{
					sqlCommand.Parameters.AddWithValue("@pflag", 4);
					sqlCommand.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(TxtDate1.Text + " " + TxtTime1.Text));
					sqlCommand.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(TxtDate2.Text + " " + TxtTime2.Text));
					sqlCommand.Parameters.AddWithValue("@Account_Level", Convert.ToInt32(TxtAccount_Level.Text));
					sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
					sqlCommand.Parameters.AddWithValue("@UserID", LoginDetails.UserID);
					sqlCommand.Parameters.AddWithValue("@HideBalance", CkHide.Checked);
				}
				sqlDataAdapter.Fill(dataSetALL.View_TrialBalanceByTotal);
				sqlConnection.Close();
				if (dataSetALL.Tables["View_TrialBalanceByTotal"].Rows.Count > 0)
				{
					StiReport stiReport = new StiReport();
					if (LoginDetails.Lang == "ar-EG")
					{
						stiReport.Load(Application.StartupPath + "\\Reports\\TrialBalanceByTotalReport.mrt");
					}
					else
					{
						stiReport.Load(Application.StartupPath + "\\ReportsEN\\TrialBalanceByTotalReport.mrt");
					}
					stiReport.RegData(dataSetALL.View_TrialBalanceByTotal);
					stiReport.Dictionary.Synchronize();
					stiReport.Compile();
					stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
					stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
					stiReport["VatNumComp"] = LoginDetails.VatNum;
					stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
					stiReport["Address"] = LoginDetails.Address;
					stiReport["Phone"] = LoginDetails.Phone;
					stiReport["Bank"] = LoginDetails.CompanyBank;
					stiReport["Date1"] = Convert.ToDateTime(TxtDate1.Text).ToShortDateString();
					stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text).ToShortDateString();
					stiReport["Between"] = "ميزان مراجعة مجمع من تاريخ " + TxtDate1.Text + " الي تاريخ" + TxtDate2.Text;
					stiReport["Title"] = "";
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
				MessageBox.Show("عفوا، لايمكن ان يكون التاريخ الاول اقل من التاريخ الثاني", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				TxtDate1.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnContinue.Focus();
		}
	}

	private void TxtFromAccount_No_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmMizan";
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblFromAccount_No.Text = frmSearchAccount.Account_Number.ToString();
				TxtFromAccount_No.Text = frmSearchAccount.Account_Number.ToString();
			}
			else
			{
				TxtFromAccount_No.Focus();
			}
			LoginDetails.FrmName = "";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtToAccount_No_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			LoginDetails.FrmName = "FrmMizan";
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblToAccount_No.Text = frmSearchAccount.Account_Number.ToString();
				TxtToAccount_No.Text = frmSearchAccount.Account_Number.ToString();
			}
			else
			{
				TxtToAccount_No.Focus();
			}
			LoginDetails.FrmName = "";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Reports.Account.FrmRPMizanTotal));
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.CkHide = new System.Windows.Forms.CheckBox();
		this.TxtTime2 = new DevExpress.XtraEditors.TimeEdit();
		this.TxtTime1 = new DevExpress.XtraEditors.TimeEdit();
		this.LblToAccount_No = new System.Windows.Forms.Label();
		this.TxtAccount_Level = new System.Windows.Forms.NumericUpDown();
		this.LblFromAccount_No = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.TxtFromAccount_No = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtToAccount_No = new DevExpress.XtraEditors.ButtonEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount_Level).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFromAccount_No.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtToAccount_No.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.CkHide);
		this.groupBox1.Controls.Add(this.TxtTime2);
		this.groupBox1.Controls.Add(this.TxtTime1);
		this.groupBox1.Controls.Add(this.LblToAccount_No);
		this.groupBox1.Controls.Add(this.TxtAccount_Level);
		this.groupBox1.Controls.Add(this.LblFromAccount_No);
		this.groupBox1.Controls.Add(this.label35);
		this.groupBox1.Controls.Add(this.TxtFromAccount_No);
		this.groupBox1.Controls.Add(this.TxtToAccount_No);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtDate2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtDate1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.CkHide, "CkHide");
		this.CkHide.Name = "CkHide";
		this.CkHide.UseVisualStyleBackColor = true;
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
		resources.ApplyResources(this.LblToAccount_No, "LblToAccount_No");
		this.LblToAccount_No.Name = "LblToAccount_No";
		resources.ApplyResources(this.TxtAccount_Level, "TxtAccount_Level");
		this.TxtAccount_Level.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtAccount_Level.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.TxtAccount_Level.Name = "TxtAccount_Level";
		this.TxtAccount_Level.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.TxtAccount_Level.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.LblFromAccount_No, "LblFromAccount_No");
		this.LblFromAccount_No.Name = "LblFromAccount_No";
		resources.ApplyResources(this.label35, "label35");
		this.label35.Name = "label35";
		resources.ApplyResources(this.TxtFromAccount_No, "TxtFromAccount_No");
		this.TxtFromAccount_No.Name = "TxtFromAccount_No";
		this.TxtFromAccount_No.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtFromAccount_No.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtFromAccount_No.Properties.Appearance.Font");
		this.TxtFromAccount_No.Properties.Appearance.Options.UseBackColor = true;
		this.TxtFromAccount_No.Properties.Appearance.Options.UseFont = true;
		this.TxtFromAccount_No.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtFromAccount_No.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtFromAccount_No.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtFromAccount_No.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtFromAccount_No.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtFromAccount_No.Properties.Buttons"), resources.GetString("TxtFromAccount_No.Properties.Buttons1"), (int)resources.GetObject("TxtFromAccount_No.Properties.Buttons2"), (bool)resources.GetObject("TxtFromAccount_No.Properties.Buttons3"), (bool)resources.GetObject("TxtFromAccount_No.Properties.Buttons4"), (bool)resources.GetObject("TxtFromAccount_No.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtFromAccount_No.Properties.Buttons6"), resources.GetObject("TxtFromAccount_No.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtFromAccount_No.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtFromAccount_No.Properties.Buttons9"))
		});
		this.TxtFromAccount_No.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtFromAccount_No_ButtonClick);
		this.TxtFromAccount_No.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtFromAccount_No.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.TxtToAccount_No, "TxtToAccount_No");
		this.TxtToAccount_No.Name = "TxtToAccount_No";
		this.TxtToAccount_No.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtToAccount_No.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtToAccount_No.Properties.Appearance.Font");
		this.TxtToAccount_No.Properties.Appearance.Options.UseBackColor = true;
		this.TxtToAccount_No.Properties.Appearance.Options.UseFont = true;
		this.TxtToAccount_No.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtToAccount_No.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtToAccount_No.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtToAccount_No.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtToAccount_No.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtToAccount_No.Properties.Buttons"), resources.GetString("TxtToAccount_No.Properties.Buttons1"), (int)resources.GetObject("TxtToAccount_No.Properties.Buttons2"), (bool)resources.GetObject("TxtToAccount_No.Properties.Buttons3"), (bool)resources.GetObject("TxtToAccount_No.Properties.Buttons4"), (bool)resources.GetObject("TxtToAccount_No.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtToAccount_No.Properties.Buttons6"), resources.GetObject("TxtToAccount_No.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtToAccount_No.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtToAccount_No.Properties.Buttons9"))
		});
		this.TxtToAccount_No.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtToAccount_No_ButtonClick);
		this.TxtToAccount_No.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtToAccount_No.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtNumberOnly_KeyPress);
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
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
		this.TxtDate2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
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
		resources.ApplyResources(this.BtnContinue, "BtnContinue");
		this.BtnContinue.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnContinue.Appearance.Font");
		this.BtnContinue.Appearance.Options.UseFont = true;
		this.BtnContinue.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContinue.ImageOptions.Image");
		this.BtnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContinue.Name = "BtnContinue";
		this.BtnContinue.Click += new System.EventHandler(BtnContinue_Click);
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnContinue);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmRPMizanTotal";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtTime2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtTime1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount_Level).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtFromAccount_No.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtToAccount_No.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


