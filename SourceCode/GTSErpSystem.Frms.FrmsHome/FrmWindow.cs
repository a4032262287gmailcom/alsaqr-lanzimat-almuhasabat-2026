using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Behaviors;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Reports;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmRestaurant;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Orders;
using GTSErpSystem.Frms.Payroll;
using GTSErpSystem.Frms.Reports.Account;
using GTSErpSystem.Frms.Reports.Orders;
using GTSErpSystem.Frms.Reports.Profit;
using GTSErpSystem.Frms.Reports.Public;
using GTSErpSystem.Frms.Reports.Purches;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Frms.Security;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.FrmsHome;

public class FrmWindow : RibbonForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Setting STTG = new Class_Setting();

	private IContainer components = null;

	private TabbedView tabbedView;

	private RibbonControl ribbonControl;

	private RibbonPage ribbonPage;

	private RibbonStatusBar ribbonStatusBar;

	private RibbonPageGroup ribbonPageGroupNavigation;

	private SkinRibbonGalleryBarItem skinRibbonGalleryBarItem;

	private NavigationFrame navigationFrame;

	private NavigationPage employeesNavigationPage;

	private BarButtonItem employeesBarButtonItem;

	private BarButtonItem customersBarButtonItem;

	private RibbonPage ribbonPage1;

	private RibbonPageGroup ribbonPageGroup1;

	private RibbonPage ribbonPage2;

	private RibbonPageGroup ribbonPageGroup2;

	private RibbonPage ribbonPage3;

	private RibbonPageGroup ribbonPageGroup3;

	private RibbonPage ribbonPage4;

	private RibbonPageGroup ribbonPageGroup4;

	private RibbonPage ribbonPage5;

	private RibbonPageGroup ribbonPageGroup5;

	private RibbonPage ribbonPage6;

	private RibbonPageGroup ribbonPageGroup6;

	private SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;

	private RepositoryItemColorEdit repositoryItemColorEdit1;

	private RibbonPageGroup ribbonPageGroup;

	private BarSubItem barSubItem1;

	private BarSubItem barSubItem2;

	private BarSubItem barSubItem3;

	private BarSubItem barSubItem4;

	private RepositoryItemColorPickEdit repositoryItemColorPickEdit1;

	private RepositoryItemColorEdit repositoryItemColorEdit2;

	private BarButtonItem BtnORDOpenQuantity;

	private BarButtonItem BtnORDPriceOffer;

	private BarButtonItem BtnORDOrder;

	private BarButtonItem BtnORDPurchaseOrder;

	private BarButtonItem BtnORDPurchase;

	private BarButtonItem BtnORDOrderReturnID;

	private BarButtonItem BtnORDPurchaseReturnID;

	private BarButtonItem BtnORDOrderReturnNotID;

	private BarButtonItem BtnORDPurchaseReturnNotID;

	private BarButtonItem barButtonItem10;

	private BarButtonItem barButtonItem11;

	private BarButtonItem barButtonItem12;

	private BarButtonItem barButtonItem13;

	private BarButtonItem barButtonItem14;

	private BarButtonItem barButtonItem15;

	private BarButtonItem BtnItemGroups;

	private BarButtonItem BtnItemUnits;

	private BarButtonItem BtnItemClass;

	private BarButtonItem BtnItemCompany;

	private BarButtonItem BtnItem;

	private BarButtonItem BtnStores;

	private BarButtonItem BtnBranches;

	private BarButtonItem barButtonItem23;

	private BarButtonItem barButtonItem24;

	private BarButtonItem BtnCusPlace;

	private BarButtonItem BtnCustomer;

	private BarButtonItem BtnCusSupplier;

	private BarButtonItem BtnCusSalesMan;

	private RibbonPageGroup ribbonPageGroup9;

	private RibbonPageGroup ribbonPageGroup10;

	private ApplicationMenu applicationMenu1;

	private BarButtonItem BtnUsers;

	private BarButtonItem BtnGroups;

	private BarButtonItem BtnPermission;

	private RibbonPageGroup ribbonPageGroup7;

	private RibbonPageGroup ribbonPageGroup8;

	private BarButtonItem BtnCashir;

	private BarButtonItem BtnRecipt;

	private BarButtonItem BtnPayment;

	private BarButtonItem BtnAccCardAccount;

	private BarButtonItem BtnAccTreeAccount;

	private BarButtonItem BtnAccCostCenter;

	private BarButtonItem BtnAccOpenMoney;

	private BarButtonItem BtnAccRestrictionsTest;

	private BarButtonItem BtnAccRestrictions;

	private BarButtonItem barButtonItem7;

	private BarButtonItem barButtonItem8;

	private RibbonPageGroup ribbonPageGroup11;

	private BarSubItem BtnReportAccount;

	private BarButtonItem BtnReportMizan;

	private BarButtonItem BtnRPProfitAccount;

	private BarButtonItem BtnSetting;

	private BarButtonItem BtnReportDesign;

	private BarButtonItem btnDefualtAccount;

	private BarStaticItem LblCompany;

	private BarStaticItem LblServer;

	private BarStaticItem LblUser;

	private BarStaticItem LblTime;

	private BarStaticItem LblDate;

	private BarStaticItem barStaticItem5;

	private Timer timer1;

	private BarSubItem BtnReportPublic;

	private BarButtonItem BtnMovementsDailyReport;

	private BarSubItem BtnReportStore;

	private BarButtonItem BtnRPGard;

	private BarButtonItem BtnPrinters;

	private BarButtonItem BtnAbout;

	private BarSubItem BtnReportOrder;

	private BarSubItem BtnReportPurshes;

	private RibbonPageGroup ribbonPageGroup12;

	private BarButtonItem BtnReportVAT;

	private BarButtonItem barButtonItem20;

	private BarButtonItem barButtonItem21;

	private BarButtonItem barButtonItem22;

	private BarButtonItem BtnReportAllOrders;

	private BarButtonItem BtnReportAllOrdersDetails;

	private BarButtonItem BtnReportAllOrdersReturn;

	private BarButtonItem BtnReportAllOrdersReturnDetails;

	private BarButtonItem BtnReportSalesEmployee;

	private BarButtonItem BtnReportAllPurches;

	private BarButtonItem BtnReportAllPurchesDetails;

	private BarButtonItem BtnReportAllPurchesReturn;

	private BarButtonItem BtnReportAllPurchesReturnDetails;

	private BarSubItem BtnReportProfit;

	private BarButtonItem BtnRPOrderProfit;

	private BarButtonItem BtnRPProfitByItems;

	private BarButtonItem barButtonItem6;

	private BarSubItem barSubItem5;

	private RibbonPageGroup ribbonPageGroup14;

	private RibbonPageGroup ribbonPageGroup15;

	private RibbonPageGroup ribbonPageGroup16;

	private RibbonPageGroup ribbonPageGroup13;

	private BarCheckItem barCheckItem1;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private BarEditItem DateApp;

	private RepositoryItemDateEdit repositoryItemDateEdit2;

	private BarButtonItem BtnUpdateDateApp;

	private RibbonPageGroup ribbonPageGroup18;

	private BarEditItem BtnDateApp;

	private RepositoryItemDateEdit repositoryItemDateEdit3;

	private RepositoryItemTimeSpanEdit repositoryItemTimeSpanEdit1;

	private RepositoryItemTimeEdit repositoryItemTimeEdit1;

	private BarEditItem barEditItem1;

	private RepositoryItemDateEdit repositoryItemDateEdit4;

	private BarStaticItem LblVerg;

	private BarButtonItem BtnReportAllOrdersRestaurant;

	private BarButtonItem BtnCheckOutOrder;

	private BarButtonItem BtnConnection;

	private RibbonPageGroup ribbonPageGroup17;

	private BarButtonItem BtnRPMovementItem;

	private BarButtonItem BtnTransfer;

	private BarButtonItem BtnRPMizanTotal;

	private BarButtonItem BtnRPMizan;

	private BarButtonItem BtnRPMizanGroups;

	private BarSubItem BtnDabase;

	private BarSubItem BtnDataBase;

	private BarButtonItem BtnClearDB;

	private BarButtonItem BtnEmptyDB;

	private BarSubItem barSubItem6;

	private BarButtonItem BtnRPCustomer;

	private BarButtonItem BtnRPSupp;

	private RibbonPageGroup ribbonPageGroup19;

	private BarButtonItem BtnRPLeadgerCustomer;

	private BarButtonItem BtnRPLeadgerSupp;

	private BarButtonItem BtnRPProfitByItemsItemOnly;

	private BarButtonItem BtnRPStoreAvergByItem;

	private BarButtonItem BtnRPStoreAvergByGroup;

	private BarButtonItem BtnRPTranTeacher;

	private BarButtonItem BtnBackup;

	private BarButtonItem BtnVat;

	private BarButtonItem BtnReportAllMovementDays;

	private BehaviorManager behaviorManager1;

	private RibbonPageGroup ribbonPageGroup20;

	private RibbonPageGroup ribbonPageGroup21;

	private BarCheckItem barCheckItem2;

	private BarButtonItem BtnEncriptPassword;

	private BarButtonItem barButtonItem1;

	private BarButtonItem barButtonItem2;

	private BarButtonItem BtnLock;

	private RibbonPageGroup ribbonPageGroup22;

	private BarButtonItem BtnReportTobaccoTax;

	private BarButtonItem BtnEmployee;

	private BarButtonItem BtnPayrollTest;

	private BarButtonItem BtnPayroll;

	private BarButtonItem BtnReportLedger;

	private BarButtonItem BtnRestraunt;

	private NavBarControl navBarControl1;

	private NavBarGroup navBarGroup1;

	private NavBarItem NavUnit;

	private NavBarItem NavGroups;

	private NavBarItem NavCompnys;

	private NavBarItem NavClass;

	private NavBarGroup navBarGroup2;

	private NavBarGroup navBarGroup3;

	private NavBarGroup navBarGroup4;

	private NavBarGroup navBarGroup5;

	private NavBarGroup navBarGroup6;

	private NavBarGroup navBarGroup7;

	private NavBarGroup navBarGroup8;

	private NavBarGroup navBarGroup9;

	private NavBarGroup navBarGroup10;

	private NavBarItem NavPriceOffer;

	private NavBarItem NavOrders;

	private NavBarItem NavCashier;

	private NavBarItem NavPurchesOrder;

	private NavBarItem NavPurches;

	private NavBarSeparatorItem navBarSeparatorItem7;

	private NavBarItem NavPurchesReturn;

	private NavBarItem NavPurchesReturnNoID;

	private NavBarItem NavOpenQuantity;

	private NavBarItem NavStoreTransfer;

	private NavBarItem NavCustomer;

	private NavBarItem NavSuppliers;

	private NavBarItem NavSalesMan;

	private NavBarItem NavPlaceCus;

	private NavBarItem NavItemAccount;

	private NavBarItem NavTreeAccount;

	private NavBarSeparatorItem navBarSeparatorItem8;

	private NavBarItem NavDailyRestrictionsTest;

	private NavBarItem NavDailyRestrictions;

	private NavBarItem NavReceipts;

	private NavBarItem NavPayment;

	private NavBarItem NavUsers;

	private NavBarItem NavGroup;

	private NavBarSeparatorItem navBarSeparatorItem2;

	private NavBarItem NanPermission;

	private NavBarItem NavSetting;

	private NavBarItem NavPrinter;

	private NavBarItem NavClear;

	private NavBarItem NavClearAll;

	private NavBarGroup navBarGroup11;

	private NavBarItem NavAbout;

	private NavBarItem NavExit;

	private SkinRibbonGalleryBarItem skinRibbonGalleryBarItem2;

	private NavBarControl navBarControl2;

	private NavBarGroup navBarGroup12;

	private NavBarItem NavCompany;

	private NavBarItem NavUser;

	private NavBarItem NavDataBase;

	private NavBarItem NavYearClosing;

	private NavBarItem NavVerg;

	private NavBarItem NavDate;

	private Label label1;

	private NavBarItem NavServer;

	private NavBarItem NavCountry;

	private NavBarItem NavGuarantee;

	private NavBarItem NavItem;

	private NavBarItem NavGroupRes;

	private NavBarItem NavResturant;

	private NavBarItem NavOrderOut;

	private NavBarSeparatorItem navBarSeparatorItem1;

	private NavBarItem NavReturnOrder;

	private NavBarItem NavReturnOrderNoID;

	private NavBarItem NavDateCasher;

	private NavBarItem NavPassword;

	private NavBarSeparatorItem navBarSeparatorItem3;

	private NavBarItem NavReceiptsAll;

	private NavBarItem NavPaymentAll;

	private NavBarItem NavReport;

	private NavBarItem NavDesign;

	private NavBarItem NavAccountDefualt;

	private NavBarItem NavCustomerDefualt;

	public FrmWindow()
	{
		InitializeComponent();
		UserLookAndFeel.Default.SkinName = "Lilian";
		UserLookAndFeel.Default.SkinMaskColor = Color.White;
		LblCompany.Caption = "برنامج مايكرو للمحاسبة ونقاط البيع مرخص الي : " + LoginDetails.CompanyNameAR;
		LblServer.Caption = "السيرفر: " + LoginDetails.nameServer + " / " + LoginDetails.databaseServer;
		LblUser.Caption = "الموظف: " + LoginDetails.UserName;
		LblVerg.Caption = "اصدار:  V.20.02.26";
		string caption = " ( " + DateTime.Today.ToString("  dd / MM /yyyy  ") + " )  : التاريخ ";
		NavCompany.Caption = "اسم الشركة :  ( " + LoginDetails.CompanyNameAR + " ) ";
		NavUser.Caption = "الموظف :  ( " + LoginDetails.UserName + " ) ";
		NavDataBase.Caption = " ( " + LoginDetails.databaseServer + " ) : قاعدة البيانات ";
		NavDate.Caption = caption;
		NavVerg.Caption = " (  V.20.02.26 )  : الاصدار ";
		NavYearClosing.Caption = " ( 2020 ) : السنة المالية ";
		NavServer.Caption = " ( " + LoginDetails.nameServer + " )  : اسم السيرفر";
		SetDateApp();
		ribbonControl.SelectedPage = ribbonPage1;
	}

	private void navBarControl_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
	{
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.L && e.Modifiers == Keys.Control)
		{
			new FrmLock().ShowDialog();
		}
	}

	private void BtnUsers_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 1))
		{
			new FrmUsers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnGroups_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 2))
		{
			new FrmGroups().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPermission_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 3))
		{
			new FrmSecurityGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemCompany_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 4))
		{
			new FrmCompany().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemGroups_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 7))
		{
			new FrmGroupsRes().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemUnits_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 5))
		{
			new FrmUnit().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemClass_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 6))
		{
			new FrmClass().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItem_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmItems().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDOpenQuantity_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 18))
		{
			new FrmOpenQuantity().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPriceOffer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 10))
		{
			new FrmPriceOffer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCashir_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashir().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDOrder_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmOrders(null).Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPurchaseOrder_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 11))
		{
			new FrmPurchaseOrder().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPurchase_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 13))
		{
			new FrmPurchases().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDOrderReturnID_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 14))
		{
			new FrmOrderReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPurchaseReturnID_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 15))
		{
			new FrmPurchasesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDOrderReturnNotID_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 16))
		{
			new FrmOrdersReturnNoPurchCode().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnORDPurchaseReturnNotID_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 17))
		{
			new FrmPurchasesReturnNoPurchCode().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCusPlace_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 21))
		{
			new FrmPlace().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCusSalesMan_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 22))
		{
			new FrmSalesMan().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCustomer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 23))
		{
			new FrmCustomer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCusSupplier_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 24))
		{
			new FrmSuppliers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnBranches_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 26))
		{
			new FrmBranches().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnStores_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 25))
		{
			new FrmStores().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilPurches_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 13))
		{
			new FrmPurchases().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilPurchesReturn_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 15))
		{
			new FrmPurchasesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilPurchesOrder_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 11))
		{
			new FrmPurchaseOrder().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileOrder_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmOrders(null).Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileOrderReturn_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 14))
		{
			new FrmOrderReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilOfferPrice_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 10))
		{
			new FrmPriceOffer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilItems_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashir().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilStoresTransfer_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 20))
		{
			new FrmStoresTransfer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileReceipt_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilePayment_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 32))
		{
			new FrmPayment().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilCustomer_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 23))
		{
			new FrmCustomer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilSupplier_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 24))
		{
			new FrmSuppliers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileUser_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 1))
		{
			new FrmUsers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilePermission_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 3))
		{
			new FrmSecurityGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilAccountQuid_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 36))
		{
			new FrmDailyRestrictions().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilAccountDefualt_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 38))
		{
			new FrmSetting().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileSetting_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 38))
		{
			new FrmSetting().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TileDesignReport_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 40))
		{
			new FrmDesignerReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void TilExit_ItemClick(object sender, TileItemEventArgs e)
	{
		switch (MessageBox.Show("هل تريد الخروج من برنامج مايكرو للمحاسبة ونقاط البيع", "خروج من البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
		{
		case DialogResult.Yes:
			Application.Exit();
			break;
		}
	}

	private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason != CloseReason.UserClosing)
		{
			return;
		}
		DialogResult dialogResult = MessageBox.Show("هل انت متأكد من اغلاق برنامج مايكرو للمحاسبة ونقاط البيع", "اغلاق البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		if (dialogResult == DialogResult.Yes)
		{
			if (LoginDetails.IsBackupWhenClose)
			{
				int num = STTG.TakeBackup();
				if (num > 0)
				{
					MessageBox.Show("شكرا\u064b لك \r\n تم أخذ نسخة احتياطية بنجاح \r\n سوف يتم اغلاق البرنامج الان نستودعك الله الذي لاتضيع ودائعه", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					Application.Exit();
				}
				else
				{
					MessageBox.Show("نعتذر، تم اكتشاف خطأ \r\n يجب التأكد من مسار مجلد النسخ الاحتياطي ثم حاول مرة أخري", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
		}
		else
		{
			e.Cancel = true;
		}
	}

	private void BtnRecipt_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPayment_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 32))
		{
			new FrmPayment().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccCardAccount_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 27))
		{
			new FrmCardAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccTreeAccount_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 28))
		{
			new FrmAccountTree().ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccCostCenter_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 29))
		{
			new FrmCostCenter().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccOpenMoney_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 30))
		{
			new FrmOpenAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccRestrictionsTest_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 35))
		{
			new FrmDailyRestrictionsTest().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAccRestrictions_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 36))
		{
			new FrmDailyRestrictions().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 33))
		{
			new FrmReceiptsBig().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 34))
		{
			new FrmPaymentBig().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportMizan_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 45))
		{
			new FrmRPMizaniyah().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPProfitAccount_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 44))
		{
			new FrmRPProfitAndLossAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnSetting_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 38))
		{
			new FrmSetting().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportDesign_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 40))
		{
			new FrmDesignerReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void btnDefualtAccount_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 39))
		{
			new GTSErpSystem.Frms.FrmsMain.FrmDefualtAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		LblDate.Caption = DateTime.Today.ToString("  dd / MM /yyyy  ");
		LblTime.Caption = "الوقت: " + DateTime.Now.ToLongTimeString();
	}

	private void BtnRPGard_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 42))
		{
			new FrmRPStoresGardbyProperties().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPrinters_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 37))
		{
			new FrmPrinterCashier().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnMovementsDailyReport_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 41))
		{
			new FrmRPMovementsDailyReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAbout_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmAbout().ShowDialog();
	}

	private void BtnReportAllOrders_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 46))
		{
			new FrmAllOrders().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllOrdersReturn_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 47))
		{
			new FrmAllOrederReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllPurches_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 48))
		{
			new FrmAllPurches().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllPurchesReturn_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 49))
		{
			new FrmAllPurchesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllOrdersDetails_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 54))
		{
			new FrmAllOrdersDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllPurchesDetails_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 56))
		{
			new FrmAllPurchesDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllPurchesReturnDetails_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 57))
		{
			new FrmAllPurchesReturnDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllOrdersReturnDetails_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 55))
		{
			new FrmAllOrdersReturnDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportVAT_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 50))
		{
			new FrmVatReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportSalesEmployee_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 53))
		{
			new FrmSalesEmployee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnUpdateDateApp_ItemClick(object sender, ItemClickEventArgs e)
	{
		BtnDateApp.EditValue = DateTime.Now;
		LoginDetails.DateApp = Convert.ToDateTime(BtnDateApp.EditValue);
	}

	private void SetDateApp()
	{
		BtnDateApp.EditWidth = 150;
		BtnDateApp.EditValue = LoginDetails.DateApp;
	}

	private void BtnDateApp_EditValueChanged(object sender, EventArgs e)
	{
		LoginDetails.DateApp = Convert.ToDateTime(BtnDateApp.EditValue);
	}

	private void BtnReportAllOrdersRestaurant_ItemClick(object sender, ItemClickEventArgs e)
	{
	}

	private void TileLeadger_ItemClick(object sender, TileItemEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 43))
		{
			new FrmRPLeadger().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCheckOutOrder_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 59))
		{
			new FrmCheckOut().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnConnection_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmSqlConnection().ShowDialog();
	}

	private void BtnRPMovementItem_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 60))
		{
			new FrmRPMovementItem().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTransfer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 60))
		{
			new FrmStoresTransfer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPOrderProfit_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 61))
		{
			new FrmAllOrdersProfit().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPMizanTotal_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 63))
		{
			new FrmRPMizanTotal().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPMizan_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 62))
		{
			new FrmRPMizan().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPMizanGroups_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 64))
		{
			new FrmRPMizanGroups().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnClearDB_ItemClick(object sender, ItemClickEventArgs e)
	{
		FrmPassWord frmPassWord = new FrmPassWord();
		frmPassWord.ShowDialog();
		if (frmPassWord.ISOK == Convert.ToBoolean("True"))
		{
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
			SqlCommand sqlCommand = new SqlCommand("ClearDB", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			MessageBox.Show("شكرا\u064b لك، \r\n تم تصفير حركات قاعدة البيانات بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnEmptyDB_ItemClick(object sender, ItemClickEventArgs e)
	{
		FrmPassWord frmPassWord = new FrmPassWord();
		frmPassWord.ShowDialog();
		if (frmPassWord.ISOK == Convert.ToBoolean("True"))
		{
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
			SqlCommand sqlCommand = new SqlCommand("ClearEmptyDB", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			MessageBox.Show("شكرا\u064b لك، \r\n تم مسح حركات قاعدة البيانات بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPCustomer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 51))
		{
			Class_Account_CustSupp class_Account_CustSupp = new Class_Account_CustSupp();
			class_Account_CustSupp.GetAllCustmer();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPSupp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 65))
		{
			Class_Account_CustSupp class_Account_CustSupp = new Class_Account_CustSupp();
			class_Account_CustSupp.GetAllSupp();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPLeadgerCustomer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 66))
		{
			FrmRPLeadger frmRPLeadger = new FrmRPLeadger();
			frmRPLeadger.FrmName = "FrmCust";
			frmRPLeadger.ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPLeadgerSupp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 67))
		{
			FrmRPLeadger frmRPLeadger = new FrmRPLeadger();
			frmRPLeadger.FrmName = "FrmSupp";
			frmRPLeadger.ShowDialog();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 68))
		{
			new FrmAllOrdersProfitByGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPProfitByItems_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 69))
		{
			new FrmAllOrdersProfitByItems().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPProfitByItemsItemOnly_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 70))
		{
			new FrmAllOrdersProfitByItems_ItemOnly().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPStoreAvergByItem_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 72))
		{
			Class_Profit class_Profit = new Class_Profit();
			class_Profit.GetProfitByItem();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPStoreAvergByGroup_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 71))
		{
			Class_Profit class_Profit = new Class_Profit();
			class_Profit.GetProfitByGroup();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPTranTeacher_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 73))
		{
			new FrmRPTranTeacher().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnBackup_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmBackUp().ShowDialog();
	}

	private void BtnVat_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmVAT().Show();
	}

	private void BtnReportAllMovementDays_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 58))
		{
			new FrmAllOrdersRestaurant().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnEncriptPassword_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmPassword().Show();
	}

	private void BtnLock_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmLock().ShowDialog();
	}

	private void BtnReportTobaccoTax_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 75))
		{
			new FrmTobaccoTaxReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnEmployee_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 76))
		{
			new FrmEmployee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPayroll_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 78))
		{
			new FrmPayroll().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPayrollTest_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 77))
		{
			new FrmPayrollTemp().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportLedger_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 43))
		{
			new FrmRPLeadger().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashirRestaurant().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavItem_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmItems().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavUnit_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmUnit().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavGroups_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmGroupsRes().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavCompnys_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmCompany().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavClass_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmClass().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavCountry_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmCountry().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavGuarantee_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmGuarantee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavGroupRes_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 8))
		{
			new FrmGroupsRes().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPurches_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 13))
		{
			new FrmPurchases().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPurchesOrder_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 11))
		{
			new FrmPurchaseOrder().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPurchesReturn_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 15))
		{
			new FrmPurchasesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPurchesReturnNoID_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 17))
		{
			new FrmPurchasesReturnNoPurchCode().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPriceOffer_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 10))
		{
			new FrmPriceOffer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavOrders_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmOrders(null).Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavCashier_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashir().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavResturant_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashirRestaurant().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavOrderOut_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 59))
		{
			new FrmCheckOut().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavReturnOrder_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 14))
		{
			new FrmOrderReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavReturnOrderNoID_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 16))
		{
			new FrmOrdersReturnNoPurchCode().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavOpenQuantity_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 18))
		{
			new FrmOpenQuantity().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavStoreTransfer_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 20))
		{
			new FrmStoresTransfer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavDateCasher_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		FrmDateCashier frmDateCashier = new FrmDateCashier();
		frmDateCashier.ShowDialog();
		string text = LoginDetails.DateApp.ToShortDateString();
		NavDate.Caption = " (  " + text + "  )  : التاريخ ";
		LblDate.Caption = text;
	}

	private void NavCustomer_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 23))
		{
			new FrmCustomer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavSuppliers_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 24))
		{
			new FrmSuppliers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavSalesMan_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 22))
		{
			new FrmSalesMan().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPlaceCus_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 21))
		{
			new FrmPlace().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavItemAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 27))
		{
			new FrmCardAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavTreeAccount_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 28))
		{
			new FrmAccountTree().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavDailyRestrictionsTest_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 35))
		{
			new FrmDailyRestrictionsTest().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavDailyRestrictions_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 36))
		{
			new FrmDailyRestrictions().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavReceipts_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 31))
		{
			new FrmReceipts().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPayment_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 32))
		{
			new FrmPayment().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavReceiptsAll_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 33))
		{
			new FrmReceiptsBig().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPaymentAll_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 34))
		{
			new FrmPaymentBig().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavUsers_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 1))
		{
			new FrmUsers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavGroup_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 2))
		{
			new FrmGroups().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NanPermission_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 3))
		{
			new FrmSecurityGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPassword_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 1))
		{
			new FrmPassword().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavReport_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 82))
		{
			new FrmReportAll().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavSetting_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 38))
		{
			new FrmSetting().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavPrinter_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 37))
		{
			new FrmPrinterCashier().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavClear_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		FrmPassWord frmPassWord = new FrmPassWord();
		frmPassWord.ShowDialog();
		if (frmPassWord.ISOK == Convert.ToBoolean("True"))
		{
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
			SqlCommand sqlCommand = new SqlCommand("ClearDB", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			MessageBox.Show("شكرا\u064b لك، \r\n تم تصفير حركات قاعدة البيانات بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavClearAll_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		FrmPassWord frmPassWord = new FrmPassWord();
		frmPassWord.ShowDialog();
		if (frmPassWord.ISOK == Convert.ToBoolean("True"))
		{
			string nameServer = LoginDetails.nameServer;
			string databaseServer = LoginDetails.databaseServer;
			string userNameServer = LoginDetails.userNameServer;
			string passServer = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
			SqlCommand sqlCommand = new SqlCommand("ClearEmptyDB", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			MessageBox.Show("شكرا\u064b لك، \r\n تم مسح حركات قاعدة البيانات بنجاح", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavAbout_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		new FrmAbout().ShowDialog();
	}

	private void NavDesign_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 40))
		{
			new FrmDesignerReport().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavExit_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		switch (MessageBox.Show("هل تريد الخروج من برنامج مايكرو للمحاسبة ونقاط البيع", "خروج من البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
		{
		case DialogResult.Yes:
			Application.Exit();
			break;
		}
	}

	private void NavAccountDefualt_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 39))
		{
			new GTSErpSystem.Frms.Account.FrmDefualtAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void NavCustomerDefualt_LinkClicked(object sender, NavBarLinkEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 39))
		{
			new FrmDefualtCustomer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsHome.FrmWindow));
		this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
		this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
		this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
		this.skinRibbonGalleryBarItem = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
		this.employeesBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
		this.customersBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
		this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
		this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
		this.BtnORDOpenQuantity = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDPriceOffer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDOrder = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDPurchaseOrder = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDPurchase = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDOrderReturnID = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDPurchaseReturnID = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDOrderReturnNotID = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDPurchaseReturnNotID = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem15 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemGroups = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemUnits = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemClass = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemCompany = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnStores = new DevExpress.XtraBars.BarButtonItem();
		this.BtnBranches = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem23 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem24 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCusPlace = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCusSupplier = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCusSalesMan = new DevExpress.XtraBars.BarButtonItem();
		this.BtnUsers = new DevExpress.XtraBars.BarButtonItem();
		this.BtnGroups = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPermission = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCashir = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRecipt = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPayment = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccCardAccount = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccTreeAccount = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccCostCenter = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccOpenMoney = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccRestrictionsTest = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAccRestrictions = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAccount = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportMizan = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPProfitAccount = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMizan = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMizanTotal = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMizanGroups = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPTranTeacher = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportLedger = new DevExpress.XtraBars.BarButtonItem();
		this.BtnSetting = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportDesign = new DevExpress.XtraBars.BarButtonItem();
		this.btnDefualtAccount = new DevExpress.XtraBars.BarButtonItem();
		this.LblCompany = new DevExpress.XtraBars.BarStaticItem();
		this.LblServer = new DevExpress.XtraBars.BarStaticItem();
		this.LblUser = new DevExpress.XtraBars.BarStaticItem();
		this.LblTime = new DevExpress.XtraBars.BarStaticItem();
		this.LblDate = new DevExpress.XtraBars.BarStaticItem();
		this.barStaticItem5 = new DevExpress.XtraBars.BarStaticItem();
		this.BtnReportPublic = new DevExpress.XtraBars.BarSubItem();
		this.BtnMovementsDailyReport = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllMovementDays = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportVAT = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportTobaccoTax = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportStore = new DevExpress.XtraBars.BarSubItem();
		this.BtnRPGard = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMovementItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByGroup = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPrinters = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAbout = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportOrder = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportAllOrders = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturn = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturnDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportSalesEmployee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportPurshes = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportAllPurches = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllPurchesDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllPurchesReturn = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllPurchesReturnDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportProfit = new DevExpress.XtraBars.BarSubItem();
		this.BtnRPOrderProfit = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPProfitByItems = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPProfitByItemsItemOnly = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem20 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem21 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem22 = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
		this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
		this.DateApp = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.BtnUpdateDateApp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDateApp = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemDateEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
		this.repositoryItemDateEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.LblVerg = new DevExpress.XtraBars.BarStaticItem();
		this.BtnReportAllOrdersRestaurant = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCheckOutOrder = new DevExpress.XtraBars.BarButtonItem();
		this.BtnConnection = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTransfer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDabase = new DevExpress.XtraBars.BarSubItem();
		this.BtnClearDB = new DevExpress.XtraBars.BarButtonItem();
		this.BtnEmptyDB = new DevExpress.XtraBars.BarButtonItem();
		this.BtnBackup = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
		this.BtnRPCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPLeadgerCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPLeadgerSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnVat = new DevExpress.XtraBars.BarButtonItem();
		this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
		this.BtnEncriptPassword = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnLock = new DevExpress.XtraBars.BarButtonItem();
		this.BtnEmployee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPayrollTest = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPayroll = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRestraunt = new DevExpress.XtraBars.BarButtonItem();
		this.skinRibbonGalleryBarItem2 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
		this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroupNavigation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup22 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup20 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup21 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup18 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup11 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup19 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup12 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup13 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup14 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup15 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup16 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage6 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup17 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.repositoryItemColorPickEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
		this.repositoryItemColorEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTimeSpanEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeSpanEdit();
		this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
		this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
		this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
		this.employeesNavigationPage = new DevExpress.XtraBars.Navigation.NavigationPage();
		this.label1 = new System.Windows.Forms.Label();
		this.navBarControl2 = new DevExpress.XtraNavBar.NavBarControl();
		this.navBarGroup12 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavCompany = new DevExpress.XtraNavBar.NavBarItem();
		this.NavUser = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDataBase = new DevExpress.XtraNavBar.NavBarItem();
		this.NavYearClosing = new DevExpress.XtraNavBar.NavBarItem();
		this.NavVerg = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDate = new DevExpress.XtraNavBar.NavBarItem();
		this.NavServer = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
		this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavUnit = new DevExpress.XtraNavBar.NavBarItem();
		this.NavGroups = new DevExpress.XtraNavBar.NavBarItem();
		this.NavCompnys = new DevExpress.XtraNavBar.NavBarItem();
		this.NavClass = new DevExpress.XtraNavBar.NavBarItem();
		this.NavCountry = new DevExpress.XtraNavBar.NavBarItem();
		this.NavGuarantee = new DevExpress.XtraNavBar.NavBarItem();
		this.NavItem = new DevExpress.XtraNavBar.NavBarItem();
		this.NavGroupRes = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavPriceOffer = new DevExpress.XtraNavBar.NavBarItem();
		this.NavOrders = new DevExpress.XtraNavBar.NavBarItem();
		this.NavCashier = new DevExpress.XtraNavBar.NavBarItem();
		this.NavResturant = new DevExpress.XtraNavBar.NavBarItem();
		this.NavOrderOut = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
		this.NavReturnOrder = new DevExpress.XtraNavBar.NavBarItem();
		this.NavReturnOrderNoID = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDateCasher = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavPurchesOrder = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPurches = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarSeparatorItem7 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
		this.NavPurchesReturn = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPurchesReturnNoID = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavOpenQuantity = new DevExpress.XtraNavBar.NavBarItem();
		this.NavStoreTransfer = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup5 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavCustomer = new DevExpress.XtraNavBar.NavBarItem();
		this.NavSuppliers = new DevExpress.XtraNavBar.NavBarItem();
		this.NavSalesMan = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPlaceCus = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup6 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavItemAccount = new DevExpress.XtraNavBar.NavBarItem();
		this.NavTreeAccount = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarSeparatorItem8 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
		this.NavDailyRestrictionsTest = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDailyRestrictions = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup7 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavReceipts = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPayment = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarSeparatorItem3 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
		this.NavReceiptsAll = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPaymentAll = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup8 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavUsers = new DevExpress.XtraNavBar.NavBarItem();
		this.NavGroup = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
		this.NanPermission = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPassword = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup9 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavReport = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup10 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavSetting = new DevExpress.XtraNavBar.NavBarItem();
		this.NavPrinter = new DevExpress.XtraNavBar.NavBarItem();
		this.NavClear = new DevExpress.XtraNavBar.NavBarItem();
		this.NavClearAll = new DevExpress.XtraNavBar.NavBarItem();
		this.NavDesign = new DevExpress.XtraNavBar.NavBarItem();
		this.NavAccountDefualt = new DevExpress.XtraNavBar.NavBarItem();
		this.NavCustomerDefualt = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarGroup11 = new DevExpress.XtraNavBar.NavBarGroup();
		this.NavAbout = new DevExpress.XtraNavBar.NavBarItem();
		this.NavExit = new DevExpress.XtraNavBar.NavBarItem();
		this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.BtnDataBase = new DevExpress.XtraBars.BarSubItem();
		this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
		((System.ComponentModel.ISupportInitialize)this.tabbedView).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ribbonControl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.applicationMenu1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit3.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit4.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorPickEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTimeSpanEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTimeEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.navigationFrame).BeginInit();
		this.navigationFrame.SuspendLayout();
		this.employeesNavigationPage.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.navBarControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.navBarControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).BeginInit();
		base.SuspendLayout();
		this.ribbonControl.ApplicationButtonDropDownControl = this.applicationMenu1;
		this.ribbonControl.ApplicationButtonImageOptions.Image = GTSErpSystem.Properties.Resources.rsz_infographic;
		this.ribbonControl.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(35, 37, 35, 37);
		this.ribbonControl.ExpandCollapseItem.Id = 0;
		this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[128]
		{
			this.ribbonControl.ExpandCollapseItem,
			this.ribbonControl.SearchEditItem,
			this.skinRibbonGalleryBarItem,
			this.employeesBarButtonItem,
			this.customersBarButtonItem,
			this.skinRibbonGalleryBarItem1,
			this.barSubItem4,
			this.BtnORDOpenQuantity,
			this.BtnORDPriceOffer,
			this.BtnORDOrder,
			this.BtnORDPurchaseOrder,
			this.BtnORDPurchase,
			this.BtnORDOrderReturnID,
			this.BtnORDPurchaseReturnID,
			this.BtnORDOrderReturnNotID,
			this.BtnORDPurchaseReturnNotID,
			this.barButtonItem10,
			this.barButtonItem11,
			this.barButtonItem12,
			this.barButtonItem13,
			this.barButtonItem14,
			this.barButtonItem15,
			this.BtnItemGroups,
			this.BtnItemUnits,
			this.BtnItemClass,
			this.BtnItemCompany,
			this.BtnItem,
			this.BtnStores,
			this.BtnBranches,
			this.barButtonItem23,
			this.barButtonItem24,
			this.BtnCusPlace,
			this.BtnCustomer,
			this.BtnCusSupplier,
			this.BtnCusSalesMan,
			this.BtnUsers,
			this.BtnGroups,
			this.BtnPermission,
			this.BtnCashir,
			this.BtnRecipt,
			this.BtnPayment,
			this.BtnAccCardAccount,
			this.BtnAccTreeAccount,
			this.BtnAccCostCenter,
			this.BtnAccOpenMoney,
			this.BtnAccRestrictionsTest,
			this.BtnAccRestrictions,
			this.barButtonItem7,
			this.barButtonItem8,
			this.BtnReportAccount,
			this.BtnReportMizan,
			this.BtnRPProfitAccount,
			this.BtnSetting,
			this.BtnReportDesign,
			this.btnDefualtAccount,
			this.LblCompany,
			this.LblServer,
			this.LblUser,
			this.LblTime,
			this.LblDate,
			this.barStaticItem5,
			this.BtnReportPublic,
			this.BtnReportStore,
			this.BtnRPGard,
			this.BtnMovementsDailyReport,
			this.BtnPrinters,
			this.BtnAbout,
			this.BtnReportOrder,
			this.BtnReportPurshes,
			this.BtnReportAllOrders,
			this.BtnReportAllOrdersReturn,
			this.BtnReportAllOrdersDetails,
			this.BtnReportAllOrdersReturnDetails,
			this.BtnReportProfit,
			this.BtnRPOrderProfit,
			this.BtnRPProfitByItems,
			this.barButtonItem6,
			this.BtnReportAllPurches,
			this.BtnReportAllPurchesDetails,
			this.BtnReportAllPurchesReturn,
			this.BtnReportAllPurchesReturnDetails,
			this.BtnReportVAT,
			this.barButtonItem20,
			this.barButtonItem21,
			this.barButtonItem22,
			this.barSubItem5,
			this.BtnReportSalesEmployee,
			this.barCheckItem1,
			this.DateApp,
			this.BtnUpdateDateApp,
			this.BtnDateApp,
			this.barEditItem1,
			this.LblVerg,
			this.BtnReportAllOrdersRestaurant,
			this.BtnCheckOutOrder,
			this.BtnConnection,
			this.BtnRPMovementItem,
			this.BtnTransfer,
			this.BtnRPMizanTotal,
			this.BtnRPMizan,
			this.BtnRPMizanGroups,
			this.BtnDabase,
			this.BtnClearDB,
			this.BtnEmptyDB,
			this.barSubItem6,
			this.BtnRPCustomer,
			this.BtnRPSupp,
			this.BtnRPLeadgerCustomer,
			this.BtnRPLeadgerSupp,
			this.BtnRPProfitByItemsItemOnly,
			this.BtnRPStoreAvergByGroup,
			this.BtnRPStoreAvergByItem,
			this.BtnRPTranTeacher,
			this.BtnBackup,
			this.BtnVat,
			this.BtnReportAllMovementDays,
			this.barCheckItem2,
			this.BtnEncriptPassword,
			this.barButtonItem1,
			this.barButtonItem2,
			this.BtnLock,
			this.BtnReportTobaccoTax,
			this.BtnEmployee,
			this.BtnPayrollTest,
			this.BtnPayroll,
			this.BtnReportLedger,
			this.BtnRestraunt,
			this.skinRibbonGalleryBarItem2
		});
		this.ribbonControl.Location = new System.Drawing.Point(0, 0);
		this.ribbonControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ribbonControl.MaxItemId = 178;
		this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
		this.ribbonControl.Name = "ribbonControl";
		this.ribbonControl.OptionsMenuMinWidth = 385;
		this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[7] { this.ribbonPage, this.ribbonPage1, this.ribbonPage2, this.ribbonPage3, this.ribbonPage4, this.ribbonPage5, this.ribbonPage6 });
		this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemColorEdit1, this.repositoryItemColorPickEdit1, this.repositoryItemColorEdit2, this.repositoryItemDateEdit1, this.repositoryItemDateEdit2, this.repositoryItemDateEdit3, this.repositoryItemTimeSpanEdit1, this.repositoryItemTimeEdit1, this.repositoryItemDateEdit4 });
		this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
		this.ribbonControl.Size = new System.Drawing.Size(1172, 193);
		this.ribbonControl.StatusBar = this.ribbonStatusBar;
		this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
		this.applicationMenu1.Name = "applicationMenu1";
		this.applicationMenu1.Ribbon = this.ribbonControl;
		this.applicationMenu1.ShowRightPane = true;
		this.skinRibbonGalleryBarItem.Id = 14;
		this.skinRibbonGalleryBarItem.Name = "skinRibbonGalleryBarItem";
		this.employeesBarButtonItem.Id = 80;
		this.employeesBarButtonItem.Name = "employeesBarButtonItem";
		this.customersBarButtonItem.Id = 81;
		this.customersBarButtonItem.Name = "customersBarButtonItem";
		this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
		this.skinRibbonGalleryBarItem1.Id = 46;
		this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
		this.barSubItem4.Caption = "barSubItem4";
		this.barSubItem4.Id = 48;
		this.barSubItem4.Name = "barSubItem4";
		this.barSubItem4.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOpenQuantity.Caption = "الأرصدة الافتتاحية";
		this.BtnORDOpenQuantity.Id = 51;
		this.BtnORDOpenQuantity.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOpenQuantity.ImageOptions.Image");
		this.BtnORDOpenQuantity.Name = "BtnORDOpenQuantity";
		this.BtnORDOpenQuantity.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOpenQuantity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOpenQuantity_ItemClick);
		this.BtnORDPriceOffer.Caption = "عروض أسعار";
		this.BtnORDPriceOffer.Id = 52;
		this.BtnORDPriceOffer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDPriceOffer.ImageOptions.Image");
		this.BtnORDPriceOffer.Name = "BtnORDPriceOffer";
		this.BtnORDPriceOffer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPriceOffer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPriceOffer_ItemClick);
		this.BtnORDOrder.Caption = "فواتير المبيعات";
		this.BtnORDOrder.Id = 53;
		this.BtnORDOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrder.ImageOptions.Image");
		this.BtnORDOrder.Name = "BtnORDOrder";
		this.BtnORDOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrder_ItemClick);
		this.BtnORDPurchaseOrder.Caption = "طلب شراء";
		this.BtnORDPurchaseOrder.Id = 54;
		this.BtnORDPurchaseOrder.ImageOptions.Image = GTSErpSystem.Properties.Resources.insert_32x32;
		this.BtnORDPurchaseOrder.Name = "BtnORDPurchaseOrder";
		this.BtnORDPurchaseOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseOrder_ItemClick);
		this.BtnORDPurchase.Caption = "فواتير المشتريات";
		this.BtnORDPurchase.Id = 55;
		this.BtnORDPurchase.ImageOptions.Image = GTSErpSystem.Properties.Resources.buy_32x32;
		this.BtnORDPurchase.Name = "BtnORDPurchase";
		this.BtnORDPurchase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchase_ItemClick);
		this.BtnORDOrderReturnID.Caption = "مرتجعات مبيعات ";
		this.BtnORDOrderReturnID.Id = 56;
		this.BtnORDOrderReturnID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnID.ImageOptions.Image");
		this.BtnORDOrderReturnID.Name = "BtnORDOrderReturnID";
		this.BtnORDOrderReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnID_ItemClick);
		this.BtnORDPurchaseReturnID.Caption = "مرتجعات مشتريات";
		this.BtnORDPurchaseReturnID.Id = 57;
		this.BtnORDPurchaseReturnID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deletegroupfooter_32x32;
		this.BtnORDPurchaseReturnID.Name = "BtnORDPurchaseReturnID";
		this.BtnORDPurchaseReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnID_ItemClick);
		this.BtnORDOrderReturnNotID.Caption = "مرتجعات مبيعات بدون فاتورة";
		this.BtnORDOrderReturnNotID.Id = 58;
		this.BtnORDOrderReturnNotID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.Image");
		this.BtnORDOrderReturnNotID.Name = "BtnORDOrderReturnNotID";
		this.BtnORDOrderReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnNotID_ItemClick);
		this.BtnORDPurchaseReturnNotID.Caption = "مرتجعات مشتريات بدون فاتورة";
		this.BtnORDPurchaseReturnNotID.Id = 59;
		this.BtnORDPurchaseReturnNotID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deleteheader_32x32;
		this.BtnORDPurchaseReturnNotID.Name = "BtnORDPurchaseReturnNotID";
		this.BtnORDPurchaseReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnNotID_ItemClick);
		this.barButtonItem10.Caption = "barButtonItem10";
		this.barButtonItem10.Id = 60;
		this.barButtonItem10.Name = "barButtonItem10";
		this.barButtonItem11.Caption = "barButtonItem11";
		this.barButtonItem11.Id = 61;
		this.barButtonItem11.Name = "barButtonItem11";
		this.barButtonItem11.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem12.Caption = "barButtonItem12";
		this.barButtonItem12.Id = 62;
		this.barButtonItem12.Name = "barButtonItem12";
		this.barButtonItem12.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem13.Caption = "barButtonItem13";
		this.barButtonItem13.Id = 63;
		this.barButtonItem13.Name = "barButtonItem13";
		this.barButtonItem13.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem14.Caption = "barButtonItem14";
		this.barButtonItem14.Id = 64;
		this.barButtonItem14.Name = "barButtonItem14";
		this.barButtonItem14.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem15.Caption = "barButtonItem15";
		this.barButtonItem15.Id = 65;
		this.barButtonItem15.Name = "barButtonItem15";
		this.barButtonItem15.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGroups.Caption = "المجموعات";
		this.BtnItemGroups.Id = 66;
		this.BtnItemGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.group_32x32;
		this.BtnItemGroups.Name = "BtnItemGroups";
		this.BtnItemGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGroups_ItemClick);
		this.BtnItemUnits.Caption = "وحدات القياس";
		this.BtnItemUnits.Id = 67;
		this.BtnItemUnits.ImageOptions.Image = GTSErpSystem.Properties.Resources.unmergecells_32x32;
		this.BtnItemUnits.Name = "BtnItemUnits";
		this.BtnItemUnits.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemUnits.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemUnits_ItemClick);
		this.BtnItemClass.Caption = "الفئات العامة";
		this.BtnItemClass.Id = 68;
		this.BtnItemClass.ImageOptions.Image = GTSErpSystem.Properties.Resources.expandcollapse_32x32;
		this.BtnItemClass.Name = "BtnItemClass";
		this.BtnItemClass.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemClass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemClass_ItemClick);
		this.BtnItemCompany.Caption = "الشركات ";
		this.BtnItemCompany.Id = 69;
		this.BtnItemCompany.ImageOptions.Image = GTSErpSystem.Properties.Resources.home_32x32;
		this.BtnItemCompany.Name = "BtnItemCompany";
		this.BtnItemCompany.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCompany.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCompany_ItemClick);
		this.BtnItem.Caption = "بطاقة صنف";
		this.BtnItem.Id = 70;
		this.BtnItem.ImageOptions.Image = GTSErpSystem.Properties.Resources.mailmerge_32x32;
		this.BtnItem.Name = "BtnItem";
		this.BtnItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItem_ItemClick);
		this.BtnStores.Caption = "المخازن ";
		this.BtnStores.Id = 71;
		this.BtnStores.ImageOptions.Image = GTSErpSystem.Properties.Resources.autoexpand_32x32;
		this.BtnStores.Name = "BtnStores";
		this.BtnStores.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnStores.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnStores_ItemClick);
		this.BtnBranches.Caption = "الفروع";
		this.BtnBranches.Id = 72;
		this.BtnBranches.ImageOptions.Image = GTSErpSystem.Properties.Resources.documentmap_32x32;
		this.BtnBranches.Name = "BtnBranches";
		this.BtnBranches.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBranches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBranches_ItemClick);
		this.barButtonItem23.Caption = "تصنيف العملاء";
		this.barButtonItem23.Id = 73;
		this.barButtonItem23.Name = "barButtonItem23";
		this.barButtonItem23.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem24.Caption = "مناطق العملاء";
		this.barButtonItem24.Id = 74;
		this.barButtonItem24.Name = "barButtonItem24";
		this.barButtonItem24.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusPlace.Caption = "مناطق المناديب والعملاء";
		this.BtnCusPlace.Id = 75;
		this.BtnCusPlace.ImageOptions.Image = GTSErpSystem.Properties.Resources.language_32x32;
		this.BtnCusPlace.Name = "BtnCusPlace";
		this.BtnCusPlace.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusPlace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusPlace_ItemClick);
		this.BtnCustomer.Caption = "العملاء";
		this.BtnCustomer.Id = 76;
		this.BtnCustomer.ImageOptions.Image = GTSErpSystem.Properties.Resources.employee_32x32;
		this.BtnCustomer.Name = "BtnCustomer";
		this.BtnCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCustomer_ItemClick);
		this.BtnCusSupplier.Caption = "الموردين";
		this.BtnCusSupplier.Id = 77;
		this.BtnCusSupplier.ImageOptions.Image = GTSErpSystem.Properties.Resources.bocustomer_32x32;
		this.BtnCusSupplier.Name = "BtnCusSupplier";
		this.BtnCusSupplier.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSupplier.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSupplier_ItemClick);
		this.BtnCusSalesMan.Caption = "المناديب";
		this.BtnCusSalesMan.Id = 79;
		this.BtnCusSalesMan.ImageOptions.Image = GTSErpSystem.Properties.Resources.meeting_32x32;
		this.BtnCusSalesMan.Name = "BtnCusSalesMan";
		this.BtnCusSalesMan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSalesMan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSalesMan_ItemClick);
		this.BtnUsers.Caption = "مستخدم جديد";
		this.BtnUsers.Id = 82;
		this.BtnUsers.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.Image");
		this.BtnUsers.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.LargeImage");
		this.BtnUsers.Name = "BtnUsers";
		this.BtnUsers.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUsers_ItemClick);
		this.BtnGroups.Caption = "مجموعات المستخدمين";
		this.BtnGroups.Id = 83;
		this.BtnGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.Groups;
		this.BtnGroups.Name = "BtnGroups";
		this.BtnGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGroups_ItemClick);
		this.BtnPermission.Caption = "صلاحيات المستخدمين";
		this.BtnPermission.Id = 84;
		this.BtnPermission.ImageOptions.Image = GTSErpSystem.Properties.Resources.Permission;
		this.BtnPermission.Name = "BtnPermission";
		this.BtnPermission.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPermission.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPermission_ItemClick);
		this.BtnCashir.Caption = "الكاشير";
		this.BtnCashir.Id = 85;
		this.BtnCashir.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCashir.ImageOptions.Image");
		this.BtnCashir.Name = "BtnCashir";
		this.BtnCashir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCashir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCashir_ItemClick);
		this.BtnRecipt.Caption = "ايصال_قبض";
		this.BtnRecipt.Id = 86;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRecipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRecipt_ItemClick);
		this.BtnPayment.Caption = "ايصال_صرف";
		this.BtnPayment.Id = 87;
		this.BtnPayment.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayment.ImageOptions.Image");
		this.BtnPayment.Name = "BtnPayment";
		this.BtnPayment.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayment_ItemClick);
		this.BtnAccCardAccount.Caption = "بطاقة حساب";
		this.BtnAccCardAccount.Id = 88;
		this.BtnAccCardAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCardAccount.ImageOptions.Image");
		this.BtnAccCardAccount.Name = "BtnAccCardAccount";
		this.BtnAccCardAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCardAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCardAccount_ItemClick);
		this.BtnAccTreeAccount.Caption = "شجرة حسابات";
		this.BtnAccTreeAccount.Id = 89;
		this.BtnAccTreeAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccTreeAccount.ImageOptions.Image");
		this.BtnAccTreeAccount.Name = "BtnAccTreeAccount";
		this.BtnAccTreeAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccTreeAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccTreeAccount_ItemClick);
		this.BtnAccCostCenter.Caption = "مركز التكلفة";
		this.BtnAccCostCenter.Id = 90;
		this.BtnAccCostCenter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCostCenter.ImageOptions.Image");
		this.BtnAccCostCenter.Name = "BtnAccCostCenter";
		this.BtnAccCostCenter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCostCenter.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccCostCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCostCenter_ItemClick);
		this.BtnAccOpenMoney.Caption = "رصيد افتتاحي حسابات";
		this.BtnAccOpenMoney.Id = 91;
		this.BtnAccOpenMoney.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccOpenMoney.ImageOptions.Image");
		this.BtnAccOpenMoney.Name = "BtnAccOpenMoney";
		this.BtnAccOpenMoney.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccOpenMoney.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccOpenMoney.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccOpenMoney_ItemClick);
		this.BtnAccRestrictionsTest.Caption = "نموذج قيود يومية";
		this.BtnAccRestrictionsTest.Id = 92;
		this.BtnAccRestrictionsTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.Image");
		this.BtnAccRestrictionsTest.Name = "BtnAccRestrictionsTest";
		this.BtnAccRestrictionsTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictionsTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictionsTest_ItemClick);
		this.BtnAccRestrictions.Caption = "قيود يومية";
		this.BtnAccRestrictions.Id = 93;
		this.BtnAccRestrictions.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictions.ImageOptions.Image");
		this.BtnAccRestrictions.Name = "BtnAccRestrictions";
		this.BtnAccRestrictions.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictions_ItemClick);
		this.barButtonItem7.Caption = "ايصال قبض متعدد";
		this.barButtonItem7.Id = 94;
		this.barButtonItem7.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
		this.barButtonItem7.Name = "barButtonItem7";
		this.barButtonItem7.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem7_ItemClick);
		this.barButtonItem8.Caption = "ايصال صرف متعدد";
		this.barButtonItem8.Id = 95;
		this.barButtonItem8.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
		this.barButtonItem8.Name = "barButtonItem8";
		this.barButtonItem8.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem8_ItemClick);
		this.BtnReportAccount.Caption = "الحسابات";
		this.BtnReportAccount.Id = 96;
		this.BtnReportAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAccount.ImageOptions.Image");
		this.BtnReportAccount.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[7]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitAccount),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanGroups),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPTranTeacher),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportLedger)
		});
		this.BtnReportAccount.Name = "BtnReportAccount";
		this.BtnReportAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportMizan.Caption = "الميزانية";
		this.BtnReportMizan.Id = 97;
		this.BtnReportMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.Image");
		this.BtnReportMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.LargeImage");
		this.BtnReportMizan.Name = "BtnReportMizan";
		this.BtnReportMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportMizan_ItemClick);
		this.BtnRPProfitAccount.Caption = "قائمة الدخل";
		this.BtnRPProfitAccount.Id = 98;
		this.BtnRPProfitAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.Image");
		this.BtnRPProfitAccount.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.LargeImage");
		this.BtnRPProfitAccount.Name = "BtnRPProfitAccount";
		this.BtnRPProfitAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitAccount_ItemClick);
		this.BtnRPMizan.Caption = "تقرير ميزان مراجعة";
		this.BtnRPMizan.Id = 149;
		this.BtnRPMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.Image");
		this.BtnRPMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.LargeImage");
		this.BtnRPMizan.Name = "BtnRPMizan";
		this.BtnRPMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizan_ItemClick);
		this.BtnRPMizanTotal.Caption = "تقرير ميزان مراجعة مجمع";
		this.BtnRPMizanTotal.Id = 148;
		this.BtnRPMizanTotal.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.Image");
		this.BtnRPMizanTotal.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.LargeImage");
		this.BtnRPMizanTotal.Name = "BtnRPMizanTotal";
		this.BtnRPMizanTotal.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPMizanTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanTotal_ItemClick);
		this.BtnRPMizanGroups.Caption = "تقرير ميزان مجموعة حسابات";
		this.BtnRPMizanGroups.Id = 150;
		this.BtnRPMizanGroups.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.Image");
		this.BtnRPMizanGroups.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.LargeImage");
		this.BtnRPMizanGroups.Name = "BtnRPMizanGroups";
		this.BtnRPMizanGroups.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPMizanGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanGroups_ItemClick);
		this.BtnRPTranTeacher.Caption = "تقرير دفتر الاستاذ ";
		this.BtnRPTranTeacher.Id = 162;
		this.BtnRPTranTeacher.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.Image");
		this.BtnRPTranTeacher.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.LargeImage");
		this.BtnRPTranTeacher.Name = "BtnRPTranTeacher";
		this.BtnRPTranTeacher.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPTranTeacher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTranTeacher_ItemClick);
		this.BtnReportLedger.Caption = "كشف حساب ";
		this.BtnReportLedger.Id = 175;
		this.BtnReportLedger.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.Image");
		this.BtnReportLedger.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.LargeImage");
		this.BtnReportLedger.Name = "BtnReportLedger";
		this.BtnReportLedger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportLedger_ItemClick);
		this.BtnSetting.Caption = "اعدادات_البرنامج";
		this.BtnSetting.Id = 99;
		this.BtnSetting.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSetting.ImageOptions.Image");
		this.BtnSetting.Name = "BtnSetting";
		this.BtnSetting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnSetting_ItemClick);
		this.BtnReportDesign.Caption = "مصمم التقارير";
		this.BtnReportDesign.Id = 100;
		this.BtnReportDesign.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportDesign.ImageOptions.Image");
		this.BtnReportDesign.Name = "BtnReportDesign";
		this.BtnReportDesign.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportDesign_ItemClick);
		this.btnDefualtAccount.Caption = "اعدادات الحسابات الافتراضية";
		this.btnDefualtAccount.Id = 101;
		this.btnDefualtAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnDefualtAccount.ImageOptions.Image");
		this.btnDefualtAccount.Name = "btnDefualtAccount";
		this.btnDefualtAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.btnDefualtAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnDefualtAccount_ItemClick);
		this.LblCompany.Caption = "LblCompany";
		this.LblCompany.Id = 102;
		this.LblCompany.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblCompany.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblCompany.Name = "LblCompany";
		this.LblServer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblServer.Caption = "LblServer";
		this.LblServer.Id = 103;
		this.LblServer.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LblServer.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblServer.Name = "LblServer";
		this.LblServer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.LblUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblUser.Caption = "LblUser";
		this.LblUser.Id = 104;
		this.LblUser.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblUser.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblUser.Name = "LblUser";
		this.LblUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.LblTime.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblTime.Caption = "LblTime";
		this.LblTime.Id = 105;
		this.LblTime.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblTime.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblTime.Name = "LblTime";
		this.LblDate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblDate.Caption = "LblDate";
		this.LblDate.Id = 106;
		this.LblDate.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblDate.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblDate.Name = "LblDate";
		this.barStaticItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.barStaticItem5.Caption = "barStaticItem5";
		this.barStaticItem5.Id = 107;
		this.barStaticItem5.Name = "barStaticItem5";
		this.BtnReportPublic.Caption = "عام";
		this.BtnReportPublic.Id = 108;
		this.BtnReportPublic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportPublic.ImageOptions.Image");
		this.BtnReportPublic.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnMovementsDailyReport, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllMovementDays),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportVAT),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnReportTobaccoTax, DevExpress.XtraBars.BarItemPaintStyle.Standard)
		});
		this.BtnReportPublic.Name = "BtnReportPublic";
		this.BtnReportPublic.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnMovementsDailyReport.Caption = "تقرير حركة اليومية";
		this.BtnMovementsDailyReport.Id = 111;
		this.BtnMovementsDailyReport.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.Image");
		this.BtnMovementsDailyReport.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.LargeImage");
		this.BtnMovementsDailyReport.Name = "BtnMovementsDailyReport";
		this.BtnMovementsDailyReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMovementsDailyReport_ItemClick);
		this.BtnReportAllMovementDays.Caption = "تقرير اليومية مصغرة";
		this.BtnReportAllMovementDays.Id = 165;
		this.BtnReportAllMovementDays.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.Image");
		this.BtnReportAllMovementDays.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.LargeImage");
		this.BtnReportAllMovementDays.Name = "BtnReportAllMovementDays";
		this.BtnReportAllMovementDays.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllMovementDays_ItemClick);
		this.BtnReportVAT.Caption = "تقرير ضريبة القيمة المضافة";
		this.BtnReportVAT.Id = 128;
		this.BtnReportVAT.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.Image");
		this.BtnReportVAT.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.LargeImage");
		this.BtnReportVAT.Name = "BtnReportVAT";
		this.BtnReportVAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportVAT_ItemClick);
		this.BtnReportTobaccoTax.Caption = "تقرير رسوم منتجات التبغ ";
		this.BtnReportTobaccoTax.Id = 171;
		this.BtnReportTobaccoTax.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.Image");
		this.BtnReportTobaccoTax.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.LargeImage");
		this.BtnReportTobaccoTax.Name = "BtnReportTobaccoTax";
		this.BtnReportTobaccoTax.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnReportTobaccoTax.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportTobaccoTax_ItemClick);
		this.BtnReportStore.Caption = "المخزون";
		this.BtnReportStore.Id = 109;
		this.BtnReportStore.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportStore.ImageOptions.Image");
		this.BtnReportStore.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPGard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMovementItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByGroup)
		});
		this.BtnReportStore.Name = "BtnReportStore";
		this.BtnReportStore.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRPGard.Caption = "تقرير الجرد";
		this.BtnRPGard.Id = 110;
		this.BtnRPGard.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.Image");
		this.BtnRPGard.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.LargeImage");
		this.BtnRPGard.Name = "BtnRPGard";
		this.BtnRPGard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPGard_ItemClick);
		this.BtnRPMovementItem.Caption = "تقرير حركة الصنف";
		this.BtnRPMovementItem.Id = 146;
		this.BtnRPMovementItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.Image");
		this.BtnRPMovementItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.LargeImage");
		this.BtnRPMovementItem.Name = "BtnRPMovementItem";
		this.BtnRPMovementItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMovementItem_ItemClick);
		this.BtnRPStoreAvergByItem.Caption = "قيمة المخزون بتفاصيل الاصناف";
		this.BtnRPStoreAvergByItem.Id = 161;
		this.BtnRPStoreAvergByItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.Image");
		this.BtnRPStoreAvergByItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByItem.Name = "BtnRPStoreAvergByItem";
		this.BtnRPStoreAvergByItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByItem_ItemClick);
		this.BtnRPStoreAvergByGroup.Caption = "قيمة المخزون بتفاصيل المجموعات";
		this.BtnRPStoreAvergByGroup.Id = 160;
		this.BtnRPStoreAvergByGroup.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.Image");
		this.BtnRPStoreAvergByGroup.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByGroup.Name = "BtnRPStoreAvergByGroup";
		this.BtnRPStoreAvergByGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByGroup_ItemClick);
		this.BtnPrinters.Caption = "اعدادات طابعة الكاشير";
		this.BtnPrinters.Id = 112;
		this.BtnPrinters.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrinters.ImageOptions.Image");
		this.BtnPrinters.Name = "BtnPrinters";
		this.BtnPrinters.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPrinters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPrinters_ItemClick);
		this.BtnAbout.Caption = "حول البرنامج";
		this.BtnAbout.Id = 113;
		this.BtnAbout.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAbout.ImageOptions.Image");
		this.BtnAbout.Name = "BtnAbout";
		this.BtnAbout.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAbout_ItemClick);
		this.BtnReportOrder.Caption = "المبيعات";
		this.BtnReportOrder.Id = 114;
		this.BtnReportOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportOrder.ImageOptions.Image");
		this.BtnReportOrder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[5]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrders),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturnDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSalesEmployee)
		});
		this.BtnReportOrder.Name = "BtnReportOrder";
		this.BtnReportOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportAllOrders.Caption = "قائمة بالمبيعات خلال فترة";
		this.BtnReportAllOrders.Id = 116;
		this.BtnReportAllOrders.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.Image");
		this.BtnReportAllOrders.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.LargeImage");
		this.BtnReportAllOrders.Name = "BtnReportAllOrders";
		this.BtnReportAllOrders.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrders_ItemClick);
		this.BtnReportAllOrdersDetails.Caption = "تفاصيل مبيعات الاصناف خلال فترة";
		this.BtnReportAllOrdersDetails.Id = 118;
		this.BtnReportAllOrdersDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.Image");
		this.BtnReportAllOrdersDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersDetails.Name = "BtnReportAllOrdersDetails";
		this.BtnReportAllOrdersDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersDetails_ItemClick);
		this.BtnReportAllOrdersReturn.Caption = "مرتجعات المبيعات خلال فترة";
		this.BtnReportAllOrdersReturn.Id = 117;
		this.BtnReportAllOrdersReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.Image");
		this.BtnReportAllOrdersReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturn.Name = "BtnReportAllOrdersReturn";
		this.BtnReportAllOrdersReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturn_ItemClick);
		this.BtnReportAllOrdersReturnDetails.Caption = "تفاصيل مرتجعات المبيعات خلال فترة";
		this.BtnReportAllOrdersReturnDetails.Id = 119;
		this.BtnReportAllOrdersReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.Image");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturnDetails.Name = "BtnReportAllOrdersReturnDetails";
		this.BtnReportAllOrdersReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturnDetails_ItemClick);
		this.BtnReportSalesEmployee.Caption = "مبيعات الموظفين خلال فترة ";
		this.BtnReportSalesEmployee.Id = 133;
		this.BtnReportSalesEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.Image");
		this.BtnReportSalesEmployee.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.LargeImage");
		this.BtnReportSalesEmployee.Name = "BtnReportSalesEmployee";
		this.BtnReportSalesEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSalesEmployee_ItemClick);
		this.BtnReportPurshes.Caption = "المشتريات";
		this.BtnReportPurshes.Id = 115;
		this.BtnReportPurshes.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportPurshes.ImageOptions.Image");
		this.BtnReportPurshes.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurches),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesReturnDetails)
		});
		this.BtnReportPurshes.Name = "BtnReportPurshes";
		this.BtnReportPurshes.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportAllPurches.Caption = "قائمة بالمشتريات خلال فترة";
		this.BtnReportAllPurches.Id = 124;
		this.BtnReportAllPurches.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.Image");
		this.BtnReportAllPurches.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.LargeImage");
		this.BtnReportAllPurches.Name = "BtnReportAllPurches";
		this.BtnReportAllPurches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurches_ItemClick);
		this.BtnReportAllPurchesDetails.Caption = "تفاصيل اصناف المشتريات خلال فترة";
		this.BtnReportAllPurchesDetails.Id = 125;
		this.BtnReportAllPurchesDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.Image");
		this.BtnReportAllPurchesDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesDetails.Name = "BtnReportAllPurchesDetails";
		this.BtnReportAllPurchesDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesDetails_ItemClick);
		this.BtnReportAllPurchesReturn.Caption = "قائمة بمرتجعات المشتريات خلال فترة";
		this.BtnReportAllPurchesReturn.Id = 126;
		this.BtnReportAllPurchesReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.Image");
		this.BtnReportAllPurchesReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturn.Name = "BtnReportAllPurchesReturn";
		this.BtnReportAllPurchesReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturn_ItemClick);
		this.BtnReportAllPurchesReturnDetails.Caption = "تفاصيل اصناف مرتجعات المشتريات خلال فترة";
		this.BtnReportAllPurchesReturnDetails.Id = 127;
		this.BtnReportAllPurchesReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.Image");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturnDetails.Name = "BtnReportAllPurchesReturnDetails";
		this.BtnReportAllPurchesReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturnDetails_ItemClick);
		this.BtnReportProfit.Caption = "الارباح";
		this.BtnReportProfit.Id = 120;
		this.BtnReportProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportProfit.ImageOptions.Image");
		this.BtnReportProfit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPOrderProfit),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItems),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItemsItemOnly)
		});
		this.BtnReportProfit.Name = "BtnReportProfit";
		this.BtnReportProfit.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRPOrderProfit.Caption = "ارباح فواتير المبيعات خلال فترة";
		this.BtnRPOrderProfit.Id = 121;
		this.BtnRPOrderProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.Image");
		this.BtnRPOrderProfit.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.LargeImage");
		this.BtnRPOrderProfit.Name = "BtnRPOrderProfit";
		this.BtnRPOrderProfit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPOrderProfit_ItemClick);
		this.BtnRPProfitByItems.Caption = "ارباح تفاصيل الاصناف خلال فترة";
		this.BtnRPProfitByItems.Id = 122;
		this.BtnRPProfitByItems.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.Image");
		this.BtnRPProfitByItems.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.LargeImage");
		this.BtnRPProfitByItems.Name = "BtnRPProfitByItems";
		this.BtnRPProfitByItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItems_ItemClick);
		this.barButtonItem6.Caption = "ارباح المجموعات خلال فترة";
		this.barButtonItem6.Id = 123;
		this.barButtonItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
		this.barButtonItem6.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
		this.barButtonItem6.Name = "barButtonItem6";
		this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem6_ItemClick);
		this.BtnRPProfitByItemsItemOnly.Caption = "تقرير أرباح صنف واحد خلال فترة";
		this.BtnRPProfitByItemsItemOnly.Id = 159;
		this.BtnRPProfitByItemsItemOnly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.Image");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage");
		this.BtnRPProfitByItemsItemOnly.Name = "BtnRPProfitByItemsItemOnly";
		this.BtnRPProfitByItemsItemOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItemsItemOnly_ItemClick);
		this.barButtonItem20.Caption = "قائمة بالعملاء";
		this.barButtonItem20.Id = 129;
		this.barButtonItem20.Name = "barButtonItem20";
		this.barButtonItem21.Caption = "قائمة بالموردين";
		this.barButtonItem21.Id = 130;
		this.barButtonItem21.Name = "barButtonItem21";
		this.barButtonItem22.Caption = "قائمة بالموظفين";
		this.barButtonItem22.Id = 131;
		this.barButtonItem22.Name = "barButtonItem22";
		this.barSubItem5.Caption = "barSubItem5";
		this.barSubItem5.Id = 132;
		this.barSubItem5.Name = "barSubItem5";
		this.barCheckItem1.Caption = "barCheckItem1";
		this.barCheckItem1.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
		this.barCheckItem1.Id = 134;
		this.barCheckItem1.Name = "barCheckItem1";
		this.barCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.DateApp.Caption = "تاريخ البرنامج";
		this.DateApp.Edit = this.repositoryItemDateEdit2;
		this.DateApp.Id = 136;
		this.DateApp.Name = "DateApp";
		this.DateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.repositoryItemDateEdit2.AutoHeight = false;
		this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
		this.BtnUpdateDateApp.Caption = "تحديث_التاريخ";
		this.BtnUpdateDateApp.Id = 137;
		this.BtnUpdateDateApp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUpdateDateApp.ImageOptions.Image");
		this.BtnUpdateDateApp.Name = "BtnUpdateDateApp";
		this.BtnUpdateDateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUpdateDateApp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUpdateDateApp_ItemClick);
		this.BtnDateApp.Caption = "تاريخ الكاشير";
		this.BtnDateApp.Edit = this.repositoryItemDateEdit3;
		this.BtnDateApp.EditWidth = 150;
		this.BtnDateApp.Id = 138;
		this.BtnDateApp.Name = "BtnDateApp";
		this.BtnDateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDateApp.EditValueChanged += new System.EventHandler(BtnDateApp_EditValueChanged);
		this.repositoryItemDateEdit3.Appearance.Font = new System.Drawing.Font("Tahoma", 9f);
		this.repositoryItemDateEdit3.Appearance.Options.UseFont = true;
		this.repositoryItemDateEdit3.Appearance.Options.UseTextOptions = true;
		this.repositoryItemDateEdit3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.repositoryItemDateEdit3.AutoHeight = false;
		this.repositoryItemDateEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit3.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit3.Name = "repositoryItemDateEdit3";
		this.barEditItem1.Caption = "barEditItem1";
		this.barEditItem1.Edit = this.repositoryItemDateEdit4;
		this.barEditItem1.Id = 141;
		this.barEditItem1.Name = "barEditItem1";
		this.repositoryItemDateEdit4.AutoHeight = false;
		this.repositoryItemDateEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit4.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit4.Name = "repositoryItemDateEdit4";
		this.LblVerg.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblVerg.Caption = "Verg";
		this.LblVerg.Id = 142;
		this.LblVerg.Name = "LblVerg";
		this.LblVerg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnReportAllOrdersRestaurant.Caption = "مبيعات اليومية خلال فترة";
		this.BtnReportAllOrdersRestaurant.Id = 143;
		this.BtnReportAllOrdersRestaurant.Name = "BtnReportAllOrdersRestaurant";
		this.BtnReportAllOrdersRestaurant.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersRestaurant_ItemClick);
		this.BtnCheckOutOrder.Caption = "صرف حجز البيع";
		this.BtnCheckOutOrder.Id = 144;
		this.BtnCheckOutOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCheckOutOrder.ImageOptions.Image");
		this.BtnCheckOutOrder.Name = "BtnCheckOutOrder";
		this.BtnCheckOutOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckOutOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckOutOrder_ItemClick);
		this.BtnConnection.Caption = "الاتصال بقاعدة البيانات";
		this.BtnConnection.Id = 145;
		this.BtnConnection.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnConnection.ImageOptions.Image");
		this.BtnConnection.Name = "BtnConnection";
		this.BtnConnection.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnConnection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnConnection_ItemClick);
		this.BtnTransfer.Caption = "تحويل الاصناف بين المستودعات";
		this.BtnTransfer.Id = 147;
		this.BtnTransfer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTransfer.ImageOptions.Image");
		this.BtnTransfer.Name = "BtnTransfer";
		this.BtnTransfer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTransfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTransfer_ItemClick);
		this.BtnDabase.Caption = "اعداد قواعد البيانات";
		this.BtnDabase.Id = 151;
		this.BtnDabase.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDabase.ImageOptions.Image");
		this.BtnDabase.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnClearDB),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnEmptyDB),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnBackup)
		});
		this.BtnDabase.Name = "BtnDabase";
		this.BtnDabase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnClearDB.Caption = "تصفير حركات قاعدة البيانات";
		this.BtnClearDB.Id = 152;
		this.BtnClearDB.Name = "BtnClearDB";
		this.BtnClearDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnClearDB_ItemClick);
		this.BtnEmptyDB.Caption = "مسح جميع الحركات + الاصناف";
		this.BtnEmptyDB.Id = 153;
		this.BtnEmptyDB.Name = "BtnEmptyDB";
		this.BtnEmptyDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmptyDB_ItemClick);
		this.BtnBackup.Caption = "أخذ نسخة احتياطية";
		this.BtnBackup.Id = 163;
		this.BtnBackup.Name = "BtnBackup";
		this.BtnBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBackup_ItemClick);
		this.barSubItem6.Caption = "العملاء والموردين";
		this.barSubItem6.Id = 154;
		this.barSubItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem6.ImageOptions.Image");
		this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerSupp)
		});
		this.barSubItem6.Name = "barSubItem6";
		this.barSubItem6.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRPCustomer.Caption = "تقرير العملاء";
		this.BtnRPCustomer.Id = 155;
		this.BtnRPCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.Image");
		this.BtnRPCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.LargeImage");
		this.BtnRPCustomer.Name = "BtnRPCustomer";
		this.BtnRPCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCustomer_ItemClick);
		this.BtnRPSupp.Caption = "تقرير الموردين";
		this.BtnRPSupp.Id = 156;
		this.BtnRPSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.Image");
		this.BtnRPSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.LargeImage");
		this.BtnRPSupp.Name = "BtnRPSupp";
		this.BtnRPSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPSupp_ItemClick);
		this.BtnRPLeadgerCustomer.Caption = "كشف حساب العملاء";
		this.BtnRPLeadgerCustomer.Id = 157;
		this.BtnRPLeadgerCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.Image");
		this.BtnRPLeadgerCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.LargeImage");
		this.BtnRPLeadgerCustomer.Name = "BtnRPLeadgerCustomer";
		this.BtnRPLeadgerCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerCustomer_ItemClick);
		this.BtnRPLeadgerSupp.Caption = "كشف حساب الموردين";
		this.BtnRPLeadgerSupp.Id = 158;
		this.BtnRPLeadgerSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.Image");
		this.BtnRPLeadgerSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.LargeImage");
		this.BtnRPLeadgerSupp.Name = "BtnRPLeadgerSupp";
		this.BtnRPLeadgerSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerSupp_ItemClick);
		this.BtnVat.Caption = "حساب ضريبة القيمة المضافة";
		this.BtnVat.Id = 164;
		this.BtnVat.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnVat.ImageOptions.Image");
		this.BtnVat.Name = "BtnVat";
		this.BtnVat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnVat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnVat_ItemClick);
		this.barCheckItem2.Caption = "barCheckItem2";
		this.barCheckItem2.Id = 166;
		this.barCheckItem2.Name = "barCheckItem2";
		this.BtnEncriptPassword.Caption = "تشفير كلمة المرور";
		this.BtnEncriptPassword.Id = 167;
		this.BtnEncriptPassword.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEncriptPassword.ImageOptions.Image");
		this.BtnEncriptPassword.Name = "BtnEncriptPassword";
		this.BtnEncriptPassword.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEncriptPassword.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnEncriptPassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEncriptPassword_ItemClick);
		this.barButtonItem1.Caption = "قفل مؤقت";
		this.barButtonItem1.Id = 168;
		this.barButtonItem1.Name = "barButtonItem1";
		this.barButtonItem2.Caption = "barButtonItem2";
		this.barButtonItem2.Id = 169;
		this.barButtonItem2.Name = "barButtonItem2";
		this.BtnLock.Caption = "قفل مؤقت";
		this.BtnLock.Id = 170;
		this.BtnLock.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnLock.ImageOptions.Image");
		this.BtnLock.Name = "BtnLock";
		this.BtnLock.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnLock_ItemClick);
		this.BtnEmployee.Caption = "موظف جديد";
		this.BtnEmployee.Id = 172;
		this.BtnEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEmployee.ImageOptions.Image");
		this.BtnEmployee.Name = "BtnEmployee";
		this.BtnEmployee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmployee_ItemClick);
		this.BtnPayrollTest.Caption = "نموذج_رواتب";
		this.BtnPayrollTest.Id = 173;
		this.BtnPayrollTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayrollTest.ImageOptions.Image");
		this.BtnPayrollTest.Name = "BtnPayrollTest";
		this.BtnPayrollTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayrollTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayrollTest_ItemClick);
		this.BtnPayroll.Caption = "صرف الرواتب";
		this.BtnPayroll.Id = 174;
		this.BtnPayroll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayroll.ImageOptions.Image");
		this.BtnPayroll.Name = "BtnPayroll";
		this.BtnPayroll.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayroll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayroll_ItemClick);
		this.BtnRestraunt.Caption = "المطعم";
		this.BtnRestraunt.Id = 176;
		this.BtnRestraunt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRestraunt.ImageOptions.Image");
		this.BtnRestraunt.Name = "BtnRestraunt";
		this.BtnRestraunt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRestraunt.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRestraunt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
		this.skinRibbonGalleryBarItem2.Caption = "skinRibbonGalleryBarItem2";
		this.skinRibbonGalleryBarItem2.Id = 177;
		this.skinRibbonGalleryBarItem2.Name = "skinRibbonGalleryBarItem2";
		this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[4] { this.ribbonPageGroupNavigation, this.ribbonPageGroup9, this.ribbonPageGroup10, this.ribbonPageGroup22 });
		this.ribbonPage.Name = "ribbonPage";
		this.ribbonPage.Text = "قسم الاصناف والعملاء والموردين";
		this.ribbonPage.Visible = false;
		this.ribbonPageGroupNavigation.AllowTextClipping = false;
		this.ribbonPageGroupNavigation.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemCompany);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemGroups);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemUnits);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemClass);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItem);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnVat);
		this.ribbonPageGroupNavigation.Name = "ribbonPageGroupNavigation";
		this.ribbonPageGroupNavigation.Text = "البيانات الرئيسية";
		this.ribbonPageGroup9.AllowTextClipping = false;
		this.ribbonPageGroup9.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnBranches);
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnStores);
		this.ribbonPageGroup9.Name = "ribbonPageGroup9";
		this.ribbonPageGroup9.Text = "الفروع والمخازن";
		this.ribbonPageGroup10.AllowTextClipping = false;
		this.ribbonPageGroup10.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusPlace);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSalesMan);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCustomer);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSupplier);
		this.ribbonPageGroup10.Name = "ribbonPageGroup10";
		this.ribbonPageGroup10.Text = "بيانات العملاء والموردين";
		this.ribbonPageGroup22.AllowTextClipping = false;
		this.ribbonPageGroup22.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup22.ItemLinks.Add(this.BtnLock);
		this.ribbonPageGroup22.Name = "ribbonPageGroup22";
		this.ribbonPageGroup22.Text = "قفل مؤقت (Ctrl+L)";
		this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[6] { this.ribbonPageGroup20, this.ribbonPageGroup21, this.ribbonPageGroup1, this.ribbonPageGroup7, this.ribbonPageGroup8, this.ribbonPageGroup18 });
		this.ribbonPage1.Name = "ribbonPage1";
		this.ribbonPage1.Text = "قسم فواتير المبيعات والمشتريات";
		this.ribbonPage1.Visible = false;
		this.ribbonPageGroup20.AllowTextClipping = false;
		this.ribbonPageGroup20.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDOpenQuantity);
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnTransfer);
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDPriceOffer);
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDPurchaseOrder);
		this.ribbonPageGroup20.Name = "ribbonPageGroup20";
		this.ribbonPageGroup20.Text = "الارصدة الافتتاحية";
		this.ribbonPageGroup21.AllowTextClipping = false;
		this.ribbonPageGroup21.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnCashir);
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnRestraunt);
		this.ribbonPageGroup21.Name = "ribbonPageGroup21";
		this.ribbonPageGroup21.Text = "الكاشير";
		this.ribbonPageGroup1.AllowTextClipping = false;
		this.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPurchase);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnCheckOutOrder);
		this.ribbonPageGroup1.Name = "ribbonPageGroup1";
		this.ribbonPageGroup1.Text = "حركات المخزون";
		this.ribbonPageGroup7.AllowTextClipping = false;
		this.ribbonPageGroup7.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDOrderReturnID);
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDPurchaseReturnID);
		this.ribbonPageGroup7.Name = "ribbonPageGroup7";
		this.ribbonPageGroup7.Text = "المرتجعات برقم الفاتورة";
		this.ribbonPageGroup8.AllowTextClipping = false;
		this.ribbonPageGroup8.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDOrderReturnNotID);
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDPurchaseReturnNotID);
		this.ribbonPageGroup8.Name = "ribbonPageGroup8";
		this.ribbonPageGroup8.Text = "مرتجعات بدون رقم الفاتورة";
		this.ribbonPageGroup18.AllowTextClipping = false;
		this.ribbonPageGroup18.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnDateApp);
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnUpdateDateApp);
		this.ribbonPageGroup18.Name = "ribbonPageGroup18";
		this.ribbonPageGroup18.Text = "اعداد تاريخ الكاشير";
		this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup11, this.ribbonPageGroup2 });
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "قسم الحسابات";
		this.ribbonPage2.Visible = false;
		this.ribbonPageGroup11.AllowTextClipping = false;
		this.ribbonPageGroup11.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCardAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccTreeAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictions);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictionsTest);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccOpenMoney);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCostCenter);
		this.ribbonPageGroup11.Name = "ribbonPageGroup11";
		this.ribbonPageGroup11.Text = "بيانات الحسابات";
		this.ribbonPageGroup2.AllowTextClipping = false;
		this.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnRecipt);
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnPayment);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem7);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem8);
		this.ribbonPageGroup2.Name = "ribbonPageGroup2";
		this.ribbonPageGroup2.Text = "حركات الحسابات";
		this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup3 });
		this.ribbonPage3.Name = "ribbonPage3";
		this.ribbonPage3.Text = "الموارد البشرية";
		this.ribbonPage3.Visible = false;
		this.ribbonPageGroup3.AllowTextClipping = false;
		this.ribbonPageGroup3.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnEmployee);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayrollTest);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayroll);
		this.ribbonPageGroup3.Name = "ribbonPageGroup3";
		this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup4 });
		this.ribbonPage4.Name = "ribbonPage4";
		this.ribbonPage4.Text = " قسم الصلاحيات";
		this.ribbonPage4.Visible = false;
		this.ribbonPageGroup4.AllowTextClipping = false;
		this.ribbonPageGroup4.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnUsers);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnGroups);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnPermission);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnEncriptPassword);
		this.ribbonPageGroup4.Name = "ribbonPageGroup4";
		this.ribbonPageGroup4.Text = "بيانات المستخدمين";
		this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[7] { this.ribbonPageGroup5, this.ribbonPageGroup19, this.ribbonPageGroup12, this.ribbonPageGroup13, this.ribbonPageGroup14, this.ribbonPageGroup15, this.ribbonPageGroup16 });
		this.ribbonPage5.Name = "ribbonPage5";
		this.ribbonPage5.Text = " قسم التقارير";
		this.ribbonPage5.Visible = false;
		this.ribbonPageGroup5.AllowTextClipping = false;
		this.ribbonPageGroup5.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup5.ItemLinks.Add(this.BtnReportPublic);
		this.ribbonPageGroup5.Name = "ribbonPageGroup5";
		this.ribbonPageGroup5.Text = "تقارير عامة";
		this.ribbonPageGroup19.AllowTextClipping = false;
		this.ribbonPageGroup19.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup19.ItemLinks.Add(this.barSubItem6);
		this.ribbonPageGroup19.Name = "ribbonPageGroup19";
		this.ribbonPageGroup19.Text = "العملاء والموردين";
		this.ribbonPageGroup12.AllowTextClipping = false;
		this.ribbonPageGroup12.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup12.ItemLinks.Add(this.BtnReportStore);
		this.ribbonPageGroup12.Name = "ribbonPageGroup12";
		this.ribbonPageGroup12.Text = " المخازن";
		this.ribbonPageGroup13.AllowTextClipping = false;
		this.ribbonPageGroup13.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup13.ItemLinks.Add(this.BtnReportPurshes);
		this.ribbonPageGroup13.Name = "ribbonPageGroup13";
		this.ribbonPageGroup13.Text = " المشتريات";
		this.ribbonPageGroup14.AllowTextClipping = false;
		this.ribbonPageGroup14.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup14.ItemLinks.Add(this.BtnReportOrder);
		this.ribbonPageGroup14.Name = "ribbonPageGroup14";
		this.ribbonPageGroup14.Text = " المبيعات";
		this.ribbonPageGroup15.AllowTextClipping = false;
		this.ribbonPageGroup15.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup15.ItemLinks.Add(this.BtnReportProfit);
		this.ribbonPageGroup15.Name = "ribbonPageGroup15";
		this.ribbonPageGroup15.Text = "تقارير الارباح";
		this.ribbonPageGroup16.AllowTextClipping = false;
		this.ribbonPageGroup16.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup16.ItemLinks.Add(this.BtnReportAccount);
		this.ribbonPageGroup16.Name = "ribbonPageGroup16";
		this.ribbonPageGroup16.Text = "الحسابات";
		this.ribbonPage6.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup6, this.ribbonPageGroup17 });
		this.ribbonPage6.Name = "ribbonPage6";
		this.ribbonPage6.Text = "قسم الاعدادات";
		this.ribbonPage6.Visible = false;
		this.ribbonPageGroup6.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnPrinters);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnSetting);
		this.ribbonPageGroup6.ItemLinks.Add(this.btnDefualtAccount);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnConnection);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnDabase);
		this.ribbonPageGroup6.Name = "ribbonPageGroup6";
		this.ribbonPageGroup6.Text = "الاعدادات الرئيسية للبرنامج";
		this.ribbonPageGroup17.AllowTextClipping = false;
		this.ribbonPageGroup17.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnAbout);
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnReportDesign);
		this.ribbonPageGroup17.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
		this.ribbonPageGroup17.Name = "ribbonPageGroup17";
		this.ribbonPageGroup17.Text = "المصمم والجرافيك";
		this.repositoryItemColorEdit1.AutoHeight = false;
		this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
		this.repositoryItemColorPickEdit1.AutoHeight = false;
		this.repositoryItemColorPickEdit1.AutomaticColor = System.Drawing.Color.Black;
		this.repositoryItemColorPickEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorPickEdit1.Name = "repositoryItemColorPickEdit1";
		this.repositoryItemColorEdit2.AutoHeight = false;
		this.repositoryItemColorEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit2.Name = "repositoryItemColorEdit2";
		this.repositoryItemDateEdit1.AutoHeight = false;
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		this.repositoryItemTimeSpanEdit1.AutoHeight = false;
		this.repositoryItemTimeSpanEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemTimeSpanEdit1.Mask.EditMask = "dd.HH:mm:ss";
		this.repositoryItemTimeSpanEdit1.Name = "repositoryItemTimeSpanEdit1";
		this.repositoryItemTimeEdit1.AutoHeight = false;
		this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
		this.ribbonStatusBar.ItemLinks.Add(this.LblCompany);
		this.ribbonStatusBar.ItemLinks.Add(this.LblVerg);
		this.ribbonStatusBar.ItemLinks.Add(this.LblServer);
		this.ribbonStatusBar.ItemLinks.Add(this.LblUser);
		this.ribbonStatusBar.ItemLinks.Add(this.LblTime);
		this.ribbonStatusBar.ItemLinks.Add(this.LblDate);
		this.ribbonStatusBar.Location = new System.Drawing.Point(0, 723);
		this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ribbonStatusBar.Name = "ribbonStatusBar";
		this.ribbonStatusBar.Ribbon = this.ribbonControl;
		this.ribbonStatusBar.Size = new System.Drawing.Size(1172, 30);
		this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
		this.navigationFrame.Appearance.Options.UseBackColor = true;
		this.navigationFrame.Controls.Add(this.employeesNavigationPage);
		this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
		this.navigationFrame.Location = new System.Drawing.Point(0, 193);
		this.navigationFrame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.navigationFrame.Name = "navigationFrame";
		this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[1] { this.employeesNavigationPage });
		this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
		this.navigationFrame.SelectedPage = this.employeesNavigationPage;
		this.navigationFrame.Size = new System.Drawing.Size(1172, 530);
		this.navigationFrame.TabIndex = 0;
		this.navigationFrame.Text = "navigationFrame";
		this.employeesNavigationPage.BackgroundImage = GTSErpSystem.Properties.Resources.Somw;
		this.employeesNavigationPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.employeesNavigationPage.Controls.Add(this.label1);
		this.employeesNavigationPage.Controls.Add(this.navBarControl2);
		this.employeesNavigationPage.Controls.Add(this.navBarControl1);
		this.employeesNavigationPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.employeesNavigationPage.Name = "employeesNavigationPage";
		this.employeesNavigationPage.Size = new System.Drawing.Size(1172, 530);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.BackColor = System.Drawing.Color.FromArgb(246, 248, 255);
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(0, 359);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.label1.Size = new System.Drawing.Size(386, 148);
		this.label1.TabIndex = 65;
		this.label1.Text = resources.GetString("label1.Text");
		this.navBarControl2.ActiveGroup = this.navBarGroup12;
		this.navBarControl2.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[1] { this.navBarGroup12 });
		this.navBarControl2.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[7] { this.NavCompany, this.NavUser, this.NavDataBase, this.NavYearClosing, this.NavVerg, this.NavDate, this.NavServer });
		this.navBarControl2.Location = new System.Drawing.Point(0, 5);
		this.navBarControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.navBarControl2.Name = "navBarControl2";
		this.navBarControl2.OptionsNavPane.ExpandedWidth = 390;
		this.navBarControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.navBarControl2.Size = new System.Drawing.Size(390, 414);
		this.navBarControl2.TabIndex = 3;
		this.navBarControl2.Text = "navBarControl2";
		this.navBarGroup12.Caption = "بيانات الشركة";
		this.navBarGroup12.Expanded = true;
		this.navBarGroup12.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[7]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCompany),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavUser),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDataBase),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavYearClosing),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavVerg),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDate),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavServer)
		});
		this.navBarGroup12.Name = "navBarGroup12";
		this.NavCompany.Appearance.Options.UseTextOptions = true;
		this.NavCompany.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavCompany.Caption = "الشركة";
		this.NavCompany.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavCompany.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("NavCompany.ImageOptions.LargeImage");
		this.NavCompany.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavCompany.ImageOptions.SmallImage");
		this.NavCompany.Name = "NavCompany";
		this.NavUser.Appearance.Options.UseTextOptions = true;
		this.NavUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavUser.Caption = "الموظف";
		this.NavUser.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavUser.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavUser.ImageOptions.SmallImage");
		this.NavUser.Name = "NavUser";
		this.NavDataBase.Appearance.Options.UseTextOptions = true;
		this.NavDataBase.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavDataBase.Caption = "قاعدة البيانات";
		this.NavDataBase.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavDataBase.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavDataBase.ImageOptions.SmallImage");
		this.NavDataBase.Name = "NavDataBase";
		this.NavYearClosing.Appearance.Options.UseTextOptions = true;
		this.NavYearClosing.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavYearClosing.Caption = "السنة المالية";
		this.NavYearClosing.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavYearClosing.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavYearClosing.ImageOptions.SmallImage");
		this.NavYearClosing.Name = "NavYearClosing";
		this.NavVerg.Appearance.Options.UseTextOptions = true;
		this.NavVerg.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavVerg.Caption = "الاصدار";
		this.NavVerg.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavVerg.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavVerg.ImageOptions.SmallImage");
		this.NavVerg.Name = "NavVerg";
		this.NavDate.Appearance.Options.UseTextOptions = true;
		this.NavDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavDate.Caption = "التاريخ";
		this.NavDate.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavDate.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavDate.ImageOptions.SmallImage");
		this.NavDate.Name = "NavDate";
		this.NavServer.Appearance.Options.UseTextOptions = true;
		this.NavServer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
		this.NavServer.Caption = "السيرفر";
		this.NavServer.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.NavServer.ImageOptions.SmallImage = (System.Drawing.Image)resources.GetObject("NavServer.ImageOptions.SmallImage");
		this.NavServer.Name = "NavServer";
		this.navBarControl1.ActiveGroup = this.navBarGroup1;
		this.navBarControl1.AllowDrop = false;
		this.navBarControl1.AllowHorizontalResizing = DevExpress.Utils.DefaultBoolean.True;
		this.navBarControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.navBarControl1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
		this.navBarControl1.Font = new System.Drawing.Font("Tahoma", 7.8f);
		this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[11]
		{
			this.navBarGroup1, this.navBarGroup2, this.navBarGroup3, this.navBarGroup4, this.navBarGroup5, this.navBarGroup6, this.navBarGroup7, this.navBarGroup8, this.navBarGroup9, this.navBarGroup10,
			this.navBarGroup11
		});
		this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[53]
		{
			this.NavUnit, this.NavGroups, this.NavCompnys, this.NavClass, this.NavPriceOffer, this.NavOrders, this.NavCashier, this.NavPurchesOrder, this.NavPurches, this.navBarSeparatorItem7,
			this.NavPurchesReturn, this.NavPurchesReturnNoID, this.NavOpenQuantity, this.NavStoreTransfer, this.NavCustomer, this.NavSuppliers, this.NavSalesMan, this.NavPlaceCus, this.NavItemAccount, this.NavTreeAccount,
			this.navBarSeparatorItem8, this.NavDailyRestrictionsTest, this.NavDailyRestrictions, this.NavReceipts, this.NavPayment, this.NavUsers, this.NavGroup, this.navBarSeparatorItem2, this.NanPermission, this.NavSetting,
			this.NavPrinter, this.NavClear, this.NavClearAll, this.NavAbout, this.NavExit, this.NavCountry, this.NavGuarantee, this.NavItem, this.NavGroupRes, this.NavResturant,
			this.NavOrderOut, this.navBarSeparatorItem1, this.NavReturnOrder, this.NavReturnOrderNoID, this.NavPassword, this.NavDateCasher, this.navBarSeparatorItem3, this.NavReceiptsAll, this.NavPaymentAll, this.NavReport,
			this.NavDesign, this.NavAccountDefualt, this.NavCustomerDefualt
		});
		this.navBarControl1.Location = new System.Drawing.Point(588, 0);
		this.navBarControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.navBarControl1.Name = "navBarControl1";
		this.navBarControl1.OptionsNavPane.ExpandedWidth = 390;
		this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
		this.navBarControl1.Size = new System.Drawing.Size(390, 503);
		this.navBarControl1.StoreDefaultPaintStyleName = true;
		this.navBarControl1.TabIndex = 2;
		this.navBarControl1.Text = "navBarControl1";
		this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup1.Appearance.Options.UseFont = true;
		this.navBarGroup1.Appearance.Options.UseTextOptions = true;
		this.navBarGroup1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup1.Caption = "إدارة الاصناف";
		this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[8]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavUnit),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavGroups),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCompnys),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavClass),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCountry),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavGuarantee),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavItem),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavGroupRes)
		});
		this.navBarGroup1.Name = "navBarGroup1";
		this.NavUnit.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUnit.Appearance.Options.UseFont = true;
		this.NavUnit.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUnit.AppearanceDisabled.Options.UseFont = true;
		this.NavUnit.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUnit.AppearanceHotTracked.Options.UseFont = true;
		this.NavUnit.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUnit.AppearancePressed.Options.UseFont = true;
		this.NavUnit.Caption = "وحدات القياس";
		this.NavUnit.Name = "NavUnit";
		this.NavUnit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavUnit_LinkClicked);
		this.NavGroups.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroups.Appearance.Options.UseFont = true;
		this.NavGroups.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroups.AppearanceDisabled.Options.UseFont = true;
		this.NavGroups.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroups.AppearanceHotTracked.Options.UseFont = true;
		this.NavGroups.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroups.AppearancePressed.Options.UseFont = true;
		this.NavGroups.Caption = "المجموعات ";
		this.NavGroups.Name = "NavGroups";
		this.NavGroups.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavGroups_LinkClicked);
		this.NavCompnys.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCompnys.Appearance.Options.UseFont = true;
		this.NavCompnys.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCompnys.AppearanceDisabled.Options.UseFont = true;
		this.NavCompnys.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCompnys.AppearanceHotTracked.Options.UseFont = true;
		this.NavCompnys.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCompnys.AppearancePressed.Options.UseFont = true;
		this.NavCompnys.Caption = "الشركات";
		this.NavCompnys.Name = "NavCompnys";
		this.NavCompnys.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavCompnys_LinkClicked);
		this.NavClass.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClass.Appearance.Options.UseFont = true;
		this.NavClass.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClass.AppearanceDisabled.Options.UseFont = true;
		this.NavClass.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClass.AppearanceHotTracked.Options.UseFont = true;
		this.NavClass.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClass.AppearancePressed.Options.UseFont = true;
		this.NavClass.Caption = "الفئات";
		this.NavClass.Name = "NavClass";
		this.NavClass.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavClass_LinkClicked);
		this.NavCountry.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCountry.Appearance.Options.UseFont = true;
		this.NavCountry.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCountry.AppearanceDisabled.Options.UseFont = true;
		this.NavCountry.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCountry.AppearanceHotTracked.Options.UseFont = true;
		this.NavCountry.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCountry.AppearancePressed.Options.UseFont = true;
		this.NavCountry.Caption = "بلد المنشأ";
		this.NavCountry.Name = "NavCountry";
		this.NavCountry.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavCountry_LinkClicked);
		this.NavGuarantee.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGuarantee.Appearance.Options.UseFont = true;
		this.NavGuarantee.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGuarantee.AppearanceDisabled.Options.UseFont = true;
		this.NavGuarantee.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGuarantee.AppearanceHotTracked.Options.UseFont = true;
		this.NavGuarantee.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGuarantee.AppearancePressed.Options.UseFont = true;
		this.NavGuarantee.Caption = "فترة الضمان";
		this.NavGuarantee.Name = "NavGuarantee";
		this.NavGuarantee.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavGuarantee_LinkClicked);
		this.NavItem.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItem.Appearance.Options.UseFont = true;
		this.NavItem.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItem.AppearanceDisabled.Options.UseFont = true;
		this.NavItem.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItem.AppearanceHotTracked.Options.UseFont = true;
		this.NavItem.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItem.AppearancePressed.Options.UseFont = true;
		this.NavItem.Caption = "بطاقة الصنف";
		this.NavItem.Name = "NavItem";
		this.NavItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavItem_LinkClicked);
		this.NavGroupRes.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroupRes.Appearance.Options.UseFont = true;
		this.NavGroupRes.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroupRes.AppearanceDisabled.Options.UseFont = true;
		this.NavGroupRes.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroupRes.AppearanceHotTracked.Options.UseFont = true;
		this.NavGroupRes.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroupRes.AppearancePressed.Options.UseFont = true;
		this.NavGroupRes.Caption = "مجموعات المطعم";
		this.NavGroupRes.Name = "NavGroupRes";
		this.NavGroupRes.Visible = false;
		this.NavGroupRes.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavGroupRes_LinkClicked);
		this.navBarGroup2.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup2.Appearance.Options.UseFont = true;
		this.navBarGroup2.Appearance.Options.UseTextOptions = true;
		this.navBarGroup2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup2.Caption = "إدارة المبيعات";
		this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[9]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPriceOffer),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavOrders),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCashier),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavResturant),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavOrderOut),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem1),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavReturnOrder),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavReturnOrderNoID),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDateCasher)
		});
		this.navBarGroup2.Name = "navBarGroup2";
		this.NavPriceOffer.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPriceOffer.Appearance.Options.UseFont = true;
		this.NavPriceOffer.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPriceOffer.AppearanceDisabled.Options.UseFont = true;
		this.NavPriceOffer.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPriceOffer.AppearanceHotTracked.Options.UseFont = true;
		this.NavPriceOffer.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPriceOffer.AppearancePressed.Options.UseFont = true;
		this.NavPriceOffer.Caption = "عرض السعر لعميل";
		this.NavPriceOffer.Name = "NavPriceOffer";
		this.NavPriceOffer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPriceOffer_LinkClicked);
		this.NavOrders.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrders.Appearance.Options.UseFont = true;
		this.NavOrders.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrders.AppearanceDisabled.Options.UseFont = true;
		this.NavOrders.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrders.AppearanceHotTracked.Options.UseFont = true;
		this.NavOrders.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrders.AppearancePressed.Options.UseFont = true;
		this.NavOrders.Caption = "فواتير المبيعات ";
		this.NavOrders.Name = "NavOrders";
		this.NavOrders.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavOrders_LinkClicked);
		this.NavCashier.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCashier.Appearance.Options.UseFont = true;
		this.NavCashier.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCashier.AppearanceDisabled.Options.UseFont = true;
		this.NavCashier.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCashier.AppearanceHotTracked.Options.UseFont = true;
		this.NavCashier.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCashier.AppearancePressed.Options.UseFont = true;
		this.NavCashier.Caption = "نقاط البيع";
		this.NavCashier.Name = "NavCashier";
		this.NavCashier.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavCashier_LinkClicked);
		this.NavResturant.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavResturant.Appearance.Options.UseFont = true;
		this.NavResturant.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavResturant.AppearanceDisabled.Options.UseFont = true;
		this.NavResturant.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavResturant.AppearanceHotTracked.Options.UseFont = true;
		this.NavResturant.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavResturant.AppearancePressed.Options.UseFont = true;
		this.NavResturant.Caption = "المطعم";
		this.NavResturant.Name = "NavResturant";
		this.NavResturant.Visible = false;
		this.NavResturant.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavResturant_LinkClicked);
		this.NavOrderOut.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrderOut.Appearance.Options.UseFont = true;
		this.NavOrderOut.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrderOut.AppearanceDisabled.Options.UseFont = true;
		this.NavOrderOut.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrderOut.AppearanceHotTracked.Options.UseFont = true;
		this.NavOrderOut.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOrderOut.AppearancePressed.Options.UseFont = true;
		this.NavOrderOut.Caption = "صرف المبيعات المحجوزة";
		this.NavOrderOut.Name = "NavOrderOut";
		this.NavOrderOut.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavOrderOut_LinkClicked);
		this.navBarSeparatorItem1.CanDrag = false;
		this.navBarSeparatorItem1.Enabled = false;
		this.navBarSeparatorItem1.Hint = null;
		this.navBarSeparatorItem1.Name = "navBarSeparatorItem1";
		this.NavReturnOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrder.Appearance.Options.UseFont = true;
		this.NavReturnOrder.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrder.AppearanceDisabled.Options.UseFont = true;
		this.NavReturnOrder.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrder.AppearanceHotTracked.Options.UseFont = true;
		this.NavReturnOrder.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrder.AppearancePressed.Options.UseFont = true;
		this.NavReturnOrder.Caption = "مردودات المبيعات";
		this.NavReturnOrder.Name = "NavReturnOrder";
		this.NavReturnOrder.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavReturnOrder_LinkClicked);
		this.NavReturnOrderNoID.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrderNoID.Appearance.Options.UseFont = true;
		this.NavReturnOrderNoID.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrderNoID.AppearanceDisabled.Options.UseFont = true;
		this.NavReturnOrderNoID.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrderNoID.AppearanceHotTracked.Options.UseFont = true;
		this.NavReturnOrderNoID.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReturnOrderNoID.AppearancePressed.Options.UseFont = true;
		this.NavReturnOrderNoID.Caption = "مردودات المبيعات بدون بيع";
		this.NavReturnOrderNoID.Name = "NavReturnOrderNoID";
		this.NavReturnOrderNoID.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavReturnOrderNoID_LinkClicked);
		this.NavDateCasher.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDateCasher.Appearance.Options.UseFont = true;
		this.NavDateCasher.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDateCasher.AppearanceDisabled.Options.UseFont = true;
		this.NavDateCasher.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDateCasher.AppearanceHotTracked.Options.UseFont = true;
		this.NavDateCasher.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDateCasher.AppearancePressed.Options.UseFont = true;
		this.NavDateCasher.Caption = "اعداد تاريخ الكاشير";
		this.NavDateCasher.Name = "NavDateCasher";
		this.NavDateCasher.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavDateCasher_LinkClicked);
		this.navBarGroup3.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup3.Appearance.Options.UseFont = true;
		this.navBarGroup3.Appearance.Options.UseTextOptions = true;
		this.navBarGroup3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup3.Caption = "إدارة المشتريات";
		this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[5]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPurchesOrder),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPurches),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem7),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPurchesReturn),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPurchesReturnNoID)
		});
		this.navBarGroup3.Name = "navBarGroup3";
		this.NavPurchesOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesOrder.Appearance.Options.UseFont = true;
		this.NavPurchesOrder.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesOrder.AppearanceDisabled.Options.UseFont = true;
		this.NavPurchesOrder.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesOrder.AppearanceHotTracked.Options.UseFont = true;
		this.NavPurchesOrder.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesOrder.AppearancePressed.Options.UseFont = true;
		this.NavPurchesOrder.Caption = "طلب شراء من مورد";
		this.NavPurchesOrder.Name = "NavPurchesOrder";
		this.NavPurchesOrder.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPurchesOrder_LinkClicked);
		this.NavPurches.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurches.Appearance.Options.UseFont = true;
		this.NavPurches.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurches.AppearanceDisabled.Options.UseFont = true;
		this.NavPurches.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurches.AppearanceHotTracked.Options.UseFont = true;
		this.NavPurches.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurches.AppearancePressed.Options.UseFont = true;
		this.NavPurches.Caption = "فواتير المشتريات";
		this.NavPurches.Name = "NavPurches";
		this.NavPurches.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPurches_LinkClicked);
		this.navBarSeparatorItem7.CanDrag = false;
		this.navBarSeparatorItem7.Enabled = false;
		this.navBarSeparatorItem7.Hint = null;
		this.navBarSeparatorItem7.Name = "navBarSeparatorItem7";
		this.NavPurchesReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturn.Appearance.Options.UseFont = true;
		this.NavPurchesReturn.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturn.AppearanceDisabled.Options.UseFont = true;
		this.NavPurchesReturn.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturn.AppearanceHotTracked.Options.UseFont = true;
		this.NavPurchesReturn.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturn.AppearancePressed.Options.UseFont = true;
		this.NavPurchesReturn.Caption = "مردودات المشتريات";
		this.NavPurchesReturn.Name = "NavPurchesReturn";
		this.NavPurchesReturn.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPurchesReturn_LinkClicked);
		this.NavPurchesReturnNoID.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturnNoID.Appearance.Options.UseFont = true;
		this.NavPurchesReturnNoID.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturnNoID.AppearanceDisabled.Options.UseFont = true;
		this.NavPurchesReturnNoID.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturnNoID.AppearanceHotTracked.Options.UseFont = true;
		this.NavPurchesReturnNoID.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPurchesReturnNoID.AppearancePressed.Options.UseFont = true;
		this.NavPurchesReturnNoID.Caption = "مردودات المشتريات بدون فاتورة";
		this.NavPurchesReturnNoID.Name = "NavPurchesReturnNoID";
		this.NavPurchesReturnNoID.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPurchesReturnNoID_LinkClicked);
		this.navBarGroup4.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup4.Appearance.Options.UseFont = true;
		this.navBarGroup4.Appearance.Options.UseTextOptions = true;
		this.navBarGroup4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup4.Caption = "إدارة المخزون";
		this.navBarGroup4.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[2]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavOpenQuantity),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavStoreTransfer)
		});
		this.navBarGroup4.Name = "navBarGroup4";
		this.NavOpenQuantity.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOpenQuantity.Appearance.Options.UseFont = true;
		this.NavOpenQuantity.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOpenQuantity.AppearanceDisabled.Options.UseFont = true;
		this.NavOpenQuantity.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOpenQuantity.AppearanceHotTracked.Options.UseFont = true;
		this.NavOpenQuantity.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavOpenQuantity.AppearancePressed.Options.UseFont = true;
		this.NavOpenQuantity.Caption = "رصيد افتتاحي لكميات المخزون";
		this.NavOpenQuantity.Name = "NavOpenQuantity";
		this.NavOpenQuantity.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavOpenQuantity_LinkClicked);
		this.NavStoreTransfer.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavStoreTransfer.Appearance.Options.UseFont = true;
		this.NavStoreTransfer.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavStoreTransfer.AppearanceDisabled.Options.UseFont = true;
		this.NavStoreTransfer.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavStoreTransfer.AppearanceHotTracked.Options.UseFont = true;
		this.NavStoreTransfer.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavStoreTransfer.AppearancePressed.Options.UseFont = true;
		this.NavStoreTransfer.Caption = "تحويل الاصناف بين المستودعات";
		this.NavStoreTransfer.Name = "NavStoreTransfer";
		this.NavStoreTransfer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavStoreTransfer_LinkClicked);
		this.navBarGroup5.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup5.Appearance.Options.UseFont = true;
		this.navBarGroup5.Appearance.Options.UseTextOptions = true;
		this.navBarGroup5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup5.Caption = "إدارة العملاء والموردين";
		this.navBarGroup5.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[4]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCustomer),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavSuppliers),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavSalesMan),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPlaceCus)
		});
		this.navBarGroup5.Name = "navBarGroup5";
		this.NavCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomer.Appearance.Options.UseFont = true;
		this.NavCustomer.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomer.AppearanceDisabled.Options.UseFont = true;
		this.NavCustomer.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomer.AppearanceHotTracked.Options.UseFont = true;
		this.NavCustomer.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomer.AppearancePressed.Options.UseFont = true;
		this.NavCustomer.Caption = "بيانات العملاء";
		this.NavCustomer.Name = "NavCustomer";
		this.NavCustomer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavCustomer_LinkClicked);
		this.NavSuppliers.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSuppliers.Appearance.Options.UseFont = true;
		this.NavSuppliers.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSuppliers.AppearanceDisabled.Options.UseFont = true;
		this.NavSuppliers.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSuppliers.AppearanceHotTracked.Options.UseFont = true;
		this.NavSuppliers.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSuppliers.AppearancePressed.Options.UseFont = true;
		this.NavSuppliers.Caption = "بيانات الموردين";
		this.NavSuppliers.Name = "NavSuppliers";
		this.NavSuppliers.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavSuppliers_LinkClicked);
		this.NavSalesMan.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSalesMan.Appearance.Options.UseFont = true;
		this.NavSalesMan.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSalesMan.AppearanceDisabled.Options.UseFont = true;
		this.NavSalesMan.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSalesMan.AppearanceHotTracked.Options.UseFont = true;
		this.NavSalesMan.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSalesMan.AppearancePressed.Options.UseFont = true;
		this.NavSalesMan.Caption = "بيانات المناديب";
		this.NavSalesMan.Name = "NavSalesMan";
		this.NavSalesMan.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavSalesMan_LinkClicked);
		this.NavPlaceCus.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPlaceCus.Appearance.Options.UseFont = true;
		this.NavPlaceCus.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPlaceCus.AppearanceDisabled.Options.UseFont = true;
		this.NavPlaceCus.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPlaceCus.AppearanceHotTracked.Options.UseFont = true;
		this.NavPlaceCus.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPlaceCus.AppearancePressed.Options.UseFont = true;
		this.NavPlaceCus.Caption = "مناطق المناديب والعملاء";
		this.NavPlaceCus.Name = "NavPlaceCus";
		this.NavPlaceCus.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPlaceCus_LinkClicked);
		this.navBarGroup6.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup6.Appearance.Options.UseFont = true;
		this.navBarGroup6.Appearance.Options.UseTextOptions = true;
		this.navBarGroup6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup6.Caption = "إدارة الحسابات";
		this.navBarGroup6.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[5]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavItemAccount),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavTreeAccount),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem8),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDailyRestrictionsTest),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDailyRestrictions)
		});
		this.navBarGroup6.Name = "navBarGroup6";
		this.NavItemAccount.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItemAccount.Appearance.Options.UseFont = true;
		this.NavItemAccount.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItemAccount.AppearanceDisabled.Options.UseFont = true;
		this.NavItemAccount.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItemAccount.AppearanceHotTracked.Options.UseFont = true;
		this.NavItemAccount.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavItemAccount.AppearancePressed.Options.UseFont = true;
		this.NavItemAccount.Caption = "بطاقة حساب ";
		this.NavItemAccount.Name = "NavItemAccount";
		this.NavItemAccount.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavItemAccount_LinkClicked);
		this.NavTreeAccount.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavTreeAccount.Appearance.Options.UseFont = true;
		this.NavTreeAccount.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavTreeAccount.AppearanceDisabled.Options.UseFont = true;
		this.NavTreeAccount.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavTreeAccount.AppearanceHotTracked.Options.UseFont = true;
		this.NavTreeAccount.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavTreeAccount.AppearancePressed.Options.UseFont = true;
		this.NavTreeAccount.Caption = "دليل الحسابات";
		this.NavTreeAccount.Name = "NavTreeAccount";
		this.NavTreeAccount.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavTreeAccount_LinkClicked);
		this.navBarSeparatorItem8.CanDrag = false;
		this.navBarSeparatorItem8.Enabled = false;
		this.navBarSeparatorItem8.Hint = null;
		this.navBarSeparatorItem8.Name = "navBarSeparatorItem8";
		this.NavDailyRestrictionsTest.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictionsTest.Appearance.Options.UseFont = true;
		this.NavDailyRestrictionsTest.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictionsTest.AppearanceDisabled.Options.UseFont = true;
		this.NavDailyRestrictionsTest.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictionsTest.AppearanceHotTracked.Options.UseFont = true;
		this.NavDailyRestrictionsTest.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictionsTest.AppearancePressed.Options.UseFont = true;
		this.NavDailyRestrictionsTest.Caption = "مسودة قيد";
		this.NavDailyRestrictionsTest.Name = "NavDailyRestrictionsTest";
		this.NavDailyRestrictionsTest.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavDailyRestrictionsTest_LinkClicked);
		this.NavDailyRestrictions.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictions.Appearance.Options.UseFont = true;
		this.NavDailyRestrictions.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictions.AppearanceDisabled.Options.UseFont = true;
		this.NavDailyRestrictions.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictions.AppearanceHotTracked.Options.UseFont = true;
		this.NavDailyRestrictions.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDailyRestrictions.AppearancePressed.Options.UseFont = true;
		this.NavDailyRestrictions.Caption = "قيد يومية";
		this.NavDailyRestrictions.Name = "NavDailyRestrictions";
		this.NavDailyRestrictions.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavDailyRestrictions_LinkClicked);
		this.navBarGroup7.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup7.Appearance.Options.UseFont = true;
		this.navBarGroup7.Appearance.Options.UseTextOptions = true;
		this.navBarGroup7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup7.Caption = "إدارة التعاملات الحسابية";
		this.navBarGroup7.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[5]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavReceipts),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPayment),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem3),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavReceiptsAll),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPaymentAll)
		});
		this.navBarGroup7.Name = "navBarGroup7";
		this.NavReceipts.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceipts.Appearance.Options.UseFont = true;
		this.NavReceipts.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceipts.AppearanceDisabled.Options.UseFont = true;
		this.NavReceipts.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceipts.AppearanceHotTracked.Options.UseFont = true;
		this.NavReceipts.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceipts.AppearancePressed.Options.UseFont = true;
		this.NavReceipts.Caption = "ايصال قبض ";
		this.NavReceipts.Name = "NavReceipts";
		this.NavReceipts.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavReceipts_LinkClicked);
		this.NavPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPayment.Appearance.Options.UseFont = true;
		this.NavPayment.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPayment.AppearanceDisabled.Options.UseFont = true;
		this.NavPayment.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPayment.AppearanceHotTracked.Options.UseFont = true;
		this.NavPayment.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPayment.AppearancePressed.Options.UseFont = true;
		this.NavPayment.Caption = "ايصال صرف ";
		this.NavPayment.Name = "NavPayment";
		this.NavPayment.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPayment_LinkClicked);
		this.navBarSeparatorItem3.CanDrag = false;
		this.navBarSeparatorItem3.Enabled = false;
		this.navBarSeparatorItem3.Hint = null;
		this.navBarSeparatorItem3.Name = "navBarSeparatorItem3";
		this.NavReceiptsAll.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceiptsAll.Appearance.Options.UseFont = true;
		this.NavReceiptsAll.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceiptsAll.AppearanceDisabled.Options.UseFont = true;
		this.NavReceiptsAll.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceiptsAll.AppearanceHotTracked.Options.UseFont = true;
		this.NavReceiptsAll.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReceiptsAll.AppearancePressed.Options.UseFont = true;
		this.NavReceiptsAll.Caption = "ايصال قبض متعدد";
		this.NavReceiptsAll.Name = "NavReceiptsAll";
		this.NavReceiptsAll.Visible = false;
		this.NavReceiptsAll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavReceiptsAll_LinkClicked);
		this.NavPaymentAll.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPaymentAll.Appearance.Options.UseFont = true;
		this.NavPaymentAll.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPaymentAll.AppearanceDisabled.Options.UseFont = true;
		this.NavPaymentAll.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPaymentAll.AppearanceHotTracked.Options.UseFont = true;
		this.NavPaymentAll.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPaymentAll.AppearancePressed.Options.UseFont = true;
		this.NavPaymentAll.Caption = "ايصال صرف متعدد";
		this.NavPaymentAll.Name = "NavPaymentAll";
		this.NavPaymentAll.Visible = false;
		this.NavPaymentAll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPaymentAll_LinkClicked);
		this.navBarGroup8.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup8.Appearance.Options.UseFont = true;
		this.navBarGroup8.Appearance.Options.UseTextOptions = true;
		this.navBarGroup8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup8.Caption = "إدارة الصلاحيات";
		this.navBarGroup8.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[5]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavUsers),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavGroup),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem2),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NanPermission),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPassword)
		});
		this.navBarGroup8.Name = "navBarGroup8";
		this.NavUsers.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUsers.Appearance.Options.UseFont = true;
		this.NavUsers.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUsers.AppearanceDisabled.Options.UseFont = true;
		this.NavUsers.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUsers.AppearanceHotTracked.Options.UseFont = true;
		this.NavUsers.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavUsers.AppearancePressed.Options.UseFont = true;
		this.NavUsers.Caption = "مستخدم جديد";
		this.NavUsers.Name = "NavUsers";
		this.NavUsers.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavUsers_LinkClicked);
		this.NavGroup.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroup.Appearance.Options.UseFont = true;
		this.NavGroup.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroup.AppearanceDisabled.Options.UseFont = true;
		this.NavGroup.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroup.AppearanceHotTracked.Options.UseFont = true;
		this.NavGroup.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavGroup.AppearancePressed.Options.UseFont = true;
		this.NavGroup.Caption = "مجموعة جديدة";
		this.NavGroup.Name = "NavGroup";
		this.NavGroup.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavGroup_LinkClicked);
		this.navBarSeparatorItem2.CanDrag = false;
		this.navBarSeparatorItem2.Enabled = false;
		this.navBarSeparatorItem2.Hint = null;
		this.navBarSeparatorItem2.Name = "navBarSeparatorItem2";
		this.NanPermission.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NanPermission.Appearance.Options.UseFont = true;
		this.NanPermission.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NanPermission.AppearanceDisabled.Options.UseFont = true;
		this.NanPermission.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NanPermission.AppearanceHotTracked.Options.UseFont = true;
		this.NanPermission.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NanPermission.AppearancePressed.Options.UseFont = true;
		this.NanPermission.Caption = "إدارة الصلاحيات";
		this.NanPermission.Name = "NanPermission";
		this.NanPermission.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NanPermission_LinkClicked);
		this.NavPassword.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPassword.Appearance.Options.UseFont = true;
		this.NavPassword.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPassword.AppearanceDisabled.Options.UseFont = true;
		this.NavPassword.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPassword.AppearanceHotTracked.Options.UseFont = true;
		this.NavPassword.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPassword.AppearancePressed.Options.UseFont = true;
		this.NavPassword.Caption = "تشفير كلمة المرور";
		this.NavPassword.Name = "NavPassword";
		this.NavPassword.Visible = false;
		this.NavPassword.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPassword_LinkClicked);
		this.navBarGroup9.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup9.Appearance.Options.UseFont = true;
		this.navBarGroup9.Appearance.Options.UseTextOptions = true;
		this.navBarGroup9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup9.Caption = "إدارة التقارير";
		this.navBarGroup9.Expanded = true;
		this.navBarGroup9.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[1]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavReport)
		});
		this.navBarGroup9.Name = "navBarGroup9";
		this.NavReport.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReport.Appearance.Options.UseFont = true;
		this.NavReport.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReport.AppearanceDisabled.Options.UseFont = true;
		this.NavReport.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReport.AppearanceHotTracked.Options.UseFont = true;
		this.NavReport.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavReport.AppearancePressed.Options.UseFont = true;
		this.NavReport.Caption = "التقارير";
		this.NavReport.Name = "NavReport";
		this.NavReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavReport_LinkClicked);
		this.navBarGroup10.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup10.Appearance.Options.UseFont = true;
		this.navBarGroup10.Appearance.Options.UseTextOptions = true;
		this.navBarGroup10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup10.Caption = "إدارة الاعدادات العامة";
		this.navBarGroup10.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[7]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavSetting),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavPrinter),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavClear),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavClearAll),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavDesign),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavAccountDefualt),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavCustomerDefualt)
		});
		this.navBarGroup10.Name = "navBarGroup10";
		this.NavSetting.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSetting.Appearance.Options.UseFont = true;
		this.NavSetting.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSetting.AppearanceDisabled.Options.UseFont = true;
		this.NavSetting.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSetting.AppearanceHotTracked.Options.UseFont = true;
		this.NavSetting.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavSetting.AppearancePressed.Options.UseFont = true;
		this.NavSetting.Caption = "اعدادات البرنامج ";
		this.NavSetting.Name = "NavSetting";
		this.NavSetting.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavSetting_LinkClicked);
		this.NavPrinter.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPrinter.Appearance.Options.UseFont = true;
		this.NavPrinter.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPrinter.AppearanceDisabled.Options.UseFont = true;
		this.NavPrinter.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPrinter.AppearanceHotTracked.Options.UseFont = true;
		this.NavPrinter.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavPrinter.AppearancePressed.Options.UseFont = true;
		this.NavPrinter.Caption = "الطابعة الافتراضية";
		this.NavPrinter.Name = "NavPrinter";
		this.NavPrinter.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavPrinter_LinkClicked);
		this.NavClear.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClear.Appearance.Options.UseFont = true;
		this.NavClear.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClear.AppearanceDisabled.Options.UseFont = true;
		this.NavClear.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClear.AppearanceHotTracked.Options.UseFont = true;
		this.NavClear.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClear.AppearancePressed.Options.UseFont = true;
		this.NavClear.Caption = "تصفير الفواتير والسندات";
		this.NavClear.Name = "NavClear";
		this.NavClear.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavClear_LinkClicked);
		this.NavClearAll.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClearAll.Appearance.Options.UseFont = true;
		this.NavClearAll.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClearAll.AppearanceDisabled.Options.UseFont = true;
		this.NavClearAll.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClearAll.AppearanceHotTracked.Options.UseFont = true;
		this.NavClearAll.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavClearAll.AppearancePressed.Options.UseFont = true;
		this.NavClearAll.Caption = "تصفير الفواتير والسندات والاصناف";
		this.NavClearAll.Name = "NavClearAll";
		this.NavClearAll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavClearAll_LinkClicked);
		this.NavDesign.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDesign.Appearance.Options.UseFont = true;
		this.NavDesign.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDesign.AppearanceDisabled.Options.UseFont = true;
		this.NavDesign.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDesign.AppearanceHotTracked.Options.UseFont = true;
		this.NavDesign.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavDesign.AppearancePressed.Options.UseFont = true;
		this.NavDesign.Caption = "مصمم التقارير";
		this.NavDesign.Name = "NavDesign";
		this.NavDesign.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavDesign_LinkClicked);
		this.NavAccountDefualt.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAccountDefualt.Appearance.Options.UseFont = true;
		this.NavAccountDefualt.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAccountDefualt.AppearanceDisabled.Options.UseFont = true;
		this.NavAccountDefualt.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAccountDefualt.AppearanceHotTracked.Options.UseFont = true;
		this.NavAccountDefualt.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAccountDefualt.AppearancePressed.Options.UseFont = true;
		this.NavAccountDefualt.Caption = "حسابات البرنامج";
		this.NavAccountDefualt.Name = "NavAccountDefualt";
		this.NavAccountDefualt.Visible = false;
		this.NavAccountDefualt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavAccountDefualt_LinkClicked);
		this.NavCustomerDefualt.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomerDefualt.Appearance.Options.UseFont = true;
		this.NavCustomerDefualt.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomerDefualt.AppearanceDisabled.Options.UseFont = true;
		this.NavCustomerDefualt.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomerDefualt.AppearanceHotTracked.Options.UseFont = true;
		this.NavCustomerDefualt.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavCustomerDefualt.AppearancePressed.Options.UseFont = true;
		this.NavCustomerDefualt.Caption = "حسابات عملاء المطعم";
		this.NavCustomerDefualt.Name = "NavCustomerDefualt";
		this.NavCustomerDefualt.Visible = false;
		this.NavCustomerDefualt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavCustomerDefualt_LinkClicked);
		this.navBarGroup11.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
		this.navBarGroup11.Appearance.Options.UseFont = true;
		this.navBarGroup11.Appearance.Options.UseTextOptions = true;
		this.navBarGroup11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.navBarGroup11.Caption = "حول البرنامج";
		this.navBarGroup11.Expanded = true;
		this.navBarGroup11.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[2]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavAbout),
			new DevExpress.XtraNavBar.NavBarItemLink(this.NavExit)
		});
		this.navBarGroup11.Name = "navBarGroup11";
		this.NavAbout.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAbout.Appearance.Options.UseFont = true;
		this.NavAbout.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAbout.AppearanceDisabled.Options.UseFont = true;
		this.NavAbout.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAbout.AppearanceHotTracked.Options.UseFont = true;
		this.NavAbout.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavAbout.AppearancePressed.Options.UseFont = true;
		this.NavAbout.Caption = "حول البرنامج";
		this.NavAbout.Name = "NavAbout";
		this.NavAbout.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavAbout_LinkClicked);
		this.NavExit.Appearance.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavExit.Appearance.Options.UseFont = true;
		this.NavExit.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavExit.AppearanceDisabled.Options.UseFont = true;
		this.NavExit.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavExit.AppearanceHotTracked.Options.UseFont = true;
		this.NavExit.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11f);
		this.NavExit.AppearancePressed.Options.UseFont = true;
		this.NavExit.Caption = "خروج من البرنامج";
		this.NavExit.Name = "NavExit";
		this.NavExit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(NavExit_LinkClicked);
		this.ribbonPageGroup.AllowTextClipping = false;
		this.ribbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem);
		this.ribbonPageGroup.Name = "ribbonPageGroup";
		this.ribbonPageGroup.Text = "Appearance";
		this.barSubItem1.Caption = "Navigation";
		this.barSubItem1.Id = 15;
		this.barSubItem1.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem1.Name = "barSubItem1";
		this.barSubItem2.Caption = "Navigation";
		this.barSubItem2.Id = 15;
		this.barSubItem2.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem2.Name = "barSubItem2";
		this.barSubItem3.Caption = "Navigation";
		this.barSubItem3.Id = 15;
		this.barSubItem3.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem3.Name = "barSubItem3";
		this.timer1.Enabled = true;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.BtnDataBase.Caption = "اعداد قواعد البيانات";
		this.BtnDataBase.Id = 148;
		this.BtnDataBase.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDataBase.ImageOptions.Image");
		this.BtnDataBase.Name = "BtnDataBase";
		this.BtnDataBase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1172, 753);
		base.Controls.Add(this.navigationFrame);
		base.Controls.Add(this.ribbonStatusBar);
		base.Controls.Add(this.ribbonControl);
		base.IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FrmWindow.IconOptions.Icon");
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.Name = "FrmWindow";
		this.Ribbon = this.ribbonControl;
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.StatusBar = this.ribbonStatusBar;
		this.Text = "برنامج مايكرو للمحاسبة ونقاط البيع";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmMain_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.tabbedView).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ribbonControl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.applicationMenu1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit3.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit4.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorPickEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTimeSpanEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTimeEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.navigationFrame).EndInit();
		this.navigationFrame.ResumeLayout(false);
		this.employeesNavigationPage.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.navBarControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.navBarControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


