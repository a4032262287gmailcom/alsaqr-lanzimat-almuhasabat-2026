using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlsaqrERP.Next
{
    public sealed class MainForm : Form
    {
        private readonly Color navy = Color.FromArgb(20, 52, 91);
        private readonly Color blue = Color.FromArgb(35, 112, 211);
        private readonly Color canvas = Color.FromArgb(244, 248, 252);
        private readonly Font arabicFont = new Font("Tahoma", 9F, FontStyle.Regular);

        public MainForm()
        {
            Text = "الصقر لأنظمة المحاسبة";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1100, 700);
            BackColor = canvas;
            Font = arabicFont;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            BuildShell();
        }

        private void BuildShell()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 76, BackColor = navy, Padding = new Padding(18, 10, 18, 10) };
            var brand = new Label { Dock = DockStyle.Right, Width = 330, Text = "الصقر لأنظمة المحاسبة\nحلول متكاملة لإدارة أعمالك", ForeColor = Color.White, Font = new Font("Tahoma", 15, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };
            var user = new Label { Dock = DockStyle.Left, Width = 220, Text = "مدير النظام  |  الإعدادات", ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft };
            header.Controls.Add(brand); header.Controls.Add(user);

            var nav = new Panel { Dock = DockStyle.Right, Width = 220, BackColor = navy, Padding = new Padding(10) };
            var navLayout = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, BackColor = navy };
            string[] sections = { "الرئيسية", "المبيعات", "المشتريات", "المخزون", "الحسابات العامة", "العملاء والموردون", "المصروفات", "التقارير", "إعدادات النظام" };
            foreach (string section in sections)
            {
                var button = new Button { Text = section, Width = 190, Height = 44, FlatStyle = FlatStyle.Flat, BackColor = section == "الرئيسية" ? blue : navy, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleRight, Font = new Font("Tahoma", 10, FontStyle.Bold), Margin = new Padding(0, 3, 0, 3), Cursor = Cursors.Hand };
                button.FlatAppearance.BorderSize = 0;
                button.Click += delegate { MessageBox.Show("هذه نقطة تنقل أولية. سيتم ربط شاشة " + section + " بوظائفها وصلاحياتها بعد تدقيق المصدر.", "الصقر ERP", MessageBoxButtons.OK, MessageBoxIcon.Information); };
                navLayout.Controls.Add(button);
            }
            nav.Controls.Add(navLayout);

            var content = new Panel { Dock = DockStyle.Fill, BackColor = canvas, Padding = new Padding(16) };
            var page = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, BackColor = canvas };
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
            page.RowStyles.Add(new RowStyle(SizeType.Absolute, 130));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            page.RowStyles.Add(new RowStyle(SizeType.Percent, 45));

            var welcome = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(225, 238, 250), Margin = new Padding(0, 0, 0, 12), Padding = new Padding(18) };
            welcome.Controls.Add(new Label { Dock = DockStyle.Fill, Text = "مرحباً بك في نظام الصقر المحاسبي\nإدارة مالية ومحاسبية متكاملة لنجاح أعمالك", Font = new Font("Tahoma", 16, FontStyle.Bold), ForeColor = navy, TextAlign = ContentAlignment.MiddleRight });

            var kpis = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, BackColor = canvas, Margin = new Padding(0, 0, 0, 12) };
            for (int i = 0; i < 4; i++) kpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            AddKpi(kpis, 0, "إجمالي المبيعات", "—", Color.FromArgb(105, 65, 210));
            AddKpi(kpis, 1, "إجمالي المشتريات", "—", blue);
            AddKpi(kpis, 2, "إجمالي المصروفات", "—", Color.FromArgb(28, 145, 82));
            AddKpi(kpis, 3, "المخزون الحالي", "—", Color.FromArgb(235, 125, 28));

            var middle = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = canvas, Margin = new Padding(0, 0, 0, 10) };
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58)); middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            middle.Controls.Add(Placeholder("حركة المبيعات والمشتريات", "ستظهر المؤشرات بعد ربط مصدر البيانات."), 0, 0);
            middle.Controls.Add(Placeholder("التنبيهات", "سيتم عرض تنبيهات المخزون والاعتمادات هنا."), 1, 0);

            var bottom = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = canvas };
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65)); bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            bottom.Controls.Add(Placeholder("أحدث العمليات", "لا توجد بيانات متصلة بعد. لا يتم عرض أرقام مالية تجريبية."), 0, 0);
            bottom.Controls.Add(Placeholder("التقارير المهمة", "تقارير المبيعات والمشتريات والمخزون والأرباح والخسائر."), 1, 0);

            page.Controls.Add(welcome, 0, 0); page.Controls.Add(kpis, 0, 1); page.Controls.Add(middle, 0, 2); page.Controls.Add(bottom, 0, 3);
            content.Controls.Add(page);

            var footer = new StatusStrip { Dock = DockStyle.Bottom, BackColor = navy, ForeColor = Color.White };
            footer.Items.Add(new ToolStripStatusLabel("الصقر لأنظمة المحاسبة — واجهة تأسيسية"));
            footer.Items.Add(new ToolStripStatusLabel { Spring = true });
            footer.Items.Add(new ToolStripStatusLabel("قاعدة البيانات: غير متصلة"));

            Controls.Add(content); Controls.Add(nav); Controls.Add(header); Controls.Add(footer);
        }

        private void AddKpi(TableLayoutPanel parent, int column, string title, string value, Color accent)
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = accent, Margin = new Padding(0, 0, 8, 0), Padding = new Padding(12) };
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, BackColor = accent };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Tahoma", 10, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            layout.Controls.Add(new Label { Text = value, Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Tahoma", 19, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight }, 0, 1);
            card.Controls.Add(layout); parent.Controls.Add(card, column, 0);
        }

        private Control Placeholder(string title, string description)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 0, 10, 0), Padding = new Padding(14) };
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1 };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.Controls.Add(new Label { Text = title, Dock = DockStyle.Fill, ForeColor = navy, Font = new Font("Tahoma", 11, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            layout.Controls.Add(new Label { Text = description, Dock = DockStyle.Fill, ForeColor = Color.DimGray, TextAlign = ContentAlignment.MiddleCenter }, 0, 1);
            panel.Controls.Add(layout); return panel;
        }
    }
}