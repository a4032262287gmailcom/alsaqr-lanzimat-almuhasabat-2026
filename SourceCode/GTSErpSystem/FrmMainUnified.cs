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
        private static readonly Dictionary<string, int[]> PermissionScreens =
            new Dictionary<string, int[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "FrmAccountTree", new[] { 28 } },
                { "FrmAdd", new[] { 156 } },
                { "FrmAllContract", new[] { 90 } },
                { "FrmAllContractActiveDisActive", new[] { 93, 95 } },
                { "FrmAllContractByCostomer", new[] { 92 } },
                { "FrmAllGuarantee", new[] { 91 } },
                { "FrmAllOrders", new[] { 46 } },
                { "FrmAllOrdersBounce", new[] { 110 } },
                { "FrmAllOrdersCashierSafryMahly", new[] { 100 } },
                { "FrmAllOrdersCheckOut", new[] { 46 } },
                { "FrmAllOrdersDetails", new[] { 54 } },
                { "FrmAllOrdersDetailsByGroup", new[] { 96, 112 } },
                { "FrmAllOrdersDetailsByProperties", new[] { 115 } },
                { "FrmAllOrdersProfit", new[] { 61 } },
                { "FrmAllOrdersProfitByGroup", new[] { 68 } },
                { "FrmAllOrdersProfitByGroupBy", new[] { 133 } },
                { "FrmAllOrdersProfitByItems", new[] { 69 } },
                { "FrmAllOrdersProfitByItems_ItemOnly", new[] { 70 } },
                { "FrmAllOrdersProfitCashierSafryMahly", new[] { 101 } },
                { "FrmAllOrdersRestaurant", new[] { 58 } },
                { "FrmAllOrdersReturnDetails", new[] { 55 } },
                { "FrmAllOrederReturn", new[] { 47 } },
                { "FrmAllPrinters", new[] { 104 } },
                { "FrmAllPurches", new[] { 48 } },
                { "FrmAllPurchesDetails", new[] { 56 } },
                { "FrmAllPurchesReturn", new[] { 49 } },
                { "FrmAllPurchesReturnDetails", new[] { 57 } },
                { "FrmAllowances", new[] { 120 } },
                { "FrmBranches", new[] { 26 } },
                { "FrmCardAccount", new[] { 27 } },
                { "FrmCashir", new[] { 12, 157 } },
                { "FrmCashirPharm", new[] { 12 } },
                { "FrmCashirRestaurant", new[] { 12, 157 } },
                { "FrmCashirWashing", new[] { 157 } },
                { "FrmCashirWithOutGroup", new[] { 12 } },
                { "FrmCheckActivationElctroncInv", new[] { 160 } },
                { "FrmCheckCollection", new[] { 124 } },
                { "FrmCheckCollectionIssued", new[] { 126 } },
                { "FrmCheckIssued", new[] { 125 } },
                { "FrmCheckOut", new[] { 59 } },
                { "FrmClass", new[] { 6, 8 } },
                { "FrmCloseDayByUser", new[] { 117 } },
                { "FrmCompany", new[] { 4, 8 } },
                { "FrmContract", new[] { 89, 98 } },
                { "FrmCostCenter", new[] { 29 } },
                { "FrmCostCenterByFather", new[] { 134 } },
                { "FrmCostCenterTree", new[] { 28 } },
                { "FrmCountry", new[] { 8, 80 } },
                { "FrmCustomer", new[] { 23 } },
                { "FrmDailyRestrictions", new[] { 36 } },
                { "FrmDailyRestrictionsTest", new[] { 35 } },
                { "FrmDalala", new[] { 107 } },
                { "FrmDefualtAccount", new[] { 39 } },
                { "FrmDefualtCustomer", new[] { 39 } },
                { "FrmDelivery", new[] { 86 } },
                { "FrmDesignerReport", new[] { 40 } },
                { "FrmDoctor", new[] { 105 } },
                { "FrmDrivers", new[] { 97 } },
                { "FrmEJAR", new[] { 147 } },
                { "FrmEmployee", new[] { 76 } },
                { "FrmEntrusted", new[] { 121 } },
                { "FrmEntrustedEmployee", new[] { 122 } },
                { "FrmGenetareCSID", new[] { 160 } },
                { "FrmGroups", new[] { 2 } },
                { "FrmGroupsRes", new[] { 7, 8 } },
                { "FrmGuarantee", new[] { 8 } },
                { "FrmInventorySettlementMinus", new[] { 83 } },
                { "FrmItems", new[] { 8 } },
                { "FrmManufacturing", new[] { 119 } },
                { "FrmManufacturingOrder", new[] { 118 } },
                { "FrmMovementsDailyReport", new[] { 41 } },
                { "FrmMovementsDailyReportAllBran", new[] { 87 } },
                { "FrmNetOrders", new[] { 153 } },
                { "FrmNetProfitSalesMan", new[] { 131 } },
                { "FrmNetPurchOrders", new[] { 154 } },
                { "FrmNetPurches", new[] { 152 } },
                { "FrmOpenAccount", new[] { 30 } },
                { "FrmOpenQuantity", new[] { 18 } },
                { "FrmOrderDetailsByDate", new[] { 143 } },
                { "FrmOrderReturn", new[] { 14 } },
                { "FrmOrderReturnRental", new[] { 111 } },
                { "FrmOrders", new[] { 12 } },
                { "FrmOrdersDraft", new[] { 10 } },
                { "FrmOrdersNetPurchOnly", new[] { 155 } },
                { "FrmOrdersReturn", new[] { 151 } },
                { "FrmOrdersReturnNoPurchCode", new[] { 16 } },
                { "FrmOrdersWedding", new[] { 12 } },
                { "FrmPassword", new[] { 1 } },
                { "FrmPayment", new[] { 32 } },
                { "FrmPaymentBig", new[] { 34 } },
                { "FrmPaymentItem", new[] { 103 } },
                { "FrmPayroll", new[] { 78 } },
                { "FrmPayrollTemp", new[] { 77 } },
                { "FrmPlace", new[] { 21 } },
                { "FrmPriceOffer", new[] { 10 } },
                { "FrmPrinterCashier", new[] { 37 } },
                { "FrmProjects", new[] { 139 } },
                { "FrmPurchaseOrder", new[] { 11 } },
                { "FrmPurchases", new[] { 13 } },
                { "FrmPurchasesReturn", new[] { 15 } },
                { "FrmPurchasesReturnNoPurchCode", new[] { 17 } },
                { "FrmPurches", new[] { 149 } },
                { "FrmPurchesReturn", new[] { 150 } },
                { "FrmRPCostCenter", new[] { 134 } },
                { "FrmRPDeportation", new[] { 43 } },
                { "FrmRPItemLimit", new[] { 74 } },
                { "FrmRPLeadger", new[] { 43, 66, 67 } },
                { "FrmRPLeadgerCar", new[] { 43 } },
                { "FrmRPLeadgerSupp", new[] { 113 } },
                { "FrmRPMizan", new[] { 62 } },
                { "FrmRPMizanCustSupp", new[] { 51 } },
                { "FrmRPMizanGroups", new[] { 64 } },
                { "FrmRPMizanTotal", new[] { 63 } },
                { "FrmRPMizaniyah", new[] { 45 } },
                { "FrmRPMizaniyahAllBran", new[] { 89 } },
                { "FrmRPMovementAllItemActive", new[] { 102 } },
                { "FrmRPMovementItem", new[] { 60 } },
                { "FrmRPMovementItemActive", new[] { 102 } },
                { "FrmRPMovementsDailyReport", new[] { 41 } },
                { "FrmRPOrderDaily", new[] { 116 } },
                { "FrmRPProfitAndLossAccount", new[] { 44 } },
                { "FrmRPProfitAndLossAccountAllBran", new[] { 88 } },
                { "FrmRPProject", new[] { 140 } },
                { "FrmRPStoresGard", new[] { 42 } },
                { "FrmRPStoresGardbyProperties", new[] { 42 } },
                { "FrmRPTasferByGroup", new[] { 141 } },
                { "FrmRPTranTeacher", new[] { 73 } },
                { "FrmReceiptCheck", new[] { 123 } },
                { "FrmReceipts", new[] { 31 } },
                { "FrmReceiptsBig", new[] { 33 } },
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
                { "FrmSalesEmployee", new[] { 53 } },
                { "FrmSalesMan", new[] { 22 } },
                { "FrmSecurityGroup", new[] { 3 } },
                { "FrmSending", new[] { 106 } },
                { "FrmSetting", new[] { 38 } },
                { "FrmStoreGardCat", new[] { 42 } },
                { "FrmStores", new[] { 25 } },
                { "FrmStoresTransfer", new[] { 20, 60 } },
                { "FrmSuppliers", new[] { 24 } },
                { "FrmTable", new[] { 85 } },
                { "FrmTobaccoTaxReport", new[] { 75 } },
                { "FrmTransferBranchMaster", new[] { 144 } },
                { "FrmTransferFromBranch", new[] { 95 } },
                { "FrmTransferMasterToBranch", new[] { 145 } },
                { "FrmTransferToBranch", new[] { 94 } },
                { "FrmUnit", new[] { 5, 8 } },
                { "FrmUsers", new[] { 1 } },
                { "FrmVatReport", new[] { 50 } }
            };

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
            public int[] ScreenIDs;
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
                ColumnCount = 2,
                RowCount = 1,
                RightToLeft = RightToLeft.Yes,
                BackColor = Background,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            var sidebar = BuildSidebar();
            var content = BuildContent();

            root.Controls.Add(sidebar, 0, 0);
            root.Controls.Add(content, 1, 0);
            Controls.Add(root);
        }

        private Control BuildSidebar()
        {
            var side = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Navy,
                RightToLeft = RightToLeft.Yes,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            side.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
            side.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            side.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));

            var brand = new Panel { Dock = DockStyle.Fill, BackColor = Navy };
            brand.Controls.Add(new Label
            {
                Text = "الصقر",
                Dock = DockStyle.Top,
                Height = 55,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = White
            });
            brand.Controls.Add(new Label
            {
                Text = "لأنظمة المحاسبة",
                Dock = DockStyle.Bottom,
                Height = 38,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 226, 235)
            });

            var navigation = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Navy,
                Padding = new Padding(10, 8, 10, 8),
                RightToLeft = RightToLeft.Yes
            };

            foreach (string category in Categories)
            {
                var navButton = new Button
                {
                    Text = "   " + category,
                    Width = 215,
                    Height = 45,
                    Margin = new Padding(0, 3, 0, 3),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = category == SelectedCategory ? Blue : Navy,
                    ForeColor = White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleRight,
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false,
                    Tag = category
                };
                navButton.FlatAppearance.BorderSize = 0;
                navButton.Click += delegate
                {
                    Navigate(category);
                };
                navigation.Controls.Add(navButton);
            }

            var footer = new Panel { Dock = DockStyle.Fill, BackColor = Navy };
            var exitButton = new Button
            {
                Text = "خروج من النظام",
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(120, 44, 49),
                ForeColor = White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
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
            footer.Controls.Add(exitButton);

            side.Controls.Add(brand, 0, 0);
            side.Controls.Add(navigation, 0, 1);
            side.Controls.Add(footer, 0, 2);
            return side;
        }

        private Control BuildContent()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Background,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            var header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = White,
                Padding = new Padding(22, 8, 22, 8)
            };

            header.Controls.Add(new Label
            {
                Text = "لوحة التشغيل الرئيسية",
                Dock = DockStyle.Right,
                Width = 410,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 21F, FontStyle.Bold),
                ForeColor = Navy
            });

            UserInfo = new Label
            {
                Text = GetUserInfo(),
                Dock = DockStyle.Left,
                Width = 390,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Blue
            };
            header.Controls.Add(UserInfo);

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = White,
                Padding = new Padding(12, 12, 12, 8),
                RightToLeft = RightToLeft.Yes
            };

            PageTitle = new Label
            {
                Text = SelectedCategory,
                Width = 220,
                Height = 38,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Navy,
                Margin = new Padding(0, 1, 8, 0)
            };

            ResultCount = new Label
            {
                Text = "",
                Width = 130,
                Height = 38,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Gray,
                Margin = new Padding(0, 1, 8, 0)
            };

            var searchLabel = new Label
            {
                Text = "بحث:",
                Width = 45,
                Height = 38,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = TextColor,
                Margin = new Padding(0, 1, 8, 0)
            };

            SearchBox = new TextBox
            {
                Width = 285,
                Height = 32,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(0, 4, 8, 0),
                RightToLeft = RightToLeft.Yes
            };
            SearchBox.TextChanged += delegate { RenderTiles(); };

            var refresh = new Button
            {
                Text = "تحديث القائمة",
                Width = 112,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = Navy,
                ForeColor = White,
                Margin = new Padding(10, 1, 0, 0),
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
                Padding = new Padding(18),
                RightToLeft = RightToLeft.Yes
            };

            var status = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = White,
                Padding = new Padding(16, 2, 16, 2)
            };
            status.Controls.Add(new Label
            {
                Text = "الصقر لأنظمة المحاسبة  |  سطح مكتب Windows  |  " +
                       DateTime.Now.ToString("yyyy/MM/dd"),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F)
            });

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(toolbar, 0, 1);
            layout.Controls.Add(Tiles, 0, 2);
            layout.Controls.Add(status, 0, 3);
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
                if (type == null || !type.IsClass || type.IsAbstract)
                    continue;

                if (!typeof(Form).IsAssignableFrom(type))
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

                ConstructorInfo ctor = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null, Type.EmptyTypes, null);

                int[] screenIds;
                if (ctor == null || !PermissionScreens.TryGetValue(name, out screenIds))
                    continue;

                Modules.Add(new ModuleEntry
                {
                    FormType = type,
                    Category = GetCategory(type),
                    Caption = GetCaption(type),
                    ScreenIDs = screenIds
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

            if (HasAny(n, "Prvlg", "Permission", "Security", "UserLogin",
                    "FrmLock", "PassWord", "Password"))
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
            if (type.Name.Equals("FrmDefualtAccount", StringComparison.OrdinalIgnoreCase))
            {
                return type.Namespace != null &&
                       type.Namespace.Equals("GTSErpSystem.Frms.FrmsMain",
                           StringComparison.OrdinalIgnoreCase)
                    ? "تهيئة الحسابات الافتراضية للفرع"
                    : "ربط الحسابات الافتراضية للأصناف";
            }

            if (type.Name.Equals("FrmDefualtCustomer", StringComparison.OrdinalIgnoreCase))
                return "ربط الحسابات الافتراضية للعملاء";

            var names = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase)
            {
                { "FrmAccountTree", "شجرة الحسابات" },
                { "FrmCardAccount", "بطاقة الحساب" },
                { "FrmDailyRestrictions", "القيود اليومية" },
                { "FrmCostCenter", "مراكز التكلفة" },
                { "FrmProjects", "المشاريع" },
                { "FrmReceipts", "سندات القبض" },
                { "FrmPayment", "سندات الصرف" },
                { "FrmCashir", "نقطة البيع" },
                { "FrmCashirRestaurant", "نقطة بيع المطاعم" },
                { "FrmOrders", "فواتير المبيعات" },
                { "FrmOrderReturn", "مرتجعات المبيعات" },
                { "FrmPurchases", "فواتير المشتريات" },
                { "FrmPurches", "فواتير المشتريات" },
                { "FrmPurchasesReturn", "مرتجعات المشتريات" },
                { "FrmItems", "الأصناف" },
                { "FrmStores", "المخازن" },
                { "FrmStoresTransfer", "تحويل المخزون" },
                { "FrmCustomer", "العملاء" },
                { "FrmSuppliers", "الموردون" },
                { "FrmBranches", "الفروع" },
                { "FrmSalesMan", "مندوبي المبيعات" },
                { "FrmSetting", "إعدادات النظام" },
                { "FrmBackUp", "النسخ الاحتياطي" },
                { "FrmImportExcel", "استيراد Excel" },
                { "FrmGuarantee", "الضمانات" },
                { "FrmContract", "العقود" },
                { "FrmRent", "الإيجارات" },
                { "FrmPriceOffer", "عروض الأسعار" },
                { "FrmSearch", "البحث" }
            };

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

                ResultCount.Text = Modules.Count + " شاشة متاحة بالنظام";
            }
            else
            {
                foreach (ModuleEntry module in matches)
                {
                    AddTile(module.Caption, module.Category + "  •  فتح الشاشة",
                        delegate { OpenModule(module); });
                }

                ResultCount.Text = matches.Count + " نتيجة";

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
            string key = module.FormType.FullName;

            try
            {
                CheckPrvlg permissions = new CheckPrvlg();
                bool hasPermission = module.ScreenIDs.Any(screenId =>
                    permissions.CheckUserPage(LoginDetails.GroupID, screenId));
                if (!hasPermission)
                {
                    MessageBox.Show(
                        "عفواً، لا تملك صلاحية فتح هذه الشاشة. راجع مدير النظام.",
                        "الصقر لأنظمة المحاسبة",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);
                    return;
                }

                Form existing;
                if (OpenForms.TryGetValue(key, out existing) &&
                    existing != null && !existing.IsDisposed)
                {
                    if (existing.WindowState == FormWindowState.Minimized)
                        existing.WindowState = FormWindowState.Normal;

                    existing.Activate();
                    return;
                }

                var form = Activator.CreateInstance(module.FormType, true) as Form;
                if (form == null)
                    throw new InvalidOperationException("The selected type is not a Windows Form.");

                OpenForms[key] = form;

                form.FormClosed += delegate
                {
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