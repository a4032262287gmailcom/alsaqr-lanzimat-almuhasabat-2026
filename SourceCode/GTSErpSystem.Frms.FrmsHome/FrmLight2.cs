using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
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
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.Orders;
using GTSErpSystem.Frms.Reports.Account;
using GTSErpSystem.Frms.Reports.Orders;
using GTSErpSystem.Frms.Reports.Profit;
using GTSErpSystem.Frms.Reports.Public;
using GTSErpSystem.Frms.Reports.Purches;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Frms.Security;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.FrmsHome;

public class FrmLight2 : RibbonForm
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

	private NavBarControl navBarControl;

	private NavBarGroup employeesNavBarGroup;

	private NavigationPage employeesNavigationPage;

	private LabelControl employeesLabelControl;

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

	private TileControl tileControl1;

	private TileGroup tileGroup2;

	private TileItem TilPurches;

	private TileItem TilOfferPrice;

	private TileItem TilItems;

	private TileItem TilCustomer;

	private TileItem TileUser;

	private TileItem TilePermission;

	private TileGroup tileGroup3;

	private TileItem TileLeadger;

	private TileItem TileReceipt;

	private TileItem TileOrderReturn;

	private TileItem TilPurchesReturn;

	private TileItem TilePayment;

	private TileItem TileOrder;

	private TileItem TilPurchesOrder;

	private TileItem TilAccountQuid;

	private TileItem TilAccountDefualt;

	private TileItem TilSupplier;

	private TileItem TileSetting;

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

	private NavBarItem navBarItem1;

	private TileItem TilStoresTransfer;

	private NavBarItem navBarItem2;

	private TileItem TileDesignReport;

	private TileItem TilExit;

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

	public FrmLight2()
	{
		InitializeComponent();
		UserLookAndFeel.Default.SkinName = "Office 2013";
		UserLookAndFeel.Default.SkinMaskColor = Color.Aqua;
		LblCompany.Caption = "برنامج مايكروسمارت للمحاسبة ونقاط البيع مرخص الي : " + LoginDetails.CompanyNameAR;
		LblServer.Caption = "السيرفر: " + LoginDetails.nameServer + " / " + LoginDetails.databaseServer;
		LblUser.Caption = "الموظف: " + LoginDetails.UserName;
		LblVerg.Caption = "اصدار:  V.19.08.10 ";
		SetDateApp();
	}

	private void navBarControl_ActiveGroupChanged(object sender, NavBarGroupEventArgs e)
	{
		navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
	}

	private void Blue()
	{
		TilPurches.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilPurches.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilPurches.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilOfferPrice.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilOfferPrice.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilOfferPrice.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilItems.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilItems.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilItems.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilCustomer.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilCustomer.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilCustomer.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileUser.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileUser.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileUser.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilePermission.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilePermission.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilePermission.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileLeadger.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileLeadger.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileLeadger.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileReceipt.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileReceipt.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileReceipt.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilStoresTransfer.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilStoresTransfer.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilStoresTransfer.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileOrderReturn.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileOrderReturn.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileOrderReturn.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilPurchesReturn.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilPurchesReturn.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilPurchesReturn.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilePayment.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilePayment.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilePayment.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileOrder.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileOrder.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileOrder.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilPurchesOrder.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilPurchesOrder.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilPurchesOrder.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilAccountQuid.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilAccountQuid.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilAccountQuid.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilAccountDefualt.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilAccountDefualt.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilAccountDefualt.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TilSupplier.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TilSupplier.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TilSupplier.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
		TileSetting.AppearanceItem.Normal.BackColor = Color.SpringGreen;
		TileSetting.AppearanceItem.Normal.BackColor2 = Color.SpringGreen;
		TileSetting.AppearanceItem.Normal.BorderColor = Color.SpringGreen;
	}

	private void Brown()
	{
		TilPurches.AppearanceItem.Normal.BackColor = Color.Brown;
		TilPurches.AppearanceItem.Normal.BackColor2 = Color.Brown;
		TilPurches.AppearanceItem.Normal.BorderColor = Color.Brown;
		TilOfferPrice.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilOfferPrice.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilOfferPrice.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilItems.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilItems.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilItems.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilCustomer.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilCustomer.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilCustomer.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileUser.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileUser.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileUser.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilePermission.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilePermission.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilePermission.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileLeadger.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileLeadger.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileLeadger.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileReceipt.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileReceipt.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileReceipt.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilStoresTransfer.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilStoresTransfer.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilStoresTransfer.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileOrderReturn.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileOrderReturn.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileOrderReturn.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilPurchesReturn.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilPurchesReturn.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilPurchesReturn.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilePayment.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilePayment.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilePayment.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileOrder.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileOrder.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileOrder.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilPurchesOrder.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilPurchesOrder.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilPurchesOrder.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilAccountQuid.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilAccountQuid.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilAccountQuid.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilAccountDefualt.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilAccountDefualt.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilAccountDefualt.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TilSupplier.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TilSupplier.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TilSupplier.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
		TileSetting.AppearanceItem.Normal.BackColor = Color.RoyalBlue;
		TileSetting.AppearanceItem.Normal.BackColor2 = Color.RoyalBlue;
		TileSetting.AppearanceItem.Normal.BorderColor = Color.RoyalBlue;
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
		switch (MessageBox.Show("هل تريد الخروج من برنامج مايكرو سمارت للمحاسبة ونقاط البيع", "خروج من البرنامج", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
		{
		case DialogResult.Yes:
			Application.Exit();
			break;
		}
	}

	private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		Application.Exit();
		if (LoginDetails.IsBackupWhenClose)
		{
			int num = STTG.TakeBackup();
			if (num > 0)
			{
				MessageBox.Show("شكرا\u064b لك \r\n تم أخذ نسخة احتياطية بنجاح \r\n سوف يتم اغلاق البرنامج الان نستودعك الله الذي لاتضيع ودائعه", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
			else
			{
				MessageBox.Show("نعتذر، تم اكتشاف خطأ \r\n يجب التأكد من مسار مجلد النسخ الاحتياطي ثم حاول مرة أخري", "النسخ الاحتياطي", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsHome.FrmLight2));
		DevExpress.XtraEditors.TileItemElement tileItemElement = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame2 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame3 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame4 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame5 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement9 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame6 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement10 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement11 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame7 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement12 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement13 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame8 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement14 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement15 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame9 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement16 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement17 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame10 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement18 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement19 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame11 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement20 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement21 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame12 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement22 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame13 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement23 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement24 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame14 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement25 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame15 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement26 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement27 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame16 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement28 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement29 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame17 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement30 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement31 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame18 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement32 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement33 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame19 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement34 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement35 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame20 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement36 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame21 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement37 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement38 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame22 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement39 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame23 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement40 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement41 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame24 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement42 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame25 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement43 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement44 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame26 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement45 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame27 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement46 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemElement tileItemElement47 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame28 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement48 = new DevExpress.XtraEditors.TileItemElement();
		DevExpress.XtraEditors.TileItemFrame tileItemFrame29 = new DevExpress.XtraEditors.TileItemFrame();
		DevExpress.XtraEditors.TileItemElement tileItemElement49 = new DevExpress.XtraEditors.TileItemElement();
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
		this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroupNavigation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup18 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
		this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
		this.employeesNavBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
		this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
		this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
		this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
		this.employeesNavigationPage = new DevExpress.XtraBars.Navigation.NavigationPage();
		this.tileControl1 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
		this.TilPurches = new DevExpress.XtraEditors.TileItem();
		this.TilOfferPrice = new DevExpress.XtraEditors.TileItem();
		this.TilPurchesOrder = new DevExpress.XtraEditors.TileItem();
		this.TilStoresTransfer = new DevExpress.XtraEditors.TileItem();
		this.TilItems = new DevExpress.XtraEditors.TileItem();
		this.TilPurchesReturn = new DevExpress.XtraEditors.TileItem();
		this.TileOrderReturn = new DevExpress.XtraEditors.TileItem();
		this.TileReceipt = new DevExpress.XtraEditors.TileItem();
		this.TilePayment = new DevExpress.XtraEditors.TileItem();
		this.TileLeadger = new DevExpress.XtraEditors.TileItem();
		this.TileOrder = new DevExpress.XtraEditors.TileItem();
		this.TilCustomer = new DevExpress.XtraEditors.TileItem();
		this.TilSupplier = new DevExpress.XtraEditors.TileItem();
		this.TileUser = new DevExpress.XtraEditors.TileItem();
		this.TilePermission = new DevExpress.XtraEditors.TileItem();
		this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
		this.TilAccountQuid = new DevExpress.XtraEditors.TileItem();
		this.TilAccountDefualt = new DevExpress.XtraEditors.TileItem();
		this.TileSetting = new DevExpress.XtraEditors.TileItem();
		this.TileDesignReport = new DevExpress.XtraEditors.TileItem();
		this.TilExit = new DevExpress.XtraEditors.TileItem();
		this.employeesLabelControl = new DevExpress.XtraEditors.LabelControl();
		this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
		this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.BtnDataBase = new DevExpress.XtraBars.BarSubItem();
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
		((System.ComponentModel.ISupportInitialize)this.navBarControl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.navigationFrame).BeginInit();
		this.navigationFrame.SuspendLayout();
		this.employeesNavigationPage.SuspendLayout();
		base.SuspendLayout();
		this.tabbedView.RootContainer.Element = null;
		this.ribbonControl.ApplicationButtonDropDownControl = this.applicationMenu1;
		this.ribbonControl.ApplicationIcon = GTSErpSystem.Properties.Resources.rsz_infographic;
		this.ribbonControl.ExpandCollapseItem.Id = 0;
		this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[115]
		{
			this.ribbonControl.ExpandCollapseItem,
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
			this.BtnReportAllMovementDays
		});
		this.ribbonControl.Location = new System.Drawing.Point(0, 0);
		this.ribbonControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.ribbonControl.MaxItemId = 166;
		this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
		this.ribbonControl.Name = "ribbonControl";
		this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[7] { this.ribbonPage, this.ribbonPage1, this.ribbonPage2, this.ribbonPage3, this.ribbonPage4, this.ribbonPage5, this.ribbonPage6 });
		this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemColorEdit1, this.repositoryItemColorPickEdit1, this.repositoryItemColorEdit2, this.repositoryItemDateEdit1, this.repositoryItemDateEdit2, this.repositoryItemDateEdit3, this.repositoryItemTimeSpanEdit1, this.repositoryItemTimeEdit1, this.repositoryItemDateEdit4 });
		this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
		this.ribbonControl.Size = new System.Drawing.Size(1173, 179);
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
		this.skinRibbonGalleryBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
		this.BtnItemGroups.Caption = "مجموعة الاصناف";
		this.BtnItemGroups.Id = 66;
		this.BtnItemGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.group_32x32;
		this.BtnItemGroups.Name = "BtnItemGroups";
		this.BtnItemGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGroups_ItemClick);
		this.BtnItemUnits.Caption = "وحدات الاصناف";
		this.BtnItemUnits.Id = 67;
		this.BtnItemUnits.ImageOptions.Image = GTSErpSystem.Properties.Resources.unmergecells_32x32;
		this.BtnItemUnits.Name = "BtnItemUnits";
		this.BtnItemUnits.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemUnits.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemUnits_ItemClick);
		this.BtnItemClass.Caption = "فئات الاصناف";
		this.BtnItemClass.Id = 68;
		this.BtnItemClass.ImageOptions.Image = GTSErpSystem.Properties.Resources.expandcollapse_32x32;
		this.BtnItemClass.Name = "BtnItemClass";
		this.BtnItemClass.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemClass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemClass_ItemClick);
		this.BtnItemCompany.Caption = "الشركات المصنعة";
		this.BtnItemCompany.Id = 69;
		this.BtnItemCompany.ImageOptions.Image = GTSErpSystem.Properties.Resources.home_32x32;
		this.BtnItemCompany.Name = "BtnItemCompany";
		this.BtnItemCompany.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCompany.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCompany_ItemClick);
		this.BtnItem.Caption = "كرت الصنف";
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
		this.BtnAccCostCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCostCenter_ItemClick);
		this.BtnAccOpenMoney.Caption = "رصيد افتتاحي حسابات";
		this.BtnAccOpenMoney.Id = 91;
		this.BtnAccOpenMoney.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccOpenMoney.ImageOptions.Image");
		this.BtnAccOpenMoney.Name = "BtnAccOpenMoney";
		this.BtnAccOpenMoney.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
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
		this.BtnReportAccount.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[6]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitAccount),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanGroups),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPTranTeacher)
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
		this.BtnRPMizanTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanTotal_ItemClick);
		this.BtnRPMizanGroups.Caption = "تقرير ميزان مجموعة حسابات";
		this.BtnRPMizanGroups.Id = 150;
		this.BtnRPMizanGroups.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.Image");
		this.BtnRPMizanGroups.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.LargeImage");
		this.BtnRPMizanGroups.Name = "BtnRPMizanGroups";
		this.BtnRPMizanGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanGroups_ItemClick);
		this.BtnRPTranTeacher.Caption = "تقرير دفتر الاستاذ ";
		this.BtnRPTranTeacher.Id = 162;
		this.BtnRPTranTeacher.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.Image");
		this.BtnRPTranTeacher.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.LargeImage");
		this.BtnRPTranTeacher.Name = "BtnRPTranTeacher";
		this.BtnRPTranTeacher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTranTeacher_ItemClick);
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
		this.LblUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblUser.Caption = "LblUser";
		this.LblUser.Id = 104;
		this.LblUser.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 10f);
		this.LblUser.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblUser.Name = "LblUser";
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
		this.BtnReportPublic.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
		{
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnMovementsDailyReport, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllMovementDays),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportVAT)
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
		this.BtnEmptyDB.Caption = "مسح جميع حركات قاعدة البيانات";
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
		this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[4] { this.ribbonPageGroupNavigation, this.ribbonPageGroup9, this.ribbonPageGroup10, this.ribbonPageGroup18 });
		this.ribbonPage.Name = "ribbonPage";
		this.ribbonPage.Text = "قسم المدخلات الاساسية";
		this.ribbonPageGroupNavigation.AllowTextClipping = false;
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemCompany);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemGroups);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemUnits);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItemClass);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnItem);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnVat);
		this.ribbonPageGroupNavigation.Name = "ribbonPageGroupNavigation";
		this.ribbonPageGroupNavigation.ShowCaptionButton = false;
		this.ribbonPageGroupNavigation.Text = "البيانات الرئيسية";
		this.ribbonPageGroup9.AllowTextClipping = false;
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnBranches);
		this.ribbonPageGroup9.ItemLinks.Add(this.BtnStores);
		this.ribbonPageGroup9.Name = "ribbonPageGroup9";
		this.ribbonPageGroup9.ShowCaptionButton = false;
		this.ribbonPageGroup9.Text = "الفروع والمخازن";
		this.ribbonPageGroup10.AllowTextClipping = false;
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusPlace);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSalesMan);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCustomer);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnCusSupplier);
		this.ribbonPageGroup10.Name = "ribbonPageGroup10";
		this.ribbonPageGroup10.ShowCaptionButton = false;
		this.ribbonPageGroup10.Text = "بيانات العملاء والموردين";
		this.ribbonPageGroup18.AllowTextClipping = false;
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnDateApp);
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnUpdateDateApp);
		this.ribbonPageGroup18.Name = "ribbonPageGroup18";
		this.ribbonPageGroup18.ShowCaptionButton = false;
		this.ribbonPageGroup18.Text = "اعداد تاريخ الكاشير";
		this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[3] { this.ribbonPageGroup1, this.ribbonPageGroup7, this.ribbonPageGroup8 });
		this.ribbonPage1.Name = "ribbonPage1";
		this.ribbonPage1.Text = "قسم الفواتير وحركات الاصناف";
		this.ribbonPageGroup1.AllowTextClipping = false;
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDOpenQuantity);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPriceOffer);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPurchaseOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPurchase);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnCashir);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnCheckOutOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnTransfer);
		this.ribbonPageGroup1.Name = "ribbonPageGroup1";
		this.ribbonPageGroup1.ShowCaptionButton = false;
		this.ribbonPageGroup1.Text = "حركات المخزون";
		this.ribbonPageGroup7.AllowTextClipping = false;
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDOrderReturnID);
		this.ribbonPageGroup7.ItemLinks.Add(this.BtnORDPurchaseReturnID);
		this.ribbonPageGroup7.Name = "ribbonPageGroup7";
		this.ribbonPageGroup7.ShowCaptionButton = false;
		this.ribbonPageGroup7.Text = "المرتجعات برقم الفاتورة";
		this.ribbonPageGroup8.AllowTextClipping = false;
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDOrderReturnNotID);
		this.ribbonPageGroup8.ItemLinks.Add(this.BtnORDPurchaseReturnNotID);
		this.ribbonPageGroup8.Name = "ribbonPageGroup8";
		this.ribbonPageGroup8.ShowCaptionButton = false;
		this.ribbonPageGroup8.Text = "مرتجعات بدون رقم الفاتورة";
		this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup11, this.ribbonPageGroup2 });
		this.ribbonPage2.Name = "ribbonPage2";
		this.ribbonPage2.Text = "قسم الحسابات";
		this.ribbonPage2.Visible = false;
		this.ribbonPageGroup11.AllowTextClipping = false;
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCardAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccTreeAccount);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccCostCenter);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccOpenMoney);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictionsTest);
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnAccRestrictions);
		this.ribbonPageGroup11.Name = "ribbonPageGroup11";
		this.ribbonPageGroup11.ShowCaptionButton = false;
		this.ribbonPageGroup11.Text = "بيانات الحسابات";
		this.ribbonPageGroup2.AllowTextClipping = false;
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnRecipt);
		this.ribbonPageGroup2.ItemLinks.Add(this.BtnPayment);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem7);
		this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem8);
		this.ribbonPageGroup2.Name = "ribbonPageGroup2";
		this.ribbonPageGroup2.ShowCaptionButton = false;
		this.ribbonPageGroup2.Text = "حركات الحسابات";
		this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup3 });
		this.ribbonPage3.Name = "ribbonPage3";
		this.ribbonPage3.Text = "شؤون الموظفين";
		this.ribbonPage3.Visible = false;
		this.ribbonPageGroup3.Name = "ribbonPageGroup3";
		this.ribbonPageGroup3.ShowCaptionButton = false;
		this.ribbonPageGroup3.Text = "ribbonPageGroup3";
		this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup4 });
		this.ribbonPage4.Name = "ribbonPage4";
		this.ribbonPage4.Text = " قسم الصلاحيات";
		this.ribbonPageGroup4.AllowTextClipping = false;
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnUsers);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnGroups);
		this.ribbonPageGroup4.ItemLinks.Add(this.BtnPermission);
		this.ribbonPageGroup4.Name = "ribbonPageGroup4";
		this.ribbonPageGroup4.ShowCaptionButton = false;
		this.ribbonPageGroup4.Text = "بيانات المستخدمين";
		this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[7] { this.ribbonPageGroup5, this.ribbonPageGroup19, this.ribbonPageGroup12, this.ribbonPageGroup13, this.ribbonPageGroup14, this.ribbonPageGroup15, this.ribbonPageGroup16 });
		this.ribbonPage5.Name = "ribbonPage5";
		this.ribbonPage5.Text = " قسم التقارير";
		this.ribbonPageGroup5.AllowTextClipping = false;
		this.ribbonPageGroup5.ItemLinks.Add(this.BtnReportPublic);
		this.ribbonPageGroup5.Name = "ribbonPageGroup5";
		this.ribbonPageGroup5.ShowCaptionButton = false;
		this.ribbonPageGroup5.Text = "تقارير عامة";
		this.ribbonPageGroup19.AllowTextClipping = false;
		this.ribbonPageGroup19.ItemLinks.Add(this.barSubItem6);
		this.ribbonPageGroup19.Name = "ribbonPageGroup19";
		this.ribbonPageGroup19.ShowCaptionButton = false;
		this.ribbonPageGroup19.Text = "العملاء والموردين";
		this.ribbonPageGroup12.AllowTextClipping = false;
		this.ribbonPageGroup12.ItemLinks.Add(this.BtnReportStore);
		this.ribbonPageGroup12.Name = "ribbonPageGroup12";
		this.ribbonPageGroup12.ShowCaptionButton = false;
		this.ribbonPageGroup12.Text = " المخازن";
		this.ribbonPageGroup13.AllowTextClipping = false;
		this.ribbonPageGroup13.ItemLinks.Add(this.BtnReportPurshes);
		this.ribbonPageGroup13.Name = "ribbonPageGroup13";
		this.ribbonPageGroup13.ShowCaptionButton = false;
		this.ribbonPageGroup13.Text = " المشتريات";
		this.ribbonPageGroup14.AllowTextClipping = false;
		this.ribbonPageGroup14.ItemLinks.Add(this.BtnReportOrder);
		this.ribbonPageGroup14.Name = "ribbonPageGroup14";
		this.ribbonPageGroup14.ShowCaptionButton = false;
		this.ribbonPageGroup14.Text = " المبيعات";
		this.ribbonPageGroup15.AllowTextClipping = false;
		this.ribbonPageGroup15.ItemLinks.Add(this.BtnReportProfit);
		this.ribbonPageGroup15.Name = "ribbonPageGroup15";
		this.ribbonPageGroup15.ShowCaptionButton = false;
		this.ribbonPageGroup15.Text = "تقارير الارباح";
		this.ribbonPageGroup16.AllowTextClipping = false;
		this.ribbonPageGroup16.ItemLinks.Add(this.BtnReportAccount);
		this.ribbonPageGroup16.Name = "ribbonPageGroup16";
		this.ribbonPageGroup16.ShowCaptionButton = false;
		this.ribbonPageGroup16.Text = "الحسابات";
		this.ribbonPageGroup16.Visible = false;
		this.ribbonPage6.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup6, this.ribbonPageGroup17 });
		this.ribbonPage6.Name = "ribbonPage6";
		this.ribbonPage6.Text = "قسم الاعدادات";
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnPrinters);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnSetting);
		this.ribbonPageGroup6.ItemLinks.Add(this.btnDefualtAccount);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnConnection);
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnDabase);
		this.ribbonPageGroup6.Name = "ribbonPageGroup6";
		this.ribbonPageGroup6.ShowCaptionButton = false;
		this.ribbonPageGroup6.Text = "الاعدادات الرئيسية للبرنامج";
		this.ribbonPageGroup17.AllowTextClipping = false;
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnAbout);
		this.ribbonPageGroup17.ItemLinks.Add(this.BtnReportDesign);
		this.ribbonPageGroup17.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
		this.ribbonPageGroup17.Name = "ribbonPageGroup17";
		this.ribbonPageGroup17.ShowCaptionButton = false;
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
		this.ribbonStatusBar.Location = new System.Drawing.Point(0, 713);
		this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.ribbonStatusBar.Name = "ribbonStatusBar";
		this.ribbonStatusBar.Ribbon = this.ribbonControl;
		this.ribbonStatusBar.Size = new System.Drawing.Size(1173, 40);
		this.navBarControl.ActiveGroup = this.employeesNavBarGroup;
		this.navBarControl.Dock = System.Windows.Forms.DockStyle.Left;
		this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[1] { this.employeesNavBarGroup });
		this.navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[2] { this.navBarItem1, this.navBarItem2 });
		this.navBarControl.Location = new System.Drawing.Point(0, 179);
		this.navBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.navBarControl.Name = "navBarControl";
		this.navBarControl.OptionsNavPane.ExpandedWidth = 192;
		this.navBarControl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
		this.navBarControl.Size = new System.Drawing.Size(192, 534);
		this.navBarControl.TabIndex = 0;
		this.navBarControl.Text = "navBarControl";
		this.navBarControl.Visible = false;
		this.navBarControl.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(navBarControl_ActiveGroupChanged);
		this.employeesNavBarGroup.Caption = "اعدادات الالوان";
		this.employeesNavBarGroup.Expanded = true;
		this.employeesNavBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[2]
		{
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
			new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2)
		});
		this.employeesNavBarGroup.Name = "employeesNavBarGroup";
		this.navBarItem1.Caption = "                    أزرق ";
		this.navBarItem1.Name = "navBarItem1";
		this.navBarItem1.SmallImage = GTSErpSystem.Properties.Resources.palette_16x16;
		this.navBarItem2.Caption = "                    بني";
		this.navBarItem2.Name = "navBarItem2";
		this.navBarItem2.SmallImage = GTSErpSystem.Properties.Resources.palette_16x161;
		this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
		this.navigationFrame.Appearance.Options.UseBackColor = true;
		this.navigationFrame.Controls.Add(this.employeesNavigationPage);
		this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
		this.navigationFrame.Location = new System.Drawing.Point(192, 179);
		this.navigationFrame.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.navigationFrame.Name = "navigationFrame";
		this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[1] { this.employeesNavigationPage });
		this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
		this.navigationFrame.SelectedPage = this.employeesNavigationPage;
		this.navigationFrame.Size = new System.Drawing.Size(981, 534);
		this.navigationFrame.TabIndex = 0;
		this.navigationFrame.Text = "navigationFrame";
		this.employeesNavigationPage.Controls.Add(this.tileControl1);
		this.employeesNavigationPage.Controls.Add(this.employeesLabelControl);
		this.employeesNavigationPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.employeesNavigationPage.Name = "employeesNavigationPage";
		this.employeesNavigationPage.Size = new System.Drawing.Size(981, 534);
		this.tileControl1.AllowDrag = false;
		this.tileControl1.AllowDragTilesBetweenGroups = false;
		this.tileControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tileControl1.Cursor = System.Windows.Forms.Cursors.Default;
		this.tileControl1.Groups.Add(this.tileGroup2);
		this.tileControl1.Groups.Add(this.tileGroup3);
		this.tileControl1.ItemSize = 150;
		this.tileControl1.Location = new System.Drawing.Point(64, 63);
		this.tileControl1.MaxId = 26;
		this.tileControl1.Name = "tileControl1";
		this.tileControl1.Position = 751;
		this.tileControl1.RowCount = 3;
		this.tileControl1.Size = new System.Drawing.Size(857, 421);
		this.tileControl1.TabIndex = 1;
		this.tileControl1.Text = "tileControl1";
		this.tileGroup2.Items.Add(this.TilPurches);
		this.tileGroup2.Items.Add(this.TilOfferPrice);
		this.tileGroup2.Items.Add(this.TilPurchesOrder);
		this.tileGroup2.Items.Add(this.TilStoresTransfer);
		this.tileGroup2.Items.Add(this.TilItems);
		this.tileGroup2.Items.Add(this.TilPurchesReturn);
		this.tileGroup2.Items.Add(this.TileOrderReturn);
		this.tileGroup2.Items.Add(this.TileReceipt);
		this.tileGroup2.Items.Add(this.TilePayment);
		this.tileGroup2.Items.Add(this.TileLeadger);
		this.tileGroup2.Items.Add(this.TileOrder);
		this.tileGroup2.Items.Add(this.TilCustomer);
		this.tileGroup2.Items.Add(this.TilSupplier);
		this.tileGroup2.Items.Add(this.TileUser);
		this.tileGroup2.Items.Add(this.TilePermission);
		this.tileGroup2.Name = "tileGroup2";
		this.tileGroup2.Text = "البيانات السريعة";
		this.TilPurches.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TilPurches.CurrentFrameIndex = 1;
		tileItemElement.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement.Text = "المشتريات";
		tileItemElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilPurches.Elements.Add(tileItemElement);
		tileItemFrame.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollLeft;
		tileItemFrame.Appearance.BackColor = System.Drawing.Color.SteelBlue;
		tileItemFrame.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame.Appearance.Options.UseBackColor = true;
		tileItemFrame.Appearance.Options.UseFont = true;
		tileItemElement2.Image = GTSErpSystem.Properties.Resources.Pur1;
		tileItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement2.Text = "المشتريات";
		tileItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame.Elements.Add(tileItemElement2);
		tileItemFrame.Image = GTSErpSystem.Properties.Resources.Pur1;
		tileItemFrame2.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.Fade;
		tileItemFrame2.Appearance.BackColor = System.Drawing.Color.SteelBlue;
		tileItemFrame2.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame2.Appearance.Options.UseBackColor = true;
		tileItemFrame2.Appearance.Options.UseFont = true;
		tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement3.Text = "المشتريات";
		tileItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame2.Elements.Add(tileItemElement3);
		this.TilPurches.Frames.Add(tileItemFrame);
		this.TilPurches.Frames.Add(tileItemFrame2);
		this.TilPurches.Id = 0;
		this.TilPurches.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
		this.TilPurches.Name = "TilPurches";
		this.TilPurches.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilPurches_ItemClick);
		this.TilOfferPrice.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement4.Image = GTSErpSystem.Properties.Resources.Offer;
		tileItemElement4.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement4.Text = "عرض سعر";
		tileItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilOfferPrice.Elements.Add(tileItemElement4);
		tileItemFrame3.Appearance.BackColor = System.Drawing.Color.SlateGray;
		tileItemFrame3.Appearance.Font = new System.Drawing.Font("Tahoma", 12f);
		tileItemFrame3.Appearance.Options.UseBackColor = true;
		tileItemFrame3.Appearance.Options.UseFont = true;
		tileItemElement5.Image = GTSErpSystem.Properties.Resources.Offer;
		tileItemElement5.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement5.Text = "عرض سعر";
		tileItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame3.Elements.Add(tileItemElement5);
		tileItemFrame3.Image = GTSErpSystem.Properties.Resources.Offer;
		this.TilOfferPrice.Frames.Add(tileItemFrame3);
		this.TilOfferPrice.Id = 1;
		this.TilOfferPrice.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilOfferPrice.Name = "TilOfferPrice";
		this.TilOfferPrice.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilOfferPrice_ItemClick);
		this.TilPurchesOrder.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement6.Image = GTSErpSystem.Properties.Resources.Purord;
		tileItemElement6.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement6.Text = "طلب شراء";
		tileItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilPurchesOrder.Elements.Add(tileItemElement6);
		tileItemFrame4.Appearance.BackColor = System.Drawing.Color.MediumSeaGreen;
		tileItemFrame4.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame4.Appearance.Options.UseBackColor = true;
		tileItemFrame4.Appearance.Options.UseFont = true;
		tileItemElement7.Image = GTSErpSystem.Properties.Resources.Purord;
		tileItemElement7.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement7.Text = "طلب شراء";
		tileItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame4.Elements.Add(tileItemElement7);
		tileItemFrame4.Image = GTSErpSystem.Properties.Resources.Purord;
		this.TilPurchesOrder.Frames.Add(tileItemFrame4);
		this.TilPurchesOrder.Id = 13;
		this.TilPurchesOrder.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilPurchesOrder.Name = "TilPurchesOrder";
		this.TilPurchesOrder.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilPurchesOrder_ItemClick);
		this.TilStoresTransfer.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TilStoresTransfer.CurrentFrameIndex = 1;
		tileItemElement8.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement8.Text = "تحويل المخازن";
		tileItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilStoresTransfer.Elements.Add(tileItemElement8);
		tileItemFrame5.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame5.Appearance.BackColor = System.Drawing.Color.MediumSeaGreen;
		tileItemFrame5.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame5.Appearance.Options.UseBackColor = true;
		tileItemFrame5.Appearance.Options.UseFont = true;
		tileItemElement9.Image = GTSErpSystem.Properties.Resources.Storetransfer;
		tileItemElement9.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement9.Text = "تحويل المخازن";
		tileItemElement9.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame5.Elements.Add(tileItemElement9);
		tileItemFrame5.Image = GTSErpSystem.Properties.Resources.Storetransfer;
		tileItemFrame6.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame6.Appearance.BackColor = System.Drawing.Color.MediumSeaGreen;
		tileItemFrame6.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame6.Appearance.Options.UseBackColor = true;
		tileItemFrame6.Appearance.Options.UseFont = true;
		tileItemElement10.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement10.Text = "تحويل المخازن";
		tileItemElement10.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame6.Elements.Add(tileItemElement10);
		this.TilStoresTransfer.Frames.Add(tileItemFrame5);
		this.TilStoresTransfer.Frames.Add(tileItemFrame6);
		this.TilStoresTransfer.Id = 22;
		this.TilStoresTransfer.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilStoresTransfer.Name = "TilStoresTransfer";
		this.TilStoresTransfer.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilStoresTransfer_ItemClick);
		this.TilItems.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement11.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement11.Text = "الكاشير";
		tileItemElement11.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilItems.Elements.Add(tileItemElement11);
		tileItemFrame7.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
		tileItemFrame7.Appearance.BackColor = System.Drawing.Color.SlateGray;
		tileItemFrame7.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame7.Appearance.Options.UseBackColor = true;
		tileItemFrame7.Appearance.Options.UseFont = true;
		tileItemElement12.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement12.Text = "الكاشير";
		tileItemElement12.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame7.Elements.Add(tileItemElement12);
		this.TilItems.Frames.Add(tileItemFrame7);
		this.TilItems.Id = 2;
		this.TilItems.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilItems.Name = "TilItems";
		this.TilItems.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilItems_ItemClick);
		this.TilPurchesReturn.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement13.Image = GTSErpSystem.Properties.Resources.Pur2;
		tileItemElement13.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement13.Text = "ظ…. المشتريات";
		tileItemElement13.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilPurchesReturn.Elements.Add(tileItemElement13);
		tileItemFrame8.Appearance.BackColor = System.Drawing.Color.CornflowerBlue;
		tileItemFrame8.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame8.Appearance.Options.UseBackColor = true;
		tileItemFrame8.Appearance.Options.UseFont = true;
		tileItemElement14.Image = GTSErpSystem.Properties.Resources.Pur2;
		tileItemElement14.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement14.Text = "ظ…. المشتريات";
		tileItemElement14.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame8.Elements.Add(tileItemElement14);
		tileItemFrame8.Image = GTSErpSystem.Properties.Resources.Pur2;
		this.TilPurchesReturn.Frames.Add(tileItemFrame8);
		this.TilPurchesReturn.Id = 10;
		this.TilPurchesReturn.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilPurchesReturn.Name = "TilPurchesReturn";
		this.TilPurchesReturn.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilPurchesReturn_ItemClick);
		this.TileOrderReturn.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement15.Image = GTSErpSystem.Properties.Resources.Ord2;
		tileItemElement15.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement15.Text = "ظ…. المبيعات";
		tileItemElement15.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileOrderReturn.Elements.Add(tileItemElement15);
		tileItemFrame9.Appearance.BackColor = System.Drawing.Color.Fuchsia;
		tileItemFrame9.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame9.Appearance.Options.UseBackColor = true;
		tileItemFrame9.Appearance.Options.UseFont = true;
		tileItemElement16.Image = GTSErpSystem.Properties.Resources.Ord2;
		tileItemElement16.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement16.Text = "ظ…. المبيعات";
		tileItemElement16.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame9.Elements.Add(tileItemElement16);
		tileItemFrame9.Image = GTSErpSystem.Properties.Resources.Ord2;
		this.TileOrderReturn.Frames.Add(tileItemFrame9);
		this.TileOrderReturn.Id = 9;
		this.TileOrderReturn.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TileOrderReturn.Name = "TileOrderReturn";
		this.TileOrderReturn.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileOrderReturn_ItemClick);
		this.TileReceipt.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement17.Image = GTSErpSystem.Properties.Resources.Recipt;
		tileItemElement17.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement17.Text = "إيصال قبض";
		tileItemElement17.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileReceipt.Elements.Add(tileItemElement17);
		tileItemFrame10.Appearance.BackColor = System.Drawing.Color.Fuchsia;
		tileItemFrame10.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame10.Appearance.Options.UseBackColor = true;
		tileItemFrame10.Appearance.Options.UseFont = true;
		tileItemElement18.Image = GTSErpSystem.Properties.Resources.Recipt;
		tileItemElement18.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement18.Text = "إيصال قبض";
		tileItemElement18.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame10.Elements.Add(tileItemElement18);
		tileItemFrame10.Image = GTSErpSystem.Properties.Resources.Recipt;
		this.TileReceipt.Frames.Add(tileItemFrame10);
		this.TileReceipt.Id = 7;
		this.TileReceipt.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TileReceipt.Name = "TileReceipt";
		this.TileReceipt.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileReceipt_ItemClick);
		this.TilePayment.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement19.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement19.Appearance.Hovered.Options.UseFont = true;
		tileItemElement19.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement19.Appearance.Normal.Options.UseFont = true;
		tileItemElement19.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement19.Appearance.Selected.Options.UseFont = true;
		tileItemElement19.Image = GTSErpSystem.Properties.Resources.Payment;
		tileItemElement19.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement19.ImageLocation = new System.Drawing.Point(-12, -8);
		tileItemElement19.ImageSize = new System.Drawing.Size(165, 165);
		tileItemElement19.Text = "إيصال صرف";
		tileItemElement19.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemElement19.TextLocation = new System.Drawing.Point(4, 3);
		this.TilePayment.Elements.Add(tileItemElement19);
		tileItemFrame11.Appearance.BackColor = System.Drawing.Color.CornflowerBlue;
		tileItemFrame11.Appearance.Options.UseBackColor = true;
		tileItemElement20.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement20.Appearance.Hovered.Options.UseFont = true;
		tileItemElement20.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement20.Appearance.Normal.Options.UseFont = true;
		tileItemElement20.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 10.725f);
		tileItemElement20.Appearance.Selected.Options.UseFont = true;
		tileItemElement20.Image = GTSErpSystem.Properties.Resources.Payment;
		tileItemElement20.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement20.ImageLocation = new System.Drawing.Point(-12, -8);
		tileItemElement20.ImageSize = new System.Drawing.Size(165, 165);
		tileItemElement20.Text = "إيصال صرف";
		tileItemElement20.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemElement20.TextLocation = new System.Drawing.Point(4, 3);
		tileItemFrame11.Elements.Add(tileItemElement20);
		tileItemFrame11.Image = GTSErpSystem.Properties.Resources.Payment;
		this.TilePayment.Frames.Add(tileItemFrame11);
		this.TilePayment.Id = 11;
		this.TilePayment.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilePayment.Name = "TilePayment";
		this.TilePayment.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilePayment_ItemClick);
		this.TileLeadger.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TileLeadger.CurrentFrameIndex = 1;
		tileItemElement21.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement21.Text = "كشف حساب";
		tileItemElement21.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileLeadger.Elements.Add(tileItemElement21);
		tileItemFrame12.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame12.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
		tileItemFrame12.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame12.Appearance.Options.UseBackColor = true;
		tileItemFrame12.Appearance.Options.UseFont = true;
		tileItemElement22.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement22.Text = "كشف حساب";
		tileItemElement22.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame12.Elements.Add(tileItemElement22);
		tileItemFrame13.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollLeft;
		tileItemFrame13.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
		tileItemFrame13.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame13.Appearance.Options.UseBackColor = true;
		tileItemFrame13.Appearance.Options.UseFont = true;
		tileItemElement23.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement23.Text = "كشف حساب";
		tileItemElement23.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame13.Elements.Add(tileItemElement23);
		this.TileLeadger.Frames.Add(tileItemFrame12);
		this.TileLeadger.Frames.Add(tileItemFrame13);
		this.TileLeadger.Id = 6;
		this.TileLeadger.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
		this.TileLeadger.Name = "TileLeadger";
		this.TileLeadger.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileLeadger_ItemClick);
		this.TileOrder.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TileOrder.CurrentFrameIndex = 1;
		tileItemElement24.Image = GTSErpSystem.Properties.Resources.Pur11;
		tileItemElement24.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement24.Text = "المبيعات";
		tileItemElement24.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileOrder.Elements.Add(tileItemElement24);
		tileItemFrame14.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.SegmentedFade;
		tileItemFrame14.Appearance.BackColor = System.Drawing.Color.DarkCyan;
		tileItemFrame14.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame14.Appearance.Options.UseBackColor = true;
		tileItemFrame14.Appearance.Options.UseFont = true;
		tileItemElement25.Image = GTSErpSystem.Properties.Resources.Ord1;
		tileItemElement25.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement25.Text = "المبيعات";
		tileItemElement25.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame14.Elements.Add(tileItemElement25);
		tileItemFrame14.Image = GTSErpSystem.Properties.Resources.Ord1;
		tileItemFrame15.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.Fade;
		tileItemFrame15.Appearance.BackColor = System.Drawing.Color.DarkCyan;
		tileItemFrame15.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame15.Appearance.Options.UseBackColor = true;
		tileItemFrame15.Appearance.Options.UseFont = true;
		tileItemElement26.Image = GTSErpSystem.Properties.Resources.Pur11;
		tileItemElement26.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement26.Text = "المبيعات";
		tileItemElement26.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame15.Elements.Add(tileItemElement26);
		tileItemFrame15.Image = GTSErpSystem.Properties.Resources.Pur11;
		this.TileOrder.Frames.Add(tileItemFrame14);
		this.TileOrder.Frames.Add(tileItemFrame15);
		this.TileOrder.Id = 12;
		this.TileOrder.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
		this.TileOrder.Name = "TileOrder";
		this.TileOrder.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileOrder_ItemClick);
		this.TilCustomer.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement27.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement27.Text = "العملاء";
		tileItemElement27.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilCustomer.Elements.Add(tileItemElement27);
		tileItemFrame16.Appearance.BackColor = System.Drawing.Color.MediumOrchid;
		tileItemFrame16.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame16.Appearance.Options.UseBackColor = true;
		tileItemFrame16.Appearance.Options.UseFont = true;
		tileItemElement28.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement28.Text = "العملاء";
		tileItemElement28.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame16.Elements.Add(tileItemElement28);
		this.TilCustomer.Frames.Add(tileItemFrame16);
		this.TilCustomer.Id = 3;
		this.TilCustomer.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilCustomer.Name = "TilCustomer";
		this.TilCustomer.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilCustomer_ItemClick);
		this.TilSupplier.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement29.Image = GTSErpSystem.Properties.Resources.Supp1;
		tileItemElement29.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement29.Text = "الموردين";
		tileItemElement29.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilSupplier.Elements.Add(tileItemElement29);
		tileItemFrame17.Appearance.BackColor = System.Drawing.Color.CadetBlue;
		tileItemFrame17.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame17.Appearance.Options.UseBackColor = true;
		tileItemFrame17.Appearance.Options.UseFont = true;
		tileItemElement30.Image = GTSErpSystem.Properties.Resources.Supp1;
		tileItemElement30.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement30.Text = "الموردين";
		tileItemElement30.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame17.Elements.Add(tileItemElement30);
		tileItemFrame17.Image = GTSErpSystem.Properties.Resources.Supp1;
		this.TilSupplier.Frames.Add(tileItemFrame17);
		this.TilSupplier.Id = 16;
		this.TilSupplier.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilSupplier.Name = "TilSupplier";
		this.TilSupplier.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilSupplier_ItemClick);
		this.TileUser.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement31.Image = GTSErpSystem.Properties.Resources.User;
		tileItemElement31.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement31.Text = "المستخدمين";
		tileItemElement31.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileUser.Elements.Add(tileItemElement31);
		tileItemFrame18.Appearance.BackColor = System.Drawing.Color.CadetBlue;
		tileItemFrame18.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame18.Appearance.Options.UseBackColor = true;
		tileItemFrame18.Appearance.Options.UseFont = true;
		tileItemElement32.Image = GTSErpSystem.Properties.Resources.User;
		tileItemElement32.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement32.Text = "المستخدمين";
		tileItemElement32.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame18.Elements.Add(tileItemElement32);
		tileItemFrame18.Image = GTSErpSystem.Properties.Resources.User;
		this.TileUser.Frames.Add(tileItemFrame18);
		this.TileUser.Id = 4;
		this.TileUser.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TileUser.Name = "TileUser";
		this.TileUser.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileUser_ItemClick);
		this.TilePermission.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		tileItemElement33.Image = GTSErpSystem.Properties.Resources.Permission1;
		tileItemElement33.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement33.Text = "الصلاحيات";
		tileItemElement33.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilePermission.Elements.Add(tileItemElement33);
		tileItemFrame19.Appearance.BackColor = System.Drawing.Color.MediumOrchid;
		tileItemFrame19.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame19.Appearance.Options.UseBackColor = true;
		tileItemFrame19.Appearance.Options.UseFont = true;
		tileItemElement34.Image = GTSErpSystem.Properties.Resources.Permission1;
		tileItemElement34.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement34.Text = "الصلاحيات";
		tileItemElement34.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame19.Elements.Add(tileItemElement34);
		tileItemFrame19.Image = GTSErpSystem.Properties.Resources.Permission1;
		this.TilePermission.Frames.Add(tileItemFrame19);
		this.TilePermission.Id = 5;
		this.TilePermission.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilePermission.Name = "TilePermission";
		this.TilePermission.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilePermission_ItemClick);
		this.tileGroup3.Items.Add(this.TilAccountQuid);
		this.tileGroup3.Items.Add(this.TilAccountDefualt);
		this.tileGroup3.Items.Add(this.TileSetting);
		this.tileGroup3.Items.Add(this.TileDesignReport);
		this.tileGroup3.Items.Add(this.TilExit);
		this.tileGroup3.Name = "tileGroup3";
		this.tileGroup3.Visible = false;
		this.TilAccountQuid.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TilAccountQuid.CurrentFrameIndex = 1;
		tileItemElement35.Image = GTSErpSystem.Properties.Resources.AccountSetting;
		tileItemElement35.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement35.Text = "قيود المحاسبة";
		tileItemElement35.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilAccountQuid.Elements.Add(tileItemElement35);
		tileItemFrame20.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollDown;
		tileItemFrame20.Appearance.BackColor = System.Drawing.Color.Magenta;
		tileItemFrame20.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame20.Appearance.Options.UseBackColor = true;
		tileItemFrame20.Appearance.Options.UseFont = true;
		tileItemElement36.Image = GTSErpSystem.Properties.Resources.AccountSetting;
		tileItemElement36.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement36.Text = "قيود المحاسبة";
		tileItemElement36.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame20.Elements.Add(tileItemElement36);
		tileItemFrame20.Image = GTSErpSystem.Properties.Resources.AccountSetting;
		tileItemFrame21.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollLeft;
		tileItemFrame21.Appearance.BackColor = System.Drawing.Color.Magenta;
		tileItemFrame21.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame21.Appearance.Options.UseBackColor = true;
		tileItemFrame21.Appearance.Options.UseFont = true;
		tileItemElement37.Image = GTSErpSystem.Properties.Resources.AccountSetting;
		tileItemElement37.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement37.Text = "قيود المحاسبة";
		tileItemElement37.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame21.Elements.Add(tileItemElement37);
		tileItemFrame21.Image = GTSErpSystem.Properties.Resources.AccountSetting;
		this.TilAccountQuid.Frames.Add(tileItemFrame20);
		this.TilAccountQuid.Frames.Add(tileItemFrame21);
		this.TilAccountQuid.Id = 14;
		this.TilAccountQuid.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilAccountQuid.Name = "TilAccountQuid";
		this.TilAccountQuid.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilAccountQuid_ItemClick);
		this.TilAccountDefualt.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TilAccountDefualt.CurrentFrameIndex = 1;
		tileItemElement38.Image = GTSErpSystem.Properties.Resources.AccountQuid;
		tileItemElement38.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement38.Text = "اعداد البرنامج";
		tileItemElement38.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TilAccountDefualt.Elements.Add(tileItemElement38);
		tileItemFrame22.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame22.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
		tileItemFrame22.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame22.Appearance.Options.UseBackColor = true;
		tileItemFrame22.Appearance.Options.UseFont = true;
		tileItemElement39.Image = GTSErpSystem.Properties.Resources.AccountQuid;
		tileItemElement39.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement39.Text = "اعداد البرنامج";
		tileItemElement39.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame22.Elements.Add(tileItemElement39);
		tileItemFrame22.Image = GTSErpSystem.Properties.Resources.AccountQuid;
		tileItemFrame23.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollDown;
		tileItemFrame23.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
		tileItemFrame23.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame23.Appearance.Options.UseBackColor = true;
		tileItemFrame23.Appearance.Options.UseFont = true;
		tileItemElement40.Image = GTSErpSystem.Properties.Resources.AccountQuid;
		tileItemElement40.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement40.Text = "اعداد البرنامج";
		tileItemElement40.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame23.Elements.Add(tileItemElement40);
		tileItemFrame23.Image = GTSErpSystem.Properties.Resources.AccountQuid;
		this.TilAccountDefualt.Frames.Add(tileItemFrame22);
		this.TilAccountDefualt.Frames.Add(tileItemFrame23);
		this.TilAccountDefualt.Id = 15;
		this.TilAccountDefualt.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TilAccountDefualt.Name = "TilAccountDefualt";
		this.TilAccountDefualt.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilAccountDefualt_ItemClick);
		this.TileSetting.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TileSetting.CurrentFrameIndex = 1;
		tileItemElement41.Image = GTSErpSystem.Properties.Resources.Setting;
		tileItemElement41.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement41.Text = "الاعدادات";
		tileItemElement41.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileSetting.Elements.Add(tileItemElement41);
		tileItemFrame24.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame24.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
		tileItemFrame24.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame24.Appearance.Options.UseBackColor = true;
		tileItemFrame24.Appearance.Options.UseFont = true;
		tileItemElement42.Image = GTSErpSystem.Properties.Resources.Setting;
		tileItemElement42.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement42.Text = "الاعدادات";
		tileItemElement42.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame24.Elements.Add(tileItemElement42);
		tileItemFrame24.Image = GTSErpSystem.Properties.Resources.Setting;
		tileItemFrame25.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
		tileItemFrame25.Appearance.BackColor = System.Drawing.Color.DeepSkyBlue;
		tileItemFrame25.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame25.Appearance.Options.UseBackColor = true;
		tileItemFrame25.Appearance.Options.UseFont = true;
		tileItemElement43.Image = GTSErpSystem.Properties.Resources.Setting;
		tileItemElement43.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement43.Text = "الاعدادات";
		tileItemElement43.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame25.Elements.Add(tileItemElement43);
		tileItemFrame25.Image = GTSErpSystem.Properties.Resources.Setting;
		this.TileSetting.Frames.Add(tileItemFrame24);
		this.TileSetting.Frames.Add(tileItemFrame25);
		this.TileSetting.Id = 17;
		this.TileSetting.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TileSetting.Name = "TileSetting";
		this.TileSetting.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileSetting_ItemClick);
		this.TileDesignReport.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TileDesignReport.CurrentFrameIndex = 1;
		tileItemElement44.Image = GTSErpSystem.Properties.Resources.Design;
		tileItemElement44.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement44.Text = "مصمم التقارير";
		tileItemElement44.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		this.TileDesignReport.Elements.Add(tileItemElement44);
		tileItemFrame26.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
		tileItemFrame26.Appearance.BackColor = System.Drawing.Color.Magenta;
		tileItemFrame26.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame26.Appearance.Options.UseBackColor = true;
		tileItemFrame26.Appearance.Options.UseFont = true;
		tileItemElement45.Image = GTSErpSystem.Properties.Resources.Design;
		tileItemElement45.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement45.Text = "مصمم التقارير";
		tileItemElement45.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame26.Elements.Add(tileItemElement45);
		tileItemFrame26.Image = GTSErpSystem.Properties.Resources.Design;
		tileItemFrame27.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollRight;
		tileItemFrame27.Appearance.BackColor = System.Drawing.Color.Magenta;
		tileItemFrame27.Appearance.Font = new System.Drawing.Font("Tahoma", 12.35f);
		tileItemFrame27.Appearance.Options.UseBackColor = true;
		tileItemFrame27.Appearance.Options.UseFont = true;
		tileItemElement46.Image = GTSErpSystem.Properties.Resources.Design;
		tileItemElement46.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement46.Text = "مصمم التقارير";
		tileItemElement46.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemFrame27.Elements.Add(tileItemElement46);
		tileItemFrame27.Image = GTSErpSystem.Properties.Resources.Design;
		this.TileDesignReport.Frames.Add(tileItemFrame26);
		this.TileDesignReport.Frames.Add(tileItemFrame27);
		this.TileDesignReport.Id = 24;
		this.TileDesignReport.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
		this.TileDesignReport.Name = "TileDesignReport";
		this.TileDesignReport.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TileDesignReport_ItemClick);
		this.TilExit.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		this.TilExit.CurrentFrameIndex = 1;
		tileItemElement47.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement47.Appearance.Hovered.Options.UseFont = true;
		tileItemElement47.Appearance.Hovered.Options.UseTextOptions = true;
		tileItemElement47.Appearance.Hovered.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement47.Appearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement47.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement47.Appearance.Normal.Options.UseFont = true;
		tileItemElement47.Appearance.Normal.Options.UseTextOptions = true;
		tileItemElement47.Appearance.Normal.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement47.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement47.Appearance.Selected.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement47.Appearance.Selected.Options.UseFont = true;
		tileItemElement47.Appearance.Selected.Options.UseTextOptions = true;
		tileItemElement47.Appearance.Selected.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement47.Appearance.Selected.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement47.Image = GTSErpSystem.Properties.Resources.Exit;
		tileItemElement47.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement47.ImageLocation = new System.Drawing.Point(4, 3);
		tileItemElement47.Text = "خروج";
		tileItemElement47.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemElement47.TextLocation = new System.Drawing.Point(9, 3);
		this.TilExit.Elements.Add(tileItemElement47);
		tileItemFrame28.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollDown;
		tileItemFrame28.Appearance.BackColor = System.Drawing.Color.LightSeaGreen;
		tileItemFrame28.Appearance.Options.UseBackColor = true;
		tileItemElement48.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement48.Appearance.Hovered.Options.UseFont = true;
		tileItemElement48.Appearance.Hovered.Options.UseTextOptions = true;
		tileItemElement48.Appearance.Hovered.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement48.Appearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement48.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement48.Appearance.Normal.Options.UseFont = true;
		tileItemElement48.Appearance.Normal.Options.UseTextOptions = true;
		tileItemElement48.Appearance.Normal.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement48.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement48.Appearance.Selected.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement48.Appearance.Selected.Options.UseFont = true;
		tileItemElement48.Appearance.Selected.Options.UseTextOptions = true;
		tileItemElement48.Appearance.Selected.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement48.Appearance.Selected.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement48.Image = GTSErpSystem.Properties.Resources.Exit;
		tileItemElement48.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement48.ImageLocation = new System.Drawing.Point(4, 3);
		tileItemElement48.Text = "خروج";
		tileItemElement48.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemElement48.TextLocation = new System.Drawing.Point(9, 3);
		tileItemFrame28.Elements.Add(tileItemElement48);
		tileItemFrame28.Image = GTSErpSystem.Properties.Resources.Exit;
		tileItemFrame29.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
		tileItemFrame29.Appearance.BackColor = System.Drawing.Color.LightSeaGreen;
		tileItemFrame29.Appearance.Options.UseBackColor = true;
		tileItemElement49.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement49.Appearance.Hovered.Options.UseFont = true;
		tileItemElement49.Appearance.Hovered.Options.UseTextOptions = true;
		tileItemElement49.Appearance.Hovered.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement49.Appearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement49.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement49.Appearance.Normal.Options.UseFont = true;
		tileItemElement49.Appearance.Normal.Options.UseTextOptions = true;
		tileItemElement49.Appearance.Normal.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement49.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement49.Appearance.Selected.Font = new System.Drawing.Font("Segoe UI Light", 23.375f);
		tileItemElement49.Appearance.Selected.Options.UseFont = true;
		tileItemElement49.Appearance.Selected.Options.UseTextOptions = true;
		tileItemElement49.Appearance.Selected.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
		tileItemElement49.Appearance.Selected.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
		tileItemElement49.Image = GTSErpSystem.Properties.Resources.Exit;
		tileItemElement49.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
		tileItemElement49.ImageLocation = new System.Drawing.Point(4, 3);
		tileItemElement49.Text = "خروج";
		tileItemElement49.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomRight;
		tileItemElement49.TextLocation = new System.Drawing.Point(9, 3);
		tileItemFrame29.Elements.Add(tileItemElement49);
		tileItemFrame29.Image = GTSErpSystem.Properties.Resources.Exit;
		this.TilExit.Frames.Add(tileItemFrame28);
		this.TilExit.Frames.Add(tileItemFrame29);
		this.TilExit.Id = 23;
		this.TilExit.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
		this.TilExit.Name = "TilExit";
		this.TilExit.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(TilExit_ItemClick);
		this.employeesLabelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 25.25f);
		this.employeesLabelControl.Appearance.ForeColor = System.Drawing.Color.Gray;
		this.employeesLabelControl.Appearance.Options.UseFont = true;
		this.employeesLabelControl.Appearance.Options.UseForeColor = true;
		this.employeesLabelControl.Appearance.Options.UseTextOptions = true;
		this.employeesLabelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.employeesLabelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.employeesLabelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
		this.employeesLabelControl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.employeesLabelControl.Location = new System.Drawing.Point(0, 0);
		this.employeesLabelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.employeesLabelControl.Name = "employeesLabelControl";
		this.employeesLabelControl.Size = new System.Drawing.Size(981, 534);
		this.employeesLabelControl.TabIndex = 0;
		this.employeesLabelControl.Text = "Employees";
		this.ribbonPageGroup.AllowTextClipping = false;
		this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem);
		this.ribbonPageGroup.Name = "ribbonPageGroup";
		this.ribbonPageGroup.ShowCaptionButton = false;
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
		base.ClientSize = new System.Drawing.Size(1173, 753);
		base.Controls.Add(this.navigationFrame);
		base.Controls.Add(this.navBarControl);
		base.Controls.Add(this.ribbonStatusBar);
		base.Controls.Add(this.ribbonControl);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "FrmLight";
		this.Ribbon = this.ribbonControl;
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.StatusBar = this.ribbonStatusBar;
		this.Text = "برنامج مايكرو سمارت للمحاسبة ونقاط البيع";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmMain_FormClosing);
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
		((System.ComponentModel.ISupportInitialize)this.navBarControl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.navigationFrame).EndInit();
		this.navigationFrame.ResumeLayout(false);
		this.employeesNavigationPage.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


