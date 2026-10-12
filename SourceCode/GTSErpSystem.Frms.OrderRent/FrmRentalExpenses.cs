using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.OrderRent;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.OrderRent.Search;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.OrderRent;

public class FrmRentalExpenses : XtraForm
{
	private DataTable datatable = new DataTable();

	private RentalExpen Exp = new RentalExpen();

	private Class_Items ITM = new Class_Items();

	private string fileNameAR = Application.StartupPath + "\\Layout\\RentalExpensesAr.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\RentalExpensesEN.xml";

	private IContainer components = null;

	private ButtonEdit TxtCode;

	private Label label1;

	private DateEdit TxtDate;

	private Label label2;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn9;

	private GridColumn gridColumn2;

	private RepositoryItemButtonEdit repositoryItemButtonBankBox;

	private GridColumn gridColumn1;

	private RepositoryItemDateEdit repositoryItemDateEdit2;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn4;

	private GridColumn gridColumn5;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private RepositoryItemButtonEdit repositoryItemButtonEditCodeSearch;

	private RepositoryItemComboBox repositoryItemComboBox1;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit1;

	private CardView cardView1;

	private GridColumn gridColumn10;

	private GridColumn gridColumn11;

	private GridColumn gridColumn12;

	private GridColumn gridColumn13;

	private GridColumn gridColumn14;

	private GridColumn gridColumn15;

	private GridColumn gridColumn16;

	private GridColumn gridColumn17;

	private GridColumn gridColumn18;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private SimpleButton BtnNew;

	private SimpleButton BtnSave;

	private SimpleButton BtnPrint;

	private SimpleButton BtnExit;

	private SimpleButton BtnEdit;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private GridColumn gridColumn7;

	private NumericEditBox TxtTotals;

	private NumericEditBox Txtvat;

	private NumericEditBox TxtNet;

	private Label label3;

	private Label label4;

	private SimpleButton BtnDelete;

	private GridColumn gridColumn19;

	public FrmRentalExpenses()
	{
		InitializeComponent();
		SetInitialRow();
		Clear();
	}

	private void gridView1_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
	{
		e.ExceptionMode = ExceptionMode.NoAction;
		gridControl1.DataSource = datatable;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
	}

	private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
	{
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		if (e.Column.FieldName == "ItemCode")
		{
			string code = Convert.ToString(e.Value);
			Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
			if (get_Item_ByCodeALL_Result != null)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["SN"] = 1;
				dataRow["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
				dataRow["ItemId"] = get_Item_ByCodeALL_Result.ItemId;
				dataRow["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
				dataRow["IsVat"] = true;
				dataRow["Total"] = 0.0;
				dataRow["Itemvat"] = 0.0;
				dataRow["Net"] = 0.0;
				CheckFfistRow();
				datatable.Rows.Add(dataRow);
				insertnewrow();
				gridView1.FocusedRowHandle = e.RowHandle;
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
			}
		}
		else if (e.Column.FieldName == "Total")
		{
			string text = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("SN");
			foreach (DataRow row in datatable.Rows)
			{
				if (row["SN"].ToString() == focusedRowCellDisplayText)
				{
					if (Convert.ToBoolean(row["IsVat"]))
					{
						decimal num = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m, 2);
						row["ItemVat"] = Math.Round(Convert.ToDecimal(row["Total"]) * num, 2);
						row["Net"] = Math.Round(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["ItemVat"]), 2);
					}
					else
					{
						row["ItemVat"] = 0;
						row["Net"] = Math.Round(Convert.ToDecimal(row["Total"]), 2);
					}
				}
			}
			CheckFfistRow();
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			GetTotals();
		}
		else
		{
			if (!(e.Column.FieldName == "Net"))
			{
				return;
			}
			string text2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("SN");
			foreach (DataRow row2 in datatable.Rows)
			{
				if (Convert.ToBoolean(row2["IsVat"]))
				{
					decimal num2 = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m + 1m, 2);
					row2["Total"] = Math.Round(Convert.ToDecimal(row2["Net"]) / num2, 2);
					row2["ItemVat"] = Math.Round(Convert.ToDecimal(row2["Net"]) - Convert.ToDecimal(row2["Total"]), 2);
				}
				else
				{
					row2["ItemVat"] = 0;
					row2["Net"] = Math.Round(Convert.ToDecimal(row2["Total"]), 2);
				}
			}
			CheckFfistRow();
			insertnewrow();
			gridView1.FocusedRowHandle = e.RowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			GetTotals();
		}
		ArrangeDatatable();
	}

	private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
	{
		if (!(e.Column.FieldName == "IsVat"))
		{
			return;
		}
		string text = Convert.ToString(e.Value);
		string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("SN");
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == focusedRowCellDisplayText)
			{
				if (Convert.ToBoolean(row["IsVat"]))
				{
					row["IsVat"] = false;
				}
				else
				{
					row["IsVat"] = true;
				}
				if (row["IsVat"] != DBNull.Value && Convert.ToBoolean(row["IsVat"]))
				{
					decimal num = Math.Round(Convert.ToDecimal(LoginDetails.PerVat) / 100m, 2);
					row["ItemVat"] = Math.Round(Convert.ToDecimal(row["Total"]) * num, 2);
					row["Net"] = Math.Round(Convert.ToDecimal(row["Total"]) + Convert.ToDecimal(row["ItemVat"]), 2);
				}
				else
				{
					row["ItemVat"] = 0;
					row["Net"] = Math.Round(Convert.ToDecimal(row["Total"]), 2);
				}
				break;
			}
		}
		GetTotals();
		CheckFfistRow();
		insertnewrow();
		gridView1.FocusedRowHandle = e.RowHandle;
		gridView1.FocusedColumn = gridView1.VisibleColumns[3];
		gridView1.ShowEditor();
	}

	private void gridView1_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
	{
		int num = e.Bounds.Height;
		Brush gradientBrush = e.Cache.GetGradientBrush(e.Bounds, Color.LightGreen, Color.FloralWhite, LinearGradientMode.Vertical);
		Rectangle bounds = e.Bounds;
		BorderPainter borderPainter = BorderHelper.GetPainter(BorderStyles.Style3D);
		AppearanceObject appearanceObject = new AppearanceObject(e.Appearance);
		appearanceObject.BorderColor = Color.DarkGray;
		appearanceObject.ParentAppearance.Options.UseTextOptions = true;
		appearanceObject.ParentAppearance.TextOptions.HAlignment = HorzAlignment.Center;
		appearanceObject.ParentAppearance.Options.UseFont = true;
		appearanceObject.ParentAppearance.ForeColor = Color.Red;
		borderPainter.DrawObject(new BorderObjectInfoArgs(e.Cache, appearanceObject, bounds));
		bounds.Inflate(-1, -1);
		e.Cache.FillRectangle(gradientBrush, bounds);
		bounds.Inflate(-2, 0);
		e.Appearance.DrawString(e.Cache, e.Info.DisplayText, bounds);
		e.Handled = true;
	}

	private void gridControl_ProcessGridKey(object sender, KeyEventArgs e)
	{
		GridControl gridControl = sender as GridControl;
		GridView gridView = gridControl.FocusedView as GridView;
		if (e.KeyData == Keys.Delete)
		{
			gridView.DeleteSelectedRows();
			e.Handled = true;
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchRentalExpenses frmSearchRentalExpenses = new FrmSearchRentalExpenses();
			frmSearchRentalExpenses.ShowDialog();
			if (frmSearchRentalExpenses.CustCode <= 0)
			{
				return;
			}
			int custCode = frmSearchRentalExpenses.CustCode;
			RentalExpens rentalExpens = Exp.GetPurchasesExp(custCode).FirstOrDefault();
			TxtCode.Text = rentalExpens.Id.ToString();
			TxtDate.DateTime = Convert.ToDateTime(rentalExpens.Date);
			TxtTotals.Text = rentalExpens.TotalPrice.ToString();
			Txtvat.Text = rentalExpens.AllTax.ToString();
			TxtNet.Text = rentalExpens.NetTotalPrice.ToString();
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
			BtnPrint.Enabled = true;
			TxtDate.ReadOnly = true;
			gridView1.OptionsBehavior.Editable = false;
			datatable.Rows.Clear();
			List<RentalExpensesDetail> purchasesDetails = Exp.GetPurchasesDetails(custCode);
			int num = 1;
			foreach (RentalExpensesDetail item in purchasesDetails)
			{
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["ItemId"] = Convert.ToInt32(item.ItemId);
				dataRow["ItemCode"] = item.ItemCode;
				dataRow["ItemName"] = item.ItemName;
				dataRow["Note"] = item.Note;
				dataRow["IsVat"] = Convert.ToBoolean(item.IsVat);
				dataRow["ItemVat"] = Convert.ToDecimal(item.Vat);
				dataRow["Total"] = Convert.ToDecimal(item.Total);
				dataRow["Net"] = Convert.ToDecimal(item.Net);
				datatable.Rows.Add(dataRow);
				num++;
			}
			gridControl1.DataSource = datatable;
			ArrangeDatatable();
		}
		catch (Exception)
		{
			Clear();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		Clear();
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void BtnEdit_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		Print();
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("ItemId", typeof(int));
		datatable.Columns.Add("ItemName", typeof(string));
		datatable.Columns.Add("ItemCode", typeof(string));
		datatable.Columns.Add("Total", typeof(decimal));
		datatable.Columns.Add("Note", typeof(string));
		datatable.Columns.Add("IsVat", typeof(bool));
		datatable.Columns.Add("ItemVat", typeof(decimal));
		datatable.Columns.Add("Net", typeof(decimal));
		TxtDate.DateTime = DateTime.Now;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
		gridView1.InvalidRowException += gridView1_InvalidRowException;
		gridView1.CustomDrawFooterCell += gridView1_CustomDrawFooterCell;
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["SN"] = 1;
		dataRow["ItemId"] = 0;
		dataRow["ItemCode"] = string.Empty;
		dataRow["ItemName"] = string.Empty;
		dataRow["Note"] = string.Empty;
		dataRow["Total"] = 0.0;
		dataRow["ItemVat"] = 0.0;
		dataRow["IsVat"] = true;
		dataRow["Net"] = 0.0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	public void CheckFfistRow()
	{
		if (datatable.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["ItemId"]);
			if (text == "0")
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["SN"] = num;
			num++;
		}
	}

	public void GetTotals()
	{
		decimal num = Convert.ToDecimal(datatable.Compute("SUM(Total)", string.Empty));
		decimal num2 = Convert.ToDecimal(datatable.Compute("SUM(Itemvat)", string.Empty));
		decimal num3 = Convert.ToDecimal(datatable.Compute("SUM(Net)", string.Empty));
		TxtTotals.Text = num.ToString();
		Txtvat.Text = num2.ToString();
		TxtNet.Text = num3.ToString();
	}

	public void Clear()
	{
		gridView1.OptionsBehavior.Editable = true;
		datatable.Rows.Clear();
		gridControl1.DataSource = datatable;
		TxtCode.Text = "";
		TxtTotals.Text = "0.00";
		Txtvat.Text = "0.00";
		TxtNet.Text = "0.00";
		TxtDate.DateTime = DateTime.Now;
		BtnDefualte();
		insertnewrow();
	}

	public void BtnDefualte()
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		TxtDate.ReadOnly = false;
	}

	public void Save()
	{
		int num = 0;
		if (datatable.Rows.Count > 1)
		{
			if (TxtDate.Text == "")
			{
				MessageBox.Show("عفوا\u064b من فضلك ادخل التاريخ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				return;
			}
			num = Exp.InsertExpenses(out var _, Convert.ToDateTime(TxtDate.DateTime), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(Txtvat.Text), Convert.ToDecimal(TxtNet.Text));
			Exp.InsertItemsExpenses(datatable, num);
			gridControl1.DataSource = datatable;
			gridView1.OptionsBehavior.Editable = false;
			if (num > 0)
			{
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnPrint.Enabled = true;
				TxtCode.Text = num.ToString();
				switch (MessageBox.Show("تم حفظ بيانات فاتورة المصروفات بنجاح  \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
				{
				case DialogResult.Yes:
					Print();
					break;
				}
			}
			else
			{
				MessageBox.Show("تم حفظ بيانات فاتورة المصروفات رقم ( " + TxtCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل صنف واحد على الاقل", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			gridControl1.Focus();
		}
	}

	public void Edit()
	{
		if (TxtDate.ReadOnly)
		{
			BtnEdit.Text = "F5 حفظ";
			insertnewrow();
			gridView1.OptionsBehavior.Editable = true;
			TxtDate.ReadOnly = false;
		}
		else if (TxtCode.Text != "")
		{
			int num = Convert.ToInt32(TxtCode.Text);
			if (datatable.Rows.Count > 1)
			{
				ArrangeDatatable();
				int num2 = 0;
				num2 = Exp.UpdateRental(Convert.ToInt32(TxtCode.Text), Convert.ToDecimal(TxtTotals.Text), Convert.ToDecimal(Txtvat.Text), Convert.ToDecimal(TxtNet.Text));
				Exp.deleteItems(Convert.ToInt32(TxtCode.Text));
				Exp.InsertItemsExpenses(datatable, num2);
				gridControl1.DataSource = datatable;
				gridView1.OptionsBehavior.Editable = false;
				if (num2 > 0)
				{
					BtnSave.Enabled = false;
					BtnEdit.Enabled = true;
					BtnDelete.Enabled = true;
					BtnEdit.Text = "تعديل F6";
					gridView1.OptionsBehavior.Editable = false;
					switch (MessageBox.Show("تم تعديل فاتورة المصروفات بنجاح \r\n هل تريد الطباعة الآن", "نجاح الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Print();
						break;
					}
				}
				else
				{
					MessageBox.Show("تم حفظ بيانات فاتورة المصروفات رقم  ( " + TxtCode.Text + " ) بنفس الاسم لا يمكن التكرار ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
			}
			else
			{
				MessageBox.Show("عفوا\u064b لا يمكن التعديل على فاتورة المصروفات  ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void Delete()
	{
		if (TxtCode.Text != "")
		{
			int num = Convert.ToInt32(TxtCode.Text);
			int num2 = 0;
			switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة المصروفات رقم  ( " + TxtCode.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
			{
			case DialogResult.Yes:
				num2 = Exp.DeleteRental(Convert.ToInt32(TxtCode.Text));
				if (num2 > 0)
				{
					MessageBox.Show("تم حذف بيانات فاتورة المصروفات رقم  ( " + TxtCode.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					Clear();
					gridView1.OptionsBehavior.Editable = true;
					CheckFfistRow();
					insertnewrow();
				}
				else
				{
					MessageBox.Show("لا يمكن حذف بيانات فاتورة المصروفات رقم ( " + TxtCode.Text + " ) بسبب ادارج بياناتها ", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				}
				break;
			}
		}
		else
		{
			MessageBox.Show("عفوا\u064b من فضلك ادخل بيانات فاتورة المصروفات بشكل صحيح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Print()
	{
		if (TxtCode.Text != "")
		{
			SqlConnection sqlConnection = new SqlConnection(LoginDetails.ConnString);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["RentalExpenses"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_RentalExpenses", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(TxtCode.Text));
			sqlDataAdapter.Fill(dataSetALL.RentalExpenses);
			sqlConnection.Close();
			if (dataSetALL.Tables["RentalExpenses"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				stiReport.Load(Application.StartupPath + "\\Reports\\RentalExpenses.mrt");
				stiReport.RegData(dataSetALL.RentalExpenses);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
				FrmReportGetOrder frmReportGetOrder = new FrmReportGetOrder();
				frmReportGetOrder.stiViewerControl1.Report = stiReport;
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، اختر رقم الفاتورة ثم حاول مرة اخري", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void FrmRentalExpenses_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
		try
		{
			base.KeyPreview = true;
			gridControl1.ForceInitialize();
			if (LoginDetails.Lang == "ar-EG")
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameAR);
			}
			else
			{
				gridControl1.MainView.RestoreLayoutFromXml(fileNameEN);
			}
		}
		catch (Exception)
		{
		}
	}

	private void FrmRentalExpenses_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (LoginDetails.Lang == "ar-EG")
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameAR);
		}
		else
		{
			gridControl1.MainView.SaveLayoutToXml(fileNameEN);
		}
	}

	private void FrmRentalExpenses_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F4")
		{
			Clear();
		}
		else if (e.KeyCode.ToString() == "F5")
		{
			Save();
		}
		else if (e.KeyCode.ToString() == "F6")
		{
			Edit();
		}
		else if (e.KeyCode.ToString() == "F7")
		{
			Delete();
		}
	}

	private void repositoryItemButtonDelete_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["SN"].ToString() == Convert.ToString(num))
			{
				if (row["ItemCode"].ToString() != string.Empty)
				{
					row.Delete();
					datatable.AcceptChanges();
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				else
				{
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				break;
			}
		}
		ArrangeDatatable();
		gridControl1.DataSource = datatable;
		GetTotals();
	}

	private void repositoryItemButtonEditCodeSearch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			string itemSearchText = null;
			FrmSearchItems1 frmSearchItems = new FrmSearchItems1(itemSearchText);
			frmSearchItems.ShowDialog();
			if (LoginDetails.ItemSearchAll2 == null)
			{
				return;
			}
			if (LoginDetails.ItemSearchAll2.Count > 0)
			{
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string code = item.ToString();
					Get_Item_ByCodeALL_Result get_Item_ByCodeALL_Result = ITM.GetItemByCode(code).FirstOrDefault();
					if (get_Item_ByCodeALL_Result != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["SN"] = 1;
						dataRow["ItemCode"] = get_Item_ByCodeALL_Result.Item_code;
						dataRow["ItemId"] = get_Item_ByCodeALL_Result.ItemId;
						dataRow["ItemName"] = get_Item_ByCodeALL_Result.item_Name;
						dataRow["IsVat"] = true;
						dataRow["Total"] = 0.0;
						dataRow["Itemvat"] = 0.0;
						dataRow["Net"] = 0.0;
						datatable.Rows.Add(dataRow);
					}
				}
				CheckFfistRow();
				insertnewrow();
				ArrangeDatatable();
				frmSearchItems.Close();
				frmSearchItems.Hide();
				LoginDetails.ItemSearchAll2.Clear();
				LoginDetails.ISPurches = false;
				gridView1.FocusedColumn = gridView1.VisibleColumns[0];
				gridView1.ShowEditor();
			}
			frmSearchItems.Close();
			frmSearchItems.Hide();
			LoginDetails.ItemSearchAll2.Clear();
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.OrderRent.FrmRentalExpenses));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtDate = new DevExpress.XtraEditors.DateEdit();
		this.label2 = new System.Windows.Forms.Label();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEditCodeSearch = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
		this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
		this.repositoryItemButtonBankBox = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtTotals = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.Txtvat = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.TxtNet = new Janus.Windows.GridEX.EditControls.NumericEditBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.TxtCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TxtCode.Location = new System.Drawing.Point(452, 22);
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.Size = new System.Drawing.Size(333, 28);
		this.TxtCode.TabIndex = 51;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(788, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(82, 21);
		this.label1.TabIndex = 52;
		this.label1.Text = "رقم السند";
		this.TxtDate.EditValue = null;
		this.TxtDate.Location = new System.Drawing.Point(6, 22);
		this.TxtDate.Name = "TxtDate";
		this.TxtDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.TxtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtDate.Properties.Appearance.Options.UseFont = true;
		this.TxtDate.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.TxtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
		this.TxtDate.Properties.MaskSettings.Set("mask", "yyyy/MM/dd HH:mm:ss tt");
		this.TxtDate.Size = new System.Drawing.Size(265, 28);
		this.TxtDate.TabIndex = 53;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(299, 25);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 21);
		this.label2.TabIndex = 54;
		this.label2.Text = "التاريخ";
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Font = new System.Drawing.Font("Tahoma", 10.2f);
		this.gridControl1.Location = new System.Drawing.Point(12, 91);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[14]
		{
			this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit1, this.repositoryItemTextEdit2, this.repositoryItemDateEdit1, this.repositoryItemTextEdit3, this.repositoryItemPictureEdit1, this.repositoryItemButtonEdit5, this.repositoryItemButtonEditCodeSearch, this.repositoryItemComboBox1,
			this.repositoryItemLookUpEdit1, this.repositoryItemButtonBankBox, this.repositoryItemDateEdit2, this.repositoryItemButtonEdit1
		});
		this.gridControl1.Size = new System.Drawing.Size(876, 248);
		this.gridControl1.TabIndex = 55;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray;
		this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[10] { this.gridColumn9, this.gridColumn2, this.gridColumn1, this.gridColumn8, this.gridColumn4, this.gridColumn5, this.gridColumn3, this.gridColumn6, this.gridColumn7, this.gridColumn19 });
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
		this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanging);
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "ظ…";
		this.gridColumn9.FieldName = "SN";
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		this.gridColumn9.OptionsColumn.AllowFocus = false;
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 0;
		this.gridColumn9.Width = 91;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "كود الصنف";
		this.gridColumn2.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn2.FieldName = "ItemCode";
		this.gridColumn2.MinWidth = 25;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 1;
		this.gridColumn2.Width = 143;
		this.repositoryItemButtonEdit1.AutoHeight = false;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions13.Image");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEditCodeSearch_ButtonClick);
		this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.Caption = "أسم الصنف";
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "ItemName";
		this.gridColumn1.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.MinWidth = 25;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 2;
		this.gridColumn1.Width = 117;
		this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.Caption = "حذف";
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.CustomizationCaption = "ط­1ظپ";
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 3;
		this.gridColumn8.Width = 55;
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		this.repositoryItemButtonEdit6.AutoHeight = false;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions14.Image");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "حذف", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonDelete_ButtonPressed);
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "المبلغ";
		this.gridColumn4.FieldName = "Total";
		this.gridColumn4.MinWidth = 25;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "SUM={0:0.##}")
		});
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 4;
		this.gridColumn4.Width = 94;
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.Caption = "تفعيل الضريبه";
		this.gridColumn5.FieldName = "IsVat";
		this.gridColumn5.MinWidth = 30;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.Caption = "الاجمالي بالضريبه";
		this.gridColumn3.FieldName = "Net";
		this.gridColumn3.MinWidth = 25;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Net", "SUM={0:0.##}")
		});
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 5;
		this.gridColumn3.Width = 94;
		this.gridColumn6.Caption = "ItemId";
		this.gridColumn6.FieldName = "ItemId";
		this.gridColumn6.MinWidth = 25;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 6;
		this.gridColumn6.Width = 94;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "الضريبه";
		this.gridColumn7.FieldName = "ItemVat";
		this.gridColumn7.MinWidth = 25;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[1]
		{
			new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ItemVat", "SUM={0:0.##}")
		});
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 7;
		this.gridColumn7.Width = 94;
		this.gridColumn19.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceCell.Options.UseFont = true;
		this.gridColumn19.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn19.Caption = "البيان";
		this.gridColumn19.FieldName = "Note";
		this.gridColumn19.MinWidth = 25;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.Visible = true;
		this.gridColumn19.VisibleIndex = 8;
		this.gridColumn19.Width = 94;
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemTextEdit1.AutoHeight = false;
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		this.repositoryItemTextEdit2.AutoHeight = false;
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		this.repositoryItemDateEdit1.AutoHeight = false;
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		this.repositoryItemTextEdit3.AutoHeight = false;
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		this.repositoryItemButtonEdit5.AutoHeight = false;
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEditCodeSearch.AutoHeight = false;
		this.repositoryItemButtonEditCodeSearch.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
		});
		this.repositoryItemButtonEditCodeSearch.Name = "repositoryItemButtonEditCodeSearch";
		this.repositoryItemComboBox1.AutoHeight = false;
		this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
		this.repositoryItemLookUpEdit1.AutoHeight = false;
		this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
		this.repositoryItemButtonBankBox.Name = "repositoryItemButtonBankBox";
		this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemDateEdit2.Mask.UseMaskAsDisplayFormat = true;
		this.repositoryItemDateEdit2.MaskSettings.Set("mask", "yyyy/MM/dd");
		this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		this.gridColumn10.Caption = "كود_الصنف";
		this.gridColumn10.Name = "gridColumn10";
		this.gridColumn10.Visible = true;
		this.gridColumn10.VisibleIndex = 0;
		this.gridColumn11.Caption = "أسم الصنف";
		this.gridColumn11.Name = "gridColumn11";
		this.gridColumn11.Visible = true;
		this.gridColumn11.VisibleIndex = 1;
		this.gridColumn12.Caption = "الوحدة";
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		this.gridColumn12.Visible = true;
		this.gridColumn12.VisibleIndex = 2;
		this.gridColumn13.Caption = "الكمية";
		this.gridColumn13.Name = "gridColumn13";
		this.gridColumn13.Visible = true;
		this.gridColumn13.VisibleIndex = 3;
		this.gridColumn14.Caption = "سعر الوحدة";
		this.gridColumn14.Name = "gridColumn14";
		this.gridColumn14.Visible = true;
		this.gridColumn14.VisibleIndex = 4;
		this.gridColumn15.Caption = "الاجمالى";
		this.gridColumn15.Name = "gridColumn15";
		this.gridColumn15.Visible = true;
		this.gridColumn15.VisibleIndex = 5;
		this.gridColumn16.Caption = "الضربية";
		this.gridColumn16.Name = "gridColumn16";
		this.gridColumn16.Visible = true;
		this.gridColumn16.VisibleIndex = 6;
		this.gridColumn17.Caption = "إجمالى المبلغ";
		this.gridColumn17.Name = "gridColumn17";
		this.gridColumn17.Visible = true;
		this.gridColumn17.VisibleIndex = 7;
		this.gridColumn18.Caption = "حذف الصنف";
		this.gridColumn18.Name = "gridColumn18";
		this.gridColumn18.Visible = true;
		this.gridColumn18.VisibleIndex = 8;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.TxtDate);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(876, 73);
		this.groupBox1.TabIndex = 56;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "السندات";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Location = new System.Drawing.Point(12, 423);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(876, 61);
		this.groupBox2.TabIndex = 57;
		this.groupBox2.TabStop = false;
		this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Location = new System.Drawing.Point(274, 14);
		this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Size = new System.Drawing.Size(122, 40);
		this.BtnDelete.TabIndex = 8;
		this.BtnDelete.Text = "حذف F7";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Location = new System.Drawing.Point(484, 14);
		this.BtnEdit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Size = new System.Drawing.Size(122, 40);
		this.BtnEdit.TabIndex = 7;
		this.BtnEdit.Text = "تعديل F6";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(744, 14);
		this.BtnNew.Margin = new System.Windows.Forms.Padding(4);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(125, 40);
		this.BtnNew.TabIndex = 6;
		this.BtnNew.Text = "جديد F4";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(614, 14);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 40);
		this.BtnSave.TabIndex = 1;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Location = new System.Drawing.Point(141, 14);
		this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Size = new System.Drawing.Size(125, 40);
		this.BtnPrint.TabIndex = 4;
		this.BtnPrint.Text = "طباعة ";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(7, 14);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 5;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.TxtTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.TxtTotals.DecimalDigits = 3;
		this.TxtTotals.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.TxtTotals.ForeColor = System.Drawing.Color.Black;
		this.TxtTotals.Location = new System.Drawing.Point(622, 350);
		this.TxtTotals.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtTotals.Name = "TxtTotals";
		this.TxtTotals.ReadOnly = true;
		this.TxtTotals.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtTotals.Size = new System.Drawing.Size(259, 27);
		this.TxtTotals.TabIndex = 162;
		this.TxtTotals.Text = "0.000";
		this.TxtTotals.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtTotals.Value = new decimal(new int[4]);
		this.TxtTotals.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.Txtvat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.Txtvat.DecimalDigits = 3;
		this.Txtvat.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.Txtvat.ForeColor = System.Drawing.Color.Black;
		this.Txtvat.Location = new System.Drawing.Point(622, 388);
		this.Txtvat.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.Txtvat.Name = "Txtvat";
		this.Txtvat.ReadOnly = true;
		this.Txtvat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Txtvat.Size = new System.Drawing.Size(259, 27);
		this.Txtvat.TabIndex = 163;
		this.Txtvat.Text = "0.000";
		this.Txtvat.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.Txtvat.Value = new decimal(new int[4]);
		this.Txtvat.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.TxtNet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.TxtNet.DecimalDigits = 3;
		this.TxtNet.Font = new System.Drawing.Font("Tahoma", 20f);
		this.TxtNet.ForeColor = System.Drawing.Color.Blue;
		this.TxtNet.Location = new System.Drawing.Point(19, 367);
		this.TxtNet.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
		this.TxtNet.Name = "TxtNet";
		this.TxtNet.ReadOnly = true;
		this.TxtNet.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtNet.Size = new System.Drawing.Size(385, 48);
		this.TxtNet.TabIndex = 164;
		this.TxtNet.Text = "0.000";
		this.TxtNet.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
		this.TxtNet.UseCompatibleTextRendering = false;
		this.TxtNet.Value = new decimal(new int[4]);
		this.TxtNet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(533, 350);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(72, 21);
		this.label3.TabIndex = 165;
		this.label3.Text = "الاجمالي";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(533, 393);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(60, 21);
		this.label4.TabIndex = 166;
		this.label4.Text = "الضريبه";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(900, 558);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.TxtNet);
		base.Controls.Add(this.Txtvat);
		base.Controls.Add(this.TxtTotals);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.gridControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmRentalExpenses";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.Text = "مصروفات";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmRentalExpenses_FormClosing);
		base.Load += new System.EventHandler(FrmRentalExpenses_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FrmRentalExpenses_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtDate.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEditCodeSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonBankBox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


