using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GTSErpSystem.BLL.Security;

namespace GTSErpSystem
{
    public sealed class FrmMainUnified : Form
    {
        private readonly Color Navy = Color.FromArgb(20, 34, 56);
        private readonly Color Blue = Color.FromArgb(39, 73, 112);
        private readonly Color Gold = Color.FromArgb(201, 163, 74);
        private readonly Color Background = Color.FromArgb(243, 245, 248);
        private readonly Color White = Color.White;
        private readonly Color TextColor = Color.FromArgb(40, 52, 69);

        private readonly List<string> Categories = new List<string>
        {
            "الرئيسية",
            "المحاسبة",
            "السندات",
            "المبيعات",
            "المشتريات",
            "المخزون والأصناف",
            "المراكز والعمليات",
            "التقارير",
            "الأمن والصلاحيات",
            "الإعدادات"
        };

        private readonly List<ModuleEntry> Modules = new List<ModuleEntry>();
        private readonly Dictionary<string, Form> OpenForms =
            new Dictionary<string, Form>(StringComparer.OrdinalIgnoreCase);
        private readonly CheckPrvlg PermissionChecker = new CheckPrvlg();

        // Explicitly copied from the legacy ribbon handlers in FrmWindow.
        // A form without a reviewed screen-permission mapping is never exposed here.
        private static readonly Dictionary<string, int[]> ScreenPermissions =
            new Dictionary<string, int[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "FrmAdd", new[] { 156 } },
                { "FrmAllContract", new[] { 90 } },
                { "FrmAllContractActiveDisActive", new[] { 93, 95 } },
                { "FrmAllContractByCostomer", new[] { 92 } },
                { "FrmAllGuarantee", new[] { 91 } },
                { "FrmAllOrdersBounce", new[] { 110 } },
                { "FrmAllOrdersCashierSafryMahly", new[] { 100 } },
                { "FrmAllOrdersCheckOut", new[] { 46 } },
                { "FrmAllOrdersDetailsByGroup", new[] { 96, 112 } },
                { "FrmAllOrdersDetailsByProperties", new[] { 115 } },
                { "FrmAllOrdersProfitByGroupBy", new[] { 133 } },
                { "FrmAllOrdersProfitCashierSafryMahly", new[] { 101 } },
                { "FrmAllPrinters", new[] { 104 } },
                { "FrmAllowances", new[] { 120 } },
                { "FrmCashirPharm", new[] { 12 } },
                { "FrmCashirWashing", new[] { 157 } },
                { "FrmCashirWithOutGroup", new[] { 12 } },
                { "FrmCheckActivationElctroncInv", new[] { 160 } },
                { "FrmCheckCollection", new[] { 124 } },
                { "FrmCheckCollectionIssued", new[] { 126 } },
                { "FrmCheckIssued", new[] { 125 } },
                { "FrmCloseDayByUser", new[] { 117 } },
                { "FrmContract", new[] { 89, 98 } },
                { "FrmCostCenterByFather", new[] { 134 } },
                { "FrmCostCenterTree", new[] { 28 } },
                { "FrmCountry", new[] { 8, 80 } },
                { "FrmDalala", new[] { 107 } },
                { "FrmDelivery", new[] { 86 } },
                { "FrmDoctor", new[] { 105 } },
                { "FrmDrivers", new[] { 97 } },
                { "FrmEJAR", new[] { 147 } },
                { "FrmEntrusted", new[] { 121 } },
                { "FrmEntrustedEmployee", new[] { 122 } },
                { "FrmGenetareCSID", new[] { 160 } },
                { "FrmGuarantee", new[] { 8 } },
                { "FrmInventorySettlementMinus", new[] { 83 } },
                { "FrmManufacturing", new[] { 119 } },
                { "FrmManufacturingOrder", new[] { 118 } },
                { "FrmMovementsDailyReport", new[] { 41 } },
                { "FrmMovementsDailyReportAllBran", new[] { 87 } },
                { "FrmNetOrders", new[] { 153 } },
                { "FrmNetProfitSalesMan", new[] { 131 } },
                { "FrmNetPurchOrders", new[] { 154 } },
                { "FrmNetPurches", new[] { 152 } },
                { "FrmOrderDetailsByDate", new[] { 143 } },
                { "FrmOrderReturnRental", new[] { 111 } },
                { "FrmOrdersDraft", new[] { 10 } },
                { "FrmOrdersNetPurchOnly", new[] { 155 } },
                { "FrmOrdersReturn", new[] { 151 } },
                { "FrmOrdersWedding", new[] { 12 } },
                { "FrmPassword", new[] { 1 } },
                { "FrmPaymentItem", new[] { 103 } },
                { "FrmProjects", new[] { 139 } },
                { "FrmPurches", new[] { 149 } },
                { "FrmPurchesReturn", new[] { 150 } },
                { "FrmRPCostCenter", new[] { 134 } },
                { "FrmRPDeportation", new[] { 43 } },
                { "FrmRPItemLimit", new[] { 74 } },
                { "FrmRPLeadgerCar", new[] { 43 } },
                { "FrmRPLeadgerSupp", new[] { 113 } },
                { "FrmRPMizanCustSupp", new[] { 51 } },
                { "FrmRPMizaniyahAllBran", new[] { 89 } },
                { "FrmRPMovementAllItemActive", new[] { 102 } },
                { "FrmRPMovementItemActive", new[] { 102 } },
                { "FrmRPOrderDaily", new[] { 116 } },
                { "FrmRPProfitAndLossAccountAllBran", new[] { 88 } },
                { "FrmRPProject", new[] { 140 } },
                { "FrmRPStoresGard", new[] { 42 } },
                { "FrmRPTasferByGroup", new[] { 141 } },
                { "FrmReceiptCheck", new[] { 123 } },
                { "FrmReceiptsScaffolds", new[] { 31 } },
                { "FrmRecipt", new[] { 112 } },
                { "FrmRecipt3", new[] { 112 } },
                { "FrmRecpItem", new[] { 138 } },
                { "FrmRent", new[] { 111 } },
                { "FrmRentReports2", new[] { 158 } },
                { "FrmRentalInvoice", new[] { 111 } },
                { "FrmReplace", new[] { 17 } },
                { "FrmReportAll", new[] { 82 } },
                { "FrmReportOrderDalala_Dalala", new[] { 108 } },
                { "FrmReportOrderDalala_Sending", new[] { 109 } },
                { "FrmRoom", new[] { 84 } },
                { "FrmRpSalesPurchesForSupp", new[] { 142 } },
                { "FrmSending", new[] { 106 } },
                { "FrmStoreGardCat", new[] { 42 } },
                { "FrmTable", new[] { 85 } },
                { "FrmTransferBranchMaster", new[] { 144 } },
                { "FrmTransferFromBranch", new[] { 95 } },
                { "FrmTransferMasterToBranch", new[] { 145 } },
                { "FrmTransferToBranch", new[] { 94 } },
                { "FrmUsers", new[] { 1 } },
                { "FrmGroups", new[] { 2 } },
                { "FrmSecurityGroup", new[] { 3 } },
                { "FrmCompany", new[] { 4, 8 } },
                { "FrmUnit", new[] { 5, 8 } },
                { "FrmClass", new[] { 6, 8 } },
                { "FrmGroupsRes", new[] { 7, 8 } },
                { "FrmItems", new[] { 8 } },
                { "FrmPriceOffer", new[] { 10 } },
                { "FrmPurchaseOrder", new[] { 11 } },
                { "FrmCashir", new[] { 12, 157 } },
                { "FrmCashirRestaurant", new[] { 12, 157 } },
                { "FrmPurchases", new[] { 13 } },
                { "FrmOrderReturn", new[] { 14 } },
                { "FrmPurchasesReturn", new[] { 15 } },
                { "FrmOrdersReturnNoPurchCode", new[] { 16 } },
                { "FrmPurchasesReturnNoPurchCode", new[] { 17 } },
                { "FrmOpenQuantity", new[] { 18 } },
                { "FrmStoresTransfer", new[] { 20, 60 } },
                { "FrmPlace", new[] { 21 } },
                { "FrmSalesMan", new[] { 22 } },
                { "FrmCustomer", new[] { 23 } },
                { "FrmSuppliers", new[] { 24 } },
                { "FrmStores", new[] { 25 } },
                { "FrmBranches", new[] { 26 } },
                { "FrmCardAccount", new[] { 27 } },
                { "FrmAccountTree", new[] { 28 } },
                { "FrmDefualtAccount", new[] { 39 } },
                { "FrmDefualtCustomer", new[] { 39 } },
                { "FrmCostCenter", new[] { 29 } },
                { "FrmOpenAccount", new[] { 30 } },
                { "FrmReceipts", new[] { 31 } },
                { "FrmPayment", new[] { 32 } },
                { "FrmReceiptsBig", new[] { 33 } },
                { "FrmPaymentBig", new[] { 34 } },
                { "FrmDailyRestrictionsTest", new[] { 35 } },
                { "FrmDailyRestrictions", new[] { 36 } },
                { "FrmPrinterCashier", new[] { 37 } },
                { "FrmSetting", new[] { 38 } },
                { "FrmDesignerReport", new[] { 40 } },
                { "FrmRPMovementsDailyReport", new[] { 41 } },
                { "FrmRPStoresGardbyProperties", new[] { 42 } },
                { "FrmRPLeadger", new[] { 43, 66, 67 } },
                { "FrmRPProfitAndLossAccount", new[] { 44 } },
                { "FrmRPMizaniyah", new[] { 45 } },
                { "FrmAllOrders", new[] { 46 } },
                { "FrmAllOrederReturn", new[] { 47 } },
                { "FrmAllPurches", new[] { 48 } },
                { "FrmAllPurchesReturn", new[] { 49 } },
                { "FrmVatReport", new[] { 50 } },
                { "FrmSalesEmployee", new[] { 53 } },
                { "FrmAllOrdersDetails", new[] { 54 } },
                { "FrmAllOrdersReturnDetails", new[] { 55 } },
                { "FrmAllPurchesDetails", new[] { 56 } },
                { "FrmAllPurchesReturnDetails", new[] { 57 } },
                { "FrmAllOrdersRestaurant", new[] { 58 } },
                { "FrmCheckOut", new[] { 59 } },
                { "FrmRPMovementItem", new[] { 60 } },
                { "FrmAllOrdersProfit", new[] { 61 } },
                { "FrmRPMizan", new[] { 62 } },
                { "FrmRPMizanTotal", new[] { 63 } },
                { "FrmRPMizanGroups", new[] { 64 } },
                { "FrmRPTranTeacher", new[] { 73 } },
                { "FrmTobaccoTaxReport", new[] { 75 } },
                { "FrmEmployee", new[] { 76 } },
                { "FrmPayrollTemp", new[] { 77 } },
                { "FrmPayroll", new[] { 78 } },
                { "FrmAllOrdersProfitByGroup", new[] { 68 } },
                { "FrmAllOrdersProfitByItems", new[] { 69 } },
                { "FrmAllOrdersProfitByItems_ItemOnly", new[] { 70 } },
                { "FrmOrders", new[] { 12 } }
            };

        // Resolve names that exist in more than one report namespace.
        // The canonical targets below are the same types opened by FrmWindow.
        private static readonly string[] CanonicalFormNames =
        {
            "GTSErpSystem.Frms.Reports.Orders.FrmAllOrders",
            "GTSErpSystem.Frms.Reports.Orders.FrmAllOrederReturn",
            "GTSErpSystem.Frms.Reports.Orders.FrmAllOrdersDetails",
            "GTSErpSystem.Frms.Reports.Orders.FrmAllOrdersReturnDetails",
            "GTSErpSystem.Frms.Reports.Profit.FrmAllOrdersProfit",
            "GTSErpSystem.Frms.Reports.Profit.FrmAllOrdersProfitByItems_ItemOnly",
            "GTSErpSystem.Frms.Orders.FrmOrders"
        };

        private static bool IsCanonicalMappedForm(Type type)
        {
            if (type == null)
                return false;

            string canonicalName = CanonicalFormNames.FirstOrDefault(
                fullName => fullName.EndsWith("." + type.Name, StringComparison.OrdinalIgnoreCase));

            return canonicalName == null ||
                string.Equals(type.FullName, canonicalName, StringComparison.OrdinalIgnoreCase);
        }

        private FlowLayoutPanel Tiles;
        private TextBox SearchBox;
        private Label PageTitle;
        private Label ResultCount;
        private Label UserInfo;
        private string SelectedCategory = "الرئيسية";

        private sealed class ModuleEntry
        {
            public Type FormType;
            public string Category;
            public string Caption;
            public Func<Form> CreateForm;
        }

        public FrmMainUnified()
        {
            Text = "الصقر لأنظمة المحاسبة";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1050, 650);
            BackColor = Background;
            ForeColor = TextColor;
            Font = new Font("Segoe UI", 10F);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            KeyPreview = true;

            DiscoverModules();
            BuildInterface();
            RenderTiles();

            FormClosed += delegate { Application.Exit(); };
        }

        private void BuildInterface()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Background,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 116));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            root.Controls.Add(BuildTopBar(), 0, 0);
            root.Controls.Add(BuildContent(), 0, 1);
            Controls.Add(root);
        }

        private Control BuildTopBar()
        {
            var topBar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Navy,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            topBar.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            topBar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Header is arranged in three stable regions so labels do not overlap
            // when the Windows Forms application is resized.
            var header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = White,
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty,
                Padding = new Padding(16, 3, 16, 3)
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29F));
            header.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var brand = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = White,
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            brand.Controls.Add(new Label
            {
                Text = "الصقر",
                Width = 92,
                Height = 54,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 21F, FontStyle.Bold),
                ForeColor = Navy,
                Margin = new Padding(0)
            });
            brand.Controls.Add(new Label
            {
                Text = "لأنظمة المحاسبة",
                Width = 132,
                Height = 54,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Blue,
                Margin = new Padding(4, 0, 0, 0)
            });

            var titlePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = White,
                Margin = Padding.Empty
            };
            titlePanel.Controls.Add(new Label
            {
                Text = "لوحة التشغيل الرئيسية",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Navy
            });

            var accountPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = White,
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty,
                Padding = new Padding(0, 7, 0, 0)
            };
            UserInfo = new Label
            {
                Text = GetUserInfo(),
                Width = 190,
                Height = 40,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Blue,
                Margin = new Padding(0, 1, 5, 0)
            };
            accountPanel.Controls.Add(UserInfo);

            var exitButton = new Button
            {
                Text = "خروج",
                Width = 84,
                Height = 36,
                Margin = new Padding(4, 2, 4, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(120, 44, 49),
                ForeColor = White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += delegate
            {
                if (MessageBox.Show(
                    "هل تريد إغلاق النظام؟",
                    "الصقر لأنظمة المحاسبة",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };
            accountPanel.Controls.Add(exitButton);

            header.Controls.Add(brand, 0, 0);
            header.Controls.Add(titlePanel, 1, 0);
            header.Controls.Add(accountPanel, 2, 0);

            var navigation = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Navy,
                Padding = new Padding(14, 5, 14, 5),
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty
            };

            foreach (string category in Categories)
            {
                var navButton = new Button
                {
                    Text = category,
                    Width = category == "المخزون والأصناف" || category == "الأمن والصلاحيات" ? 142 : 116,
                    Height = 40,
                    Margin = new Padding(3, 1, 3, 1),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = category == SelectedCategory ? Blue : Navy,
                    ForeColor = White,
                    Font = new Font("Segoe UI", 9.5F,
                        category == SelectedCategory ? FontStyle.Bold : FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false,
                    Tag = category
                };
                navButton.FlatAppearance.BorderSize = 0;
                navButton.FlatAppearance.BorderColor = Gold;
                navButton.Click += delegate { Navigate(category); };
                navigation.Controls.Add(navButton);
            }

            topBar.Controls.Add(header, 0, 0);
            topBar.Controls.Add(navigation, 0, 1);
            return topBar;
        }

        private Control BuildContent()
        {
            // The app title and user information live in the top bar;
            // this area is reserved for category context, search, and modules.
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Background,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = White,
                Padding = new Padding(16, 12, 16, 7),
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty
            };

            PageTitle = new Label
            {
                Text = SelectedCategory,
                Width = 205,
                Height = 36,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Navy,
                Margin = new Padding(0, 1, 8, 0)
            };

            ResultCount = new Label
            {
                Text = "",
                Width = 165,
                Height = 36,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(105, 116, 132),
                Margin = new Padding(0, 1, 10, 0)
            };

            var searchLabel = new Label
            {
                Text = "بحث:",
                Width = 45,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = TextColor,
                Margin = new Padding(0, 1, 8, 0)
            };

            SearchBox = new TextBox
            {
                Width = 285,
                Height = 32,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(0, 3, 8, 0),
                RightToLeft = RightToLeft.Yes
            };
            SearchBox.TextChanged += delegate { RenderTiles(); };

            var refresh = new Button
            {
                Text = "تحديث القائمة",
                Width = 112,
                Height = 34,
                FlatStyle = FlatStyle.Flat,
                BackColor = Navy,
                ForeColor = White,
                Margin = new Padding(8, 1, 0, 0),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            refresh.FlatAppearance.BorderSize = 0;
            refresh.Click += delegate
            {
                DiscoverModules();
                RenderTiles();
            };

            toolbar.Controls.Add(PageTitle);
            toolbar.Controls.Add(ResultCount);
            toolbar.Controls.Add(searchLabel);
            toolbar.Controls.Add(SearchBox);
            toolbar.Controls.Add(refresh);

            Tiles = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = Background,
                Padding = new Padding(18, 16, 18, 18),
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty
            };

            var status = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = White,
                Padding = new Padding(16, 2, 16, 2),
                Margin = Padding.Empty
            };
            status.Controls.Add(new Label
            {
                Text = "الصقر لأنظمة المحاسبة  |  " + DateTime.Now.ToString("yyyy/MM/dd"),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F)
            });

            layout.Controls.Add(toolbar, 0, 0);
            layout.Controls.Add(Tiles, 0, 1);
            layout.Controls.Add(status, 0, 2);
            return layout;
        }

        private string GetUserInfo()
        {
            string username = string.IsNullOrWhiteSpace(LoginDetails.UserName)
                ? "المستخدم"
                : LoginDetails.UserName;

            return "المستخدم: " + username +
                   "     |     الفرع: " + LoginDetails.BranchID;
        }

                private void DiscoverModules()
        {
            Modules.Clear();
            if (LoginDetails.GroupID <= 0)
                return;

            HashSet<int> allowedScreenIds;
            try
            {
                allowedScreenIds = new HashSet<int>(
                    PermissionChecker.GetAllowedScreenIds(LoginDetails.GroupID));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "تعذر تحميل صلاحيات الشاشات؛ تم إيقاف عرض الشاشات لحماية النظام.\r\n" + ex.Message,
                    "الصقر لأنظمة المحاسبة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                return;
            }

            Type[] types;
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray();
            }

            foreach (Type type in types)
            {
                if (type == null || !type.IsClass || type.IsAbstract ||
                    !typeof(Form).IsAssignableFrom(type))
                    continue;

                string ns = type.Namespace ?? "";
                string name = type.Name;

                if (!ns.StartsWith("GTSErpSystem", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (ns.IndexOf(".Frms.FrmsHome", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;

                if (name.Equals("FrmMainUnified", StringComparison.OrdinalIgnoreCase) ||
                    name.Equals("WaitForm", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("FrmLogin", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("FrmActiv", StringComparison.OrdinalIgnoreCase) ||
                    name.IndexOf("Activation", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.IndexOf("Activtion", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.IndexOf("FrmTest", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.Equals("FrmSqlConnection", StringComparison.OrdinalIgnoreCase))
                    continue;

                int[] screenIds;
                if (!ScreenPermissions.TryGetValue(name, out screenIds) ||
                    !IsCanonicalMappedForm(type) ||
                    !screenIds.Any(id => allowedScreenIds.Contains(id)))
                    continue;

                Func<Form> formFactory = null;
                ConstructorInfo ctor = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null, Type.EmptyTypes, null);

                // The legacy ribbon opens a new invoice with FrmOrders(null).
                // Preserve that exact entry path; do not invent invoice data.
                if (type == typeof(GTSErpSystem.Frms.Orders.FrmOrders))
                {
                    formFactory = delegate
                    {
                        return new GTSErpSystem.Frms.Orders.FrmOrders(null);
                    };
                }
                else if (ctor == null)
                {
                    continue;
                }

                Modules.Add(new ModuleEntry
                {
                    FormType = type,
                    Category = GetCategory(type),
                    Caption = GetCaption(type),
                    CreateForm = formFactory
                });
            }

            Modules.Sort(delegate(ModuleEntry a, ModuleEntry b)
            {
                int c = string.Compare(a.Category, b.Category,
                    StringComparison.CurrentCulture);
                return c != 0 ? c : string.Compare(a.Caption, b.Caption,
                    StringComparison.CurrentCulture);
            });
        }

                private static string GetCategory(Type type)
        {
            string n = type.Name;
            string ns = type.Namespace ?? "";

            // Explicit exceptions for forms whose namespace otherwise misclassifies them.
            if (n.Equals("FrmStoresTransfer", StringComparison.OrdinalIgnoreCase) ||
                n.Equals("FrmOpenQuantity", StringComparison.OrdinalIgnoreCase))
                return "المخزون والأصناف";

            if (n.Equals("FrmPrinterCashier", StringComparison.OrdinalIgnoreCase))
                return "الإعدادات";

            if (ns.IndexOf(".Frms.Reports", StringComparison.OrdinalIgnoreCase) >= 0 ||
                n.StartsWith("FrmRP", StringComparison.OrdinalIgnoreCase) ||
                HasAny(n, "Report", "Rpt", "TotalAverg"))
                return "التقارير";

            if (HasAny(n, "Purch", "Purches", "Purchase") ||
                ns.IndexOf("Purch", StringComparison.OrdinalIgnoreCase) >= 0)
                return "المشتريات";

            if (ns.IndexOf(".Frms.Orders", StringComparison.OrdinalIgnoreCase) >= 0 ||
                ns.IndexOf(".OrderDalala", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Cashir", "Orders", "OrderReturn", "Sales", "PriceOffer"))
                return "المبيعات";

            if (ns.IndexOf(".Frms.Account.Check", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Receipts", "Recipt", "Payment", "Bounce", "Check"))
                return "السندات";

            if (ns.IndexOf(".Frms.Items", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Items", "Item", "Stores", "Store", "Inventory",
                    "Barcode", "Unit", "Stock"))
                return "المخزون والأصناف";

            if (ns.IndexOf(".Frms.Account", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Account", "CostCenter", "DailyRestriction", "OpenAccount"))
                return "المحاسبة";

            if (ns.IndexOf(".Frms.Security", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Prvlg", "Permission", "Security", "UserLogin",
                    "FrmLock", "PassWord", "Password", "FrmUsers", "FrmGroups"))
                return "الأمن والصلاحيات";

            if (HasAny(n, "Setting", "Configuration", "Connection", "Backup"))
                return "الإعدادات";

            if (ns.IndexOf(".Frms.Contract", StringComparison.OrdinalIgnoreCase) >= 0 ||
                ns.IndexOf(".Frms.Cust_Sup", StringComparison.OrdinalIgnoreCase) >= 0 ||
                HasAny(n, "Contract", "Dalala", "Room", "Rent", "Hall",
                    "Repair", "Branch", "Customer", "Supplier", "Project",
                    "Transfer", "Delivery", "Sending", "Group"))
                return "المراكز والعمليات";

            if (HasAny(n, "Account", "Restriction", "CostCenter"))
                return "المحاسبة";

            return "المراكز والعمليات";
        }

        private static bool HasAny(string value, params string[] terms)
        {
            foreach (string term in terms)
            {
                if (value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }
            return false;
        }

        private static string GetCaption(Type type)
        {
            // Use indexer assignments so repeated keys cannot abort startup.
            // When a legacy key is repeated, the final caption in this list wins.
            var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            names["FrmAccountTree"] = "شجرة الحسابات";
            names["FrmDefualtAccount"] = "الحسابات الافتراضية";
            names["FrmDefualtCustomer"] = "حسابات العملاء الافتراضية";
            names["FrmUsers"] = "المستخدمون";
            names["FrmGroups"] = "مجموعات المستخدمين";
            names["FrmSecurityGroup"] = "صلاحيات المجموعات";
            names["FrmOpenQuantity"] = "كميات افتتاحية";
            names["FrmVatReport"] = "تقرير ضريبة القيمة المضافة";
            names["FrmPrinterCashier"] = "إعدادات طباعة فواتير نقطة البيع";
            names["FrmSetting"] = "إعدادات النظام العامة";
            names["FrmCardAccount"] = "بطاقة الحساب";
            names["FrmDailyRestrictions"] = "القيود اليومية";
            names["FrmCostCenter"] = "مراكز التكلفة";
            names["FrmProjects"] = "المشاريع";
            names["FrmReceipts"] = "سندات القبض";
            names["FrmPayment"] = "سندات الصرف";
            names["FrmCashir"] = "نقطة البيع";
            names["FrmCashirRestaurant"] = "نقطة بيع المطاعم";
            names["FrmOrders"] = "فواتير المبيعات";
            names["FrmOrderReturn"] = "مرتجعات المبيعات";
            names["FrmPurchases"] = "فواتير المشتريات";
            names["FrmPurches"] = "فواتير المشتريات";
            names["FrmPurchasesReturn"] = "مرتجعات المشتريات";
            names["FrmItems"] = "الأصناف";
            names["FrmStores"] = "المخازن";
            names["FrmStoresTransfer"] = "تحويل المخزون";
            names["FrmCustomer"] = "العملاء";
            names["FrmSuppliers"] = "الموردون";
            names["FrmBranches"] = "الفروع";
            names["FrmSalesMan"] = "مندوبي المبيعات";
            names["FrmSetting"] = "إعدادات النظام";
            names["FrmBackUp"] = "النسخ الاحتياطي";
            names["FrmImportExcel"] = "استيراد Excel";
            names["FrmGuarantee"] = "الضمانات";
            names["FrmContract"] = "العقود";
            names["FrmRent"] = "الإيجارات";
            names["FrmPriceOffer"] = "عروض الأسعار";
            names["FrmSearch"] = "البحث";
            names["FrmCompany"] = "الشركات";
            names["FrmUnit"] = "وحدات القياس";
            names["FrmSuppliers"] = "الموردون";
            names["FrmSalesMan"] = "مندوبو المبيعات";
            names["FrmPurchaseOrder"] = "أوامر الشراء";
            names["FrmReceiptsBig"] = "سندات القبض المتعددة";
            names["FrmPaymentBig"] = "سندات الصرف المتعددة";
            names["FrmDailyRestrictionsTest"] = "القيود اليومية المعلقة";
            names["FrmDesignerReport"] = "مصمم التقارير";
            names["FrmRPMovementsDailyReport"] = "تقرير الحركة اليومية";
            names["FrmRPStoresGardbyProperties"] = "تقرير أرصدة المخزون حسب الخصائص";
            names["FrmRPLeadger"] = "دفتر الأستاذ";
            names["FrmRPProfitAndLossAccount"] = "تقرير الأرباح والخسائر";
            names["FrmRPMizaniyah"] = "الميزانية العمومية";
            names["FrmAllOrders"] = "فواتير المبيعات";
            names["FrmAllOrederReturn"] = "مرتجعات المبيعات";
            names["FrmAllPurches"] = "فواتير المشتريات";
            names["FrmAllPurchesReturn"] = "مرتجعات المشتريات";
            names["FrmAllOrdersDetails"] = "تفاصيل فواتير المبيعات";
            names["FrmAllOrdersReturnDetails"] = "تفاصيل مرتجعات المبيعات";
            names["FrmAllPurchesDetails"] = "تفاصيل فواتير المشتريات";
            names["FrmAllPurchesReturnDetails"] = "تفاصيل مرتجعات المشتريات";
            names["FrmAllOrdersRestaurant"] = "تقرير مبيعات المطاعم";
            names["FrmCheckOut"] = "تسوية المخزون";
            names["FrmRPMovementItem"] = "حركة صنف";
            names["FrmAllOrdersProfit"] = "أرباح المبيعات";
            names["FrmRPMizan"] = "ميزان المراجعة";
            names["FrmRPMizanTotal"] = "ميزان المراجعة الإجمالي";
            names["FrmRPMizanGroups"] = "ميزان المراجعة حسب المجموعات";
            names["FrmEmployee"] = "الموظفون";
            names["FrmPayrollTemp"] = "مسير الرواتب المؤقت";
            names["FrmPayroll"] = "مسير الرواتب";
            names["FrmCashirPharm"] = "نقطة بيع الصيدلية";
            names["FrmCashirWashing"] = "نقطة بيع المغسلة";
            names["FrmAllOrdersProfitByGroup"] = "أرباح المبيعات حسب المجموعة";
            names["FrmAllOrdersProfitByItems"] = "أرباح المبيعات حسب الصنف";
            names["FrmOrdersReturnNoPurchCode"] = "مرتجعات المبيعات دون فاتورة أصلية";
            names["FrmPurchasesReturnNoPurchCode"] = "مرتجعات المشتريات دون فاتورة أصلية";

            string caption;
            if (names.TryGetValue(type.Name, out caption))
                return caption;

            string raw = type.Name.StartsWith("Frm", StringComparison.OrdinalIgnoreCase)
                ? type.Name.Substring(3)
                : type.Name;

            raw = Regex.Replace(raw, "([a-z])([A-Z])", "$1 $2");
            raw = raw.Replace("Cashir", "كاشير")
                     .Replace("Purches", "مشتريات")
                     .Replace("Purchases", "مشتريات")
                     .Replace("Search", "بحث")
                     .Replace("Report", "تقرير")
                     .Replace("Stores", "المخازن")
                     .Replace("Items", "الأصناف");

            return "شاشة " + raw;
        }

        private static string GetModuleDescription(ModuleEntry module)
        {
            string caption = module.Caption;
            if (caption.StartsWith("شاشة ", StringComparison.Ordinal))
                caption = caption.Substring("شاشة ".Length);

            switch (module.FormType.Name)
            {
                case "FrmUsers":
                    return "إضافة المستخدمين وإدارة حساباتهم";
                case "FrmGroups":
                    return "إنشاء مجموعات المستخدمين وتنظيمها";
                case "FrmSecurityGroup":
                    return "تحديد صلاحيات الدخول والعمليات للمجموعات";
                case "FrmSetting":
                    return "تهيئة إعدادات النظام العامة";
                case "FrmPrinterCashier":
                    return "ضبط طباعة فواتير وإيصالات نقطة البيع";
                case "FrmDefualtAccount":
                    return "ربط الحسابات الافتراضية وإعدادات المستودعات";
                case "FrmDefualtCustomer":
                    return "ربط الحسابات الافتراضية للعملاء";
                case "FrmCostCenter":
                    return "إدارة مراكز التكلفة وبياناتها";
                case "FrmProjects":
                    return "إدارة المشاريع ومراكزها";
                case "FrmAccountTree":
                    return "استعراض وإدارة دليل الحسابات";
                case "FrmDailyRestrictions":
                    return "إدخال القيود اليومية ومراجعتها";
            }

            if (module.Category == "التقارير")
                return "عرض " + caption;
            if (module.Category == "المبيعات" || module.Category == "المشتريات")
                return "تسجيل ومتابعة " + caption;
            if (module.Category == "السندات")
                return "إدارة " + caption + " والحركة المالية";
            if (module.Category == "المخزون والأصناف")
                return "إدارة " + caption + " وحركة المخزون";
            if (module.Category == "الأمن والصلاحيات")
                return "إدارة " + caption + " والوصول للنظام";
            if (module.Category == "الإعدادات")
                return "تهيئة " + caption;

            return "إدارة " + caption + " وبياناتها";
        }

        private void Navigate(string category)
        {
            SelectedCategory = category;
            RenderTiles();

            foreach (Control control in Controls)
                RefreshNavigationHighlight(control);
        }

        private void RefreshNavigationHighlight(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                var button = child as Button;
                if (button != null && button.Tag is string)
                {
                    string category = (string)button.Tag;
                    button.BackColor = category == SelectedCategory ? Blue : Navy;
                }

                if (child.HasChildren)
                    RefreshNavigationHighlight(child);
            }
        }

        private void RenderTiles()
        {
            if (Tiles == null)
                return;

            Tiles.SuspendLayout();
            Tiles.Controls.Clear();

            PageTitle.Text = SelectedCategory;
            string query = SearchBox == null ? "" : SearchBox.Text.Trim();
            var matches = new List<ModuleEntry>();

            if (!string.IsNullOrWhiteSpace(query))
            {
                matches = Modules.Where(m =>
                    m.Caption.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    m.FormType.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    m.Category.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0
                ).ToList();
            }
            else if (SelectedCategory != "الرئيسية")
            {
                matches = Modules.Where(m => m.Category == SelectedCategory).ToList();
            }

            if (SelectedCategory == "الرئيسية" && string.IsNullOrWhiteSpace(query))
            {
                foreach (string category in Categories.Where(c => c != "الرئيسية"))
                {
                    int count = Modules.Count(m => m.Category == category);
                    AddTile(category, count + " شاشة متاحة",
                        delegate { Navigate(category); });
                }

                foreach (ModuleEntry module in Modules.Where(m =>
                    HasAny(m.FormType.Name, "FrmCashir", "FrmOrders",
                        "FrmItems", "FrmAccountTree", "FrmReceipts"))
                    .Take(6))
                {
                    AddTile(module.Caption, "اختصار سريع • " + module.Category,
                        delegate { OpenModule(module); });
                }

                ResultCount.Text = Modules.Count + " شاشة متاحة بصلاحياتك";
            }
            else
            {
                foreach (ModuleEntry module in matches)
                {
                    AddTile(module.Caption, GetModuleDescription(module),
                        delegate { OpenModule(module); });
                }

                ResultCount.Text = matches.Count + " شاشة حسب صلاحياتك";

                if (matches.Count == 0)
                {
                    Tiles.Controls.Add(new Label
                    {
                        Text = "لا توجد شاشة مطابقة في هذا القسم. جرّب البحث أو اختر قسمًا آخر.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.Gray,
                        Margin = new Padding(18)
                    });
                }
            }

            Tiles.ResumeLayout();
        }

        private void AddTile(string title, string subtitle, Action action)
        {
            var button = new Button
            {
                Width = 245,
                Height = 112,
                Margin = new Padding(9),
                FlatStyle = FlatStyle.Flat,
                BackColor = White,
                ForeColor = Navy,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Text = title + Environment.NewLine + Environment.NewLine + subtitle,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(14, 8, 14, 8),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };

            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.FromArgb(221, 226, 234);
            button.Click += delegate { action(); };
            Tiles.Controls.Add(button);
        }

                private void OpenModule(ModuleEntry module)
        {
            if (module == null || module.FormType == null || LoginDetails.GroupID <= 0)
                return;

            int[] screenIds;
            bool allowed = false;
            try
            {
                if (IsCanonicalMappedForm(module.FormType) &&
                    ScreenPermissions.TryGetValue(module.FormType.Name, out screenIds))
                    allowed = screenIds.Any(id =>
                        PermissionChecker.CheckUserPage(LoginDetails.GroupID, id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "تعذر التحقق من صلاحية الشاشة؛ لم يتم فتحها.\r\n" + ex.Message,
                    "الصقر لأنظمة المحاسبة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                return;
            }

            if (!allowed)
            {
                MessageBox.Show(
                    "عفوًا، لا توجد لديك صلاحية دخول لهذه الشاشة. راجع مدير النظام.",
                    "صلاحيات المستخدم",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                return;
            }

            string key = module.FormType.FullName;

            Form existing;
            if (OpenForms.TryGetValue(key, out existing) &&
                existing != null && !existing.IsDisposed)
            {
                if (existing.WindowState == FormWindowState.Minimized)
                    existing.WindowState = FormWindowState.Normal;

                existing.Activate();
                return;
            }

            try
            {
                Form form = module.CreateForm != null
                    ? module.CreateForm()
                    : Activator.CreateInstance(module.FormType, true) as Form;

                if (form == null)
                    throw new InvalidOperationException("The selected type is not a Windows Form.");

                OpenForms[key] = form;

                form.FormClosed += delegate
                {
                    Form current;
                    if (OpenForms.TryGetValue(key, out current) &&
                        ReferenceEquals(current, form))
                        OpenForms.Remove(key);
                };

                form.Show(this);
                form.BringToFront();
            }
            catch (Exception ex)
            {
                Exception actual = ex;
                while (actual.InnerException != null)
                    actual = actual.InnerException;

                MessageBox.Show(
                    "تعذر فتح الشاشة: " + module.Caption +
                    Environment.NewLine + Environment.NewLine + actual.Message,
                    "الصقر لأنظمة المحاسبة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
            }
        }
    }
}