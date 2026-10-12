using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.BLL.Security;

namespace GTSErpSystem.Frms.Security;

public class FrmSecurityGroup : XtraForm
{
	private Class_Groups GRP = new Class_Groups();

	private Class_Users USR = new Class_Users();

	private Class_Groups GoupClass = new Class_Groups();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private DataTable datatablePermassions = new DataTable();

	private DataTable datatableGroups = new DataTable();

	public int GrpID = 0;

	private IContainer components = null;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem BtnEditUser;

	private GridColumn gridColumn17;

	private GridColumn gridColumn18;

	private Panel panel9;

	private TableLayoutPanel tableLayoutPanel1;

	private Panel panel1;

	private Panel panel2;

	private SimpleButton BtnSave;

	private System.Windows.Forms.ComboBox TxtGroups;

	private GridColumn gridColumn16;

	private GridColumn gridColumn15;

	private Panel panel3;

	private Panel panel4;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn9;

	private GridColumn gridColumn2;

	private GridColumn gridColumn8;

	private GridColumn gridColumn1;

	private GridColumn gridColumn3;

	private GridColumn gridColumn4;

	private GridColumn gridColumn5;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private RepositoryItemButtonEdit repositoryItemDelete;

	private CardView cardView1;

	private GridColumn gridColumn10;

	private GridColumn gridColumn11;

	private GridColumn gridColumn12;

	private GridColumn gridColumn13;

	private GridColumn gridColumn14;

	private ToggleSwitch CheckAll;

	private ToggleSwitch CheckEnter;

	private ToggleSwitch CheckSave;

	private ToggleSwitch CheckEdit;

	private ToggleSwitch CheckDelete;

	private ToggleSwitch CheckPrint;

	public FrmSecurityGroup()
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
		CRTDataTablePermassions();
		BindGroups();
	}

	public void InsertPrivalge()
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			GoupClass.InsertOnePermassion(GrpID, Convert.ToInt32(row[0].ToString()), Convert.ToBoolean(row[3].ToString()), Convert.ToBoolean(row[4].ToString()), Convert.ToBoolean(row[5].ToString()), Convert.ToBoolean(row[6].ToString()), Allow_Print: true, Allow_Export: true, LoginDetails.UserID, LoginDetails.macAddress);
		}
	}

	private void BtnSavePermission_Click_1(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 3))
		{
			switch (MessageBox.Show("هل أنت متأكد من اضافة الصلاحيات الي المجموعة", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
			case DialogResult.Yes:
				InsertPrivalge();
				MessageBox.Show("تم حفظ صلاحيات المجموعة بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				break;
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public int SlECTID()
	{
		return GoupClass.SLCID();
	}

	public void CRTDataTablePermassions()
	{
		datatablePermassions.Columns.Add("ScreenID", typeof(int));
		datatablePermassions.Columns.Add("كود ", typeof(int));
		datatablePermassions.Columns.Add("اسم الشاشة", typeof(string));
		datatablePermassions.Columns.Add("دخول", typeof(bool));
		datatablePermassions.Columns.Add("حفظ", typeof(bool));
		datatablePermassions.Columns.Add("تعديل", typeof(bool));
		datatablePermassions.Columns.Add("حذف", typeof(bool));
		datatablePermassions.Columns.Add("طباعة", typeof(bool));
		datatablePermassions.Columns.Add("إستخراج", typeof(bool));
	}

	private void BindGroups()
	{
		TxtGroups.DataSource = GRP.GetAllUsers().ToList();
		TxtGroups.DisplayMember = "Name";
		TxtGroups.ValueMember = "ID";
	}

	public void BindGridGroupsPrlivalges(int GroupId)
	{
		datatablePermassions.Clear();
		int num = 1;
		List<User_Permission> permassionByGroupID = GoupClass.GetPermassionByGroupID(GroupId);
		foreach (User_Screens item in from x in GoupClass.GetAllGroupsPermaion()
			orderby x.ScreenNum
			orderby x.ScreenTypeID
			select x)
		{
			User_Permission user_Permission = permassionByGroupID.Where((User_Permission x) => x.ScreenID == item.ID).FirstOrDefault();
			if (user_Permission == null)
			{
				datatablePermassions.Rows.Add(item.ID, num, item.Screen_Name, 0, 0, 0, 0, 0, 0);
			}
			else
			{
				datatablePermassions.Rows.Add(item.ID, num, item.Screen_Name, user_Permission.Allow_Enter, user_Permission.Allow_Save, user_Permission.Allow_Edit, user_Permission.Allow_Delete, user_Permission.Allow_Print, user_Permission.Allow_Export);
			}
			num++;
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void TxtGroups_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			GrpID = Convert.ToInt32(TxtGroups.SelectedValue);
			BindGridGroupsPrlivalges(Convert.ToInt32(TxtGroups.SelectedValue));
		}
		catch
		{
			GrpID = 1;
			BindGridGroupsPrlivalges(1);
		}
	}

	private void gridControl1_Click(object sender, EventArgs e)
	{
	}

	private void CheckAll_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckAll.IsOn)
			{
				row[3] = true;
				row[4] = true;
				row[5] = true;
				row[6] = true;
				row[7] = true;
			}
			else
			{
				row[3] = false;
				row[4] = false;
				row[5] = false;
				row[6] = false;
				row[7] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
		if (CheckAll.IsOn)
		{
			CheckEnter.IsOn = true;
			CheckEdit.IsOn = true;
			CheckDelete.IsOn = true;
			CheckPrint.IsOn = true;
			CheckSave.IsOn = true;
		}
		else
		{
			CheckEnter.IsOn = false;
			CheckEdit.IsOn = false;
			CheckDelete.IsOn = false;
			CheckPrint.IsOn = false;
			CheckSave.IsOn = false;
		}
	}

	private void CheckEnter_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckEnter.IsOn)
			{
				row[3] = true;
			}
			else
			{
				row[3] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void CheckSave_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckSave.IsOn)
			{
				row[4] = true;
			}
			else
			{
				row[4] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void CheckEdit_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckEdit.IsOn)
			{
				row[5] = true;
			}
			else
			{
				row[5] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void CheckDelete_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckDelete.IsOn)
			{
				row[6] = true;
			}
			else
			{
				row[6] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void CheckPrint_Toggled(object sender, EventArgs e)
	{
		foreach (DataRow row in datatablePermassions.Rows)
		{
			if (CheckPrint.IsOn)
			{
				row[7] = true;
			}
			else
			{
				row[7] = false;
			}
		}
		gridControl1.DataSource = datatablePermassions;
	}

	private void FrmUsers_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void FrmUsers_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F10")
		{
			FrmEditUser frmEditUser = new FrmEditUser();
			frmEditUser.ShowDialog();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Security.FrmSecurityGroup));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.BtnEditUser = new System.Windows.Forms.ToolStripMenuItem();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.panel9 = new System.Windows.Forms.Panel();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.CheckPrint = new DevExpress.XtraEditors.ToggleSwitch();
		this.CheckAll = new DevExpress.XtraEditors.ToggleSwitch();
		this.CheckEnter = new DevExpress.XtraEditors.ToggleSwitch();
		this.CheckSave = new DevExpress.XtraEditors.ToggleSwitch();
		this.CheckEdit = new DevExpress.XtraEditors.ToggleSwitch();
		this.CheckDelete = new DevExpress.XtraEditors.ToggleSwitch();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.TxtGroups = new System.Windows.Forms.ComboBox();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.contextMenuStrip1.SuspendLayout();
		this.panel9.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CheckPrint.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CheckAll.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CheckEnter.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CheckSave.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CheckEdit.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.CheckDelete.Properties).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDelete).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		base.SuspendLayout();
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.BtnEditUser });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.BtnEditUser.Name = "BtnEditUser";
		resources.ApplyResources(this.BtnEditUser, "BtnEditUser");
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 15;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 15;
		this.gridColumn18.Name = "gridColumn18";
		this.panel9.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
		this.panel9.Controls.Add(this.tableLayoutPanel1);
		resources.ApplyResources(this.panel9, "panel9");
		this.panel9.Name = "panel9";
		resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
		this.tableLayoutPanel1.Controls.Add(this.CheckPrint, 5, 0);
		this.tableLayoutPanel1.Controls.Add(this.CheckAll, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.CheckEnter, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.CheckSave, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.CheckEdit, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.CheckDelete, 4, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		resources.ApplyResources(this.CheckPrint, "CheckPrint");
		this.CheckPrint.Name = "CheckPrint";
		this.CheckPrint.Properties.OffText = resources.GetString("CheckPrint.Properties.OffText");
		this.CheckPrint.Properties.OnText = resources.GetString("CheckPrint.Properties.OnText");
		this.CheckPrint.Toggled += new System.EventHandler(CheckPrint_Toggled);
		resources.ApplyResources(this.CheckAll, "CheckAll");
		this.CheckAll.Name = "CheckAll";
		this.CheckAll.Properties.OffText = resources.GetString("CheckAll.Properties.OffText");
		this.CheckAll.Properties.OnText = resources.GetString("CheckAll.Properties.OnText");
		this.CheckAll.Toggled += new System.EventHandler(CheckAll_CheckedChanged);
		resources.ApplyResources(this.CheckEnter, "CheckEnter");
		this.CheckEnter.Name = "CheckEnter";
		this.CheckEnter.Properties.OffText = resources.GetString("CheckEnter.Properties.OffText");
		this.CheckEnter.Properties.OnText = resources.GetString("CheckEnter.Properties.OnText");
		this.CheckEnter.Toggled += new System.EventHandler(CheckEnter_CheckedChanged);
		resources.ApplyResources(this.CheckSave, "CheckSave");
		this.CheckSave.Name = "CheckSave";
		this.CheckSave.Properties.OffText = resources.GetString("CheckSave.Properties.OffText");
		this.CheckSave.Properties.OnText = resources.GetString("CheckSave.Properties.OnText");
		this.CheckSave.Toggled += new System.EventHandler(CheckSave_CheckedChanged);
		resources.ApplyResources(this.CheckEdit, "CheckEdit");
		this.CheckEdit.Name = "CheckEdit";
		this.CheckEdit.Properties.OffText = resources.GetString("CheckEdit.Properties.OffText");
		this.CheckEdit.Properties.OnText = resources.GetString("CheckEdit.Properties.OnText");
		this.CheckEdit.Toggled += new System.EventHandler(CheckEdit_CheckedChanged);
		resources.ApplyResources(this.CheckDelete, "CheckDelete");
		this.CheckDelete.Name = "CheckDelete";
		this.CheckDelete.Properties.OffText = resources.GetString("CheckDelete.Properties.OffText");
		this.CheckDelete.Properties.OnText = resources.GetString("CheckDelete.Properties.OnText");
		this.CheckDelete.Toggled += new System.EventHandler(CheckDelete_CheckedChanged);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.panel2);
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.Name = "panel1";
		this.panel2.BackColor = System.Drawing.Color.Transparent;
		this.panel2.Controls.Add(this.BtnSave);
		this.panel2.Controls.Add(this.TxtGroups);
		resources.ApplyResources(this.panel2, "panel2");
		this.panel2.Name = "panel2";
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.ForeColor = System.Drawing.Color.White;
		this.BtnSave.Appearance.Options.UseBackColor = true;
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.Appearance.Options.UseForeColor = true;
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSavePermission_Click_1);
		resources.ApplyResources(this.TxtGroups, "TxtGroups");
		this.TxtGroups.FormattingEnabled = true;
		this.TxtGroups.Name = "TxtGroups";
		this.TxtGroups.SelectedIndexChanged += new System.EventHandler(TxtGroups_SelectedIndexChanged);
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 15;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 15;
		this.gridColumn15.Name = "gridColumn15";
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Controls.Add(this.panel4);
		resources.ApplyResources(this.panel3, "panel3");
		this.panel3.Name = "panel3";
		this.panel4.BackColor = System.Drawing.Color.Transparent;
		this.panel4.Controls.Add(this.gridControl1);
		this.panel4.Controls.Add(this.panel9);
		resources.ApplyResources(this.panel4, "panel4");
		this.panel4.Name = "panel4";
		resources.ApplyResources(this.gridControl1, "gridControl1");
		this.gridControl1.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridControl1.EmbeddedNavigator.Appearance.Font = (System.Drawing.Font)resources.GetObject("gridControl1.EmbeddedNavigator.Appearance.Font");
		this.gridControl1.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
		this.gridControl1.EmbeddedNavigator.Appearance.Options.UseFont = true;
		this.gridControl1.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl1.EmbeddedNavigator.Margin");
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.repositoryItemDelete });
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
		this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridView1.Appearance.HeaderPanel.BackColor2 = (System.Drawing.Color)resources.GetObject("gridView1.Appearance.HeaderPanel.BackColor2");
		this.gridView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
		this.gridView1.Appearance.HeaderPanel.Options.UseBorderColor = true;
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		this.gridView1.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridView1.AppearancePrint.HeaderPanel.BackColor2 = (System.Drawing.Color)resources.GetObject("gridView1.AppearancePrint.HeaderPanel.BackColor2");
		this.gridView1.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.White;
		this.gridView1.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
		this.gridView1.AppearancePrint.HeaderPanel.Options.UseBorderColor = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn9, this.gridColumn2, this.gridColumn8, this.gridColumn1, this.gridColumn3, this.gridColumn4, this.gridColumn5, this.gridColumn6, this.gridColumn7 });
		this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(731, 189, 294, 574);
		this.gridView1.DetailHeight = 284;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
		this.gridView1.OptionsBehavior.KeepGroupExpandedOnSorting = false;
		this.gridView1.OptionsCustomization.AllowSort = false;
		this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.EnableAppearanceOddRow = true;
		this.gridView1.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		this.gridColumn9.AppearanceCell.BorderColor = System.Drawing.Color.White;
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseBorderColor = true;
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn9.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridColumn9.AppearanceHeader.BorderColor = System.Drawing.Color.White;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.ForeColor = System.Drawing.Color.White;
		this.gridColumn9.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn9.AppearanceHeader.Options.UseBorderColor = true;
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseForeColor = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.FieldName = "ScreenID";
		this.gridColumn9.MinWidth = 15;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn2.AppearanceCell.BorderColor = System.Drawing.Color.White;
		this.gridColumn2.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceCell.Font");
		this.gridColumn2.AppearanceCell.Options.UseBorderColor = true;
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridColumn2.AppearanceHeader.BorderColor = System.Drawing.Color.White;
		this.gridColumn2.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceHeader.Font");
		this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.White;
		this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn2.AppearanceHeader.Options.UseBorderColor = true;
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.ColumnEdit = this.repositoryItemDelete;
		this.gridColumn2.FieldName = "كود";
		this.gridColumn2.MinWidth = 15;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.repositoryItemDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.repositoryItemDelete.Appearance.BackColor2 = (System.Drawing.Color)resources.GetObject("repositoryItemDelete.Appearance.BackColor2");
		this.repositoryItemDelete.Appearance.ForeColor = System.Drawing.Color.White;
		this.repositoryItemDelete.Appearance.Options.UseBackColor = true;
		this.repositoryItemDelete.Appearance.Options.UseForeColor = true;
		this.repositoryItemDelete.Appearance.Options.UseImage = true;
		this.repositoryItemDelete.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.repositoryItemDelete.AppearanceDisabled.BackColor2 = (System.Drawing.Color)resources.GetObject("repositoryItemDelete.AppearanceDisabled.BackColor2");
		this.repositoryItemDelete.AppearanceDisabled.ForeColor = System.Drawing.Color.White;
		this.repositoryItemDelete.AppearanceDisabled.Options.UseBackColor = true;
		this.repositoryItemDelete.AppearanceDisabled.Options.UseForeColor = true;
		this.repositoryItemDelete.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.repositoryItemDelete.AppearanceFocused.BackColor2 = (System.Drawing.Color)resources.GetObject("repositoryItemDelete.AppearanceFocused.BackColor2");
		this.repositoryItemDelete.AppearanceFocused.ForeColor = System.Drawing.Color.White;
		this.repositoryItemDelete.AppearanceFocused.Options.UseBackColor = true;
		this.repositoryItemDelete.AppearanceFocused.Options.UseForeColor = true;
		this.repositoryItemDelete.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.repositoryItemDelete.AppearanceReadOnly.BackColor2 = (System.Drawing.Color)resources.GetObject("repositoryItemDelete.AppearanceReadOnly.BackColor2");
		this.repositoryItemDelete.AppearanceReadOnly.ForeColor = System.Drawing.Color.White;
		this.repositoryItemDelete.AppearanceReadOnly.Options.UseBackColor = true;
		this.repositoryItemDelete.AppearanceReadOnly.Options.UseForeColor = true;
		resources.ApplyResources(this.repositoryItemDelete, "repositoryItemDelete");
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions2");
		resources.ApplyResources(serializableAppearanceObject, "serializableAppearanceObject5");
		serializableAppearanceObject.Options.UseFont = true;
		this.repositoryItemDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDelete.Buttons"), resources.GetString("repositoryItemDelete.Buttons1"), (int)resources.GetObject("repositoryItemDelete.Buttons2"), (bool)resources.GetObject("repositoryItemDelete.Buttons3"), (bool)resources.GetObject("repositoryItemDelete.Buttons4"), (bool)resources.GetObject("repositoryItemDelete.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("repositoryItemDelete.Buttons6"), resources.GetObject("repositoryItemDelete.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemDelete.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemDelete.Buttons9"))
		});
		this.repositoryItemDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
		this.repositoryItemDelete.Name = "repositoryItemDelete";
		this.repositoryItemDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.gridColumn8.AppearanceCell.BorderColor = System.Drawing.Color.White;
		this.gridColumn8.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceCell.Font");
		this.gridColumn8.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn8.AppearanceCell.FontStyleDelta");
		this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn8.AppearanceCell.Options.UseBorderColor = true;
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn8.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 68);
		this.gridColumn8.AppearanceHeader.BorderColor = System.Drawing.Color.White;
		this.gridColumn8.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceHeader.Font");
		this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.White;
		this.gridColumn8.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn8.AppearanceHeader.Options.UseBorderColor = true;
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		resources.ApplyResources(this.gridColumn8, "gridColumn8");
		this.gridColumn8.FieldName = "اسم الشاشة";
		this.gridColumn8.MinWidth = 15;
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.FieldName = "دخول";
		this.gridColumn1.MinWidth = 23;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.FieldName = "حفظ";
		this.gridColumn3.MinWidth = 23;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.FieldName = "تعديل";
		this.gridColumn4.MinWidth = 23;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn5.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceCell.Font");
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn5.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn5.AppearanceHeader.Font");
		this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn5, "gridColumn5");
		this.gridColumn5.FieldName = "حذف";
		this.gridColumn5.MinWidth = 23;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.FieldName = "طباعة";
		this.gridColumn6.MinWidth = 23;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(3, 40, 86);
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.FieldName = "إستخراج";
		this.gridColumn7.MinWidth = 23;
		this.gridColumn7.Name = "gridColumn7";
		this.cardView1.CardWidth = 149;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 284;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 15;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 15;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 15;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 15;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 15;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FrmSecurityGroup.IconOptions.Icon");
		base.Name = "FrmSecurityGroup";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FrmUsers_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmUsers_KeyDown);
		this.contextMenuStrip1.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.CheckPrint.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CheckAll.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CheckEnter.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CheckSave.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CheckEdit.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.CheckDelete.Properties).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDelete).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		base.ResumeLayout(false);
	}
}


