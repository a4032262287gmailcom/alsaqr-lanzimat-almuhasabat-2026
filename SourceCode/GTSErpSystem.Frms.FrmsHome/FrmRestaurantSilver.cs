using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Behaviors;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Reports;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Contract;
using GTSErpSystem.Frms.Contract.Report;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.ElctorncInvoic;
using GTSErpSystem.Frms.FrmRestaurant;
using GTSErpSystem.Frms.FrmRestaurant.LuckUp;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Orders;
using GTSErpSystem.Frms.Payroll;
using GTSErpSystem.Frms.Reports.Account;
using GTSErpSystem.Frms.Reports.AllBranch;
using GTSErpSystem.Frms.Reports.Orders;
using GTSErpSystem.Frms.Reports.Profit;
using GTSErpSystem.Frms.Reports.Public;
using GTSErpSystem.Frms.Reports.Purches;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Frms.Security;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.FrmsHome;

public class FrmRestaurantSilver : RibbonForm
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

	private System.Windows.Forms.Timer timer1;

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

	private BarButtonItem btnDefualtCustomer;

	private BarButtonItem BtnItemCountry;

	private BarButtonItem BtnItemGuarantee;

	private BarButtonItem BtnORDInventorySettlementMinus;

	private BarButtonItem BtnRoom;

	private BarButtonItem BtnTable;

	private BarButtonItem BtnDelivery;

	private RibbonPageGroup ribbonPageGroup23;

	private BarSubItem barSubItem7;

	private BarButtonItem BtnMovementsDailyAllBranReport;

	private BarButtonItem BtnRPProfitAccountAllBran;

	private BarButtonItem BtnReportMizanAllBran;

	private RibbonPageGroup ribbonPageGroup24;

	private BarButtonItem BtnContract;

	private BarButtonItem BtnGuarantee;

	private BarButtonItem barButtonItem5;

	private BarSubItem barSubItem8;

	private BarButtonItem BtnReportContract;

	private BarButtonItem BtnReportGuarantee;

	private BarButtonItem BtnReportSearchCustomer;

	private RibbonPage ribbonPage7;

	private RibbonPageGroup ribbonPageGroup25;

	private RibbonPageGroup ribbonPageGroup26;

	private BarButtonItem BtnReportSearchContract;

	private BarButtonItem BtnItemVat;

	private BarButtonItem BtnTrnsferToBranch;

	private BarButtonItem BtnTrnsferFromBranch;

	private RibbonPage ribbonPage8;

	private RibbonPageGroup ribbonPageGroup27;

	private RibbonPageGroup ribbonPageGroup28;

	private BarButtonItem BtnRPMovementItemActive;

	private BarButtonItem BtnRptStoreItemLimit;

	private BarButtonItem BtnAllPrinter;

	private BarButtonItem BtnOrderDaily;

	private BarButtonItem BtnOrderDaily2;

	private BarButtonItem BtnAllowances;

	private BarButtonItem BtnGard2;

	private BarButtonItem BtnOrderDetailsSmall;

	public NavigationPage employeesNavigationPage;

	private BarButtonItem BtnAdd;

	private BarButtonItem BtnBounce2;

	private RibbonPage RElctornc;

	private RibbonPageGroup ribbonPageGroup18;

	private BarButtonItem BtnGenetareCSID;

	private BarButtonItem RElctornc2112;

	private BarButtonItem barButtonItem3;

	private BarButtonItem barButtonItem4;

	private BarButtonItem barButtonItem9;

	private BarButtonItem barButtonItem16;

	private BarButtonItem barButtonItem17;

	public FrmRestaurantSilver()
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
		UserLookAndFeel.Default.SkinName = "Office 2013";
		UserLookAndFeel.Default.SkinMaskColor = Color.DodgerBlue;
		LblCompany.Caption = "برنامج GTS للمحاسبة ونقاط البيع مرخص الي : " + LoginDetails.CompanyNameAR;
		LblServer.Caption = "السيرفر: " + LoginDetails.nameServer + " / " + LoginDetails.databaseServer;
		LblUser.Caption = "الموظف: " + LoginDetails.UserName;
		LblVerg.Caption = LoginDetails.VergDate;
		SetDateApp();
		ribbonControl.SelectedPage = ribbonPage1;
		if (!LoginDetails.IsElectronicInvoice)
		{
			RElctornc.Visible = false;
		}
		else
		{
			RElctornc.Visible = true;
		}
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
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

	private void BtnORDInventorySettlementMinus_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 83))
		{
			new FrmInventorySettlementMinus().Show();
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
		switch (MessageBox.Show("هل تريد الخروج من برنامج مايكرو سمارت للمحاسبة ونقاط البيع", "خروج من البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
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
			Thread thread = new Thread((ThreadStart)delegate
			{
				BeginInvoke((Action)delegate
				{
					STTG.RebuildCGEAllIndexes();
				});
			});
			thread.Start();
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
					Application.Exit();
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
			new GTSErpSystem.Frms.Account.FrmDefualtAccount().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void btnDefualtCustomer_ItemClick(object sender, ItemClickEventArgs e)
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
			new FrmMovementsDailyReport().Show();
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
		BtnDateApp.EditValue = DateTime.Now.ToShortDateString().ToString(new CultureInfo("en-US"));
		LoginDetails.DateApp = Convert.ToDateTime(BtnDateApp.EditValue);
	}

	private void SetDateApp()
	{
		BtnDateApp.EditWidth = 150;
		BtnDateApp.EditValue = LoginDetails.DateApp.ToShortDateString().ToString(new CultureInfo("en-US"));
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 157))
		{
			new FrmCashirRestaurant().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemCountry_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 80))
		{
			new FrmCountry().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemGuarantee_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 81))
		{
			new GTSErpSystem.Frms.Items.FrmGuarantee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRoom_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 84))
		{
			new FrmRoom().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTable_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 85))
		{
			new FrmTable().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnDelivery_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 86))
		{
			new FrmDelivery().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnMovementsDailyAllBranReport_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 87))
		{
			new FrmMovementsDailyReportAllBran().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPProfitAccountAllBran_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 88))
		{
			new FrmRPProfitAndLossAccountAllBran().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportMizanAllBran_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 89))
		{
			new FrmRPMizaniyahAllBran().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnContract_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 89))
		{
			new FrmContract().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnGuarantee_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 89))
		{
			new GTSErpSystem.Frms.Contract.FrmGuarantee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportContract_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 90))
		{
			new FrmAllContract().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportGuarantee_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 91))
		{
			new FrmAllGuarantee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportSearchCustomer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 92))
		{
			new FrmAllContractByCostomer().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportSearchContract_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 93))
		{
			new FrmAllContractActiveDisActive().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemVat_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmItemVat().ShowDialog();
	}

	private void BtnTrnsferToBranch_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 94))
		{
			new FrmTransferToBranch().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTrnsferFromBranch_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 95))
		{
			new FrmAllContractActiveDisActive().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPMovementItemActive_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			new FrmRPMovementItemActive().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRptStoreItemLimit_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 74))
		{
			new FrmRPItemLimit().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAllPrinter_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 104))
		{
			new GTSErpSystem.Frms.FrmsMain.FrmImportExcel().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderDaily2_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 116))
		{
			new FrmRPOrderDaily().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAllowances_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 120))
		{
			new FrmAllowances().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnGard2_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 42))
		{
			new FrmRPStoresGard().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderDetailsSmall_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 143))
		{
			new FrmOrderDetailsByDate().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAdd_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 156))
		{
			new FrmAdd().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnBounce2_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 97))
		{
			new GTSErpSystem.Frms.Contract.FrmBounce().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnGenetareCSID_ItemClick(object sender, ItemClickEventArgs e)
	{
		FrmCheckActivationElctroncInv frmCheckActivationElctroncInv = new FrmCheckActivationElctroncInv();
		frmCheckActivationElctroncInv.ShowDialog();
		if (frmCheckActivationElctroncInv.Active)
		{
			new FrmGenetareCSID().Show();
		}
		else
		{
			MessageBox.Show(" فشل العملية، \r\n يرجي المحاولة مرة أخري او مراجعة الدعم الفني الخاص بالبرنامج", "المستكشف", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void barButtonItem3_ItemClick_1(object sender, ItemClickEventArgs e)
	{
		new SentInvoiceToZakat().Show();
	}

	private void barButtonItem3_ItemClick_2(object sender, ItemClickEventArgs e)
	{
		try
		{
			using (SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString))
			{
				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand("Processing_OF_Profits", sqlConnection))
				{
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.ExecuteNonQuery();
				}
				sqlConnection.Close();
			}
			MessageBox.Show("تم تنفيذ عملية معالجه الأرباح بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show("حدث خطأ أثناء تنفيذ العملية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmLoginCrm().ShowDialog();
	}

	private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmCheckActivationCloseYear().ShowDialog();
	}

	private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmLoginCrm().ShowDialog();
	}

	private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
	{
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			sqlConnection.Open();
			using SqlCommand sqlCommand = new SqlCommand("Fix_Item", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			List<string> list = new List<string>();
			while (sqlDataReader.Read())
			{
				string item = sqlDataReader["item_Name"].ToString();
				list.Add(item);
			}
			if (list.Count > 0)
			{
				string text = "تم معالجة الأصناف التالية بنجاح:\n\n";
				foreach (string item2 in list)
				{
					text = text + "- " + item2 + "\n";
				}
				MessageBox.Show(text, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("لا يوجد أصناف تحتاج معالجة", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("حدث خطأ أثناء تنفيذ العملية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsHome.FrmRestaurantSilver));
		DevExpress.Skins.SkinPaddingEdges skinPaddingEdges = new DevExpress.Skins.SkinPaddingEdges();
		DevExpress.Skins.SkinPaddingEdges skinPaddingEdges2 = new DevExpress.Skins.SkinPaddingEdges();
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
		this.BtnOrderDaily2 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportVAT = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportTobaccoTax = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportStore = new DevExpress.XtraBars.BarSubItem();
		this.BtnGard2 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPGard = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMovementItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMovementItemActive = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByGroup = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRptStoreItemLimit = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPrinters = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAbout = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportOrder = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportAllOrders = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturn = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturnDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportSalesEmployee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderDetailsSmall = new DevExpress.XtraBars.BarButtonItem();
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
		this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem16 = new DevExpress.XtraBars.BarButtonItem();
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
		this.btnDefualtCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemCountry = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemGuarantee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnORDInventorySettlementMinus = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRoom = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTable = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDelivery = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
		this.BtnMovementsDailyAllBranReport = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPProfitAccountAllBran = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportMizanAllBran = new DevExpress.XtraBars.BarButtonItem();
		this.BtnContract = new DevExpress.XtraBars.BarButtonItem();
		this.BtnGuarantee = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem8 = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportContract = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportGuarantee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportSearchCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportSearchContract = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemVat = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferToBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferFromBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAllPrinter = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderDaily = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAllowances = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAdd = new DevExpress.XtraBars.BarButtonItem();
		this.BtnBounce2 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnGenetareCSID = new DevExpress.XtraBars.BarButtonItem();
		this.RElctornc2112 = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
		this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroupNavigation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup23 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup22 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup20 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup21 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage8 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup27 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup28 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage7 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup25 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup26 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup11 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
		this.ribbonPageGroup24 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.RElctornc = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup18 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
		this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.BtnDataBase = new DevExpress.XtraBars.BarSubItem();
		this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
		this.barButtonItem17 = new DevExpress.XtraBars.BarButtonItem();
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
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.ribbonControl, "ribbonControl");
		this.ribbonControl.ApplicationButtonDropDownControl = this.applicationMenu1;
		this.ribbonControl.ApplicationButtonImageOptions.Image = GTSErpSystem.Properties.Resources.rsz_infographic;
		this.ribbonControl.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(35, 37, 35, 37);
		this.ribbonControl.ExpandCollapseItem.Id = 0;
		this.ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex = (int)resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.ImageIndex");
		this.ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.LargeImageIndex");
		this.ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbonControl.ExpandCollapseItem.ImageOptions.SvgImage");
		this.ribbonControl.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl.ExpandCollapseItem.SearchTags");
		this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[166]
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
			this.btnDefualtCustomer,
			this.BtnItemCountry,
			this.BtnItemGuarantee,
			this.BtnORDInventorySettlementMinus,
			this.BtnRoom,
			this.BtnTable,
			this.BtnDelivery,
			this.barSubItem7,
			this.BtnMovementsDailyAllBranReport,
			this.BtnRPProfitAccountAllBran,
			this.BtnReportMizanAllBran,
			this.BtnContract,
			this.BtnGuarantee,
			this.barButtonItem5,
			this.barSubItem8,
			this.BtnReportContract,
			this.BtnReportGuarantee,
			this.BtnReportSearchCustomer,
			this.BtnReportSearchContract,
			this.BtnItemVat,
			this.BtnTrnsferToBranch,
			this.BtnTrnsferFromBranch,
			this.BtnRPMovementItemActive,
			this.BtnRptStoreItemLimit,
			this.BtnAllPrinter,
			this.BtnOrderDaily,
			this.BtnOrderDaily2,
			this.BtnAllowances,
			this.BtnGard2,
			this.BtnOrderDetailsSmall,
			this.BtnAdd,
			this.BtnBounce2,
			this.BtnGenetareCSID,
			this.RElctornc2112,
			this.barButtonItem3,
			this.barButtonItem4,
			this.barButtonItem9,
			this.barButtonItem16,
			this.barButtonItem17
		});
		this.ribbonControl.MaxItemId = 216;
		this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
		this.ribbonControl.Name = "ribbonControl";
		this.ribbonControl.OptionsMenuMinWidth = 385;
		this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[10] { this.ribbonPage, this.ribbonPage1, this.ribbonPage8, this.ribbonPage3, this.ribbonPage7, this.ribbonPage2, this.ribbonPage4, this.ribbonPage5, this.RElctornc, this.ribbonPage6 });
		this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemColorEdit1, this.repositoryItemColorPickEdit1, this.repositoryItemColorEdit2, this.repositoryItemDateEdit1, this.repositoryItemDateEdit2, this.repositoryItemDateEdit3, this.repositoryItemTimeSpanEdit1, this.repositoryItemTimeEdit1, this.repositoryItemDateEdit4 });
		this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
		this.ribbonControl.StatusBar = this.ribbonStatusBar;
		this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
		this.applicationMenu1.Name = "applicationMenu1";
		this.applicationMenu1.Ribbon = this.ribbonControl;
		this.applicationMenu1.ShowRightPane = true;
		resources.ApplyResources(this.skinRibbonGalleryBarItem, "skinRibbonGalleryBarItem");
		this.skinRibbonGalleryBarItem.Gallery.AllowHoverImages = true;
		this.skinRibbonGalleryBarItem.Gallery.ColumnCount = 4;
		this.skinRibbonGalleryBarItem.Gallery.FixedHoverImageSize = false;
		this.skinRibbonGalleryBarItem.Gallery.ImageSize = new System.Drawing.Size(16, 16);
		this.skinRibbonGalleryBarItem.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
		this.skinRibbonGalleryBarItem.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Squeeze;
		this.skinRibbonGalleryBarItem.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
		skinPaddingEdges.Left = 8;
		skinPaddingEdges.Right = 8;
		this.skinRibbonGalleryBarItem.Gallery.ItemImagePadding = skinPaddingEdges;
		this.skinRibbonGalleryBarItem.Id = 14;
		this.skinRibbonGalleryBarItem.ImageOptions.ImageIndex = (int)resources.GetObject("skinRibbonGalleryBarItem.ImageOptions.ImageIndex");
		this.skinRibbonGalleryBarItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("skinRibbonGalleryBarItem.ImageOptions.LargeImageIndex");
		this.skinRibbonGalleryBarItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("skinRibbonGalleryBarItem.ImageOptions.SvgImage");
		this.skinRibbonGalleryBarItem.Name = "skinRibbonGalleryBarItem";
		resources.ApplyResources(this.employeesBarButtonItem, "employeesBarButtonItem");
		this.employeesBarButtonItem.Id = 80;
		this.employeesBarButtonItem.ImageOptions.ImageIndex = (int)resources.GetObject("employeesBarButtonItem.ImageOptions.ImageIndex");
		this.employeesBarButtonItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("employeesBarButtonItem.ImageOptions.LargeImageIndex");
		this.employeesBarButtonItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("employeesBarButtonItem.ImageOptions.SvgImage");
		this.employeesBarButtonItem.Name = "employeesBarButtonItem";
		resources.ApplyResources(this.customersBarButtonItem, "customersBarButtonItem");
		this.customersBarButtonItem.Id = 81;
		this.customersBarButtonItem.ImageOptions.ImageIndex = (int)resources.GetObject("customersBarButtonItem.ImageOptions.ImageIndex");
		this.customersBarButtonItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("customersBarButtonItem.ImageOptions.LargeImageIndex");
		this.customersBarButtonItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("customersBarButtonItem.ImageOptions.SvgImage");
		this.customersBarButtonItem.Name = "customersBarButtonItem";
		resources.ApplyResources(this.skinRibbonGalleryBarItem1, "skinRibbonGalleryBarItem1");
		this.skinRibbonGalleryBarItem1.Gallery.AllowHoverImages = true;
		this.skinRibbonGalleryBarItem1.Gallery.ColumnCount = 4;
		this.skinRibbonGalleryBarItem1.Gallery.FixedHoverImageSize = false;
		this.skinRibbonGalleryBarItem1.Gallery.ImageSize = new System.Drawing.Size(16, 16);
		this.skinRibbonGalleryBarItem1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Squeeze;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
		skinPaddingEdges2.Left = 8;
		skinPaddingEdges2.Right = 8;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImagePadding = skinPaddingEdges2;
		this.skinRibbonGalleryBarItem1.Id = 46;
		this.skinRibbonGalleryBarItem1.ImageOptions.ImageIndex = (int)resources.GetObject("skinRibbonGalleryBarItem1.ImageOptions.ImageIndex");
		this.skinRibbonGalleryBarItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("skinRibbonGalleryBarItem1.ImageOptions.LargeImageIndex");
		this.skinRibbonGalleryBarItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("skinRibbonGalleryBarItem1.ImageOptions.SvgImage");
		this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
		resources.ApplyResources(this.barSubItem4, "barSubItem4");
		this.barSubItem4.Id = 48;
		this.barSubItem4.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem4.ImageOptions.ImageIndex");
		this.barSubItem4.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem4.ImageOptions.LargeImageIndex");
		this.barSubItem4.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem4.ImageOptions.SvgImage");
		this.barSubItem4.Name = "barSubItem4";
		this.barSubItem4.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnORDOpenQuantity, "BtnORDOpenQuantity");
		this.BtnORDOpenQuantity.Id = 51;
		this.BtnORDOpenQuantity.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOpenQuantity.ImageOptions.Image");
		this.BtnORDOpenQuantity.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDOpenQuantity.ImageOptions.ImageIndex");
		this.BtnORDOpenQuantity.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDOpenQuantity.ImageOptions.LargeImageIndex");
		this.BtnORDOpenQuantity.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDOpenQuantity.ImageOptions.SvgImage");
		this.BtnORDOpenQuantity.Name = "BtnORDOpenQuantity";
		this.BtnORDOpenQuantity.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOpenQuantity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOpenQuantity_ItemClick);
		resources.ApplyResources(this.BtnORDPriceOffer, "BtnORDPriceOffer");
		this.BtnORDPriceOffer.Id = 52;
		this.BtnORDPriceOffer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDPriceOffer.ImageOptions.Image");
		this.BtnORDPriceOffer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDPriceOffer.ImageOptions.ImageIndex");
		this.BtnORDPriceOffer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDPriceOffer.ImageOptions.LargeImageIndex");
		this.BtnORDPriceOffer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDPriceOffer.ImageOptions.SvgImage");
		this.BtnORDPriceOffer.Name = "BtnORDPriceOffer";
		this.BtnORDPriceOffer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPriceOffer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPriceOffer_ItemClick);
		resources.ApplyResources(this.BtnORDOrder, "BtnORDOrder");
		this.BtnORDOrder.Id = 53;
		this.BtnORDOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrder.ImageOptions.Image");
		this.BtnORDOrder.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDOrder.ImageOptions.ImageIndex");
		this.BtnORDOrder.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDOrder.ImageOptions.LargeImageIndex");
		this.BtnORDOrder.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDOrder.ImageOptions.SvgImage");
		this.BtnORDOrder.Name = "BtnORDOrder";
		this.BtnORDOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrder_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseOrder, "BtnORDPurchaseOrder");
		this.BtnORDPurchaseOrder.Id = 54;
		this.BtnORDPurchaseOrder.ImageOptions.Image = GTSErpSystem.Properties.Resources.insert_32x32;
		this.BtnORDPurchaseOrder.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDPurchaseOrder.ImageOptions.ImageIndex");
		this.BtnORDPurchaseOrder.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDPurchaseOrder.ImageOptions.LargeImageIndex");
		this.BtnORDPurchaseOrder.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDPurchaseOrder.ImageOptions.SvgImage");
		this.BtnORDPurchaseOrder.Name = "BtnORDPurchaseOrder";
		this.BtnORDPurchaseOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseOrder_ItemClick);
		resources.ApplyResources(this.BtnORDPurchase, "BtnORDPurchase");
		this.BtnORDPurchase.Id = 55;
		this.BtnORDPurchase.ImageOptions.Image = GTSErpSystem.Properties.Resources.buy_32x32;
		this.BtnORDPurchase.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDPurchase.ImageOptions.ImageIndex");
		this.BtnORDPurchase.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDPurchase.ImageOptions.LargeImageIndex");
		this.BtnORDPurchase.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDPurchase.ImageOptions.SvgImage");
		this.BtnORDPurchase.Name = "BtnORDPurchase";
		this.BtnORDPurchase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchase_ItemClick);
		resources.ApplyResources(this.BtnORDOrderReturnID, "BtnORDOrderReturnID");
		this.BtnORDOrderReturnID.Id = 56;
		this.BtnORDOrderReturnID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnID.ImageOptions.Image");
		this.BtnORDOrderReturnID.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDOrderReturnID.ImageOptions.ImageIndex");
		this.BtnORDOrderReturnID.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDOrderReturnID.ImageOptions.LargeImageIndex");
		this.BtnORDOrderReturnID.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDOrderReturnID.ImageOptions.SvgImage");
		this.BtnORDOrderReturnID.Name = "BtnORDOrderReturnID";
		this.BtnORDOrderReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnID_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseReturnID, "BtnORDPurchaseReturnID");
		this.BtnORDPurchaseReturnID.Id = 57;
		this.BtnORDPurchaseReturnID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deletegroupfooter_32x32;
		this.BtnORDPurchaseReturnID.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDPurchaseReturnID.ImageOptions.ImageIndex");
		this.BtnORDPurchaseReturnID.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDPurchaseReturnID.ImageOptions.LargeImageIndex");
		this.BtnORDPurchaseReturnID.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDPurchaseReturnID.ImageOptions.SvgImage");
		this.BtnORDPurchaseReturnID.Name = "BtnORDPurchaseReturnID";
		this.BtnORDPurchaseReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnID_ItemClick);
		resources.ApplyResources(this.BtnORDOrderReturnNotID, "BtnORDOrderReturnNotID");
		this.BtnORDOrderReturnNotID.Id = 58;
		this.BtnORDOrderReturnNotID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.Image");
		this.BtnORDOrderReturnNotID.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.ImageIndex");
		this.BtnORDOrderReturnNotID.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.LargeImageIndex");
		this.BtnORDOrderReturnNotID.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.SvgImage");
		this.BtnORDOrderReturnNotID.Name = "BtnORDOrderReturnNotID";
		this.BtnORDOrderReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnNotID_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseReturnNotID, "BtnORDPurchaseReturnNotID");
		this.BtnORDPurchaseReturnNotID.Id = 59;
		this.BtnORDPurchaseReturnNotID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deleteheader_32x32;
		this.BtnORDPurchaseReturnNotID.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDPurchaseReturnNotID.ImageOptions.ImageIndex");
		this.BtnORDPurchaseReturnNotID.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDPurchaseReturnNotID.ImageOptions.LargeImageIndex");
		this.BtnORDPurchaseReturnNotID.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDPurchaseReturnNotID.ImageOptions.SvgImage");
		this.BtnORDPurchaseReturnNotID.Name = "BtnORDPurchaseReturnNotID";
		this.BtnORDPurchaseReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnNotID_ItemClick);
		resources.ApplyResources(this.barButtonItem10, "barButtonItem10");
		this.barButtonItem10.Id = 60;
		this.barButtonItem10.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem10.ImageOptions.ImageIndex");
		this.barButtonItem10.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem10.ImageOptions.LargeImageIndex");
		this.barButtonItem10.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem10.ImageOptions.SvgImage");
		this.barButtonItem10.Name = "barButtonItem10";
		resources.ApplyResources(this.barButtonItem11, "barButtonItem11");
		this.barButtonItem11.Id = 61;
		this.barButtonItem11.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem11.ImageOptions.ImageIndex");
		this.barButtonItem11.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem11.ImageOptions.LargeImageIndex");
		this.barButtonItem11.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem11.ImageOptions.SvgImage");
		this.barButtonItem11.Name = "barButtonItem11";
		this.barButtonItem11.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem12, "barButtonItem12");
		this.barButtonItem12.Id = 62;
		this.barButtonItem12.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem12.ImageOptions.ImageIndex");
		this.barButtonItem12.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem12.ImageOptions.LargeImageIndex");
		this.barButtonItem12.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem12.ImageOptions.SvgImage");
		this.barButtonItem12.Name = "barButtonItem12";
		this.barButtonItem12.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem13, "barButtonItem13");
		this.barButtonItem13.Id = 63;
		this.barButtonItem13.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem13.ImageOptions.ImageIndex");
		this.barButtonItem13.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem13.ImageOptions.LargeImageIndex");
		this.barButtonItem13.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem13.ImageOptions.SvgImage");
		this.barButtonItem13.Name = "barButtonItem13";
		this.barButtonItem13.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem14, "barButtonItem14");
		this.barButtonItem14.Id = 64;
		this.barButtonItem14.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem14.ImageOptions.ImageIndex");
		this.barButtonItem14.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem14.ImageOptions.LargeImageIndex");
		this.barButtonItem14.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem14.ImageOptions.SvgImage");
		this.barButtonItem14.Name = "barButtonItem14";
		this.barButtonItem14.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem15, "barButtonItem15");
		this.barButtonItem15.Id = 65;
		this.barButtonItem15.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem15.ImageOptions.ImageIndex");
		this.barButtonItem15.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem15.ImageOptions.LargeImageIndex");
		this.barButtonItem15.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem15.ImageOptions.SvgImage");
		this.barButtonItem15.Name = "barButtonItem15";
		this.barButtonItem15.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnItemGroups, "BtnItemGroups");
		this.BtnItemGroups.Id = 66;
		this.BtnItemGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.group_32x32;
		this.BtnItemGroups.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemGroups.ImageOptions.ImageIndex");
		this.BtnItemGroups.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemGroups.ImageOptions.LargeImageIndex");
		this.BtnItemGroups.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemGroups.ImageOptions.SvgImage");
		this.BtnItemGroups.Name = "BtnItemGroups";
		this.BtnItemGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGroups_ItemClick);
		resources.ApplyResources(this.BtnItemUnits, "BtnItemUnits");
		this.BtnItemUnits.Id = 67;
		this.BtnItemUnits.ImageOptions.Image = GTSErpSystem.Properties.Resources.unmergecells_32x32;
		this.BtnItemUnits.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemUnits.ImageOptions.ImageIndex");
		this.BtnItemUnits.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemUnits.ImageOptions.LargeImageIndex");
		this.BtnItemUnits.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemUnits.ImageOptions.SvgImage");
		this.BtnItemUnits.Name = "BtnItemUnits";
		this.BtnItemUnits.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemUnits.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemUnits_ItemClick);
		resources.ApplyResources(this.BtnItemClass, "BtnItemClass");
		this.BtnItemClass.Id = 68;
		this.BtnItemClass.ImageOptions.Image = GTSErpSystem.Properties.Resources.expandcollapse_32x32;
		this.BtnItemClass.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemClass.ImageOptions.ImageIndex");
		this.BtnItemClass.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemClass.ImageOptions.LargeImageIndex");
		this.BtnItemClass.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemClass.ImageOptions.SvgImage");
		this.BtnItemClass.Name = "BtnItemClass";
		this.BtnItemClass.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemClass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemClass_ItemClick);
		resources.ApplyResources(this.BtnItemCompany, "BtnItemCompany");
		this.BtnItemCompany.Id = 69;
		this.BtnItemCompany.ImageOptions.Image = GTSErpSystem.Properties.Resources.home_32x32;
		this.BtnItemCompany.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemCompany.ImageOptions.ImageIndex");
		this.BtnItemCompany.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemCompany.ImageOptions.LargeImageIndex");
		this.BtnItemCompany.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemCompany.ImageOptions.SvgImage");
		this.BtnItemCompany.Name = "BtnItemCompany";
		this.BtnItemCompany.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCompany.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCompany_ItemClick);
		resources.ApplyResources(this.BtnItem, "BtnItem");
		this.BtnItem.Id = 70;
		this.BtnItem.ImageOptions.Image = GTSErpSystem.Properties.Resources.mailmerge_32x32;
		this.BtnItem.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItem.ImageOptions.ImageIndex");
		this.BtnItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItem.ImageOptions.LargeImageIndex");
		this.BtnItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItem.ImageOptions.SvgImage");
		this.BtnItem.Name = "BtnItem";
		this.BtnItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItem_ItemClick);
		resources.ApplyResources(this.BtnStores, "BtnStores");
		this.BtnStores.Id = 71;
		this.BtnStores.ImageOptions.Image = GTSErpSystem.Properties.Resources.autoexpand_32x32;
		this.BtnStores.ImageOptions.ImageIndex = (int)resources.GetObject("BtnStores.ImageOptions.ImageIndex");
		this.BtnStores.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnStores.ImageOptions.LargeImageIndex");
		this.BtnStores.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnStores.ImageOptions.SvgImage");
		this.BtnStores.Name = "BtnStores";
		this.BtnStores.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnStores.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnStores_ItemClick);
		resources.ApplyResources(this.BtnBranches, "BtnBranches");
		this.BtnBranches.Enabled = false;
		this.BtnBranches.Id = 72;
		this.BtnBranches.ImageOptions.Image = GTSErpSystem.Properties.Resources.documentmap_32x32;
		this.BtnBranches.ImageOptions.ImageIndex = (int)resources.GetObject("BtnBranches.ImageOptions.ImageIndex");
		this.BtnBranches.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnBranches.ImageOptions.LargeImageIndex");
		this.BtnBranches.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnBranches.ImageOptions.SvgImage");
		this.BtnBranches.Name = "BtnBranches";
		this.BtnBranches.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBranches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBranches_ItemClick);
		resources.ApplyResources(this.barButtonItem23, "barButtonItem23");
		this.barButtonItem23.Id = 73;
		this.barButtonItem23.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem23.ImageOptions.ImageIndex");
		this.barButtonItem23.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem23.ImageOptions.LargeImageIndex");
		this.barButtonItem23.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem23.ImageOptions.SvgImage");
		this.barButtonItem23.Name = "barButtonItem23";
		this.barButtonItem23.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem24, "barButtonItem24");
		this.barButtonItem24.Id = 74;
		this.barButtonItem24.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem24.ImageOptions.ImageIndex");
		this.barButtonItem24.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem24.ImageOptions.LargeImageIndex");
		this.barButtonItem24.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem24.ImageOptions.SvgImage");
		this.barButtonItem24.Name = "barButtonItem24";
		this.barButtonItem24.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnCusPlace, "BtnCusPlace");
		this.BtnCusPlace.Id = 75;
		this.BtnCusPlace.ImageOptions.Image = GTSErpSystem.Properties.Resources.language_32x32;
		this.BtnCusPlace.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCusPlace.ImageOptions.ImageIndex");
		this.BtnCusPlace.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCusPlace.ImageOptions.LargeImageIndex");
		this.BtnCusPlace.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCusPlace.ImageOptions.SvgImage");
		this.BtnCusPlace.Name = "BtnCusPlace";
		this.BtnCusPlace.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusPlace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusPlace_ItemClick);
		resources.ApplyResources(this.BtnCustomer, "BtnCustomer");
		this.BtnCustomer.Id = 76;
		this.BtnCustomer.ImageOptions.Image = GTSErpSystem.Properties.Resources.employee_32x32;
		this.BtnCustomer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCustomer.ImageOptions.ImageIndex");
		this.BtnCustomer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCustomer.ImageOptions.LargeImageIndex");
		this.BtnCustomer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCustomer.ImageOptions.SvgImage");
		this.BtnCustomer.Name = "BtnCustomer";
		this.BtnCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCustomer_ItemClick);
		resources.ApplyResources(this.BtnCusSupplier, "BtnCusSupplier");
		this.BtnCusSupplier.Id = 77;
		this.BtnCusSupplier.ImageOptions.Image = GTSErpSystem.Properties.Resources.bocustomer_32x32;
		this.BtnCusSupplier.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCusSupplier.ImageOptions.ImageIndex");
		this.BtnCusSupplier.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCusSupplier.ImageOptions.LargeImageIndex");
		this.BtnCusSupplier.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCusSupplier.ImageOptions.SvgImage");
		this.BtnCusSupplier.Name = "BtnCusSupplier";
		this.BtnCusSupplier.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSupplier.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSupplier_ItemClick);
		resources.ApplyResources(this.BtnCusSalesMan, "BtnCusSalesMan");
		this.BtnCusSalesMan.Enabled = false;
		this.BtnCusSalesMan.Id = 79;
		this.BtnCusSalesMan.ImageOptions.Image = GTSErpSystem.Properties.Resources.meeting_32x32;
		this.BtnCusSalesMan.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCusSalesMan.ImageOptions.ImageIndex");
		this.BtnCusSalesMan.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCusSalesMan.ImageOptions.LargeImageIndex");
		this.BtnCusSalesMan.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCusSalesMan.ImageOptions.SvgImage");
		this.BtnCusSalesMan.Name = "BtnCusSalesMan";
		this.BtnCusSalesMan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSalesMan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSalesMan_ItemClick);
		resources.ApplyResources(this.BtnUsers, "BtnUsers");
		this.BtnUsers.Id = 82;
		this.BtnUsers.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.Image");
		this.BtnUsers.ImageOptions.ImageIndex = (int)resources.GetObject("BtnUsers.ImageOptions.ImageIndex");
		this.BtnUsers.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.LargeImage");
		this.BtnUsers.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnUsers.ImageOptions.LargeImageIndex");
		this.BtnUsers.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnUsers.ImageOptions.SvgImage");
		this.BtnUsers.Name = "BtnUsers";
		this.BtnUsers.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUsers_ItemClick);
		resources.ApplyResources(this.BtnGroups, "BtnGroups");
		this.BtnGroups.Id = 83;
		this.BtnGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.Groups;
		this.BtnGroups.ImageOptions.ImageIndex = (int)resources.GetObject("BtnGroups.ImageOptions.ImageIndex");
		this.BtnGroups.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnGroups.ImageOptions.LargeImageIndex");
		this.BtnGroups.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGroups.ImageOptions.SvgImage");
		this.BtnGroups.Name = "BtnGroups";
		this.BtnGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGroups_ItemClick);
		resources.ApplyResources(this.BtnPermission, "BtnPermission");
		this.BtnPermission.Id = 84;
		this.BtnPermission.ImageOptions.Image = GTSErpSystem.Properties.Resources.Permission;
		this.BtnPermission.ImageOptions.ImageIndex = (int)resources.GetObject("BtnPermission.ImageOptions.ImageIndex");
		this.BtnPermission.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnPermission.ImageOptions.LargeImageIndex");
		this.BtnPermission.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPermission.ImageOptions.SvgImage");
		this.BtnPermission.Name = "BtnPermission";
		this.BtnPermission.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPermission.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPermission_ItemClick);
		resources.ApplyResources(this.BtnCashir, "BtnCashir");
		this.BtnCashir.Id = 85;
		this.BtnCashir.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCashir.ImageOptions.Image");
		this.BtnCashir.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCashir.ImageOptions.ImageIndex");
		this.BtnCashir.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCashir.ImageOptions.LargeImageIndex");
		this.BtnCashir.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCashir.ImageOptions.SvgImage");
		this.BtnCashir.Name = "BtnCashir";
		this.BtnCashir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCashir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnCashir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCashir_ItemClick);
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Id = 86;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRecipt.ImageOptions.ImageIndex");
		this.BtnRecipt.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRecipt.ImageOptions.LargeImageIndex");
		this.BtnRecipt.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRecipt.ImageOptions.SvgImage");
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRecipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRecipt_ItemClick);
		resources.ApplyResources(this.BtnPayment, "BtnPayment");
		this.BtnPayment.Id = 87;
		this.BtnPayment.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayment.ImageOptions.Image");
		this.BtnPayment.ImageOptions.ImageIndex = (int)resources.GetObject("BtnPayment.ImageOptions.ImageIndex");
		this.BtnPayment.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnPayment.ImageOptions.LargeImageIndex");
		this.BtnPayment.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPayment.ImageOptions.SvgImage");
		this.BtnPayment.Name = "BtnPayment";
		this.BtnPayment.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayment_ItemClick);
		resources.ApplyResources(this.BtnAccCardAccount, "BtnAccCardAccount");
		this.BtnAccCardAccount.Id = 88;
		this.BtnAccCardAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCardAccount.ImageOptions.Image");
		this.BtnAccCardAccount.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccCardAccount.ImageOptions.ImageIndex");
		this.BtnAccCardAccount.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccCardAccount.ImageOptions.LargeImageIndex");
		this.BtnAccCardAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccCardAccount.ImageOptions.SvgImage");
		this.BtnAccCardAccount.Name = "BtnAccCardAccount";
		this.BtnAccCardAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCardAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCardAccount_ItemClick);
		resources.ApplyResources(this.BtnAccTreeAccount, "BtnAccTreeAccount");
		this.BtnAccTreeAccount.Id = 89;
		this.BtnAccTreeAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccTreeAccount.ImageOptions.Image");
		this.BtnAccTreeAccount.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccTreeAccount.ImageOptions.ImageIndex");
		this.BtnAccTreeAccount.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccTreeAccount.ImageOptions.LargeImageIndex");
		this.BtnAccTreeAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccTreeAccount.ImageOptions.SvgImage");
		this.BtnAccTreeAccount.Name = "BtnAccTreeAccount";
		this.BtnAccTreeAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccTreeAccount.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccTreeAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccTreeAccount_ItemClick);
		resources.ApplyResources(this.BtnAccCostCenter, "BtnAccCostCenter");
		this.BtnAccCostCenter.Id = 90;
		this.BtnAccCostCenter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCostCenter.ImageOptions.Image");
		this.BtnAccCostCenter.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccCostCenter.ImageOptions.ImageIndex");
		this.BtnAccCostCenter.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccCostCenter.ImageOptions.LargeImageIndex");
		this.BtnAccCostCenter.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccCostCenter.ImageOptions.SvgImage");
		this.BtnAccCostCenter.Name = "BtnAccCostCenter";
		this.BtnAccCostCenter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCostCenter.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccCostCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCostCenter_ItemClick);
		resources.ApplyResources(this.BtnAccOpenMoney, "BtnAccOpenMoney");
		this.BtnAccOpenMoney.Id = 91;
		this.BtnAccOpenMoney.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccOpenMoney.ImageOptions.Image");
		this.BtnAccOpenMoney.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccOpenMoney.ImageOptions.ImageIndex");
		this.BtnAccOpenMoney.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccOpenMoney.ImageOptions.LargeImageIndex");
		this.BtnAccOpenMoney.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccOpenMoney.ImageOptions.SvgImage");
		this.BtnAccOpenMoney.Name = "BtnAccOpenMoney";
		this.BtnAccOpenMoney.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccOpenMoney.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccOpenMoney.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccOpenMoney_ItemClick);
		resources.ApplyResources(this.BtnAccRestrictionsTest, "BtnAccRestrictionsTest");
		this.BtnAccRestrictionsTest.Id = 92;
		this.BtnAccRestrictionsTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.Image");
		this.BtnAccRestrictionsTest.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.ImageIndex");
		this.BtnAccRestrictionsTest.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.LargeImageIndex");
		this.BtnAccRestrictionsTest.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.SvgImage");
		this.BtnAccRestrictionsTest.Name = "BtnAccRestrictionsTest";
		this.BtnAccRestrictionsTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictionsTest.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccRestrictionsTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictionsTest_ItemClick);
		resources.ApplyResources(this.BtnAccRestrictions, "BtnAccRestrictions");
		this.BtnAccRestrictions.Id = 93;
		this.BtnAccRestrictions.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictions.ImageOptions.Image");
		this.BtnAccRestrictions.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAccRestrictions.ImageOptions.ImageIndex");
		this.BtnAccRestrictions.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAccRestrictions.ImageOptions.LargeImageIndex");
		this.BtnAccRestrictions.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAccRestrictions.ImageOptions.SvgImage");
		this.BtnAccRestrictions.Name = "BtnAccRestrictions";
		this.BtnAccRestrictions.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictions.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnAccRestrictions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictions_ItemClick);
		resources.ApplyResources(this.barButtonItem7, "barButtonItem7");
		this.barButtonItem7.Id = 94;
		this.barButtonItem7.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
		this.barButtonItem7.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem7.ImageOptions.ImageIndex");
		this.barButtonItem7.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem7.ImageOptions.LargeImageIndex");
		this.barButtonItem7.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem7.ImageOptions.SvgImage");
		this.barButtonItem7.Name = "barButtonItem7";
		this.barButtonItem7.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem7_ItemClick);
		resources.ApplyResources(this.barButtonItem8, "barButtonItem8");
		this.barButtonItem8.Id = 95;
		this.barButtonItem8.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
		this.barButtonItem8.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem8.ImageOptions.ImageIndex");
		this.barButtonItem8.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem8.ImageOptions.LargeImageIndex");
		this.barButtonItem8.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem8.ImageOptions.SvgImage");
		this.barButtonItem8.Name = "barButtonItem8";
		this.barButtonItem8.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem8_ItemClick);
		resources.ApplyResources(this.BtnReportAccount, "BtnReportAccount");
		this.BtnReportAccount.Id = 96;
		this.BtnReportAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAccount.ImageOptions.Image");
		this.BtnReportAccount.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAccount.ImageOptions.ImageIndex");
		this.BtnReportAccount.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAccount.ImageOptions.LargeImageIndex");
		this.BtnReportAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAccount.ImageOptions.SvgImage");
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
		resources.ApplyResources(this.BtnReportMizan, "BtnReportMizan");
		this.BtnReportMizan.Id = 97;
		this.BtnReportMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.Image");
		this.BtnReportMizan.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportMizan.ImageOptions.ImageIndex");
		this.BtnReportMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.LargeImage");
		this.BtnReportMizan.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportMizan.ImageOptions.LargeImageIndex");
		this.BtnReportMizan.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportMizan.ImageOptions.SvgImage");
		this.BtnReportMizan.Name = "BtnReportMizan";
		this.BtnReportMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportMizan_ItemClick);
		resources.ApplyResources(this.BtnRPProfitAccount, "BtnRPProfitAccount");
		this.BtnRPProfitAccount.Id = 98;
		this.BtnRPProfitAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.Image");
		this.BtnRPProfitAccount.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPProfitAccount.ImageOptions.ImageIndex");
		this.BtnRPProfitAccount.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.LargeImage");
		this.BtnRPProfitAccount.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPProfitAccount.ImageOptions.LargeImageIndex");
		this.BtnRPProfitAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPProfitAccount.ImageOptions.SvgImage");
		this.BtnRPProfitAccount.Name = "BtnRPProfitAccount";
		this.BtnRPProfitAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitAccount_ItemClick);
		resources.ApplyResources(this.BtnRPMizan, "BtnRPMizan");
		this.BtnRPMizan.Id = 149;
		this.BtnRPMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.Image");
		this.BtnRPMizan.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPMizan.ImageOptions.ImageIndex");
		this.BtnRPMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.LargeImage");
		this.BtnRPMizan.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPMizan.ImageOptions.LargeImageIndex");
		this.BtnRPMizan.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPMizan.ImageOptions.SvgImage");
		this.BtnRPMizan.Name = "BtnRPMizan";
		this.BtnRPMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizan_ItemClick);
		resources.ApplyResources(this.BtnRPMizanTotal, "BtnRPMizanTotal");
		this.BtnRPMizanTotal.Id = 148;
		this.BtnRPMizanTotal.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.Image");
		this.BtnRPMizanTotal.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPMizanTotal.ImageOptions.ImageIndex");
		this.BtnRPMizanTotal.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.LargeImage");
		this.BtnRPMizanTotal.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPMizanTotal.ImageOptions.LargeImageIndex");
		this.BtnRPMizanTotal.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPMizanTotal.ImageOptions.SvgImage");
		this.BtnRPMizanTotal.Name = "BtnRPMizanTotal";
		this.BtnRPMizanTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanTotal_ItemClick);
		resources.ApplyResources(this.BtnRPMizanGroups, "BtnRPMizanGroups");
		this.BtnRPMizanGroups.Id = 150;
		this.BtnRPMizanGroups.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.Image");
		this.BtnRPMizanGroups.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPMizanGroups.ImageOptions.ImageIndex");
		this.BtnRPMizanGroups.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.LargeImage");
		this.BtnRPMizanGroups.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPMizanGroups.ImageOptions.LargeImageIndex");
		this.BtnRPMizanGroups.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPMizanGroups.ImageOptions.SvgImage");
		this.BtnRPMizanGroups.Name = "BtnRPMizanGroups";
		this.BtnRPMizanGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanGroups_ItemClick);
		resources.ApplyResources(this.BtnRPTranTeacher, "BtnRPTranTeacher");
		this.BtnRPTranTeacher.Id = 162;
		this.BtnRPTranTeacher.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.Image");
		this.BtnRPTranTeacher.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPTranTeacher.ImageOptions.ImageIndex");
		this.BtnRPTranTeacher.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.LargeImage");
		this.BtnRPTranTeacher.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPTranTeacher.ImageOptions.LargeImageIndex");
		this.BtnRPTranTeacher.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPTranTeacher.ImageOptions.SvgImage");
		this.BtnRPTranTeacher.Name = "BtnRPTranTeacher";
		this.BtnRPTranTeacher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTranTeacher_ItemClick);
		resources.ApplyResources(this.BtnReportLedger, "BtnReportLedger");
		this.BtnReportLedger.Id = 175;
		this.BtnReportLedger.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.Image");
		this.BtnReportLedger.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportLedger.ImageOptions.ImageIndex");
		this.BtnReportLedger.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.LargeImage");
		this.BtnReportLedger.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportLedger.ImageOptions.LargeImageIndex");
		this.BtnReportLedger.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportLedger.ImageOptions.SvgImage");
		this.BtnReportLedger.Name = "BtnReportLedger";
		this.BtnReportLedger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportLedger_ItemClick);
		resources.ApplyResources(this.BtnSetting, "BtnSetting");
		this.BtnSetting.Id = 99;
		this.BtnSetting.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSetting.ImageOptions.Image");
		this.BtnSetting.ImageOptions.ImageIndex = (int)resources.GetObject("BtnSetting.ImageOptions.ImageIndex");
		this.BtnSetting.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnSetting.ImageOptions.LargeImageIndex");
		this.BtnSetting.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnSetting.ImageOptions.SvgImage");
		this.BtnSetting.Name = "BtnSetting";
		this.BtnSetting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnSetting_ItemClick);
		resources.ApplyResources(this.BtnReportDesign, "BtnReportDesign");
		this.BtnReportDesign.Id = 100;
		this.BtnReportDesign.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportDesign.ImageOptions.Image");
		this.BtnReportDesign.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportDesign.ImageOptions.ImageIndex");
		this.BtnReportDesign.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportDesign.ImageOptions.LargeImageIndex");
		this.BtnReportDesign.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportDesign.ImageOptions.SvgImage");
		this.BtnReportDesign.Name = "BtnReportDesign";
		this.BtnReportDesign.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportDesign_ItemClick);
		resources.ApplyResources(this.btnDefualtAccount, "btnDefualtAccount");
		this.btnDefualtAccount.Id = 101;
		this.btnDefualtAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnDefualtAccount.ImageOptions.Image");
		this.btnDefualtAccount.ImageOptions.ImageIndex = (int)resources.GetObject("btnDefualtAccount.ImageOptions.ImageIndex");
		this.btnDefualtAccount.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnDefualtAccount.ImageOptions.LargeImageIndex");
		this.btnDefualtAccount.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnDefualtAccount.ImageOptions.SvgImage");
		this.btnDefualtAccount.Name = "btnDefualtAccount";
		this.btnDefualtAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.btnDefualtAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnDefualtAccount_ItemClick);
		resources.ApplyResources(this.LblCompany, "LblCompany");
		this.LblCompany.Id = 102;
		this.LblCompany.ImageOptions.ImageIndex = (int)resources.GetObject("LblCompany.ImageOptions.ImageIndex");
		this.LblCompany.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblCompany.ImageOptions.LargeImageIndex");
		this.LblCompany.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblCompany.ImageOptions.SvgImage");
		this.LblCompany.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblCompany.ItemAppearance.Disabled.Font");
		this.LblCompany.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblCompany.Name = "LblCompany";
		resources.ApplyResources(this.LblServer, "LblServer");
		this.LblServer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblServer.Id = 103;
		this.LblServer.ImageOptions.ImageIndex = (int)resources.GetObject("LblServer.ImageOptions.ImageIndex");
		this.LblServer.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblServer.ImageOptions.LargeImageIndex");
		this.LblServer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblServer.ImageOptions.SvgImage");
		this.LblServer.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblServer.ItemAppearance.Disabled.Font");
		this.LblServer.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblServer.Name = "LblServer";
		resources.ApplyResources(this.LblUser, "LblUser");
		this.LblUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblUser.Id = 104;
		this.LblUser.ImageOptions.ImageIndex = (int)resources.GetObject("LblUser.ImageOptions.ImageIndex");
		this.LblUser.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblUser.ImageOptions.LargeImageIndex");
		this.LblUser.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblUser.ImageOptions.SvgImage");
		this.LblUser.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblUser.ItemAppearance.Disabled.Font");
		this.LblUser.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblUser.Name = "LblUser";
		resources.ApplyResources(this.LblTime, "LblTime");
		this.LblTime.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblTime.Id = 105;
		this.LblTime.ImageOptions.ImageIndex = (int)resources.GetObject("LblTime.ImageOptions.ImageIndex");
		this.LblTime.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblTime.ImageOptions.LargeImageIndex");
		this.LblTime.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblTime.ImageOptions.SvgImage");
		this.LblTime.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblTime.ItemAppearance.Disabled.Font");
		this.LblTime.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblTime.Name = "LblTime";
		resources.ApplyResources(this.LblDate, "LblDate");
		this.LblDate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblDate.Id = 106;
		this.LblDate.ImageOptions.ImageIndex = (int)resources.GetObject("LblDate.ImageOptions.ImageIndex");
		this.LblDate.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblDate.ImageOptions.LargeImageIndex");
		this.LblDate.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblDate.ImageOptions.SvgImage");
		this.LblDate.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblDate.ItemAppearance.Disabled.Font");
		this.LblDate.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblDate.Name = "LblDate";
		resources.ApplyResources(this.barStaticItem5, "barStaticItem5");
		this.barStaticItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.barStaticItem5.Id = 107;
		this.barStaticItem5.ImageOptions.ImageIndex = (int)resources.GetObject("barStaticItem5.ImageOptions.ImageIndex");
		this.barStaticItem5.ImageOptions.LargeImageIndex = (int)resources.GetObject("barStaticItem5.ImageOptions.LargeImageIndex");
		this.barStaticItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barStaticItem5.ImageOptions.SvgImage");
		this.barStaticItem5.Name = "barStaticItem5";
		resources.ApplyResources(this.BtnReportPublic, "BtnReportPublic");
		this.BtnReportPublic.Id = 108;
		this.BtnReportPublic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportPublic.ImageOptions.Image");
		this.BtnReportPublic.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportPublic.ImageOptions.ImageIndex");
		this.BtnReportPublic.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportPublic.ImageOptions.LargeImageIndex");
		this.BtnReportPublic.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportPublic.ImageOptions.SvgImage");
		this.BtnReportPublic.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[5]
		{
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnMovementsDailyReport, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllMovementDays),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderDaily2),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportVAT),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnReportTobaccoTax, DevExpress.XtraBars.BarItemPaintStyle.Standard)
		});
		this.BtnReportPublic.Name = "BtnReportPublic";
		this.BtnReportPublic.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnMovementsDailyReport, "BtnMovementsDailyReport");
		this.BtnMovementsDailyReport.Id = 111;
		this.BtnMovementsDailyReport.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.Image");
		this.BtnMovementsDailyReport.ImageOptions.ImageIndex = (int)resources.GetObject("BtnMovementsDailyReport.ImageOptions.ImageIndex");
		this.BtnMovementsDailyReport.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.LargeImage");
		this.BtnMovementsDailyReport.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnMovementsDailyReport.ImageOptions.LargeImageIndex");
		this.BtnMovementsDailyReport.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnMovementsDailyReport.ImageOptions.SvgImage");
		this.BtnMovementsDailyReport.Name = "BtnMovementsDailyReport";
		this.BtnMovementsDailyReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMovementsDailyReport_ItemClick);
		resources.ApplyResources(this.BtnReportAllMovementDays, "BtnReportAllMovementDays");
		this.BtnReportAllMovementDays.Id = 165;
		this.BtnReportAllMovementDays.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.Image");
		this.BtnReportAllMovementDays.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllMovementDays.ImageOptions.ImageIndex");
		this.BtnReportAllMovementDays.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.LargeImage");
		this.BtnReportAllMovementDays.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllMovementDays.ImageOptions.LargeImageIndex");
		this.BtnReportAllMovementDays.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllMovementDays.ImageOptions.SvgImage");
		this.BtnReportAllMovementDays.Name = "BtnReportAllMovementDays";
		this.BtnReportAllMovementDays.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllMovementDays_ItemClick);
		resources.ApplyResources(this.BtnOrderDaily2, "BtnOrderDaily2");
		this.BtnOrderDaily2.Id = 203;
		this.BtnOrderDaily2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderDaily2.ImageOptions.Image");
		this.BtnOrderDaily2.ImageOptions.ImageIndex = (int)resources.GetObject("BtnOrderDaily2.ImageOptions.ImageIndex");
		this.BtnOrderDaily2.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderDaily2.ImageOptions.LargeImage");
		this.BtnOrderDaily2.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnOrderDaily2.ImageOptions.LargeImageIndex");
		this.BtnOrderDaily2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnOrderDaily2.ImageOptions.SvgImage");
		this.BtnOrderDaily2.Name = "BtnOrderDaily2";
		this.BtnOrderDaily2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDaily2_ItemClick);
		resources.ApplyResources(this.BtnReportVAT, "BtnReportVAT");
		this.BtnReportVAT.Id = 128;
		this.BtnReportVAT.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.Image");
		this.BtnReportVAT.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportVAT.ImageOptions.ImageIndex");
		this.BtnReportVAT.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.LargeImage");
		this.BtnReportVAT.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportVAT.ImageOptions.LargeImageIndex");
		this.BtnReportVAT.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportVAT.ImageOptions.SvgImage");
		this.BtnReportVAT.Name = "BtnReportVAT";
		this.BtnReportVAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportVAT_ItemClick);
		resources.ApplyResources(this.BtnReportTobaccoTax, "BtnReportTobaccoTax");
		this.BtnReportTobaccoTax.Id = 171;
		this.BtnReportTobaccoTax.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.Image");
		this.BtnReportTobaccoTax.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportTobaccoTax.ImageOptions.ImageIndex");
		this.BtnReportTobaccoTax.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.LargeImage");
		this.BtnReportTobaccoTax.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportTobaccoTax.ImageOptions.LargeImageIndex");
		this.BtnReportTobaccoTax.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportTobaccoTax.ImageOptions.SvgImage");
		this.BtnReportTobaccoTax.Name = "BtnReportTobaccoTax";
		this.BtnReportTobaccoTax.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportTobaccoTax_ItemClick);
		resources.ApplyResources(this.BtnReportStore, "BtnReportStore");
		this.BtnReportStore.Id = 109;
		this.BtnReportStore.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportStore.ImageOptions.Image");
		this.BtnReportStore.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportStore.ImageOptions.ImageIndex");
		this.BtnReportStore.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportStore.ImageOptions.LargeImageIndex");
		this.BtnReportStore.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportStore.ImageOptions.SvgImage");
		this.BtnReportStore.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[7]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnGard2),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPGard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMovementItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMovementItemActive),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByGroup),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRptStoreItemLimit)
		});
		this.BtnReportStore.Name = "BtnReportStore";
		this.BtnReportStore.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnGard2, "BtnGard2");
		this.BtnGard2.Id = 205;
		this.BtnGard2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGard2.ImageOptions.Image");
		this.BtnGard2.ImageOptions.ImageIndex = (int)resources.GetObject("BtnGard2.ImageOptions.ImageIndex");
		this.BtnGard2.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnGard2.ImageOptions.LargeImageIndex");
		this.BtnGard2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGard2.ImageOptions.SvgImage");
		this.BtnGard2.Name = "BtnGard2";
		this.BtnGard2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGard2_ItemClick);
		resources.ApplyResources(this.BtnRPGard, "BtnRPGard");
		this.BtnRPGard.Id = 110;
		this.BtnRPGard.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.Image");
		this.BtnRPGard.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPGard.ImageOptions.ImageIndex");
		this.BtnRPGard.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.LargeImage");
		this.BtnRPGard.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPGard.ImageOptions.LargeImageIndex");
		this.BtnRPGard.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPGard.ImageOptions.SvgImage");
		this.BtnRPGard.Name = "BtnRPGard";
		this.BtnRPGard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPGard_ItemClick);
		resources.ApplyResources(this.BtnRPMovementItem, "BtnRPMovementItem");
		this.BtnRPMovementItem.Id = 146;
		this.BtnRPMovementItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.Image");
		this.BtnRPMovementItem.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPMovementItem.ImageOptions.ImageIndex");
		this.BtnRPMovementItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.LargeImage");
		this.BtnRPMovementItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPMovementItem.ImageOptions.LargeImageIndex");
		this.BtnRPMovementItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPMovementItem.ImageOptions.SvgImage");
		this.BtnRPMovementItem.Name = "BtnRPMovementItem";
		this.BtnRPMovementItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMovementItem_ItemClick);
		resources.ApplyResources(this.BtnRPMovementItemActive, "BtnRPMovementItemActive");
		this.BtnRPMovementItemActive.Id = 199;
		this.BtnRPMovementItemActive.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMovementItemActive.ImageOptions.Image");
		this.BtnRPMovementItemActive.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPMovementItemActive.ImageOptions.ImageIndex");
		this.BtnRPMovementItemActive.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMovementItemActive.ImageOptions.LargeImage");
		this.BtnRPMovementItemActive.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPMovementItemActive.ImageOptions.LargeImageIndex");
		this.BtnRPMovementItemActive.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPMovementItemActive.ImageOptions.SvgImage");
		this.BtnRPMovementItemActive.Name = "BtnRPMovementItemActive";
		this.BtnRPMovementItemActive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMovementItemActive_ItemClick);
		resources.ApplyResources(this.BtnRPStoreAvergByItem, "BtnRPStoreAvergByItem");
		this.BtnRPStoreAvergByItem.Id = 161;
		this.BtnRPStoreAvergByItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.Image");
		this.BtnRPStoreAvergByItem.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.ImageIndex");
		this.BtnRPStoreAvergByItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.LargeImageIndex");
		this.BtnRPStoreAvergByItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.SvgImage");
		this.BtnRPStoreAvergByItem.Name = "BtnRPStoreAvergByItem";
		this.BtnRPStoreAvergByItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByItem_ItemClick);
		resources.ApplyResources(this.BtnRPStoreAvergByGroup, "BtnRPStoreAvergByGroup");
		this.BtnRPStoreAvergByGroup.Id = 160;
		this.BtnRPStoreAvergByGroup.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.Image");
		this.BtnRPStoreAvergByGroup.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.ImageIndex");
		this.BtnRPStoreAvergByGroup.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByGroup.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.LargeImageIndex");
		this.BtnRPStoreAvergByGroup.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.SvgImage");
		this.BtnRPStoreAvergByGroup.Name = "BtnRPStoreAvergByGroup";
		this.BtnRPStoreAvergByGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByGroup_ItemClick);
		resources.ApplyResources(this.BtnRptStoreItemLimit, "BtnRptStoreItemLimit");
		this.BtnRptStoreItemLimit.Id = 200;
		this.BtnRptStoreItemLimit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRptStoreItemLimit.ImageOptions.Image");
		this.BtnRptStoreItemLimit.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRptStoreItemLimit.ImageOptions.ImageIndex");
		this.BtnRptStoreItemLimit.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRptStoreItemLimit.ImageOptions.LargeImageIndex");
		this.BtnRptStoreItemLimit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRptStoreItemLimit.ImageOptions.SvgImage");
		this.BtnRptStoreItemLimit.Name = "BtnRptStoreItemLimit";
		this.BtnRptStoreItemLimit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRptStoreItemLimit_ItemClick);
		resources.ApplyResources(this.BtnPrinters, "BtnPrinters");
		this.BtnPrinters.Id = 112;
		this.BtnPrinters.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrinters.ImageOptions.Image");
		this.BtnPrinters.ImageOptions.ImageIndex = (int)resources.GetObject("BtnPrinters.ImageOptions.ImageIndex");
		this.BtnPrinters.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnPrinters.ImageOptions.LargeImageIndex");
		this.BtnPrinters.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPrinters.ImageOptions.SvgImage");
		this.BtnPrinters.Name = "BtnPrinters";
		this.BtnPrinters.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPrinters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPrinters_ItemClick);
		resources.ApplyResources(this.BtnAbout, "BtnAbout");
		this.BtnAbout.Id = 113;
		this.BtnAbout.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAbout.ImageOptions.Image");
		this.BtnAbout.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAbout.ImageOptions.ImageIndex");
		this.BtnAbout.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAbout.ImageOptions.LargeImageIndex");
		this.BtnAbout.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAbout.ImageOptions.SvgImage");
		this.BtnAbout.Name = "BtnAbout";
		this.BtnAbout.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAbout_ItemClick);
		resources.ApplyResources(this.BtnReportOrder, "BtnReportOrder");
		this.BtnReportOrder.Id = 114;
		this.BtnReportOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportOrder.ImageOptions.Image");
		this.BtnReportOrder.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportOrder.ImageOptions.ImageIndex");
		this.BtnReportOrder.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportOrder.ImageOptions.LargeImageIndex");
		this.BtnReportOrder.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportOrder.ImageOptions.SvgImage");
		this.BtnReportOrder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[6]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrders),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturnDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSalesEmployee),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderDetailsSmall)
		});
		this.BtnReportOrder.Name = "BtnReportOrder";
		this.BtnReportOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnReportAllOrders, "BtnReportAllOrders");
		this.BtnReportAllOrders.Id = 116;
		this.BtnReportAllOrders.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.Image");
		this.BtnReportAllOrders.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllOrders.ImageOptions.ImageIndex");
		this.BtnReportAllOrders.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.LargeImage");
		this.BtnReportAllOrders.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllOrders.ImageOptions.LargeImageIndex");
		this.BtnReportAllOrders.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllOrders.ImageOptions.SvgImage");
		this.BtnReportAllOrders.Name = "BtnReportAllOrders";
		this.BtnReportAllOrders.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrders_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersDetails, "BtnReportAllOrdersDetails");
		this.BtnReportAllOrdersDetails.Id = 118;
		this.BtnReportAllOrdersDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.Image");
		this.BtnReportAllOrdersDetails.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.ImageIndex");
		this.BtnReportAllOrdersDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersDetails.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.LargeImageIndex");
		this.BtnReportAllOrdersDetails.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.SvgImage");
		this.BtnReportAllOrdersDetails.Name = "BtnReportAllOrdersDetails";
		this.BtnReportAllOrdersDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersDetails_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersReturn, "BtnReportAllOrdersReturn");
		this.BtnReportAllOrdersReturn.Id = 117;
		this.BtnReportAllOrdersReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.Image");
		this.BtnReportAllOrdersReturn.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.ImageIndex");
		this.BtnReportAllOrdersReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturn.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.LargeImageIndex");
		this.BtnReportAllOrdersReturn.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.SvgImage");
		this.BtnReportAllOrdersReturn.Name = "BtnReportAllOrdersReturn";
		this.BtnReportAllOrdersReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturn_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersReturnDetails, "BtnReportAllOrdersReturnDetails");
		this.BtnReportAllOrdersReturnDetails.Id = 119;
		this.BtnReportAllOrdersReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.Image");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.ImageIndex");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.LargeImageIndex");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.SvgImage");
		this.BtnReportAllOrdersReturnDetails.Name = "BtnReportAllOrdersReturnDetails";
		this.BtnReportAllOrdersReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturnDetails_ItemClick);
		resources.ApplyResources(this.BtnReportSalesEmployee, "BtnReportSalesEmployee");
		this.BtnReportSalesEmployee.Id = 133;
		this.BtnReportSalesEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.Image");
		this.BtnReportSalesEmployee.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportSalesEmployee.ImageOptions.ImageIndex");
		this.BtnReportSalesEmployee.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.LargeImage");
		this.BtnReportSalesEmployee.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportSalesEmployee.ImageOptions.LargeImageIndex");
		this.BtnReportSalesEmployee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportSalesEmployee.ImageOptions.SvgImage");
		this.BtnReportSalesEmployee.Name = "BtnReportSalesEmployee";
		this.BtnReportSalesEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSalesEmployee_ItemClick);
		resources.ApplyResources(this.BtnOrderDetailsSmall, "BtnOrderDetailsSmall");
		this.BtnOrderDetailsSmall.Id = 206;
		this.BtnOrderDetailsSmall.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderDetailsSmall.ImageOptions.Image");
		this.BtnOrderDetailsSmall.ImageOptions.ImageIndex = (int)resources.GetObject("BtnOrderDetailsSmall.ImageOptions.ImageIndex");
		this.BtnOrderDetailsSmall.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderDetailsSmall.ImageOptions.LargeImage");
		this.BtnOrderDetailsSmall.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnOrderDetailsSmall.ImageOptions.LargeImageIndex");
		this.BtnOrderDetailsSmall.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnOrderDetailsSmall.ImageOptions.SvgImage");
		this.BtnOrderDetailsSmall.Name = "BtnOrderDetailsSmall";
		this.BtnOrderDetailsSmall.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDetailsSmall_ItemClick);
		resources.ApplyResources(this.BtnReportPurshes, "BtnReportPurshes");
		this.BtnReportPurshes.Id = 115;
		this.BtnReportPurshes.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportPurshes.ImageOptions.Image");
		this.BtnReportPurshes.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportPurshes.ImageOptions.ImageIndex");
		this.BtnReportPurshes.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportPurshes.ImageOptions.LargeImageIndex");
		this.BtnReportPurshes.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportPurshes.ImageOptions.SvgImage");
		this.BtnReportPurshes.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurches),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllPurchesReturnDetails)
		});
		this.BtnReportPurshes.Name = "BtnReportPurshes";
		this.BtnReportPurshes.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnReportAllPurches, "BtnReportAllPurches");
		this.BtnReportAllPurches.Id = 124;
		this.BtnReportAllPurches.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.Image");
		this.BtnReportAllPurches.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllPurches.ImageOptions.ImageIndex");
		this.BtnReportAllPurches.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.LargeImage");
		this.BtnReportAllPurches.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllPurches.ImageOptions.LargeImageIndex");
		this.BtnReportAllPurches.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllPurches.ImageOptions.SvgImage");
		this.BtnReportAllPurches.Name = "BtnReportAllPurches";
		this.BtnReportAllPurches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurches_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesDetails, "BtnReportAllPurchesDetails");
		this.BtnReportAllPurchesDetails.Id = 125;
		this.BtnReportAllPurchesDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.Image");
		this.BtnReportAllPurchesDetails.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.ImageIndex");
		this.BtnReportAllPurchesDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesDetails.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.LargeImageIndex");
		this.BtnReportAllPurchesDetails.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.SvgImage");
		this.BtnReportAllPurchesDetails.Name = "BtnReportAllPurchesDetails";
		this.BtnReportAllPurchesDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesDetails_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesReturn, "BtnReportAllPurchesReturn");
		this.BtnReportAllPurchesReturn.Id = 126;
		this.BtnReportAllPurchesReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.Image");
		this.BtnReportAllPurchesReturn.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.ImageIndex");
		this.BtnReportAllPurchesReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturn.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.LargeImageIndex");
		this.BtnReportAllPurchesReturn.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.SvgImage");
		this.BtnReportAllPurchesReturn.Name = "BtnReportAllPurchesReturn";
		this.BtnReportAllPurchesReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturn_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesReturnDetails, "BtnReportAllPurchesReturnDetails");
		this.BtnReportAllPurchesReturnDetails.Id = 127;
		this.BtnReportAllPurchesReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.Image");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.ImageIndex");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.LargeImageIndex");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.SvgImage");
		this.BtnReportAllPurchesReturnDetails.Name = "BtnReportAllPurchesReturnDetails";
		this.BtnReportAllPurchesReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturnDetails_ItemClick);
		resources.ApplyResources(this.BtnReportProfit, "BtnReportProfit");
		this.BtnReportProfit.Id = 120;
		this.BtnReportProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportProfit.ImageOptions.Image");
		this.BtnReportProfit.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportProfit.ImageOptions.ImageIndex");
		this.BtnReportProfit.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportProfit.ImageOptions.LargeImageIndex");
		this.BtnReportProfit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportProfit.ImageOptions.SvgImage");
		this.BtnReportProfit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPOrderProfit),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItems),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItemsItemOnly)
		});
		this.BtnReportProfit.Name = "BtnReportProfit";
		this.BtnReportProfit.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnRPOrderProfit, "BtnRPOrderProfit");
		this.BtnRPOrderProfit.Id = 121;
		this.BtnRPOrderProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.Image");
		this.BtnRPOrderProfit.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPOrderProfit.ImageOptions.ImageIndex");
		this.BtnRPOrderProfit.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.LargeImage");
		this.BtnRPOrderProfit.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPOrderProfit.ImageOptions.LargeImageIndex");
		this.BtnRPOrderProfit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPOrderProfit.ImageOptions.SvgImage");
		this.BtnRPOrderProfit.Name = "BtnRPOrderProfit";
		this.BtnRPOrderProfit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPOrderProfit_ItemClick);
		resources.ApplyResources(this.BtnRPProfitByItems, "BtnRPProfitByItems");
		this.BtnRPProfitByItems.Id = 122;
		this.BtnRPProfitByItems.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.Image");
		this.BtnRPProfitByItems.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPProfitByItems.ImageOptions.ImageIndex");
		this.BtnRPProfitByItems.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.LargeImage");
		this.BtnRPProfitByItems.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPProfitByItems.ImageOptions.LargeImageIndex");
		this.BtnRPProfitByItems.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPProfitByItems.ImageOptions.SvgImage");
		this.BtnRPProfitByItems.Name = "BtnRPProfitByItems";
		this.BtnRPProfitByItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItems_ItemClick);
		resources.ApplyResources(this.barButtonItem6, "barButtonItem6");
		this.barButtonItem6.Id = 123;
		this.barButtonItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
		this.barButtonItem6.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem6.ImageOptions.ImageIndex");
		this.barButtonItem6.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
		this.barButtonItem6.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem6.ImageOptions.LargeImageIndex");
		this.barButtonItem6.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem6.ImageOptions.SvgImage");
		this.barButtonItem6.Name = "barButtonItem6";
		this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem6_ItemClick);
		resources.ApplyResources(this.BtnRPProfitByItemsItemOnly, "BtnRPProfitByItemsItemOnly");
		this.BtnRPProfitByItemsItemOnly.Id = 159;
		this.BtnRPProfitByItemsItemOnly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.Image");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.ImageIndex");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.LargeImageIndex");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.SvgImage");
		this.BtnRPProfitByItemsItemOnly.Name = "BtnRPProfitByItemsItemOnly";
		this.BtnRPProfitByItemsItemOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItemsItemOnly_ItemClick);
		resources.ApplyResources(this.barButtonItem20, "barButtonItem20");
		this.barButtonItem20.Id = 129;
		this.barButtonItem20.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem20.ImageOptions.ImageIndex");
		this.barButtonItem20.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem20.ImageOptions.LargeImageIndex");
		this.barButtonItem20.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem20.ImageOptions.SvgImage");
		this.barButtonItem20.Name = "barButtonItem20";
		resources.ApplyResources(this.barButtonItem21, "barButtonItem21");
		this.barButtonItem21.Id = 130;
		this.barButtonItem21.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem21.ImageOptions.ImageIndex");
		this.barButtonItem21.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem21.ImageOptions.LargeImageIndex");
		this.barButtonItem21.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem21.ImageOptions.SvgImage");
		this.barButtonItem21.Name = "barButtonItem21";
		resources.ApplyResources(this.barButtonItem22, "barButtonItem22");
		this.barButtonItem22.Id = 131;
		this.barButtonItem22.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem22.ImageOptions.ImageIndex");
		this.barButtonItem22.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem22.ImageOptions.LargeImageIndex");
		this.barButtonItem22.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem22.ImageOptions.SvgImage");
		this.barButtonItem22.Name = "barButtonItem22";
		resources.ApplyResources(this.barSubItem5, "barSubItem5");
		this.barSubItem5.Id = 132;
		this.barSubItem5.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem5.ImageOptions.ImageIndex");
		this.barSubItem5.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem5.ImageOptions.LargeImageIndex");
		this.barSubItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem5.ImageOptions.SvgImage");
		this.barSubItem5.Name = "barSubItem5";
		resources.ApplyResources(this.barCheckItem1, "barCheckItem1");
		this.barCheckItem1.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
		this.barCheckItem1.Id = 134;
		this.barCheckItem1.ImageOptions.ImageIndex = (int)resources.GetObject("barCheckItem1.ImageOptions.ImageIndex");
		this.barCheckItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barCheckItem1.ImageOptions.LargeImageIndex");
		this.barCheckItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barCheckItem1.ImageOptions.SvgImage");
		this.barCheckItem1.Name = "barCheckItem1";
		this.barCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.DateApp, "DateApp");
		this.DateApp.Edit = this.repositoryItemDateEdit2;
		this.DateApp.Id = 136;
		this.DateApp.ImageOptions.ImageIndex = (int)resources.GetObject("DateApp.ImageOptions.ImageIndex");
		this.DateApp.ImageOptions.LargeImageIndex = (int)resources.GetObject("DateApp.ImageOptions.LargeImageIndex");
		this.DateApp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("DateApp.ImageOptions.SvgImage");
		this.DateApp.Name = "DateApp";
		this.DateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.repositoryItemDateEdit2, "repositoryItemDateEdit2");
		this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit2.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
		resources.ApplyResources(this.BtnUpdateDateApp, "BtnUpdateDateApp");
		this.BtnUpdateDateApp.Id = 137;
		this.BtnUpdateDateApp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUpdateDateApp.ImageOptions.Image");
		this.BtnUpdateDateApp.ImageOptions.ImageIndex = (int)resources.GetObject("BtnUpdateDateApp.ImageOptions.ImageIndex");
		this.BtnUpdateDateApp.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnUpdateDateApp.ImageOptions.LargeImageIndex");
		this.BtnUpdateDateApp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnUpdateDateApp.ImageOptions.SvgImage");
		this.BtnUpdateDateApp.Name = "BtnUpdateDateApp";
		this.BtnUpdateDateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUpdateDateApp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUpdateDateApp_ItemClick);
		resources.ApplyResources(this.BtnDateApp, "BtnDateApp");
		this.BtnDateApp.Edit = this.repositoryItemDateEdit3;
		this.BtnDateApp.Id = 138;
		this.BtnDateApp.ImageOptions.ImageIndex = (int)resources.GetObject("BtnDateApp.ImageOptions.ImageIndex");
		this.BtnDateApp.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnDateApp.ImageOptions.LargeImageIndex");
		this.BtnDateApp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnDateApp.ImageOptions.SvgImage");
		this.BtnDateApp.Name = "BtnDateApp";
		this.BtnDateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDateApp.EditValueChanged += new System.EventHandler(BtnDateApp_EditValueChanged);
		this.repositoryItemDateEdit3.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.repositoryItemDateEdit3.Appearance.Font = (System.Drawing.Font)resources.GetObject("repositoryItemDateEdit3.Appearance.Font");
		this.repositoryItemDateEdit3.Appearance.Options.UseFont = true;
		this.repositoryItemDateEdit3.Appearance.Options.UseTextOptions = true;
		this.repositoryItemDateEdit3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		resources.ApplyResources(this.repositoryItemDateEdit3, "repositoryItemDateEdit3");
		this.repositoryItemDateEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit3.Buttons"))
		});
		this.repositoryItemDateEdit3.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit3.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit3.Mask.EditMask = resources.GetString("repositoryItemDateEdit3.Mask.EditMask");
		this.repositoryItemDateEdit3.Mask.UseMaskAsDisplayFormat = (bool)resources.GetObject("repositoryItemDateEdit3.Mask.UseMaskAsDisplayFormat");
		this.repositoryItemDateEdit3.Name = "repositoryItemDateEdit3";
		resources.ApplyResources(this.barEditItem1, "barEditItem1");
		this.barEditItem1.Edit = this.repositoryItemDateEdit4;
		this.barEditItem1.Id = 141;
		this.barEditItem1.ImageOptions.ImageIndex = (int)resources.GetObject("barEditItem1.ImageOptions.ImageIndex");
		this.barEditItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barEditItem1.ImageOptions.LargeImageIndex");
		this.barEditItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barEditItem1.ImageOptions.SvgImage");
		this.barEditItem1.Name = "barEditItem1";
		resources.ApplyResources(this.repositoryItemDateEdit4, "repositoryItemDateEdit4");
		this.repositoryItemDateEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit4.Buttons"))
		});
		this.repositoryItemDateEdit4.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit4.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit4.Name = "repositoryItemDateEdit4";
		resources.ApplyResources(this.LblVerg, "LblVerg");
		this.LblVerg.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblVerg.Id = 142;
		this.LblVerg.ImageOptions.ImageIndex = (int)resources.GetObject("LblVerg.ImageOptions.ImageIndex");
		this.LblVerg.ImageOptions.LargeImageIndex = (int)resources.GetObject("LblVerg.ImageOptions.LargeImageIndex");
		this.LblVerg.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("LblVerg.ImageOptions.SvgImage");
		this.LblVerg.Name = "LblVerg";
		resources.ApplyResources(this.BtnReportAllOrdersRestaurant, "BtnReportAllOrdersRestaurant");
		this.BtnReportAllOrdersRestaurant.Id = 143;
		this.BtnReportAllOrdersRestaurant.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportAllOrdersRestaurant.ImageOptions.ImageIndex");
		this.BtnReportAllOrdersRestaurant.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportAllOrdersRestaurant.ImageOptions.LargeImageIndex");
		this.BtnReportAllOrdersRestaurant.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportAllOrdersRestaurant.ImageOptions.SvgImage");
		this.BtnReportAllOrdersRestaurant.Name = "BtnReportAllOrdersRestaurant";
		this.BtnReportAllOrdersRestaurant.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersRestaurant_ItemClick);
		resources.ApplyResources(this.BtnCheckOutOrder, "BtnCheckOutOrder");
		this.BtnCheckOutOrder.Id = 144;
		this.BtnCheckOutOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCheckOutOrder.ImageOptions.Image");
		this.BtnCheckOutOrder.ImageOptions.ImageIndex = (int)resources.GetObject("BtnCheckOutOrder.ImageOptions.ImageIndex");
		this.BtnCheckOutOrder.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnCheckOutOrder.ImageOptions.LargeImageIndex");
		this.BtnCheckOutOrder.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCheckOutOrder.ImageOptions.SvgImage");
		this.BtnCheckOutOrder.Name = "BtnCheckOutOrder";
		this.BtnCheckOutOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckOutOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckOutOrder_ItemClick);
		resources.ApplyResources(this.BtnConnection, "BtnConnection");
		this.BtnConnection.Id = 145;
		this.BtnConnection.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnConnection.ImageOptions.Image");
		this.BtnConnection.ImageOptions.ImageIndex = (int)resources.GetObject("BtnConnection.ImageOptions.ImageIndex");
		this.BtnConnection.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnConnection.ImageOptions.LargeImageIndex");
		this.BtnConnection.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnConnection.ImageOptions.SvgImage");
		this.BtnConnection.Name = "BtnConnection";
		this.BtnConnection.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnConnection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnConnection_ItemClick);
		resources.ApplyResources(this.BtnTransfer, "BtnTransfer");
		this.BtnTransfer.Id = 147;
		this.BtnTransfer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTransfer.ImageOptions.Image");
		this.BtnTransfer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnTransfer.ImageOptions.ImageIndex");
		this.BtnTransfer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnTransfer.ImageOptions.LargeImageIndex");
		this.BtnTransfer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTransfer.ImageOptions.SvgImage");
		this.BtnTransfer.Name = "BtnTransfer";
		this.BtnTransfer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTransfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTransfer_ItemClick);
		resources.ApplyResources(this.BtnDabase, "BtnDabase");
		this.BtnDabase.Id = 151;
		this.BtnDabase.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDabase.ImageOptions.Image");
		this.BtnDabase.ImageOptions.ImageIndex = (int)resources.GetObject("BtnDabase.ImageOptions.ImageIndex");
		this.BtnDabase.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnDabase.ImageOptions.LargeImageIndex");
		this.BtnDabase.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnDabase.ImageOptions.SvgImage");
		this.BtnDabase.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[7]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnClearDB),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnEmptyDB),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnBackup),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem16),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem17)
		});
		this.BtnDabase.Name = "BtnDabase";
		this.BtnDabase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnClearDB, "BtnClearDB");
		this.BtnClearDB.Id = 152;
		this.BtnClearDB.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnClearDB.ImageOptions.Image");
		this.BtnClearDB.ImageOptions.ImageIndex = (int)resources.GetObject("BtnClearDB.ImageOptions.ImageIndex");
		this.BtnClearDB.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnClearDB.ImageOptions.LargeImage");
		this.BtnClearDB.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnClearDB.ImageOptions.LargeImageIndex");
		this.BtnClearDB.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnClearDB.ImageOptions.SvgImage");
		this.BtnClearDB.Name = "BtnClearDB";
		this.BtnClearDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnClearDB_ItemClick);
		resources.ApplyResources(this.BtnEmptyDB, "BtnEmptyDB");
		this.BtnEmptyDB.Id = 153;
		this.BtnEmptyDB.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEmptyDB.ImageOptions.Image");
		this.BtnEmptyDB.ImageOptions.ImageIndex = (int)resources.GetObject("BtnEmptyDB.ImageOptions.ImageIndex");
		this.BtnEmptyDB.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnEmptyDB.ImageOptions.LargeImage");
		this.BtnEmptyDB.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnEmptyDB.ImageOptions.LargeImageIndex");
		this.BtnEmptyDB.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnEmptyDB.ImageOptions.SvgImage");
		this.BtnEmptyDB.Name = "BtnEmptyDB";
		this.BtnEmptyDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmptyDB_ItemClick);
		resources.ApplyResources(this.BtnBackup, "BtnBackup");
		this.BtnBackup.Id = 163;
		this.BtnBackup.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnBackup.ImageOptions.Image");
		this.BtnBackup.ImageOptions.ImageIndex = (int)resources.GetObject("BtnBackup.ImageOptions.ImageIndex");
		this.BtnBackup.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnBackup.ImageOptions.LargeImage");
		this.BtnBackup.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnBackup.ImageOptions.LargeImageIndex");
		this.BtnBackup.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnBackup.ImageOptions.SvgImage");
		this.BtnBackup.Name = "BtnBackup";
		this.BtnBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBackup_ItemClick);
		resources.ApplyResources(this.barButtonItem3, "barButtonItem3");
		this.barButtonItem3.Id = 211;
		this.barButtonItem3.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem3.ImageOptions.ImageIndex");
		this.barButtonItem3.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem3.ImageOptions.LargeImageIndex");
		this.barButtonItem3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem3.ImageOptions.SvgImage");
		this.barButtonItem3.Name = "barButtonItem3";
		this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick_2);
		resources.ApplyResources(this.barButtonItem9, "barButtonItem9");
		this.barButtonItem9.Id = 213;
		this.barButtonItem9.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem9.ImageOptions.Image");
		this.barButtonItem9.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem9.ImageOptions.ImageIndex");
		this.barButtonItem9.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem9.ImageOptions.LargeImage");
		this.barButtonItem9.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem9.ImageOptions.LargeImageIndex");
		this.barButtonItem9.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem9.ImageOptions.SvgImage");
		this.barButtonItem9.Name = "barButtonItem9";
		this.barButtonItem9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem9_ItemClick);
		resources.ApplyResources(this.barButtonItem16, "barButtonItem16");
		this.barButtonItem16.Id = 214;
		this.barButtonItem16.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem16.ImageOptions.Image");
		this.barButtonItem16.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem16.ImageOptions.ImageIndex");
		this.barButtonItem16.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem16.ImageOptions.LargeImage");
		this.barButtonItem16.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem16.ImageOptions.LargeImageIndex");
		this.barButtonItem16.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem16.ImageOptions.SvgImage");
		this.barButtonItem16.Name = "barButtonItem16";
		this.barButtonItem16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem16_ItemClick);
		resources.ApplyResources(this.barSubItem6, "barSubItem6");
		this.barSubItem6.Id = 154;
		this.barSubItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem6.ImageOptions.Image");
		this.barSubItem6.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem6.ImageOptions.ImageIndex");
		this.barSubItem6.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem6.ImageOptions.LargeImageIndex");
		this.barSubItem6.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem6.ImageOptions.SvgImage");
		this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerSupp)
		});
		this.barSubItem6.Name = "barSubItem6";
		this.barSubItem6.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnRPCustomer, "BtnRPCustomer");
		this.BtnRPCustomer.Id = 155;
		this.BtnRPCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.Image");
		this.BtnRPCustomer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPCustomer.ImageOptions.ImageIndex");
		this.BtnRPCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.LargeImage");
		this.BtnRPCustomer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPCustomer.ImageOptions.LargeImageIndex");
		this.BtnRPCustomer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPCustomer.ImageOptions.SvgImage");
		this.BtnRPCustomer.Name = "BtnRPCustomer";
		this.BtnRPCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCustomer_ItemClick);
		resources.ApplyResources(this.BtnRPSupp, "BtnRPSupp");
		this.BtnRPSupp.Id = 156;
		this.BtnRPSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.Image");
		this.BtnRPSupp.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPSupp.ImageOptions.ImageIndex");
		this.BtnRPSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.LargeImage");
		this.BtnRPSupp.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPSupp.ImageOptions.LargeImageIndex");
		this.BtnRPSupp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPSupp.ImageOptions.SvgImage");
		this.BtnRPSupp.Name = "BtnRPSupp";
		this.BtnRPSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPSupp_ItemClick);
		resources.ApplyResources(this.BtnRPLeadgerCustomer, "BtnRPLeadgerCustomer");
		this.BtnRPLeadgerCustomer.Id = 157;
		this.BtnRPLeadgerCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.Image");
		this.BtnRPLeadgerCustomer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.ImageIndex");
		this.BtnRPLeadgerCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.LargeImage");
		this.BtnRPLeadgerCustomer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.LargeImageIndex");
		this.BtnRPLeadgerCustomer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.SvgImage");
		this.BtnRPLeadgerCustomer.Name = "BtnRPLeadgerCustomer";
		this.BtnRPLeadgerCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerCustomer_ItemClick);
		resources.ApplyResources(this.BtnRPLeadgerSupp, "BtnRPLeadgerSupp");
		this.BtnRPLeadgerSupp.Id = 158;
		this.BtnRPLeadgerSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.Image");
		this.BtnRPLeadgerSupp.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.ImageIndex");
		this.BtnRPLeadgerSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.LargeImage");
		this.BtnRPLeadgerSupp.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.LargeImageIndex");
		this.BtnRPLeadgerSupp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.SvgImage");
		this.BtnRPLeadgerSupp.Name = "BtnRPLeadgerSupp";
		this.BtnRPLeadgerSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerSupp_ItemClick);
		resources.ApplyResources(this.BtnVat, "BtnVat");
		this.BtnVat.Id = 164;
		this.BtnVat.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnVat.ImageOptions.Image");
		this.BtnVat.ImageOptions.ImageIndex = (int)resources.GetObject("BtnVat.ImageOptions.ImageIndex");
		this.BtnVat.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnVat.ImageOptions.LargeImageIndex");
		this.BtnVat.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnVat.ImageOptions.SvgImage");
		this.BtnVat.Name = "BtnVat";
		this.BtnVat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnVat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnVat.VisibleInSearchMenu = false;
		this.BtnVat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnVat_ItemClick);
		resources.ApplyResources(this.barCheckItem2, "barCheckItem2");
		this.barCheckItem2.Id = 166;
		this.barCheckItem2.ImageOptions.ImageIndex = (int)resources.GetObject("barCheckItem2.ImageOptions.ImageIndex");
		this.barCheckItem2.ImageOptions.LargeImageIndex = (int)resources.GetObject("barCheckItem2.ImageOptions.LargeImageIndex");
		this.barCheckItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barCheckItem2.ImageOptions.SvgImage");
		this.barCheckItem2.Name = "barCheckItem2";
		resources.ApplyResources(this.BtnEncriptPassword, "BtnEncriptPassword");
		this.BtnEncriptPassword.Id = 167;
		this.BtnEncriptPassword.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEncriptPassword.ImageOptions.Image");
		this.BtnEncriptPassword.ImageOptions.ImageIndex = (int)resources.GetObject("BtnEncriptPassword.ImageOptions.ImageIndex");
		this.BtnEncriptPassword.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnEncriptPassword.ImageOptions.LargeImageIndex");
		this.BtnEncriptPassword.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnEncriptPassword.ImageOptions.SvgImage");
		this.BtnEncriptPassword.Name = "BtnEncriptPassword";
		this.BtnEncriptPassword.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEncriptPassword.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnEncriptPassword.VisibleInSearchMenu = false;
		this.BtnEncriptPassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEncriptPassword_ItemClick);
		resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
		this.barButtonItem1.Id = 168;
		this.barButtonItem1.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem1.ImageOptions.ImageIndex");
		this.barButtonItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem1.ImageOptions.LargeImageIndex");
		this.barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
		this.barButtonItem1.Name = "barButtonItem1";
		resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
		this.barButtonItem2.Id = 169;
		this.barButtonItem2.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem2.ImageOptions.ImageIndex");
		this.barButtonItem2.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem2.ImageOptions.LargeImageIndex");
		this.barButtonItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem2.ImageOptions.SvgImage");
		this.barButtonItem2.Name = "barButtonItem2";
		resources.ApplyResources(this.BtnLock, "BtnLock");
		this.BtnLock.Id = 170;
		this.BtnLock.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnLock.ImageOptions.Image");
		this.BtnLock.ImageOptions.ImageIndex = (int)resources.GetObject("BtnLock.ImageOptions.ImageIndex");
		this.BtnLock.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnLock.ImageOptions.LargeImageIndex");
		this.BtnLock.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnLock.ImageOptions.SvgImage");
		this.BtnLock.Name = "BtnLock";
		this.BtnLock.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnLock_ItemClick);
		resources.ApplyResources(this.BtnEmployee, "BtnEmployee");
		this.BtnEmployee.Id = 172;
		this.BtnEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEmployee.ImageOptions.Image");
		this.BtnEmployee.ImageOptions.ImageIndex = (int)resources.GetObject("BtnEmployee.ImageOptions.ImageIndex");
		this.BtnEmployee.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnEmployee.ImageOptions.LargeImageIndex");
		this.BtnEmployee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnEmployee.ImageOptions.SvgImage");
		this.BtnEmployee.Name = "BtnEmployee";
		this.BtnEmployee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmployee_ItemClick);
		resources.ApplyResources(this.BtnPayrollTest, "BtnPayrollTest");
		this.BtnPayrollTest.Id = 173;
		this.BtnPayrollTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayrollTest.ImageOptions.Image");
		this.BtnPayrollTest.ImageOptions.ImageIndex = (int)resources.GetObject("BtnPayrollTest.ImageOptions.ImageIndex");
		this.BtnPayrollTest.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnPayrollTest.ImageOptions.LargeImageIndex");
		this.BtnPayrollTest.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPayrollTest.ImageOptions.SvgImage");
		this.BtnPayrollTest.Name = "BtnPayrollTest";
		this.BtnPayrollTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayrollTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayrollTest_ItemClick);
		resources.ApplyResources(this.BtnPayroll, "BtnPayroll");
		this.BtnPayroll.Id = 174;
		this.BtnPayroll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayroll.ImageOptions.Image");
		this.BtnPayroll.ImageOptions.ImageIndex = (int)resources.GetObject("BtnPayroll.ImageOptions.ImageIndex");
		this.BtnPayroll.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnPayroll.ImageOptions.LargeImageIndex");
		this.BtnPayroll.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPayroll.ImageOptions.SvgImage");
		this.BtnPayroll.Name = "BtnPayroll";
		this.BtnPayroll.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayroll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayroll_ItemClick);
		resources.ApplyResources(this.BtnRestraunt, "BtnRestraunt");
		this.BtnRestraunt.Id = 176;
		this.BtnRestraunt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRestraunt.ImageOptions.Image");
		this.BtnRestraunt.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRestraunt.ImageOptions.ImageIndex");
		this.BtnRestraunt.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRestraunt.ImageOptions.LargeImageIndex");
		this.BtnRestraunt.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRestraunt.ImageOptions.SvgImage");
		this.BtnRestraunt.Name = "BtnRestraunt";
		this.BtnRestraunt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRestraunt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
		resources.ApplyResources(this.btnDefualtCustomer, "btnDefualtCustomer");
		this.btnDefualtCustomer.Id = 177;
		this.btnDefualtCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnDefualtCustomer.ImageOptions.Image");
		this.btnDefualtCustomer.ImageOptions.ImageIndex = (int)resources.GetObject("btnDefualtCustomer.ImageOptions.ImageIndex");
		this.btnDefualtCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnDefualtCustomer.ImageOptions.LargeImage");
		this.btnDefualtCustomer.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnDefualtCustomer.ImageOptions.LargeImageIndex");
		this.btnDefualtCustomer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnDefualtCustomer.ImageOptions.SvgImage");
		this.btnDefualtCustomer.Name = "btnDefualtCustomer";
		this.btnDefualtCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.btnDefualtCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnDefualtCustomer_ItemClick);
		resources.ApplyResources(this.BtnItemCountry, "BtnItemCountry");
		this.BtnItemCountry.Id = 178;
		this.BtnItemCountry.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemCountry.ImageOptions.ImageIndex");
		this.BtnItemCountry.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemCountry.ImageOptions.LargeImageIndex");
		this.BtnItemCountry.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemCountry.ImageOptions.SvgImage");
		this.BtnItemCountry.Name = "BtnItemCountry";
		this.BtnItemCountry.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCountry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCountry_ItemClick);
		resources.ApplyResources(this.BtnItemGuarantee, "BtnItemGuarantee");
		this.BtnItemGuarantee.Id = 179;
		this.BtnItemGuarantee.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemGuarantee.ImageOptions.ImageIndex");
		this.BtnItemGuarantee.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemGuarantee.ImageOptions.LargeImageIndex");
		this.BtnItemGuarantee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemGuarantee.ImageOptions.SvgImage");
		this.BtnItemGuarantee.Name = "BtnItemGuarantee";
		this.BtnItemGuarantee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGuarantee_ItemClick);
		resources.ApplyResources(this.BtnORDInventorySettlementMinus, "BtnORDInventorySettlementMinus");
		this.BtnORDInventorySettlementMinus.Id = 180;
		this.BtnORDInventorySettlementMinus.ImageOptions.ImageIndex = (int)resources.GetObject("BtnORDInventorySettlementMinus.ImageOptions.ImageIndex");
		this.BtnORDInventorySettlementMinus.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnORDInventorySettlementMinus.ImageOptions.LargeImageIndex");
		this.BtnORDInventorySettlementMinus.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDInventorySettlementMinus.ImageOptions.SvgImage");
		this.BtnORDInventorySettlementMinus.Name = "BtnORDInventorySettlementMinus";
		this.BtnORDInventorySettlementMinus.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDInventorySettlementMinus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDInventorySettlementMinus_ItemClick);
		resources.ApplyResources(this.BtnRoom, "BtnRoom");
		this.BtnRoom.Id = 181;
		this.BtnRoom.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRoom.ImageOptions.ImageIndex");
		this.BtnRoom.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRoom.ImageOptions.LargeImageIndex");
		this.BtnRoom.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRoom.ImageOptions.SvgImage");
		this.BtnRoom.Name = "BtnRoom";
		this.BtnRoom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRoom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRoom_ItemClick);
		resources.ApplyResources(this.BtnTable, "BtnTable");
		this.BtnTable.Id = 182;
		this.BtnTable.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTable.ImageOptions.Image");
		this.BtnTable.ImageOptions.ImageIndex = (int)resources.GetObject("BtnTable.ImageOptions.ImageIndex");
		this.BtnTable.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnTable.ImageOptions.LargeImage");
		this.BtnTable.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnTable.ImageOptions.LargeImageIndex");
		this.BtnTable.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTable.ImageOptions.SvgImage");
		this.BtnTable.Name = "BtnTable";
		this.BtnTable.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTable_ItemClick);
		resources.ApplyResources(this.BtnDelivery, "BtnDelivery");
		this.BtnDelivery.Id = 183;
		this.BtnDelivery.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelivery.ImageOptions.Image");
		this.BtnDelivery.ImageOptions.ImageIndex = (int)resources.GetObject("BtnDelivery.ImageOptions.ImageIndex");
		this.BtnDelivery.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnDelivery.ImageOptions.LargeImage");
		this.BtnDelivery.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnDelivery.ImageOptions.LargeImageIndex");
		this.BtnDelivery.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnDelivery.ImageOptions.SvgImage");
		this.BtnDelivery.Name = "BtnDelivery";
		this.BtnDelivery.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDelivery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnDelivery_ItemClick);
		resources.ApplyResources(this.barSubItem7, "barSubItem7");
		this.barSubItem7.Id = 184;
		this.barSubItem7.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem7.ImageOptions.Image");
		this.barSubItem7.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem7.ImageOptions.ImageIndex");
		this.barSubItem7.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItem7.ImageOptions.LargeImage");
		this.barSubItem7.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem7.ImageOptions.LargeImageIndex");
		this.barSubItem7.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem7.ImageOptions.SvgImage");
		this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnMovementsDailyAllBranReport),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitAccountAllBran),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportMizanAllBran)
		});
		this.barSubItem7.Name = "barSubItem7";
		this.barSubItem7.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnMovementsDailyAllBranReport, "BtnMovementsDailyAllBranReport");
		this.BtnMovementsDailyAllBranReport.Id = 185;
		this.BtnMovementsDailyAllBranReport.ImageOptions.ImageIndex = (int)resources.GetObject("BtnMovementsDailyAllBranReport.ImageOptions.ImageIndex");
		this.BtnMovementsDailyAllBranReport.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnMovementsDailyAllBranReport.ImageOptions.LargeImageIndex");
		this.BtnMovementsDailyAllBranReport.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnMovementsDailyAllBranReport.ImageOptions.SvgImage");
		this.BtnMovementsDailyAllBranReport.Name = "BtnMovementsDailyAllBranReport";
		this.BtnMovementsDailyAllBranReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMovementsDailyAllBranReport_ItemClick);
		resources.ApplyResources(this.BtnRPProfitAccountAllBran, "BtnRPProfitAccountAllBran");
		this.BtnRPProfitAccountAllBran.Id = 186;
		this.BtnRPProfitAccountAllBran.ImageOptions.ImageIndex = (int)resources.GetObject("BtnRPProfitAccountAllBran.ImageOptions.ImageIndex");
		this.BtnRPProfitAccountAllBran.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnRPProfitAccountAllBran.ImageOptions.LargeImageIndex");
		this.BtnRPProfitAccountAllBran.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPProfitAccountAllBran.ImageOptions.SvgImage");
		this.BtnRPProfitAccountAllBran.Name = "BtnRPProfitAccountAllBran";
		this.BtnRPProfitAccountAllBran.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitAccountAllBran_ItemClick);
		resources.ApplyResources(this.BtnReportMizanAllBran, "BtnReportMizanAllBran");
		this.BtnReportMizanAllBran.Id = 187;
		this.BtnReportMizanAllBran.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportMizanAllBran.ImageOptions.ImageIndex");
		this.BtnReportMizanAllBran.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportMizanAllBran.ImageOptions.LargeImageIndex");
		this.BtnReportMizanAllBran.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportMizanAllBran.ImageOptions.SvgImage");
		this.BtnReportMizanAllBran.Name = "BtnReportMizanAllBran";
		this.BtnReportMizanAllBran.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportMizanAllBran_ItemClick);
		resources.ApplyResources(this.BtnContract, "BtnContract");
		this.BtnContract.Id = 188;
		this.BtnContract.ImageOptions.ImageIndex = (int)resources.GetObject("BtnContract.ImageOptions.ImageIndex");
		this.BtnContract.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnContract.ImageOptions.LargeImageIndex");
		this.BtnContract.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnContract.ImageOptions.SvgImage");
		this.BtnContract.Name = "BtnContract";
		this.BtnContract.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnContract_ItemClick);
		resources.ApplyResources(this.BtnGuarantee, "BtnGuarantee");
		this.BtnGuarantee.Id = 189;
		this.BtnGuarantee.ImageOptions.ImageIndex = (int)resources.GetObject("BtnGuarantee.ImageOptions.ImageIndex");
		this.BtnGuarantee.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnGuarantee.ImageOptions.LargeImageIndex");
		this.BtnGuarantee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGuarantee.ImageOptions.SvgImage");
		this.BtnGuarantee.Name = "BtnGuarantee";
		this.BtnGuarantee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGuarantee_ItemClick);
		resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
		this.barButtonItem5.Id = 190;
		this.barButtonItem5.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem5.ImageOptions.ImageIndex");
		this.barButtonItem5.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem5.ImageOptions.LargeImageIndex");
		this.barButtonItem5.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem5.ImageOptions.SvgImage");
		this.barButtonItem5.Name = "barButtonItem5";
		resources.ApplyResources(this.barSubItem8, "barSubItem8");
		this.barSubItem8.Id = 191;
		this.barSubItem8.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem8.ImageOptions.ImageIndex");
		this.barSubItem8.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem8.ImageOptions.LargeImageIndex");
		this.barSubItem8.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem8.ImageOptions.SvgImage");
		this.barSubItem8.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportContract),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportGuarantee),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSearchCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSearchContract)
		});
		this.barSubItem8.Name = "barSubItem8";
		resources.ApplyResources(this.BtnReportContract, "BtnReportContract");
		this.BtnReportContract.Id = 192;
		this.BtnReportContract.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportContract.ImageOptions.ImageIndex");
		this.BtnReportContract.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportContract.ImageOptions.LargeImageIndex");
		this.BtnReportContract.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportContract.ImageOptions.SvgImage");
		this.BtnReportContract.Name = "BtnReportContract";
		this.BtnReportContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportContract_ItemClick);
		resources.ApplyResources(this.BtnReportGuarantee, "BtnReportGuarantee");
		this.BtnReportGuarantee.Id = 193;
		this.BtnReportGuarantee.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportGuarantee.ImageOptions.ImageIndex");
		this.BtnReportGuarantee.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportGuarantee.ImageOptions.LargeImageIndex");
		this.BtnReportGuarantee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportGuarantee.ImageOptions.SvgImage");
		this.BtnReportGuarantee.Name = "BtnReportGuarantee";
		this.BtnReportGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportGuarantee_ItemClick);
		resources.ApplyResources(this.BtnReportSearchCustomer, "BtnReportSearchCustomer");
		this.BtnReportSearchCustomer.Id = 194;
		this.BtnReportSearchCustomer.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportSearchCustomer.ImageOptions.ImageIndex");
		this.BtnReportSearchCustomer.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportSearchCustomer.ImageOptions.LargeImageIndex");
		this.BtnReportSearchCustomer.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportSearchCustomer.ImageOptions.SvgImage");
		this.BtnReportSearchCustomer.Name = "BtnReportSearchCustomer";
		this.BtnReportSearchCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSearchCustomer_ItemClick);
		resources.ApplyResources(this.BtnReportSearchContract, "BtnReportSearchContract");
		this.BtnReportSearchContract.Id = 195;
		this.BtnReportSearchContract.ImageOptions.ImageIndex = (int)resources.GetObject("BtnReportSearchContract.ImageOptions.ImageIndex");
		this.BtnReportSearchContract.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnReportSearchContract.ImageOptions.LargeImageIndex");
		this.BtnReportSearchContract.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReportSearchContract.ImageOptions.SvgImage");
		this.BtnReportSearchContract.Name = "BtnReportSearchContract";
		this.BtnReportSearchContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSearchContract_ItemClick);
		resources.ApplyResources(this.BtnItemVat, "BtnItemVat");
		this.BtnItemVat.Id = 196;
		this.BtnItemVat.ImageOptions.Image = GTSErpSystem.Properties.Resources.Vat2;
		this.BtnItemVat.ImageOptions.ImageIndex = (int)resources.GetObject("BtnItemVat.ImageOptions.ImageIndex");
		this.BtnItemVat.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnItemVat.ImageOptions.LargeImageIndex");
		this.BtnItemVat.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemVat.ImageOptions.SvgImage");
		this.BtnItemVat.Name = "BtnItemVat";
		this.BtnItemVat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemVat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemVat_ItemClick);
		resources.ApplyResources(this.BtnTrnsferToBranch, "BtnTrnsferToBranch");
		this.BtnTrnsferToBranch.Enabled = false;
		this.BtnTrnsferToBranch.Id = 197;
		this.BtnTrnsferToBranch.ImageOptions.ImageIndex = (int)resources.GetObject("BtnTrnsferToBranch.ImageOptions.ImageIndex");
		this.BtnTrnsferToBranch.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnTrnsferToBranch.ImageOptions.LargeImageIndex");
		this.BtnTrnsferToBranch.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTrnsferToBranch.ImageOptions.SvgImage");
		this.BtnTrnsferToBranch.Name = "BtnTrnsferToBranch";
		this.BtnTrnsferToBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferToBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferToBranch_ItemClick);
		resources.ApplyResources(this.BtnTrnsferFromBranch, "BtnTrnsferFromBranch");
		this.BtnTrnsferFromBranch.Enabled = false;
		this.BtnTrnsferFromBranch.Id = 198;
		this.BtnTrnsferFromBranch.ImageOptions.ImageIndex = (int)resources.GetObject("BtnTrnsferFromBranch.ImageOptions.ImageIndex");
		this.BtnTrnsferFromBranch.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnTrnsferFromBranch.ImageOptions.LargeImageIndex");
		this.BtnTrnsferFromBranch.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTrnsferFromBranch.ImageOptions.SvgImage");
		this.BtnTrnsferFromBranch.Name = "BtnTrnsferFromBranch";
		this.BtnTrnsferFromBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferFromBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferFromBranch_ItemClick);
		resources.ApplyResources(this.BtnAllPrinter, "BtnAllPrinter");
		this.BtnAllPrinter.Id = 201;
		this.BtnAllPrinter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAllPrinter.ImageOptions.Image");
		this.BtnAllPrinter.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAllPrinter.ImageOptions.ImageIndex");
		this.BtnAllPrinter.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnAllPrinter.ImageOptions.LargeImage");
		this.BtnAllPrinter.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAllPrinter.ImageOptions.LargeImageIndex");
		this.BtnAllPrinter.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAllPrinter.ImageOptions.SvgImage");
		this.BtnAllPrinter.Name = "BtnAllPrinter";
		this.BtnAllPrinter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAllPrinter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAllPrinter_ItemClick);
		resources.ApplyResources(this.BtnOrderDaily, "BtnOrderDaily");
		this.BtnOrderDaily.Id = 202;
		this.BtnOrderDaily.ImageOptions.ImageIndex = (int)resources.GetObject("BtnOrderDaily.ImageOptions.ImageIndex");
		this.BtnOrderDaily.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnOrderDaily.ImageOptions.LargeImageIndex");
		this.BtnOrderDaily.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnOrderDaily.ImageOptions.SvgImage");
		this.BtnOrderDaily.Name = "BtnOrderDaily";
		resources.ApplyResources(this.BtnAllowances, "BtnAllowances");
		this.BtnAllowances.Id = 204;
		this.BtnAllowances.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAllowances.ImageOptions.ImageIndex");
		this.BtnAllowances.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAllowances.ImageOptions.LargeImageIndex");
		this.BtnAllowances.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAllowances.ImageOptions.SvgImage");
		this.BtnAllowances.Name = "BtnAllowances";
		this.BtnAllowances.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAllowances.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAllowances_ItemClick);
		resources.ApplyResources(this.BtnAdd, "BtnAdd");
		this.BtnAdd.Id = 207;
		this.BtnAdd.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAdd.ImageOptions.Image");
		this.BtnAdd.ImageOptions.ImageIndex = (int)resources.GetObject("BtnAdd.ImageOptions.ImageIndex");
		this.BtnAdd.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnAdd.ImageOptions.LargeImage");
		this.BtnAdd.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnAdd.ImageOptions.LargeImageIndex");
		this.BtnAdd.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAdd.ImageOptions.SvgImage");
		this.BtnAdd.Name = "BtnAdd";
		this.BtnAdd.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAdd_ItemClick);
		resources.ApplyResources(this.BtnBounce2, "BtnBounce2");
		this.BtnBounce2.Id = 208;
		this.BtnBounce2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnBounce2.ImageOptions.Image");
		this.BtnBounce2.ImageOptions.ImageIndex = (int)resources.GetObject("BtnBounce2.ImageOptions.ImageIndex");
		this.BtnBounce2.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnBounce2.ImageOptions.LargeImageIndex");
		this.BtnBounce2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnBounce2.ImageOptions.SvgImage");
		this.BtnBounce2.Name = "BtnBounce2";
		this.BtnBounce2.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBounce2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBounce2_ItemClick);
		resources.ApplyResources(this.BtnGenetareCSID, "BtnGenetareCSID");
		this.BtnGenetareCSID.Id = 209;
		this.BtnGenetareCSID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGenetareCSID.ImageOptions.Image");
		this.BtnGenetareCSID.ImageOptions.ImageIndex = (int)resources.GetObject("BtnGenetareCSID.ImageOptions.ImageIndex");
		this.BtnGenetareCSID.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnGenetareCSID.ImageOptions.LargeImage");
		this.BtnGenetareCSID.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnGenetareCSID.ImageOptions.LargeImageIndex");
		this.BtnGenetareCSID.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGenetareCSID.ImageOptions.SvgImage");
		this.BtnGenetareCSID.Name = "BtnGenetareCSID";
		this.BtnGenetareCSID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGenetareCSID_ItemClick);
		resources.ApplyResources(this.RElctornc2112, "RElctornc2112");
		this.RElctornc2112.Id = 210;
		this.RElctornc2112.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("RElctornc2112.ImageOptions.Image");
		this.RElctornc2112.ImageOptions.ImageIndex = (int)resources.GetObject("RElctornc2112.ImageOptions.ImageIndex");
		this.RElctornc2112.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("RElctornc2112.ImageOptions.LargeImage");
		this.RElctornc2112.ImageOptions.LargeImageIndex = (int)resources.GetObject("RElctornc2112.ImageOptions.LargeImageIndex");
		this.RElctornc2112.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("RElctornc2112.ImageOptions.SvgImage");
		this.RElctornc2112.Name = "RElctornc2112";
		this.RElctornc2112.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick_1);
		resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
		this.barButtonItem4.Id = 212;
		this.barButtonItem4.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem4.ImageOptions.Image");
		this.barButtonItem4.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem4.ImageOptions.ImageIndex");
		this.barButtonItem4.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem4.ImageOptions.LargeImage");
		this.barButtonItem4.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem4.ImageOptions.LargeImageIndex");
		this.barButtonItem4.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem4.ImageOptions.SvgImage");
		this.barButtonItem4.Name = "barButtonItem4";
		this.barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.barButtonItem4.VisibleInSearchMenu = false;
		this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem4_ItemClick);
		this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[5] { this.ribbonPageGroupNavigation, this.ribbonPageGroup9, this.ribbonPageGroup10, this.ribbonPageGroup23, this.ribbonPageGroup22 });
		this.ribbonPage.Name = "ribbonPage";
		resources.ApplyResources(this.ribbonPage, "ribbonPage");
		this.ribbonPageGroupNavigation.AllowTextClipping = false;
		this.ribbonPageGroupNavigation.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemCompany);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemGroups);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemUnits);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemClass);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemCountry);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemGuarantee);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItem);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnVat);
		this.ribbonPageGroupNavigation.Name = "ribbonPageGroupNavigation";
		resources.ApplyResources(this.ribbonPageGroupNavigation, "ribbonPageGroupNavigation");
		this.ribbonPageGroup9.AllowTextClipping = false;
		this.ribbonPageGroup9.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnBranches);
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnStores);
		this.ribbonPageGroup9.Name = "ribbonPageGroup9";
		resources.ApplyResources(this.ribbonPageGroup9, "ribbonPageGroup9");
		this.ribbonPageGroup10.AllowTextClipping = false;
		this.ribbonPageGroup10.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusPlace);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSalesMan);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCustomer);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSupplier);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnBounce2);
		this.ribbonPageGroup10.Name = "ribbonPageGroup10";
		resources.ApplyResources(this.ribbonPageGroup10, "ribbonPageGroup10");
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnRoom);
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnTable);
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnDelivery);
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnAdd);
		this.ribbonPageGroup23.Name = "ribbonPageGroup23";
		resources.ApplyResources(this.ribbonPageGroup23, "ribbonPageGroup23");
		this.ribbonPageGroup22.AllowTextClipping = false;
		this.ribbonPageGroup22.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup22.ItemLinks.Add(this.BtnLock, true);
		this.ribbonPageGroup22.Name = "ribbonPageGroup22";
		resources.ApplyResources(this.ribbonPageGroup22, "ribbonPageGroup22");
		this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[5] { this.ribbonPageGroup20, this.ribbonPageGroup21, this.ribbonPageGroup1, this.ribbonPageGroup7, this.ribbonPageGroup8 });
		this.ribbonPage1.Name = "ribbonPage1";
		resources.ApplyResources(this.ribbonPage1, "ribbonPage1");
		this.ribbonPageGroup20.AllowTextClipping = false;
		this.ribbonPageGroup20.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDOpenQuantity);
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDPriceOffer);
		this.ribbonPageGroup20.ItemLinks.Add(this.BtnORDPurchaseOrder);
		this.ribbonPageGroup20.Name = "ribbonPageGroup20";
		resources.ApplyResources(this.ribbonPageGroup20, "ribbonPageGroup20");
		this.ribbonPageGroup21.AllowTextClipping = false;
		this.ribbonPageGroup21.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnCashir);
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnRestraunt);
		this.ribbonPageGroup21.Name = "ribbonPageGroup21";
		resources.ApplyResources(this.ribbonPageGroup21, "ribbonPageGroup21");
		this.ribbonPageGroup1.AllowTextClipping = false;
		this.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPurchase);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnCheckOutOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDInventorySettlementMinus);
		this.ribbonPageGroup1.Name = "ribbonPageGroup1";
		resources.ApplyResources(this.ribbonPageGroup1, "ribbonPageGroup1");
		this.ribbonPageGroup7.AllowTextClipping = false;
		this.ribbonPageGroup7.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDOrderReturnID);
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDPurchaseReturnID);
		this.ribbonPageGroup7.Name = "ribbonPageGroup7";
		resources.ApplyResources(this.ribbonPageGroup7, "ribbonPageGroup7");
		this.ribbonPageGroup8.AllowTextClipping = false;
		this.ribbonPageGroup8.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDOrderReturnNotID);
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDPurchaseReturnNotID);
		this.ribbonPageGroup8.Name = "ribbonPageGroup8";
		resources.ApplyResources(this.ribbonPageGroup8, "ribbonPageGroup8");
		this.ribbonPage8.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup27, this.ribbonPageGroup28 });
		this.ribbonPage8.Name = "ribbonPage8";
		resources.ApplyResources(this.ribbonPage8, "ribbonPage8");
		this.ribbonPageGroup27.Enabled = false;
		this.ribbonPageGroup27.ItemLinks.Add(this.BtnTrnsferToBranch);
		this.ribbonPageGroup27.ItemLinks.Add(this.BtnTrnsferFromBranch);
		this.ribbonPageGroup27.Name = "ribbonPageGroup27";
		resources.ApplyResources(this.ribbonPageGroup27, "ribbonPageGroup27");
		this.ribbonPageGroup28.ItemLinks.Add(this.BtnTransfer);
		this.ribbonPageGroup28.Name = "ribbonPageGroup28";
		resources.ApplyResources(this.ribbonPageGroup28, "ribbonPageGroup28");
		this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup3 });
		this.ribbonPage3.Name = "ribbonPage3";
		resources.ApplyResources(this.ribbonPage3, "ribbonPage3");
		this.ribbonPageGroup3.AllowTextClipping = false;
		this.ribbonPageGroup3.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnAllowances);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnEmployee);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayrollTest);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayroll);
		this.ribbonPageGroup3.Name = "ribbonPageGroup3";
		this.ribbonPage7.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup25, this.ribbonPageGroup26 });
		this.ribbonPage7.Name = "ribbonPage7";
		resources.ApplyResources(this.ribbonPage7, "ribbonPage7");
		this.ribbonPage7.Visible = false;
		this.ribbonPageGroup25.AllowTextClipping = false;
		this.ribbonPageGroup25.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup25.ItemLinks.Add(this.BtnContract);
		this.ribbonPageGroup25.ItemLinks.Add(this.BtnGuarantee);
		this.ribbonPageGroup25.Name = "ribbonPageGroup25";
		resources.ApplyResources(this.ribbonPageGroup25, "ribbonPageGroup25");
		this.ribbonPageGroup26.AllowTextClipping = false;
		this.ribbonPageGroup26.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup26.ItemLinks.Add(this.barSubItem8);
		this.ribbonPageGroup26.Name = "ribbonPageGroup26";
		resources.ApplyResources(this.ribbonPageGroup26, "ribbonPageGroup26");
		this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup11, this.ribbonPageGroup2 });
		this.ribbonPage2.Name = "ribbonPage2";
		resources.ApplyResources(this.ribbonPage2, "ribbonPage2");
		this.ribbonPageGroup11.AllowTextClipping = false;
		this.ribbonPageGroup11.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCardAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccTreeAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictions);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictionsTest);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccOpenMoney);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCostCenter);
		this.ribbonPageGroup11.Name = "ribbonPageGroup11";
		resources.ApplyResources(this.ribbonPageGroup11, "ribbonPageGroup11");
		this.ribbonPageGroup2.AllowTextClipping = false;
		this.ribbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnRecipt);
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnPayment);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem7);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem8);
		this.ribbonPageGroup2.Name = "ribbonPageGroup2";
		resources.ApplyResources(this.ribbonPageGroup2, "ribbonPageGroup2");
		this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup4 });
		this.ribbonPage4.Name = "ribbonPage4";
		resources.ApplyResources(this.ribbonPage4, "ribbonPage4");
		this.ribbonPageGroup4.AllowTextClipping = false;
		this.ribbonPageGroup4.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnUsers);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnGroups);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnPermission);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnEncriptPassword);
		this.ribbonPageGroup4.Name = "ribbonPageGroup4";
		resources.ApplyResources(this.ribbonPageGroup4, "ribbonPageGroup4");
		this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[8] { this.ribbonPageGroup5, this.ribbonPageGroup19, this.ribbonPageGroup12, this.ribbonPageGroup13, this.ribbonPageGroup14, this.ribbonPageGroup15, this.ribbonPageGroup16, this.ribbonPageGroup24 });
		this.ribbonPage5.Name = "ribbonPage5";
		resources.ApplyResources(this.ribbonPage5, "ribbonPage5");
		this.ribbonPageGroup5.AllowTextClipping = false;
		this.ribbonPageGroup5.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup5.ItemLinks.Add(this.BtnReportPublic);
		this.ribbonPageGroup5.Name = "ribbonPageGroup5";
		resources.ApplyResources(this.ribbonPageGroup5, "ribbonPageGroup5");
		this.ribbonPageGroup19.AllowTextClipping = false;
		this.ribbonPageGroup19.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup19.ItemLinks.Add(this.barSubItem6);
		this.ribbonPageGroup19.Name = "ribbonPageGroup19";
		resources.ApplyResources(this.ribbonPageGroup19, "ribbonPageGroup19");
		this.ribbonPageGroup12.AllowTextClipping = false;
		this.ribbonPageGroup12.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup12.ItemLinks.Add(this.BtnReportStore);
		this.ribbonPageGroup12.Name = "ribbonPageGroup12";
		resources.ApplyResources(this.ribbonPageGroup12, "ribbonPageGroup12");
		this.ribbonPageGroup13.AllowTextClipping = false;
		this.ribbonPageGroup13.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup13.ItemLinks.Add(this.BtnReportPurshes);
		this.ribbonPageGroup13.Name = "ribbonPageGroup13";
		resources.ApplyResources(this.ribbonPageGroup13, "ribbonPageGroup13");
		this.ribbonPageGroup14.AllowTextClipping = false;
		this.ribbonPageGroup14.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup14.ItemLinks.Add(this.BtnReportOrder);
		this.ribbonPageGroup14.Name = "ribbonPageGroup14";
		resources.ApplyResources(this.ribbonPageGroup14, "ribbonPageGroup14");
		this.ribbonPageGroup15.AllowTextClipping = false;
		this.ribbonPageGroup15.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup15.ItemLinks.Add(this.BtnReportProfit);
		this.ribbonPageGroup15.Name = "ribbonPageGroup15";
		resources.ApplyResources(this.ribbonPageGroup15, "ribbonPageGroup15");
		this.ribbonPageGroup16.AllowTextClipping = false;
		this.ribbonPageGroup16.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup16.ItemLinks.Add(this.BtnReportAccount);
		this.ribbonPageGroup16.Name = "ribbonPageGroup16";
		resources.ApplyResources(this.ribbonPageGroup16, "ribbonPageGroup16");
		this.ribbonPageGroup16.Visible = false;
		this.ribbonPageGroup24.ItemLinks.Add(this.barSubItem7);
		this.ribbonPageGroup24.Name = "ribbonPageGroup24";
		resources.ApplyResources(this.ribbonPageGroup24, "ribbonPageGroup24");
		this.ribbonPageGroup24.Visible = false;
		this.RElctornc.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup18 });
		this.RElctornc.Name = "RElctornc";
		resources.ApplyResources(this.RElctornc, "RElctornc");
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnGenetareCSID);
		this.ribbonPageGroup18.ItemLinks.Add(this.RElctornc2112);
		this.ribbonPageGroup18.Name = "ribbonPageGroup18";
		resources.ApplyResources(this.ribbonPageGroup18, "ribbonPageGroup18");
		this.ribbonPage6.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup6, this.ribbonPageGroup17 });
		this.ribbonPage6.Name = "ribbonPage6";
		resources.ApplyResources(this.ribbonPage6, "ribbonPage6");
		this.ribbonPageGroup6.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnPrinters);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnAllPrinter);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnSetting);
		this.ribbonPageGroup6.ItemLinks.Add(this.btnDefualtAccount);
		this.ribbonPageGroup6.ItemLinks.Add(this.btnDefualtCustomer);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnConnection);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnDabase);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnItemVat);
		this.ribbonPageGroup6.ItemLinks.Add(this.barButtonItem4);
		this.ribbonPageGroup6.Name = "ribbonPageGroup6";
		resources.ApplyResources(this.ribbonPageGroup6, "ribbonPageGroup6");
		this.ribbonPageGroup17.AllowTextClipping = false;
		this.ribbonPageGroup17.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnAbout);
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnReportDesign);
		this.ribbonPageGroup17.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
		this.ribbonPageGroup17.Name = "ribbonPageGroup17";
		resources.ApplyResources(this.ribbonPageGroup17, "ribbonPageGroup17");
		resources.ApplyResources(this.repositoryItemColorEdit1, "repositoryItemColorEdit1");
		this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemColorEdit1.Buttons"))
		});
		this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
		resources.ApplyResources(this.repositoryItemColorPickEdit1, "repositoryItemColorPickEdit1");
		this.repositoryItemColorPickEdit1.AutomaticColor = System.Drawing.Color.Black;
		this.repositoryItemColorPickEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemColorPickEdit1.Buttons"))
		});
		this.repositoryItemColorPickEdit1.Name = "repositoryItemColorPickEdit1";
		resources.ApplyResources(this.repositoryItemColorEdit2, "repositoryItemColorEdit2");
		this.repositoryItemColorEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemColorEdit2.Buttons"))
		});
		this.repositoryItemColorEdit2.Name = "repositoryItemColorEdit2";
		resources.ApplyResources(this.repositoryItemDateEdit1, "repositoryItemDateEdit1");
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.Buttons"))
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		resources.ApplyResources(this.repositoryItemTimeSpanEdit1, "repositoryItemTimeSpanEdit1");
		this.repositoryItemTimeSpanEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemTimeSpanEdit1.Buttons"))
		});
		this.repositoryItemTimeSpanEdit1.Mask.EditMask = resources.GetString("repositoryItemTimeSpanEdit1.Mask.EditMask");
		this.repositoryItemTimeSpanEdit1.Name = "repositoryItemTimeSpanEdit1";
		resources.ApplyResources(this.repositoryItemTimeEdit1, "repositoryItemTimeEdit1");
		this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemTimeEdit1.Buttons"))
		});
		this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
		resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
		this.ribbonStatusBar.ItemLinks.Add(this.LblCompany);
		this.ribbonStatusBar.ItemLinks.Add(this.LblVerg);
		this.ribbonStatusBar.ItemLinks.Add(this.LblServer);
		this.ribbonStatusBar.ItemLinks.Add(this.LblUser);
		this.ribbonStatusBar.ItemLinks.Add(this.LblTime);
		this.ribbonStatusBar.ItemLinks.Add(this.LblDate);
		this.ribbonStatusBar.Name = "ribbonStatusBar";
		this.ribbonStatusBar.Ribbon = this.ribbonControl;
		resources.ApplyResources(this.navigationFrame, "navigationFrame");
		this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
		this.navigationFrame.Appearance.Options.UseBackColor = true;
		this.navigationFrame.Controls.Add(this.employeesNavigationPage);
		this.navigationFrame.Name = "navigationFrame";
		this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[1] { this.employeesNavigationPage });
		this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
		this.navigationFrame.SelectedPage = this.employeesNavigationPage;
		resources.ApplyResources(this.employeesNavigationPage, "employeesNavigationPage");
		this.employeesNavigationPage.BackgroundImage = GTSErpSystem.Properties.Resources.M2restaurant;
		this.employeesNavigationPage.Name = "employeesNavigationPage";
		this.ribbonPageGroup.AllowTextClipping = false;
		this.ribbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem);
		this.ribbonPageGroup.Name = "ribbonPageGroup";
		resources.ApplyResources(this.ribbonPageGroup, "ribbonPageGroup");
		resources.ApplyResources(this.barSubItem1, "barSubItem1");
		this.barSubItem1.Id = 15;
		this.barSubItem1.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem1.ImageOptions.ImageIndex");
		this.barSubItem1.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem1.ImageOptions.LargeImageIndex");
		this.barSubItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem1.ImageOptions.SvgImage");
		this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem1.Name = "barSubItem1";
		resources.ApplyResources(this.barSubItem2, "barSubItem2");
		this.barSubItem2.Id = 15;
		this.barSubItem2.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem2.ImageOptions.ImageIndex");
		this.barSubItem2.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem2.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem2.ImageOptions.LargeImageIndex");
		this.barSubItem2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem2.ImageOptions.SvgImage");
		this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem2.Name = "barSubItem2";
		resources.ApplyResources(this.barSubItem3, "barSubItem3");
		this.barSubItem3.Id = 15;
		this.barSubItem3.ImageOptions.ImageIndex = (int)resources.GetObject("barSubItem3.ImageOptions.ImageIndex");
		this.barSubItem3.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem3.ImageOptions.LargeImageIndex = (int)resources.GetObject("barSubItem3.ImageOptions.LargeImageIndex");
		this.barSubItem3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem3.ImageOptions.SvgImage");
		this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem3.Name = "barSubItem3";
		this.timer1.Enabled = true;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this.BtnDataBase, "BtnDataBase");
		this.BtnDataBase.Id = 148;
		this.BtnDataBase.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDataBase.ImageOptions.Image");
		this.BtnDataBase.ImageOptions.ImageIndex = (int)resources.GetObject("BtnDataBase.ImageOptions.ImageIndex");
		this.BtnDataBase.ImageOptions.LargeImageIndex = (int)resources.GetObject("BtnDataBase.ImageOptions.LargeImageIndex");
		this.BtnDataBase.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnDataBase.ImageOptions.SvgImage");
		this.BtnDataBase.Name = "BtnDataBase";
		this.BtnDataBase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem17, "barButtonItem17");
		this.barButtonItem17.Id = 215;
		this.barButtonItem17.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem17.ImageOptions.Image");
		this.barButtonItem17.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem17.ImageOptions.ImageIndex");
		this.barButtonItem17.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem17.ImageOptions.LargeImage");
		this.barButtonItem17.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem17.ImageOptions.LargeImageIndex");
		this.barButtonItem17.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem17.ImageOptions.SvgImage");
		this.barButtonItem17.Name = "barButtonItem17";
		this.barButtonItem17.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem17_ItemClick);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.navigationFrame);
		base.Controls.Add(this.ribbonStatusBar);
		base.Controls.Add(this.ribbonControl);
		base.IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FrmRestaurantSilver.IconOptions.Icon");
		base.Name = "FrmRestaurantSilver";
		this.Ribbon = this.ribbonControl;
		this.StatusBar = this.ribbonStatusBar;
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
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


