using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.UserSkins;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace GTSErpSystem;

internal static class Program
{
	private static ServiceProvider serviceProvider;

	[STAThread]
	private static void Main()
	{
		ChangeConnection changeConnection = new ChangeConnection();
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		SecureCrmIntegration.Load();
		LoginDetails.Crm = SecureCrmIntegration.Crm;
		BonusSkins.Register();
		SkinManager.EnableFormSkins();
		LoginDetails.VergDate = "اصدار:VER [1]26.07.08";
		// GetPass(); // temporarily disabled for activation diagnosis
		// Respect the connection saved by FrmSqlConnection. If no saved settings
		// exist, leave the deployed app.config connection unchanged.
		System.Windows.Forms.Application.Idle += AlSaqarTheme.OnApplicationIdle;
		string savedConnectionPath = System.IO.Path.Combine(Application.StartupPath, "ERP.xml");
		if (System.IO.File.Exists(savedConnectionPath))
		{
			try
			{
				string savedSqlConnection = changeConnection.GetSqlConnectionFromFile();
				System.Data.SqlClient.SqlConnectionStringBuilder savedBuilder =
					new System.Data.SqlClient.SqlConnectionStringBuilder(savedSqlConnection);

				if (string.IsNullOrWhiteSpace(savedBuilder.DataSource) ||
					string.IsNullOrWhiteSpace(savedBuilder.InitialCatalog))
				{
					throw new InvalidOperationException(
						"ملف ERP.xml لا يحتوي على اسم خادم وقاعدة بيانات صالحين.");
				}

				changeConnection.SaveConnectionString("GTSdbEntities", savedSqlConnection);
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"تعذر تحميل إعدادات الاتصال المحفوظة. سيُستخدم اتصال app.config الحالي.\r\n" + ex.Message,
					"إعدادات الاتصال",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}
		}
		int num = changeConnection.TestCon();
		if (num > 0)
		{
			LoginDetails.Status = "نسخة مرخصة";
			Application.Run(new FrmLogin3());
		}
		else
		{
			Application.Run(new FrmSqlConnection());
		}
	}

	private static void GetPass()
	{
		string pathValue = "Software\\ERP\\Information";
		LoginDetails.PathValue = pathValue;
	}

	private static void GetVerg2(string Verg)
	{
		switch (Verg)
		{
		case "EFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue9 = "Software\\Tanmay\\Protection";
			LoginDetails.PathValue = pathValue9;
			break;
		}
		case "SilverEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue8 = "Software\\SystemS\\Protection";
			LoginDetails.PathValue = pathValue8;
			break;
		}
		case "SilverPlusEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue7 = "Software\\SystemSS\\Protection";
			LoginDetails.PathValue = pathValue7;
			break;
		}
		case "GoldEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue6 = "Software\\SystemG\\Protection";
			LoginDetails.PathValue = pathValue6;
			break;
		}
		case "GoldPlusEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue5 = "Software\\SystemGG\\Protection";
			LoginDetails.PathValue = pathValue5;
			break;
		}
		case "RestaurantEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue4 = "Software\\SystemR\\Protection";
			LoginDetails.PathValue = pathValue4;
			break;
		}
		case "RestaurantPlusEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue3 = "Software\\SystemRR\\Protection";
			LoginDetails.PathValue = pathValue3;
			break;
		}
		case "BlueEFms":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue2 = "Software\\SystemB\\Protection";
			LoginDetails.PathValue = pathValue2;
			break;
		}
		case "FrmActivation":
		{
			LoginDetails.AppVerg = Verg;
			string pathValue = "Software\\SystemACC\\Protection";
			LoginDetails.PathValue = pathValue;
			break;
		}
		}
	}

internal static class AlSaqarTheme
{
    private static readonly System.Drawing.Color LightGray =
        System.Drawing.Color.FromArgb(243, 245, 248);
    private static readonly System.Drawing.Color Navy =
        System.Drawing.Color.FromArgb(16, 29, 51);
    private static readonly System.Drawing.Color Gold =
        System.Drawing.Color.FromArgb(201, 163, 74);
    private static readonly System.Drawing.Color White =
        System.Drawing.Color.White;
    private const string BrandName = "الصقر لأنظمة المحاسبة";

    private static readonly System.Collections.Generic.HashSet<System.Windows.Forms.Control>
        Seen = new System.Collections.Generic.HashSet<System.Windows.Forms.Control>();

    public static void OnApplicationIdle(object sender, System.EventArgs e)
    {
        for (int i = 0; i < System.Windows.Forms.Application.OpenForms.Count; i++)
        {
            System.Windows.Forms.Form form = System.Windows.Forms.Application.OpenForms[i];
            if (form != null && !form.IsDisposed)
            {
                BrandCaption(form);
                ApplyControl(form);
            }
        }
    }

    private static void BrandCaption(System.Windows.Forms.Form form)
    {
        string title = (form.Text ?? System.String.Empty).Trim();
        if (title.StartsWith(BrandName, System.StringComparison.Ordinal))
            return;

        title = Regex.Replace(title, @"(?i)\b(?:GTS|ERP)\b", String.Empty)
            .Trim(' ', '-', '|', ':', '–', '—');
        form.Text = String.IsNullOrWhiteSpace(title)
            ? BrandName
            : BrandName + " | " + title;
    }

    private static void ApplyControl(System.Windows.Forms.Control control)
    {
        if (control == null || control.IsDisposed)
            return;

        System.Windows.Forms.Form form = control as System.Windows.Forms.Form;
        if (form != null)
        {
            form.BackColor = LightGray;
            if (form.ForeColor == System.Drawing.SystemColors.ControlText ||
                form.ForeColor == System.Drawing.SystemColors.WindowText)
                form.ForeColor = Navy;
        }

        if (!Seen.Add(control))
            return;

        control.ControlAdded += ControlAdded;
        control.Disposed += ControlDisposed;

        System.Windows.Forms.DataGridView grid =
            control as System.Windows.Forms.DataGridView;

        if (grid != null)
        {
            grid.BackgroundColor = White;
            grid.GridColor = System.Drawing.Color.FromArgb(216, 222, 231);
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = White;
            grid.DefaultCellStyle.BackColor = White;
            grid.DefaultCellStyle.ForeColor = Navy;
            grid.AlternatingRowsDefaultCellStyle.BackColor = LightGray;
            grid.RowHeadersDefaultCellStyle.BackColor = Navy;
            grid.RowHeadersDefaultCellStyle.ForeColor = White;
        }
        else
        {
            System.Windows.Forms.ToolStrip strip =
                control as System.Windows.Forms.ToolStrip;

            if (strip != null)
            {
                strip.BackColor = Navy;
                strip.ForeColor = White;
                StyleItems(strip.Items);
            }
            else
            {
                System.Windows.Forms.Button button =
                    control as System.Windows.Forms.Button;

                if (button != null &&
                    (button.UseVisualStyleBackColor ||
                     button.BackColor == System.Drawing.SystemColors.Control))
                {
                    button.UseVisualStyleBackColor = false;
                    button.BackColor = Navy;
                    button.ForeColor = White;
                    button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Gold;
                }
                else if (control is System.Windows.Forms.TextBoxBase ||
                         control is System.Windows.Forms.ComboBox ||
                         control is System.Windows.Forms.DateTimePicker ||
                         control is System.Windows.Forms.NumericUpDown ||
                         control is System.Windows.Forms.ListControl)
                {
                    control.BackColor = White;
                    control.ForeColor = Navy;
                }
                else
                {
                    System.Windows.Forms.LinkLabel link =
                        control as System.Windows.Forms.LinkLabel;

                    if (link != null)
                    {
                        link.LinkColor = Gold;
                        if (link.ForeColor == System.Drawing.SystemColors.ControlText)
                            link.ForeColor = Navy;
                    }
                    else if (control is System.Windows.Forms.Label)
                    {
                        if (control.ForeColor == System.Drawing.SystemColors.ControlText ||
                            control.ForeColor == System.Drawing.SystemColors.WindowText)
                            control.ForeColor = Navy;
                    }

                    if ((control is System.Windows.Forms.Panel ||
                         control is System.Windows.Forms.GroupBox ||
                         control is System.Windows.Forms.TabPage ||
                         control is System.Windows.Forms.TableLayoutPanel ||
                         control is System.Windows.Forms.FlowLayoutPanel) &&
                        control.BackColor == System.Drawing.SystemColors.Control)
                    {
                        control.BackColor = LightGray;
                    }
                }
            }
        }

        foreach (System.Windows.Forms.Control child in control.Controls)
            ApplyControl(child);
    }

    private static void StyleItems(System.Windows.Forms.ToolStripItemCollection items)
    {
        foreach (System.Windows.Forms.ToolStripItem item in items)
        {
            item.BackColor = Navy;
            item.ForeColor = White;

            System.Windows.Forms.ToolStripDropDownItem dropDown =
                item as System.Windows.Forms.ToolStripDropDownItem;

            if (dropDown != null)
                StyleItems(dropDown.DropDownItems);
        }
    }

    private static void ControlAdded(
        object sender, System.Windows.Forms.ControlEventArgs e)
    {
        ApplyControl(e.Control);
    }

    private static void ControlDisposed(object sender, System.EventArgs e)
    {
        System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
        if (control != null)
            Seen.Remove(control);
    }
}
}
