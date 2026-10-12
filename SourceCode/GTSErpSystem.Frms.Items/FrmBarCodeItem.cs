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
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Items.FrmSearch;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Items;

public class FrmBarCodeItem : XtraForm
{
	private Class_Items Items = new Class_Items();

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

	private Label label16;

	private SimpleButton BtnContinue;

	private SimpleButton BtnExit;

	public ButtonEdit TxtItemCode;

	private Label lblItemID;

	private Label lblCode;

	private Label label1;

	private DateEdit TxtDate1;

	private Label label2;

	private DateEdit TxtDate2;

	public FrmBarCodeItem()
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
		TxtDate1.Text = DateTime.Now.ToShortDateString();
		TxtDate2.Text = DateTime.Now.AddDays(7.0).ToShortDateString();
	}

	private void BtnContinue_Click(object sender, EventArgs e)
	{
		if (TxtItemCode.Text != "")
		{
			Print();
		}
		else
		{
			MessageBox.Show("عفوا، يرجي اختيار صنف ثم المحاولة مرة اخري", "المستكشف ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
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

	private void Print()
	{
		string nameServer = LoginDetails.nameServer;
		string databaseServer = LoginDetails.databaseServer;
		string userNameServer = LoginDetails.userNameServer;
		string text = LoginDetails.passServer;
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + text);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["BarcodeItem"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Select_BarCode", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@ItemCode", lblCode.Text);
		sqlDataAdapter.Fill(dataSetALL.BarcodeItem);
		sqlConnection.Close();
		StiReport stiReport = new StiReport();
		stiReport.Load(Application.StartupPath + "\\Reports\\RptBarcodeDate.mrt");
		stiReport.RegData(dataSetALL.BarcodeItem);
		stiReport.Dictionary.Synchronize();
		stiReport.Compile();
		stiReport["Date"] = Convert.ToDateTime(TxtDate1.Text);
		stiReport["Date2"] = Convert.ToDateTime(TxtDate2.Text);
		stiReport.Render();
		stiReport.Print();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmBarCodeItem));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.lblItemID = new System.Windows.Forms.Label();
		this.lblCode = new System.Windows.Forms.Label();
		this.BtnContinue = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox24 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtDate2 = new DevExpress.XtraEditors.DateEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate1 = new DevExpress.XtraEditors.DateEdit();
		this.TxtItemCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label16 = new System.Windows.Forms.Label();
		this.groupBox22.SuspendLayout();
		this.groupBox23.SuspendLayout();
		this.groupBox24.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox22, "groupBox22");
		this.groupBox22.Controls.Add(this.groupBox23);
		this.groupBox22.Controls.Add(this.groupBox24);
		this.groupBox22.Name = "groupBox22";
		this.groupBox22.TabStop = false;
		resources.ApplyResources(this.groupBox23, "groupBox23");
		this.groupBox23.Controls.Add(this.lblItemID);
		this.groupBox23.Controls.Add(this.lblCode);
		this.groupBox23.Controls.Add(this.BtnContinue);
		this.groupBox23.Controls.Add(this.BtnExit);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.TabStop = false;
		resources.ApplyResources(this.lblItemID, "lblItemID");
		this.lblItemID.Name = "lblItemID";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.Name = "lblCode";
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
		resources.ApplyResources(this.groupBox24, "groupBox24");
		this.groupBox24.Controls.Add(this.label2);
		this.groupBox24.Controls.Add(this.TxtDate2);
		this.groupBox24.Controls.Add(this.label1);
		this.groupBox24.Controls.Add(this.TxtDate1);
		this.groupBox24.Controls.Add(this.TxtItemCode);
		this.groupBox24.Controls.Add(this.label16);
		this.groupBox24.Name = "groupBox24";
		this.groupBox24.TabStop = false;
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.TxtDate2, "TxtDate2");
		this.TxtDate2.Name = "TxtDate2";
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
		this.TxtDate2.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("TxtDate2.Properties.CalendarTimeProperties.Mask.EditMask");
		this.TxtDate2.Properties.Mask.EditMask = resources.GetString("TxtDate2.Properties.Mask.EditMask");
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.TxtDate1, "TxtDate1");
		this.TxtDate1.Name = "TxtDate1";
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
		this.TxtDate1.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("TxtDate1.Properties.CalendarTimeProperties.Mask.EditMask");
		this.TxtDate1.Properties.Mask.EditMask = resources.GetString("TxtDate1.Properties.Mask.EditMask");
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
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox22);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmBarCodeItem";
		this.groupBox22.ResumeLayout(false);
		this.groupBox23.ResumeLayout(false);
		this.groupBox23.PerformLayout();
		this.groupBox24.ResumeLayout(false);
		this.groupBox24.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


