using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Stimulsoft.Report.Design;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmDesignerReport : XtraForm
{
	private IContainer components = null;

	private StiRibbonDesignerControl stiRibbonDesignerControl1;

	public FrmDesignerReport()
	{
		InitializeComponent();
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
		this.stiRibbonDesignerControl1 = new Stimulsoft.Report.Design.StiRibbonDesignerControl();
		base.SuspendLayout();
		this.stiRibbonDesignerControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.stiRibbonDesignerControl1.BackColor = System.Drawing.Color.White;
		this.stiRibbonDesignerControl1.Location = new System.Drawing.Point(12, 12);
		this.stiRibbonDesignerControl1.Name = "stiRibbonDesignerControl1";
		this.stiRibbonDesignerControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.stiRibbonDesignerControl1.ShowCaption = false;
		this.stiRibbonDesignerControl1.ShowHelp = false;
		this.stiRibbonDesignerControl1.ShowLocalizationMenu = false;
		this.stiRibbonDesignerControl1.ShowMainMenuReportOpenFromGoogleDocs = false;
		this.stiRibbonDesignerControl1.ShowMainMenuReportSaveAsToGoogleDocs = false;
		this.stiRibbonDesignerControl1.Size = new System.Drawing.Size(1095, 648);
		this.stiRibbonDesignerControl1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1119, 672);
		base.Controls.Add(this.stiRibbonDesignerControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmDesignerReport";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مصمم التقارير";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}


