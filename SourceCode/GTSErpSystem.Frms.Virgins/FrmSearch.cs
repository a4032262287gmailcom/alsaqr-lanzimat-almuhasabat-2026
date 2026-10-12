using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Virgins;

public class FrmSearch : XtraForm
{
	private GTSdbEntities DB = new GTSdbEntities();

	private DataTable myTable = new DataTable("gridEX1");

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private TextBox TxtSerialBoard2;

	private TextBox TxtHDD2;

	private TextBox TxtMac2;

	private Label SerialNumber;

	private Label label1;

	private Label label2;

	public TextBox TxtSerialBoard;

	public TextBox TxtHDD;

	public TextBox TxtMac;

	public FrmSearch()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		InitializeComponent();
		BindReport();
		TxtHDD.Text = LoginDetails.HDD;
		TxtMac.Text = LoginDetails.Mac;
		TxtSerialBoard.Text = LoginDetails.SerialBoard;
	}

	private void BindReport()
	{
		DataTable dataTable = new DataTable();
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		SqlCommand sqlCommand = new SqlCommand("Get_Serial", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@HDD", LoginDetails.HDD);
		sqlDataAdapter.Fill(dataTable);
		sqlConnection.Close();
		gridEX1.ColumnAutoResize = true;
		gridEX1.AllowEdit = InheritableBoolean.True;
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		gridEX1.ColumnAutoSizeMode = ColumnAutoSizeMode.Default;
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			TxtHDD2.Text = gridEX1.GetRow(position).Cells[5].Text;
			TxtMac2.Text = gridEX1.GetRow(position).Cells[6].Text;
			TxtSerialBoard2.Text = gridEX1.GetRow(position).Cells[7].Text;
			if (TxtHDD2.Text == TxtHDD.Text)
			{
				TxtHDD2.BackColor = Color.MistyRose;
			}
			else
			{
				TxtHDD.BackColor = Color.White;
			}
			if (TxtMac2.Text == TxtMac.Text)
			{
				TxtMac2.BackColor = Color.MistyRose;
			}
			else
			{
				TxtMac2.BackColor = Color.White;
			}
			if (TxtSerialBoard2.Text == TxtSerialBoard.Text)
			{
				TxtSerialBoard2.BackColor = Color.MistyRose;
			}
			else
			{
				TxtSerialBoard2.BackColor = Color.White;
			}
		}
		catch (Exception)
		{
		}
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtSerialBoard2 = new System.Windows.Forms.TextBox();
		this.TxtHDD2 = new System.Windows.Forms.TextBox();
		this.TxtMac2 = new System.Windows.Forms.TextBox();
		this.SerialNumber = new System.Windows.Forms.Label();
		this.TxtSerialBoard = new System.Windows.Forms.TextBox();
		this.TxtHDD = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtMac = new System.Windows.Forms.TextBox();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TxtSerialBoard2);
		this.groupBox1.Controls.Add(this.TxtHDD2);
		this.groupBox1.Controls.Add(this.TxtMac2);
		this.groupBox1.Controls.Add(this.SerialNumber);
		this.groupBox1.Controls.Add(this.TxtSerialBoard);
		this.groupBox1.Controls.Add(this.TxtHDD);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtMac);
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Location = new System.Drawing.Point(12, 11);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Size = new System.Drawing.Size(944, 617);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.TxtSerialBoard2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSerialBoard2.BackColor = System.Drawing.Color.White;
		this.TxtSerialBoard2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSerialBoard2.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtSerialBoard2.Location = new System.Drawing.Point(8, 583);
		this.TxtSerialBoard2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtSerialBoard2.Name = "TxtSerialBoard2";
		this.TxtSerialBoard2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSerialBoard2.Size = new System.Drawing.Size(358, 28);
		this.TxtSerialBoard2.TabIndex = 238;
		this.TxtSerialBoard2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtHDD2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHDD2.BackColor = System.Drawing.Color.White;
		this.TxtHDD2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtHDD2.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtHDD2.Location = new System.Drawing.Point(8, 511);
		this.TxtHDD2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHDD2.Name = "TxtHDD2";
		this.TxtHDD2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHDD2.Size = new System.Drawing.Size(358, 28);
		this.TxtHDD2.TabIndex = 236;
		this.TxtHDD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtMac2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMac2.BackColor = System.Drawing.Color.White;
		this.TxtMac2.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMac2.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtMac2.Location = new System.Drawing.Point(8, 547);
		this.TxtMac2.Margin = new System.Windows.Forms.Padding(4);
		this.TxtMac2.Name = "TxtMac2";
		this.TxtMac2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMac2.Size = new System.Drawing.Size(358, 28);
		this.TxtMac2.TabIndex = 237;
		this.TxtMac2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.SerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.SerialNumber.AutoSize = true;
		this.SerialNumber.Location = new System.Drawing.Point(865, 590);
		this.SerialNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.SerialNumber.Name = "SerialNumber";
		this.SerialNumber.Size = new System.Drawing.Size(68, 17);
		this.SerialNumber.TabIndex = 235;
		this.SerialNumber.Text = "SerialNum";
		this.TxtSerialBoard.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtSerialBoard.BackColor = System.Drawing.Color.White;
		this.TxtSerialBoard.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSerialBoard.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtSerialBoard.Location = new System.Drawing.Point(374, 583);
		this.TxtSerialBoard.Margin = new System.Windows.Forms.Padding(4);
		this.TxtSerialBoard.Name = "TxtSerialBoard";
		this.TxtSerialBoard.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtSerialBoard.Size = new System.Drawing.Size(483, 28);
		this.TxtSerialBoard.TabIndex = 234;
		this.TxtSerialBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtHDD.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtHDD.BackColor = System.Drawing.Color.White;
		this.TxtHDD.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtHDD.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtHDD.Location = new System.Drawing.Point(374, 511);
		this.TxtHDD.Margin = new System.Windows.Forms.Padding(4);
		this.TxtHDD.Name = "TxtHDD";
		this.TxtHDD.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtHDD.Size = new System.Drawing.Size(483, 28);
		this.TxtHDD.TabIndex = 230;
		this.TxtHDD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(865, 554);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(63, 17);
		this.label1.TabIndex = 232;
		this.label1.Text = "MACADD";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(865, 518);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 17);
		this.label2.TabIndex = 233;
		this.label2.Text = "HDD";
		this.TxtMac.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtMac.BackColor = System.Drawing.Color.White;
		this.TxtMac.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMac.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.TxtMac.Location = new System.Drawing.Point(374, 547);
		this.TxtMac.Margin = new System.Windows.Forms.Padding(4);
		this.TxtMac.Name = "TxtMac";
		this.TxtMac.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtMac.Size = new System.Drawing.Size(483, 28);
		this.TxtMac.TabIndex = 231;
		this.TxtMac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.ColumnAutoResize = true;
		this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
		this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
		this.gridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
		this.gridEX1.FilterRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
		this.gridEX1.FocusCellFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.FocusCellFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 10f);
		this.gridEX1.GroupByBoxFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxInfoFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.LinkFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(6, 21);
		this.gridEX1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.NewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.NewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Black;
		this.gridEX1.PreviewRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.PreviewRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.RowHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.SelectedInactiveFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Size = new System.Drawing.Size(932, 484);
		this.gridEX1.TabIndex = 12;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(968, 639);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "FrmSearch";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مستكشف الفيرجنات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


