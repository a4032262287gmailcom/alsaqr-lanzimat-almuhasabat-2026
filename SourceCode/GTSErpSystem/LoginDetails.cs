using System;
using System.Collections.Generic;

namespace GTSErpSystem;

public static class LoginDetails
{
	public static string DefualtPrint;

	public static string DefualtPrintA4;

	public static string SecondPrintCasheir;

	public static bool IsPrintSecondCasheir;

	public static bool FrmOpenDayIsPrint;

	public static bool FrmOpenDayIsPrintA4;

	public static bool IsPrintSecondFatoraCasheir;

	public static bool IsTakeDateApp;

	public static bool IsRestaurant;

	public static DateTime DateApp;

	public static int FrmUserIDEdit;

	public static string BackupPath;

	public static bool IsBackupWhenClose;

	public static string ActionName;

	public static int AccountSN;

	public static int AccountBrother;

	public static int TobaccoNum = 406;

	public static string Status = "نسخة تجريبية";

	public static string VergDate = " ";

	public static string AppVerg;

	public static int UserID;

	public static string UserName;

	public static int YearID = 1;

	public static int BranchID;

	public static int GroupID;

	public static string macAddress;

	public static int StoreID;

	public static int StoreIDWaiting;

	public static int StoreIDPurches;

	public static string FrmName;

	public static string AccountNameSearch;

	public static int StoreEntagTam;

	public static int Acc_StockNum;

	public static int Acc_CostItemOrder;

	public static int Acc_VatNum;

	public static int Acc_CashBox;

	public static int Acc_OrderCash;

	public static int Acc_OrderAjila;

	public static int Acc_OrderReturnCash;

	public static int Acc_OrderReturnAgl;

	public static decimal TotalFatorPrice = 100m;

	public static decimal Cach = 100m;

	public static decimal Bank = 100m;

	public static int Acc_BankNumCachir;

	public static int Acc_Payroll;

	public static int Acc_CustomerCash;

	public static int Acc_Hanger;

	public static int Acc_Room;

	public static int Acc_Table;

	public static int Acc_CustomerCashAccount;

	public static int Acc_HangerAccount;

	public static int Acc_RoomAccount;

	public static int Acc_TableAccount;

	public static int Acc_Tawsel;

	public static int Acc_TawselAccount;

	public static int Acc_DiscountForAll;

	public static int Acc_InventorySettlementMinus;

	public static int Acc_CheckIncome;

	public static int Acc_CheckOutCome;

	public static int Acc_TransferAjila;

	public static int Acc_TransferCash;

	public static int Acc_Rewards;

	public static int Acc_Penalties;

	public static int Acc_Bonus;

	public static int Acc_Absences;

	public static int Acc_LateArrivals;

	public static int Acc_Overtime;

	public static int Acc_Allowances;

	public static int Acc_AdvanceMoney;

	public static int Acc_AdvanceMoneRecipt;

	public static int InventoryUnderTransfer;

	public static int GroupSize;

	public static string GroupColor;

	public static string GroupFontColor;

	public static int ItemSize;

	public static string ItemColor;

	public static string ItemFontColor;

	public static bool IsItemNameAR;

	public static bool IsItemNameEN;

	public static bool IsItemShowPrice;

	public static string ItemNameAR;

	public static string ItemNameEN;

	public static string ItemShowPrice;

	public static string ItemCode;

	public static string PassWord;

	public static List<string> Privalge;

	public static string nameServer;

	public static string databaseServer;

	public static string userNameServer;

	public static string passServer;

	public static string CompanyNameAR;

	public static string CompanyNameEN;

	public static string VatNum;

	public static string PrintComp;

	public static bool IsVat;

	public static string ItemcodeForUnits;

	public static int TafqitID;

	public static int StoreSN;

	public static string ItemSearch;

	public static int ItemSearchID;

	public static string ItemcodeNewItem;

	public static List<string> ItemSearchAll;

	public static List<string> ItemSearchAll2;

	public static int ItemcodeForStore;

	public static bool ISCheckSearchItem = false;

	public static bool IsComplateMony;

	public static decimal Cash;

	public static decimal Visa;

	public static string TableID;

	public static string WaiterId;

	public static string DilverId;

	public static int Order_Type;

	public static string CustomerFhone;

	public static int OrderSearchId;

	public static bool ISPurches;

	public static bool SellPrice2;

	public static bool SellPrice3;

	public static bool LastPrice;

	public static bool AvergPrice;

	public static bool ProvePrice;

	public static int UserSellPrice;

	public static bool IsQuantityMinZero;

	public static string SearchBuBarcode;

	public static int CashierOrderNum;

	public static int DraftNum;

	public static bool IsTobacc;

	public static bool IsImage;

	public static bool IsFrmCollect;

	public static string SecondPrintDefualt;

	public static bool IsShowRoomInFormRestaurant;

	public static bool IsTimeInRestaurant;

	public static decimal TotalCalc;

	public static decimal NetCalc;

	public static string CommercialRegister;

	public static string CompanyBank;

	public static string Phone;

	public static string Fax;

	public static string Mobile;

	public static string Web;

	public static string Address;

	public static int DecimalNum;

	public static string NamePrintFatora;

	public static bool DiscountDecimal;

	public static bool NoDiscountDecimal;

	public static bool DiscountDecimalForCustomer;

	public static bool IsPrintCashier;

	public static bool IsPrintOrder;

	public static bool IsPrintReturnOrder;

	public static bool IsPharmacy;

	public static bool IsPrintItemCook;

	public static decimal DiscountForRestaurant;

	public static bool IsSecureCashier;

	public static bool IsMizanWeight;

	public static bool OrderStore;

	public static bool Hasm;

	public static bool Bouns;

	public static bool DisCode;

	public static bool Gabr;

	public static int DecimalNumQty;

	public static decimal MinTobaccoTax;

	public static bool CloseYear;

	public static List<TblRemember> remembers = new List<TblRemember>();

	public static string BuildingNum;

	public static string Street;

	public static string District;

	public static string City;

	public static string Country;

	public static string PostalCode;

	public static string AdditionalNum;

	public static string CommercialRecord;

	public static decimal PerVat;

	public static string PathValue;

	public static int CBGroupIDItem;

	public static string Lang = "ar-EG";

	public static decimal NetCash;

	public static decimal VatCash;

	public static int FrmOrderID;

	public static bool FrmOrderClose = false;

	public static string ConnString;

	public static string TypeVerg;

	public static bool ItemTotalWithVat;

	public static string HDD;

	public static string Mac;

	public static string SerialBoard;

	public static int Active_Company;

	public static int Active_Type;

	public static int Active_Virgin;

	public static DateTime Active_DateTo;

	public static bool Active_Message;

	public static bool Active_Closed;

	public static int Active_NumMessage;

	public static int GroupShow;

	public static bool Lbl_CB_VirginShow;

	public static bool Active_AdminPermission;

	public static string Active_LblUserName;

	public static int Active_LblUserID;

	public static bool Active_ShowVirgin;

	public static int Active_CompID;

	public static int Active_TypeID;

	public static int Active_FranchiseID = 1;

	public static bool Active_ShowAllCustomer = true;

	public static decimal FontSize;

	public static bool ImgShape;

	public static string ConCloud;

	public static int InstallAll;

	public static bool Glasses;

	public static bool BondTax;

	public static bool IsElectronicInvoice;

	public static bool CsIdForUser;

	public static bool SentXmlZakateNow;

	public static string Crs;

	public static string PrivateKey;

	public static string PublicKey;

	public static string SecretKey;

	public static string InvoiceHash;

	public static string ModeInvoice;

	public static string SupplierPartyIdentificationID;

	public static string SupplierPartyIdentificationschemeID;

	public static int OrderTypeElectronicInvoiceId;

	public static string NormalOrSimpleInvoice;

	public static bool ThirdParty;

	public static bool NominalInvoice;

	public static bool ExportInvoice;

	public static bool SummaryInvoice;

	public static bool SelfInvoice;

	public static decimal DisPerCashier;

	public static decimal PerGrid;

	public static decimal AmountPaid;

	public static decimal Rest;

	public static string MizanRate;

	public static string BasicContract;

	public static string contractTerms;

	public static bool BarcodeResturant;

	public static bool ShowFatora;

	public static string AccountSid { get; set; }

	public static string AuthToken { get; set; }

	public static string FromPhone { get; set; }

	public static bool SentInvoice { get; set; } = false;

	public static string EncHash { get; set; }

	public static string Crm { get; set; }

	public static string EmployeeId { get; set; }

	public static string Token { get; set; }

	public static int ActivationId { get; set; }

	public static bool ShowRoom { get; set; }

	public static bool AlLItemInRow { get; set; }
}


