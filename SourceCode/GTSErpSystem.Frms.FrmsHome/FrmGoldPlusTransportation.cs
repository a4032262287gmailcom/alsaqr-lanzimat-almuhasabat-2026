using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Reports;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.Account;
using GTSErpSystem.Frms.Account.Check;
using GTSErpSystem.Frms.Contract;
using GTSErpSystem.Frms.Contract.Report;
using GTSErpSystem.Frms.Cust_Sup;
using GTSErpSystem.Frms.FrmRestaurant;
using GTSErpSystem.Frms.FrmRestaurant.LuckUp;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items;
using GTSErpSystem.Frms.OrderDalala;
using GTSErpSystem.Frms.OrderDalala.Report;
using GTSErpSystem.Frms.OrderManufacturing;
using GTSErpSystem.Frms.OrderRent;
using GTSErpSystem.Frms.Orders;
using GTSErpSystem.Frms.OrdersTransfer;
using GTSErpSystem.Frms.Payroll;
using GTSErpSystem.Frms.Reports.Account;
using GTSErpSystem.Frms.Reports.AllBranch;
using GTSErpSystem.Frms.Reports.Gold;
using GTSErpSystem.Frms.Reports.Orders;
using GTSErpSystem.Frms.Reports.Profit;
using GTSErpSystem.Frms.Reports.Public;
using GTSErpSystem.Frms.Reports.Purches;
using GTSErpSystem.Frms.Reports.SalesMan;
using GTSErpSystem.Frms.Reports.Stores;
using GTSErpSystem.Frms.Security;
using GTSErpSystem.Frms.Transportation;
using GTSErpSystem.Properties;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.FrmsHome;

public class FrmGoldPlusTransportation : RibbonForm
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Setting STTG = new Class_Setting();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	private TileItem item1 = new TileItem();

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

	private BarButtonItem BtnBarCode;

	private BarButtonItem BtnBounce;

	private BarButtonItem BtnRPTBounce;

	private BarButtonItem BtnReportAllOrdersSafryMahly;

	private BarButtonItem BtnRPProfitByItemsItemsafryMahly;

	private BarButtonItem BtnRPMovementItemActive;

	private BarButtonItem BtnPaymentItems;

	private RibbonPageGroup ribbonPageGroup29;

	private BarButtonItem BtnRptStoreItemLimit;

	private BarButtonItem BtnAllPrinter;

	private BarButtonItem BtnDoctor;

	private BarButtonItem BtnSending;

	private RibbonPage ribbonPage9;

	private RibbonPageGroup ribbonPageGroup30;

	private BarButtonItem BtnDalala;

	private BarButtonItem BtnReportErsalaya;

	private BarButtonItem BtnReportDalala;

	private RibbonPageGroup ribbonPageGroup31;

	private BarButtonItem barButtonItem3;

	private BarButtonItem BtnOrderBounce;

	private BarButtonItem BtnReportAllOrdersDetailsByGroup;

	private BarButtonItem BtnRent;

	private BarButtonItem BtnRecipetRent;

	private RibbonPageGroup ribbonPageGroup32;

	private BarButtonItem BtnLeadgerSupp;

	private BarButtonItem BtnMizanItem;

	private BarButtonItem BtnOrdersDetailsByProperties;

	private BarButtonItem BtnOrderDaily;

	private BarButtonItem BtnCloseDayByUser;

	private BarButtonItem BtnPharm;

	private BarButtonItem BtnItemManufacturing;

	private BarButtonItem BtnManufacturing;

	private RibbonPage ribbonPage10;

	private RibbonPageGroup ribbonPageGroup33;

	private BarButtonItem BtnAllowances;

	private RibbonPageGroup ribbonPageGroup34;

	private BarButtonItem BtnEntrusted;

	private BarButtonItem BtnEntrustedEmp;

	private BarButtonItem BtnCheckRecipt;

	private BarButtonItem BtnCheckCom;

	private BarButtonItem BtnCheckPayment;

	private BarButtonItem BtnPaymentGo;

	private RibbonPageGroup ribbonPageGroup35;

	private BarButtonItem BtnGard2;

	private BarSubItem barSubItem9;

	private BarButtonItem BtnOrderSales;

	private BarButtonItem BtnOrderDetailsSales;

	private BarButtonItem BtnReturnOrderSales;

	private BarButtonItem BtnReturnOrderDetailsSales;

	private BarButtonItem BtnOrderNetSale;

	private BarButtonItem BtnProfitOrderSales;

	private BarButtonItem BtnProfitByGroupSales;

	private BarButtonItem BtnProfitByItemSales;

	private RibbonPageGroup ribbonPageGroup36;

	private BarButtonItem BtnRPCostCenter;

	private BarButtonItem BtnRPCostCenterByTotal;

	private BarButtonItem BtnCustomerByTotal;

	private BarButtonItem BtnAllItems;

	private BarStaticItem LblStatus;

	private BarButtonItem BtnReplace;

	private BarButtonItem BtnImportExcel;

	private BarButtonItem BtnRecItems;

	private TileControl tileControl1;

	private TileGroup tileGroup2;

	private BarButtonItem BtnProject;

	private BarButtonItem BtnGard;

	private BarButtonItem BtnReportProject;

	private BarButtonItem BtnRPTasfer;

	private BarButtonItem BtnRPSalesPurForSupp;

	private BarButtonItem BtnOil;

	private BarButtonItem BtnOrderDetailsSmall11;

	private BarButtonItem BtnOrderDetailsSmall;

	private BarButtonItem BtnCostCenterTotal;

	private BarButtonItem BtnCashirWithOut;

	private BarButtonItem BtnTrnsferRequest;

	private BarButtonItem BtnTrnsferRequestToBranch;

	private BarButtonItem BtnTrnsferRequestFromBranch;

	private RibbonPageGroup ribbonPageGroup18;

	private BarButtonItem BtnRPCust;

	private BarButtonItem BtnReportAllOrdersCheckOut;

	private BarButtonItem BtnRPMizanCustSupp;

	private BarButtonItem BtnEJAR;

	private BarSubItem barReportGold;

	private BarButtonItem RBGoldPurches;

	private BarButtonItem RBGoldPurchesReturn;

	private BarButtonItem RBGoldOrder;

	private BarButtonItem RBGoldOrderReturn;

	private BarButtonItem RBGoldPurchesNet;

	private BarButtonItem RBGoldOrdersNet;

	private BarButtonItem RBGoldOrdPurchNet;

	private RibbonPageGroup ribbonPageGroup37;

	private BarButtonItem RBGoldOrdersNetPurchOnly;

	public NavigationPage employeesNavigationPage;

	private BarButtonItem BtnDriver;

	private BarButtonItem BtnCustom;

	private BarButtonItem BtnReciver;

	private BarButtonItem BtnSender;

	private BarButtonItem BtnContracter;

	private BarButtonItem BtnCashRecp;

	private BarButtonItem BtnPaymentCash;

	private BarButtonItem BtnLedger;

	private BarButtonItem BtnExpulsion;

	private RibbonPage ribbonPage11;

	private RibbonPageGroup ribbonPageGroup38;

	private RibbonPageGroup ribbonPageGroup39;

	public FrmGoldPlusTransportation()
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
		LblStatus.Caption = "   " + LoginDetails.Status + "   ";
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
			new GTSErpSystem.Frms.Orders.FrmOrders(null).Show();
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
			new GTSErpSystem.Frms.Orders.FrmOrders(null).Show();
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
			else
			{
				Application.Exit();
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
		if (LoginDetails.remembers.Count() <= 0)
		{
			return;
		}
		foreach (TblRemember remember in LoginDetails.remembers)
		{
			if (remember.DateTimeRem.Value.ToLongTimeString() == DateTime.Now.ToLongTimeString())
			{
				tileGroup2.Items.Add(item1);
				item1.Text = remember.NameRem;
				item1.Id = remember.ID;
				item1.ItemSize = TileItemSize.Wide;
				item1.AllowGlyphSkinning = DefaultBoolean.True;
				item1.Appearance.Font = new Font("Tahoma", 12.35f);
				item1.Appearance.BackColor = ColorTranslator.FromHtml(LoginDetails.GroupColor);
				item1.Appearance.ForeColor = ColorTranslator.FromHtml(LoginDetails.GroupFontColor);
				item1.TextAlignment = TileItemContentAlignment.MiddleCenter;
				item1.ItemClick += tileBarItem2_ItemClick;
				TileItemElement element = new TileItemElement();
				item1.Elements.Add(element);
				break;
			}
		}
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
			new GTSErpSystem.Frms.Reports.Orders.FrmAllOrders().Show();
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
			new GTSErpSystem.Frms.Reports.Orders.FrmAllOrederReturn().Show();
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
			new GTSErpSystem.Frms.Reports.Orders.FrmAllOrdersDetails().Show();
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
			new GTSErpSystem.Frms.Reports.Orders.FrmAllOrdersReturnDetails().Show();
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
	}

	private void SetDateApp()
	{
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
			new GTSErpSystem.Frms.Reports.Profit.FrmAllOrdersProfit().Show();
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
			string text = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + text);
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
			string text = LoginDetails.passServer;
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + text);
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
			frmRPLeadger.Show();
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
			frmRPLeadger.Show();
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
			new GTSErpSystem.Frms.Reports.Profit.FrmAllOrdersProfitByItems_ItemOnly().Show();
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 98))
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
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 99))
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
			new FrmTransferFromBranch().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnBarCode_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmBarCodeItem().Show();
	}

	private void BtnBounce_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnRPTBounce_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 100))
		{
			new GTSErpSystem.Frms.Contract.Report.FrmBounce().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllOrdersSafryMahly_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 100))
		{
			new FrmAllOrdersCashierSafryMahly().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPProfitByItemsItemsafryMahly_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 101))
		{
			new FrmAllOrdersProfitCashierSafryMahly().Show();
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

	private void BtnPaymentItems_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 103))
		{
			new FrmPaymentItem().Show();
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
			new FrmAllPrinters().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnDoctor_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 105))
		{
			new FrmDoctor().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnSending_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 106))
		{
			new FrmSending().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnDalala_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 107))
		{
			new FrmDalala().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportErsalaya_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 109))
		{
			new FrmReportOrderDalala_Sending().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportDalala_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 108))
		{
			new FrmReportOrderDalala_Dalala().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderBounce_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 110))
		{
			new FrmAllOrdersBounce().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportAllOrdersDetailsByGroup_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 96))
		{
			new FrmAllOrdersDetailsByGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRent_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 111))
		{
			new FrmRent().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRecipetRent_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 112))
		{
			new FrmRecipt().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnLeadgerSupp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 113))
		{
			new FrmRPLeadgerSupp().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnMizanItem_ItemClick(object sender, ItemClickEventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["DataTableItemMizan"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Get_MizanBarcode", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlDataAdapter.Fill(dataSetALL.DataTableItemMizan);
		sqlConnection.Close();
		if (dataSetALL.Tables["DataTableItemMizan"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			stiReport.Load(Application.StartupPath + "\\Reports\\ReportItemMizan.mrt");
			stiReport.RegData(dataSetALL.DataTableItemMizan);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport.Render();
			stiReport.Show(dialogForm: true);
		}
	}

	private void BtnOrdersDetailsByProperties_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 115))
		{
			new FrmAllOrdersDetailsByProperties().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderDaily_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnCloseDayByUser_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 117))
		{
			new FrmCloseDayByUser().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPharm_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashirPharm().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnItemManufacturing_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 118))
		{
			new FrmManufacturingOrder().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnManufacturing_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 119))
		{
			new FrmManufacturing().Show();
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

	private void BtnEntrusted_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 121))
		{
			new FrmEntrusted().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnEntrustedEmp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 122))
		{
			new FrmEntrustedEmployee().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCheckRecipt_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 123))
		{
			new FrmReceiptCheck().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCheckCom_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 124))
		{
			new FrmCheckCollection().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCheckPayment_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 125))
		{
			new FrmCheckIssued().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnPaymentGo_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 126))
		{
			new FrmCheckCollectionIssued().Show();
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

	private void BtnOrderSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 127))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrders().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderDetailsSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 128))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrdersDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReturnOrderSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 129))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrederReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReturnOrderDetailsSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 130))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrdersReturnDetails().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOrderNetSale_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 131))
		{
			new FrmNetProfitSalesMan().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnProfitOrderSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 132))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrdersProfit().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnProfitByGroupSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 133))
		{
			new FrmAllOrdersProfitByGroupBy().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnProfitByItemSales_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 134))
		{
			new GTSErpSystem.Frms.Reports.SalesMan.FrmAllOrdersProfitByItems_ItemOnly().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPCostCenter_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 134))
		{
			new FrmRPCostCenter().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPCostCenterByTotal_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 134))
		{
			new FrmRPCostCenter().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCustomerByTotal_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 51))
		{
			Class_Account_CustSupp class_Account_CustSupp = new Class_Account_CustSupp();
			class_Account_CustSupp.GetAllCustmerByTotal();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnAllItems_ItemClick(object sender, ItemClickEventArgs e)
	{
		Class_Items class_Items = new Class_Items();
		class_Items.GetAllItems();
	}

	private void BtnReplace_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 17))
		{
			new FrmReplace().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnImportExcel_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 137))
		{
			new GTSErpSystem.Frms.Items.FrmImportExcel().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRecItems_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 138))
		{
			new FrmRecpItem().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
	{
		int itemid = e.Item.Id;
		TblRemember tblRemember = LoginDetails.remembers.FirstOrDefault((TblRemember s) => s.ID == itemid);
		if (tblRemember != null && MessageBox.Show("هل تريد التأكيد والحذف من التنيهات طں", "التنبيه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			using (GTSdbEntities gTSdbEntities = new GTSdbEntities())
			{
				tileGroup2.Items.Remove(item1);
				gTSdbEntities.TblRemembers.Attach(tblRemember);
				gTSdbEntities.TblRemembers.Remove(tblRemember);
				LoginDetails.remembers.Remove(tblRemember);
				gTSdbEntities.SaveChanges();
			}
		}
	}

	private void BtnProject_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 139))
		{
			new FrmProjects().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnGard_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 42))
		{
			new FrmStoreGardCat().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnReportProject_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 140))
		{
			new FrmRPProject().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPTasfer_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 141))
		{
			new FrmRPTasferByGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPSalesPurForSupp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 142))
		{
			new FrmRpSalesPurchesForSupp().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnOil_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmOilChange().Show();
	}

	private void BtnOrderDetailsSmall_ItemClick_1(object sender, ItemClickEventArgs e)
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

	private void BtnCashirWithOut_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 12))
		{
			new FrmCashirWithOutGroup().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTrnsferRequest_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 144))
		{
			new FrmTransferBranchMaster().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTrnsferRequestToBranch_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 145))
		{
			new FrmTransferMasterToBranch().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnTrnsferRequestFromBranch_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (!CheckPermession.CheckUserPage(LoginDetails.GroupID, 146))
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPCust_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnReportAllOrdersCheckOut_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 46))
		{
			new FrmAllOrdersCheckOut().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnRPMizanCustSupp_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 51))
		{
			new FrmRPMizanCustSupp().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnEJAR_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 147))
		{
			new FrmEJAR().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldOrder_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 148))
		{
			new GTSErpSystem.Frms.Reports.Gold.FrmOrders().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldPurches_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 149))
		{
			new FrmPurches().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldPurchesReturn_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 150))
		{
			new FrmPurchesReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldOrderReturn_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 151))
		{
			new FrmOrdersReturn().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldPurchesNet_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 152))
		{
			new FrmNetPurches().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldOrdersNet_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 153))
		{
			new FrmNetOrders().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldOrdPurchNet_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 154))
		{
			new FrmNetPurchOrders().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void RBGoldOrdersNetPurchOnly_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 155))
		{
			new FrmOrdersNetPurchOnly().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnDriver_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 97))
		{
			new FrmDrivers().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCustom_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnReciver_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmReceive().Show();
	}

	private void BtnSender_ItemClick(object sender, ItemClickEventArgs e)
	{
		new FrmResend().Show();
	}

	private void BtnContracter_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 98))
		{
			new FrmContract().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnCashRecp_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnPaymentCash_ItemClick(object sender, ItemClickEventArgs e)
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

	private void BtnLedger_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 43))
		{
			new FrmRPLeadgerCar().Show();
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnExpulsion_ItemClick(object sender, ItemClickEventArgs e)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 43))
		{
			new FrmRPDeportation().Show();
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
		DevExpress.Skins.SkinPaddingEdges skinPaddingEdges = new DevExpress.Skins.SkinPaddingEdges();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsHome.FrmGoldPlusTransportation));
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
		this.BtnRPCostCenterByTotal = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCostCenterTotal = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportProject = new DevExpress.XtraBars.BarButtonItem();
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
		this.BtnOrderDaily = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllMovementDays = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportVAT = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportTobaccoTax = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCloseDayByUser = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportStore = new DevExpress.XtraBars.BarSubItem();
		this.BtnAllItems = new DevExpress.XtraBars.BarButtonItem();
		this.BtnGard2 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPGard = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMovementItemActive = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMovementItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPStoreAvergByGroup = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRptStoreItemLimit = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPTasfer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPrinters = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAbout = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportOrder = new DevExpress.XtraBars.BarSubItem();
		this.BtnReportAllOrders = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturn = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersReturnDetails = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportSalesEmployee = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersSafryMahly = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderBounce = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersDetailsByGroup = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrdersDetailsByProperties = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderDetailsSmall = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportAllOrdersCheckOut = new DevExpress.XtraBars.BarButtonItem();
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
		this.BtnRPProfitByItemsItemsafryMahly = new DevExpress.XtraBars.BarButtonItem();
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
		this.BtnRPCust = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCustomerByTotal = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPLeadgerCustomer = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPLeadgerSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnLeadgerSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPSalesPurForSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPMizanCustSupp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPCustomer = new DevExpress.XtraBars.BarButtonItem();
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
		this.BtnRPTBounce = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemVat = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferToBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferFromBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnBarCode = new DevExpress.XtraBars.BarButtonItem();
		this.BtnBounce = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPaymentItems = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAllPrinter = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDoctor = new DevExpress.XtraBars.BarButtonItem();
		this.BtnSending = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDalala = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportErsalaya = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReportDalala = new DevExpress.XtraBars.BarButtonItem();
		this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRent = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRecipetRent = new DevExpress.XtraBars.BarButtonItem();
		this.BtnMizanItem = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPharm = new DevExpress.XtraBars.BarButtonItem();
		this.BtnItemManufacturing = new DevExpress.XtraBars.BarButtonItem();
		this.BtnManufacturing = new DevExpress.XtraBars.BarButtonItem();
		this.BtnAllowances = new DevExpress.XtraBars.BarButtonItem();
		this.BtnEntrusted = new DevExpress.XtraBars.BarButtonItem();
		this.BtnEntrustedEmp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCheckRecipt = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCheckCom = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCheckPayment = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPaymentGo = new DevExpress.XtraBars.BarButtonItem();
		this.barSubItem9 = new DevExpress.XtraBars.BarSubItem();
		this.BtnOrderSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderDetailsSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReturnOrderSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReturnOrderDetailsSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnProfitOrderSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnProfitByGroupSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnProfitByItemSales = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderNetSale = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRPCostCenter = new DevExpress.XtraBars.BarButtonItem();
		this.LblStatus = new DevExpress.XtraBars.BarStaticItem();
		this.BtnReplace = new DevExpress.XtraBars.BarButtonItem();
		this.BtnImportExcel = new DevExpress.XtraBars.BarButtonItem();
		this.BtnRecItems = new DevExpress.XtraBars.BarButtonItem();
		this.BtnProject = new DevExpress.XtraBars.BarButtonItem();
		this.BtnGard = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOil = new DevExpress.XtraBars.BarButtonItem();
		this.BtnOrderDetailsSmall11 = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCashirWithOut = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferRequest = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferRequestToBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnTrnsferRequestFromBranch = new DevExpress.XtraBars.BarButtonItem();
		this.BtnEJAR = new DevExpress.XtraBars.BarButtonItem();
		this.barReportGold = new DevExpress.XtraBars.BarSubItem();
		this.RBGoldPurches = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldPurchesReturn = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldOrder = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldOrderReturn = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldPurchesNet = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldOrdersNet = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldOrdPurchNet = new DevExpress.XtraBars.BarButtonItem();
		this.RBGoldOrdersNetPurchOnly = new DevExpress.XtraBars.BarButtonItem();
		this.BtnDriver = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCustom = new DevExpress.XtraBars.BarButtonItem();
		this.BtnReciver = new DevExpress.XtraBars.BarButtonItem();
		this.BtnSender = new DevExpress.XtraBars.BarButtonItem();
		this.BtnContracter = new DevExpress.XtraBars.BarButtonItem();
		this.BtnCashRecp = new DevExpress.XtraBars.BarButtonItem();
		this.BtnPaymentCash = new DevExpress.XtraBars.BarButtonItem();
		this.BtnLedger = new DevExpress.XtraBars.BarButtonItem();
		this.BtnExpulsion = new DevExpress.XtraBars.BarButtonItem();
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
		this.ribbonPageGroup29 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup32 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup18 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage11 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup38 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup39 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage9 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup30 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup31 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup34 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage7 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup25 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup26 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup11 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup35 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage10 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup33 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
		this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup19 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup12 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup13 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup14 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup15 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup36 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup16 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup24 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
		this.ribbonPageGroup37 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
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
		this.tileControl1 = new DevExpress.XtraEditors.TileControl();
		this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
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
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).BeginInit();
		base.SuspendLayout();
		this.ribbonControl.ApplicationButtonDropDownControl = this.applicationMenu1;
		this.ribbonControl.ApplicationButtonImageOptions.Image = GTSErpSystem.Properties.Resources.rsz_infographic;
		this.ribbonControl.ExpandCollapseItem.Id = 0;
		this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[236]
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
			this.BtnBarCode,
			this.BtnBounce,
			this.BtnRPTBounce,
			this.BtnReportAllOrdersSafryMahly,
			this.BtnRPProfitByItemsItemsafryMahly,
			this.BtnRPMovementItemActive,
			this.BtnPaymentItems,
			this.BtnRptStoreItemLimit,
			this.BtnAllPrinter,
			this.BtnDoctor,
			this.BtnSending,
			this.BtnDalala,
			this.BtnReportErsalaya,
			this.BtnReportDalala,
			this.barButtonItem3,
			this.BtnOrderBounce,
			this.BtnReportAllOrdersDetailsByGroup,
			this.BtnRent,
			this.BtnRecipetRent,
			this.BtnLeadgerSupp,
			this.BtnMizanItem,
			this.BtnOrdersDetailsByProperties,
			this.BtnOrderDaily,
			this.BtnCloseDayByUser,
			this.BtnPharm,
			this.BtnItemManufacturing,
			this.BtnManufacturing,
			this.BtnAllowances,
			this.BtnEntrusted,
			this.BtnEntrustedEmp,
			this.BtnCheckRecipt,
			this.BtnCheckCom,
			this.BtnCheckPayment,
			this.BtnPaymentGo,
			this.BtnGard2,
			this.barSubItem9,
			this.BtnOrderSales,
			this.BtnOrderDetailsSales,
			this.BtnReturnOrderSales,
			this.BtnReturnOrderDetailsSales,
			this.BtnOrderNetSale,
			this.BtnProfitOrderSales,
			this.BtnProfitByItemSales,
			this.BtnProfitByGroupSales,
			this.BtnRPCostCenter,
			this.BtnRPCostCenterByTotal,
			this.BtnCustomerByTotal,
			this.BtnAllItems,
			this.LblStatus,
			this.BtnReplace,
			this.BtnImportExcel,
			this.BtnRecItems,
			this.BtnProject,
			this.BtnGard,
			this.BtnReportProject,
			this.BtnRPTasfer,
			this.BtnRPSalesPurForSupp,
			this.BtnOil,
			this.BtnOrderDetailsSmall11,
			this.BtnOrderDetailsSmall,
			this.BtnCostCenterTotal,
			this.BtnCashirWithOut,
			this.BtnTrnsferRequest,
			this.BtnTrnsferRequestToBranch,
			this.BtnTrnsferRequestFromBranch,
			this.BtnRPCust,
			this.BtnReportAllOrdersCheckOut,
			this.BtnRPMizanCustSupp,
			this.BtnEJAR,
			this.barReportGold,
			this.RBGoldPurches,
			this.RBGoldPurchesReturn,
			this.RBGoldOrder,
			this.RBGoldOrderReturn,
			this.RBGoldPurchesNet,
			this.RBGoldOrdersNet,
			this.RBGoldOrdPurchNet,
			this.RBGoldOrdersNetPurchOnly,
			this.BtnDriver,
			this.BtnCustom,
			this.BtnReciver,
			this.BtnSender,
			this.BtnContracter,
			this.BtnCashRecp,
			this.BtnPaymentCash,
			this.BtnLedger,
			this.BtnExpulsion
		});
		resources.ApplyResources(this.ribbonControl, "ribbonControl");
		this.ribbonControl.MaxItemId = 288;
		this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
		this.ribbonControl.Name = "ribbonControl";
		this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[12]
		{
			this.ribbonPage, this.ribbonPage1, this.ribbonPage8, this.ribbonPage11, this.ribbonPage9, this.ribbonPage3, this.ribbonPage7, this.ribbonPage2, this.ribbonPage10, this.ribbonPage4,
			this.ribbonPage5, this.ribbonPage6
		});
		this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemColorEdit1, this.repositoryItemColorPickEdit1, this.repositoryItemColorEdit2, this.repositoryItemDateEdit1, this.repositoryItemDateEdit2, this.repositoryItemDateEdit3, this.repositoryItemTimeSpanEdit1, this.repositoryItemTimeEdit1, this.repositoryItemDateEdit4 });
		this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
		this.ribbonControl.StatusBar = this.ribbonStatusBar;
		this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
		this.applicationMenu1.Name = "applicationMenu1";
		this.applicationMenu1.Ribbon = this.ribbonControl;
		this.applicationMenu1.ShowRightPane = true;
		this.skinRibbonGalleryBarItem.Gallery.AllowHoverImages = true;
		this.skinRibbonGalleryBarItem.Gallery.ColumnCount = 4;
		this.skinRibbonGalleryBarItem.Gallery.FixedHoverImageSize = false;
		this.skinRibbonGalleryBarItem.Gallery.ImageSize = new System.Drawing.Size(14, 13);
		this.skinRibbonGalleryBarItem.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
		this.skinRibbonGalleryBarItem.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Squeeze;
		this.skinRibbonGalleryBarItem.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
		skinPaddingEdges.Left = 7;
		skinPaddingEdges.Right = 7;
		this.skinRibbonGalleryBarItem.Gallery.ItemImagePadding = skinPaddingEdges;
		this.skinRibbonGalleryBarItem.Id = 14;
		this.skinRibbonGalleryBarItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("skinRibbonGalleryBarItem.ImageOptions.SvgImage");
		this.skinRibbonGalleryBarItem.Name = "skinRibbonGalleryBarItem";
		this.employeesBarButtonItem.Id = 80;
		this.employeesBarButtonItem.Name = "employeesBarButtonItem";
		this.customersBarButtonItem.Id = 81;
		this.customersBarButtonItem.Name = "customersBarButtonItem";
		resources.ApplyResources(this.skinRibbonGalleryBarItem1, "skinRibbonGalleryBarItem1");
		this.skinRibbonGalleryBarItem1.Gallery.AllowHoverImages = true;
		this.skinRibbonGalleryBarItem1.Gallery.ColumnCount = 4;
		this.skinRibbonGalleryBarItem1.Gallery.FixedHoverImageSize = false;
		this.skinRibbonGalleryBarItem1.Gallery.ImageSize = new System.Drawing.Size(14, 13);
		this.skinRibbonGalleryBarItem1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Squeeze;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
		skinPaddingEdges2.Left = 7;
		skinPaddingEdges2.Right = 7;
		this.skinRibbonGalleryBarItem1.Gallery.ItemImagePadding = skinPaddingEdges2;
		this.skinRibbonGalleryBarItem1.Id = 46;
		this.skinRibbonGalleryBarItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("skinRibbonGalleryBarItem1.ImageOptions.SvgImage");
		this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
		resources.ApplyResources(this.barSubItem4, "barSubItem4");
		this.barSubItem4.Id = 48;
		this.barSubItem4.Name = "barSubItem4";
		this.barSubItem4.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnORDOpenQuantity, "BtnORDOpenQuantity");
		this.BtnORDOpenQuantity.Id = 51;
		this.BtnORDOpenQuantity.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOpenQuantity.ImageOptions.Image");
		this.BtnORDOpenQuantity.Name = "BtnORDOpenQuantity";
		this.BtnORDOpenQuantity.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOpenQuantity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOpenQuantity_ItemClick);
		resources.ApplyResources(this.BtnORDPriceOffer, "BtnORDPriceOffer");
		this.BtnORDPriceOffer.Id = 52;
		this.BtnORDPriceOffer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDPriceOffer.ImageOptions.Image");
		this.BtnORDPriceOffer.Name = "BtnORDPriceOffer";
		this.BtnORDPriceOffer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPriceOffer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPriceOffer_ItemClick);
		resources.ApplyResources(this.BtnORDOrder, "BtnORDOrder");
		this.BtnORDOrder.Id = 53;
		this.BtnORDOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrder.ImageOptions.Image");
		this.BtnORDOrder.Name = "BtnORDOrder";
		this.BtnORDOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrder_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseOrder, "BtnORDPurchaseOrder");
		this.BtnORDPurchaseOrder.Id = 54;
		this.BtnORDPurchaseOrder.ImageOptions.Image = GTSErpSystem.Properties.Resources.insert_32x32;
		this.BtnORDPurchaseOrder.Name = "BtnORDPurchaseOrder";
		this.BtnORDPurchaseOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseOrder_ItemClick);
		resources.ApplyResources(this.BtnORDPurchase, "BtnORDPurchase");
		this.BtnORDPurchase.Id = 55;
		this.BtnORDPurchase.ImageOptions.Image = GTSErpSystem.Properties.Resources.buy_32x32;
		this.BtnORDPurchase.Name = "BtnORDPurchase";
		this.BtnORDPurchase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchase_ItemClick);
		resources.ApplyResources(this.BtnORDOrderReturnID, "BtnORDOrderReturnID");
		this.BtnORDOrderReturnID.Id = 56;
		this.BtnORDOrderReturnID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnID.ImageOptions.Image");
		this.BtnORDOrderReturnID.Name = "BtnORDOrderReturnID";
		this.BtnORDOrderReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnID_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseReturnID, "BtnORDPurchaseReturnID");
		this.BtnORDPurchaseReturnID.Id = 57;
		this.BtnORDPurchaseReturnID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deletegroupfooter_32x32;
		this.BtnORDPurchaseReturnID.Name = "BtnORDPurchaseReturnID";
		this.BtnORDPurchaseReturnID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnID_ItemClick);
		resources.ApplyResources(this.BtnORDOrderReturnNotID, "BtnORDOrderReturnNotID");
		this.BtnORDOrderReturnNotID.Id = 58;
		this.BtnORDOrderReturnNotID.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnORDOrderReturnNotID.ImageOptions.Image");
		this.BtnORDOrderReturnNotID.Name = "BtnORDOrderReturnNotID";
		this.BtnORDOrderReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDOrderReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDOrderReturnNotID_ItemClick);
		resources.ApplyResources(this.BtnORDPurchaseReturnNotID, "BtnORDPurchaseReturnNotID");
		this.BtnORDPurchaseReturnNotID.Id = 59;
		this.BtnORDPurchaseReturnNotID.ImageOptions.Image = GTSErpSystem.Properties.Resources.deleteheader_32x32;
		this.BtnORDPurchaseReturnNotID.Name = "BtnORDPurchaseReturnNotID";
		this.BtnORDPurchaseReturnNotID.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDPurchaseReturnNotID.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDPurchaseReturnNotID_ItemClick);
		resources.ApplyResources(this.barButtonItem10, "barButtonItem10");
		this.barButtonItem10.Id = 60;
		this.barButtonItem10.Name = "barButtonItem10";
		resources.ApplyResources(this.barButtonItem11, "barButtonItem11");
		this.barButtonItem11.Id = 61;
		this.barButtonItem11.Name = "barButtonItem11";
		this.barButtonItem11.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem12, "barButtonItem12");
		this.barButtonItem12.Id = 62;
		this.barButtonItem12.Name = "barButtonItem12";
		this.barButtonItem12.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem13, "barButtonItem13");
		this.barButtonItem13.Id = 63;
		this.barButtonItem13.Name = "barButtonItem13";
		this.barButtonItem13.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem14, "barButtonItem14");
		this.barButtonItem14.Id = 64;
		this.barButtonItem14.Name = "barButtonItem14";
		this.barButtonItem14.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem15, "barButtonItem15");
		this.barButtonItem15.Id = 65;
		this.barButtonItem15.Name = "barButtonItem15";
		this.barButtonItem15.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnItemGroups, "BtnItemGroups");
		this.BtnItemGroups.Id = 66;
		this.BtnItemGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.group_32x32;
		this.BtnItemGroups.Name = "BtnItemGroups";
		this.BtnItemGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGroups_ItemClick);
		resources.ApplyResources(this.BtnItemUnits, "BtnItemUnits");
		this.BtnItemUnits.Id = 67;
		this.BtnItemUnits.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnItemUnits.ImageOptions.Image");
		this.BtnItemUnits.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnItemUnits.ImageOptions.LargeImage");
		this.BtnItemUnits.Name = "BtnItemUnits";
		this.BtnItemUnits.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemUnits.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemUnits_ItemClick);
		resources.ApplyResources(this.BtnItemClass, "BtnItemClass");
		this.BtnItemClass.Id = 68;
		this.BtnItemClass.ImageOptions.Image = GTSErpSystem.Properties.Resources.expandcollapse_32x32;
		this.BtnItemClass.Name = "BtnItemClass";
		this.BtnItemClass.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemClass.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemClass_ItemClick);
		resources.ApplyResources(this.BtnItemCompany, "BtnItemCompany");
		this.BtnItemCompany.Id = 69;
		this.BtnItemCompany.ImageOptions.Image = GTSErpSystem.Properties.Resources.home_32x32;
		this.BtnItemCompany.Name = "BtnItemCompany";
		this.BtnItemCompany.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCompany.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCompany_ItemClick);
		resources.ApplyResources(this.BtnItem, "BtnItem");
		this.BtnItem.Id = 70;
		this.BtnItem.ImageOptions.Image = GTSErpSystem.Properties.Resources.mailmerge_32x32;
		this.BtnItem.Name = "BtnItem";
		this.BtnItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItem_ItemClick);
		resources.ApplyResources(this.BtnStores, "BtnStores");
		this.BtnStores.Id = 71;
		this.BtnStores.ImageOptions.Image = GTSErpSystem.Properties.Resources.autoexpand_32x32;
		this.BtnStores.Name = "BtnStores";
		this.BtnStores.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnStores.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnStores_ItemClick);
		resources.ApplyResources(this.BtnBranches, "BtnBranches");
		this.BtnBranches.Enabled = false;
		this.BtnBranches.Id = 72;
		this.BtnBranches.ImageOptions.Image = GTSErpSystem.Properties.Resources.documentmap_32x32;
		this.BtnBranches.Name = "BtnBranches";
		this.BtnBranches.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBranches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBranches_ItemClick);
		resources.ApplyResources(this.barButtonItem23, "barButtonItem23");
		this.barButtonItem23.Id = 73;
		this.barButtonItem23.Name = "barButtonItem23";
		this.barButtonItem23.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.barButtonItem24, "barButtonItem24");
		this.barButtonItem24.Id = 74;
		this.barButtonItem24.Name = "barButtonItem24";
		this.barButtonItem24.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnCusPlace, "BtnCusPlace");
		this.BtnCusPlace.Id = 75;
		this.BtnCusPlace.ImageOptions.Image = GTSErpSystem.Properties.Resources.language_32x32;
		this.BtnCusPlace.Name = "BtnCusPlace";
		this.BtnCusPlace.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusPlace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusPlace_ItemClick);
		resources.ApplyResources(this.BtnCustomer, "BtnCustomer");
		this.BtnCustomer.Id = 76;
		this.BtnCustomer.ImageOptions.Image = GTSErpSystem.Properties.Resources.employee_32x32;
		this.BtnCustomer.Name = "BtnCustomer";
		this.BtnCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCustomer_ItemClick);
		resources.ApplyResources(this.BtnCusSupplier, "BtnCusSupplier");
		this.BtnCusSupplier.Id = 77;
		this.BtnCusSupplier.ImageOptions.Image = GTSErpSystem.Properties.Resources.bocustomer_32x32;
		this.BtnCusSupplier.Name = "BtnCusSupplier";
		this.BtnCusSupplier.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSupplier.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSupplier_ItemClick);
		resources.ApplyResources(this.BtnCusSalesMan, "BtnCusSalesMan");
		this.BtnCusSalesMan.Id = 79;
		this.BtnCusSalesMan.ImageOptions.Image = GTSErpSystem.Properties.Resources.meeting_32x32;
		this.BtnCusSalesMan.Name = "BtnCusSalesMan";
		this.BtnCusSalesMan.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCusSalesMan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCusSalesMan_ItemClick);
		resources.ApplyResources(this.BtnUsers, "BtnUsers");
		this.BtnUsers.Id = 82;
		this.BtnUsers.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.Image");
		this.BtnUsers.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnUsers.ImageOptions.LargeImage");
		this.BtnUsers.Name = "BtnUsers";
		this.BtnUsers.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUsers_ItemClick);
		resources.ApplyResources(this.BtnGroups, "BtnGroups");
		this.BtnGroups.Id = 83;
		this.BtnGroups.ImageOptions.Image = GTSErpSystem.Properties.Resources.Groups;
		this.BtnGroups.Name = "BtnGroups";
		this.BtnGroups.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGroups_ItemClick);
		resources.ApplyResources(this.BtnPermission, "BtnPermission");
		this.BtnPermission.Id = 84;
		this.BtnPermission.ImageOptions.Image = GTSErpSystem.Properties.Resources.Permission;
		this.BtnPermission.Name = "BtnPermission";
		this.BtnPermission.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPermission.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPermission_ItemClick);
		resources.ApplyResources(this.BtnCashir, "BtnCashir");
		this.BtnCashir.Id = 85;
		this.BtnCashir.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCashir.ImageOptions.Image");
		this.BtnCashir.Name = "BtnCashir";
		this.BtnCashir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCashir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCashir_ItemClick);
		resources.ApplyResources(this.BtnRecipt, "BtnRecipt");
		this.BtnRecipt.Id = 86;
		this.BtnRecipt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRecipt.ImageOptions.Image");
		this.BtnRecipt.Name = "BtnRecipt";
		this.BtnRecipt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRecipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRecipt_ItemClick);
		resources.ApplyResources(this.BtnPayment, "BtnPayment");
		this.BtnPayment.Id = 87;
		this.BtnPayment.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayment.ImageOptions.Image");
		this.BtnPayment.Name = "BtnPayment";
		this.BtnPayment.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayment_ItemClick);
		resources.ApplyResources(this.BtnAccCardAccount, "BtnAccCardAccount");
		this.BtnAccCardAccount.Id = 88;
		this.BtnAccCardAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCardAccount.ImageOptions.Image");
		this.BtnAccCardAccount.Name = "BtnAccCardAccount";
		this.BtnAccCardAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCardAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCardAccount_ItemClick);
		resources.ApplyResources(this.BtnAccTreeAccount, "BtnAccTreeAccount");
		this.BtnAccTreeAccount.Id = 89;
		this.BtnAccTreeAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccTreeAccount.ImageOptions.Image");
		this.BtnAccTreeAccount.Name = "BtnAccTreeAccount";
		this.BtnAccTreeAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccTreeAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccTreeAccount_ItemClick);
		resources.ApplyResources(this.BtnAccCostCenter, "BtnAccCostCenter");
		this.BtnAccCostCenter.Id = 90;
		this.BtnAccCostCenter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccCostCenter.ImageOptions.Image");
		this.BtnAccCostCenter.Name = "BtnAccCostCenter";
		this.BtnAccCostCenter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccCostCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccCostCenter_ItemClick);
		resources.ApplyResources(this.BtnAccOpenMoney, "BtnAccOpenMoney");
		this.BtnAccOpenMoney.Id = 91;
		this.BtnAccOpenMoney.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccOpenMoney.ImageOptions.Image");
		this.BtnAccOpenMoney.Name = "BtnAccOpenMoney";
		this.BtnAccOpenMoney.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccOpenMoney.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccOpenMoney_ItemClick);
		resources.ApplyResources(this.BtnAccRestrictionsTest, "BtnAccRestrictionsTest");
		this.BtnAccRestrictionsTest.Id = 92;
		this.BtnAccRestrictionsTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictionsTest.ImageOptions.Image");
		this.BtnAccRestrictionsTest.Name = "BtnAccRestrictionsTest";
		this.BtnAccRestrictionsTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictionsTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictionsTest_ItemClick);
		resources.ApplyResources(this.BtnAccRestrictions, "BtnAccRestrictions");
		this.BtnAccRestrictions.Id = 93;
		this.BtnAccRestrictions.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAccRestrictions.ImageOptions.Image");
		this.BtnAccRestrictions.Name = "BtnAccRestrictions";
		this.BtnAccRestrictions.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAccRestrictions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAccRestrictions_ItemClick);
		resources.ApplyResources(this.barButtonItem7, "barButtonItem7");
		this.barButtonItem7.Id = 94;
		this.barButtonItem7.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem7.ImageOptions.Image");
		this.barButtonItem7.Name = "barButtonItem7";
		this.barButtonItem7.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem7_ItemClick);
		resources.ApplyResources(this.barButtonItem8, "barButtonItem8");
		this.barButtonItem8.Id = 95;
		this.barButtonItem8.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem8.ImageOptions.Image");
		this.barButtonItem8.Name = "barButtonItem8";
		this.barButtonItem8.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem8_ItemClick);
		resources.ApplyResources(this.BtnReportAccount, "BtnReportAccount");
		this.BtnReportAccount.Id = 96;
		this.BtnReportAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAccount.ImageOptions.Image");
		this.BtnReportAccount.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[10]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitAccount),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizan),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanGroups),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPTranTeacher),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportLedger),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPCostCenterByTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnCostCenterTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportProject)
		});
		this.BtnReportAccount.Name = "BtnReportAccount";
		this.BtnReportAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnReportMizan, "BtnReportMizan");
		this.BtnReportMizan.Id = 97;
		this.BtnReportMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.Image");
		this.BtnReportMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportMizan.ImageOptions.LargeImage");
		this.BtnReportMizan.Name = "BtnReportMizan";
		this.BtnReportMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportMizan_ItemClick);
		resources.ApplyResources(this.BtnRPProfitAccount, "BtnRPProfitAccount");
		this.BtnRPProfitAccount.Id = 98;
		this.BtnRPProfitAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.Image");
		this.BtnRPProfitAccount.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitAccount.ImageOptions.LargeImage");
		this.BtnRPProfitAccount.Name = "BtnRPProfitAccount";
		this.BtnRPProfitAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitAccount_ItemClick);
		resources.ApplyResources(this.BtnRPMizan, "BtnRPMizan");
		this.BtnRPMizan.Id = 149;
		this.BtnRPMizan.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.Image");
		this.BtnRPMizan.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizan.ImageOptions.LargeImage");
		this.BtnRPMizan.Name = "BtnRPMizan";
		this.BtnRPMizan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizan_ItemClick);
		resources.ApplyResources(this.BtnRPMizanTotal, "BtnRPMizanTotal");
		this.BtnRPMizanTotal.Id = 148;
		this.BtnRPMizanTotal.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.Image");
		this.BtnRPMizanTotal.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanTotal.ImageOptions.LargeImage");
		this.BtnRPMizanTotal.Name = "BtnRPMizanTotal";
		this.BtnRPMizanTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanTotal_ItemClick);
		resources.ApplyResources(this.BtnRPMizanGroups, "BtnRPMizanGroups");
		this.BtnRPMizanGroups.Id = 150;
		this.BtnRPMizanGroups.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.Image");
		this.BtnRPMizanGroups.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanGroups.ImageOptions.LargeImage");
		this.BtnRPMizanGroups.Name = "BtnRPMizanGroups";
		this.BtnRPMizanGroups.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanGroups_ItemClick);
		resources.ApplyResources(this.BtnRPTranTeacher, "BtnRPTranTeacher");
		this.BtnRPTranTeacher.Id = 162;
		this.BtnRPTranTeacher.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.Image");
		this.BtnRPTranTeacher.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPTranTeacher.ImageOptions.LargeImage");
		this.BtnRPTranTeacher.Name = "BtnRPTranTeacher";
		this.BtnRPTranTeacher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTranTeacher_ItemClick);
		resources.ApplyResources(this.BtnReportLedger, "BtnReportLedger");
		this.BtnReportLedger.Id = 175;
		this.BtnReportLedger.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.Image");
		this.BtnReportLedger.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportLedger.ImageOptions.LargeImage");
		this.BtnReportLedger.Name = "BtnReportLedger";
		this.BtnReportLedger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportLedger_ItemClick);
		resources.ApplyResources(this.BtnRPCostCenterByTotal, "BtnRPCostCenterByTotal");
		this.BtnRPCostCenterByTotal.Id = 244;
		this.BtnRPCostCenterByTotal.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCostCenterByTotal.ImageOptions.Image");
		this.BtnRPCostCenterByTotal.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCostCenterByTotal.ImageOptions.LargeImage");
		this.BtnRPCostCenterByTotal.Name = "BtnRPCostCenterByTotal";
		this.BtnRPCostCenterByTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCostCenterByTotal_ItemClick);
		resources.ApplyResources(this.BtnCostCenterTotal, "BtnCostCenterTotal");
		this.BtnCostCenterTotal.Id = 261;
		this.BtnCostCenterTotal.Name = "BtnCostCenterTotal";
		this.BtnCostCenterTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCostCenterByTotal_ItemClick);
		resources.ApplyResources(this.BtnReportProject, "BtnReportProject");
		this.BtnReportProject.Id = 255;
		this.BtnReportProject.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportProject.ImageOptions.Image");
		this.BtnReportProject.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportProject.ImageOptions.LargeImage");
		this.BtnReportProject.Name = "BtnReportProject";
		this.BtnReportProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportProject_ItemClick);
		resources.ApplyResources(this.BtnSetting, "BtnSetting");
		this.BtnSetting.Id = 99;
		this.BtnSetting.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSetting.ImageOptions.Image");
		this.BtnSetting.Name = "BtnSetting";
		this.BtnSetting.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnSetting_ItemClick);
		resources.ApplyResources(this.BtnReportDesign, "BtnReportDesign");
		this.BtnReportDesign.Id = 100;
		this.BtnReportDesign.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportDesign.ImageOptions.Image");
		this.BtnReportDesign.Name = "BtnReportDesign";
		this.BtnReportDesign.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportDesign_ItemClick);
		resources.ApplyResources(this.btnDefualtAccount, "btnDefualtAccount");
		this.btnDefualtAccount.Id = 101;
		this.btnDefualtAccount.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnDefualtAccount.ImageOptions.Image");
		this.btnDefualtAccount.Name = "btnDefualtAccount";
		this.btnDefualtAccount.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.btnDefualtAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnDefualtAccount_ItemClick);
		resources.ApplyResources(this.LblCompany, "LblCompany");
		this.LblCompany.Id = 102;
		this.LblCompany.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblCompany.ItemAppearance.Disabled.Font");
		this.LblCompany.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblCompany.Name = "LblCompany";
		this.LblServer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.LblServer, "LblServer");
		this.LblServer.Id = 103;
		this.LblServer.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblServer.ItemAppearance.Disabled.Font");
		this.LblServer.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblServer.Name = "LblServer";
		this.LblUser.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.LblUser, "LblUser");
		this.LblUser.Id = 104;
		this.LblUser.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblUser.ItemAppearance.Disabled.Font");
		this.LblUser.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblUser.Name = "LblUser";
		this.LblTime.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.LblTime, "LblTime");
		this.LblTime.Id = 105;
		this.LblTime.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblTime.ItemAppearance.Disabled.Font");
		this.LblTime.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblTime.Name = "LblTime";
		this.LblDate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.LblDate, "LblDate");
		this.LblDate.Id = 106;
		this.LblDate.ItemAppearance.Disabled.Font = (System.Drawing.Font)resources.GetObject("LblDate.ItemAppearance.Disabled.Font");
		this.LblDate.ItemAppearance.Disabled.Options.UseFont = true;
		this.LblDate.Name = "LblDate";
		this.barStaticItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.barStaticItem5, "barStaticItem5");
		this.barStaticItem5.Id = 107;
		this.barStaticItem5.Name = "barStaticItem5";
		resources.ApplyResources(this.BtnReportPublic, "BtnReportPublic");
		this.BtnReportPublic.Id = 108;
		this.BtnReportPublic.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportPublic.ImageOptions.Image");
		this.BtnReportPublic.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[6]
		{
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnMovementsDailyReport, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderDaily),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllMovementDays),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportVAT),
			new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BtnReportTobaccoTax, DevExpress.XtraBars.BarItemPaintStyle.Standard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnCloseDayByUser)
		});
		this.BtnReportPublic.Name = "BtnReportPublic";
		this.BtnReportPublic.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnMovementsDailyReport, "BtnMovementsDailyReport");
		this.BtnMovementsDailyReport.Id = 111;
		this.BtnMovementsDailyReport.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.Image");
		this.BtnMovementsDailyReport.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnMovementsDailyReport.ImageOptions.LargeImage");
		this.BtnMovementsDailyReport.Name = "BtnMovementsDailyReport";
		this.BtnMovementsDailyReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMovementsDailyReport_ItemClick);
		resources.ApplyResources(this.BtnOrderDaily, "BtnOrderDaily");
		this.BtnOrderDaily.Id = 221;
		this.BtnOrderDaily.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderDaily.ImageOptions.Image");
		this.BtnOrderDaily.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderDaily.ImageOptions.LargeImage");
		this.BtnOrderDaily.Name = "BtnOrderDaily";
		this.BtnOrderDaily.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDaily_ItemClick);
		resources.ApplyResources(this.BtnReportAllMovementDays, "BtnReportAllMovementDays");
		this.BtnReportAllMovementDays.Id = 165;
		this.BtnReportAllMovementDays.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.Image");
		this.BtnReportAllMovementDays.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllMovementDays.ImageOptions.LargeImage");
		this.BtnReportAllMovementDays.Name = "BtnReportAllMovementDays";
		this.BtnReportAllMovementDays.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllMovementDays_ItemClick);
		resources.ApplyResources(this.BtnReportVAT, "BtnReportVAT");
		this.BtnReportVAT.Id = 128;
		this.BtnReportVAT.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.Image");
		this.BtnReportVAT.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportVAT.ImageOptions.LargeImage");
		this.BtnReportVAT.Name = "BtnReportVAT";
		this.BtnReportVAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportVAT_ItemClick);
		resources.ApplyResources(this.BtnReportTobaccoTax, "BtnReportTobaccoTax");
		this.BtnReportTobaccoTax.Id = 171;
		this.BtnReportTobaccoTax.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.Image");
		this.BtnReportTobaccoTax.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportTobaccoTax.ImageOptions.LargeImage");
		this.BtnReportTobaccoTax.Name = "BtnReportTobaccoTax";
		this.BtnReportTobaccoTax.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportTobaccoTax_ItemClick);
		resources.ApplyResources(this.BtnCloseDayByUser, "BtnCloseDayByUser");
		this.BtnCloseDayByUser.Id = 222;
		this.BtnCloseDayByUser.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCloseDayByUser.ImageOptions.Image");
		this.BtnCloseDayByUser.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnCloseDayByUser.ImageOptions.LargeImage");
		this.BtnCloseDayByUser.Name = "BtnCloseDayByUser";
		this.BtnCloseDayByUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCloseDayByUser_ItemClick);
		resources.ApplyResources(this.BtnReportStore, "BtnReportStore");
		this.BtnReportStore.Id = 109;
		this.BtnReportStore.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportStore.ImageOptions.Image");
		this.BtnReportStore.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[9]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnAllItems),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnGard2),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPGard),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMovementItemActive),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMovementItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPStoreAvergByGroup),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRptStoreItemLimit),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPTasfer)
		});
		this.BtnReportStore.Name = "BtnReportStore";
		this.BtnReportStore.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnAllItems, "BtnAllItems");
		this.BtnAllItems.Id = 246;
		this.BtnAllItems.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAllItems.ImageOptions.Image");
		this.BtnAllItems.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnAllItems.ImageOptions.LargeImage");
		this.BtnAllItems.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAllItems.ImageOptions.SvgImage");
		this.BtnAllItems.Name = "BtnAllItems";
		this.BtnAllItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAllItems_ItemClick);
		resources.ApplyResources(this.BtnGard2, "BtnGard2");
		this.BtnGard2.Id = 233;
		this.BtnGard2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGard2.ImageOptions.Image");
		this.BtnGard2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGard2.ImageOptions.SvgImage");
		this.BtnGard2.Name = "BtnGard2";
		this.BtnGard2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGard2_ItemClick);
		resources.ApplyResources(this.BtnRPGard, "BtnRPGard");
		this.BtnRPGard.Id = 110;
		this.BtnRPGard.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.Image");
		this.BtnRPGard.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPGard.ImageOptions.LargeImage");
		this.BtnRPGard.Name = "BtnRPGard";
		this.BtnRPGard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPGard_ItemClick);
		resources.ApplyResources(this.BtnRPMovementItemActive, "BtnRPMovementItemActive");
		this.BtnRPMovementItemActive.Id = 204;
		this.BtnRPMovementItemActive.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMovementItemActive.ImageOptions.Image");
		this.BtnRPMovementItemActive.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMovementItemActive.ImageOptions.LargeImage");
		this.BtnRPMovementItemActive.Name = "BtnRPMovementItemActive";
		this.BtnRPMovementItemActive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMovementItemActive_ItemClick);
		resources.ApplyResources(this.BtnRPMovementItem, "BtnRPMovementItem");
		this.BtnRPMovementItem.Id = 146;
		this.BtnRPMovementItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.Image");
		this.BtnRPMovementItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMovementItem.ImageOptions.LargeImage");
		this.BtnRPMovementItem.Name = "BtnRPMovementItem";
		this.BtnRPMovementItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMovementItem_ItemClick);
		resources.ApplyResources(this.BtnRPStoreAvergByItem, "BtnRPStoreAvergByItem");
		this.BtnRPStoreAvergByItem.Id = 161;
		this.BtnRPStoreAvergByItem.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.Image");
		this.BtnRPStoreAvergByItem.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByItem.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByItem.Name = "BtnRPStoreAvergByItem";
		this.BtnRPStoreAvergByItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByItem_ItemClick);
		resources.ApplyResources(this.BtnRPStoreAvergByGroup, "BtnRPStoreAvergByGroup");
		this.BtnRPStoreAvergByGroup.Id = 160;
		this.BtnRPStoreAvergByGroup.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.Image");
		this.BtnRPStoreAvergByGroup.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPStoreAvergByGroup.ImageOptions.LargeImage");
		this.BtnRPStoreAvergByGroup.Name = "BtnRPStoreAvergByGroup";
		this.BtnRPStoreAvergByGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPStoreAvergByGroup_ItemClick);
		resources.ApplyResources(this.BtnRptStoreItemLimit, "BtnRptStoreItemLimit");
		this.BtnRptStoreItemLimit.Id = 206;
		this.BtnRptStoreItemLimit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRptStoreItemLimit.ImageOptions.Image");
		this.BtnRptStoreItemLimit.Name = "BtnRptStoreItemLimit";
		this.BtnRptStoreItemLimit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRptStoreItemLimit_ItemClick);
		resources.ApplyResources(this.BtnRPTasfer, "BtnRPTasfer");
		this.BtnRPTasfer.Id = 256;
		this.BtnRPTasfer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPTasfer.ImageOptions.Image");
		this.BtnRPTasfer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPTasfer.ImageOptions.LargeImage");
		this.BtnRPTasfer.Name = "BtnRPTasfer";
		this.BtnRPTasfer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPTasfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTasfer_ItemClick);
		resources.ApplyResources(this.BtnPrinters, "BtnPrinters");
		this.BtnPrinters.Id = 112;
		this.BtnPrinters.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrinters.ImageOptions.Image");
		this.BtnPrinters.Name = "BtnPrinters";
		this.BtnPrinters.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPrinters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPrinters_ItemClick);
		resources.ApplyResources(this.BtnAbout, "BtnAbout");
		this.BtnAbout.Id = 113;
		this.BtnAbout.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAbout.ImageOptions.Image");
		this.BtnAbout.Name = "BtnAbout";
		this.BtnAbout.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAbout_ItemClick);
		resources.ApplyResources(this.BtnReportOrder, "BtnReportOrder");
		this.BtnReportOrder.Id = 114;
		this.BtnReportOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportOrder.ImageOptions.Image");
		this.BtnReportOrder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[11]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrders),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersReturnDetails),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSalesEmployee),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersSafryMahly),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderBounce),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersDetailsByGroup),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrdersDetailsByProperties),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderDetailsSmall),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportAllOrdersCheckOut)
		});
		this.BtnReportOrder.Name = "BtnReportOrder";
		this.BtnReportOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnReportAllOrders, "BtnReportAllOrders");
		this.BtnReportAllOrders.Id = 116;
		this.BtnReportAllOrders.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.Image");
		this.BtnReportAllOrders.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrders.ImageOptions.LargeImage");
		this.BtnReportAllOrders.Name = "BtnReportAllOrders";
		this.BtnReportAllOrders.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrders_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersDetails, "BtnReportAllOrdersDetails");
		this.BtnReportAllOrdersDetails.Id = 118;
		this.BtnReportAllOrdersDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.Image");
		this.BtnReportAllOrdersDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersDetails.Name = "BtnReportAllOrdersDetails";
		this.BtnReportAllOrdersDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersDetails_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersReturn, "BtnReportAllOrdersReturn");
		this.BtnReportAllOrdersReturn.Id = 117;
		this.BtnReportAllOrdersReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.Image");
		this.BtnReportAllOrdersReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturn.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturn.Name = "BtnReportAllOrdersReturn";
		this.BtnReportAllOrdersReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturn_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersReturnDetails, "BtnReportAllOrdersReturnDetails");
		this.BtnReportAllOrdersReturnDetails.Id = 119;
		this.BtnReportAllOrdersReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.Image");
		this.BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllOrdersReturnDetails.Name = "BtnReportAllOrdersReturnDetails";
		this.BtnReportAllOrdersReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersReturnDetails_ItemClick);
		resources.ApplyResources(this.BtnReportSalesEmployee, "BtnReportSalesEmployee");
		this.BtnReportSalesEmployee.Id = 133;
		this.BtnReportSalesEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.Image");
		this.BtnReportSalesEmployee.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportSalesEmployee.ImageOptions.LargeImage");
		this.BtnReportSalesEmployee.Name = "BtnReportSalesEmployee";
		this.BtnReportSalesEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSalesEmployee_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersSafryMahly, "BtnReportAllOrdersSafryMahly");
		this.BtnReportAllOrdersSafryMahly.Id = 202;
		this.BtnReportAllOrdersSafryMahly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersSafryMahly.ImageOptions.Image");
		this.BtnReportAllOrdersSafryMahly.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersSafryMahly.ImageOptions.LargeImage");
		this.BtnReportAllOrdersSafryMahly.Name = "BtnReportAllOrdersSafryMahly";
		this.BtnReportAllOrdersSafryMahly.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnReportAllOrdersSafryMahly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersSafryMahly_ItemClick);
		resources.ApplyResources(this.BtnOrderBounce, "BtnOrderBounce");
		this.BtnOrderBounce.Id = 214;
		this.BtnOrderBounce.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderBounce.ImageOptions.Image");
		this.BtnOrderBounce.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderBounce.ImageOptions.LargeImage");
		this.BtnOrderBounce.Name = "BtnOrderBounce";
		this.BtnOrderBounce.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderBounce_ItemClick);
		resources.ApplyResources(this.BtnReportAllOrdersDetailsByGroup, "BtnReportAllOrdersDetailsByGroup");
		this.BtnReportAllOrdersDetailsByGroup.Id = 215;
		this.BtnReportAllOrdersDetailsByGroup.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetailsByGroup.ImageOptions.Image");
		this.BtnReportAllOrdersDetailsByGroup.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersDetailsByGroup.ImageOptions.LargeImage");
		this.BtnReportAllOrdersDetailsByGroup.Name = "BtnReportAllOrdersDetailsByGroup";
		this.BtnReportAllOrdersDetailsByGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersDetailsByGroup_ItemClick);
		resources.ApplyResources(this.BtnOrdersDetailsByProperties, "BtnOrdersDetailsByProperties");
		this.BtnOrdersDetailsByProperties.Id = 220;
		this.BtnOrdersDetailsByProperties.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrdersDetailsByProperties.ImageOptions.Image");
		this.BtnOrdersDetailsByProperties.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrdersDetailsByProperties.ImageOptions.LargeImage");
		this.BtnOrdersDetailsByProperties.Name = "BtnOrdersDetailsByProperties";
		this.BtnOrdersDetailsByProperties.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrdersDetailsByProperties_ItemClick);
		resources.ApplyResources(this.BtnOrderDetailsSmall, "BtnOrderDetailsSmall");
		this.BtnOrderDetailsSmall.Id = 260;
		this.BtnOrderDetailsSmall.Name = "BtnOrderDetailsSmall";
		this.BtnOrderDetailsSmall.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDetailsSmall_ItemClick_1);
		resources.ApplyResources(this.BtnReportAllOrdersCheckOut, "BtnReportAllOrdersCheckOut");
		this.BtnReportAllOrdersCheckOut.Id = 267;
		this.BtnReportAllOrdersCheckOut.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersCheckOut.ImageOptions.Image");
		this.BtnReportAllOrdersCheckOut.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllOrdersCheckOut.ImageOptions.LargeImage");
		this.BtnReportAllOrdersCheckOut.Name = "BtnReportAllOrdersCheckOut";
		this.BtnReportAllOrdersCheckOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersCheckOut_ItemClick);
		resources.ApplyResources(this.BtnReportPurshes, "BtnReportPurshes");
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
		resources.ApplyResources(this.BtnReportAllPurches, "BtnReportAllPurches");
		this.BtnReportAllPurches.Id = 124;
		this.BtnReportAllPurches.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.Image");
		this.BtnReportAllPurches.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurches.ImageOptions.LargeImage");
		this.BtnReportAllPurches.Name = "BtnReportAllPurches";
		this.BtnReportAllPurches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurches_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesDetails, "BtnReportAllPurchesDetails");
		this.BtnReportAllPurchesDetails.Id = 125;
		this.BtnReportAllPurchesDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.Image");
		this.BtnReportAllPurchesDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesDetails.Name = "BtnReportAllPurchesDetails";
		this.BtnReportAllPurchesDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesDetails_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesReturn, "BtnReportAllPurchesReturn");
		this.BtnReportAllPurchesReturn.Id = 126;
		this.BtnReportAllPurchesReturn.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.Image");
		this.BtnReportAllPurchesReturn.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturn.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturn.Name = "BtnReportAllPurchesReturn";
		this.BtnReportAllPurchesReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturn_ItemClick);
		resources.ApplyResources(this.BtnReportAllPurchesReturnDetails, "BtnReportAllPurchesReturnDetails");
		this.BtnReportAllPurchesReturnDetails.Id = 127;
		this.BtnReportAllPurchesReturnDetails.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.Image");
		this.BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportAllPurchesReturnDetails.ImageOptions.LargeImage");
		this.BtnReportAllPurchesReturnDetails.Name = "BtnReportAllPurchesReturnDetails";
		this.BtnReportAllPurchesReturnDetails.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllPurchesReturnDetails_ItemClick);
		resources.ApplyResources(this.BtnReportProfit, "BtnReportProfit");
		this.BtnReportProfit.Id = 120;
		this.BtnReportProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportProfit.ImageOptions.Image");
		this.BtnReportProfit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[5]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPOrderProfit),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItems),
			new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItemsItemOnly),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPProfitByItemsItemsafryMahly)
		});
		this.BtnReportProfit.Name = "BtnReportProfit";
		this.BtnReportProfit.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnRPOrderProfit, "BtnRPOrderProfit");
		this.BtnRPOrderProfit.Id = 121;
		this.BtnRPOrderProfit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.Image");
		this.BtnRPOrderProfit.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPOrderProfit.ImageOptions.LargeImage");
		this.BtnRPOrderProfit.Name = "BtnRPOrderProfit";
		this.BtnRPOrderProfit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPOrderProfit_ItemClick);
		resources.ApplyResources(this.BtnRPProfitByItems, "BtnRPProfitByItems");
		this.BtnRPProfitByItems.Id = 122;
		this.BtnRPProfitByItems.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.Image");
		this.BtnRPProfitByItems.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItems.ImageOptions.LargeImage");
		this.BtnRPProfitByItems.Name = "BtnRPProfitByItems";
		this.BtnRPProfitByItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItems_ItemClick);
		resources.ApplyResources(this.barButtonItem6, "barButtonItem6");
		this.barButtonItem6.Id = 123;
		this.barButtonItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.Image");
		this.barButtonItem6.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barButtonItem6.ImageOptions.LargeImage");
		this.barButtonItem6.Name = "barButtonItem6";
		this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem6_ItemClick);
		resources.ApplyResources(this.BtnRPProfitByItemsItemOnly, "BtnRPProfitByItemsItemOnly");
		this.BtnRPProfitByItemsItemOnly.Id = 159;
		this.BtnRPProfitByItemsItemOnly.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.Image");
		this.BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPProfitByItemsItemOnly.ImageOptions.LargeImage");
		this.BtnRPProfitByItemsItemOnly.Name = "BtnRPProfitByItemsItemOnly";
		this.BtnRPProfitByItemsItemOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItemsItemOnly_ItemClick);
		resources.ApplyResources(this.BtnRPProfitByItemsItemsafryMahly, "BtnRPProfitByItemsItemsafryMahly");
		this.BtnRPProfitByItemsItemsafryMahly.Id = 203;
		this.BtnRPProfitByItemsItemsafryMahly.Name = "BtnRPProfitByItemsItemsafryMahly";
		this.BtnRPProfitByItemsItemsafryMahly.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPProfitByItemsItemsafryMahly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitByItemsItemsafryMahly_ItemClick);
		resources.ApplyResources(this.barButtonItem20, "barButtonItem20");
		this.barButtonItem20.Id = 129;
		this.barButtonItem20.Name = "barButtonItem20";
		resources.ApplyResources(this.barButtonItem21, "barButtonItem21");
		this.barButtonItem21.Id = 130;
		this.barButtonItem21.Name = "barButtonItem21";
		resources.ApplyResources(this.barButtonItem22, "barButtonItem22");
		this.barButtonItem22.Id = 131;
		this.barButtonItem22.Name = "barButtonItem22";
		resources.ApplyResources(this.barSubItem5, "barSubItem5");
		this.barSubItem5.Id = 132;
		this.barSubItem5.Name = "barSubItem5";
		resources.ApplyResources(this.barCheckItem1, "barCheckItem1");
		this.barCheckItem1.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
		this.barCheckItem1.Id = 134;
		this.barCheckItem1.Name = "barCheckItem1";
		this.barCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.DateApp, "DateApp");
		this.DateApp.Edit = this.repositoryItemDateEdit2;
		this.DateApp.Id = 136;
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
		this.BtnUpdateDateApp.Name = "BtnUpdateDateApp";
		this.BtnUpdateDateApp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnUpdateDateApp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnUpdateDateApp_ItemClick);
		resources.ApplyResources(this.BtnDateApp, "BtnDateApp");
		this.BtnDateApp.Edit = this.repositoryItemDateEdit3;
		this.BtnDateApp.Id = 138;
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
		this.LblVerg.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		resources.ApplyResources(this.LblVerg, "LblVerg");
		this.LblVerg.Id = 142;
		this.LblVerg.Name = "LblVerg";
		resources.ApplyResources(this.BtnReportAllOrdersRestaurant, "BtnReportAllOrdersRestaurant");
		this.BtnReportAllOrdersRestaurant.Id = 143;
		this.BtnReportAllOrdersRestaurant.Name = "BtnReportAllOrdersRestaurant";
		this.BtnReportAllOrdersRestaurant.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportAllOrdersRestaurant_ItemClick);
		resources.ApplyResources(this.BtnCheckOutOrder, "BtnCheckOutOrder");
		this.BtnCheckOutOrder.Id = 144;
		this.BtnCheckOutOrder.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCheckOutOrder.ImageOptions.Image");
		this.BtnCheckOutOrder.Name = "BtnCheckOutOrder";
		this.BtnCheckOutOrder.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckOutOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckOutOrder_ItemClick);
		resources.ApplyResources(this.BtnConnection, "BtnConnection");
		this.BtnConnection.Id = 145;
		this.BtnConnection.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnConnection.ImageOptions.Image");
		this.BtnConnection.Name = "BtnConnection";
		this.BtnConnection.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnConnection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnConnection_ItemClick);
		resources.ApplyResources(this.BtnTransfer, "BtnTransfer");
		this.BtnTransfer.Id = 147;
		this.BtnTransfer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTransfer.ImageOptions.Image");
		this.BtnTransfer.Name = "BtnTransfer";
		this.BtnTransfer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTransfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTransfer_ItemClick);
		resources.ApplyResources(this.BtnDabase, "BtnDabase");
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
		resources.ApplyResources(this.BtnClearDB, "BtnClearDB");
		this.BtnClearDB.Id = 152;
		this.BtnClearDB.Name = "BtnClearDB";
		this.BtnClearDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnClearDB_ItemClick);
		resources.ApplyResources(this.BtnEmptyDB, "BtnEmptyDB");
		this.BtnEmptyDB.Id = 153;
		this.BtnEmptyDB.Name = "BtnEmptyDB";
		this.BtnEmptyDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmptyDB_ItemClick);
		resources.ApplyResources(this.BtnBackup, "BtnBackup");
		this.BtnBackup.Id = 163;
		this.BtnBackup.Name = "BtnBackup";
		this.BtnBackup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBackup_ItemClick);
		resources.ApplyResources(this.barSubItem6, "barSubItem6");
		this.barSubItem6.Id = 154;
		this.barSubItem6.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem6.ImageOptions.Image");
		this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[8]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPCust),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnCustomerByTotal),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPLeadgerSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnLeadgerSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPSalesPurForSupp),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPMizanCustSupp)
		});
		this.barSubItem6.Name = "barSubItem6";
		this.barSubItem6.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnRPCust, "BtnRPCust");
		this.BtnRPCust.Id = 266;
		this.BtnRPCust.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCust.ImageOptions.Image");
		this.BtnRPCust.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCust.ImageOptions.LargeImage");
		this.BtnRPCust.Name = "BtnRPCust";
		this.BtnRPCust.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCust_ItemClick);
		resources.ApplyResources(this.BtnRPSupp, "BtnRPSupp");
		this.BtnRPSupp.Id = 156;
		this.BtnRPSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.Image");
		this.BtnRPSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPSupp.ImageOptions.LargeImage");
		this.BtnRPSupp.Name = "BtnRPSupp";
		this.BtnRPSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPSupp_ItemClick);
		resources.ApplyResources(this.BtnCustomerByTotal, "BtnCustomerByTotal");
		this.BtnCustomerByTotal.Id = 245;
		this.BtnCustomerByTotal.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCustomerByTotal.ImageOptions.Image");
		this.BtnCustomerByTotal.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnCustomerByTotal.ImageOptions.LargeImage");
		this.BtnCustomerByTotal.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCustomerByTotal.ImageOptions.SvgImage");
		this.BtnCustomerByTotal.Name = "BtnCustomerByTotal";
		this.BtnCustomerByTotal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCustomerByTotal_ItemClick);
		resources.ApplyResources(this.BtnRPLeadgerCustomer, "BtnRPLeadgerCustomer");
		this.BtnRPLeadgerCustomer.Id = 157;
		this.BtnRPLeadgerCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.Image");
		this.BtnRPLeadgerCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerCustomer.ImageOptions.LargeImage");
		this.BtnRPLeadgerCustomer.Name = "BtnRPLeadgerCustomer";
		this.BtnRPLeadgerCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerCustomer_ItemClick);
		resources.ApplyResources(this.BtnRPLeadgerSupp, "BtnRPLeadgerSupp");
		this.BtnRPLeadgerSupp.Id = 158;
		this.BtnRPLeadgerSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.Image");
		this.BtnRPLeadgerSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPLeadgerSupp.ImageOptions.LargeImage");
		this.BtnRPLeadgerSupp.Name = "BtnRPLeadgerSupp";
		this.BtnRPLeadgerSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPLeadgerSupp_ItemClick);
		resources.ApplyResources(this.BtnLeadgerSupp, "BtnLeadgerSupp");
		this.BtnLeadgerSupp.Id = 218;
		this.BtnLeadgerSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnLeadgerSupp.ImageOptions.Image");
		this.BtnLeadgerSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnLeadgerSupp.ImageOptions.LargeImage");
		this.BtnLeadgerSupp.Name = "BtnLeadgerSupp";
		this.BtnLeadgerSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnLeadgerSupp_ItemClick);
		resources.ApplyResources(this.BtnRPSalesPurForSupp, "BtnRPSalesPurForSupp");
		this.BtnRPSalesPurForSupp.Id = 257;
		this.BtnRPSalesPurForSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPSalesPurForSupp.ImageOptions.Image");
		this.BtnRPSalesPurForSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPSalesPurForSupp.ImageOptions.LargeImage");
		this.BtnRPSalesPurForSupp.Name = "BtnRPSalesPurForSupp";
		this.BtnRPSalesPurForSupp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRPSalesPurForSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPSalesPurForSupp_ItemClick);
		resources.ApplyResources(this.BtnRPMizanCustSupp, "BtnRPMizanCustSupp");
		this.BtnRPMizanCustSupp.Id = 268;
		this.BtnRPMizanCustSupp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPMizanCustSupp.ImageOptions.Image");
		this.BtnRPMizanCustSupp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPMizanCustSupp.ImageOptions.LargeImage");
		this.BtnRPMizanCustSupp.Name = "BtnRPMizanCustSupp";
		this.BtnRPMizanCustSupp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPMizanCustSupp_ItemClick);
		resources.ApplyResources(this.BtnRPCustomer, "BtnRPCustomer");
		this.BtnRPCustomer.Id = 155;
		this.BtnRPCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.Image");
		this.BtnRPCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCustomer.ImageOptions.LargeImage");
		this.BtnRPCustomer.Name = "BtnRPCustomer";
		this.BtnRPCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCustomer_ItemClick);
		resources.ApplyResources(this.BtnVat, "BtnVat");
		this.BtnVat.Id = 164;
		this.BtnVat.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnVat.ImageOptions.Image");
		this.BtnVat.Name = "BtnVat";
		this.BtnVat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnVat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnVat.VisibleInSearchMenu = false;
		this.BtnVat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnVat_ItemClick);
		resources.ApplyResources(this.barCheckItem2, "barCheckItem2");
		this.barCheckItem2.Id = 166;
		this.barCheckItem2.Name = "barCheckItem2";
		resources.ApplyResources(this.BtnEncriptPassword, "BtnEncriptPassword");
		this.BtnEncriptPassword.Id = 167;
		this.BtnEncriptPassword.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEncriptPassword.ImageOptions.Image");
		this.BtnEncriptPassword.Name = "BtnEncriptPassword";
		this.BtnEncriptPassword.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEncriptPassword.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnEncriptPassword.VisibleInSearchMenu = false;
		this.BtnEncriptPassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEncriptPassword_ItemClick);
		resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
		this.barButtonItem1.Id = 168;
		this.barButtonItem1.Name = "barButtonItem1";
		resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
		this.barButtonItem2.Id = 169;
		this.barButtonItem2.Name = "barButtonItem2";
		resources.ApplyResources(this.BtnLock, "BtnLock");
		this.BtnLock.Id = 170;
		this.BtnLock.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnLock.ImageOptions.Image");
		this.BtnLock.Name = "BtnLock";
		this.BtnLock.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnLock_ItemClick);
		resources.ApplyResources(this.BtnEmployee, "BtnEmployee");
		this.BtnEmployee.Id = 172;
		this.BtnEmployee.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEmployee.ImageOptions.Image");
		this.BtnEmployee.Name = "BtnEmployee";
		this.BtnEmployee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEmployee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEmployee_ItemClick);
		resources.ApplyResources(this.BtnPayrollTest, "BtnPayrollTest");
		this.BtnPayrollTest.Id = 173;
		this.BtnPayrollTest.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayrollTest.ImageOptions.Image");
		this.BtnPayrollTest.Name = "BtnPayrollTest";
		this.BtnPayrollTest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayrollTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayrollTest_ItemClick);
		resources.ApplyResources(this.BtnPayroll, "BtnPayroll");
		this.BtnPayroll.Id = 174;
		this.BtnPayroll.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPayroll.ImageOptions.Image");
		this.BtnPayroll.Name = "BtnPayroll";
		this.BtnPayroll.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPayroll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPayroll_ItemClick);
		resources.ApplyResources(this.BtnRestraunt, "BtnRestraunt");
		this.BtnRestraunt.Id = 176;
		this.BtnRestraunt.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRestraunt.ImageOptions.Image");
		this.BtnRestraunt.Name = "BtnRestraunt";
		this.BtnRestraunt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRestraunt.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnRestraunt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
		resources.ApplyResources(this.btnDefualtCustomer, "btnDefualtCustomer");
		this.btnDefualtCustomer.Id = 177;
		this.btnDefualtCustomer.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnDefualtCustomer.ImageOptions.Image");
		this.btnDefualtCustomer.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("btnDefualtCustomer.ImageOptions.LargeImage");
		this.btnDefualtCustomer.Name = "btnDefualtCustomer";
		this.btnDefualtCustomer.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.btnDefualtCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnDefualtCustomer_ItemClick);
		resources.ApplyResources(this.BtnItemCountry, "BtnItemCountry");
		this.BtnItemCountry.Id = 178;
		this.BtnItemCountry.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemCountry.ImageOptions.SvgImage");
		this.BtnItemCountry.Name = "BtnItemCountry";
		this.BtnItemCountry.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemCountry.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemCountry_ItemClick);
		resources.ApplyResources(this.BtnItemGuarantee, "BtnItemGuarantee");
		this.BtnItemGuarantee.Id = 179;
		this.BtnItemGuarantee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnItemGuarantee.ImageOptions.SvgImage");
		this.BtnItemGuarantee.Name = "BtnItemGuarantee";
		this.BtnItemGuarantee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemGuarantee_ItemClick);
		resources.ApplyResources(this.BtnORDInventorySettlementMinus, "BtnORDInventorySettlementMinus");
		this.BtnORDInventorySettlementMinus.Id = 180;
		this.BtnORDInventorySettlementMinus.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnORDInventorySettlementMinus.ImageOptions.SvgImage");
		this.BtnORDInventorySettlementMinus.Name = "BtnORDInventorySettlementMinus";
		this.BtnORDInventorySettlementMinus.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnORDInventorySettlementMinus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnORDInventorySettlementMinus_ItemClick);
		resources.ApplyResources(this.BtnRoom, "BtnRoom");
		this.BtnRoom.Id = 181;
		this.BtnRoom.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRoom.ImageOptions.SvgImage");
		this.BtnRoom.Name = "BtnRoom";
		this.BtnRoom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRoom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRoom_ItemClick);
		resources.ApplyResources(this.BtnTable, "BtnTable");
		this.BtnTable.Id = 182;
		this.BtnTable.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnTable.ImageOptions.Image");
		this.BtnTable.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnTable.ImageOptions.LargeImage");
		this.BtnTable.Name = "BtnTable";
		this.BtnTable.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTable_ItemClick);
		resources.ApplyResources(this.BtnDelivery, "BtnDelivery");
		this.BtnDelivery.Id = 183;
		this.BtnDelivery.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelivery.ImageOptions.Image");
		this.BtnDelivery.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnDelivery.ImageOptions.LargeImage");
		this.BtnDelivery.Name = "BtnDelivery";
		this.BtnDelivery.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDelivery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnDelivery_ItemClick);
		resources.ApplyResources(this.barSubItem7, "barSubItem7");
		this.barSubItem7.Id = 184;
		this.barSubItem7.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("barSubItem7.ImageOptions.Image");
		this.barSubItem7.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("barSubItem7.ImageOptions.LargeImage");
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
		this.BtnMovementsDailyAllBranReport.Name = "BtnMovementsDailyAllBranReport";
		this.BtnMovementsDailyAllBranReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMovementsDailyAllBranReport_ItemClick);
		resources.ApplyResources(this.BtnRPProfitAccountAllBran, "BtnRPProfitAccountAllBran");
		this.BtnRPProfitAccountAllBran.Id = 186;
		this.BtnRPProfitAccountAllBran.Name = "BtnRPProfitAccountAllBran";
		this.BtnRPProfitAccountAllBran.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPProfitAccountAllBran_ItemClick);
		resources.ApplyResources(this.BtnReportMizanAllBran, "BtnReportMizanAllBran");
		this.BtnReportMizanAllBran.Id = 187;
		this.BtnReportMizanAllBran.Name = "BtnReportMizanAllBran";
		this.BtnReportMizanAllBran.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportMizanAllBran_ItemClick);
		resources.ApplyResources(this.BtnContract, "BtnContract");
		this.BtnContract.Id = 188;
		this.BtnContract.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnContract.ImageOptions.SvgImage");
		this.BtnContract.Name = "BtnContract";
		this.BtnContract.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnContract_ItemClick);
		resources.ApplyResources(this.BtnGuarantee, "BtnGuarantee");
		this.BtnGuarantee.Id = 189;
		this.BtnGuarantee.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnGuarantee.ImageOptions.SvgImage");
		this.BtnGuarantee.Name = "BtnGuarantee";
		this.BtnGuarantee.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGuarantee_ItemClick);
		resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
		this.barButtonItem5.Id = 190;
		this.barButtonItem5.Name = "barButtonItem5";
		resources.ApplyResources(this.barSubItem8, "barSubItem8");
		this.barSubItem8.Id = 191;
		this.barSubItem8.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem8.ImageOptions.SvgImage");
		this.barSubItem8.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[5]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportContract),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportGuarantee),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSearchCustomer),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReportSearchContract),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnRPTBounce)
		});
		this.barSubItem8.Name = "barSubItem8";
		resources.ApplyResources(this.BtnReportContract, "BtnReportContract");
		this.BtnReportContract.Id = 192;
		this.BtnReportContract.Name = "BtnReportContract";
		this.BtnReportContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportContract_ItemClick);
		resources.ApplyResources(this.BtnReportGuarantee, "BtnReportGuarantee");
		this.BtnReportGuarantee.Id = 193;
		this.BtnReportGuarantee.Name = "BtnReportGuarantee";
		this.BtnReportGuarantee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportGuarantee_ItemClick);
		resources.ApplyResources(this.BtnReportSearchCustomer, "BtnReportSearchCustomer");
		this.BtnReportSearchCustomer.Id = 194;
		this.BtnReportSearchCustomer.Name = "BtnReportSearchCustomer";
		this.BtnReportSearchCustomer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSearchCustomer_ItemClick);
		resources.ApplyResources(this.BtnReportSearchContract, "BtnReportSearchContract");
		this.BtnReportSearchContract.Id = 195;
		this.BtnReportSearchContract.Name = "BtnReportSearchContract";
		this.BtnReportSearchContract.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportSearchContract_ItemClick);
		resources.ApplyResources(this.BtnRPTBounce, "BtnRPTBounce");
		this.BtnRPTBounce.Id = 201;
		this.BtnRPTBounce.Name = "BtnRPTBounce";
		this.BtnRPTBounce.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPTBounce_ItemClick);
		resources.ApplyResources(this.BtnItemVat, "BtnItemVat");
		this.BtnItemVat.Id = 196;
		this.BtnItemVat.ImageOptions.Image = GTSErpSystem.Properties.Resources.Vat2;
		this.BtnItemVat.Name = "BtnItemVat";
		this.BtnItemVat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemVat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemVat_ItemClick);
		resources.ApplyResources(this.BtnTrnsferToBranch, "BtnTrnsferToBranch");
		this.BtnTrnsferToBranch.Id = 197;
		this.BtnTrnsferToBranch.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTrnsferToBranch.ImageOptions.SvgImage");
		this.BtnTrnsferToBranch.Name = "BtnTrnsferToBranch";
		this.BtnTrnsferToBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferToBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferToBranch_ItemClick);
		resources.ApplyResources(this.BtnTrnsferFromBranch, "BtnTrnsferFromBranch");
		this.BtnTrnsferFromBranch.Id = 198;
		this.BtnTrnsferFromBranch.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnTrnsferFromBranch.ImageOptions.SvgImage");
		this.BtnTrnsferFromBranch.Name = "BtnTrnsferFromBranch";
		this.BtnTrnsferFromBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferFromBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferFromBranch_ItemClick);
		resources.ApplyResources(this.BtnBarCode, "BtnBarCode");
		this.BtnBarCode.Id = 199;
		this.BtnBarCode.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnBarCode.ImageOptions.Image");
		this.BtnBarCode.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnBarCode.ImageOptions.LargeImage");
		this.BtnBarCode.Name = "BtnBarCode";
		this.BtnBarCode.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBarCode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBarCode_ItemClick);
		resources.ApplyResources(this.BtnBounce, "BtnBounce");
		this.BtnBounce.Id = 200;
		this.BtnBounce.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnBounce.ImageOptions.SvgImage");
		this.BtnBounce.Name = "BtnBounce";
		this.BtnBounce.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnBounce.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnBounce_ItemClick);
		resources.ApplyResources(this.BtnPaymentItems, "BtnPaymentItems");
		this.BtnPaymentItems.Id = 205;
		this.BtnPaymentItems.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPaymentItems.ImageOptions.SvgImage");
		this.BtnPaymentItems.Name = "BtnPaymentItems";
		this.BtnPaymentItems.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPaymentItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPaymentItems_ItemClick);
		resources.ApplyResources(this.BtnAllPrinter, "BtnAllPrinter");
		this.BtnAllPrinter.Id = 207;
		this.BtnAllPrinter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnAllPrinter.ImageOptions.Image");
		this.BtnAllPrinter.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnAllPrinter.ImageOptions.LargeImage");
		this.BtnAllPrinter.Name = "BtnAllPrinter";
		this.BtnAllPrinter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnAllPrinter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAllPrinter_ItemClick);
		resources.ApplyResources(this.BtnDoctor, "BtnDoctor");
		this.BtnDoctor.Id = 208;
		this.BtnDoctor.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnDoctor.ImageOptions.SvgImage");
		this.BtnDoctor.Name = "BtnDoctor";
		this.BtnDoctor.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDoctor.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnDoctor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnDoctor_ItemClick);
		resources.ApplyResources(this.BtnSending, "BtnSending");
		this.BtnSending.Id = 209;
		this.BtnSending.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnSending.ImageOptions.SvgImage");
		this.BtnSending.Name = "BtnSending";
		this.BtnSending.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnSending.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnSending_ItemClick);
		resources.ApplyResources(this.BtnDalala, "BtnDalala");
		this.BtnDalala.Id = 210;
		this.BtnDalala.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDalala.ImageOptions.Image");
		this.BtnDalala.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnDalala.ImageOptions.LargeImage");
		this.BtnDalala.Name = "BtnDalala";
		this.BtnDalala.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDalala.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnDalala_ItemClick);
		resources.ApplyResources(this.BtnReportErsalaya, "BtnReportErsalaya");
		this.BtnReportErsalaya.Id = 211;
		this.BtnReportErsalaya.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportErsalaya.ImageOptions.Image");
		this.BtnReportErsalaya.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportErsalaya.ImageOptions.LargeImage");
		this.BtnReportErsalaya.Name = "BtnReportErsalaya";
		this.BtnReportErsalaya.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportErsalaya.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportErsalaya_ItemClick);
		resources.ApplyResources(this.BtnReportDalala, "BtnReportDalala");
		this.BtnReportDalala.Id = 212;
		this.BtnReportDalala.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReportDalala.ImageOptions.Image");
		this.BtnReportDalala.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReportDalala.ImageOptions.LargeImage");
		this.BtnReportDalala.Name = "BtnReportDalala";
		this.BtnReportDalala.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReportDalala.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReportDalala_ItemClick);
		resources.ApplyResources(this.barButtonItem3, "barButtonItem3");
		this.barButtonItem3.Id = 213;
		this.barButtonItem3.Name = "barButtonItem3";
		resources.ApplyResources(this.BtnRent, "BtnRent");
		this.BtnRent.Id = 216;
		this.BtnRent.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRent.ImageOptions.SvgImage");
		this.BtnRent.Name = "BtnRent";
		this.BtnRent.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRent_ItemClick);
		resources.ApplyResources(this.BtnRecipetRent, "BtnRecipetRent");
		this.BtnRecipetRent.Id = 217;
		this.BtnRecipetRent.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRecipetRent.ImageOptions.SvgImage");
		this.BtnRecipetRent.Name = "BtnRecipetRent";
		this.BtnRecipetRent.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnRecipetRent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRecipetRent_ItemClick);
		resources.ApplyResources(this.BtnMizanItem, "BtnMizanItem");
		this.BtnMizanItem.Id = 219;
		this.BtnMizanItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnMizanItem.ImageOptions.SvgImage");
		this.BtnMizanItem.Name = "BtnMizanItem";
		this.BtnMizanItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnMizanItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnMizanItem_ItemClick);
		resources.ApplyResources(this.BtnPharm, "BtnPharm");
		this.BtnPharm.Id = 223;
		this.BtnPharm.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPharm.ImageOptions.SvgImage");
		this.BtnPharm.Name = "BtnPharm";
		this.BtnPharm.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPharm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnPharm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPharm_ItemClick);
		resources.ApplyResources(this.BtnItemManufacturing, "BtnItemManufacturing");
		this.BtnItemManufacturing.Id = 224;
		this.BtnItemManufacturing.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnItemManufacturing.ImageOptions.Image");
		this.BtnItemManufacturing.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnItemManufacturing.ImageOptions.LargeImage");
		this.BtnItemManufacturing.Name = "BtnItemManufacturing";
		this.BtnItemManufacturing.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnItemManufacturing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnItemManufacturing_ItemClick);
		resources.ApplyResources(this.BtnManufacturing, "BtnManufacturing");
		this.BtnManufacturing.Id = 225;
		this.BtnManufacturing.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnManufacturing.ImageOptions.Image");
		this.BtnManufacturing.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnManufacturing.ImageOptions.LargeImage");
		this.BtnManufacturing.Name = "BtnManufacturing";
		this.BtnManufacturing.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnManufacturing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnManufacturing_ItemClick);
		resources.ApplyResources(this.BtnAllowances, "BtnAllowances");
		this.BtnAllowances.Id = 226;
		this.BtnAllowances.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnAllowances.ImageOptions.SvgImage");
		this.BtnAllowances.Name = "BtnAllowances";
		this.BtnAllowances.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnAllowances_ItemClick);
		resources.ApplyResources(this.BtnEntrusted, "BtnEntrusted");
		this.BtnEntrusted.Id = 227;
		this.BtnEntrusted.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEntrusted.ImageOptions.Image");
		this.BtnEntrusted.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnEntrusted.ImageOptions.LargeImage");
		this.BtnEntrusted.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnEntrusted.ImageOptions.SvgImage");
		this.BtnEntrusted.Name = "BtnEntrusted";
		this.BtnEntrusted.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEntrusted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEntrusted_ItemClick);
		resources.ApplyResources(this.BtnEntrustedEmp, "BtnEntrustedEmp");
		this.BtnEntrustedEmp.Id = 228;
		this.BtnEntrustedEmp.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEntrustedEmp.ImageOptions.Image");
		this.BtnEntrustedEmp.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnEntrustedEmp.ImageOptions.LargeImage");
		this.BtnEntrustedEmp.Name = "BtnEntrustedEmp";
		this.BtnEntrustedEmp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEntrustedEmp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEntrustedEmp_ItemClick);
		resources.ApplyResources(this.BtnCheckRecipt, "BtnCheckRecipt");
		this.BtnCheckRecipt.Id = 229;
		this.BtnCheckRecipt.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCheckRecipt.ImageOptions.SvgImage");
		this.BtnCheckRecipt.Name = "BtnCheckRecipt";
		this.BtnCheckRecipt.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckRecipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckRecipt_ItemClick);
		resources.ApplyResources(this.BtnCheckCom, "BtnCheckCom");
		this.BtnCheckCom.Id = 230;
		this.BtnCheckCom.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCheckCom.ImageOptions.SvgImage");
		this.BtnCheckCom.Name = "BtnCheckCom";
		this.BtnCheckCom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckCom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckCom_ItemClick);
		resources.ApplyResources(this.BtnCheckPayment, "BtnCheckPayment");
		this.BtnCheckPayment.Id = 231;
		this.BtnCheckPayment.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCheckPayment.ImageOptions.SvgImage");
		this.BtnCheckPayment.Name = "BtnCheckPayment";
		this.BtnCheckPayment.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCheckPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCheckPayment_ItemClick);
		resources.ApplyResources(this.BtnPaymentGo, "BtnPaymentGo");
		this.BtnPaymentGo.Id = 232;
		this.BtnPaymentGo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPaymentGo.ImageOptions.SvgImage");
		this.BtnPaymentGo.Name = "BtnPaymentGo";
		this.BtnPaymentGo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPaymentGo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPaymentGo_ItemClick);
		resources.ApplyResources(this.barSubItem9, "barSubItem9");
		this.barSubItem9.Id = 234;
		this.barSubItem9.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barSubItem9.ImageOptions.SvgImage");
		this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[7]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnOrderDetailsSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReturnOrderSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnReturnOrderDetailsSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnProfitOrderSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnProfitByGroupSales),
			new DevExpress.XtraBars.LinkPersistInfo(this.BtnProfitByItemSales)
		});
		this.barSubItem9.Name = "barSubItem9";
		this.barSubItem9.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.BtnOrderSales, "BtnOrderSales");
		this.BtnOrderSales.Id = 235;
		this.BtnOrderSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderSales.ImageOptions.Image");
		this.BtnOrderSales.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnOrderSales.ImageOptions.SvgImage");
		this.BtnOrderSales.Name = "BtnOrderSales";
		this.BtnOrderSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderSales_ItemClick);
		resources.ApplyResources(this.BtnOrderDetailsSales, "BtnOrderDetailsSales");
		this.BtnOrderDetailsSales.Id = 236;
		this.BtnOrderDetailsSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderDetailsSales.ImageOptions.Image");
		this.BtnOrderDetailsSales.Name = "BtnOrderDetailsSales";
		this.BtnOrderDetailsSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDetailsSales_ItemClick);
		resources.ApplyResources(this.BtnReturnOrderSales, "BtnReturnOrderSales");
		this.BtnReturnOrderSales.Id = 237;
		this.BtnReturnOrderSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnOrderSales.ImageOptions.Image");
		this.BtnReturnOrderSales.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReturnOrderSales.ImageOptions.LargeImage");
		this.BtnReturnOrderSales.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReturnOrderSales.ImageOptions.SvgImage");
		this.BtnReturnOrderSales.Name = "BtnReturnOrderSales";
		this.BtnReturnOrderSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReturnOrderSales_ItemClick);
		resources.ApplyResources(this.BtnReturnOrderDetailsSales, "BtnReturnOrderDetailsSales");
		this.BtnReturnOrderDetailsSales.Id = 238;
		this.BtnReturnOrderDetailsSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReturnOrderDetailsSales.ImageOptions.Image");
		this.BtnReturnOrderDetailsSales.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReturnOrderDetailsSales.ImageOptions.LargeImage");
		this.BtnReturnOrderDetailsSales.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReturnOrderDetailsSales.ImageOptions.SvgImage");
		this.BtnReturnOrderDetailsSales.Name = "BtnReturnOrderDetailsSales";
		this.BtnReturnOrderDetailsSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReturnOrderDetailsSales_ItemClick);
		resources.ApplyResources(this.BtnProfitOrderSales, "BtnProfitOrderSales");
		this.BtnProfitOrderSales.Id = 240;
		this.BtnProfitOrderSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnProfitOrderSales.ImageOptions.Image");
		this.BtnProfitOrderSales.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnProfitOrderSales.ImageOptions.LargeImage");
		this.BtnProfitOrderSales.Name = "BtnProfitOrderSales";
		this.BtnProfitOrderSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnProfitOrderSales_ItemClick);
		resources.ApplyResources(this.BtnProfitByGroupSales, "BtnProfitByGroupSales");
		this.BtnProfitByGroupSales.Id = 242;
		this.BtnProfitByGroupSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnProfitByGroupSales.ImageOptions.Image");
		this.BtnProfitByGroupSales.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnProfitByGroupSales.ImageOptions.LargeImage");
		this.BtnProfitByGroupSales.Name = "BtnProfitByGroupSales";
		this.BtnProfitByGroupSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnProfitByGroupSales_ItemClick);
		resources.ApplyResources(this.BtnProfitByItemSales, "BtnProfitByItemSales");
		this.BtnProfitByItemSales.Id = 241;
		this.BtnProfitByItemSales.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnProfitByItemSales.ImageOptions.Image");
		this.BtnProfitByItemSales.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnProfitByItemSales.ImageOptions.LargeImage");
		this.BtnProfitByItemSales.Name = "BtnProfitByItemSales";
		this.BtnProfitByItemSales.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnProfitByItemSales_ItemClick);
		resources.ApplyResources(this.BtnOrderNetSale, "BtnOrderNetSale");
		this.BtnOrderNetSale.Id = 239;
		this.BtnOrderNetSale.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderNetSale.ImageOptions.Image");
		this.BtnOrderNetSale.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderNetSale.ImageOptions.LargeImage");
		this.BtnOrderNetSale.Name = "BtnOrderNetSale";
		this.BtnOrderNetSale.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderNetSale_ItemClick);
		resources.ApplyResources(this.BtnRPCostCenter, "BtnRPCostCenter");
		this.BtnRPCostCenter.Id = 243;
		this.BtnRPCostCenter.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnRPCostCenter.ImageOptions.Image");
		this.BtnRPCostCenter.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnRPCostCenter.ImageOptions.LargeImage");
		this.BtnRPCostCenter.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRPCostCenter.ImageOptions.SvgImage");
		this.BtnRPCostCenter.Name = "BtnRPCostCenter";
		this.BtnRPCostCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRPCostCenter_ItemClick);
		this.LblStatus.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
		this.LblStatus.Id = 247;
		this.LblStatus.Name = "LblStatus";
		this.BtnReplace.Id = 248;
		this.BtnReplace.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnReplace.ImageOptions.SvgImage");
		this.BtnReplace.Name = "BtnReplace";
		this.BtnReplace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReplace_ItemClick);
		resources.ApplyResources(this.BtnImportExcel, "BtnImportExcel");
		this.BtnImportExcel.Id = 251;
		this.BtnImportExcel.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnImportExcel.ImageOptions.Image");
		this.BtnImportExcel.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnImportExcel.ImageOptions.LargeImage");
		this.BtnImportExcel.Name = "BtnImportExcel";
		this.BtnImportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnImportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnImportExcel_ItemClick);
		resources.ApplyResources(this.BtnRecItems, "BtnRecItems");
		this.BtnRecItems.Id = 252;
		this.BtnRecItems.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnRecItems.ImageOptions.SvgImage");
		this.BtnRecItems.Name = "BtnRecItems";
		this.BtnRecItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnRecItems_ItemClick);
		resources.ApplyResources(this.BtnProject, "BtnProject");
		this.BtnProject.Id = 253;
		this.BtnProject.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnProject.ImageOptions.Image");
		this.BtnProject.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnProject.ImageOptions.LargeImage");
		this.BtnProject.Name = "BtnProject";
		this.BtnProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnProject_ItemClick);
		resources.ApplyResources(this.BtnGard, "BtnGard");
		this.BtnGard.Id = 254;
		this.BtnGard.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGard.ImageOptions.Image");
		this.BtnGard.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnGard.ImageOptions.LargeImage");
		this.BtnGard.Name = "BtnGard";
		this.BtnGard.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnGard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnGard_ItemClick);
		resources.ApplyResources(this.BtnOil, "BtnOil");
		this.BtnOil.Id = 258;
		this.BtnOil.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOil.ImageOptions.Image");
		this.BtnOil.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOil.ImageOptions.LargeImage");
		this.BtnOil.Name = "BtnOil";
		this.BtnOil.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnOil.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOil_ItemClick);
		resources.ApplyResources(this.BtnOrderDetailsSmall11, "BtnOrderDetailsSmall11");
		this.BtnOrderDetailsSmall11.Id = 259;
		this.BtnOrderDetailsSmall11.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnOrderDetailsSmall11.ImageOptions.Image");
		this.BtnOrderDetailsSmall11.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnOrderDetailsSmall11.ImageOptions.LargeImage");
		this.BtnOrderDetailsSmall11.Name = "BtnOrderDetailsSmall11";
		this.BtnOrderDetailsSmall11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnOrderDetailsSmall_ItemClick_1);
		resources.ApplyResources(this.BtnCashirWithOut, "BtnCashirWithOut");
		this.BtnCashirWithOut.Id = 262;
		this.BtnCashirWithOut.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCashirWithOut.ImageOptions.Image");
		this.BtnCashirWithOut.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnCashirWithOut.ImageOptions.LargeImage");
		this.BtnCashirWithOut.Name = "BtnCashirWithOut";
		this.BtnCashirWithOut.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
		this.BtnCashirWithOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCashirWithOut_ItemClick);
		resources.ApplyResources(this.BtnTrnsferRequest, "BtnTrnsferRequest");
		this.BtnTrnsferRequest.Id = 263;
		this.BtnTrnsferRequest.Name = "BtnTrnsferRequest";
		this.BtnTrnsferRequest.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferRequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferRequest_ItemClick);
		resources.ApplyResources(this.BtnTrnsferRequestToBranch, "BtnTrnsferRequestToBranch");
		this.BtnTrnsferRequestToBranch.Id = 264;
		this.BtnTrnsferRequestToBranch.Name = "BtnTrnsferRequestToBranch";
		this.BtnTrnsferRequestToBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferRequestToBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferRequestToBranch_ItemClick);
		resources.ApplyResources(this.BtnTrnsferRequestFromBranch, "BtnTrnsferRequestFromBranch");
		this.BtnTrnsferRequestFromBranch.Id = 265;
		this.BtnTrnsferRequestFromBranch.Name = "BtnTrnsferRequestFromBranch";
		this.BtnTrnsferRequestFromBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnTrnsferRequestFromBranch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnTrnsferRequestFromBranch_ItemClick);
		resources.ApplyResources(this.BtnEJAR, "BtnEJAR");
		this.BtnEJAR.Id = 269;
		this.BtnEJAR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEJAR.ImageOptions.Image");
		this.BtnEJAR.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnEJAR.ImageOptions.LargeImage");
		this.BtnEJAR.Name = "BtnEJAR";
		this.BtnEJAR.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnEJAR.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnEJAR_ItemClick);
		resources.ApplyResources(this.barReportGold, "barReportGold");
		this.barReportGold.Id = 270;
		this.barReportGold.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barReportGold.ImageOptions.SvgImage");
		this.barReportGold.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[8]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldPurches),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldPurchesReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldOrder),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldOrderReturn),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldPurchesNet),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldOrdersNet),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldOrdPurchNet),
			new DevExpress.XtraBars.LinkPersistInfo(this.RBGoldOrdersNetPurchOnly)
		});
		this.barReportGold.Name = "barReportGold";
		this.barReportGold.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this.RBGoldPurches, "RBGoldPurches");
		this.RBGoldPurches.Id = 271;
		this.RBGoldPurches.Name = "RBGoldPurches";
		this.RBGoldPurches.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldPurches_ItemClick);
		resources.ApplyResources(this.RBGoldPurchesReturn, "RBGoldPurchesReturn");
		this.RBGoldPurchesReturn.Id = 272;
		this.RBGoldPurchesReturn.Name = "RBGoldPurchesReturn";
		this.RBGoldPurchesReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldPurchesReturn_ItemClick);
		resources.ApplyResources(this.RBGoldOrder, "RBGoldOrder");
		this.RBGoldOrder.Id = 273;
		this.RBGoldOrder.Name = "RBGoldOrder";
		this.RBGoldOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldOrder_ItemClick);
		resources.ApplyResources(this.RBGoldOrderReturn, "RBGoldOrderReturn");
		this.RBGoldOrderReturn.Id = 274;
		this.RBGoldOrderReturn.Name = "RBGoldOrderReturn";
		this.RBGoldOrderReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldOrderReturn_ItemClick);
		resources.ApplyResources(this.RBGoldPurchesNet, "RBGoldPurchesNet");
		this.RBGoldPurchesNet.Id = 275;
		this.RBGoldPurchesNet.Name = "RBGoldPurchesNet";
		this.RBGoldPurchesNet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldPurchesNet_ItemClick);
		resources.ApplyResources(this.RBGoldOrdersNet, "RBGoldOrdersNet");
		this.RBGoldOrdersNet.Id = 276;
		this.RBGoldOrdersNet.Name = "RBGoldOrdersNet";
		this.RBGoldOrdersNet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldOrdersNet_ItemClick);
		resources.ApplyResources(this.RBGoldOrdPurchNet, "RBGoldOrdPurchNet");
		this.RBGoldOrdPurchNet.Id = 277;
		this.RBGoldOrdPurchNet.Name = "RBGoldOrdPurchNet";
		this.RBGoldOrdPurchNet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldOrdPurchNet_ItemClick);
		resources.ApplyResources(this.RBGoldOrdersNetPurchOnly, "RBGoldOrdersNetPurchOnly");
		this.RBGoldOrdersNetPurchOnly.Id = 278;
		this.RBGoldOrdersNetPurchOnly.Name = "RBGoldOrdersNetPurchOnly";
		this.RBGoldOrdersNetPurchOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(RBGoldOrdersNetPurchOnly_ItemClick);
		resources.ApplyResources(this.BtnDriver, "BtnDriver");
		this.BtnDriver.Id = 279;
		this.BtnDriver.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDriver.ImageOptions.Image");
		this.BtnDriver.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnDriver.ImageOptions.LargeImage");
		this.BtnDriver.Name = "BtnDriver";
		this.BtnDriver.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnDriver.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnDriver_ItemClick);
		resources.ApplyResources(this.BtnCustom, "BtnCustom");
		this.BtnCustom.Id = 280;
		this.BtnCustom.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCustom.ImageOptions.Image");
		this.BtnCustom.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnCustom.ImageOptions.LargeImage");
		this.BtnCustom.Name = "BtnCustom";
		this.BtnCustom.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCustom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCustom_ItemClick);
		resources.ApplyResources(this.BtnReciver, "BtnReciver");
		this.BtnReciver.Id = 281;
		this.BtnReciver.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnReciver.ImageOptions.Image");
		this.BtnReciver.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnReciver.ImageOptions.LargeImage");
		this.BtnReciver.Name = "BtnReciver";
		this.BtnReciver.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnReciver.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnReciver_ItemClick);
		resources.ApplyResources(this.BtnSender, "BtnSender");
		this.BtnSender.Id = 282;
		this.BtnSender.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSender.ImageOptions.Image");
		this.BtnSender.ImageOptions.LargeImage = (System.Drawing.Image)resources.GetObject("BtnSender.ImageOptions.LargeImage");
		this.BtnSender.Name = "BtnSender";
		this.BtnSender.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnSender.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnSender_ItemClick);
		resources.ApplyResources(this.BtnContracter, "BtnContracter");
		this.BtnContracter.Id = 283;
		this.BtnContracter.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnContracter.ImageOptions.SvgImage");
		this.BtnContracter.Name = "BtnContracter";
		this.BtnContracter.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnContracter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnContracter_ItemClick);
		resources.ApplyResources(this.BtnCashRecp, "BtnCashRecp");
		this.BtnCashRecp.Id = 284;
		this.BtnCashRecp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnCashRecp.ImageOptions.SvgImage");
		this.BtnCashRecp.Name = "BtnCashRecp";
		this.BtnCashRecp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnCashRecp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnCashRecp_ItemClick);
		resources.ApplyResources(this.BtnPaymentCash, "BtnPaymentCash");
		this.BtnPaymentCash.Id = 285;
		this.BtnPaymentCash.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnPaymentCash.ImageOptions.SvgImage");
		this.BtnPaymentCash.Name = "BtnPaymentCash";
		this.BtnPaymentCash.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnPaymentCash.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnPaymentCash_ItemClick);
		resources.ApplyResources(this.BtnLedger, "BtnLedger");
		this.BtnLedger.Id = 286;
		this.BtnLedger.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnLedger.ImageOptions.SvgImage");
		this.BtnLedger.Name = "BtnLedger";
		this.BtnLedger.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnLedger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnLedger_ItemClick);
		resources.ApplyResources(this.BtnExpulsion, "BtnExpulsion");
		this.BtnExpulsion.Id = 287;
		this.BtnExpulsion.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("BtnExpulsion.ImageOptions.SvgImage");
		this.BtnExpulsion.Name = "BtnExpulsion";
		this.BtnExpulsion.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		this.BtnExpulsion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(BtnExpulsion_ItemClick);
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
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnBarCode);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnImportExcel);
		this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnOil);
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
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnDoctor);
		this.ribbonPageGroup10.ItemLinks.Add(this.BtnBounce);
		this.ribbonPageGroup10.Name = "ribbonPageGroup10";
		resources.ApplyResources(this.ribbonPageGroup10, "ribbonPageGroup10");
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnRoom);
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnTable);
		this.ribbonPageGroup23.ItemLinks.Add(this.BtnDelivery);
		this.ribbonPageGroup23.Name = "ribbonPageGroup23";
		resources.ApplyResources(this.ribbonPageGroup23, "ribbonPageGroup23");
		this.ribbonPageGroup23.Visible = false;
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
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnPharm);
		this.ribbonPageGroup21.ItemLinks.Add(this.BtnCashirWithOut);
		this.ribbonPageGroup21.Name = "ribbonPageGroup21";
		resources.ApplyResources(this.ribbonPageGroup21, "ribbonPageGroup21");
		this.ribbonPageGroup1.AllowTextClipping = false;
		this.ribbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDPurchase);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnCheckOutOrder);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnORDInventorySettlementMinus);
		this.ribbonPageGroup1.ItemLinks.Add(this.BtnGard);
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
		this.ribbonPage8.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[5] { this.ribbonPageGroup27, this.ribbonPageGroup28, this.ribbonPageGroup29, this.ribbonPageGroup32, this.ribbonPageGroup18 });
		this.ribbonPage8.Name = "ribbonPage8";
		resources.ApplyResources(this.ribbonPage8, "ribbonPage8");
		this.ribbonPageGroup27.ItemLinks.Add(this.BtnTrnsferToBranch);
		this.ribbonPageGroup27.ItemLinks.Add(this.BtnTrnsferFromBranch);
		this.ribbonPageGroup27.Name = "ribbonPageGroup27";
		resources.ApplyResources(this.ribbonPageGroup27, "ribbonPageGroup27");
		this.ribbonPageGroup28.ItemLinks.Add(this.BtnTransfer);
		this.ribbonPageGroup28.Name = "ribbonPageGroup28";
		resources.ApplyResources(this.ribbonPageGroup28, "ribbonPageGroup28");
		this.ribbonPageGroup29.ItemLinks.Add(this.BtnPaymentItems);
		this.ribbonPageGroup29.ItemLinks.Add(this.BtnRecItems);
		this.ribbonPageGroup29.Name = "ribbonPageGroup29";
		resources.ApplyResources(this.ribbonPageGroup29, "ribbonPageGroup29");
		this.ribbonPageGroup32.ItemLinks.Add(this.BtnRent);
		this.ribbonPageGroup32.ItemLinks.Add(this.BtnRecipetRent);
		this.ribbonPageGroup32.Name = "ribbonPageGroup32";
		resources.ApplyResources(this.ribbonPageGroup32, "ribbonPageGroup32");
		this.ribbonPageGroup32.Visible = false;
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnTrnsferRequest);
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnTrnsferRequestToBranch);
		this.ribbonPageGroup18.ItemLinks.Add(this.BtnTrnsferRequestFromBranch);
		this.ribbonPageGroup18.Name = "ribbonPageGroup18";
		resources.ApplyResources(this.ribbonPageGroup18, "ribbonPageGroup18");
		this.ribbonPageGroup18.Visible = false;
		this.ribbonPage11.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup38, this.ribbonPageGroup39 });
		this.ribbonPage11.Name = "ribbonPage11";
		resources.ApplyResources(this.ribbonPage11, "ribbonPage11");
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnDriver);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnCustom);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnReciver);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnSender);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnContracter);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnCashRecp);
		this.ribbonPageGroup38.ItemLinks.Add(this.BtnPaymentCash);
		this.ribbonPageGroup38.Name = "ribbonPageGroup38";
		resources.ApplyResources(this.ribbonPageGroup38, "ribbonPageGroup38");
		this.ribbonPageGroup39.ItemLinks.Add(this.BtnLedger);
		this.ribbonPageGroup39.ItemLinks.Add(this.BtnExpulsion);
		this.ribbonPageGroup39.Name = "ribbonPageGroup39";
		resources.ApplyResources(this.ribbonPageGroup39, "ribbonPageGroup39");
		this.ribbonPage9.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup30, this.ribbonPageGroup31 });
		this.ribbonPage9.Name = "ribbonPage9";
		resources.ApplyResources(this.ribbonPage9, "ribbonPage9");
		this.ribbonPage9.Visible = false;
		this.ribbonPageGroup30.ItemLinks.Add(this.BtnSending);
		this.ribbonPageGroup30.ItemLinks.Add(this.BtnDalala);
		this.ribbonPageGroup30.Name = "ribbonPageGroup30";
		resources.ApplyResources(this.ribbonPageGroup30, "ribbonPageGroup30");
		this.ribbonPageGroup31.AllowTextClipping = false;
		this.ribbonPageGroup31.ItemLinks.Add(this.BtnReportErsalaya);
		this.ribbonPageGroup31.ItemLinks.Add(this.BtnReportDalala);
		this.ribbonPageGroup31.Name = "ribbonPageGroup31";
		resources.ApplyResources(this.ribbonPageGroup31, "ribbonPageGroup31");
		this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup3, this.ribbonPageGroup34 });
		this.ribbonPage3.Name = "ribbonPage3";
		resources.ApplyResources(this.ribbonPage3, "ribbonPage3");
		this.ribbonPageGroup3.AllowTextClipping = false;
		this.ribbonPageGroup3.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnAllowances);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnEmployee);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayrollTest);
		this.ribbonPageGroup3.ItemLinks.Add(this.BtnPayroll);
		this.ribbonPageGroup3.Name = "ribbonPageGroup3";
		this.ribbonPageGroup34.ItemLinks.Add(this.BtnEntrusted);
		this.ribbonPageGroup34.ItemLinks.Add(this.BtnEntrustedEmp);
		this.ribbonPageGroup34.Name = "ribbonPageGroup34";
		this.ribbonPage7.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2] { this.ribbonPageGroup25, this.ribbonPageGroup26 });
		this.ribbonPage7.Name = "ribbonPage7";
		resources.ApplyResources(this.ribbonPage7, "ribbonPage7");
		this.ribbonPage7.Visible = false;
		this.ribbonPageGroup25.AllowTextClipping = false;
		this.ribbonPageGroup25.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup25.ItemLinks.Add(this.BtnContract);
		this.ribbonPageGroup25.ItemLinks.Add(this.BtnGuarantee);
		this.ribbonPageGroup25.ItemLinks.Add(this.BtnEJAR);
		this.ribbonPageGroup25.Name = "ribbonPageGroup25";
		resources.ApplyResources(this.ribbonPageGroup25, "ribbonPageGroup25");
		this.ribbonPageGroup26.AllowTextClipping = false;
		this.ribbonPageGroup26.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup26.ItemLinks.Add(this.barSubItem8);
		this.ribbonPageGroup26.Name = "ribbonPageGroup26";
		resources.ApplyResources(this.ribbonPageGroup26, "ribbonPageGroup26");
		this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[3] { this.ribbonPageGroup11, this.ribbonPageGroup2, this.ribbonPageGroup35 });
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
		this.ribbonPageGroup11.ItemLinks.Add(this.BtnProject);
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
		this.ribbonPageGroup35.ItemLinks.Add(this.BtnCheckRecipt);
		this.ribbonPageGroup35.ItemLinks.Add(this.BtnCheckCom);
		this.ribbonPageGroup35.ItemLinks.Add(this.BtnCheckPayment);
		this.ribbonPageGroup35.ItemLinks.Add(this.BtnPaymentGo);
		this.ribbonPageGroup35.Name = "ribbonPageGroup35";
		this.ribbonPage10.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1] { this.ribbonPageGroup33 });
		this.ribbonPage10.Name = "ribbonPage10";
		resources.ApplyResources(this.ribbonPage10, "ribbonPage10");
		this.ribbonPage10.Visible = false;
		this.ribbonPageGroup33.ItemLinks.Add(this.BtnItemManufacturing);
		this.ribbonPageGroup33.ItemLinks.Add(this.BtnManufacturing);
		this.ribbonPageGroup33.Name = "ribbonPageGroup33";
		resources.ApplyResources(this.ribbonPageGroup33, "ribbonPageGroup33");
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
		this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[10] { this.ribbonPageGroup5, this.ribbonPageGroup19, this.ribbonPageGroup12, this.ribbonPageGroup13, this.ribbonPageGroup14, this.ribbonPageGroup15, this.ribbonPageGroup36, this.ribbonPageGroup16, this.ribbonPageGroup24, this.ribbonPageGroup37 });
		this.ribbonPage5.Name = "ribbonPage5";
		resources.ApplyResources(this.ribbonPage5, "ribbonPage5");
		this.ribbonPageGroup5.AllowTextClipping = false;
		this.ribbonPageGroup5.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup5.ItemLinks.Add(this.BtnReportPublic);
		this.ribbonPageGroup5.Name = "ribbonPageGroup5";
		resources.ApplyResources(this.ribbonPageGroup5, "ribbonPageGroup5");
		this.ribbonPageGroup19.AllowTextClipping = false;
		this.ribbonPageGroup19.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup19.ItemLinks.Add(this.barSubItem6, true);
		this.ribbonPageGroup19.Name = "ribbonPageGroup19";
		resources.ApplyResources(this.ribbonPageGroup19, "ribbonPageGroup19");
		this.ribbonPageGroup12.AllowTextClipping = false;
		this.ribbonPageGroup12.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup12.ItemLinks.Add(this.BtnReportStore, true);
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
		this.ribbonPageGroup36.AllowTextClipping = false;
		this.ribbonPageGroup36.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup36.ItemLinks.Add(this.barSubItem9);
		this.ribbonPageGroup36.Name = "ribbonPageGroup36";
		this.ribbonPageGroup16.AllowTextClipping = false;
		this.ribbonPageGroup16.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup16.ItemLinks.Add(this.BtnReportAccount);
		this.ribbonPageGroup16.Name = "ribbonPageGroup16";
		resources.ApplyResources(this.ribbonPageGroup16, "ribbonPageGroup16");
		this.ribbonPageGroup24.ItemLinks.Add(this.barSubItem7);
		this.ribbonPageGroup24.Name = "ribbonPageGroup24";
		resources.ApplyResources(this.ribbonPageGroup24, "ribbonPageGroup24");
		this.ribbonPageGroup24.Visible = false;
		this.ribbonPageGroup37.ItemLinks.Add(this.barReportGold);
		this.ribbonPageGroup37.Name = "ribbonPageGroup37";
		resources.ApplyResources(this.ribbonPageGroup37, "ribbonPageGroup37");
		this.ribbonPageGroup37.Visible = false;
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
		this.ribbonPageGroup6.ItemLinks.Add(this.BtnMizanItem);
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
		this.ribbonStatusBar.ItemLinks.Add(this.LblCompany);
		this.ribbonStatusBar.ItemLinks.Add(this.LblStatus);
		this.ribbonStatusBar.ItemLinks.Add(this.LblVerg);
		this.ribbonStatusBar.ItemLinks.Add(this.LblServer);
		this.ribbonStatusBar.ItemLinks.Add(this.LblUser);
		this.ribbonStatusBar.ItemLinks.Add(this.LblTime);
		this.ribbonStatusBar.ItemLinks.Add(this.LblDate);
		resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
		this.ribbonStatusBar.Name = "ribbonStatusBar";
		this.ribbonStatusBar.Ribbon = this.ribbonControl;
		this.navigationFrame.Appearance.BackColor = System.Drawing.Color.White;
		this.navigationFrame.Appearance.Options.UseBackColor = true;
		this.navigationFrame.Controls.Add(this.employeesNavigationPage);
		resources.ApplyResources(this.navigationFrame, "navigationFrame");
		this.navigationFrame.Name = "navigationFrame";
		this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[1] { this.employeesNavigationPage });
		this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
		this.navigationFrame.SelectedPage = this.employeesNavigationPage;
		this.employeesNavigationPage.BackgroundImage = GTSErpSystem.Properties.Resources.M3goldplus;
		resources.ApplyResources(this.employeesNavigationPage, "employeesNavigationPage");
		this.employeesNavigationPage.Controls.Add(this.tileControl1);
		this.employeesNavigationPage.Name = "employeesNavigationPage";
		this.tileControl1.BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.tileControl1, "tileControl1");
		this.tileControl1.Groups.Add(this.tileGroup2);
		this.tileControl1.Name = "tileControl1";
		this.tileGroup2.Name = "tileGroup2";
		this.ribbonPageGroup.AllowTextClipping = false;
		this.ribbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
		this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem);
		this.ribbonPageGroup.Name = "ribbonPageGroup";
		resources.ApplyResources(this.ribbonPageGroup, "ribbonPageGroup");
		resources.ApplyResources(this.barSubItem1, "barSubItem1");
		this.barSubItem1.Id = 15;
		this.barSubItem1.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem1.Name = "barSubItem1";
		resources.ApplyResources(this.barSubItem2, "barSubItem2");
		this.barSubItem2.Id = 15;
		this.barSubItem2.ImageOptions.ImageUri.Uri = "NavigationBar";
		this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.employeesBarButtonItem),
			new DevExpress.XtraBars.LinkPersistInfo(this.customersBarButtonItem)
		});
		this.barSubItem2.Name = "barSubItem2";
		resources.ApplyResources(this.barSubItem3, "barSubItem3");
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
		resources.ApplyResources(this.BtnDataBase, "BtnDataBase");
		this.BtnDataBase.Id = 148;
		this.BtnDataBase.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDataBase.ImageOptions.Image");
		this.BtnDataBase.Name = "BtnDataBase";
		this.BtnDataBase.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.navigationFrame);
		base.Controls.Add(this.ribbonStatusBar);
		base.Controls.Add(this.ribbonControl);
		base.IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("FrmGoldPlusTransportation.IconOptions.Icon");
		base.Name = "FrmGoldPlusTransportation";
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
		this.employeesNavigationPage.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


