using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Behaviors;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using GTSErpSystem.BLL.Account;

namespace GTSErpSystem.Frms.Account;

public class FrmCostCenterTree : XtraForm
{
	private Class_AccountCard Acct = new Class_AccountCard();

	private Calss_CostCenter UNT = new Calss_CostCenter();

	private DataTable datatable = new DataTable();

	public int AccountSN;

	public int AccountBro;

	private IContainer components = null;

	private TreeList treeList1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem ToolStripMenuItemEdit;

	private ToolStripMenuItem ToolStripMenuItemDelete;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem ToolStripMenuItemBrother;

	private ToolStripMenuItem ToolStripMenuItemSon;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem ToolStripMenuItemclose;

	private BehaviorManager behaviorManager1;

	private NavBarControl navBarControl2;

	private NavBarGroup navBarGroup12;

	private NavBarItem NavAddAccount;

	private NavBarItem NavAddAccountSon;

	private NavBarItem NavEdit;

	private NavBarItem NavDelete;

	private NavBarItem NavVerg;

	private NavBarItem NavExit;

	private Label label1;

	public FrmCostCenterTree()
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
		GTSdbEntities dbContext = new GTSdbEntities();
		dbContext.ViewCostCenters_Data_Tree.LoadAsync().ContinueWith(delegate
		{
			treeList1.DataSource = dbContext.ViewCostCenters_Data_Tree.Local.ToBindingList();
			treeList1.KeyFieldName = "CostCentersID";
			treeList1.ParentFieldName = "MainCostCentersID";
			treeList1.Columns.Clear();
			TreeListColumn treeListColumn = treeList1.Columns.Add();
			treeListColumn.FieldName = "اسم_الحساب";
			treeListColumn.Caption = "اسم الحساب";
			treeListColumn.Visible = true;
			treeList1.CollapseAll();
			for (int i = 0; i < treeList1.Nodes.Count; i++)
			{
				treeList1.Nodes[i].Expanded = true;
			}
			treeList1.Focus();
		}, TaskScheduler.FromCurrentSynchronizationContext());
		treeList1.OptionsBehavior.Editable = false;
		treeList1.TreeLineStyle = LineStyle.Solid;
		Skin skin = GridSkins.GetSkin(treeList1.LookAndFeel);
		skin.Properties[GridSkins.OptShowTreeLine] = true;
		treeList1.OptionsView.ShowIndicator = false;
		treeList1.OptionsView.ShowVertLines = false;
		treeList1.LookAndFeel.UpdateStyleSettings();
		treeList1.CustomDrawNodeCell += treeList1_CustomDrawNodeCell;
	}

	private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
	{
		if (e.Node == treeList1.FocusedNode)
		{
			e.Appearance.BackColor = Color.FromArgb(200, 220, 255);
			e.Appearance.ForeColor = Color.Black;
			e.Appearance.Options.UseBackColor = true;
			e.Appearance.Options.UseForeColor = true;
		}
	}

	private void treeList1_DoubleClick(object sender, EventArgs e)
	{
		TreeList treeList = sender as TreeList;
		TreeListHitInfo treeListHitInfo = treeList.CalcHitInfo(treeList.PointToClient(Control.MousePosition));
		if (treeListHitInfo.Node == null)
		{
		}
	}

	private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
	{
		try
		{
			if (e.Node != null)
			{
				object value = e.Node.GetValue("CostCentersID");
				object value2 = e.Node.GetValue("MainCostCentersID");
				if (value != null)
				{
					AccountSN = Convert.ToInt32(value);
				}
				if (value2 != null)
				{
					AccountBro = Convert.ToInt32(value2);
				}
			}
		}
		catch
		{
		}
	}

	private void Delete()
	{
		int num = 0;
		num = UNT.Delete(AccountSN);
		if (num > 0)
		{
			MessageBox.Show("تم حذف بيانات الحسابات بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			NavVerg_LinkClicked(null, null);
		}
		else
		{
			MessageBox.Show("عفوا، فشل حذف بيانات الحساب لوجود حركات علي هذا الحساب او وجود حساب ابن تحت هذا الحساب", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		try
		{
			LoginDetails.AccountSN = AccountSN;
			LoginDetails.ActionName = "Edit";
			FrmCostCenter frmCostCenter = new FrmCostCenter();
			frmCostCenter.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		NavVerg_LinkClicked(null, null);
	}

	private void CreateSon()
	{
		try
		{
			LoginDetails.AccountSN = AccountSN;
			LoginDetails.ActionName = "Son";
			FrmCostCenter frmCostCenter = new FrmCostCenter();
			frmCostCenter.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		NavVerg_LinkClicked(null, null);
	}

	private void CreateBrother()
	{
		try
		{
			LoginDetails.AccountBrother = AccountBro;
			LoginDetails.ActionName = "Bro";
			FrmCostCenter frmCostCenter = new FrmCostCenter();
			frmCostCenter.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		NavVerg_LinkClicked(null, null);
	}

	private void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void ToolStripMenuItemBrother_Click(object sender, EventArgs e)
	{
		CreateBrother();
	}

	private void ToolStripMenuItemSon_Click(object sender, EventArgs e)
	{
		CreateSon();
	}

	private void NavAddAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		CreateBrother();
	}

	private void NavAddAccountSon_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		CreateSon();
	}

	private void NavEdit_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		Edit();
	}

	private void NavDelete_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		Delete();
	}

	private void NavExit_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		Close();
	}

	private void NavVerg_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		GTSdbEntities dbContext = new GTSdbEntities();
		dbContext.ViewCostCenters_Data_Tree.LoadAsync().ContinueWith(delegate
		{
			treeList1.DataSource = dbContext.ViewCostCenters_Data_Tree.Local.ToBindingList();
			treeList1.KeyFieldName = "CostCentersID";
			treeList1.ParentFieldName = "MainCostCentersID";
			treeList1.Columns.Clear();
			TreeListColumn treeListColumn = treeList1.Columns.Add();
			treeListColumn.FieldName = "اسم_الحساب";
			treeListColumn.Caption = "اسم الحساب";
			treeListColumn.Visible = true;
			treeList1.CollapseAll();
			for (int i = 0; i < treeList1.Nodes.Count; i++)
			{
				treeList1.Nodes[i].Expanded = true;
			}
		}, TaskScheduler.FromCurrentSynchronizationContext());
	}

	private void navBarControl2_Click(object sender, EventArgs e)
	{
	}

	private void ToolStripMenuItemclose_Click(object sender, EventArgs e)
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Account.FrmCostCenterTree));
		this.treeList1 = new DevExpress.XtraTreeList.TreeList();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripMenuItemBrother = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItemSon = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripMenuItemclose = new System.Windows.Forms.ToolStripMenuItem();
		this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
		this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
		this.navBarGroup12 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavAddAccount = new DevExpress.XtraNavBar.NavBarItem();
		this.NavAddAccountSon = new DevExpress.XtraNavBar.NavBarItem();
		this.NavEdit = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDelete = new DevExpress.XtraNavBar.NavBarItem();
		this.NavVerg = new DevExpress.XtraNavBar.NavBarItem();
		this.NavExit = new DevExpress.XtraNavBar.NavBarItem();
		this.label1 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.treeList1).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.navBarControl2).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.treeList1, "treeList1");
		this.treeList1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("treeList1.Appearance.HeaderPanel.Font");
		this.treeList1.Appearance.HeaderPanel.Options.UseFont = true;
		this.treeList1.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("treeList1.Appearance.Row.Font");
		this.treeList1.Appearance.Row.Options.UseFont = true;
		this.treeList1.Appearance.SelectedRow.Font = (System.Drawing.Font)resources.GetObject("treeList1.Appearance.SelectedRow.Font");
		this.treeList1.Appearance.SelectedRow.Options.UseFont = true;
		this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
		this.treeList1.Name = "treeList1";
		this.treeList1.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.True;
		this.treeList1.OptionsView.AnimationType = DevExpress.XtraTreeList.TreeListAnimationType.AnimateFocusedNode;
		this.treeList1.OptionsView.ShowAutoFilterRow = true;
		this.treeList1.ParentFieldName = "Main_Account_No";
		this.treeList1.TreeLevelWidth = 20;
		this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged);
		this.treeList1.DoubleClick += new System.EventHandler(treeList1_DoubleClick);
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.ToolStripMenuItemEdit, this.ToolStripMenuItemDelete, this.toolStripSeparator2, this.ToolStripMenuItemBrother, this.ToolStripMenuItemSon, this.toolStripSeparator1, this.ToolStripMenuItemclose });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
		resources.ApplyResources(this.ToolStripMenuItemEdit, "ToolStripMenuItemEdit");
		this.ToolStripMenuItemEdit.Click += new System.EventHandler(ToolStripMenuItemEdit_Click);
		this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
		resources.ApplyResources(this.ToolStripMenuItemDelete, "ToolStripMenuItemDelete");
		this.ToolStripMenuItemDelete.Click += new System.EventHandler(ToolStripMenuItemDelete_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.ToolStripMenuItemBrother.Name = "ToolStripMenuItemBrother";
		resources.ApplyResources(this.ToolStripMenuItemBrother, "ToolStripMenuItemBrother");
		this.ToolStripMenuItemBrother.Click += new System.EventHandler(ToolStripMenuItemBrother_Click);
		this.ToolStripMenuItemSon.Name = "ToolStripMenuItemSon";
		resources.ApplyResources(this.ToolStripMenuItemSon, "ToolStripMenuItemSon");
		this.ToolStripMenuItemSon.Click += new System.EventHandler(ToolStripMenuItemSon_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.ToolStripMenuItemclose.Name = "ToolStripMenuItemclose";
		resources.ApplyResources(this.ToolStripMenuItemclose, "ToolStripMenuItemclose");
		this.ToolStripMenuItemclose.Click += new System.EventHandler(ToolStripMenuItemclose_Click);
		this.navBarControl2.ActiveGroup = this.navBarGroup12;
		resources.ApplyResources(this.navBarControl2, "navBarControl2");
		this.navBarControl2.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[1] { this.navBarGroup12 });
		this.navBarControl2.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[6] { this.NavAddAccount, this.NavAddAccountSon, this.NavEdit, this.NavDelete, this.NavVerg, this.NavExit });
		this.navBarControl2.Name = "navBarControl2";
		this.navBarControl2.OptionsNavPane.ExpandedWidth = (int)resources.GetObject("resource.ExpandedWidth");
		this.navBarControl2.Click += new System.EventHandler(navBarControl2_Click);
		resources.ApplyResources(this.navBarGroup12, "navBarGroup12");
		this.navBarGroup12.Expanded = true;
		this.navBarGroup12.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[6]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavAddAccount),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavAddAccountSon),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavEdit),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDelete),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavVerg),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavExit)
		});
		this.navBarGroup12.Name = "navBarGroup12";
		this.NavAddAccount.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavAddAccount.Appearance.Font");
		this.NavAddAccount.Appearance.Options.UseFont = true;
		this.NavAddAccount.Appearance.Options.UseTextOptions = true;
		this.NavAddAccount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavAddAccount.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavAddAccount.AppearanceDisabled.Font");
		this.NavAddAccount.AppearanceDisabled.Options.UseFont = true;
		this.NavAddAccount.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavAddAccount.AppearanceHotTracked.Font");
		this.NavAddAccount.AppearanceHotTracked.Options.UseFont = true;
		this.NavAddAccount.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavAddAccount.AppearancePressed.Font");
		this.NavAddAccount.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavAddAccount, "NavAddAccount");
		this.NavAddAccount.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavAddAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavAddAccount.ImageOptions.SvgImage");
		this.NavAddAccount.Name = "NavAddAccount";
		this.NavAddAccount.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavAddAccount_LinkClicked);
		this.NavAddAccountSon.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavAddAccountSon.Appearance.Font");
		this.NavAddAccountSon.Appearance.Options.UseFont = true;
		this.NavAddAccountSon.Appearance.Options.UseTextOptions = true;
		this.NavAddAccountSon.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavAddAccountSon.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavAddAccountSon.AppearanceDisabled.Font");
		this.NavAddAccountSon.AppearanceDisabled.Options.UseFont = true;
		this.NavAddAccountSon.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavAddAccountSon.AppearanceHotTracked.Font");
		this.NavAddAccountSon.AppearanceHotTracked.Options.UseFont = true;
		this.NavAddAccountSon.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavAddAccountSon.AppearancePressed.Font");
		this.NavAddAccountSon.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavAddAccountSon, "NavAddAccountSon");
		this.NavAddAccountSon.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavAddAccountSon.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavAddAccountSon.ImageOptions.SvgImage");
		this.NavAddAccountSon.Name = "NavAddAccountSon";
		this.NavAddAccountSon.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavAddAccountSon_LinkClicked);
		this.NavEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavEdit.Appearance.Font");
		this.NavEdit.Appearance.Options.UseFont = true;
		this.NavEdit.Appearance.Options.UseTextOptions = true;
		this.NavEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavEdit.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavEdit.AppearanceDisabled.Font");
		this.NavEdit.AppearanceDisabled.Options.UseFont = true;
		this.NavEdit.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavEdit.AppearanceHotTracked.Font");
		this.NavEdit.AppearanceHotTracked.Options.UseFont = true;
		this.NavEdit.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavEdit.AppearancePressed.Font");
		this.NavEdit.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavEdit, "NavEdit");
		this.NavEdit.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavEdit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavEdit.ImageOptions.SvgImage");
		this.NavEdit.Name = "NavEdit";
		this.NavEdit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavEdit_LinkClicked);
		this.NavDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavDelete.Appearance.Font");
		this.NavDelete.Appearance.Options.UseFont = true;
		this.NavDelete.Appearance.Options.UseTextOptions = true;
		this.NavDelete.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavDelete.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavDelete.AppearanceDisabled.Font");
		this.NavDelete.AppearanceDisabled.Options.UseFont = true;
		this.NavDelete.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavDelete.AppearanceHotTracked.Font");
		this.NavDelete.AppearanceHotTracked.Options.UseFont = true;
		this.NavDelete.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavDelete.AppearancePressed.Font");
		this.NavDelete.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavDelete, "NavDelete");
		this.NavDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavDelete.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavDelete.ImageOptions.SvgImage");
		this.NavDelete.Name = "NavDelete";
		this.NavDelete.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavDelete_LinkClicked);
		this.NavVerg.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavVerg.Appearance.Font");
		this.NavVerg.Appearance.Options.UseFont = true;
		this.NavVerg.Appearance.Options.UseTextOptions = true;
		this.NavVerg.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavVerg.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavVerg.AppearanceDisabled.Font");
		this.NavVerg.AppearanceDisabled.Options.UseFont = true;
		this.NavVerg.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavVerg.AppearanceHotTracked.Font");
		this.NavVerg.AppearanceHotTracked.Options.UseFont = true;
		this.NavVerg.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavVerg.AppearancePressed.Font");
		this.NavVerg.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavVerg, "NavVerg");
		this.NavVerg.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavVerg.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavVerg.ImageOptions.SvgImage");
		this.NavVerg.Name = "NavVerg";
		this.NavVerg.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavVerg_LinkClicked);
		this.NavExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("NavExit.Appearance.Font");
		this.NavExit.Appearance.Options.UseFont = true;
		this.NavExit.Appearance.Options.UseTextOptions = true;
		this.NavExit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.NavExit.AppearanceDisabled.Font = (System.Drawing.Font)resources.GetObject("NavExit.AppearanceDisabled.Font");
		this.NavExit.AppearanceDisabled.Options.UseFont = true;
		this.NavExit.AppearanceHotTracked.Font = (System.Drawing.Font)resources.GetObject("NavExit.AppearanceHotTracked.Font");
		this.NavExit.AppearanceHotTracked.Options.UseFont = true;
		this.NavExit.AppearancePressed.Font = (System.Drawing.Font)resources.GetObject("NavExit.AppearancePressed.Font");
		this.NavExit.AppearancePressed.Options.UseFont = true;
		resources.ApplyResources(this.NavExit, "NavExit");
		this.NavExit.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
		this.NavExit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("NavExit.ImageOptions.SvgImage");
		this.NavExit.Name = "NavExit";
		this.NavExit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavExit_LinkClicked);
		resources.ApplyResources(this.label1, "label1");
		this.label1.BackColor = System.Drawing.Color.FromArgb(246, 248, 255);
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Name = "label1";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label1);
		base.Controls.Add(this.navBarControl2);
		base.Controls.Add(this.treeList1);
		base.Name = "FrmCostCenterTree";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		((System.ComponentModel.ISupportInitialize)this.treeList1).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.navBarControl2).EndInit();
		base.ResumeLayout(false);
	}
}


