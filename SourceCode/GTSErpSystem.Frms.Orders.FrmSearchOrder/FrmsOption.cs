using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Main;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Orders.FrmSearchOrder;

public class FrmsOption : XtraForm
{
	private XmlSerializer xs;

	private XmlSerializer xs2;

	private List<SaveForXml> y1;

	private DataTable datatable = new DataTable();

	private IContainer components = null;

	private GridEX gridEX1;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	public FrmsOption()
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
	}

	public void SetInitialRow()
	{
	}

	private void SaveBackup()
	{
	}

	private void InsertDataInToXml()
	{
	}

	private void GetDataFromXml()
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmSearchOrder.FrmsOption));
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(15, 12);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(398, 272);
		this.gridEX1.TabIndex = 40;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(312, 291);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(100, 40);
		this.BtnExit.TabIndex = 42;
		this.BtnExit.Text = "خروج ";
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(15, 291);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(100, 40);
		this.BtnSave.TabIndex = 41;
		this.BtnSave.Text = "حفظ ";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(425, 344);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.gridEX1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmsOption";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تخصيص";
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


