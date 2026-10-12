using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.DAL.Models;
using GTSErpSystem.DataSetRep;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Items;

public class FrmDisCode : XtraForm
{
	private ApplicationDbContext context;

	private DiscountCodesManager DisCode;

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private ButtonEdit TxtCode;

	private UIGroupBox uiGroupBox1;

	private DateEdit TxtDate;

	private Label label3;

	private NumericEditBox TxtDiscount;

	private NumericEditBox Txtdicoutprege;

	private DateEdit TxtDateEnd;

	private Label label4;

	private RadioButton CheckAmount;

	private RadioButton CheckPer;

	private NumericEditBox TxtUseNum;

	private Label label5;

	private UIGroupBox uiGroupBox2;

	private Label label6;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private GridEX gridEX1;

	private Label ID;

	private SimpleButton BtnDownLode;

	public FrmDisCode()
	{
		InitializeComponent();
		Clear();
		context = new ApplicationDbContext();
		DisCode = new DiscountCodesManager(context);
		Bind();
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = LoginDetails.CompanyNameEN ?? "";
		string arg = ((text.Length >= 3) ? text.Substring(0, 3) : text);
		Random random = new Random();
		int num = random.Next(100000, 999999);
		TxtCode.Text = $"{arg}{num}";
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void gridEX1_MouseClick(object sender, MouseEventArgs e)
	{
		BindFromGrid();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Update();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnDownLode_Click(object sender, EventArgs e)
	{
		Print2();
	}

	private async void Bind()
	{
		try
		{
			waitForm.Show();
			await Task.Delay(100);
			gridEX1.ColumnAutoResize = true;
			DataTable myTable = new DataTable("gridEX1");
			myTable.Columns.Add("Id", typeof(int));
			myTable.Columns.Add("كود الخصم", typeof(string));
			myTable.Columns.Add("قيمه الخصم", typeof(decimal));
			myTable.Columns.Add("نسبه الخصم", typeof(decimal));
			myTable.Columns.Add("باقي مرات الاستخدام", typeof(decimal));
			myTable.Columns.Add("تاريخ انتهاءالاستخدام", typeof(string));
			myTable.Columns.Add("تاريخ الانشاء", typeof(string));
			foreach (DiscountCodes item in await DisCode.GetAllDisCodesAsync(LoginDetails.CompanyNameAR, LoginDetails.VatNum))
			{
				myTable.Rows.Add(item.Id, item.DisCode, item.DiscountValue, item.DiscountPercentage, item.NumberUsed, item.DistExpirationDate?.ToString("yyyy-MM-dd"), item.CreationDate?.ToString("yyyy-MM-dd"));
			}
			gridEX1.Invoke((MethodInvoker)delegate
			{
				gridEX1.SetDataBinding(myTable, "");
				gridEX1.RetrieveStructure();
				gridEX1.RootTable.Columns["كود الخصم"].RightToLeft = RightToLeft.No;
				gridEX1.RootTable.Columns["كود الخصم"].TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
				gridEX1.RootTable.Columns["قيمه الخصم"].TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
				gridEX1.RootTable.Columns["نسبه الخصم"].TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
				gridEX1.RootTable.Columns["باقي مرات الاستخدام"].TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
				gridEX1.RootTable.Columns["تاريخ انتهاءالاستخدام"].TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
				gridEX1.RootTable.Columns["Id"].Visible = false;
				gridEX1.RootTable.Columns["تاريخ الانشاء"].Visible = false;
				gridEX1.RootTable.Columns["قيمه الخصم"].FormatString = "0.00";
				gridEX1.RootTable.Columns["نسبه الخصم"].FormatString = "0.00";
				gridEX1.RootTable.Columns["باقي مرات الاستخدام"].FormatString = "0";
			});
		}
		finally
		{
			waitForm.Close();
		}
	}

	private async void Save()
	{
		try
		{
			if (string.IsNullOrEmpty(TxtCode.Text))
			{
				XtraMessageBox.Show("أدخل كود الخصم ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Convert.ToDecimal(TxtUseNum.Text) < 1m)
			{
				XtraMessageBox.Show("يجب تحديد مده استخدام الكود ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Convert.ToDecimal(TxtDiscount.Text) < 1m && Convert.ToDecimal(Txtdicoutprege.Text) < 1m)
			{
				XtraMessageBox.Show("يجب تحديد قيمه او نسبه الخصم ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (await DisCode.IsDisCodeExistsAsync(TxtCode.Text, LoginDetails.CompanyNameAR, LoginDetails.VatNum))
			{
				XtraMessageBox.Show("هذا الكود موجود بالفعل يرجي ادخال كود مختلف ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			waitForm.Show();
			DiscountCodes newDiscount = await CreateDiscountCode();
			await DisCode.AddDisCodeAsync(newDiscount);
			waitForm.Close();
			Clear();
			Bind();
			ID.Text = newDiscount.Id.ToString();
			XtraMessageBox.Show("تم حفظ كود الخصم بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			XtraMessageBox.Show("حدث خطأ أثناء الحفظ: " + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private new async void Update()
	{
		try
		{
			if (string.IsNullOrEmpty(TxtCode.Text))
			{
				XtraMessageBox.Show("أدخل كود الخصم ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Convert.ToDecimal(TxtUseNum.Text) < 1m)
			{
				XtraMessageBox.Show("يجب تحديد مده استخدام الكود ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Convert.ToDecimal(TxtDiscount.Text) < 1m && Convert.ToDecimal(Txtdicoutprege.Text) < 1m)
			{
				XtraMessageBox.Show("يجب تحديد قيمه او نسبه الخصم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			DiscountCodes newDiscount = await CreateDiscountCodeUpdate();
			if (MessageBox.Show("هل أنت متأكد من تعديل كود الخصم؟", "تأكيد التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				waitForm.Show();
				await DisCode.UpdateDisCodeAsync(newDiscount);
				waitForm.Close();
				Clear();
				Bind();
				XtraMessageBox.Show("تم تعديل كود الخصم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			XtraMessageBox.Show("حدث خطأ أثناء التعديل: " + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private async void Delete()
	{
		try
		{
			if (MessageBox.Show("هل أنت متأكد من حذف كود الخصم؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				waitForm.Show();
				await DisCode.DeleteDisCodeAsync(Convert.ToInt32(ID.Text));
				waitForm.Close();
				Clear();
				Bind();
				XtraMessageBox.Show("تم حذف كود الخصم بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			XtraMessageBox.Show("حدث خطأ أثناء الحذف: " + ex2.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Print()
	{
		if (TxtCode.Text != string.Empty)
		{
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConCloud);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["DisCode"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_DisCode", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(ID.Text));
			sqlDataAdapter.Fill(dataSetALL.DisCode);
			sqlConnection.Close();
			if (dataSetALL.Tables["DisCode"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				stiReport.Load(Application.StartupPath + "\\Reports\\DisCode.mrt");
				stiReport.RegData(dataSetALL.DisCode);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["EncHash"] = TxtCode.Text;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، اختر الكود ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Print2()
	{
		if (TxtCode.Text != string.Empty)
		{
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConCloud);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["DisCode"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_DisCode", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(ID.Text));
			sqlDataAdapter.Fill(dataSetALL.DisCode);
			sqlConnection.Close();
			if (dataSetALL.Tables["DisCode"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				stiReport.Load(Application.StartupPath + "\\Reports\\DisCode.mrt");
				stiReport.RegData(dataSetALL.DisCode);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["EncHash"] = TxtCode.Text;
				stiReport.Render();
				using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.Description = "اختر مكان حفظ الصورة";
				folderBrowserDialog.ShowNewFolderButton = true;
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					string selectedPath = folderBrowserDialog.SelectedPath;
					string text = DateTime.Now.ToString("yyyyMMdd_HHmmss");
					string file = Path.Combine(selectedPath, "ReportImage_" + text + ".jpg");
					stiReport.ExportDocument(StiExportFormat.ImageJpeg, file);
					MessageBox.Show("تم حفظ الصورة بنجاح", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				return;
			}
			MessageBox.Show("لا يوجد سجلات متاحة للتنزيل", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			MessageBox.Show("عفوا، اختر الكود ثم حاول مرة اخرى", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Clear()
	{
		TxtDate.DateTime = DateTime.Now;
		TxtDate.ReadOnly = true;
		TxtDate.BackColor = Color.White;
		TxtDateEnd.DateTime = DateTime.Now;
		TxtCode.Text = string.Empty;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnDownLode.Enabled = false;
		BtnPrint.Enabled = false;
		BtnSave.Enabled = true;
		TxtDiscount.Text = "0.00";
		Txtdicoutprege.Text = "0.00";
		CheckPer.Checked = false;
		Txtdicoutprege.ReadOnly = true;
		CheckAmount.Checked = true;
		TxtDiscount.ReadOnly = false;
		TxtUseNum.Value = 1;
		TxtUseNum.FormatString = "0";
	}

	private async Task<DiscountCodes> CreateDiscountCode()
	{
		int lastId = await DisCode.GetLastDisCodeIdAsync();
		decimal discountValue;
		decimal discountPercentage;
		decimal UseNum;
		return new DiscountCodes
		{
			Id = lastId + 1,
			DisCode = TxtCode.Text.Trim(),
			DiscountValue = (decimal.TryParse(TxtDiscount.Text, out discountValue) ? discountValue : 0m),
			DiscountPercentage = (decimal.TryParse(Txtdicoutprege.Text, out discountPercentage) ? discountPercentage : 0m),
			NumberUsed = (decimal.TryParse(TxtUseNum.Text, out UseNum) ? UseNum : 0m),
			DistExpirationDate = TxtDateEnd.DateTime,
			CreationDate = TxtDate.DateTime,
			CompanyName = LoginDetails.CompanyNameAR,
			CompanyVatNum = LoginDetails.VatNum,
			UserID_Add = LoginDetails.UserID,
			UserBranch_Add = LoginDetails.BranchID,
			UserMacAddress_Add = LoginDetails.macAddress,
			UserDate_Add = DateTime.Now
		};
	}

	private async Task<DiscountCodes> CreateDiscountCodeUpdate()
	{
		decimal discountValue;
		decimal discountPercentage;
		decimal UseNum;
		return new DiscountCodes
		{
			Id = Convert.ToInt32(ID.Text),
			DisCode = TxtCode.Text.Trim(),
			DiscountValue = (decimal.TryParse(TxtDiscount.Text, out discountValue) ? discountValue : 0m),
			DiscountPercentage = (decimal.TryParse(Txtdicoutprege.Text, out discountPercentage) ? discountPercentage : 0m),
			NumberUsed = (decimal.TryParse(TxtUseNum.Text, out UseNum) ? UseNum : 0m),
			DistExpirationDate = TxtDateEnd.DateTime,
			CreationDate = TxtDate.DateTime,
			CompanyName = LoginDetails.CompanyNameAR,
			CompanyVatNum = LoginDetails.VatNum,
			UserID_Update = LoginDetails.UserID,
			UserBranch_Update = LoginDetails.BranchID,
			UserMacAddress_Update = LoginDetails.macAddress,
			UserDate_Update = DateTime.Now
		};
	}

	private void BindFromGrid()
	{
		if (gridEX1.CurrentRow != null && gridEX1.CurrentRow.RowType == RowType.Record)
		{
			GridEXRow currentRow = gridEX1.CurrentRow;
			ID.Text = currentRow.Cells["Id"].Value?.ToString();
			TxtCode.Text = currentRow.Cells["كود الخصم"].Value?.ToString();
			TxtDiscount.Text = currentRow.Cells["قيمه الخصم"].Value?.ToString();
			Txtdicoutprege.Text = currentRow.Cells["نسبه الخصم"].Value?.ToString();
			TxtUseNum.Text = currentRow.Cells["باقي مرات الاستخدام"].Value?.ToString();
			TxtDateEnd.Text = currentRow.Cells["تاريخ انتهاءالاستخدام"].Value?.ToString();
			TxtDate.Text = currentRow.Cells["تاريخ الانشاء"].Value?.ToString();
		}
		if (Convert.ToDecimal(TxtDiscount.Text) > 0m)
		{
			CheckAmount.Checked = true;
		}
		if (Convert.ToDecimal(Txtdicoutprege.Text) > 0m)
		{
			CheckPer.Checked = true;
		}
		BtnSave.Enabled = false;
		BtnEdit.Enabled = true;
		BtnDelete.Enabled = true;
		BtnDownLode.Enabled = true;
		BtnPrint.Enabled = true;
	}

	private void CheckPer_CheckedChanged(object sender, EventArgs e)
	{
		if (CheckPer.Checked)
		{
			Txtdicoutprege.ReadOnly = false;
			TxtDiscount.ReadOnly = true;
			TxtDiscount.Text = "0.00";
		}
	}

	private void CheckAmount_CheckedChanged(object sender, EventArgs e)
	{
		if (CheckAmount.Checked)
		{
			TxtDiscount.ReadOnly = false;
			Txtdicoutprege.ReadOnly = true;
			Txtdicoutprege.Text = "0.00";
		}
	}

	private void Txtdicoutprege_KeyUp(object sender, KeyEventArgs e)
	{
		if (!decimal.TryParse(Txtdicoutprege.Text, out var result) || result > 100m)
		{
			Txtdicoutprege.Text = "0.00";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmDisCode));
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label3 = new System.Windows.Forms.Label();
		this.TxtDiscount = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.Txtdicoutprege = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtDateEnd = new DevExpress.XtraEditors.DateEdit();
		this.label4 = new System.Windows.Forms.Label();
		this.CheckAmount = new System.Windows.Forms.RadioButton();
		this.CheckPer = new System.Windows.Forms.RadioButton();
		this.TxtUseNum = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label5 = new System.Windows.Forms.Label();
		this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.gridEX1 = new Janus.Windows.GridEX.GridEX();
		this.ID = new System.Windows.Forms.Label();
		this.BtnDownLode = new DevExpress.XtraEditors.SimpleButton();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox1).BeginInit();
		this.uiGroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox2).BeginInit();
		this.uiGroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 13.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Snow;
		this.label1.Location = new System.Drawing.Point(648, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(78, 28);
		this.label1.TabIndex = 1;
		this.label1.Text = "الإضافه";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(8, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(50, 21);
		this.label2.TabIndex = 1;
		this.label2.Text = "الكود";
		this.TxtCode.Location = new System.Drawing.Point(12, 87);
		this.TxtCode.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.TxtCode.Size = new System.Drawing.Size(285, 30);
		this.TxtCode.TabIndex = 12;
		this.TxtCode.Tag = "NEW2025";
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.uiGroupBox1.BackColor = System.Drawing.Color.SteelBlue;
		this.uiGroupBox1.Controls.Add(this.label1);
		this.uiGroupBox1.Location = new System.Drawing.Point(12, 12);
		this.uiGroupBox1.Name = "uiGroupBox1";
		this.uiGroupBox1.Size = new System.Drawing.Size(758, 35);
		this.uiGroupBox1.TabIndex = 13;
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(306, 88);
		this.TxtDate.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.DisplayFormat.FormatString = "";
		this.TxtDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.EditFormat.FormatString = "";
		this.TxtDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.Size = new System.Drawing.Size(224, 30);
		this.TxtDate.TabIndex = 14;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(302, 59);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(61, 21);
		this.label3.TabIndex = 15;
		this.label3.Text = "التاريخ";
		this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtDiscount.DecimalDigits = 3;
		this.TxtDiscount.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDiscount.ForeColor = System.Drawing.Color.SteelBlue;
		this.TxtDiscount.Location = new System.Drawing.Point(12, 154);
		this.TxtDiscount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDiscount.Name = "TxtDiscount";
		this.TxtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtDiscount.Size = new System.Drawing.Size(757, 32);
		this.TxtDiscount.TabIndex = 204;
		this.TxtDiscount.Text = "0.000";
		this.TxtDiscount.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtDiscount.Value = new decimal(new int[4]);
		this.TxtDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.Txtdicoutprege.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Txtdicoutprege.DecimalDigits = 3;
		this.Txtdicoutprege.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Txtdicoutprege.ForeColor = System.Drawing.Color.SteelBlue;
		this.Txtdicoutprege.Location = new System.Drawing.Point(13, 226);
		this.Txtdicoutprege.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtdicoutprege.Name = "Txtdicoutprege";
		this.Txtdicoutprege.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Txtdicoutprege.Size = new System.Drawing.Size(757, 32);
		this.Txtdicoutprege.TabIndex = 205;
		this.Txtdicoutprege.Text = "0.000";
		this.Txtdicoutprege.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.Txtdicoutprege.Value = new decimal(new int[4]);
		this.Txtdicoutprege.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.Txtdicoutprege.KeyUp += new System.Windows.Forms.KeyEventHandler(Txtdicoutprege_KeyUp);
		this.TxtDateEnd.EditValue = null;
		this.TxtDateEnd.Location = new System.Drawing.Point(538, 87);
		this.TxtDateEnd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtDateEnd.Name = "TxtDateEnd";
		this.TxtDateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtDateEnd.Properties.Appearance.Options.UseFont = true;
		this.TxtDateEnd.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDateEnd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateEnd.Properties.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDateEnd.Properties.DisplayFormat.FormatString = "";
		this.TxtDateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateEnd.Properties.EditFormat.FormatString = "";
		this.TxtDateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
		this.TxtDateEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDateEnd.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDateEnd.Size = new System.Drawing.Size(232, 30);
		this.TxtDateEnd.TabIndex = 206;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(534, 59);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(154, 21);
		this.label4.TabIndex = 207;
		this.label4.Text = "تاريخ انتهاء الخصم";
		this.CheckAmount.AutoSize = true;
		this.CheckAmount.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CheckAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.CheckAmount.Location = new System.Drawing.Point(12, 124);
		this.CheckAmount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CheckAmount.Name = "CheckAmount";
		this.CheckAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CheckAmount.Size = new System.Drawing.Size(120, 25);
		this.CheckAmount.TabIndex = 208;
		this.CheckAmount.Text = "تفعيل القيمه";
		this.CheckAmount.UseVisualStyleBackColor = true;
		this.CheckAmount.CheckedChanged += new System.EventHandler(CheckAmount_CheckedChanged);
		this.CheckPer.AutoSize = true;
		this.CheckPer.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CheckPer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.CheckPer.Location = new System.Drawing.Point(13, 194);
		this.CheckPer.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.CheckPer.Name = "CheckPer";
		this.CheckPer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CheckPer.Size = new System.Drawing.Size(125, 25);
		this.CheckPer.TabIndex = 209;
		this.CheckPer.Text = "تفعيل النسبه";
		this.CheckPer.UseVisualStyleBackColor = true;
		this.CheckPer.CheckedChanged += new System.EventHandler(CheckPer_CheckedChanged);
		this.TxtUseNum.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtUseNum.DecimalDigits = 3;
		this.TxtUseNum.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtUseNum.ForeColor = System.Drawing.Color.SteelBlue;
		this.TxtUseNum.Location = new System.Drawing.Point(13, 295);
		this.TxtUseNum.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtUseNum.Name = "TxtUseNum";
		this.TxtUseNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtUseNum.Size = new System.Drawing.Size(757, 32);
		this.TxtUseNum.TabIndex = 210;
		this.TxtUseNum.Text = "0.000";
		this.TxtUseNum.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtUseNum.Value = new decimal(new int[4]);
		this.TxtUseNum.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(12, 265);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(148, 18);
		this.label5.TabIndex = 211;
		this.label5.Text = "عدد مرات الاستخدام";
		this.uiGroupBox2.BackColor = System.Drawing.Color.SteelBlue;
		this.uiGroupBox2.Controls.Add(this.label6);
		this.uiGroupBox2.Location = new System.Drawing.Point(13, 398);
		this.uiGroupBox2.Name = "uiGroupBox2";
		this.uiGroupBox2.Size = new System.Drawing.Size(756, 35);
		this.uiGroupBox2.TabIndex = 212;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.Snow;
		this.label6.Location = new System.Drawing.Point(618, 3);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(108, 24);
		this.label6.TabIndex = 1;
		this.label6.Text = "أكواد الخصم";
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(265, 340);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(5);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(124, 39);
		this.BtnEdit.TabIndex = 214;
		this.BtnEdit.Text = "تعديل";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(138, 340);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(124, 39);
		this.BtnSave.TabIndex = 213;
		this.BtnSave.Text = "حفظ";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnPrint.Appearance.Options.UseFont = true;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(519, 340);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(5);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(124, 39);
		this.BtnPrint.TabIndex = 217;
		this.BtnPrint.Text = "طباعه";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(12, 340);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(5);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(124, 39);
		this.BtnNew.TabIndex = 216;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9f);
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(392, 340);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(124, 39);
		this.BtnDelete.TabIndex = 215;
		this.BtnDelete.Text = "حذف";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.gridEX1.AllowCardSizing = false;
		this.gridEX1.AllowColumnDrag = false;
		this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
		this.gridEX1.AlternatingColors = true;
		this.gridEX1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gridEX1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.gridEX1.GroupByBoxVisible = false;
		this.gridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle;
		this.gridEX1.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.gridEX1.Location = new System.Drawing.Point(15, 439);
		this.gridEX1.Name = "gridEX1";
		this.gridEX1.Size = new System.Drawing.Size(755, 249);
		this.gridEX1.TabIndex = 218;
		this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2010;
		this.gridEX1.VisualStyleAreas.HeadersStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
		this.gridEX1.MouseClick += new System.Windows.Forms.MouseEventHandler(gridEX1_MouseClick);
		this.ID.AutoSize = true;
		this.ID.Location = new System.Drawing.Point(400, 130);
		this.ID.Name = "ID";
		this.ID.Size = new System.Drawing.Size(14, 16);
		this.ID.TabIndex = 219;
		this.ID.Text = "0";
		this.ID.Visible = false;
		this.BtnDownLode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BtnDownLode.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnDownLode.Appearance.Options.UseFont = true;
		this.BtnDownLode.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDownLode.ImageOptions.Image");
		this.BtnDownLode.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDownLode.Location = new System.Drawing.Point(645, 340);
		this.BtnDownLode.Margin = new System.Windows.Forms.Padding(5);
		this.BtnDownLode.Name = "BtnDownLode";
		this.BtnDownLode.Size = new System.Drawing.Size(124, 39);
		this.BtnDownLode.TabIndex = 220;
		this.BtnDownLode.Text = "تنزيل";
		this.BtnDownLode.Click += new System.EventHandler(BtnDownLode_Click);
		base.Appearance.BackColor = System.Drawing.Color.White;
		base.Appearance.Options.UseBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(773, 700);
		base.Controls.Add(this.BtnDownLode);
		base.Controls.Add(this.ID);
		base.Controls.Add(this.gridEX1);
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnPrint);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.uiGroupBox2);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.TxtUseNum);
		base.Controls.Add(this.CheckPer);
		base.Controls.Add(this.CheckAmount);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.TxtDateEnd);
		base.Controls.Add(this.Txtdicoutprege);
		base.Controls.Add(this.TxtDiscount);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.TxtDate);
		base.Controls.Add(this.uiGroupBox1);
		base.Controls.Add(this.TxtCode);
		base.Controls.Add(this.label2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmDisCode";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "كود الخصم";
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox1).EndInit();
		this.uiGroupBox1.ResumeLayout(false);
		this.uiGroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDateEnd.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox2).EndInit();
		this.uiGroupBox2.ResumeLayout(false);
		this.uiGroupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gridEX1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


