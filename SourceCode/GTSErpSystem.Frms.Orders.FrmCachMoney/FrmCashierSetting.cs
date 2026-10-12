using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Main;

namespace GTSErpSystem.Frms.Orders.FrmCachMoney;

public class FrmCashierSetting : XtraForm
{
	private Class_Setting STNG = new Class_Setting();

	private IContainer components = null;

	private GroupBox groupBox1;

	private SimpleButton BtnExit;

	private SimpleButton BtnSave;

	private System.Windows.Forms.ComboBox comboPrint;

	public System.Windows.Forms.ComboBox comboPrintA4;

	private Label label2;

	private Label label1;

	private ToggleSwitch toggleSectoral;

	private Label label4;

	private Label label3;

	private Label label5;

	public FrmCashierSetting()
	{
		InitializeComponent();
		GetPrinters();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	public void GetPrinters()
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		int num = 1;
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			dictionary.Add(num, installedPrinter);
			num++;
		}
		comboPrint.DataSource = dictionary.ToList();
		comboPrint.DisplayMember = "value";
		comboPrint.ValueMember = "key";
		comboPrintA4.DataSource = dictionary.ToList();
		comboPrintA4.DisplayMember = "value";
		comboPrintA4.ValueMember = "key";
		comboPrint.Text = LoginDetails.DefualtPrint;
		comboPrintA4.Text = LoginDetails.DefualtPrintA4;
	}

	private void Save()
	{
		int num = InsertDataInToXml();
		LoginDetails.DefualtPrint = comboPrint.Text;
		LoginDetails.DefualtPrintA4 = comboPrintA4.Text;
		if (num > 0)
		{
			MessageBox.Show("تم حفظ بيانات الطابعة بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		else
		{
			MessageBox.Show("لم يتم الحفظ طŒ الرجاء المحاولة مرة ثانية", "فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void comboPrint_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private int InsertDataInToXml()
	{
		int result = 0;
		try
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SavePrintForXml>));
			List<SavePrintForXml> list = new List<SavePrintForXml>();
			FileStream fileStream = new FileStream(Application.StartupPath + "\\Print.xml", FileMode.Create, FileAccess.Write);
			SavePrintForXml savePrintForXml = new SavePrintForXml();
			savePrintForXml._PrintName = comboPrint.Text;
			savePrintForXml._PrintNameA4 = comboPrintA4.Text;
			list.Add(savePrintForXml);
			xmlSerializer.Serialize(fileStream, list);
			fileStream.Close();
			result = 1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return result;
	}

	private void RemoveActiveXml()
	{
		try
		{
			File.Delete(Application.StartupPath + "\\Print.xml");
		}
		catch (Exception)
		{
			throw;
		}
		MessageBox.Show("تم حذف ملف الاعدادات بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Orders.FrmCachMoney.FrmCashierSetting));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.comboPrintA4 = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.comboPrint = new System.Windows.Forms.ComboBox();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.toggleSectoral = new DevExpress.XtraEditors.ToggleSwitch();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.toggleSectoral);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.comboPrintA4);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.comboPrint);
		this.groupBox1.Location = new System.Drawing.Point(6, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(495, 307);
		this.groupBox1.TabIndex = 46;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label1.Location = new System.Drawing.Point(358, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(115, 17);
		this.label1.TabIndex = 54;
		this.label1.Text = "اظهار بيانات العميل";
		this.comboPrintA4.FormattingEnabled = true;
		this.comboPrintA4.Location = new System.Drawing.Point(25, 254);
		this.comboPrintA4.Name = "comboPrintA4";
		this.comboPrintA4.Size = new System.Drawing.Size(358, 24);
		this.comboPrintA4.TabIndex = 53;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label2.Location = new System.Drawing.Point(389, 257);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(68, 17);
		this.label2.TabIndex = 52;
		this.label2.Text = "الطابعه A4";
		this.comboPrint.FormattingEnabled = true;
		this.comboPrint.Location = new System.Drawing.Point(25, 219);
		this.comboPrint.Name = "comboPrint";
		this.comboPrint.Size = new System.Drawing.Size(358, 24);
		this.comboPrint.TabIndex = 49;
		this.comboPrint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(comboPrint_KeyPress);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(330, 326);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(146, 40);
		this.BtnExit.TabIndex = 48;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(96, 326);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(145, 40);
		this.BtnSave.TabIndex = 47;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.toggleSectoral.Location = new System.Drawing.Point(93, 22);
		this.toggleSectoral.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.toggleSectoral.Name = "toggleSectoral";
		this.toggleSectoral.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.toggleSectoral.Properties.Appearance.Options.UseFont = true;
		this.toggleSectoral.Properties.Appearance.Options.UseForeColor = true;
		this.toggleSectoral.Properties.OffText = "جملة";
		this.toggleSectoral.Properties.OnText = "قطاعى";
		this.toggleSectoral.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.toggleSectoral.Size = new System.Drawing.Size(218, 25);
		this.toggleSectoral.TabIndex = 199;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label3.Location = new System.Drawing.Point(358, 94);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(105, 17);
		this.label3.TabIndex = 200;
		this.label3.Text = "اظهار المجموعات";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label4.Location = new System.Drawing.Point(358, 58);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(117, 17);
		this.label4.TabIndex = 201;
		this.label4.Text = "اظهار بيانات الفاتورة";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 8f);
		this.label5.Location = new System.Drawing.Point(358, 131);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(129, 17);
		this.label5.TabIndex = 202;
		this.label5.Text = "اظهار بيانات الاجمالي";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(513, 379);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmCashierSetting";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اعدادات الكاشير";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.toggleSectoral.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


