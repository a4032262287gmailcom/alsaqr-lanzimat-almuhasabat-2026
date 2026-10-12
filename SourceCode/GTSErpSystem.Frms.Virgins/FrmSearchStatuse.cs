using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Janus.Windows.GridEX;

namespace GTSErpSystem.Frms.Virgins;

public class FrmSearchStatuse : XtraForm
{
	private GTSdbEntities DB = new GTSdbEntities();

	private DataTable myTable = new DataTable("gridEX1");

	public int CustID;

	public int SN;

	private IContainer components = null;

	private GroupBox groupBox1;

	public GridEX gridEX1;

	private Label label5;

	private System.Windows.Forms.ComboBox CbAppType;

	private SimpleButton BtnTest;

	public FrmSearchStatuse()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ar-EG");
		InitializeComponent();
		CbAppType.DataSource = DB.Virg_Statuse.ToList();
		CbAppType.DisplayMember = "Name";
		CbAppType.ValueMember = "ID";
	}

	private void BindReport()
	{
		DataTable dataTable = new DataTable();
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
		SqlCommand sqlCommand = new SqlCommand("Get_virginByStatuse", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@StatuseID", Convert.ToInt32(CbAppType.SelectedValue));
		if (LoginDetails.Active_AdminPermission)
		{
			sqlCommand.Parameters.AddWithValue("@FranchiseID", 0);
		}
		else
		{
			sqlCommand.Parameters.AddWithValue("@FranchiseID", LoginDetails.Active_FranchiseID);
		}
		sqlDataAdapter.Fill(dataTable);
		sqlConnection.Close();
		gridEX1.SetDataBinding(dataTable, "");
		gridEX1.RetrieveStructure();
		if (gridEX1.RootTable.Columns.Contains("TypeName"))
		{
			gridEX1.RootTable.Columns["TypeName"].Visible = false;
		}
		if (gridEX1.RootTable.Columns.Contains("DeviceName"))
		{
			gridEX1.RootTable.Columns["DeviceName"].Visible = false;
		}
		if (gridEX1.RootTable.Columns.Contains("CompName"))
		{
			gridEX1.RootTable.Columns["CompName"].Visible = false;
		}
		if (!gridEX1.RootTable.Columns.Contains("رفض"))
		{
			GridEXColumn gridEXColumn = gridEX1.RootTable.Columns.Add("رفض");
			gridEXColumn.Caption = "رفض";
			gridEXColumn.ButtonDisplayMode = CellButtonDisplayMode.Always;
			gridEXColumn.ButtonText = "â‌Œ";
			gridEXColumn.Width = 30;
			gridEXColumn.TextAlignment = TextAlignment.Center;
			gridEXColumn.CellStyle.TextAlignment = TextAlignment.Center;
			gridEXColumn.ButtonStyle = ButtonStyle.TextButton;
			gridEXColumn.EditType = EditType.NoEdit;
			GridEXFormatStyle gridEXFormatStyle = new GridEXFormatStyle();
			gridEXFormatStyle.Font = new Font("Segoe UI Symbol", 9f, FontStyle.Bold);
			gridEXFormatStyle.ForeColor = Color.White;
			gridEXFormatStyle.TextAlignment = TextAlignment.Center;
			gridEXColumn.CellStyle = gridEXFormatStyle;
			gridEX1.RootTable.RowHeight = 30;
		}
		foreach (GridEXColumn column in gridEX1.RootTable.Columns)
		{
			if (column.Key == "CustName")
			{
				column.Width *= 3;
			}
			else if (column.Key == "SN")
			{
				column.Width = Math.Max(60, column.Width / 2);
			}
			else if (column.Key == "VatNum" || column.Key == "CommercialRecord" || column.Key == "Phone" || column.Key == "Address")
			{
				column.Width = (int)((double)column.Width * 0.4);
			}
			else
			{
				column.Width = (int)((double)column.Width * 0.85);
			}
		}
		gridEX1.AllowEdit = InheritableBoolean.False;
		gridEX1.ColumnAutoResize = true;
		gridEX1.ColumnAutoSizeMode = ColumnAutoSizeMode.Default;
		gridEX1.ColumnButtonClick -= gridEX1_ColumnButtonClick;
		gridEX1.ColumnButtonClick += gridEX1_ColumnButtonClick;
	}

	private void gridEX1_ColumnButtonClick(object sender, ColumnActionEventArgs e)
	{
		if (!(e.Column.Key == "رفض"))
		{
			return;
		}
		GridEX gridEX = (GridEX)sender;
		GridEXRow currentRow = gridEX.CurrentRow;
		if (currentRow == null || currentRow.RowType != RowType.Record)
		{
			return;
		}
		try
		{
			int num = Convert.ToInt32(currentRow.Cells["SN"].Value);
			DialogResult dialogResult = MessageBox.Show($"هل تريد رفض الطلب رقم {num}طں", "تأكيد الرفض", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult != DialogResult.Yes)
			{
				return;
			}
			using (SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString))
			{
				using SqlCommand sqlCommand = new SqlCommand("UPDATE Virg_MasterDetails SET StatuseID = 3 WHERE SN = @SN", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@SN", num);
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
			}
			MessageBox.Show("تم رفض الطلب بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			BindReport();
		}
		catch (Exception ex)
		{
			MessageBox.Show("حدث خطأ أثناء تنفيذ عملية الرفض:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			int position = gridEX1.SelectedItems[0].Position;
			CustID = Convert.ToInt32(gridEX1.GetRow(position).Cells[1].Text);
			SN = Convert.ToInt32(gridEX1.GetRow(position).Cells[0].Text);
			Close();
		}
		catch (Exception)
		{
		}
	}

	private void BtnTest_Click(object sender, EventArgs e)
	{
		BindReport();
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
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.label5 = new System.Windows.Forms.Label();
		this.CbAppType = new System.Windows.Forms.ComboBox();
		this.BtnTest = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.BtnTest);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.CbAppType);
		this.groupBox1.Controls.Add(this.gridEX1);
		this.groupBox1.Location = new System.Drawing.Point(12, 11);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.groupBox1.Size = new System.Drawing.Size(944, 617);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
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
		this.gridEX1.Location = new System.Drawing.Point(6, 57);
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
		this.gridEX1.Size = new System.Drawing.Size(932, 556);
		this.gridEX1.TabIndex = 12;
		this.gridEX1.TableHeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(863, 25);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label5.Size = new System.Drawing.Size(73, 17);
		this.label5.TabIndex = 265;
		this.label5.Text = "نوع البرنامج";
		this.CbAppType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CbAppType.BackColor = System.Drawing.Color.White;
		this.CbAppType.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
		this.CbAppType.FormattingEnabled = true;
		this.CbAppType.Location = new System.Drawing.Point(518, 22);
		this.CbAppType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CbAppType.Name = "CbAppType";
		this.CbAppType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CbAppType.Size = new System.Drawing.Size(337, 24);
		this.CbAppType.TabIndex = 264;
		this.BtnTest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnTest.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnTest.Location = new System.Drawing.Point(289, 17);
		this.BtnTest.Margin = new System.Windows.Forms.Padding(4);
		this.BtnTest.Name = "BtnTest";
		this.BtnTest.Size = new System.Drawing.Size(195, 29);
		this.BtnTest.TabIndex = 266;
		this.BtnTest.Text = "بحث";
		this.BtnTest.Click += new System.EventHandler(BtnTest_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(968, 639);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		base.Name = "FrmSearchStatuse";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "مستكشف انواع البرامج";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
	}
}


