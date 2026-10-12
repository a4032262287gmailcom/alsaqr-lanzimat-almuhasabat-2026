using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace GTSErpSystem;

public class GTSdbEntities : DbContext
{
	public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }

	public virtual DbSet<Account_Accounts> Account_Accounts { get; set; }

	public virtual DbSet<Account_AccountType> Account_AccountType { get; set; }

	public virtual DbSet<Account_Branch> Account_Branch { get; set; }

	public virtual DbSet<Account_CostCenters> Account_CostCenters { get; set; }

	public virtual DbSet<Account_CustSup> Account_CustSup { get; set; }

	public virtual DbSet<Account_CustTailor> Account_CustTailor { get; set; }

	public virtual DbSet<Account_DefualtAccount> Account_DefualtAccount { get; set; }

	public virtual DbSet<Account_DefualtAccount2> Account_DefualtAccount2 { get; set; }

	public virtual DbSet<Account_DefualtCustomer> Account_DefualtCustomer { get; set; }

	public virtual DbSet<Account_Final> Account_Final { get; set; }

	public virtual DbSet<Account_Nature> Account_Nature { get; set; }

	public virtual DbSet<Account_Place> Account_Place { get; set; }

	public virtual DbSet<Account_Projects> Account_Projects { get; set; }

	public virtual DbSet<Account_ProjectsDetails> Account_ProjectsDetails { get; set; }

	public virtual DbSet<Account_Receipts> Account_Receipts { get; set; }

	public virtual DbSet<Account_ReceiptsDetails> Account_ReceiptsDetails { get; set; }

	public virtual DbSet<Account_SalesMan> Account_SalesMan { get; set; }

	public virtual DbSet<Account_Stores> Account_Stores { get; set; }

	public virtual DbSet<Account_Suspended> Account_Suspended { get; set; }

	public virtual DbSet<Account_Type> Account_Type { get; set; }

	public virtual DbSet<AccountStartBalance> AccountStartBalances { get; set; }

	public virtual DbSet<AccountYear> AccountYears { get; set; }

	public virtual DbSet<AgeingAnalysi> AgeingAnalysis { get; set; }

	public virtual DbSet<Car_Receive> Car_Receive { get; set; }

	public virtual DbSet<CashReceiptsType> CashReceiptsTypes { get; set; }

	public virtual DbSet<Contract_Bounce> Contract_Bounce { get; set; }

	public virtual DbSet<Contract_Contract> Contract_Contract { get; set; }

	public virtual DbSet<Contract_ContractDetails> Contract_ContractDetails { get; set; }

	public virtual DbSet<Contract_EJAR> Contract_EJAR { get; set; }

	public virtual DbSet<Contract_Files> Contract_Files { get; set; }

	public virtual DbSet<Contract_Guarantee> Contract_Guarantee { get; set; }

	public virtual DbSet<Contract_GuaranteeDetails> Contract_GuaranteeDetails { get; set; }

	public virtual DbSet<Contract_Store> Contract_Store { get; set; }

	public virtual DbSet<Emp_Allowances> Emp_Allowances { get; set; }

	public virtual DbSet<Emp_Employee> Emp_Employee { get; set; }

	public virtual DbSet<Emp_Entrusted> Emp_Entrusted { get; set; }

	public virtual DbSet<Emp_EntrustedEmployeeDetails> Emp_EntrustedEmployeeDetails { get; set; }

	public virtual DbSet<Emp_Payroll> Emp_Payroll { get; set; }

	public virtual DbSet<Emp_PayrollDetails> Emp_PayrollDetails { get; set; }

	public virtual DbSet<Emp_PayrollTemp> Emp_PayrollTemp { get; set; }

	public virtual DbSet<Emp_PayrollTempDetails> Emp_PayrollTempDetails { get; set; }

	public virtual DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }

	public virtual DbSet<Hall> Halls { get; set; }

	public virtual DbSet<Item_Add> Item_Add { get; set; }

	public virtual DbSet<Item_Class> Item_Class { get; set; }

	public virtual DbSet<Item_Company> Item_Company { get; set; }

	public virtual DbSet<Item_Country> Item_Country { get; set; }

	public virtual DbSet<Item_Doctor> Item_Doctor { get; set; }

	public virtual DbSet<Item_Groups> Item_Groups { get; set; }

	public virtual DbSet<Item_Guarantee> Item_Guarantee { get; set; }

	public virtual DbSet<Item_ItemComponent> Item_ItemComponent { get; set; }

	public virtual DbSet<Item_Items> Item_Items { get; set; }

	public virtual DbSet<Item_Type> Item_Type { get; set; }

	public virtual DbSet<Item_Unit> Item_Unit { get; set; }

	public virtual DbSet<ItemQuantity> ItemQuantities { get; set; }

	public virtual DbSet<Items_ContentAlignment_ForCashier> Items_ContentAlignment_ForCashier { get; set; }

	public virtual DbSet<LoginLog> LoginLogs { get; set; }

	public virtual DbSet<Order_CheckoutOrders> Order_CheckoutOrders { get; set; }

	public virtual DbSet<Order_CheckoutOrdersDetails> Order_CheckoutOrdersDetails { get; set; }

	public virtual DbSet<Order_Extension> Order_Extension { get; set; }

	public virtual DbSet<Order_Gard> Order_Gard { get; set; }

	public virtual DbSet<Order_GardDetails> Order_GardDetails { get; set; }

	public virtual DbSet<Order_InventorySettlementMinus> Order_InventorySettlementMinus { get; set; }

	public virtual DbSet<Order_InventorySettlementMinusDetails> Order_InventorySettlementMinusDetails { get; set; }

	public virtual DbSet<Order_Manufacturing> Order_Manufacturing { get; set; }

	public virtual DbSet<Order_ManufacturingDetailsItems> Order_ManufacturingDetailsItems { get; set; }

	public virtual DbSet<Order_ManufacturingDetailsNew> Order_ManufacturingDetailsNew { get; set; }

	public virtual DbSet<Order_ManufacturingOrder> Order_ManufacturingOrder { get; set; }

	public virtual DbSet<Order_ManufacturingOrderDetails> Order_ManufacturingOrderDetails { get; set; }

	public virtual DbSet<Order_MoveHall> Order_MoveHall { get; set; }

	public virtual DbSet<Order_OpenQuantity> Order_OpenQuantity { get; set; }

	public virtual DbSet<Order_OpenQuantityDetails> Order_OpenQuantityDetails { get; set; }

	public virtual DbSet<Order_OrderReturn> Order_OrderReturn { get; set; }

	public virtual DbSet<Order_OrderReturnDetails> Order_OrderReturnDetails { get; set; }

	public virtual DbSet<Order_Orders> Order_Orders { get; set; }

	public virtual DbSet<Order_OrdersDetails> Order_OrdersDetails { get; set; }

	public virtual DbSet<Order_OrdersDetailsDraft> Order_OrdersDetailsDraft { get; set; }

	public virtual DbSet<Order_OrdersDraft> Order_OrdersDraft { get; set; }

	public virtual DbSet<Order_PaymentItem> Order_PaymentItem { get; set; }

	public virtual DbSet<Order_PaymentItemDetails> Order_PaymentItemDetails { get; set; }

	public virtual DbSet<Order_PriceOffer> Order_PriceOffer { get; set; }

	public virtual DbSet<Order_PriceOfferDetails> Order_PriceOfferDetails { get; set; }

	public virtual DbSet<Order_Purchases> Order_Purchases { get; set; }

	public virtual DbSet<Order_PurchasesDetails> Order_PurchasesDetails { get; set; }

	public virtual DbSet<Order_PurchasesOrder> Order_PurchasesOrder { get; set; }

	public virtual DbSet<Order_PurchasesOrderDetails> Order_PurchasesOrderDetails { get; set; }

	public virtual DbSet<Order_PurchasesReturn> Order_PurchasesReturn { get; set; }

	public virtual DbSet<Order_PurchasesReturnDetails> Order_PurchasesReturnDetails { get; set; }

	public virtual DbSet<Order_RecItem> Order_RecItem { get; set; }

	public virtual DbSet<Order_RecItemDetails> Order_RecItemDetails { get; set; }

	public virtual DbSet<Order_Reservation> Order_Reservation { get; set; }

	public virtual DbSet<Order_Reservations> Order_Reservations { get; set; }

	public virtual DbSet<Order_StoreTransfer> Order_StoreTransfer { get; set; }

	public virtual DbSet<Order_StoreTransferDetails> Order_StoreTransferDetails { get; set; }

	public virtual DbSet<Order_Transfer_Master> Order_Transfer_Master { get; set; }

	public virtual DbSet<Order_Transfer_MasterDetails> Order_Transfer_MasterDetails { get; set; }

	public virtual DbSet<Order_Transfer_StatusType> Order_Transfer_StatusType { get; set; }

	public virtual DbSet<Order_Transfer_TrackingType> Order_Transfer_TrackingType { get; set; }

	public virtual DbSet<Order_TransferFromBranch> Order_TransferFromBranch { get; set; }

	public virtual DbSet<Order_TransferFromBranchDetails> Order_TransferFromBranchDetails { get; set; }

	public virtual DbSet<Order_TransferToBranch> Order_TransferToBranch { get; set; }

	public virtual DbSet<Order_TransferToBranchDetails> Order_TransferToBranchDetails { get; set; }

	public virtual DbSet<OrderDalala_Dalala> OrderDalala_Dalala { get; set; }

	public virtual DbSet<OrderDalala_DalalaDetails> OrderDalala_DalalaDetails { get; set; }

	public virtual DbSet<OrderDalala_Sending> OrderDalala_Sending { get; set; }

	public virtual DbSet<OrderDalala_SendingDetails> OrderDalala_SendingDetails { get; set; }

	public virtual DbSet<OrderRent_Recipt> OrderRent_Recipt { get; set; }

	public virtual DbSet<OrderRent_Recipt3> OrderRent_Recipt3 { get; set; }

	public virtual DbSet<OrderRent_RecpitDetails> OrderRent_RecpitDetails { get; set; }

	public virtual DbSet<OrderRent_RecpitDetails3> OrderRent_RecpitDetails3 { get; set; }

	public virtual DbSet<OrderRent_Rent> OrderRent_Rent { get; set; }

	public virtual DbSet<OrderRent_RentDetails> OrderRent_RentDetails { get; set; }

	public virtual DbSet<Orders_PriceShowRental> Orders_PriceShowRental { get; set; }

	public virtual DbSet<Orders_RentalInvoice> Orders_RentalInvoice { get; set; }

	public virtual DbSet<OrdersReturn_RentalInvoice> OrdersReturn_RentalInvoice { get; set; }

	public virtual DbSet<Ordert_PriceShowRentalDetails> Ordert_PriceShowRentalDetails { get; set; }

	public virtual DbSet<Ordert_RentalInvoiceDetails> Ordert_RentalInvoiceDetails { get; set; }

	public virtual DbSet<OrdertReturn_RentalInvoiceDetails> OrdertReturn_RentalInvoiceDetails { get; set; }

	public virtual DbSet<PriceOffer_Reservations> PriceOffer_Reservations { get; set; }

	public virtual DbSet<Printer> Printers { get; set; }

	public virtual DbSet<PrintersCook> PrintersCooks { get; set; }

	public virtual DbSet<RebuildAllIndex> RebuildAllIndexes { get; set; }

	public virtual DbSet<RentalExpens> RentalExpenses { get; set; }

	public virtual DbSet<RentalExpensesDetail> RentalExpensesDetails { get; set; }

	public virtual DbSet<Repairs_Groups> Repairs_Groups { get; set; }

	public virtual DbSet<Repairs_Malfunctions> Repairs_Malfunctions { get; set; }

	public virtual DbSet<Repairs_Repair> Repairs_Repair { get; set; }

	public virtual DbSet<Repairs_RepairDetails> Repairs_RepairDetails { get; set; }

	public virtual DbSet<Restaurant_Delivery> Restaurant_Delivery { get; set; }

	public virtual DbSet<Restaurant_DeliveryMan> Restaurant_DeliveryMan { get; set; }

	public virtual DbSet<Restaurant_Room> Restaurant_Room { get; set; }

	public virtual DbSet<Restaurant_Table> Restaurant_Table { get; set; }

	public virtual DbSet<Restaurant_Type> Restaurant_Type { get; set; }

	public virtual DbSet<ResultElectronicInvoiceXml> ResultElectronicInvoiceXmls { get; set; }

	public virtual DbSet<Scaffold_EntryPermission> Scaffold_EntryPermission { get; set; }

	public virtual DbSet<Scaffold_EntryPermissionDetails> Scaffold_EntryPermissionDetails { get; set; }

	public virtual DbSet<Scaffold_ExitPermission> Scaffold_ExitPermission { get; set; }

	public virtual DbSet<Scaffold_ExitPermissionDetails> Scaffold_ExitPermissionDetails { get; set; }

	public virtual DbSet<Scaffold_PriceOffer> Scaffold_PriceOffer { get; set; }

	public virtual DbSet<Scaffold_PriceOfferDetails> Scaffold_PriceOfferDetails { get; set; }

	public virtual DbSet<Scaffolds_Contract> Scaffolds_Contract { get; set; }

	public virtual DbSet<Scaffolds_ContractDetails> Scaffolds_ContractDetails { get; set; }

	public virtual DbSet<Scaffolds_MinutesEnd> Scaffolds_MinutesEnd { get; set; }

	public virtual DbSet<Scaffolds_MinutesEndDetails> Scaffolds_MinutesEndDetails { get; set; }

	public virtual DbSet<Scaffolds_MinutesStarted> Scaffolds_MinutesStarted { get; set; }

	public virtual DbSet<Scaffolds_MinutesStartedDetails> Scaffolds_MinutesStartedDetails { get; set; }

	public virtual DbSet<Tafqit> Tafqits { get; set; }

	public virtual DbSet<TblRemember> TblRemembers { get; set; }

	public virtual DbSet<TblSetting> TblSettings { get; set; }

	public virtual DbSet<Temp_Accounts> Temp_Accounts { get; set; }

	public virtual DbSet<TempTranMaterDetail> TempTranMaterDetails { get; set; }

	public virtual DbSet<TempTrialBalanceByTotal> TempTrialBalanceByTotals { get; set; }

	public virtual DbSet<Tran_Tran> Tran_Tran { get; set; }

	public virtual DbSet<Tran_TranDetails> Tran_TranDetails { get; set; }

	public virtual DbSet<Tran_TranTemp> Tran_TranTemp { get; set; }

	public virtual DbSet<Tran_TranTempDetails> Tran_TranTempDetails { get; set; }

	public virtual DbSet<Tran_Type> Tran_Type { get; set; }

	public virtual DbSet<User_DayClose> User_DayClose { get; set; }

	public virtual DbSet<User_Groups> User_Groups { get; set; }

	public virtual DbSet<User_Login> User_Login { get; set; }

	public virtual DbSet<User_Permission> User_Permission { get; set; }

	public virtual DbSet<User_Screens> User_Screens { get; set; }

	public virtual DbSet<User_SellPrice> User_SellPrice { get; set; }

	public virtual DbSet<Virg_AppType> Virg_AppType { get; set; }

	public virtual DbSet<Virg_Branch> Virg_Branch { get; set; }

	public virtual DbSet<Virg_Company> Virg_Company { get; set; }

	public virtual DbSet<Virg_Franchise> Virg_Franchise { get; set; }

	public virtual DbSet<Virg_Master> Virg_Master { get; set; }

	public virtual DbSet<Virg_MasterDetails> Virg_MasterDetails { get; set; }

	public virtual DbSet<Virg_Statuse> Virg_Statuse { get; set; }

	public virtual DbSet<Virg_Type> Virg_Type { get; set; }

	public virtual DbSet<Virg_Virgins> Virg_Virgins { get; set; }

	public virtual DbSet<Order_TypeElectronicInvoice> Order_TypeElectronicInvoice { get; set; }

	public virtual DbSet<TempAccount> TempAccounts { get; set; }

	public virtual DbSet<TempBudget> TempBudgets { get; set; }

	public virtual DbSet<View_AccountsLastLeve> View_AccountsLastLeve { get; set; }

	public virtual DbSet<View_Budget_Accounts> View_Budget_Accounts { get; set; }

	public virtual DbSet<View_TempTranMaterDetails> View_TempTranMaterDetails { get; set; }

	public virtual DbSet<View_TranDetails> View_TranDetails { get; set; }

	public virtual DbSet<ViewAccount_Data> ViewAccount_Data { get; set; }

	public virtual DbSet<ViewAccount_Data_Tree> ViewAccount_Data_Tree { get; set; }

	public virtual DbSet<ViewAccountsLastLeve> ViewAccountsLastLeves { get; set; }

	public virtual DbSet<ViewCostCenters_Data_Tree> ViewCostCenters_Data_Tree { get; set; }

	public virtual DbSet<ViewTran_TranDetails> ViewTran_TranDetails { get; set; }

	public virtual DbSet<ViewTranMaterDetail> ViewTranMaterDetails { get; set; }

	public GTSdbEntities()
		: base("name=GTSdbEntities")
	{
	}

	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		throw new UnintentionalCodeFirstException();
	}

	[DbFunction("GTSdbEntities", "ufn_FindReports")]
	public virtual IQueryable<ufn_FindReports_Result> ufn_FindReports(int? inAccountID)
	{
		ObjectParameter objectParameter = (inAccountID.HasValue ? new ObjectParameter("InAccountID", inAccountID) : new ObjectParameter("InAccountID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufn_FindReports_Result>("[GTSdbEntities].[ufn_FindReports](@InAccountID)", new ObjectParameter[1] { objectParameter });
	}

	[DbFunction("GTSdbEntities", "View_Budget_AccountsDataFun")]
	public virtual IQueryable<View_Budget_AccountsDataFun_Result> View_Budget_AccountsDataFun(int? branchID, int? userID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<View_Budget_AccountsDataFun_Result>("[GTSdbEntities].[View_Budget_AccountsDataFun](@BranchID, @UserID)", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	[DbFunction("GTSdbEntities", "View_Budget_AccountsDataFunAll")]
	public virtual IQueryable<View_Budget_AccountsDataFunAll_Result> View_Budget_AccountsDataFunAll()
	{
		return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<View_Budget_AccountsDataFunAll_Result>("[GTSdbEntities].[View_Budget_AccountsDataFunAll]()", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Account_YearEnd_Result> Account_YearEnd()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Account_YearEnd_Result>("Account_YearEnd", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<decimal?> AccountCharge(int? sn_Account)
	{
		ObjectParameter objectParameter = (sn_Account.HasValue ? new ObjectParameter("sn_Account", sn_Account) : new ObjectParameter("sn_Account", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("AccountCharge", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<AddTemp_Budget_Result> AddTemp_Budget(DateTime? toDate, int? branchID, int? userID)
	{
		ObjectParameter objectParameter = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddTemp_Budget_Result>("AddTemp_Budget", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<AddTemp_BudgetAllBranch_Result> AddTemp_BudgetAllBranch(DateTime? toDate, int? branchID, int? userID)
	{
		ObjectParameter objectParameter = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddTemp_BudgetAllBranch_Result>("AddTemp_BudgetAllBranch", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual int AddTempAgeingAnalysis(DateTime? fromDate, DateTime? toDate)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddTempAgeingAnalysis", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<AddTempTranMaterDetails_Result> AddTempTranMaterDetails(DateTime? fromDate, DateTime? toDate, decimal? account_No, int? compID)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(decimal)));
		ObjectParameter objectParameter4 = (compID.HasValue ? new ObjectParameter("CompID", compID) : new ObjectParameter("CompID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddTempTranMaterDetails_Result>("AddTempTranMaterDetails", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual int AddTempTrialBalance_New(int? pflag, DateTime? fromDate, DateTime? toDate, decimal? fromAccount_No, decimal? toAccount_No, int? branchID, bool? hideBalance)
	{
		ObjectParameter objectParameter = (pflag.HasValue ? new ObjectParameter("pflag", pflag) : new ObjectParameter("pflag", typeof(int)));
		ObjectParameter objectParameter2 = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter4 = (fromAccount_No.HasValue ? new ObjectParameter("FromAccount_No", fromAccount_No) : new ObjectParameter("FromAccount_No", typeof(decimal)));
		ObjectParameter objectParameter5 = (toAccount_No.HasValue ? new ObjectParameter("ToAccount_No", toAccount_No) : new ObjectParameter("ToAccount_No", typeof(decimal)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (hideBalance.HasValue ? new ObjectParameter("HideBalance", hideBalance) : new ObjectParameter("HideBalance", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddTempTrialBalance_New", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7);
	}

	public virtual ObjectResult<AddTempTrialBalanceByTotal_Result> AddTempTrialBalanceByTotal(int? pflag, DateTime? fromDate, DateTime? toDate, decimal? fromAccount_No, decimal? toAccount_No, int? branchID, int? userID, int? account_Level, bool? hideBalance)
	{
		ObjectParameter objectParameter = (pflag.HasValue ? new ObjectParameter("pflag", pflag) : new ObjectParameter("pflag", typeof(int)));
		ObjectParameter objectParameter2 = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter4 = (fromAccount_No.HasValue ? new ObjectParameter("FromAccount_No", fromAccount_No) : new ObjectParameter("FromAccount_No", typeof(decimal)));
		ObjectParameter objectParameter5 = (toAccount_No.HasValue ? new ObjectParameter("ToAccount_No", toAccount_No) : new ObjectParameter("ToAccount_No", typeof(decimal)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		ObjectParameter objectParameter8 = (account_Level.HasValue ? new ObjectParameter("Account_Level", account_Level) : new ObjectParameter("Account_Level", typeof(int)));
		ObjectParameter objectParameter9 = (hideBalance.HasValue ? new ObjectParameter("HideBalance", hideBalance) : new ObjectParameter("HideBalance", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AddTempTrialBalanceByTotal_Result>("AddTempTrialBalanceByTotal", new ObjectParameter[9] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9 });
	}

	public virtual ObjectResult<api_CategoriesGetAllGategories_Result> api_CategoriesGetAllGategories()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_CategoriesGetAllGategories_Result>("api_CategoriesGetAllGategories", Array.Empty<ObjectParameter>());
	}

	public virtual int api_Get_RoomDetails(int? branchID, int? roomNum)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("api_Get_RoomDetails", objectParameter, objectParameter2);
	}

	public virtual int api_Get_TableDetails(int? branchID, int? tableNum)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("api_Get_TableDetails", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<api_GetAllBranches_Result> api_GetAllBranches()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllBranches_Result>("api_GetAllBranches", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<api_GetAllItemsByGategories_Result> api_GetAllItemsByGategories(int? gategoryID)
	{
		ObjectParameter objectParameter = (gategoryID.HasValue ? new ObjectParameter("GategoryID", gategoryID) : new ObjectParameter("GategoryID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllItemsByGategories_Result>("api_GetAllItemsByGategories", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<api_GetAllRestaurant_Room_Result> api_GetAllRestaurant_Room(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllRestaurant_Room_Result>("api_GetAllRestaurant_Room", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<api_GetAllRestaurant_RoomByPause_Result> api_GetAllRestaurant_RoomByPause(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllRestaurant_RoomByPause_Result>("api_GetAllRestaurant_RoomByPause", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<api_GetAllRestaurant_Table_Result> api_GetAllRestaurant_Table(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllRestaurant_Table_Result>("api_GetAllRestaurant_Table", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<api_GetAllRestaurant_TableByPause_Result> api_GetAllRestaurant_TableByPause(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<api_GetAllRestaurant_TableByPause_Result>("api_GetAllRestaurant_TableByPause", new ObjectParameter[1] { objectParameter });
	}

	public virtual int api_Login(int? branchID, int? userID, string passWord)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		ObjectParameter objectParameter3 = ((passWord != null) ? new ObjectParameter("PassWord", passWord) : new ObjectParameter("PassWord", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("api_Login", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int ClearDB()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearDB");
	}

	public virtual int ClearEmptyDB()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearEmptyDB");
	}

	public virtual ObjectResult<Cloud_Result> Cloud(int? itemID, int? supplierID)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Cloud_Result>("Cloud", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int CreateTableType()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateTableType");
	}

	public virtual ObjectResult<DebtReconstruction_Result> DebtReconstruction(DateTime? date, string supplierIds)
	{
		ObjectParameter objectParameter = (date.HasValue ? new ObjectParameter("Date", date) : new ObjectParameter("Date", typeof(DateTime)));
		ObjectParameter objectParameter2 = ((supplierIds != null) ? new ObjectParameter("SupplierIds", supplierIds) : new ObjectParameter("SupplierIds", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DebtReconstruction_Result>("DebtReconstruction", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int Delete_Order_OpenQuantity(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OpenQuantity", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_OpenQuantity_ALL(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OpenQuantity_ALL", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_OpenQuantityDetails(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OpenQuantityDetails", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_Order_ALL(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_Order_ALL", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_Order_ALL_Draft(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_Order_ALL_Draft", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_Orders(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_Orders", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_OrdersDetails(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OrdersDetails", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_OrdersDetailsDraft(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OrdersDetailsDraft", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_OrdersDraft(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_OrdersDraft", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_Purchases(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_Purchases", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_Purchases_ALL(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_Purchases_ALL", objectParameter, objectParameter2);
	}

	public virtual int Delete_Order_PurchasesDetails(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Order_PurchasesDetails", objectParameter, objectParameter2);
	}

	public virtual int Delete_Tran_Tran(int? referenceCode, int? branchID, int? tranTypeID)
	{
		ObjectParameter objectParameter = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Tran_Tran", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int Delete_Tran_TranDetails(int? tranSn, int? branchID)
	{
		ObjectParameter objectParameter = (tranSn.HasValue ? new ObjectParameter("TranSn", tranSn) : new ObjectParameter("TranSn", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Tran_TranDetails", objectParameter, objectParameter2);
	}

	public virtual int Drop_Procedures_And_Type()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Drop_Procedures_And_Type");
	}

	public virtual int Fix_Item()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Fix_Item");
	}

	public virtual ObjectResult<Get_Account_Tree_Result> Get_Account_Tree()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Account_Tree_Result>("Get_Account_Tree", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<int?> Get_AfterRoots(long? account_No)
	{
		ObjectParameter objectParameter = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(long)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<int?>("Get_AfterRoots", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_All_Items_Result> Get_All_Items()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_All_Items_Result>("Get_All_Items", Array.Empty<ObjectParameter>());
	}

	public virtual int Get_Averg_AllItems()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_Averg_AllItems");
	}

	public virtual int Get_AvergItem(int? itemID, ObjectParameter avergItm)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_AvergItem", objectParameter, avergItm);
	}

	public virtual int Get_AvergItemByTable()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_AvergItemByTable");
	}

	public virtual ObjectResult<Get_ContractActive_DisActive_Result> Get_ContractActive_DisActive(int? branchID, DateTime? date1, bool? isBigger)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (isBigger.HasValue ? new ObjectParameter("IsBigger", isBigger) : new ObjectParameter("IsBigger", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_ContractActive_DisActive_Result>("Get_ContractActive_DisActive", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Get_ContractCustomer_Result> Get_ContractCustomer(int? custID, int? branchID)
	{
		ObjectParameter objectParameter = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_ContractCustomer_Result>("Get_ContractCustomer", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_Cust_Guarantee_Result> Get_Cust_Guarantee(int? custID, int? branchID)
	{
		ObjectParameter objectParameter = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Cust_Guarantee_Result>("Get_Cust_Guarantee", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_EmployeeAllowances_Result> Get_EmployeeAllowances(int? empID)
	{
		ObjectParameter objectParameter = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_EmployeeAllowances_Result>("Get_EmployeeAllowances", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_ExitItems_ByContractID_Result> Get_ExitItems_ByContractID(int? contractID)
	{
		ObjectParameter objectParameter = (contractID.HasValue ? new ObjectParameter("ContractID", contractID) : new ObjectParameter("ContractID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_ExitItems_ByContractID_Result>("Get_ExitItems_ByContractID", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_Item_ByCodeALL_Result> Get_Item_ByCodeALL(string code, int? branchID)
	{
		ObjectParameter objectParameter = ((code != null) ? new ObjectParameter("Code", code) : new ObjectParameter("Code", typeof(string)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Item_ByCodeALL_Result>("Get_Item_ByCodeALL", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_Item_ByID_Result> Get_Item_ByID(int? code, int? branchID)
	{
		ObjectParameter objectParameter = (code.HasValue ? new ObjectParameter("Code", code) : new ObjectParameter("Code", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Item_ByID_Result>("Get_Item_ByID", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_Item_ByItemCode_Result> Get_Item_ByItemCode(string itemCode)
	{
		ObjectParameter objectParameter = ((itemCode != null) ? new ObjectParameter("ItemCode", itemCode) : new ObjectParameter("ItemCode", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Item_ByItemCode_Result>("Get_Item_ByItemCode", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_Item_BySearchOpenQauntity_Result> Get_Item_BySearchOpenQauntity(int? purchese_ID, int? branchID)
	{
		ObjectParameter objectParameter = (purchese_ID.HasValue ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Item_BySearchOpenQauntity_Result>("Get_Item_BySearchOpenQauntity", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_Item_BySearchPurchases_Result> Get_Item_BySearchPurchases(int? purchese_ID, int? branchID)
	{
		ObjectParameter objectParameter = (purchese_ID.HasValue ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Item_BySearchPurchases_Result>("Get_Item_BySearchPurchases", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Get_ItemGroupToPrintCashier_Result> Get_ItemGroupToPrintCashier(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_ItemGroupToPrintCashier_Result>("Get_ItemGroupToPrintCashier", new ObjectParameter[1] { objectParameter });
	}

	public virtual int Get_ItemGroupToPrintCashierItem(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_ItemGroupToPrintCashierItem", objectParameter);
	}

	public virtual ObjectResult<Get_Items_ByCode_Result> Get_Items_ByCode(string code)
	{
		ObjectParameter objectParameter = ((code != null) ? new ObjectParameter("Code", code) : new ObjectParameter("Code", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Items_ByCode_Result>("Get_Items_ByCode", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_Items_Stock_Report_Result> Get_Items_Stock_Report()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Items_Stock_Report_Result>("Get_Items_Stock_Report", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Get_MizanBarcode_Result> Get_MizanBarcode()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_MizanBarcode_Result>("Get_MizanBarcode", Array.Empty<ObjectParameter>());
	}

	public virtual int Get_ProfitAndLossAccount(DateTime? fromDate, DateTime? toDate, int? branchID, int? costID)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (costID.HasValue ? new ObjectParameter("CostID", costID) : new ObjectParameter("CostID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_ProfitAndLossAccount", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Get_ProfitAndLossAccountAllBranch(DateTime? fromDate, DateTime? toDate)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_ProfitAndLossAccountAllBranch", objectParameter, objectParameter2);
	}

	public virtual int Get_Report_OrdersPurches(int? branchID, DateTime? date1, DateTime? date2, int? category_ID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter4 = (category_ID.HasValue ? new ObjectParameter("Category_ID", category_ID) : new ObjectParameter("Category_ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_Report_OrdersPurches", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Get_Report_OrdersPurches_ByGroups(int? branchID, int? category_ID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (category_ID.HasValue ? new ObjectParameter("Category_ID", category_ID) : new ObjectParameter("Category_ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_Report_OrdersPurches_ByGroups", objectParameter, objectParameter2);
	}

	public virtual int Get_Report_QuantityOrdersPurches(int? branchID, DateTime? date1, DateTime? date2, int? category_ID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter4 = (category_ID.HasValue ? new ObjectParameter("Category_ID", category_ID) : new ObjectParameter("Category_ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Get_Report_QuantityOrdersPurches", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual ObjectResult<Get_Restaurant_Delivery_Result> Get_Restaurant_Delivery(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Restaurant_Delivery_Result>("Get_Restaurant_Delivery", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_Scaffolds_Contracts_Filter_Result> Get_Scaffolds_Contracts_Filter(string dateType, int? status, DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = ((dateType != null) ? new ObjectParameter("DateType", dateType) : new ObjectParameter("DateType", typeof(string)));
		ObjectParameter objectParameter2 = (status.HasValue ? new ObjectParameter("Status", status) : new ObjectParameter("Status", typeof(int)));
		ObjectParameter objectParameter3 = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter4 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Scaffolds_Contracts_Filter_Result>("Get_Scaffolds_Contracts_Filter", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<Get_Serial_Result> Get_Serial(string hDD)
	{
		ObjectParameter objectParameter = ((hDD != null) ? new ObjectParameter("HDD", hDD) : new ObjectParameter("HDD", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Serial_Result>("Get_Serial", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_Supplier_Items_Scaffold_Result> Get_Supplier_Items_Scaffold(int? supplierID)
	{
		ObjectParameter objectParameter = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Supplier_Items_Scaffold_Result>("Get_Supplier_Items_Scaffold", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Get_SupplierAccount_Result> Get_SupplierAccount()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_SupplierAccount_Result>("Get_SupplierAccount", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Get_virginByStatuse_Result> Get_virginByStatuse(int? statuseID, int? franchiseID)
	{
		ObjectParameter objectParameter = (statuseID.HasValue ? new ObjectParameter("StatuseID", statuseID) : new ObjectParameter("StatuseID", typeof(int)));
		ObjectParameter objectParameter2 = (franchiseID.HasValue ? new ObjectParameter("FranchiseID", franchiseID) : new ObjectParameter("FranchiseID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_virginByStatuse_Result>("Get_virginByStatuse", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<decimal?> GetAccountBalance(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("GetAccountBalance", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<decimal?> GetAccountBalanceByAccID(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("GetAccountBalanceByAccID", new ObjectParameter[1] { objectParameter });
	}

	public virtual int GetAccountPath(decimal? account_No)
	{
		ObjectParameter objectParameter = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAccountPath", objectParameter);
	}

	public virtual ObjectResult<GetAccountTree_Result> GetAccountTree()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAccountTree_Result>("GetAccountTree", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<GetAllBranches_Result> GetAllBranches()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllBranches_Result>("GetAllBranches", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<GetAllitems_Alternatives_Result> GetAllitems_Alternatives(int? itemID, int? storeID, int? unitType)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (unitType.HasValue ? new ObjectParameter("UnitType", unitType) : new ObjectParameter("UnitType", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllitems_Alternatives_Result>("GetAllitems_Alternatives", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual int GetBalanceGroupAccounts(DateTime? fromDate, DateTime? toDate, decimal? account_No, int? branchID, int? nowBalance)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(decimal)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (nowBalance.HasValue ? new ObjectParameter("NowBalance", nowBalance) : new ObjectParameter("NowBalance", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetBalanceGroupAccounts", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5);
	}

	public virtual ObjectResult<GetCostCenterDetails_Result> GetCostCenterDetails(DateTime? date1, DateTime? date2, int? costID, int? branchID, int? typeID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (costID.HasValue ? new ObjectParameter("CostID", costID) : new ObjectParameter("CostID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCostCenterDetails_Result>("GetCostCenterDetails", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<GetCsrZakat_Result> GetCsrZakat(int? branchId, int? userId)
	{
		ObjectParameter objectParameter = (branchId.HasValue ? new ObjectParameter("BranchId", branchId) : new ObjectParameter("BranchId", typeof(int)));
		ObjectParameter objectParameter2 = (userId.HasValue ? new ObjectParameter("UserId", userId) : new ObjectParameter("UserId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCsrZakat_Result>("GetCsrZakat", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<GetDataForZakatToCrm_Result> GetDataForZakatToCrm(int? branchId)
	{
		ObjectParameter objectParameter = (branchId.HasValue ? new ObjectParameter("BranchId", branchId) : new ObjectParameter("BranchId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDataForZakatToCrm_Result>("GetDataForZakatToCrm", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetItemDetails_Result> GetItemDetails(string code)
	{
		ObjectParameter objectParameter = ((code != null) ? new ObjectParameter("Code", code) : new ObjectParameter("Code", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetItemDetails_Result>("GetItemDetails", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetItemQuantityByBranch_Result> GetItemQuantityByBranch(int? itemID)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetItemQuantityByBranch_Result>("GetItemQuantityByBranch", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetLastOrderDetailsByItemID_Result> GetLastOrderDetailsByItemID(int? itemID)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLastOrderDetailsByItemID_Result>("GetLastOrderDetailsByItemID", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<decimal?> GetLedger_Employee(int? accountID)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("GetLedger_Employee", new ObjectParameter[1] { objectParameter });
	}

	public virtual int GetOrderDetailsByInvoiceID(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetOrderDetailsByInvoiceID", objectParameter);
	}

	public virtual int GetOrderDetailsWithReturns(int? orderID)
	{
		ObjectParameter objectParameter = (orderID.HasValue ? new ObjectParameter("OrderID", orderID) : new ObjectParameter("OrderID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetOrderDetailsWithReturns", objectParameter);
	}

	public virtual int GetOrderRentalDetails(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetOrderRentalDetails", objectParameter);
	}

	public virtual ObjectResult<GetOrderReservations_Result> GetOrderReservations(DateTime? date1, DateTime? date2, int? mM)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (mM.HasValue ? new ObjectParameter("MM", mM) : new ObjectParameter("MM", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderReservations_Result>("GetOrderReservations", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetOrdersWithDetails_Result> GetOrdersWithDetails(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrdersWithDetails_Result>("GetOrdersWithDetails", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<GetOrdersWithDetailsDraft_Result> GetOrdersWithDetailsDraft(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrdersWithDetailsDraft_Result>("GetOrdersWithDetailsDraft", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<GetPriceOffersWithDetails_Result> GetPriceOffersWithDetails(int? purBranchID, int? branchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPriceOffersWithDetails_Result>("GetPriceOffersWithDetails", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int GetPrinter_ID_Name(int? itemID, ObjectParameter printersID, ObjectParameter printersName)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetPrinter_ID_Name", objectParameter, printersID, printersName);
	}

	public virtual int GetQuantityFromItem_ByItemCode_StoreId(string itmCode, int? storeNum)
	{
		ObjectParameter objectParameter = ((itmCode != null) ? new ObjectParameter("ItmCode", itmCode) : new ObjectParameter("ItmCode", typeof(string)));
		ObjectParameter objectParameter2 = (storeNum.HasValue ? new ObjectParameter("StoreNum", storeNum) : new ObjectParameter("StoreNum", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetQuantityFromItem_ByItemCode_StoreId", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<GetRecpitInvoiceDetails_Result> GetRecpitInvoiceDetails(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRecpitInvoiceDetails_Result>("GetRecpitInvoiceDetails", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetRentalInvoiceDetails_Result> GetRentalInvoiceDetails(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRentalInvoiceDetails_Result>("GetRentalInvoiceDetails", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetRented_Result> GetRented(int? itemID, int? branchID, int? storeID)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRented_Result>("GetRented", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetRepairWithDetails_Result> GetRepairWithDetails(int? repairId)
	{
		ObjectParameter objectParameter = (repairId.HasValue ? new ObjectParameter("RepairId", repairId) : new ObjectParameter("RepairId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRepairWithDetails_Result>("GetRepairWithDetails", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetReport_BounceContract_Result> GetReport_BounceContract(int? bounceID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_BounceContract_Result>("GetReport_BounceContract", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_Car_Result> GetReport_Car(int? driverID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (driverID.HasValue ? new ObjectParameter("DriverID", driverID) : new ObjectParameter("DriverID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Car_Result>("GetReport_Car", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_Contract_Result> GetReport_Contract(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Contract_Result>("GetReport_Contract", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual int GetReport_CostCenters(DateTime? fromDate, DateTime? toDate, decimal? account_No, decimal? costCentersID, int? branchID, int? pflag)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(decimal)));
		ObjectParameter objectParameter4 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(decimal)));
		ObjectParameter objectParameter5 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter6 = (pflag.HasValue ? new ObjectParameter("pflag", pflag) : new ObjectParameter("pflag", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_CostCenters", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6);
	}

	public virtual int GetReport_CostCentersByFather(DateTime? fromDate, DateTime? toDate, decimal? account_No, decimal? costCentersID, int? branchID, int? pflag)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (account_No.HasValue ? new ObjectParameter("Account_No", account_No) : new ObjectParameter("Account_No", typeof(decimal)));
		ObjectParameter objectParameter4 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(decimal)));
		ObjectParameter objectParameter5 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter6 = (pflag.HasValue ? new ObjectParameter("pflag", pflag) : new ObjectParameter("pflag", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_CostCentersByFather", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6);
	}

	public virtual int GetReport_EmployeeSales(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_EmployeeSales", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int GetReport_EmployeeSales_2(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_EmployeeSales_2", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual ObjectResult<GetReport_Guarantee_Result> GetReport_Guarantee(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Guarantee_Result>("GetReport_Guarantee", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_ItemsMenu_Result> GetReport_ItemsMenu()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_ItemsMenu_Result>("GetReport_ItemsMenu", Array.Empty<ObjectParameter>());
	}

	public virtual int GetReport_MovementAllItemActive(int? branchID, DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter3 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementAllItemActive", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual ObjectResult<GetReport_MovementAllItemActive_Summary_Result> GetReport_MovementAllItemActive_Summary(int? branchID, DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter3 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_MovementAllItemActive_Summary_Result>("GetReport_MovementAllItemActive_Summary", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual int GetReport_MovementItem(int? itemID, int? branchID)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementItem", objectParameter, objectParameter2);
	}

	public virtual int GetReport_MovementItemActive(int? itemID, int? branchID, DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter4 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementItemActive", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int GetReport_MovementItemBetweenToDate(int? itemID, int? branchID, DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter4 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementItemBetweenToDate", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int GetReport_MovementsDailyReport(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementsDailyReport", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int GetReport_MovementsDailyReport2(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementsDailyReport2", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int GetReport_MovementsDailyReport2_ByUser(DateTime? date1, DateTime? date2, int? branchID, int? userID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementsDailyReport2_ByUser", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int GetReport_MovementsDailyReportAllBranch(DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementsDailyReportAllBranch", objectParameter, objectParameter2);
	}

	public virtual int GetReport_MovementsDailyReportCloseDay(DateTime? date1, DateTime? date2, int? branchID, int? userID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_MovementsDailyReportCloseDay", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int GetReport_NetProfitOrdersBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_NetProfitOrdersBySalesMan", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6);
	}

	public virtual ObjectResult<GetReport_Order_Reservation_Result> GetReport_Order_Reservation(DateTime? date1, DateTime? date2, int? typeID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Order_Reservation_Result>("GetReport_Order_Reservation", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_Order_TransferfromBranch_Result> GetReport_Order_TransferfromBranch(DateTime? date1, DateTime? date2, int? typeID, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Order_TransferfromBranch_Result>("GetReport_Order_TransferfromBranch", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_Order_TransferToBranch_Result> GetReport_Order_TransferToBranch(DateTime? date1, DateTime? date2, int? typeID, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Order_TransferToBranch_Result>("GetReport_Order_TransferToBranch", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderDalala_Dalala_Result> GetReport_OrderDalala_Dalala(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDalala_Dalala_Result>("GetReport_OrderDalala_Dalala", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderDalala_Sending_Result> GetReport_OrderDalala_Sending(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDalala_Sending_Result>("GetReport_OrderDalala_Sending", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderDetails_Result> GetReport_OrderDetails(int? cashType, int? branchID, int? custID, DateTime? date1Ord, DateTime? date2Ord, int? storeId)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter4 = (date1Ord.HasValue ? new ObjectParameter("Date1Ord", date1Ord) : new ObjectParameter("Date1Ord", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2Ord.HasValue ? new ObjectParameter("Date2Ord", date2Ord) : new ObjectParameter("Date2Ord", typeof(DateTime)));
		ObjectParameter objectParameter6 = (storeId.HasValue ? new ObjectParameter("StoreId", storeId) : new ObjectParameter("StoreId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetails_Result>("GetReport_OrderDetails", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_OrderDetailsByBounce_Result> GetReport_OrderDetailsByBounce(int? branchID, int? bounceID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetailsByBounce_Result>("GetReport_OrderDetailsByBounce", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderDetailsByCatogry_Result> GetReport_OrderDetailsByCatogry(int? cashType, int? branchID, int? custID, int? empID, int? groupID, int? classID, int? companyID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter4 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter5 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter6 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter7 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter8 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter9 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetailsByCatogry_Result>("GetReport_OrderDetailsByCatogry", new ObjectParameter[9] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9 });
	}

	public virtual ObjectResult<GetReport_OrderDetailsByGroups_Result> GetReport_OrderDetailsByGroups(int? groupID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetailsByGroups_Result>("GetReport_OrderDetailsByGroups", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderDetailsBySalesMan_Result> GetReport_OrderDetailsBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetailsBySalesMan_Result>("GetReport_OrderDetailsBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_OrderDetailsSmall_Result> GetReport_OrderDetailsSmall(int? branchID, DateTime? date1Ord, DateTime? date2Ord)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1Ord.HasValue ? new ObjectParameter("Date1Ord", date1Ord) : new ObjectParameter("Date1Ord", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2Ord.HasValue ? new ObjectParameter("Date2Ord", date2Ord) : new ObjectParameter("Date2Ord", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderDetailsSmall_Result>("GetReport_OrderDetailsSmall", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderProfit_Result> GetReport_OrderProfit(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfit_Result>("GetReport_OrderProfit", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByGroup_Result> GetReport_OrderProfitByGroup(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByGroup_Result>("GetReport_OrderProfitByGroup", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByGroupBySalesMan_Result> GetReport_OrderProfitByGroupBySalesMan(int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter2 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByGroupBySalesMan_Result>("GetReport_OrderProfitByGroupBySalesMan", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByItem_Result> GetReport_OrderProfitByItem(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByItem_Result>("GetReport_OrderProfitByItem", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByItem_ItemOnly_Result> GetReport_OrderProfitByItem_ItemOnly(DateTime? date1, DateTime? date2, int? itemID, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByItem_ItemOnly_Result>("GetReport_OrderProfitByItem_ItemOnly", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByItem_ItemOnlyBySalesMan_Result> GetReport_OrderProfitByItem_ItemOnlyBySalesMan(int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2, int? itemID)
	{
		ObjectParameter objectParameter = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter2 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter6 = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByItem_ItemOnlyBySalesMan_Result>("GetReport_OrderProfitByItem_ItemOnlyBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_OrderProfitByItemCashierSafryMahly_Result> GetReport_OrderProfitByItemCashierSafryMahly(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitByItemCashierSafryMahly_Result>("GetReport_OrderProfitByItemCashierSafryMahly", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderProfitBySalesMan_Result> GetReport_OrderProfitBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderProfitBySalesMan_Result>("GetReport_OrderProfitBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_OrderRent_Result> GetReport_OrderRent(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderRent_Result>("GetReport_OrderRent", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<GetReport_OrderReturnDetails_Result> GetReport_OrderReturnDetails(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderReturnDetails_Result>("GetReport_OrderReturnDetails", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrderReturnDetailsBySalesMan_Result> GetReport_OrderReturnDetailsBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrderReturnDetailsBySalesMan_Result>("GetReport_OrderReturnDetailsBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_Orders_Result> GetReport_Orders(int? cashType, int? branchID, int? custID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Orders_Result>("GetReport_Orders", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<GetReport_Orders_ByProperties_Result> GetReport_Orders_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Orders_ByProperties_Result>("GetReport_Orders_ByProperties", new ObjectParameter[10] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10 });
	}

	public virtual ObjectResult<GetReport_Orders_CashierSafryMahly_Result> GetReport_Orders_CashierSafryMahly(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Orders_CashierSafryMahly_Result>("GetReport_Orders_CashierSafryMahly", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrdersBounce_Result> GetReport_OrdersBounce(int? cashType, int? branchID, int? bounceID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersBounce_Result>("GetReport_OrdersBounce", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<GetReport_OrdersBySalesMan_Result> GetReport_OrdersBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersBySalesMan_Result>("GetReport_OrdersBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_OrdersDateHold_Result> GetReport_OrdersDateHold(int? cashType, int? branchID, int? custID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersDateHold_Result>("GetReport_OrdersDateHold", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual int GetReport_OrdersNet_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_OrdersNet_ByProperties", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10);
	}

	public virtual int GetReport_OrdersNetPurchOnly_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_OrdersNetPurchOnly_ByProperties", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10);
	}

	public virtual int GetReport_OrdersPurchNet_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_OrdersPurchNet_ByProperties", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10);
	}

	public virtual ObjectResult<GetReport_OrdersReturn_Result> GetReport_OrdersReturn(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersReturn_Result>("GetReport_OrdersReturn", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_OrdersReturn_ByProperties_Result> GetReport_OrdersReturn_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersReturn_ByProperties_Result>("GetReport_OrdersReturn_ByProperties", new ObjectParameter[10] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10 });
	}

	public virtual ObjectResult<GetReport_OrdersReturnBySalesMan_Result> GetReport_OrdersReturnBySalesMan(int? cashType, int? custID, int? salesManID, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (salesManID.HasValue ? new ObjectParameter("SalesManID", salesManID) : new ObjectParameter("SalesManID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter5 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter6 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_OrdersReturnBySalesMan_Result>("GetReport_OrdersReturnBySalesMan", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual int GetReport_Project(DateTime? fromDate, DateTime? toDate, int? projectID, int? branchID)
	{
		ObjectParameter objectParameter = (fromDate.HasValue ? new ObjectParameter("FromDate", fromDate) : new ObjectParameter("FromDate", typeof(DateTime)));
		ObjectParameter objectParameter2 = (toDate.HasValue ? new ObjectParameter("ToDate", toDate) : new ObjectParameter("ToDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = (projectID.HasValue ? new ObjectParameter("ProjectID", projectID) : new ObjectParameter("ProjectID", typeof(int)));
		ObjectParameter objectParameter4 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_Project", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual ObjectResult<GetReport_PurchaesReturnDetails_Result> GetReport_PurchaesReturnDetails(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_PurchaesReturnDetails_Result>("GetReport_PurchaesReturnDetails", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual int GetReport_PurchasesNet_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_PurchasesNet_ByProperties", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10);
	}

	public virtual ObjectResult<GetReport_Purches_Result> GetReport_Purches(int? cashType, int? branchID, int? supID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supID.HasValue ? new ObjectParameter("SupID", supID) : new ObjectParameter("SupID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Purches_Result>("GetReport_Purches", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<GetReport_Purches_ByProperties_Result> GetReport_Purches_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_Purches_ByProperties_Result>("GetReport_Purches_ByProperties", new ObjectParameter[10] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10 });
	}

	public virtual ObjectResult<GetReport_PurchesDetails_Result> GetReport_PurchesDetails(int? cashType, int? branchID, int? custID, DateTime? date1, DateTime? date2, int? storeId)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter4 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter5 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter6 = (storeId.HasValue ? new ObjectParameter("StoreId", storeId) : new ObjectParameter("StoreId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_PurchesDetails_Result>("GetReport_PurchesDetails", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<GetReport_PurchesReturn_Result> GetReport_PurchesReturn(int? cashType, int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter4 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_PurchesReturn_Result>("GetReport_PurchesReturn", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual ObjectResult<GetReport_PurchesReturn_ByProperties_Result> GetReport_PurchesReturn_ByProperties(int? cashType, int? custID, int? empID, int? groupID, int? companyID, int? branchID, int? classID, int? storeID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (cashType.HasValue ? new ObjectParameter("CashType", cashType) : new ObjectParameter("CashType", typeof(int)));
		ObjectParameter objectParameter2 = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter3 = (empID.HasValue ? new ObjectParameter("EmpID", empID) : new ObjectParameter("EmpID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		ObjectParameter objectParameter8 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter9 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter10 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_PurchesReturn_ByProperties_Result>("GetReport_PurchesReturn_ByProperties", new ObjectParameter[10] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10 });
	}

	public virtual ObjectResult<GetReport_RentalExpensesDetails_Result> GetReport_RentalExpensesDetails(DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_RentalExpensesDetails_Result>("GetReport_RentalExpensesDetails", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<GetReport_RevenuesAndExpensesByItemCode_Result> GetReport_RevenuesAndExpensesByItemCode(string itemCode, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = ((itemCode != null) ? new ObjectParameter("ItemCode", itemCode) : new ObjectParameter("ItemCode", typeof(string)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_RevenuesAndExpensesByItemCode_Result>("GetReport_RevenuesAndExpensesByItemCode", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual int GetReport_TobaccoTax(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_TobaccoTax", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual ObjectResult<GetReport_TransActionItems_Result> GetReport_TransActionItems(int? storeID)
	{
		ObjectParameter objectParameter = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReport_TransActionItems_Result>("GetReport_TransActionItems", new ObjectParameter[1] { objectParameter });
	}

	public virtual int GetReport_VatReport(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_VatReport", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int GetReport_Wedding(DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetReport_Wedding", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<GetReportItemLimit_Result> GetReportItemLimit(int? storeID)
	{
		ObjectParameter objectParameter = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReportItemLimit_Result>("GetReportItemLimit", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetRes_Result> GetRes(int? id)
	{
		ObjectParameter objectParameter = (id.HasValue ? new ObjectParameter("Id", id) : new ObjectParameter("Id", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRes_Result>("GetRes", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<decimal?> GetTotal_DalalaQuantity(int? iD, int? itemID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("GetTotal_DalalaQuantity", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<GetTotalAvergForSore_ByGroup_Result> GetTotalAvergForSore_ByGroup(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTotalAvergForSore_ByGroup_Result>("GetTotalAvergForSore_ByGroup", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetTotalAvergForSore_ByItem_Result> GetTotalAvergForSore_ByItem(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTotalAvergForSore_ByItem_Result>("GetTotalAvergForSore_ByItem", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetTotalAvergForSoreScaffolds_ByItem_Result> GetTotalAvergForSoreScaffolds_ByItem(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTotalAvergForSoreScaffolds_ByItem_Result>("GetTotalAvergForSoreScaffolds_ByItem", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<decimal?> GetTotalMoneyByProjectId(int? id)
	{
		ObjectParameter objectParameter = (id.HasValue ? new ObjectParameter("Id", id) : new ObjectParameter("Id", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<decimal?>("GetTotalMoneyByProjectId", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<GetTranTeacher_Result> GetTranTeacher(DateTime? date1, DateTime? date2, int? branchID, int? tranType)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (tranType.HasValue ? new ObjectParameter("TranType", tranType) : new ObjectParameter("TranType", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTranTeacher_Result>("GetTranTeacher", new ObjectParameter[4] { objectParameter, objectParameter2, objectParameter3, objectParameter4 });
	}

	public virtual int GetTranTeacherByAccountCashBox(DateTime? date1, DateTime? date2, int? branchID, int? tranType, int? accountID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter4 = (tranType.HasValue ? new ObjectParameter("TranType", tranType) : new ObjectParameter("TranType", typeof(int)));
		ObjectParameter objectParameter5 = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetTranTeacherByAccountCashBox", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5);
	}

	public virtual ObjectResult<string> GetUnitNameByID(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetUnitNameByID", new ObjectParameter[1] { objectParameter });
	}

	public virtual int Insert_Gard(int? branchID, DateTime? gardDate, string noteNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (gardDate.HasValue ? new ObjectParameter("GardDate", gardDate) : new ObjectParameter("GardDate", typeof(DateTime)));
		ObjectParameter objectParameter3 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter4 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter5 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter6 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Gard", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6);
	}

	public virtual int Insert_GardDetails(int? purBranchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_GardDetails", objectParameter);
	}

	public virtual int Insert_Item_Unit(string name, int? userID_Add, int? userBranch_Add, string userMacAddress_Add)
	{
		ObjectParameter objectParameter = ((name != null) ? new ObjectParameter("Name", name) : new ObjectParameter("Name", typeof(string)));
		ObjectParameter objectParameter2 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter3 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter4 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Item_Unit", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Insert_Items()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Items");
	}

	public virtual int Insert_ItemsByExecl()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_ItemsByExecl");
	}

	public virtual int Insert_OpenQuantity(int? branchID, DateTime? purchases_Date, string noteNum, decimal? totalPrices, int? userID_Add, int? userBranch_Add, string userMacAddress_Add)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter3 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter4 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter5 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter6 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter7 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_OpenQuantity", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7);
	}

	public virtual int Insert_OpenQuantityDetails(int? purBranchID)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_OpenQuantityDetails", objectParameter);
	}

	public virtual int Insert_Order_Order_ALL(int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? hasmPer, decimal? hasmAmount, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? projectID, int? roomNum, int? tableNum, int? sectoral, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge, decimal? allDiscount, decimal? bounsAmount, string disCode, decimal? amountPaid, decimal? rest, string carName, string carModel, string plateNumber, string chassisNum, string carColor, string counter)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter4 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter6 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter7 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter8 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter9 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter10 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter11 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter12 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter13 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter14 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter15 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter16 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter17 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter18 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter19 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter20 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter21 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter22 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter23 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter24 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter25 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter26 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter27 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter29 = (hasmPer.HasValue ? new ObjectParameter("HasmPer", hasmPer) : new ObjectParameter("HasmPer", typeof(decimal)));
		ObjectParameter objectParameter30 = (hasmAmount.HasValue ? new ObjectParameter("HasmAmount", hasmAmount) : new ObjectParameter("HasmAmount", typeof(decimal)));
		ObjectParameter objectParameter31 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter33 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter35 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter37 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter38 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter39 = (projectID.HasValue ? new ObjectParameter("ProjectID", projectID) : new ObjectParameter("ProjectID", typeof(int)));
		ObjectParameter objectParameter40 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter41 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter42 = (sectoral.HasValue ? new ObjectParameter("Sectoral", sectoral) : new ObjectParameter("Sectoral", typeof(int)));
		ObjectParameter objectParameter43 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter44 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter45 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter46 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter47 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter48 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter49 = (allDiscount.HasValue ? new ObjectParameter("AllDiscount", allDiscount) : new ObjectParameter("AllDiscount", typeof(decimal)));
		ObjectParameter objectParameter50 = (bounsAmount.HasValue ? new ObjectParameter("BounsAmount", bounsAmount) : new ObjectParameter("BounsAmount", typeof(decimal)));
		ObjectParameter objectParameter51 = ((disCode != null) ? new ObjectParameter("DisCode", disCode) : new ObjectParameter("DisCode", typeof(string)));
		ObjectParameter objectParameter52 = (amountPaid.HasValue ? new ObjectParameter("AmountPaid", amountPaid) : new ObjectParameter("AmountPaid", typeof(decimal)));
		ObjectParameter objectParameter53 = (rest.HasValue ? new ObjectParameter("Rest", rest) : new ObjectParameter("Rest", typeof(decimal)));
		ObjectParameter objectParameter54 = ((carName != null) ? new ObjectParameter("CarName", carName) : new ObjectParameter("CarName", typeof(string)));
		ObjectParameter objectParameter55 = ((carModel != null) ? new ObjectParameter("CarModel", carModel) : new ObjectParameter("CarModel", typeof(string)));
		ObjectParameter objectParameter56 = ((plateNumber != null) ? new ObjectParameter("PlateNumber", plateNumber) : new ObjectParameter("PlateNumber", typeof(string)));
		ObjectParameter objectParameter57 = ((chassisNum != null) ? new ObjectParameter("ChassisNum", chassisNum) : new ObjectParameter("ChassisNum", typeof(string)));
		ObjectParameter objectParameter58 = ((carColor != null) ? new ObjectParameter("CarColor", carColor) : new ObjectParameter("CarColor", typeof(string)));
		ObjectParameter objectParameter59 = ((counter != null) ? new ObjectParameter("Counter", counter) : new ObjectParameter("Counter", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Order_ALL", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46, objectParameter47, objectParameter48, objectParameter49, objectParameter50, objectParameter51, objectParameter52, objectParameter53, objectParameter54, objectParameter55, objectParameter56, objectParameter57, objectParameter58, objectParameter59);
	}

	public virtual int Insert_Order_Order_ALL_Draft(int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? hasmPer, decimal? hasmAmount, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? projectID, int? roomNum, int? tableNum, int? sectoral, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge, decimal? allDiscount, decimal? bounsAmount, string disCode, decimal? amountPaid, decimal? rest, string carName, string carModel, string plateNumber, string chassisNum, string carColor, string counter)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter4 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter6 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter7 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter8 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter9 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter10 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter11 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter12 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter13 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter14 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter15 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter16 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter17 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter18 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter19 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter20 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter21 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter22 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter23 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter24 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter25 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter26 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter27 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter29 = (hasmPer.HasValue ? new ObjectParameter("HasmPer", hasmPer) : new ObjectParameter("HasmPer", typeof(decimal)));
		ObjectParameter objectParameter30 = (hasmAmount.HasValue ? new ObjectParameter("HasmAmount", hasmAmount) : new ObjectParameter("HasmAmount", typeof(decimal)));
		ObjectParameter objectParameter31 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter33 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter35 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter37 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter38 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter39 = (projectID.HasValue ? new ObjectParameter("ProjectID", projectID) : new ObjectParameter("ProjectID", typeof(int)));
		ObjectParameter objectParameter40 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter41 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter42 = (sectoral.HasValue ? new ObjectParameter("Sectoral", sectoral) : new ObjectParameter("Sectoral", typeof(int)));
		ObjectParameter objectParameter43 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter44 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter45 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter46 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter47 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter48 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter49 = (allDiscount.HasValue ? new ObjectParameter("AllDiscount", allDiscount) : new ObjectParameter("AllDiscount", typeof(decimal)));
		ObjectParameter objectParameter50 = (bounsAmount.HasValue ? new ObjectParameter("BounsAmount", bounsAmount) : new ObjectParameter("BounsAmount", typeof(decimal)));
		ObjectParameter objectParameter51 = ((disCode != null) ? new ObjectParameter("DisCode", disCode) : new ObjectParameter("DisCode", typeof(string)));
		ObjectParameter objectParameter52 = (amountPaid.HasValue ? new ObjectParameter("AmountPaid", amountPaid) : new ObjectParameter("AmountPaid", typeof(decimal)));
		ObjectParameter objectParameter53 = (rest.HasValue ? new ObjectParameter("Rest", rest) : new ObjectParameter("Rest", typeof(decimal)));
		ObjectParameter objectParameter54 = ((carName != null) ? new ObjectParameter("CarName", carName) : new ObjectParameter("CarName", typeof(string)));
		ObjectParameter objectParameter55 = ((carModel != null) ? new ObjectParameter("CarModel", carModel) : new ObjectParameter("CarModel", typeof(string)));
		ObjectParameter objectParameter56 = ((plateNumber != null) ? new ObjectParameter("PlateNumber", plateNumber) : new ObjectParameter("PlateNumber", typeof(string)));
		ObjectParameter objectParameter57 = ((chassisNum != null) ? new ObjectParameter("ChassisNum", chassisNum) : new ObjectParameter("ChassisNum", typeof(string)));
		ObjectParameter objectParameter58 = ((carColor != null) ? new ObjectParameter("CarColor", carColor) : new ObjectParameter("CarColor", typeof(string)));
		ObjectParameter objectParameter59 = ((counter != null) ? new ObjectParameter("Counter", counter) : new ObjectParameter("Counter", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Order_ALL_Draft", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46, objectParameter47, objectParameter48, objectParameter49, objectParameter50, objectParameter51, objectParameter52, objectParameter53, objectParameter54, objectParameter55, objectParameter56, objectParameter57, objectParameter58, objectParameter59);
	}

	public virtual int Insert_Order_Order_ALL2(int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? hasmPer, decimal? hasmAmount, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter4 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter6 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter7 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter8 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter9 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter10 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter11 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter12 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter13 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter14 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter15 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter16 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter17 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter18 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter19 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter20 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter21 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter22 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter23 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter24 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter25 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter26 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter27 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter29 = (hasmPer.HasValue ? new ObjectParameter("HasmPer", hasmPer) : new ObjectParameter("HasmPer", typeof(decimal)));
		ObjectParameter objectParameter30 = (hasmAmount.HasValue ? new ObjectParameter("HasmAmount", hasmAmount) : new ObjectParameter("HasmAmount", typeof(decimal)));
		ObjectParameter objectParameter31 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter33 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter35 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter37 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter38 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter39 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter40 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter41 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter42 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter43 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter44 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter45 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter46 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Order_ALL2", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46);
	}

	public virtual int Insert_Order_OrdersDetails(bool? iSWaitng, int? purBranchID)
	{
		ObjectParameter objectParameter = (iSWaitng.HasValue ? new ObjectParameter("ISWaitng", iSWaitng) : new ObjectParameter("ISWaitng", typeof(bool)));
		ObjectParameter objectParameter2 = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_OrdersDetails", objectParameter, objectParameter2);
	}

	public virtual int Insert_Order_OrdersDetails_Restaurant_A(bool? iSWaitng, int? purBranchID)
	{
		ObjectParameter objectParameter = (iSWaitng.HasValue ? new ObjectParameter("ISWaitng", iSWaitng) : new ObjectParameter("ISWaitng", typeof(bool)));
		ObjectParameter objectParameter2 = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_OrdersDetails_Restaurant_A", objectParameter, objectParameter2);
	}

	public virtual int Insert_Order_OrdersDetailsDraft(bool? iSWaitng, int? purBranchID)
	{
		ObjectParameter objectParameter = (iSWaitng.HasValue ? new ObjectParameter("ISWaitng", iSWaitng) : new ObjectParameter("ISWaitng", typeof(bool)));
		ObjectParameter objectParameter2 = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_OrdersDetailsDraft", objectParameter, objectParameter2);
	}

	public virtual int Insert_Order_Purchases(int? branchID, int? supplierID, string supplierName, string supplierPhone, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, int? costCentersID, string note, string noteNum, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? net, decimal? cash, decimal? bank, int? acc_Cash, int? acc_Bank, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, decimal? charge, int? projectId)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter4 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter6 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter7 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter8 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter9 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter10 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter11 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter12 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter13 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter14 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter15 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter16 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter17 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter18 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter19 = (cash.HasValue ? new ObjectParameter("Cash", cash) : new ObjectParameter("Cash", typeof(decimal)));
		ObjectParameter objectParameter20 = (bank.HasValue ? new ObjectParameter("Bank", bank) : new ObjectParameter("Bank", typeof(decimal)));
		ObjectParameter objectParameter21 = (acc_Cash.HasValue ? new ObjectParameter("Acc_Cash", acc_Cash) : new ObjectParameter("Acc_Cash", typeof(int)));
		ObjectParameter objectParameter22 = (acc_Bank.HasValue ? new ObjectParameter("Acc_Bank", acc_Bank) : new ObjectParameter("Acc_Bank", typeof(int)));
		ObjectParameter objectParameter23 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter24 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter25 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter26 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter27 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter28 = (projectId.HasValue ? new ObjectParameter("ProjectId", projectId) : new ObjectParameter("ProjectId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Purchases", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28);
	}

	public virtual int Insert_Order_PurchasesDetails(int? purchese_ID)
	{
		ObjectParameter objectParameter = (purchese_ID.HasValue ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_PurchasesDetails", objectParameter);
	}

	public virtual int Insert_Order_Restaurant_A(int? branchID, int? supplierID, int? accountID, bool? isWaiting, string supplierName, string supplierPhone, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, bool? iSPasue, bool? iSPayment, bool? iSDelivery, int? deliveryStatus, int? deliveryID, DateTime? time1)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		ObjectParameter objectParameter4 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter5 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter7 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter8 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter9 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter10 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter11 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter12 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter13 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter14 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter15 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter16 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter17 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter18 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter19 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter20 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter21 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter22 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter23 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter24 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter25 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter26 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter27 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter28 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter29 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter30 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter31 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter32 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter33 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter34 = (iSPasue.HasValue ? new ObjectParameter("ISPasue", iSPasue) : new ObjectParameter("ISPasue", typeof(bool)));
		ObjectParameter objectParameter35 = (iSPayment.HasValue ? new ObjectParameter("ISPayment", iSPayment) : new ObjectParameter("ISPayment", typeof(bool)));
		ObjectParameter objectParameter36 = (iSDelivery.HasValue ? new ObjectParameter("ISDelivery", iSDelivery) : new ObjectParameter("ISDelivery", typeof(bool)));
		ObjectParameter objectParameter37 = (deliveryStatus.HasValue ? new ObjectParameter("DeliveryStatus", deliveryStatus) : new ObjectParameter("DeliveryStatus", typeof(int)));
		ObjectParameter objectParameter38 = (deliveryID.HasValue ? new ObjectParameter("DeliveryID", deliveryID) : new ObjectParameter("DeliveryID", typeof(int)));
		ObjectParameter objectParameter39 = (time1.HasValue ? new ObjectParameter("Time1", time1) : new ObjectParameter("Time1", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Restaurant_A", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39);
	}

	public virtual int Insert_Order_Restaurant_Delivery(int? deliveryID, string custName, string custPhone, string custPlace, decimal? mONEY, int? orderID, int? deliveryStatus, int? branchID)
	{
		ObjectParameter objectParameter = (deliveryID.HasValue ? new ObjectParameter("DeliveryID", deliveryID) : new ObjectParameter("DeliveryID", typeof(int)));
		ObjectParameter objectParameter2 = ((custName != null) ? new ObjectParameter("CustName", custName) : new ObjectParameter("CustName", typeof(string)));
		ObjectParameter objectParameter3 = ((custPhone != null) ? new ObjectParameter("CustPhone", custPhone) : new ObjectParameter("CustPhone", typeof(string)));
		ObjectParameter objectParameter4 = ((custPlace != null) ? new ObjectParameter("CustPlace", custPlace) : new ObjectParameter("CustPlace", typeof(string)));
		ObjectParameter objectParameter5 = (mONEY.HasValue ? new ObjectParameter("MONEY", mONEY) : new ObjectParameter("MONEY", typeof(decimal)));
		ObjectParameter objectParameter6 = (orderID.HasValue ? new ObjectParameter("OrderID", orderID) : new ObjectParameter("OrderID", typeof(int)));
		ObjectParameter objectParameter7 = (deliveryStatus.HasValue ? new ObjectParameter("DeliveryStatus", deliveryStatus) : new ObjectParameter("DeliveryStatus", typeof(int)));
		ObjectParameter objectParameter8 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Restaurant_Delivery", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8);
	}

	public virtual int Insert_Order_Restaurant_TakeAway(int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? hasmPer, decimal? hasmAmount, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, bool? sectoral, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter3 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter4 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter6 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter7 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter8 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter9 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter10 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter11 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter12 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter13 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter14 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter15 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter16 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter17 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter18 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter19 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter20 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter21 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter22 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter23 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter24 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter25 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter26 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter27 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter29 = (hasmPer.HasValue ? new ObjectParameter("HasmPer", hasmPer) : new ObjectParameter("HasmPer", typeof(decimal)));
		ObjectParameter objectParameter30 = (hasmAmount.HasValue ? new ObjectParameter("HasmAmount", hasmAmount) : new ObjectParameter("HasmAmount", typeof(decimal)));
		ObjectParameter objectParameter31 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter33 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter35 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter37 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter38 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter39 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter40 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter41 = (sectoral.HasValue ? new ObjectParameter("Sectoral", sectoral) : new ObjectParameter("Sectoral", typeof(bool)));
		ObjectParameter objectParameter42 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter43 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter44 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter45 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter46 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter47 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Order_Restaurant_TakeAway", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46, objectParameter47);
	}

	public virtual int Insert_Tran_Tran(int? referenceCode, int? tranTypeID, string docCode, DateTime? tranDate, string note, int? branchID, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, int? projectId, ObjectParameter transn)
	{
		ObjectParameter objectParameter = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter2 = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter3 = ((docCode != null) ? new ObjectParameter("DocCode", docCode) : new ObjectParameter("DocCode", typeof(string)));
		ObjectParameter objectParameter4 = (tranDate.HasValue ? new ObjectParameter("TranDate", tranDate) : new ObjectParameter("TranDate", typeof(DateTime)));
		ObjectParameter objectParameter5 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter8 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter9 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter10 = (projectId.HasValue ? new ObjectParameter("ProjectId", projectId) : new ObjectParameter("ProjectId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Tran_Tran", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, transn);
	}

	public virtual int INSERT_Tran_TranDetails(int? tranSn, int? account_Sn, int? accounIindex, string tranDesc, decimal? debit, decimal? credit, int? costCentersID, int? branchID)
	{
		ObjectParameter objectParameter = (tranSn.HasValue ? new ObjectParameter("TranSn", tranSn) : new ObjectParameter("TranSn", typeof(int)));
		ObjectParameter objectParameter2 = (account_Sn.HasValue ? new ObjectParameter("Account_Sn", account_Sn) : new ObjectParameter("Account_Sn", typeof(int)));
		ObjectParameter objectParameter3 = (accounIindex.HasValue ? new ObjectParameter("AccounIindex", accounIindex) : new ObjectParameter("AccounIindex", typeof(int)));
		ObjectParameter objectParameter4 = ((tranDesc != null) ? new ObjectParameter("TranDesc", tranDesc) : new ObjectParameter("TranDesc", typeof(string)));
		ObjectParameter objectParameter5 = (debit.HasValue ? new ObjectParameter("Debit", debit) : new ObjectParameter("Debit", typeof(decimal)));
		ObjectParameter objectParameter6 = (credit.HasValue ? new ObjectParameter("Credit", credit) : new ObjectParameter("Credit", typeof(decimal)));
		ObjectParameter objectParameter7 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter8 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERT_Tran_TranDetails", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8);
	}

	public virtual int InsertDataExcelToSql()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertDataExcelToSql");
	}

	public virtual ObjectResult<InvoiceTax_Result> InvoiceTax(DateTime? dateFrom, DateTime? dateTo)
	{
		ObjectParameter objectParameter = (dateFrom.HasValue ? new ObjectParameter("DateFrom", dateFrom) : new ObjectParameter("DateFrom", typeof(DateTime)));
		ObjectParameter objectParameter2 = (dateTo.HasValue ? new ObjectParameter("DateTo", dateTo) : new ObjectParameter("DateTo", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InvoiceTax_Result>("InvoiceTax", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int ItemComponentQuantity_IN(int? itemID, int? storeID, decimal? quantity)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (quantity.HasValue ? new ObjectParameter("Quantity", quantity) : new ObjectParameter("Quantity", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ItemComponentQuantity_IN", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int ItemComponentQuantity_Out(int? itemID, int? storeID, decimal? quantity)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (quantity.HasValue ? new ObjectParameter("Quantity", quantity) : new ObjectParameter("Quantity", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ItemComponentQuantity_Out", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int ItemQuantity_IN(int? itemID, int? storeID, decimal? quantity, decimal? unitNumber, string itemUnitType)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (quantity.HasValue ? new ObjectParameter("Quantity", quantity) : new ObjectParameter("Quantity", typeof(decimal)));
		ObjectParameter objectParameter4 = (unitNumber.HasValue ? new ObjectParameter("UnitNumber", unitNumber) : new ObjectParameter("UnitNumber", typeof(decimal)));
		ObjectParameter objectParameter5 = ((itemUnitType != null) ? new ObjectParameter("ItemUnitType", itemUnitType) : new ObjectParameter("ItemUnitType", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ItemQuantity_IN", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5);
	}

	public virtual int ItemQuantity_Out(int? itemID, int? storeID, decimal? quantity, decimal? unitNumber, string itemUnitType)
	{
		ObjectParameter objectParameter = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (quantity.HasValue ? new ObjectParameter("Quantity", quantity) : new ObjectParameter("Quantity", typeof(decimal)));
		ObjectParameter objectParameter4 = (unitNumber.HasValue ? new ObjectParameter("UnitNumber", unitNumber) : new ObjectParameter("UnitNumber", typeof(decimal)));
		ObjectParameter objectParameter5 = ((itemUnitType != null) ? new ObjectParameter("ItemUnitType", itemUnitType) : new ObjectParameter("ItemUnitType", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ItemQuantity_Out", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5);
	}

	public virtual int Ledger_Account(int? accountID, DateTime? date1, DateTime? date2, bool? typeID)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter4 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Ledger_Account", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Ledger_Account_ByGroups(int? accountID, ObjectParameter credit, ObjectParameter debit, ObjectParameter total)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Ledger_Account_ByGroups", objectParameter, credit, debit, total);
	}

	public virtual int Ledger_Account_ByItems(int? accountID, DateTime? date1, DateTime? date2, bool? typeID)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter4 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Ledger_Account_ByItems", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Ledger_Account_Car(int? accountID, DateTime? date1, DateTime? date2, bool? typeID)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter4 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Ledger_Account_Car", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Ledger_AccountDetails(int? accountID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (accountID.HasValue ? new ObjectParameter("AccountID", accountID) : new ObjectParameter("AccountID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Ledger_AccountDetails", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual ObjectResult<Print_Account_Cuss_Result> Print_Account_Cuss()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_Cuss_Result>("Print_Account_Cuss", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Print_Account_CussByTotal_Result> Print_Account_CussByTotal()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_CussByTotal_Result>("Print_Account_CussByTotal", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Print_Account_DailyRestrictions_ALL_Result> Print_Account_DailyRestrictions_ALL(int? referenceCode, int? tranTypeID, int? branchID)
	{
		ObjectParameter objectParameter = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter2 = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_DailyRestrictions_ALL_Result>("Print_Account_DailyRestrictions_ALL", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Account_Receipts_Result> Print_Account_Receipts(int? tranTypeID, int? referenceCode, int? branchID)
	{
		ObjectParameter objectParameter = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter2 = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_Receipts_Result>("Print_Account_Receipts", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Account_Receipts2_Result> Print_Account_Receipts2(int? tranTypeID, int? referenceCode, int? branchID)
	{
		ObjectParameter objectParameter = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter2 = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_Receipts2_Result>("Print_Account_Receipts2", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Account_Supp_Result> Print_Account_Supp()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Account_Supp_Result>("Print_Account_Supp", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Print_Car_Result> Print_Car(int? id, bool? isDone)
	{
		ObjectParameter objectParameter = (id.HasValue ? new ObjectParameter("Id", id) : new ObjectParameter("Id", typeof(int)));
		ObjectParameter objectParameter2 = (isDone.HasValue ? new ObjectParameter("IsDone", isDone) : new ObjectParameter("IsDone", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Car_Result>("Print_Car", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Contract_Contract_Result> Print_Contract_Contract(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Contract_Contract_Result>("Print_Contract_Contract", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int Print_Emp_Payroll(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Print_Emp_Payroll", objectParameter);
	}

	public virtual ObjectResult<Print_Emp_Payroll_All_Result> Print_Emp_Payroll_All(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Emp_Payroll_All_Result>("Print_Emp_Payroll_All", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_Emp_PayrollTemp_Result> Print_Emp_PayrollTemp(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Emp_PayrollTemp_Result>("Print_Emp_PayrollTemp", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_EntrustedEmployee_Result> Print_EntrustedEmployee(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_EntrustedEmployee_Result>("Print_EntrustedEmployee", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_Cashier_Result> Print_Order_Cashier(int? iD, int? printerID, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (printerID.HasValue ? new ObjectParameter("PrinterID", printerID) : new ObjectParameter("PrinterID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Cashier_Result>("Print_Order_Cashier", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_CashierByItem_Result> Print_Order_CashierByItem(int? iD, int? printerID, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (printerID.HasValue ? new ObjectParameter("PrinterID", printerID) : new ObjectParameter("PrinterID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_CashierByItem_Result>("Print_Order_CashierByItem", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_CashierByItemCook_Result> Print_Order_CashierByItemCook(int? iD, int? printerID, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (printerID.HasValue ? new ObjectParameter("PrinterID", printerID) : new ObjectParameter("PrinterID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_CashierByItemCook_Result>("Print_Order_CashierByItemCook", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_CashierNoPrint_Result> Print_Order_CashierNoPrint(int? iD, int? printerID, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (printerID.HasValue ? new ObjectParameter("PrinterID", printerID) : new ObjectParameter("PrinterID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_CashierNoPrint_Result>("Print_Order_CashierNoPrint", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_CashierRes_Result> Print_Order_CashierRes(int? iD, int? printerID, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (printerID.HasValue ? new ObjectParameter("PrinterID", printerID) : new ObjectParameter("PrinterID", typeof(int)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_CashierRes_Result>("Print_Order_CashierRes", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_CheckoutOrders_Result> Print_Order_CheckoutOrders(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_CheckoutOrders_Result>("Print_Order_CheckoutOrders", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_InventorySettlementMinus_Result> Print_Order_InventorySettlementMinus(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_InventorySettlementMinus_Result>("Print_Order_InventorySettlementMinus", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_ManufacturingOrder_Result> Print_Order_ManufacturingOrder(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_ManufacturingOrder_Result>("Print_Order_ManufacturingOrder", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_OpenQuantity_Result> Print_Order_OpenQuantity(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_OpenQuantity_Result>("Print_Order_OpenQuantity", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int Print_Order_OrderDalala_ISPrint(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Print_Order_OrderDalala_ISPrint", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<Print_Order_OrderReturn_Result> Print_Order_OrderReturn(int? iD, int? branchID, int? creditNote)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (creditNote.HasValue ? new ObjectParameter("CreditNote", creditNote) : new ObjectParameter("CreditNote", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_OrderReturn_Result>("Print_Order_OrderReturn", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Print_Order_Orders_Result> Print_Order_Orders(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Orders_Result>("Print_Order_Orders", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_Orders_Res_Result> Print_Order_Orders_Res(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Orders_Res_Result>("Print_Order_Orders_Res", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int Print_Order_OrdersAfterReturn(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Print_Order_OrdersAfterReturn", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<Print_Order_OrdersDraft_Result> Print_Order_OrdersDraft(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_OrdersDraft_Result>("Print_Order_OrdersDraft", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_PaymentItem_Result> Print_Order_PaymentItem(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_PaymentItem_Result>("Print_Order_PaymentItem", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_PriceOffer_Result> Print_Order_PriceOffer(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_PriceOffer_Result>("Print_Order_PriceOffer", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_Purchases_Result> Print_Order_Purchases(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Purchases_Result>("Print_Order_Purchases", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_PurchasesOrder_Result> Print_Order_PurchasesOrder(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_PurchasesOrder_Result>("Print_Order_PurchasesOrder", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_PurchasesReturn_Result> Print_Order_PurchasesReturn(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_PurchasesReturn_Result>("Print_Order_PurchasesReturn", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_RecItem_Result> Print_Order_RecItem(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_RecItem_Result>("Print_Order_RecItem", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_Rent_Result> Print_Order_Rent(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Rent_Result>("Print_Order_Rent", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_Rent_Recipt_Result> Print_Order_Rent_Recipt(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_Rent_Recipt_Result>("Print_Order_Rent_Recipt", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_StoreTransfer_Result> Print_Order_StoreTransfer(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_StoreTransfer_Result>("Print_Order_StoreTransfer", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_TransferFromBranch_Result> Print_Order_TransferFromBranch(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_TransferFromBranch_Result>("Print_Order_TransferFromBranch", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Order_TransferToBranch_Result> Print_Order_TransferToBranch(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Order_TransferToBranch_Result>("Print_Order_TransferToBranch", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_OrderDalala_Dalala_Result> Print_OrderDalala_Dalala(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_OrderDalala_Dalala_Result>("Print_OrderDalala_Dalala", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_PriceShowRentalInvoice_Result> Print_PriceShowRentalInvoice(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_PriceShowRentalInvoice_Result>("Print_PriceShowRentalInvoice", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Recipt3_Result> Print_Recipt3(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Recipt3_Result>("Print_Recipt3", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_RentalExpenses_Result> Print_RentalExpenses(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_RentalExpenses_Result>("Print_RentalExpenses", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_RentalInvoice_Result> Print_RentalInvoice(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_RentalInvoice_Result>("Print_RentalInvoice", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Scaffold_EntryPermission_Result> Print_Scaffold_EntryPermission(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffold_EntryPermission_Result>("Print_Scaffold_EntryPermission", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_Scaffold_ExitPermission_Result> Print_Scaffold_ExitPermission(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffold_ExitPermission_Result>("Print_Scaffold_ExitPermission", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_Scaffold_PriceOffer_Result> Print_Scaffold_PriceOffer(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffold_PriceOffer_Result>("Print_Scaffold_PriceOffer", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Scaffolds_Contract_Result> Print_Scaffolds_Contract(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffolds_Contract_Result>("Print_Scaffolds_Contract", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_Scaffolds_MinutesEnd_Result> Print_Scaffolds_MinutesEnd(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffolds_MinutesEnd_Result>("Print_Scaffolds_MinutesEnd", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_Scaffolds_MinutesStarted_Result> Print_Scaffolds_MinutesStarted(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_Scaffolds_MinutesStarted_Result>("Print_Scaffolds_MinutesStarted", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Print_SearchAccountTranDetailsByCode_Result> Print_SearchAccountTranDetailsByCode(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_SearchAccountTranDetailsByCode_Result>("Print_SearchAccountTranDetailsByCode", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Print_SearchAccountTranTempDetailsByCode_Result> Print_SearchAccountTranTempDetailsByCode(int? iD, int? branchID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Print_SearchAccountTranTempDetailsByCode_Result>("Print_SearchAccountTranTempDetailsByCode", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int Processing_OF_Profits()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Processing_OF_Profits");
	}

	public virtual int RebuildCGEAllIndexes(string microDataBase)
	{
		ObjectParameter objectParameter = ((microDataBase != null) ? new ObjectParameter("MicroDataBase", microDataBase) : new ObjectParameter("MicroDataBase", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RebuildCGEAllIndexes", objectParameter);
	}

	public virtual int RepairDB()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RepairDB");
	}

	public virtual int Report_AllOrdersCheckOut(int? branchID, DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter2 = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter3 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Report_AllOrdersCheckOut", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual ObjectResult<Report_CloseDayByUser_Result> Report_CloseDayByUser(int? userID)
	{
		ObjectParameter objectParameter = (userID.HasValue ? new ObjectParameter("UserID", userID) : new ObjectParameter("UserID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Report_CloseDayByUser_Result>("Report_CloseDayByUser", new ObjectParameter[1] { objectParameter });
	}

	public virtual int Report_DailyOrderByDate(DateTime? date1, DateTime? date2, int? branchID)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		ObjectParameter objectParameter3 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Report_DailyOrderByDate", objectParameter, objectParameter2, objectParameter3);
	}

	public virtual int ReportMizanCustSupp(DateTime? date1, DateTime? date2)
	{
		ObjectParameter objectParameter = (date1.HasValue ? new ObjectParameter("Date1", date1) : new ObjectParameter("Date1", typeof(DateTime)));
		ObjectParameter objectParameter2 = (date2.HasValue ? new ObjectParameter("Date2", date2) : new ObjectParameter("Date2", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ReportMizanCustSupp", objectParameter, objectParameter2);
	}

	public virtual ObjectResult<string> SearchNameCus_Sup(string name)
	{
		ObjectParameter objectParameter = ((name != null) ? new ObjectParameter("Name", name) : new ObjectParameter("Name", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SearchNameCus_Sup", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Account_CusTailor_Result> Select_Account_CusTailor(int? custCode)
	{
		ObjectParameter objectParameter = (custCode.HasValue ? new ObjectParameter("CustCode", custCode) : new ObjectParameter("CustCode", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Account_CusTailor_Result>("Select_Account_CusTailor", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_AccountCustomer_Result> Select_AccountCustomer(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_AccountCustomer_Result>("Select_AccountCustomer", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_AccountDefualt_Result> Select_AccountDefualt(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_AccountDefualt_Result>("Select_AccountDefualt", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_BarCode_Result> Select_BarCode(string itemCode)
	{
		ObjectParameter objectParameter = ((itemCode != null) ? new ObjectParameter("ItemCode", itemCode) : new ObjectParameter("ItemCode", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_BarCode_Result>("Select_BarCode", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_BarCodeExpireOpenQuantity_Result> Select_BarCodeExpireOpenQuantity(string purchese_ID, int? branchID, int? itemID)
	{
		ObjectParameter objectParameter = ((purchese_ID != null) ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(string)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_BarCodeExpireOpenQuantity_Result>("Select_BarCodeExpireOpenQuantity", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Select_BarCodePurchase_Result> Select_BarCodePurchase(string purchese_ID, int? branchID, int? itemID)
	{
		ObjectParameter objectParameter = ((purchese_ID != null) ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(string)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (itemID.HasValue ? new ObjectParameter("ItemID", itemID) : new ObjectParameter("ItemID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_BarCodePurchase_Result>("Select_BarCodePurchase", new ObjectParameter[3] { objectParameter, objectParameter2, objectParameter3 });
	}

	public virtual ObjectResult<Select_Car_Receive_Result> Select_Car_Receive(bool? status)
	{
		ObjectParameter objectParameter = (status.HasValue ? new ObjectParameter("Status", status) : new ObjectParameter("Status", typeof(bool)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Car_Receive_Result>("Select_Car_Receive", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Contract_Contract_Result> Select_Contract_Contract(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Contract_Contract_Result>("Select_Contract_Contract", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<SELECT_Contract_EJAR_Result> SELECT_Contract_EJAR(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_Contract_EJAR_Result>("SELECT_Contract_EJAR", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<SELECT_Contract_EJAR_Search_Result> SELECT_Contract_EJAR_Search()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SELECT_Contract_EJAR_Search_Result>("SELECT_Contract_EJAR_Search", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Select_Contract_Guarantee_Result> Select_Contract_Guarantee(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Contract_Guarantee_Result>("Select_Contract_Guarantee", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_ItemQuantity_Result> Select_ItemQuantity(int? iD, int? storeID, int? typeID, int? groupID, int? compID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (compID.HasValue ? new ObjectParameter("CompID", compID) : new ObjectParameter("CompID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_ItemQuantity_Result>("Select_ItemQuantity", new ObjectParameter[5] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5 });
	}

	public virtual ObjectResult<Select_ItemQuantity_ByProperties_Result> Select_ItemQuantity_ByProperties(int? iD, int? storeID, int? typeID, int? groupID, int? compID, int? classID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		ObjectParameter objectParameter3 = (typeID.HasValue ? new ObjectParameter("TypeID", typeID) : new ObjectParameter("TypeID", typeof(int)));
		ObjectParameter objectParameter4 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter5 = (compID.HasValue ? new ObjectParameter("CompID", compID) : new ObjectParameter("CompID", typeof(int)));
		ObjectParameter objectParameter6 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_ItemQuantity_ByProperties_Result>("Select_ItemQuantity_ByProperties", new ObjectParameter[6] { objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6 });
	}

	public virtual ObjectResult<Select_ItemQuantity_Only_Result> Select_ItemQuantity_Only(int? iD, int? storeID)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (storeID.HasValue ? new ObjectParameter("StoreID", storeID) : new ObjectParameter("StoreID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_ItemQuantity_Only_Result>("Select_ItemQuantity_Only", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_Order_CheckoutOrders_Result> Select_Order_CheckoutOrders(bool? iSNum, int? branchID)
	{
		ObjectParameter objectParameter = (iSNum.HasValue ? new ObjectParameter("ISNum", iSNum) : new ObjectParameter("ISNum", typeof(bool)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_CheckoutOrders_Result>("Select_Order_CheckoutOrders", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_Order_InventorySettlementMinus_Result> Select_Order_InventorySettlementMinus(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_InventorySettlementMinus_Result>("Select_Order_InventorySettlementMinus", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_Manufacturing_Result> Select_Order_Manufacturing(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_Manufacturing_Result>("Select_Order_Manufacturing", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_ManufacturingOrder_Result> Select_Order_ManufacturingOrder(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_ManufacturingOrder_Result>("Select_Order_ManufacturingOrder", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_OpenQuantity_Result> Select_Order_OpenQuantity(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_OpenQuantity_Result>("Select_Order_OpenQuantity", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_OrderReturn_Result> Select_Order_OrderReturn(bool? iSNum, int? branchID)
	{
		ObjectParameter objectParameter = (iSNum.HasValue ? new ObjectParameter("ISNum", iSNum) : new ObjectParameter("ISNum", typeof(bool)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_OrderReturn_Result>("Select_Order_OrderReturn", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_Order_Orders_Result> Select_Order_Orders(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_Orders_Result>("Select_Order_Orders", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_OrdersDraft_Result> Select_Order_OrdersDraft(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_OrdersDraft_Result>("Select_Order_OrdersDraft", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_PaymentItem_Result> Select_Order_PaymentItem(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_PaymentItem_Result>("Select_Order_PaymentItem", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_PriceOffer_Result> Select_Order_PriceOffer(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_PriceOffer_Result>("Select_Order_PriceOffer", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_Purchases_Result> Select_Order_Purchases(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_Purchases_Result>("Select_Order_Purchases", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_PurchasesOrder_Result> Select_Order_PurchasesOrder(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_PurchasesOrder_Result>("Select_Order_PurchasesOrder", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_PurchasesReturn_Result> Select_Order_PurchasesReturn(bool? iSNum, int? branchID)
	{
		ObjectParameter objectParameter = (iSNum.HasValue ? new ObjectParameter("ISNum", iSNum) : new ObjectParameter("ISNum", typeof(bool)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_PurchasesReturn_Result>("Select_Order_PurchasesReturn", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_Order_RecItem_Result> Select_Order_RecItem(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_RecItem_Result>("Select_Order_RecItem", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_Reservation_Result> Select_Order_Reservation(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_Reservation_Result>("Select_Order_Reservation", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_StoreTransfer_Result> Select_Order_StoreTransfer(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_StoreTransfer_Result>("Select_Order_StoreTransfer", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_Transfer_Master_Result> Select_Order_Transfer_Master(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_Transfer_Master_Result>("Select_Order_Transfer_Master", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_TransferFromBranch_Result> Select_Order_TransferFromBranch(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_TransferFromBranch_Result>("Select_Order_TransferFromBranch", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Order_TransferToBranch_Result> Select_Order_TransferToBranch(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Order_TransferToBranch_Result>("Select_Order_TransferToBranch", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_OrderDalala_Dalala_Result> Select_OrderDalala_Dalala(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_OrderDalala_Dalala_Result>("Select_OrderDalala_Dalala", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_OrderDalala_Sending_Result> Select_OrderDalala_Sending(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_OrderDalala_Sending_Result>("Select_OrderDalala_Sending", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_OrderGard_Result> Select_OrderGard(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_OrderGard_Result>("Select_OrderGard", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_OrderRent_Rent_Result> Select_OrderRent_Rent(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_OrderRent_Rent_Result>("Select_OrderRent_Rent", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Remember_Result> Select_Remember()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Remember_Result>("Select_Remember", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Select_Scaffold_EntryPermission_Result> Select_Scaffold_EntryPermission(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffold_EntryPermission_Result>("Select_Scaffold_EntryPermission", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Scaffold_ExitPermission_Result> Select_Scaffold_ExitPermission(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffold_ExitPermission_Result>("Select_Scaffold_ExitPermission", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_scaffold_PriceOffer_Result> Select_scaffold_PriceOffer(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_scaffold_PriceOffer_Result>("Select_scaffold_PriceOffer", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Scaffolds_Contract_Result> Select_Scaffolds_Contract(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffolds_Contract_Result>("Select_Scaffolds_Contract", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Scaffolds_ContractNotMinutesStarted_Result> Select_Scaffolds_ContractNotMinutesStarted(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffolds_ContractNotMinutesStarted_Result>("Select_Scaffolds_ContractNotMinutesStarted", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Scaffolds_MinutesEnd_Result> Select_Scaffolds_MinutesEnd(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffolds_MinutesEnd_Result>("Select_Scaffolds_MinutesEnd", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Scaffolds_MinutesStarted_Result> Select_Scaffolds_MinutesStarted(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Scaffolds_MinutesStarted_Result>("Select_Scaffolds_MinutesStarted", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Search_Account_Receipts_Result> Select_Search_Account_Receipts(int? tranTypeID, int? branchID)
	{
		ObjectParameter objectParameter = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Search_Account_Receipts_Result>("Select_Search_Account_Receipts", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_SearchAccount_Result> Select_SearchAccount()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccount_Result>("Select_SearchAccount", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Select_SearchAccount_ByAccountNumber_Result> Select_SearchAccount_ByAccountNumber(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccount_ByAccountNumber_Result>("Select_SearchAccount_ByAccountNumber", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountCostCenter_Result> Select_SearchAccountCostCenter(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountCostCenter_Result>("Select_SearchAccountCostCenter", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountCostCenterAll_Result> Select_SearchAccountCostCenterAll(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountCostCenterAll_Result>("Select_SearchAccountCostCenterAll", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountCostCenterByProject_Result> Select_SearchAccountCostCenterByProject(int? projectId, int? branchID)
	{
		ObjectParameter objectParameter = (projectId.HasValue ? new ObjectParameter("ProjectId", projectId) : new ObjectParameter("ProjectId", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountCostCenterByProject_Result>("Select_SearchAccountCostCenterByProject", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual ObjectResult<Select_SearchAccountProjects_Result> Select_SearchAccountProjects(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountProjects_Result>("Select_SearchAccountProjects", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountReceipt_Result> Select_SearchAccountReceipt(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountReceipt_Result>("Select_SearchAccountReceipt", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountReceiptForAccountTree_Result> Select_SearchAccountReceiptForAccountTree()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountReceiptForAccountTree_Result>("Select_SearchAccountReceiptForAccountTree", Array.Empty<ObjectParameter>());
	}

	public virtual ObjectResult<Select_SearchAccountTran_Result> Select_SearchAccountTran(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountTran_Result>("Select_SearchAccountTran", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountTranDetailsByCode_Result> Select_SearchAccountTranDetailsByCode(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountTranDetailsByCode_Result>("Select_SearchAccountTranDetailsByCode", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountTranTemp_Result> Select_SearchAccountTranTemp(int? branchID)
	{
		ObjectParameter objectParameter = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountTranTemp_Result>("Select_SearchAccountTranTemp", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_SearchAccountTranTempDetailsByCode_Result> Select_SearchAccountTranTempDetailsByCode(int? iD)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_SearchAccountTranTempDetailsByCode_Result>("Select_SearchAccountTranTempDetailsByCode", new ObjectParameter[1] { objectParameter });
	}

	public virtual ObjectResult<Select_Virgins_Result> Select_Virgins(int? custID, int? sN)
	{
		ObjectParameter objectParameter = (custID.HasValue ? new ObjectParameter("CustID", custID) : new ObjectParameter("CustID", typeof(int)));
		ObjectParameter objectParameter2 = (sN.HasValue ? new ObjectParameter("SN", sN) : new ObjectParameter("SN", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Virgins_Result>("Select_Virgins", new ObjectParameter[2] { objectParameter, objectParameter2 });
	}

	public virtual int SetActivate()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SetActivate");
	}

	public virtual int UPDATE_ItemProfit(decimal? perc, int? groupID, int? companyID, int? classID)
	{
		ObjectParameter objectParameter = (perc.HasValue ? new ObjectParameter("Perc", perc) : new ObjectParameter("Perc", typeof(decimal)));
		ObjectParameter objectParameter2 = (groupID.HasValue ? new ObjectParameter("GroupID", groupID) : new ObjectParameter("GroupID", typeof(int)));
		ObjectParameter objectParameter3 = (companyID.HasValue ? new ObjectParameter("CompanyID", companyID) : new ObjectParameter("CompanyID", typeof(int)));
		ObjectParameter objectParameter4 = (classID.HasValue ? new ObjectParameter("ClassID", classID) : new ObjectParameter("ClassID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UPDATE_ItemProfit", objectParameter, objectParameter2, objectParameter3, objectParameter4);
	}

	public virtual int Update_OpenQuantity(int? purBranchID, int? branchID, DateTime? purchases_Date, string noteNum, decimal? totalPrices, int? userID_Add, int? userBranch_Add, string userMacAddress_Add)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter4 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter5 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter6 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter7 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter8 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_OpenQuantity", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8);
	}

	public virtual int Update_Order_Order_ALL(int? purBranchID, int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? projectID, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge, decimal? allDiscount, decimal? bounsAmount, string carName, string carModel, string plateNumber, string chassisNum, string carColor, string counter)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter4 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter5 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter7 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter8 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter9 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter10 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter11 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter12 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter13 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter14 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter15 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter16 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter17 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter18 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter19 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter20 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter21 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter22 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter23 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter24 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter25 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter26 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter27 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter29 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter30 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter31 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter33 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter35 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter37 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter38 = (projectID.HasValue ? new ObjectParameter("ProjectID", projectID) : new ObjectParameter("ProjectID", typeof(int)));
		ObjectParameter objectParameter39 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter40 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter41 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter42 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter43 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter44 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter45 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter46 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter47 = (allDiscount.HasValue ? new ObjectParameter("AllDiscount", allDiscount) : new ObjectParameter("AllDiscount", typeof(decimal)));
		ObjectParameter objectParameter48 = (bounsAmount.HasValue ? new ObjectParameter("BounsAmount", bounsAmount) : new ObjectParameter("BounsAmount", typeof(decimal)));
		ObjectParameter objectParameter49 = ((carName != null) ? new ObjectParameter("CarName", carName) : new ObjectParameter("CarName", typeof(string)));
		ObjectParameter objectParameter50 = ((carModel != null) ? new ObjectParameter("CarModel", carModel) : new ObjectParameter("CarModel", typeof(string)));
		ObjectParameter objectParameter51 = ((plateNumber != null) ? new ObjectParameter("PlateNumber", plateNumber) : new ObjectParameter("PlateNumber", typeof(string)));
		ObjectParameter objectParameter52 = ((chassisNum != null) ? new ObjectParameter("ChassisNum", chassisNum) : new ObjectParameter("ChassisNum", typeof(string)));
		ObjectParameter objectParameter53 = ((carColor != null) ? new ObjectParameter("CarColor", carColor) : new ObjectParameter("CarColor", typeof(string)));
		ObjectParameter objectParameter54 = ((counter != null) ? new ObjectParameter("Counter", counter) : new ObjectParameter("Counter", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Order_Order_ALL", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46, objectParameter47, objectParameter48, objectParameter49, objectParameter50, objectParameter51, objectParameter52, objectParameter53, objectParameter54);
	}

	public virtual int Update_Order_Order_ALL_Draft(int? purBranchID, int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string buildingNum, string street, string district, string city, string country, string postalCode, string additionalNum, string commercialRecord, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge, decimal? allDiscount, decimal? bounsAmount, string carName, string carModel, string plateNumber, string chassisNum, string carColor, string counter)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter4 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter5 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter7 = ((buildingNum != null) ? new ObjectParameter("BuildingNum", buildingNum) : new ObjectParameter("BuildingNum", typeof(string)));
		ObjectParameter objectParameter8 = ((street != null) ? new ObjectParameter("Street", street) : new ObjectParameter("Street", typeof(string)));
		ObjectParameter objectParameter9 = ((district != null) ? new ObjectParameter("District", district) : new ObjectParameter("District", typeof(string)));
		ObjectParameter objectParameter10 = ((city != null) ? new ObjectParameter("City", city) : new ObjectParameter("City", typeof(string)));
		ObjectParameter objectParameter11 = ((country != null) ? new ObjectParameter("Country", country) : new ObjectParameter("Country", typeof(string)));
		ObjectParameter objectParameter12 = ((postalCode != null) ? new ObjectParameter("PostalCode", postalCode) : new ObjectParameter("PostalCode", typeof(string)));
		ObjectParameter objectParameter13 = ((additionalNum != null) ? new ObjectParameter("AdditionalNum", additionalNum) : new ObjectParameter("AdditionalNum", typeof(string)));
		ObjectParameter objectParameter14 = ((commercialRecord != null) ? new ObjectParameter("CommercialRecord", commercialRecord) : new ObjectParameter("CommercialRecord", typeof(string)));
		ObjectParameter objectParameter15 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter16 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter17 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter18 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter19 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter20 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter21 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter22 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter23 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter24 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter25 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter26 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter27 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter28 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter29 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter30 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter31 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter32 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter33 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter34 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter35 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter36 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter37 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter38 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter39 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter40 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter41 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter42 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter43 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter44 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter45 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter46 = (allDiscount.HasValue ? new ObjectParameter("AllDiscount", allDiscount) : new ObjectParameter("AllDiscount", typeof(decimal)));
		ObjectParameter objectParameter47 = (bounsAmount.HasValue ? new ObjectParameter("BounsAmount", bounsAmount) : new ObjectParameter("BounsAmount", typeof(decimal)));
		ObjectParameter objectParameter48 = ((carName != null) ? new ObjectParameter("CarName", carName) : new ObjectParameter("CarName", typeof(string)));
		ObjectParameter objectParameter49 = ((carModel != null) ? new ObjectParameter("CarModel", carModel) : new ObjectParameter("CarModel", typeof(string)));
		ObjectParameter objectParameter50 = ((plateNumber != null) ? new ObjectParameter("PlateNumber", plateNumber) : new ObjectParameter("PlateNumber", typeof(string)));
		ObjectParameter objectParameter51 = ((chassisNum != null) ? new ObjectParameter("ChassisNum", chassisNum) : new ObjectParameter("ChassisNum", typeof(string)));
		ObjectParameter objectParameter52 = ((carColor != null) ? new ObjectParameter("CarColor", carColor) : new ObjectParameter("CarColor", typeof(string)));
		ObjectParameter objectParameter53 = ((counter != null) ? new ObjectParameter("Counter", counter) : new ObjectParameter("Counter", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Order_Order_ALL_Draft", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39, objectParameter40, objectParameter41, objectParameter42, objectParameter43, objectParameter44, objectParameter45, objectParameter46, objectParameter47, objectParameter48, objectParameter49, objectParameter50, objectParameter51, objectParameter52, objectParameter53);
	}

	public virtual int Update_Order_Order_ALL2(int? purBranchID, int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, int? bounceID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? hasmPer, decimal? hasmAmount, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, DateTime? dateHold, decimal? charge)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter4 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter5 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter7 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter8 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter9 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter10 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter11 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter12 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter13 = (bounceID.HasValue ? new ObjectParameter("BounceID", bounceID) : new ObjectParameter("BounceID", typeof(int)));
		ObjectParameter objectParameter14 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter15 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter16 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter17 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter18 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter19 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter20 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter21 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter22 = (hasmPer.HasValue ? new ObjectParameter("HasmPer", hasmPer) : new ObjectParameter("HasmPer", typeof(decimal)));
		ObjectParameter objectParameter23 = (hasmAmount.HasValue ? new ObjectParameter("HasmAmount", hasmAmount) : new ObjectParameter("HasmAmount", typeof(decimal)));
		ObjectParameter objectParameter24 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter25 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter26 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter27 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter28 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter29 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter30 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter31 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter32 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter33 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter34 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter35 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter36 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter37 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter38 = (dateHold.HasValue ? new ObjectParameter("DateHold", dateHold) : new ObjectParameter("DateHold", typeof(DateTime)));
		ObjectParameter objectParameter39 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Order_Order_ALL2", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36, objectParameter37, objectParameter38, objectParameter39);
	}

	public virtual int Update_Order_Purchases(int? purBranchID, int? branchID, int? supplierID, string supplierName, string supplierPhone, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, int? costCentersID, string note, string noteNum, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? net, decimal? cash, decimal? bank, int? acc_Cash, int? acc_Bank, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, string salesMan, decimal? charge, int? projectId)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter4 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter5 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter7 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter8 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter9 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter10 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter11 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter12 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter13 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter14 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter15 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter16 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter17 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter18 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter19 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter20 = (cash.HasValue ? new ObjectParameter("Cash", cash) : new ObjectParameter("Cash", typeof(decimal)));
		ObjectParameter objectParameter21 = (bank.HasValue ? new ObjectParameter("Bank", bank) : new ObjectParameter("Bank", typeof(decimal)));
		ObjectParameter objectParameter22 = (acc_Cash.HasValue ? new ObjectParameter("Acc_Cash", acc_Cash) : new ObjectParameter("Acc_Cash", typeof(int)));
		ObjectParameter objectParameter23 = (acc_Bank.HasValue ? new ObjectParameter("Acc_Bank", acc_Bank) : new ObjectParameter("Acc_Bank", typeof(int)));
		ObjectParameter objectParameter24 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter25 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter26 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter27 = ((salesMan != null) ? new ObjectParameter("SalesMan", salesMan) : new ObjectParameter("SalesMan", typeof(string)));
		ObjectParameter objectParameter28 = (charge.HasValue ? new ObjectParameter("Charge", charge) : new ObjectParameter("Charge", typeof(decimal)));
		ObjectParameter objectParameter29 = (projectId.HasValue ? new ObjectParameter("ProjectId", projectId) : new ObjectParameter("ProjectId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Order_Purchases", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29);
	}

	public virtual int Update_Order_Restaurant_B(int? purBranchID, int? branchID, int? supplierID, bool? isWaiting, string supplierName, string supplierPhone, string supplierVatNum, DateTime? purchases_Date, int? order_Paymant_Type, string restaurant_PayType, int? restaurant_TypeID, int? costCentersID, string note, string noteNum, decimal? costOrder, decimal? tax, decimal? totalPrices, decimal? safy, decimal? discountNum, decimal? discountPerantage, decimal? tax_Discount, decimal? totalPrices_Discount, decimal? tobaccoTax, decimal? allTax, decimal? net, decimal? cashMoney, decimal? cashBank, bool? orderCashierType, int? roomNum, int? tableNum, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, bool? iSPasue, bool? iSPayment, DateTime? time1)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (supplierID.HasValue ? new ObjectParameter("SupplierID", supplierID) : new ObjectParameter("SupplierID", typeof(int)));
		ObjectParameter objectParameter4 = (isWaiting.HasValue ? new ObjectParameter("IsWaiting", isWaiting) : new ObjectParameter("IsWaiting", typeof(bool)));
		ObjectParameter objectParameter5 = ((supplierName != null) ? new ObjectParameter("SupplierName", supplierName) : new ObjectParameter("SupplierName", typeof(string)));
		ObjectParameter objectParameter6 = ((supplierPhone != null) ? new ObjectParameter("SupplierPhone", supplierPhone) : new ObjectParameter("SupplierPhone", typeof(string)));
		ObjectParameter objectParameter7 = ((supplierVatNum != null) ? new ObjectParameter("SupplierVatNum", supplierVatNum) : new ObjectParameter("SupplierVatNum", typeof(string)));
		ObjectParameter objectParameter8 = (purchases_Date.HasValue ? new ObjectParameter("Purchases_Date", purchases_Date) : new ObjectParameter("Purchases_Date", typeof(DateTime)));
		ObjectParameter objectParameter9 = (order_Paymant_Type.HasValue ? new ObjectParameter("Order_Paymant_Type", order_Paymant_Type) : new ObjectParameter("Order_Paymant_Type", typeof(int)));
		ObjectParameter objectParameter10 = ((restaurant_PayType != null) ? new ObjectParameter("Restaurant_PayType", restaurant_PayType) : new ObjectParameter("Restaurant_PayType", typeof(string)));
		ObjectParameter objectParameter11 = (restaurant_TypeID.HasValue ? new ObjectParameter("Restaurant_TypeID", restaurant_TypeID) : new ObjectParameter("Restaurant_TypeID", typeof(int)));
		ObjectParameter objectParameter12 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter13 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter14 = ((noteNum != null) ? new ObjectParameter("NoteNum", noteNum) : new ObjectParameter("NoteNum", typeof(string)));
		ObjectParameter objectParameter15 = (costOrder.HasValue ? new ObjectParameter("CostOrder", costOrder) : new ObjectParameter("CostOrder", typeof(decimal)));
		ObjectParameter objectParameter16 = (tax.HasValue ? new ObjectParameter("Tax", tax) : new ObjectParameter("Tax", typeof(decimal)));
		ObjectParameter objectParameter17 = (totalPrices.HasValue ? new ObjectParameter("TotalPrices", totalPrices) : new ObjectParameter("TotalPrices", typeof(decimal)));
		ObjectParameter objectParameter18 = (safy.HasValue ? new ObjectParameter("Safy", safy) : new ObjectParameter("Safy", typeof(decimal)));
		ObjectParameter objectParameter19 = (discountNum.HasValue ? new ObjectParameter("DiscountNum", discountNum) : new ObjectParameter("DiscountNum", typeof(decimal)));
		ObjectParameter objectParameter20 = (discountPerantage.HasValue ? new ObjectParameter("DiscountPerantage", discountPerantage) : new ObjectParameter("DiscountPerantage", typeof(decimal)));
		ObjectParameter objectParameter21 = (tax_Discount.HasValue ? new ObjectParameter("Tax_Discount", tax_Discount) : new ObjectParameter("Tax_Discount", typeof(decimal)));
		ObjectParameter objectParameter22 = (totalPrices_Discount.HasValue ? new ObjectParameter("TotalPrices_Discount", totalPrices_Discount) : new ObjectParameter("TotalPrices_Discount", typeof(decimal)));
		ObjectParameter objectParameter23 = (tobaccoTax.HasValue ? new ObjectParameter("TobaccoTax", tobaccoTax) : new ObjectParameter("TobaccoTax", typeof(decimal)));
		ObjectParameter objectParameter24 = (allTax.HasValue ? new ObjectParameter("AllTax", allTax) : new ObjectParameter("AllTax", typeof(decimal)));
		ObjectParameter objectParameter25 = (net.HasValue ? new ObjectParameter("Net", net) : new ObjectParameter("Net", typeof(decimal)));
		ObjectParameter objectParameter26 = (cashMoney.HasValue ? new ObjectParameter("CashMoney", cashMoney) : new ObjectParameter("CashMoney", typeof(decimal)));
		ObjectParameter objectParameter27 = (cashBank.HasValue ? new ObjectParameter("CashBank", cashBank) : new ObjectParameter("CashBank", typeof(decimal)));
		ObjectParameter objectParameter28 = (orderCashierType.HasValue ? new ObjectParameter("OrderCashierType", orderCashierType) : new ObjectParameter("OrderCashierType", typeof(bool)));
		ObjectParameter objectParameter29 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter30 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter31 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter32 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter33 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter34 = (iSPasue.HasValue ? new ObjectParameter("ISPasue", iSPasue) : new ObjectParameter("ISPasue", typeof(bool)));
		ObjectParameter objectParameter35 = (iSPayment.HasValue ? new ObjectParameter("ISPayment", iSPayment) : new ObjectParameter("ISPayment", typeof(bool)));
		ObjectParameter objectParameter36 = (time1.HasValue ? new ObjectParameter("Time1", time1) : new ObjectParameter("Time1", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Order_Restaurant_B", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, objectParameter11, objectParameter12, objectParameter13, objectParameter14, objectParameter15, objectParameter16, objectParameter17, objectParameter18, objectParameter19, objectParameter20, objectParameter21, objectParameter22, objectParameter23, objectParameter24, objectParameter25, objectParameter26, objectParameter27, objectParameter28, objectParameter29, objectParameter30, objectParameter31, objectParameter32, objectParameter33, objectParameter34, objectParameter35, objectParameter36);
	}

	public virtual int Update_Restaurant_Mahly(int? purBranchID, int? branchID, int? costCentersID, decimal? cash, decimal? bank, bool? isPaymant, int? roomNum, int? tableNum, int? userID_Add, string userMacAddress_Add)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter4 = (cash.HasValue ? new ObjectParameter("Cash", cash) : new ObjectParameter("Cash", typeof(decimal)));
		ObjectParameter objectParameter5 = (bank.HasValue ? new ObjectParameter("Bank", bank) : new ObjectParameter("Bank", typeof(decimal)));
		ObjectParameter objectParameter6 = (isPaymant.HasValue ? new ObjectParameter("IsPaymant", isPaymant) : new ObjectParameter("IsPaymant", typeof(bool)));
		ObjectParameter objectParameter7 = (roomNum.HasValue ? new ObjectParameter("RoomNum", roomNum) : new ObjectParameter("RoomNum", typeof(int)));
		ObjectParameter objectParameter8 = (tableNum.HasValue ? new ObjectParameter("TableNum", tableNum) : new ObjectParameter("TableNum", typeof(int)));
		ObjectParameter objectParameter9 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter10 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Restaurant_Mahly", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10);
	}

	public virtual int Update_Restaurant_Tawsel(int? purBranchID, int? branchID, int? costCentersID, decimal? cash, decimal? bank)
	{
		ObjectParameter objectParameter = (purBranchID.HasValue ? new ObjectParameter("PurBranchID", purBranchID) : new ObjectParameter("PurBranchID", typeof(int)));
		ObjectParameter objectParameter2 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter3 = (costCentersID.HasValue ? new ObjectParameter("CostCentersID", costCentersID) : new ObjectParameter("CostCentersID", typeof(int)));
		ObjectParameter objectParameter4 = (cash.HasValue ? new ObjectParameter("Cash", cash) : new ObjectParameter("Cash", typeof(decimal)));
		ObjectParameter objectParameter5 = (bank.HasValue ? new ObjectParameter("Bank", bank) : new ObjectParameter("Bank", typeof(decimal)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Restaurant_Tawsel", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5);
	}

	public virtual int Update_Room_A(int? iD, bool? iSPasue, decimal? money, int? orderID, int? branch, bool? iSPaymentType, DateTime? timeA)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (iSPasue.HasValue ? new ObjectParameter("ISPasue", iSPasue) : new ObjectParameter("ISPasue", typeof(bool)));
		ObjectParameter objectParameter3 = (money.HasValue ? new ObjectParameter("Money", money) : new ObjectParameter("Money", typeof(decimal)));
		ObjectParameter objectParameter4 = (orderID.HasValue ? new ObjectParameter("OrderID", orderID) : new ObjectParameter("OrderID", typeof(int)));
		ObjectParameter objectParameter5 = (branch.HasValue ? new ObjectParameter("Branch", branch) : new ObjectParameter("Branch", typeof(int)));
		ObjectParameter objectParameter6 = (iSPaymentType.HasValue ? new ObjectParameter("ISPaymentType", iSPaymentType) : new ObjectParameter("ISPaymentType", typeof(bool)));
		ObjectParameter objectParameter7 = (timeA.HasValue ? new ObjectParameter("TimeA", timeA) : new ObjectParameter("TimeA", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Room_A", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7);
	}

	public virtual int Update_Table_A(int? iD, bool? iSPasue, decimal? money, int? orderID, int? branch, bool? iSPaymentType, DateTime? timeA)
	{
		ObjectParameter objectParameter = (iD.HasValue ? new ObjectParameter("ID", iD) : new ObjectParameter("ID", typeof(int)));
		ObjectParameter objectParameter2 = (iSPasue.HasValue ? new ObjectParameter("ISPasue", iSPasue) : new ObjectParameter("ISPasue", typeof(bool)));
		ObjectParameter objectParameter3 = (money.HasValue ? new ObjectParameter("Money", money) : new ObjectParameter("Money", typeof(decimal)));
		ObjectParameter objectParameter4 = (orderID.HasValue ? new ObjectParameter("OrderID", orderID) : new ObjectParameter("OrderID", typeof(int)));
		ObjectParameter objectParameter5 = (branch.HasValue ? new ObjectParameter("Branch", branch) : new ObjectParameter("Branch", typeof(int)));
		ObjectParameter objectParameter6 = (iSPaymentType.HasValue ? new ObjectParameter("ISPaymentType", iSPaymentType) : new ObjectParameter("ISPaymentType", typeof(bool)));
		ObjectParameter objectParameter7 = (timeA.HasValue ? new ObjectParameter("TimeA", timeA) : new ObjectParameter("TimeA", typeof(DateTime)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Table_A", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7);
	}

	public virtual int Update_Tran_Tran(int? referenceCode, int? tranTypeID, string docCode, DateTime? tranDate, string note, int? branchID, int? userID_Add, int? userBranch_Add, string userMacAddress_Add, int? projectId, ObjectParameter transn)
	{
		ObjectParameter objectParameter = (referenceCode.HasValue ? new ObjectParameter("ReferenceCode", referenceCode) : new ObjectParameter("ReferenceCode", typeof(int)));
		ObjectParameter objectParameter2 = (tranTypeID.HasValue ? new ObjectParameter("TranTypeID", tranTypeID) : new ObjectParameter("TranTypeID", typeof(int)));
		ObjectParameter objectParameter3 = ((docCode != null) ? new ObjectParameter("DocCode", docCode) : new ObjectParameter("DocCode", typeof(string)));
		ObjectParameter objectParameter4 = (tranDate.HasValue ? new ObjectParameter("TranDate", tranDate) : new ObjectParameter("TranDate", typeof(DateTime)));
		ObjectParameter objectParameter5 = ((note != null) ? new ObjectParameter("Note", note) : new ObjectParameter("Note", typeof(string)));
		ObjectParameter objectParameter6 = (branchID.HasValue ? new ObjectParameter("BranchID", branchID) : new ObjectParameter("BranchID", typeof(int)));
		ObjectParameter objectParameter7 = (userID_Add.HasValue ? new ObjectParameter("UserID_Add", userID_Add) : new ObjectParameter("UserID_Add", typeof(int)));
		ObjectParameter objectParameter8 = (userBranch_Add.HasValue ? new ObjectParameter("UserBranch_Add", userBranch_Add) : new ObjectParameter("UserBranch_Add", typeof(int)));
		ObjectParameter objectParameter9 = ((userMacAddress_Add != null) ? new ObjectParameter("UserMacAddress_Add", userMacAddress_Add) : new ObjectParameter("UserMacAddress_Add", typeof(string)));
		ObjectParameter objectParameter10 = (projectId.HasValue ? new ObjectParameter("ProjectId", projectId) : new ObjectParameter("ProjectId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_Tran_Tran", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8, objectParameter9, objectParameter10, transn);
	}

	public virtual int UpdateCsr(int? branchId, bool? csIdForUser, string cSR, string publicKey, string privateKey, string secretKey, string modeInvoice, int? userId)
	{
		ObjectParameter objectParameter = (branchId.HasValue ? new ObjectParameter("BranchId", branchId) : new ObjectParameter("BranchId", typeof(int)));
		ObjectParameter objectParameter2 = (csIdForUser.HasValue ? new ObjectParameter("CsIdForUser", csIdForUser) : new ObjectParameter("CsIdForUser", typeof(bool)));
		ObjectParameter objectParameter3 = ((cSR != null) ? new ObjectParameter("CSR", cSR) : new ObjectParameter("CSR", typeof(string)));
		ObjectParameter objectParameter4 = ((publicKey != null) ? new ObjectParameter("PublicKey", publicKey) : new ObjectParameter("PublicKey", typeof(string)));
		ObjectParameter objectParameter5 = ((privateKey != null) ? new ObjectParameter("PrivateKey", privateKey) : new ObjectParameter("PrivateKey", typeof(string)));
		ObjectParameter objectParameter6 = ((secretKey != null) ? new ObjectParameter("SecretKey", secretKey) : new ObjectParameter("SecretKey", typeof(string)));
		ObjectParameter objectParameter7 = ((modeInvoice != null) ? new ObjectParameter("ModeInvoice", modeInvoice) : new ObjectParameter("ModeInvoice", typeof(string)));
		ObjectParameter objectParameter8 = (userId.HasValue ? new ObjectParameter("UserId", userId) : new ObjectParameter("UserId", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateCsr", objectParameter, objectParameter2, objectParameter3, objectParameter4, objectParameter5, objectParameter6, objectParameter7, objectParameter8);
	}

	public virtual int UpdateOrderCosts(int? purchese_ID)
	{
		ObjectParameter objectParameter = (purchese_ID.HasValue ? new ObjectParameter("Purchese_ID", purchese_ID) : new ObjectParameter("Purchese_ID", typeof(int)));
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderCosts", objectParameter);
	}

	public virtual int UpdateOrderCostsAll()
	{
		return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderCostsAll");
	}
}
