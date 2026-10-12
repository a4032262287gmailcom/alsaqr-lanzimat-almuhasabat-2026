using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Main;
using Janus.Windows.EditControls;

namespace GTSErpSystem.Frms.CloseYears;

public class FrmCloseYear : XtraForm
{
	private SqlConnection Conn;

	private SqlCommand Command;

	private SqlDataReader Reader;

	private string sql = "";

	private string ConnectionString = "";

	private IContainer components = null;

	private GroupBox groupBox3;

	private GroupBox groupBox2;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private System.Windows.Forms.ComboBox comboServerName;

	private TextBox TxtPassword;

	private TextBox TxtUser;

	private SimpleButton BtnExit;

	private SimpleButton BtnClose;

	private SimpleButton BtnTest;

	private SimpleButton BtnSearch;

	private TextBox TxtNewDBName;

	private Label label5;

	private UIProgressBar uiProgressBar1;

	private ComboBoxEdit comboDatabaseName;

	private TextBox TxtPath;

	private SimpleButton btnChooseFolder;

	private FolderBrowserDialog folderBrowserDialog1;

	public FrmCloseYear()
	{
		InitializeComponent();
		BindServer();
		UserLookAndFeel.Default.SkinName = "Office 2013";
		comboServerName.Focus();
		comboDatabaseName.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
	}

	private void BtnConnection_Click(object sender, EventArgs e)
	{
		if (comboServerName.Text != "" && TxtUser.Text != "" && TxtPassword.Text != "")
		{
			try
			{
				ConnectionString = "Data Source = " + comboServerName.Text + ";User Id=" + TxtUser.Text + ";Password=" + TxtPassword.Text;
				Conn = new SqlConnection(ConnectionString);
				Conn.Open();
				sql = "SELECT * FROM sys.databases d where d.database_id > 4 AND   d.name NOT LIKE 'ReportServer'AND  d.name NOT LIKE 'ReportServerTempDB' ORDER BY d.create_date DESC ";
				Command = new SqlCommand(sql, Conn);
				Reader = Command.ExecuteReader();
				comboDatabaseName.Properties.Items.Clear();
				while (Reader.Read())
				{
					comboDatabaseName.Properties.Items.Add(Reader[0].ToString());
				}
				if (comboDatabaseName.Properties.Items.Count > 0)
				{
					comboDatabaseName.Text = LoginDetails.databaseServer;
					string text = comboDatabaseName.SelectedItem.ToString();
					if (text.EndsWith("2025"))
					{
						TxtNewDBName.Text = text.Substring(0, text.Length - 4) + "2026";
					}
					else if (text.EndsWith("2026"))
					{
						TxtNewDBName.Text = text.Substring(0, text.Length - 4) + "M2026";
					}
					else
					{
						TxtNewDBName.Text = text + "2026";
					}
				}
				MessageBox.Show("تم الاتصال بنجاح", "مستكشف الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}
		MessageBox.Show("عفوا، يرجي ادخال البيانات بشكل صحيح", "مستكشف الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		TxtUser.Focus();
	}

	private void GetAllServer()
	{
		DataTable dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
		foreach (DataRow row in dataSources.Rows)
		{
			if (!comboServerName.Items.Contains(row[dataSources.Columns["ServerName"]].ToString()))
			{
				comboServerName.Items.Add(row[dataSources.Columns["ServerName"]].ToString());
			}
		}
	}

	private void UpdateProgress(int value)
	{
		if (base.InvokeRequired)
		{
			Invoke(new Action<int>(UpdateProgress), value);
		}
		else
		{
			uiProgressBar1.Value = value;
		}
	}

	private async void BtnSave_Click(object sender, EventArgs e)
	{
		if (comboDatabaseName.SelectedItem == null || string.IsNullOrWhiteSpace(TxtNewDBName.Text))
		{
			MessageBox.Show("يرجى اختيار قاعدة البيانات واسم القاعدة الجديدة");
			return;
		}
		string sourceDb = comboDatabaseName.SelectedItem.ToString();
		string newDb = TxtNewDBName.Text.Trim();
		string connectionString = "Data Source=" + comboServerName.Text + ";User Id=" + TxtUser.Text + ";Password=" + TxtPassword.Text + ";TrustServerCertificate=True;";
		uiProgressBar1.Minimum = 0;
		uiProgressBar1.Maximum = 100;
		uiProgressBar1.Value = 0;
		uiProgressBar1.Visible = true;
		BtnClose.Enabled = false;
		try
		{
			await Task.Run(delegate
			{
				using SqlConnection sqlConnection = new SqlConnection(connectionString);
				sqlConnection.Open();
				string text = TxtPath.Text.Trim();
				string cmdText = "\r\n                            DECLARE @BackupPath NVARCHAR(4000);\r\n                            DECLARE @DataPath NVARCHAR(4000);\r\n                            DECLARE @BackupFile NVARCHAR(4000);\r\n                            DECLARE @Sql NVARCHAR(MAX);\r\n\r\n                            -- مسار الباك اب الافتراضي\r\n                            EXEC master.dbo.xp_instance_regread\r\n                                N'HKEY_LOCAL_MACHINE',\r\n                                N'Software\\Microsoft\\MSSQLServer\\MSSQLServer',\r\n                                N'BackupDirectory',\r\n                                @BackupPath OUTPUT;\r\n\r\n                            -- المسار القادم من التطبيق (ممكن يكون فاضي)\r\n                            SET @DataPath = N'" + text.Replace("'", "''") + "';\r\n\r\n                            -- لو مفيش مسار من المستخدم استخدم Default Data Path\r\n                            IF (@DataPath IS NULL OR LTRIM(RTRIM(@DataPath)) = '')\r\n                            BEGIN\r\n                                SET @DataPath = CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR(4000));\r\n                            END\r\n\r\n                            -- تأكيد وجود \\\r\n                            IF RIGHT(@DataPath, 1) <> '\\'\r\n                                SET @DataPath = @DataPath + '\\';\r\n\r\n                            SET @BackupFile = @BackupPath + '\\" + sourceDb + ".bak';\r\n\r\n                            -- Backup\r\n                            BACKUP DATABASE [" + sourceDb + "]\r\n                            TO DISK = @BackupFile\r\n                            WITH INIT, COPY_ONLY;\r\n\r\n                            -- Restore\r\n                            SET @Sql = '\r\n                            RESTORE DATABASE [" + newDb + "]\r\n                            FROM DISK = ''' + @BackupFile + '''\r\n                            WITH\r\n                            MOVE ''" + sourceDb + "'' TO ''' + @DataPath + '" + newDb + ".mdf'',\r\n                            MOVE ''" + sourceDb + "_log'' TO ''' + @DataPath + '" + newDb + "_log.ldf'',\r\n                            RECOVERY;';\r\n\r\n                            EXEC (@Sql);\r\n                            ";
				SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
				sqlCommand.CommandTimeout = 0;
				sqlCommand.ExecuteNonQuery();
				UpdateProgress(30);
				new SqlCommand("\r\n                        WHILE DB_ID('" + newDb + "') IS NULL\r\n                        BEGIN\r\n                            WAITFOR DELAY '00:00:01';\r\n                        END\r\n                                ", sqlConnection).ExecuteNonQuery();
				UpdateProgress(50);
				string cmdText2 = "\r\n                                USE [" + newDb + "];\r\n\r\n                            IF OBJECT_ID('dbo.Account_YearEnd', 'P') IS NOT NULL\r\n                                DROP PROCEDURE dbo.Account_YearEnd;\r\n\r\n                                EXEC('\r\n                              Create PROC [dbo].[Account_YearEnd]\r\n                            AS\r\n                            BEGIN\r\n                                DECLARE @Todate DATETIME;\r\n                                SET @Todate =\r\n                                (\r\n                                    SELECT dbo.DatePart(GETDATE())\r\n                                );\r\n\t                            DELETE View_Temp_Acc_Budget\r\n                                UPDATE dbo.Account_CostCenters\r\n                                SET dbo.Account_CostCenters.Priv_Credit = dbo.View_New_CostCenter.NewCredit,\r\n                                    dbo.Account_CostCenters.Priv_Debit = dbo.View_New_CostCenter.NewDebit\r\n                                FROM dbo.Account_CostCenters\r\n                                    INNER JOIN dbo.View_New_CostCenter\r\n                                        ON dbo.Account_CostCenters.CostCentersID = dbo.View_New_CostCenter.CostCentersID;\r\n                                DELETE FROM dbo.AccountYearEndClosing;\r\n\r\n                                EXEC dbo.AddTemp_Budget @ToDate = @Todate, -- datetime\r\n                                                                 @BranchID = 1,     -- int\r\n                                                                 @UserID = 1;       -- int\r\n\r\n\r\n\t\t                            TRUNCATE TABLE dbo.Order_ManufacturingDetailsItems;\r\n\t\t                            TRUNCATE TABLE dbo.Order_ManufacturingDetailsNew;\r\n                                    DELETE  dbo.Order_Manufacturing;\r\n\t\t                            TRUNCATE TABLE dbo.Order_ManufacturingOrderDetails;\r\n                                    DELETE  dbo.Order_ManufacturingOrder;\r\n\t\t                            TRUNCATE TABLE dbo.OrderRent_RentDetails;\r\n                                    DELETE  dbo.OrderRent_Rent;\r\n\t\t                            TRUNCATE TABLE dbo.OrderRent_RecpitDetails;\r\n                                    DELETE  dbo.OrderRent_Recipt;\r\n\t\t                            TRUNCATE TABLE dbo.OrderDalala_SendingDetails;\r\n                                    DELETE  dbo.OrderDalala_Sending;\r\n\t\t                            TRUNCATE TABLE dbo.OrderDalala_DalalaDetails;\r\n                                    DELETE  dbo.OrderDalala_Dalala;\r\n\t\t                            TRUNCATE TABLE dbo.Order_PaymentItemDetails;\r\n                                    DELETE  dbo.Order_PaymentItem;\r\n\t\t                            TRUNCATE TABLE dbo.Order_TransferToBranchDetails;\r\n                                    DELETE  dbo.Order_TransferToBranch;\r\n\t\t                            TRUNCATE TABLE dbo.Order_TransferFromBranchDetails;\r\n                                    DELETE  dbo.Order_TransferFromBranch;\r\n\t                                TRUNCATE TABLE dbo.Contract_ContractDetails;\r\n                                    DELETE  dbo.Contract_Contract;\r\n\t\t                            TRUNCATE TABLE dbo.Contract_GuaranteeDetails;\r\n                                    DELETE  dbo.Contract_Guarantee;\r\n\t\t                            DELETE  dbo.Contract_Store;\r\n                                    TRUNCATE TABLE dbo.Order_CheckoutOrdersDetails;\r\n\t\t                            DELETE  dbo.Order_CheckoutOrders;\r\n                                    TRUNCATE TABLE dbo.Order_OpenQuantityDetails;\r\n                                    DELETE  dbo.Order_OpenQuantity;\r\n\t\t                            TRUNCATE TABLE dbo.Order_InventorySettlementMinusDetails;\r\n\t\t                            DELETE dbo.Order_InventorySettlementMinus;\r\n                                    TRUNCATE TABLE dbo.Order_OrderReturnDetails;\r\n                                    DELETE  dbo.Order_OrderReturn;\r\n                                    TRUNCATE TABLE dbo.Order_PriceOfferDetails;\r\n                                    DELETE  dbo.Order_PriceOffer;\r\n                                    TRUNCATE TABLE dbo.Order_PurchasesOrderDetails;\r\n                                    DELETE  dbo.Order_PurchasesOrder;\r\n                                    TRUNCATE TABLE dbo.Order_PurchasesReturnDetails;\r\n                                    DELETE  dbo.Order_PurchasesReturn;\r\n                                    TRUNCATE TABLE dbo.Order_PurchasesDetails;\r\n                                    DELETE  dbo.Order_Purchases;\r\n                                    TRUNCATE TABLE dbo.Order_StoreTransferDetails;\r\n                                    DELETE  dbo.Order_StoreTransfer;\r\n                                    TRUNCATE TABLE dbo.Order_OrdersDetails;\r\n                                    DELETE  dbo.Order_Orders;\r\n                                    TRUNCATE TABLE dbo.Tran_TranDetails;\r\n                                    DELETE  dbo.Tran_Tran;\r\n                                    TRUNCATE TABLE dbo.Account_ReceiptsDetails;\r\n                                    TRUNCATE TABLE dbo.Tran_TranTempDetails;\r\n                                    TRUNCATE TABLE dbo.Tran_TranTemp;\r\n                                    DELETE  dbo.Account_Receipts;\r\n                                    UPDATE  dbo.Account_Accounts\r\n                                    SET     Priv_Debit = 0 ,\r\n                                            Priv_Credit = 0;\r\n        \r\n\t\t                            TRUNCATE TABLE dbo.Emp_PayrollDetails;\r\n\t\t                            DELETE dbo.Emp_Payroll;\r\n\t\t                            TRUNCATE TABLE dbo.Emp_PayrollTemp;\r\n\t\t                            DELETE dbo.Emp_PayrollTemp;\r\n\t\t                            UPDATE dbo.Restaurant_Room SET [Money] = 0, OrderID = 0 , ISPause = 0;\r\n\t\t                            UPDATE dbo.Restaurant_Table SET [Money] = 0, OrderID = 0 , ISPause = 0;\r\n\t\t                            TRUNCATE TABLE dbo.Restaurant_Delivery;\r\n\t\t                            UPDATE dbo.User_Login SET IsOpenDay = 0 , MoneyOpenDay = 0, Note = '''';\r\n;\r\n\t\t                            TRUNCATE TABLE dbo.User_DayClose;\r\n\r\n                                INSERT INTO dbo.AccountYearEndClosing\r\n                                (\r\n                                    Account_Sn,\r\n                                    Account_No,\r\n                                    Account_Name,\r\n                                    Credit,\r\n                                    Debit,\r\n                                    ViewBalance,\r\n                                    IsUpdate\r\n                                )\r\n                                SELECT Account_Sn,\r\n                                       Account_No,\r\n                                       Account_Name,\r\n                                       Credit,\r\n                                       Debit,\r\n                                       ViewBalance,\r\n                                       0\r\n                                FROM View_Temp_Acc_Budget;\r\n                                DELETE FROM dbo.AccountYearEndClosing\r\n                                WHERE ViewBalance = 0;\r\n                                UPDATE dbo.Account_Accounts\r\n                                SET Priv_Credit = 0,\r\n                                    Priv_Debit = 0;\r\n                                UPDATE dbo.AccountYearEndClosing\r\n                                SET IsUpdate = 1\r\n                                WHERE Account_Sn =\r\n                                (\r\n                                    SELECT dbo.GetBeginningInventoryID()\r\n                                )\r\n                                      OR Account_Sn =\r\n                                      (\r\n                                          SELECT dbo.GetEndingInventoryID()\r\n                                      )\r\n                                      OR Account_Sn =\r\n                                      (\r\n                                          SELECT dbo.GetCapitalAccountID()\r\n                                      )\r\n                                      OR Account_Sn =\r\n                                      (\r\n                                          SELECT dbo.GetProfitAndLossID()\r\n                                      );\r\n                                UPDATE dbo.Account_Accounts\r\n                                SET dbo.Account_Accounts.Priv_Debit = ABS(dbo.View_AccountYearEndClosing_Debit.ViewBalance)\r\n                                FROM dbo.View_AccountYearEndClosing_Debit\r\n                                    INNER JOIN dbo.Account_Accounts\r\n                                        ON dbo.View_AccountYearEndClosing_Debit.Account_Sn = dbo.Account_Accounts.ID;\r\n                                UPDATE dbo.Account_Accounts\r\n                                SET dbo.Account_Accounts.Priv_Credit = ABS(dbo.AccountYearEndClosing_credit.ViewBalance)\r\n                                FROM dbo.AccountYearEndClosing_credit\r\n                                    INNER JOIN dbo.Account_Accounts\r\n                                        ON dbo.AccountYearEndClosing_credit.Account_Sn = dbo.Account_Accounts.ID;\r\n                                --------------------------------- \r\n                                DECLARE @BeginningInventoryID BIGINT;\r\n                                DECLARE @EndingInventoryID BIGINT;\r\n                                DECLARE @CapitalAccountID BIGINT;\r\n                                DECLARE @ProfitAndLossID BIGINT;\r\n                                DECLARE @EndingInventoryBalance decimal(18, 6);\r\n                                DECLARE @ProfitAndLossBalance decimal(18, 6);\r\n                                SET @BeginningInventoryID =\r\n                                (\r\n                                    SELECT dbo.GetBeginningInventoryID()\r\n                                );\r\n                                SET @EndingInventoryID =\r\n                                (\r\n                                    SELECT dbo.GetEndingInventoryID()\r\n                                );\r\n                                SET @CapitalAccountID =\r\n                                (\r\n                                    SELECT dbo.GetCapitalAccountID()\r\n                                );\r\n                                SET @ProfitAndLossID =\r\n                                (\r\n                                    SELECT dbo.GetProfitAndLossID()\r\n                                );\r\n                                SET @EndingInventoryBalance =\r\n                                (\r\n                                    SELECT ViewBalance\r\n                                    FROM dbo.AccountYearEndClosing\r\n                                    WHERE Account_Sn = @EndingInventoryID\r\n                                );\r\n                                IF @EndingInventoryBalance < 0\r\n                                    UPDATE dbo.Account_Accounts\r\n                                    SET Priv_Debit = ABS(@EndingInventoryBalance)\r\n                                    WHERE ID = @BeginningInventoryID;\r\n                                ELSE\r\n                                    UPDATE dbo.Account_Accounts\r\n                                    SET Priv_Credit = ABS(@EndingInventoryBalance)\r\n                                    WHERE ID = @BeginningInventoryID;\r\n                                SET @ProfitAndLossBalance =\r\n                                (\r\n                                    SELECT ISNULL(SUM(ViewBalance), 0)\r\n                                    FROM dbo.AccountYearEndClosing\r\n                                    WHERE Account_Sn IN ( @CapitalAccountID, @ProfitAndLossID )\r\n                                );\r\n                                IF @ProfitAndLossBalance < 0\r\n                                    UPDATE dbo.Account_Accounts\r\n                                    SET Priv_Debit = ABS(@ProfitAndLossBalance)\r\n                                    WHERE ID = @CapitalAccountID;\r\n                                ELSE\r\n                                    UPDATE dbo.Account_Accounts\r\n                                    SET Priv_Credit = ABS(@ProfitAndLossBalance)\r\n                                    WHERE ID = @CapitalAccountID;\r\n                            END;\r\n                            ');\r\n\r\n                            EXEC dbo.Account_YearEnd;\r\n                            DROP PROCEDURE dbo.Account_YearEnd;\r\n\r\n                            USE [" + sourceDb + "];\r\n\r\n                            IF COL_LENGTH('dbo.TblSetting', 'CloseYear') IS NOT NULL\r\n                            BEGIN\r\n                                UPDATE dbo.TblSetting\r\n                                SET CloseYear = 1;\r\n                            END\r\n                            ";
				SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection);
				sqlCommand2.CommandTimeout = 0;
				sqlCommand2.ExecuteNonQuery();
				UpdateProgress(100);
			});
			MessageBox.Show("تم إغلاق السنة الماليه بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			uiProgressBar1.Visible = false;
			BtnClose.Enabled = true;
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Application.Exit();
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
			BtnTest.Focus();
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("en"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		GetAllServer();
		MessageBox.Show("تم الاتصال بنجاح", "مستكشف الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
	}

	private void BindServer()
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		try
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
			List<SaveConnectionForXml> list = new List<SaveConnectionForXml>();
			if (File.Exists(Application.StartupPath + "\\ERP.xml"))
			{
				FileStream fileStream = new FileStream(Application.StartupPath + "\\ERP.xml", FileMode.Open, FileAccess.Read);
				list = (List<SaveConnectionForXml>)xmlSerializer.Deserialize(fileStream);
				SaveConnectionForXml saveConnectionForXml = list[0];
				text = saveConnectionForXml._comboServer;
				text2 = Class_Encryption.DecryptString(saveConnectionForXml._TxtUser, "EF");
				text3 = Class_Encryption.DecryptString(saveConnectionForXml._TxtPassword, "EF");
				text4 = saveConnectionForXml._comboDatabase;
				comboServerName.Text = text;
				TxtUser.Text = text2;
				TxtPassword.Text = text3;
				fileStream.Close();
			}
		}
		catch (Exception)
		{
		}
	}

	private void comboDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (comboDatabaseName.Properties.Items.Count > 0)
		{
			string text = comboDatabaseName.SelectedItem.ToString();
			if (text.EndsWith("2025"))
			{
				TxtNewDBName.Text = text.Substring(0, text.Length - 4) + "2026";
			}
			else if (text.EndsWith("2026"))
			{
				TxtNewDBName.Text = text.Substring(0, text.Length - 4) + "M2026";
			}
			else
			{
				TxtNewDBName.Text = text + "2026";
			}
		}
	}

	private void btnChooseFolder_Click(object sender, EventArgs e)
	{
		folderBrowserDialog1.Description = "اختر مسار الريستور";
		folderBrowserDialog1.ShowNewFolderButton = true;
		if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		{
			TxtPath.Clear();
			TxtPath.Text = folderBrowserDialog1.SelectedPath;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.CloseYears.FrmCloseYear));
		this.uiProgressBar1 = new Janus.Windows.EditControls.UIProgressBar();
		this.TxtNewDBName = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.comboDatabaseName = new DevExpress.XtraEditors.ComboBoxEdit();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.TxtPassword = new System.Windows.Forms.TextBox();
		this.BtnTest = new DevExpress.XtraEditors.SimpleButton();
		this.TxtUser = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.comboServerName = new System.Windows.Forms.ComboBox();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.btnChooseFolder = new DevExpress.XtraEditors.SimpleButton();
		this.TxtPath = new System.Windows.Forms.TextBox();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.comboDatabaseName.Properties).BeginInit();
		base.SuspendLayout();
		this.uiProgressBar1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.uiProgressBar1.Location = new System.Drawing.Point(5, 257);
		this.uiProgressBar1.Name = "uiProgressBar1";
		this.uiProgressBar1.Size = new System.Drawing.Size(628, 34);
		this.uiProgressBar1.TabIndex = 45;
		this.TxtNewDBName.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtNewDBName.Location = new System.Drawing.Point(5, 177);
		this.TxtNewDBName.Name = "TxtNewDBName";
		this.TxtNewDBName.Size = new System.Drawing.Size(496, 26);
		this.TxtNewDBName.TabIndex = 43;
		this.TxtNewDBName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(507, 182);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(125, 16);
		this.label5.TabIndex = 44;
		this.label5.Text = "قاعده البيانات الجديده";
		this.groupBox3.Controls.Add(this.TxtPath);
		this.groupBox3.Controls.Add(this.BtnExit);
		this.groupBox3.Controls.Add(this.BtnClose);
		this.groupBox3.Location = new System.Drawing.Point(3, 320);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(642, 67);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(5, 16);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(142, 40);
		this.BtnExit.TabIndex = 2;
		this.BtnExit.Text = "خروج Exit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnClose.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnClose.ImageOptions.Image");
		this.BtnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClose.Location = new System.Drawing.Point(458, 16);
		this.BtnClose.Margin = new System.Windows.Forms.Padding(4);
		this.BtnClose.Name = "BtnClose";
		this.BtnClose.Size = new System.Drawing.Size(174, 40);
		this.BtnClose.TabIndex = 1;
		this.BtnClose.Text = "اغلاق السنه الماليه";
		this.BtnClose.Click += new System.EventHandler(BtnSave_Click);
		this.BtnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.groupBox2.Controls.Add(this.btnChooseFolder);
		this.groupBox2.Controls.Add(this.uiProgressBar1);
		this.groupBox2.Controls.Add(this.comboDatabaseName);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.TxtNewDBName);
		this.groupBox2.Controls.Add(this.BtnSearch);
		this.groupBox2.Controls.Add(this.TxtPassword);
		this.groupBox2.Controls.Add(this.BtnTest);
		this.groupBox2.Controls.Add(this.TxtUser);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.comboServerName);
		this.groupBox2.Location = new System.Drawing.Point(3, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(642, 302);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "البيانات الرئيسه";
		this.comboDatabaseName.Location = new System.Drawing.Point(6, 100);
		this.comboDatabaseName.Name = "comboDatabaseName";
		this.comboDatabaseName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboDatabaseName.Properties.Appearance.Options.UseFont = true;
		this.comboDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.comboDatabaseName.Size = new System.Drawing.Size(496, 24);
		this.comboDatabaseName.TabIndex = 44;
		this.comboDatabaseName.SelectedIndexChanged += new System.EventHandler(comboDatabaseName_SelectedIndexChanged);
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Location = new System.Drawing.Point(6, 30);
		this.BtnSearch.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Size = new System.Drawing.Size(133, 24);
		this.BtnSearch.TabIndex = 3;
		this.BtnSearch.Text = "بحث Search";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		this.TxtPassword.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPassword.Location = new System.Drawing.Point(6, 66);
		this.TxtPassword.Name = "TxtPassword";
		this.TxtPassword.PasswordChar = '#';
		this.TxtPassword.Size = new System.Drawing.Size(208, 26);
		this.TxtPassword.TabIndex = 2;
		this.TxtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPassword.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTest.ImageOptions.Image");
		this.BtnTest.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTest.Location = new System.Drawing.Point(6, 130);
		this.BtnTest.Margin = new System.Windows.Forms.Padding(4);
		this.BtnTest.Name = "BtnTest";
		this.BtnTest.Size = new System.Drawing.Size(624, 40);
		this.BtnTest.TabIndex = 0;
		this.BtnTest.Text = "اختبار Test";
		this.BtnTest.Click += new System.EventHandler(BtnConnection_Click);
		this.BtnTest.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtUser.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtUser.Location = new System.Drawing.Point(282, 66);
		this.TxtUser.Name = "TxtUser";
		this.TxtUser.Size = new System.Drawing.Size(220, 26);
		this.TxtUser.TabIndex = 1;
		this.TxtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtUser.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(507, 103);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(85, 16);
		this.label4.TabIndex = 43;
		this.label4.Text = "قاعدة البيانات ";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(507, 69);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(94, 16);
		this.label3.TabIndex = 42;
		this.label3.Text = "اسم المستخدم";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(220, 69);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 16);
		this.label2.TabIndex = 41;
		this.label2.Text = "الباسورد";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(507, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 16);
		this.label1.TabIndex = 40;
		this.label1.Text = "اسم السيرفر";
		this.comboServerName.FormattingEnabled = true;
		this.comboServerName.Location = new System.Drawing.Point(146, 30);
		this.comboServerName.Name = "comboServerName";
		this.comboServerName.Size = new System.Drawing.Size(356, 24);
		this.comboServerName.TabIndex = 0;
		this.comboServerName.Text = ".";
		this.comboServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.btnChooseFolder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
		this.btnChooseFolder.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.btnChooseFolder.Location = new System.Drawing.Point(7, 210);
		this.btnChooseFolder.Margin = new System.Windows.Forms.Padding(4);
		this.btnChooseFolder.Name = "btnChooseFolder";
		this.btnChooseFolder.Size = new System.Drawing.Size(622, 40);
		this.btnChooseFolder.TabIndex = 46;
		this.btnChooseFolder.Text = " مسار قاعده البيانات";
		this.btnChooseFolder.Click += new System.EventHandler(btnChooseFolder_Click);
		this.TxtPath.Location = new System.Drawing.Point(189, 36);
		this.TxtPath.Name = "TxtPath";
		this.TxtPath.Size = new System.Drawing.Size(81, 23);
		this.TxtPath.TabIndex = 47;
		this.TxtPath.Visible = false;
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(649, 395);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCloseYear";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اغلاق السنه الماليه";
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.comboDatabaseName.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


