using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Stimulsoft.Report.Viewer;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmReportGetOrder : XtraForm
{
	private IContainer components = null;

	public StiViewerControl stiViewerControl1;

	public FrmReportGetOrder()
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
		this.stiViewerControl1 = new Stimulsoft.Report.Viewer.StiViewerControl();
		base.SuspendLayout();
		this.stiViewerControl1.AllowDrop = true;
		this.stiViewerControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.stiViewerControl1.Location = new System.Drawing.Point(12, 12);
		this.stiViewerControl1.Name = "stiViewerControl1";
		this.stiViewerControl1.PageViewMode = Stimulsoft.Report.Viewer.StiPageViewMode.SinglePage;
		this.stiViewerControl1.Report = null;
		this.stiViewerControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.stiViewerControl1.ShowZoom = true;
		this.stiViewerControl1.Size = new System.Drawing.Size(693, 446);
		this.stiViewerControl1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(717, 470);
		base.Controls.Add(this.stiViewerControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "FrmReportGetOrder";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "معاينة قبل الطباعة";
		base.ResumeLayout(false);
	}
}


