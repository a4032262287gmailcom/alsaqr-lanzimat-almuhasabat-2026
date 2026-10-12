using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.Properties;

namespace GTSErpSystem.Frms.Contract;

public class FrmViewFiles : XtraForm
{
	private List<string> _filePaths;

	private IContainer components = null;

	private FlowLayoutPanel flowPanelFiles;

	private SimpleButton BtnNew;

	public FrmViewFiles(List<string> filePaths)
	{
		InitializeComponent();
		_filePaths = filePaths;
	}

	private bool IsImageFile(string path)
	{
		string text = Path.GetExtension(path).ToLower();
		int result;
		switch (text)
		{
		default:
			result = ((text == ".gif") ? 1 : 0);
			break;
		case ".jpg":
		case ".jpeg":
		case ".png":
		case ".bmp":
			result = 1;
			break;
		}
		return (byte)result != 0;
	}

	private void flowPanelFiles_Paint(object sender, PaintEventArgs e)
	{
	}

	private void FrmViewFiles_Load(object sender, EventArgs e)
	{
		foreach (string filePath in _filePaths)
		{
			string fullPath = Path.Combine(Application.StartupPath, filePath);
			if (!File.Exists(fullPath))
			{
				continue;
			}
			Control value;
			if (IsImageFile(fullPath))
			{
				PictureBox pictureBox = new PictureBox();
				pictureBox.Image = Image.FromFile(fullPath);
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.Width = 200;
				pictureBox.Height = 200;
				value = pictureBox;
			}
			else if (Path.GetExtension(fullPath).ToLower() == ".pdf")
			{
				PictureBox pictureBox2 = new PictureBox();
				pictureBox2.Image = Resources.pdf2;
				pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox2.Width = 50;
				pictureBox2.Height = 50;
				pictureBox2.Cursor = Cursors.Hand;
				pictureBox2.Tag = fullPath;
				ToolTip toolTip = new ToolTip();
				toolTip.SetToolTip(pictureBox2, Path.GetFileName(fullPath));
				pictureBox2.Click += delegate
				{
					Process.Start(new ProcessStartInfo
					{
						FileName = fullPath,
						UseShellExecute = true
					});
				};
				value = pictureBox2;
			}
			else
			{
				Label label = new Label();
				label.Text = "غير مدعوم: " + Path.GetFileName(fullPath);
				value = label;
			}
			flowPanelFiles.Controls.Add(value);
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Close();
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
		this.flowPanelFiles = new System.Windows.Forms.FlowLayoutPanel();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		base.SuspendLayout();
		this.flowPanelFiles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowPanelFiles.Location = new System.Drawing.Point(12, 37);
		this.flowPanelFiles.Name = "flowPanelFiles";
		this.flowPanelFiles.Size = new System.Drawing.Size(1061, 506);
		this.flowPanelFiles.TabIndex = 0;
		this.flowPanelFiles.Paint += new System.Windows.Forms.PaintEventHandler(flowPanelFiles_Paint);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = GTSErpSystem.Properties.Resources.movedown_16x16;
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(13, 550);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(275, 40);
		this.BtnNew.TabIndex = 8;
		this.BtnNew.Text = "استمرار";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1085, 597);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.flowPanelFiles);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmViewFiles";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "عرض المرفقات";
		base.Load += new System.EventHandler(FrmViewFiles_Load);
		base.ResumeLayout(false);
	}
}


