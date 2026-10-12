using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GTSErpSystem.BLL.OrderRent;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmItemRentDeatils : XtraForm
{
	private Class_Rent Rent = new Class_Rent();

	private int ItemID;

	private int BranchID;

	private int StoreID;

	private IContainer components = null;

	public GridEX gridEX1;

	private GroupBox groupBox1;

	private Label label3;

	private Label label4;

	private Label label6;

	private Label label7;

	private GroupBox groupBox2;

	private Label label5;

	private Label label2;

	private GroupBox groupBox3;

	private GroupBox groupBox4;

	private TextBox TxtName;

	private GroupBox groupBox5;

	private BackgroundWorker backgroundWorker1;

	public FrmItemRentDeatils(int itemID, int branchID, int storeID)
	{
		InitializeComponent();
		ItemID = itemID;
		BranchID = branchID;
		StoreID = storeID;
		Bind();
	}

	private void Bind()
	{
		gridEX1.ColumnAutoResize = true;
		DataTable dataTable = new DataTable("gridEX1");
		dataTable.Columns.Add("اسم العميل", typeof(string));
		dataTable.Columns.Add("الكمية", typeof(string));
		dataTable.Columns.Add("تاريخ التأجير", typeof(string));
		dataTable.Columns.Add("تاريخ الاستلام", typeof(string));
		dataTable.Columns.Add("عدد الأيام", typeof(string));
		dataTable.Columns.Add("باقي الأيام", typeof(string));
		List<GetRented_Result> itemRentalDetails = Rent.GetItemRentalDetails(ItemID, BranchID, StoreID);
		if (itemRentalDetails != null)
		{
			foreach (GetRented_Result item in itemRentalDetails)
			{
				decimal num = Math.Round(Convert.ToDecimal(item.Quantity));
				decimal num2 = Math.Round(Convert.ToDecimal(item.RestDays));
				decimal num3 = Math.Round(Convert.ToDecimal(item.DayNums));
				decimal num4 = Math.Round(Convert.ToDecimal(item.OpeningBalance));
				decimal num5 = Math.Round(Convert.ToDecimal(item.CurrentBalance));
				decimal num6 = Math.Round(Convert.ToDecimal(item.RentedQuantity));
				TxtName.Text = item.Item_Name ?? "غير متوفر";
				label5.Text = num4.ToString() ?? "0";
				label6.Text = num5.ToString() ?? "0";
				label7.Text = num6.ToString() ?? "0";
				dataTable.Rows.Add(item.SupplierName ?? "غير معروف", num, item.RentalDate?.ToString("yyyy-MM-dd") ?? "غير متوفر", item.ReturnDate?.ToString("yyyy-MM-dd") ?? "غير متوفر", num3, num2);
			}
		}
		gridEX1.SetDataBinding(dataTable, string.Empty);
		gridEX1.RetrieveStructure();
		GridEXColumnCollection columns = gridEX1.RootTable.Columns;
		columns["اسم العميل"].TextAlignment = TextAlignment.Center;
		columns["الكمية"].TextAlignment = TextAlignment.Center;
		columns["تاريخ التأجير"].TextAlignment = TextAlignment.Center;
		columns["تاريخ الاستلام"].TextAlignment = TextAlignment.Center;
		columns["عدد الأيام"].TextAlignment = TextAlignment.Center;
		columns["باقي الأيام"].TextAlignment = TextAlignment.Center;
		TxtName.BackColor = Color.White;
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
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.EmptyRows = true;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(6, 14);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(886, 295);
		this.gridEX1.TabIndex = 13;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Location = new System.Drawing.Point(12, 212);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(898, 316);
		this.groupBox1.TabIndex = 14;
		this.groupBox1.TabStop = false;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Green;
		this.label3.Location = new System.Drawing.Point(78, 69);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(134, 24);
		this.label3.TabIndex = 17;
		this.label3.Text = "الكميه المتاحه ";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Red;
		this.label4.Location = new System.Drawing.Point(79, 69);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(134, 24);
		this.label4.TabIndex = 18;
		this.label4.Text = "الكميه المتأجره";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.Green;
		this.label6.Location = new System.Drawing.Point(128, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(21, 24);
		this.label6.TabIndex = 20;
		this.label6.Text = "0";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.Red;
		this.label7.Location = new System.Drawing.Point(128, 26);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(21, 24);
		this.label7.TabIndex = 21;
		this.label7.Text = "0";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox2.BackColor = System.Drawing.Color.LightBlue;
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Location = new System.Drawing.Point(12, 77);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(298, 123);
		this.groupBox2.TabIndex = 22;
		this.groupBox2.TabStop = false;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
		this.label5.Location = new System.Drawing.Point(133, 26);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(21, 24);
		this.label5.TabIndex = 21;
		this.label5.Text = "0";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
		this.label2.Location = new System.Drawing.Point(81, 69);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(149, 24);
		this.label2.TabIndex = 20;
		this.label2.Text = "الكميه الاساسيه";
		this.groupBox3.BackColor = System.Drawing.Color.Honeydew;
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Location = new System.Drawing.Point(312, 77);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(298, 123);
		this.groupBox3.TabIndex = 23;
		this.groupBox3.TabStop = false;
		this.groupBox4.BackColor = System.Drawing.Color.MistyRose;
		this.groupBox4.Controls.Add(this.label7);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Location = new System.Drawing.Point(612, 77);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(298, 123);
		this.groupBox4.TabIndex = 24;
		this.groupBox4.TabStop = false;
		this.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtName.Font = new System.Drawing.Font("Tahoma", 16.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtName.ForeColor = System.Drawing.Color.Green;
		this.TxtName.Location = new System.Drawing.Point(3, 14);
		this.TxtName.Name = "TxtName";
		this.TxtName.ReadOnly = true;
		this.TxtName.Size = new System.Drawing.Size(892, 40);
		this.TxtName.TabIndex = 25;
		this.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.groupBox5.Controls.Add(this.TxtName);
		this.groupBox5.Location = new System.Drawing.Point(12, 9);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(898, 62);
		this.groupBox5.TabIndex = 21;
		this.groupBox5.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(922, 540);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmItemRentDeatils";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "تقاصيل الصنف";
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		base.ResumeLayout(false);
	}
}


