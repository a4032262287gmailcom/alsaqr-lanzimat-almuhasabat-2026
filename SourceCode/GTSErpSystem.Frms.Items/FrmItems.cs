using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Behaviors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using GTSErpSystem.Frms.FrmsMain;
using GTSErpSystem.Frms.Items.FrmSearch;
using GTSErpSystem.Frms.Orders.FrmSearchOrder;
using GTSErpSystem.Helper;
using Stimulsoft.Report;

namespace GTSErpSystem.Frms.Items;

public class FrmItems : XtraForm
{
	private Class_Items Items = new Class_Items();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Purchases Pur = new Class_Purchases();

	private Class_Group class_Group = new Class_Group();

	private GTSdbEntities db = new GTSdbEntities();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	public string CodeItm;

	private static int IsShow;

	private static int PrintID;

	private int DecimalNum = LoginDetails.DecimalNum;

	private bool _isUpdating = false;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private SimpleButton BtnExit;

	private SimpleButton BtnDelete;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnPrint;

	private GroupBox groupBox2;

	private XtraTabControl TapPrice;

	private XtraTabPage xtraTabPage1;

	private XtraTabPage xtraTabPage2;

	private XtraTabPage xtraTabPage3;

	private XtraTabPage xtraTabPage4;

	private System.Windows.Forms.ComboBox CBClass;

	private System.Windows.Forms.ComboBox CbItemtype;

	private ButtonEdit TxtItemName;

	private Label label31;

	private Label label30;

	private Label label36;

	private ButtonEdit TxtitemnameEn;

	private SeparatorControl separatorControl1;

	private SeparatorControl separatorControl3;

	private SeparatorControl separatorControl6;

	private SeparatorControl separatorControl5;

	private SeparatorControl separatorControl2;

	private SeparatorControl separatorControl4;

	private NumericUpDown nlastpriceunit3;

	private NumericUpDown nlastpriceunit2;

	private NumericUpDown nlastpriceunit1;

	private Label label19;

	private TextBox TxtNotes;

	private Label label37;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn shearch;

	private RepositoryItemButtonEdit repositoryItemButtonEdit4;

	private GridColumn gridColumn19;

	private GridColumn gridColumn4;

	private RepositoryItemButtonEdit repositoryItemButtonEdit5;

	private GridColumn gridColumn20;

	private GridColumn gridColumn21;

	private GridColumn gridColumn3;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit6;

	private GridColumn gridColumn9;

	private RepositoryItemTextEdit repositoryItemTextEdit4;

	private RepositoryItemTextEdit repositoryItemTextEdit5;

	private RepositoryItemDateEdit repositoryItemDateEdit2;

	private RepositoryItemTextEdit repositoryItemTextEdit6;

	private RepositoryItemPictureEdit repositoryItemPictureEdit2;

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

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemTextEdit repositoryItemTextEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit2;

	private RepositoryItemDateEdit repositoryItemDateEdit1;

	private RepositoryItemTextEdit repositoryItemTextEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit3;

	private OpenFileDialog openFileDialog1;

	private BehaviorManager behaviorManager1;

	private Label label8;

	private ButtonEdit TxtLocaton1;

	private Label label50;

	private Label label7;

	private ButtonEdit TxtLocaton2;

	private ButtonEdit TxtLocaton3;

	private CheckBox checkBox8;

	private CheckBox checkBox7;

	private CheckBox checkBox6;

	private CheckBox checkBox5;

	private CheckBox checkBox4;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private SeparatorControl separatorControl8;

	private RepositoryItemButtonEdit repositoryItemButtonEdit7;

	public ButtonEdit TxtItemCode;

	private System.Windows.Forms.ComboBox CBCompany;

	private Label label9;

	private CheckBox CHVat;

	private Label label49;

	private NumericUpDown NVat;

	private ButtonEdit TxtBarcodeBig2;

	private Label label43;

	private ButtonEdit TxtBarcodeMedium2;

	private Label label41;

	private ButtonEdit TxtBarcodeSmall2;

	private Label label39;

	private NumericUpDown NPriceMowazaunit3;

	private NumericUpDown NPriceExportunit3;

	private NumericUpDown NAverg;

	private Label label53;

	private NumericUpDown NPriceExportunit1;

	private Label label54;

	private NumericUpDown NPriceMowazaunit2;

	private NumericUpDown NPriceExportunit2;

	private Label label26;

	private Label label15;

	private NumericUpDown nRequestunit3;

	private NumericUpDown nRequestunit2;

	private XtraTabPage xtraTabPage5;

	private CheckBox CHISItemMizan;

	private System.Windows.Forms.ComboBox CBGuarantee;

	private Label label58;

	private System.Windows.Forms.ComboBox CBCountry;

	private Label label57;

	private NumericUpDown nRequestunit1;

	private Label label21;

	private SimpleButton BtnSearch;

	private Label label24;

	private Label label52;

	private Label label55;

	private Label label13;

	private Label label44;

	private Label label51;

	private Label LblItmID;

	private RadioButton RBUnitTafqit3;

	private RadioButton RBUnitTafqit2;

	private RadioButton RBUnitTafqit1;

	private System.Windows.Forms.ComboBox comboPrint;

	private Label label56;

	private XtraTabPage xtraTabPage6;

	private Label label63;

	private NumericUpDown Npharmacist;

	private Label label64;

	private Label label61;

	private NumericUpDown NDoctor;

	private Label label62;

	private Label label60;

	private NumericUpDown NDiscItem;

	private Label label59;

	private ButtonEdit TxtBarcodeBig3;

	private Label label42;

	private ButtonEdit TxtBarcodeMedium3;

	private Label label38;

	private ButtonEdit TxtBarcodeSmall3;

	private Label label40;

	private System.Windows.Forms.ComboBox CbUnit2;

	private System.Windows.Forms.ComboBox CbUnit3;

	private System.Windows.Forms.ComboBox CpUnit1;

	private ButtonEdit TxtBarcodeBig1;

	private ButtonEdit TxtBarcodeMedium1;

	private ButtonEdit TxtBarcodeSmall1;

	private NumericUpDown nConvertUnit3;

	private Label label28;

	private Label label18;

	private Label label22;

	private NumericUpDown nConvertUnit2;

	private Label label27;

	private Label label11;

	private Label label12;

	private NumericUpDown numericUpDown1;

	private Label label35;

	private Label label34;

	private Label label33;

	private Label label23;

	private Label label25;

	private Label label29;

	private Label label10;

	private Label label14;

	private Label label17;

	private NumericUpDown NPrice3unit3;

	private NumericUpDown NPrice3unit2;

	private Label label16;

	private NumericUpDown NPrice3unit1;

	private NumericUpDown NPrice2unit3;

	private NumericUpDown NPrice1unit3;

	private NumericUpDown NPrice2unit2;

	private NumericUpDown NPrice1unit2;

	private NumericUpDown NPrice2unit1;

	private NumericUpDown NPrice1unit1;

	private Label label20;

	private Label label32;

	private SimpleButton BtnImage;

	private Label label47;

	private PictureEdit pictureEdit1;

	public ButtonEdit buttonEdit1;

	private Label label65;

	private NumericUpDown TxtPerUnit3Price3;

	private NumericUpDown TxtPerUnit2Price3;

	private NumericUpDown TxtPerUnit1Price3;

	private NumericUpDown TxtPerUnit3Price2;

	private NumericUpDown TxtPerUnit2Price2;

	private NumericUpDown TxtPerUnit1Price2;

	private NumericUpDown TxtPerUnit3Price1;

	private NumericUpDown TxtPerUnit2Price1;

	private NumericUpDown TxtPerUnit1Price1;

	private System.Windows.Forms.ComboBox CBCat;

	private Label label66;

	private NumericUpDown NQty;

	private SimpleButton BtnGuanartyR;

	private SimpleButton BtnCompanyR;

	private SimpleButton BtnContryR;

	private SimpleButton BtnClassR;

	private SimpleButton BtnGroupR;

	private Label label74;

	private Label label71;

	private Label label69;

	private GroupBox groupBox3;

	private CheckBox CBISOffer;

	private NumericUpDown TxtNumOffer;

	private NumericUpDown TxtPriceOffer;

	private Label label46;

	private Label label45;

	private System.Windows.Forms.ComboBox CBStore;

	private SimpleButton BtnGuanartyP;

	private SimpleButton BtnCompanyP;

	private SimpleButton BtnContryP;

	private SimpleButton BtnClassP;

	private SimpleButton BtnGroupP;

	private SimpleButton BtnUnit3P;

	private SimpleButton BtnUnit3R;

	private SimpleButton BtnUnit2P;

	private SimpleButton BtnUnit2R;

	private SimpleButton BtnUnit1P;

	private SimpleButton BtnUnit1R;

	private SimpleButton BtnCalcVatLarg3;

	private SimpleButton BtnCalcVatLarg2;

	private SimpleButton BtnCalcVatLarg1;

	private SimpleButton BtnCalcVatMedium3;

	private SimpleButton BtnCalcVatMedium2;

	private SimpleButton BtnCalcVatMedium1;

	private SimpleButton BtnCalcVatSmall2;

	private SimpleButton BtnCalcVatSmall3;

	private SimpleButton BtnCalcVatSmall1;

	private RadioButton RBUnitDefualtSell3;

	private RadioButton RBUnitDefualtSell2;

	private RadioButton RBUnitDefualtSell1;

	private Label label76;

	private NumericUpDown NTobacco;

	private Label label67;

	private Label label73;

	private Label label75;

	private Label label48;

	private Label label70;

	private Label label72;

	private Label label68;

	private Label label77;

	private GroupBox GropElctronc;

	private Label label78;

	private System.Windows.Forms.ComboBox TxtVatCode;

	private Label label79;

	private System.Windows.Forms.ComboBox TxtExemptionReason;

	private Label label81;

	private Label label80;

	public FrmItems()
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
		TxtDigital();
		ClearControls();
		label81.Text = CpUnit1.Text;
		SetInitialRow();
		gridView1.OptionsBehavior.Editable = false;
		TapPrice.SelectedTabPage = xtraTabPage1;
		BindElctorncVat();
		ElctorncSeeting();
		GetItmAnotherFrm();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("كود_الصنف", typeof(string));
		datatable.Columns.Add("أسم الصنف", typeof(string));
		datatable.Columns.Add("الوحدة", typeof(string));
		datatable.Columns.Add("المخزن", typeof(string));
		datatable.Columns.Add("رقم المخزن", typeof(int));
		datatable.Columns.Add("الكمية", typeof(decimal));
		datatable.Columns.Add("ظ….التكلفة", typeof(decimal));
		datatable.Columns.Add("سعربعدالخصم", typeof(decimal));
		datatable.Columns.Add("نسبةالخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		datatable.Columns.Add("هل_ضريبة", typeof(int));
		datatable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		datatable.Columns.Add("أسم_الوحدة", typeof(string));
		datatable.Columns.Add("الاجمالى", typeof(string));
		datatable.Columns.Add("الضربية", typeof(decimal));
		datatable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		datatable.Columns.Add("إجمالى المبلغ", typeof(string));
		datatable.Columns.Add("حذف الصنف", typeof(int));
		datatable.Columns.Add("ظ…", typeof(int));
		datatable.Columns.Add("مسح", typeof(int));
		datatable.Columns.Add("تم", typeof(int));
		dtCurrentTable.Columns.Add("كود_الصنف", typeof(string));
		dtCurrentTable.Columns.Add("أسم الصنف", typeof(string));
		dtCurrentTable.Columns.Add("الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("المخزن", typeof(string));
		dtCurrentTable.Columns.Add("رقم المخزن", typeof(int));
		dtCurrentTable.Columns.Add("الكمية", typeof(decimal));
		dtCurrentTable.Columns.Add("ظ….التكلفة", typeof(decimal));
		dtCurrentTable.Columns.Add("سعربعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("نسبةالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى_بعدالخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("هل_ضريبة", typeof(int));
		dtCurrentTable.Columns.Add("قيمة_الضريبة", typeof(decimal));
		dtCurrentTable.Columns.Add("أسم_الوحدة", typeof(string));
		dtCurrentTable.Columns.Add("الاجمالى", typeof(string));
		dtCurrentTable.Columns.Add("الضربية", typeof(decimal));
		dtCurrentTable.Columns.Add("الضريبه_بعد_الخصم", typeof(decimal));
		dtCurrentTable.Columns.Add("إجمالى المبلغ", typeof(string));
		dtCurrentTable.Columns.Add("حذف الصنف", typeof(int));
		dtCurrentTable.Columns.Add("ظ…", typeof(int));
		dtCurrentTable.Columns.Add("مسح", typeof(int));
		dtCurrentTable.Columns.Add("تم", typeof(int));
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["الكمية"] = 0;
		dataRow["ظ….التكلفة"] = 0.0;
		dataRow["سعربعدالخصم"] = 0.0;
		dataRow["نسبةالخصم"] = 0.0;
		dataRow["إجمالى_بعدالخصم"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["الضربية"] = 0.0;
		dataRow["الضريبه_بعد_الخصم"] = 0.0;
		dataRow["إجمالى المبلغ"] = 0.0;
		dataRow["هل_ضريبة"] = 0;
		dataRow["قيمة_الضريبة"] = 0.0;
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["مسح"] = 0;
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
		gridView1.ValidateRow += gridView1_ValidateRow;
		gridView1.CellValueChanged += gridView1_CellValueChanged;
		gridView1.Columns["الكمية"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.##}", "0")
		});
		gridView1.Columns["ظ….التكلفة"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.##}", "0")
		});
		gridView1.Columns["الاجمالى"].Summary.AddRange(new GridSummaryItem[1]
		{
			new GridColumnSummaryItem(SummaryItemType.Sum, string.Empty, "{0:0.##}", "0")
		});
	}

	private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
	{
		string text = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "كود_الصنف"));
		string text2 = Convert.ToString(gridView1.GetRowCellValue(e.RowHandle, "أسم الصنف"));
		decimal num = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "الكمية"));
		decimal num2 = Convert.ToDecimal(gridView1.GetRowCellValue(e.RowHandle, "ظ….التكلفة"));
		if (text == string.Empty || text2 == string.Empty || num <= 0m || num2 <= 0m)
		{
			Item_Items item_Items = Pur.GetItems(text).FirstOrDefault();
			if (item_Items != null)
			{
				e.Valid = false;
				if (text == string.Empty)
				{
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				else if (text2 == string.Empty)
				{
					MessageBox.Show("برجاء إدخال أسم صنف");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[0];
					gridView1.ShowEditor();
				}
				else if (num <= 0m)
				{
					MessageBox.Show("برجاء إدخال كمية أكبر من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[3];
					gridView1.ShowEditor();
				}
				else if (num2 <= 0m)
				{
					MessageBox.Show("برجاء إدخال متوسط التكلفة أكبر من الصفر");
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[4];
					gridView1.ShowEditor();
				}
			}
		}
		GetTotalCost();
	}

	public void CheckFfistRow()
	{
		for (int num = datatable.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = datatable.Rows[num];
			if (dataRow["الكمية"].ToString() == "0" || dataRow["ظ….التكلفة"].ToString() == "0")
			{
				dataRow.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
		GetTotalCost();
	}

	public void insertnewrow()
	{
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["ظ….التكلفة"] = 0;
		dataRow["الكمية"] = 1;
		dataRow["تم"] = 0;
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["رقم المخزن"] = Pur.GetStoreID();
		datatable.Rows.Add(dataRow);
		gridControl1.DataSource = datatable;
	}

	private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
	{
		int num = 0;
		if (e.Column.FieldName == "كود_الصنف")
		{
			string code = Convert.ToString(e.Value);
			Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
			if (0 == 0)
			{
				object rowCellValue = gridView1.GetRowCellValue(e.RowHandle, "تم");
				string text = rowCellValue.ToString();
				if (text == "0")
				{
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["مسح"] = 0;
						dataRow["تم"] = 1;
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["ظ….التكلفة"] = item_Items.Average_cost;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.Average_cost * (decimal?)Convert.ToDecimal(1)), DecimalNum);
						dataRow["المخزن"] = Pur.GetStoreName();
						dataRow["رقم المخزن"] = Pur.GetStoreID();
						object rowCellValue2 = gridView1.GetRowCellValue(e.RowHandle, "ظ…");
						string text2 = rowCellValue2.ToString();
						if (text2 == string.Empty)
						{
							dataRow["ظ…"] = e.RowHandle + 1;
						}
						else
						{
							dataRow["ظ…"] = text2;
						}
						CheckFfistRow();
						datatable.Rows.Add(dataRow);
						insertnewrow();
						gridView1.FocusedRowHandle = e.RowHandle;
						gridView1.FocusedColumn = gridView1.VisibleColumns[0];
						gridView1.ShowEditor();
					}
					else
					{
						switch (MessageBox.Show("نعتذر، كود الصنف غير موجود في قاعدة البيانات \r\n ادخل كود الصنف ثم حاول مرة اخري", "المستكشف", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
						{
						case DialogResult.Yes:
							gridView1.DeleteRow(gridView1.FocusedRowHandle);
							insertnewrow();
							gridView1.FocusedColumn = gridView1.VisibleColumns[-1];
							break;
						}
					}
				}
			}
			else
			{
				MessageBox.Show("تم إدخال هذا الصنف من قبل");
			}
		}
		else if (e.Column.FieldName == "الكمية")
		{
			string value = Convert.ToString(e.Value);
			string focusedRowCellDisplayText = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText2 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText3 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText4 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText != string.Empty)
			{
				Item_Items item_Items2 = Pur.GetItems(focusedRowCellDisplayText).FirstOrDefault();
				if (item_Items2 != null)
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == focusedRowCellDisplayText4)
						{
							decimal num2 = Convert.ToDecimal(row["ظ….التكلفة"].ToString());
							if (row["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value)), DecimalNum);
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value)), DecimalNum);
								break;
							}
							if (row["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(num2 * Convert.ToDecimal(value)), DecimalNum);
								break;
							}
						}
					}
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[3];
					gridView1.ShowEditor();
				}
			}
		}
		else
		{
			if (!(e.Column.FieldName == "ظ….التكلفة"))
			{
				return;
			}
			string value2 = Convert.ToString(e.Value);
			string focusedRowCellDisplayText5 = gridView1.GetFocusedRowCellDisplayText("كود_الصنف");
			string focusedRowCellDisplayText6 = gridView1.GetFocusedRowCellDisplayText("الوحدة");
			string focusedRowCellDisplayText7 = gridView1.GetFocusedRowCellDisplayText("أسم_الوحدة");
			string focusedRowCellDisplayText8 = gridView1.GetFocusedRowCellDisplayText("الكمية");
			string focusedRowCellDisplayText9 = gridView1.GetFocusedRowCellDisplayText("ظ…");
			if (focusedRowCellDisplayText5 != string.Empty)
			{
				Item_Items item_Items3 = Pur.GetItems(focusedRowCellDisplayText5).FirstOrDefault();
				if (item_Items3 != null)
				{
					foreach (DataRow row2 in datatable.Rows)
					{
						if (row2["ظ…"].ToString() == focusedRowCellDisplayText9)
						{
							if (row2["أسم_الوحدة"].ToString() == "الصغرى")
							{
								row2["ظ….التكلفة"] = value2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "المتوسطة")
							{
								row2["ظ….التكلفة"] = value2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
								break;
							}
							if (row2["أسم_الوحدة"].ToString() == "الكبرى")
							{
								row2["ظ….التكلفة"] = value2;
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(Convert.ToDecimal(value2) * Convert.ToDecimal(focusedRowCellDisplayText8)), DecimalNum);
								break;
							}
						}
					}
					gridView1.FocusedRowHandle = e.RowHandle;
					gridView1.FocusedColumn = gridView1.VisibleColumns[4];
					gridView1.ShowEditor();
				}
			}
		}
		ArrangeDatatable();
		GetTotalCost();
	}

	private void repositoryItemButtonEdit1_ButtonPressed_1(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != string.Empty)
		{
			LoginDetails.ItemcodeForUnits = Itemocde;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			Item_Items item_Items = (from x in Pur.GetItems()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			int itemId = item_Items.ItemId;
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != string.Empty)
			{
				string itemCode = frmSearchUnit.ItemCode;
				if (frmSearchUnit.unitName == "الصغرى")
				{
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["ظ…"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items2.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["المخزن"] = Pur.GetStoreName();
								row["رقم المخزن"] = Pur.GetStoreID();
								row["الكمية"] = 1;
								row["ظ….التكلفة"] = item_Items2.Average_cost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.Average_cost * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						string StoreName = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num2 = (from x in db.Account_Stores
							where x.Store_Name == StoreName
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["ظ…"].ToString() == Convert.ToString(num3))
							{
								row2["الوحدة"] = item_Items3.Item_Unit1.Name;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["المخزن"] = Pur.GetStoreName();
								row2["رقم المخزن"] = Pur.GetStoreID();
								row2["ظ….التكلفة"] = Math.Round(Convert.ToDecimal(item_Items3.Average_cost * item_Items3.ConvertMediumUnit), DecimalNum);
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.Average_cost * item_Items3.ConvertMediumUnit * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						string StoreName2 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num4 = (from x in db.Account_Stores
							where x.Store_Name == StoreName2
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Item_Items item_Items4 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items4 != null)
					{
						int num5 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["ظ…"].ToString() == Convert.ToString(num5))
							{
								row3["الوحدة"] = item_Items4.Item_Unit2.Name;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["المخزن"] = Pur.GetStoreName();
								row3["رقم المخزن"] = Pur.GetStoreID();
								row3["ظ….التكلفة"] = Math.Round(Convert.ToDecimal(item_Items4.Average_cost * item_Items.ConvertMediumUnit * item_Items.ConvertBigUnit), DecimalNum);
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items4.Average_cost * item_Items.ConvertMediumUnit * item_Items.ConvertBigUnit * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						string StoreName3 = gridView1.GetFocusedRowCellDisplayText("المخزن");
						int num6 = (from x in db.Account_Stores
							where x.Store_Name == StoreName3
							select x.ID).FirstOrDefault();
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		GetTotalCost();
	}

	private void repositoryItemButtonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != string.Empty)
		{
			Item_Items item_Items = (from x in Pur.GetItems()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			LoginDetails.ItemcodeForStore = item_Items.ItemId;
			FrmSearchTransfer frmSearchTransfer = new FrmSearchTransfer();
			frmSearchTransfer.ShowDialog();
			if (frmSearchTransfer.ItemIdStore <= 0)
			{
				int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
				{
					foreach (DataRow row in datatable.Rows)
					{
						if (row["ظ…"].ToString() == Convert.ToString(num))
						{
							row["المخزن"] = Pur.GetStoreName();
							row["رقم المخزن"] = 1;
						}
					}
					return;
				}
			}
			int StoreID = LoginDetails.StoreID;
			if (StoreID == 0)
			{
				StoreID = 1;
			}
			int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
			string value = (from x in db.Account_Stores
				where x.ID == StoreID
				select x.Store_Name).FirstOrDefault();
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["ظ…"].ToString() == Convert.ToString(num2))
				{
					row2["المخزن"] = value;
					row2["رقم المخزن"] = StoreID;
				}
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[5];
			gridView1.ShowEditor();
			frmSearchTransfer.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف المخازن", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		string Itemocde = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "كود_الصنف"));
		if (Itemocde != string.Empty)
		{
			LoginDetails.ItemcodeForUnits = Itemocde;
			FrmSearchUnit frmSearchUnit = new FrmSearchUnit();
			Item_Items item_Items = (from x in Pur.GetItems()
				where x.Item_code == Itemocde
				select x).FirstOrDefault();
			int itemId = item_Items.ItemId;
			frmSearchUnit.ShowDialog();
			if (frmSearchUnit.ItemCode != string.Empty)
			{
				string itemCode = frmSearchUnit.ItemCode;
				if (frmSearchUnit.unitName == "الصغرى")
				{
					Item_Items item_Items2 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items2 != null)
					{
						int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row in datatable.Rows)
						{
							if (row["ظ…"].ToString() == Convert.ToString(num))
							{
								row["الوحدة"] = item_Items2.Item_Unit.Name;
								row["أسم_الوحدة"] = "الصغرى";
								row["الكمية"] = 1;
								row["ظ….التكلفة"] = item_Items2.Average_cost;
								row["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items2.Average_cost * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "المتوسطة")
				{
					Item_Items item_Items3 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items3 != null)
					{
						int num2 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row2 in datatable.Rows)
						{
							if (row2["ظ…"].ToString() == Convert.ToString(num2))
							{
								row2["الوحدة"] = item_Items3.Item_Unit1.Name;
								row2["أسم_الوحدة"] = "المتوسطة";
								row2["الكمية"] = 1;
								row2["ظ….التكلفة"] = Math.Round(Convert.ToDecimal(item_Items3.Average_cost * item_Items.ConvertMediumUnit), DecimalNum);
								row2["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items3.Average_cost * item_Items.ConvertMediumUnit * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						frmSearchUnit.Close();
					}
				}
				if (frmSearchUnit.unitName == "الكبرى")
				{
					Item_Items item_Items4 = Pur.GetItems(itemCode).FirstOrDefault();
					if (item_Items4 != null)
					{
						int num3 = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
						foreach (DataRow row3 in datatable.Rows)
						{
							if (row3["ظ…"].ToString() == Convert.ToString(num3))
							{
								row3["الوحدة"] = item_Items4.Item_Unit2.Name;
								row3["أسم_الوحدة"] = "الكبرى";
								row3["الكمية"] = 1;
								row3["ظ….التكلفة"] = Math.Round(Convert.ToDecimal(item_Items4.Average_cost * item_Items.ConvertBigUnit * item_Items.ConvertMediumUnit), DecimalNum);
								row3["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items4.Average_cost * item_Items.ConvertBigUnit * item_Items.ConvertMediumUnit * (decimal?)Convert.ToInt32(1)), DecimalNum);
							}
						}
						frmSearchUnit.Close();
					}
				}
				frmSearchUnit.Close();
			}
			gridView1.FocusedRowHandle = gridView1.FocusedRowHandle;
			gridView1.FocusedColumn = gridView1.VisibleColumns[3];
			gridView1.ShowEditor();
			frmSearchUnit.Close();
		}
		else
		{
			MessageBox.Show("برجاء إدخال كود صنف صحيح", "مستكشف الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void repositoryItemButtonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		foreach (DataRow row in datatable.Rows)
		{
			if (row["ظ…"].ToString() == Convert.ToString(num))
			{
				if (row["كود_الصنف"].ToString() != string.Empty)
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
	}

	private void repositoryItemButtonEdit7_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		SearchItems();
	}

	public void FiterDatatableItems()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (text != string.Empty)
			{
				DataRow[] array = dtCurrentTable.Select("كود_الصنف = '" + text + "'");
				if (array.Length == 0)
				{
					dtCurrentTable.Rows.Add(row.ItemArray);
				}
			}
			gridControl1.DataSource = dtCurrentTable;
		}
		datatable = dtCurrentTable;
		GetTotalCost();
	}

	public void FiterDatatableItemsUpdate()
	{
		foreach (DataRow row in datatable.Rows)
		{
			string text = Convert.ToString(row["كود_الصنف"]);
			if (text == string.Empty)
			{
				row.Delete();
				datatable.AcceptChanges();
				break;
			}
		}
		gridControl1.DataSource = datatable;
	}

	public void ArrangeDatatable()
	{
		int num = 1;
		foreach (DataRow row in datatable.Rows)
		{
			row["ظ…"] = num;
			num++;
		}
	}

	public string ImageToBase64(Image image, ImageFormat format)
	{
		using MemoryStream memoryStream = new MemoryStream();
		image.Save(memoryStream, format);
		byte[] inArray = memoryStream.ToArray();
		return Convert.ToBase64String(inArray);
	}

	public Image Base64ToImage(string base64String)
	{
		byte[] array = Convert.FromBase64String(base64String);
		MemoryStream memoryStream = new MemoryStream(array, 0, array.Length);
		memoryStream.Write(array, 0, array.Length);
		return Image.FromStream(memoryStream, useEmbeddedColorManagement: true);
	}

	public void BindDropDownList()
	{
		GetStores();
		FillItemType();
		FillItemUnit();
		FillItemUnit2();
		FillItemUnit3();
		FillItemClass();
		FillItemCat();
		FillItemCompany();
		FillItemGuarantee();
		FillItemCountry();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnNew.Enabled = true;
		BtnPrint.Enabled = false;
		if (LoginDetails.ItemcodeNewItem != null)
		{
			TxtItemCode.Text = LoginDetails.ItemcodeNewItem;
		}
	}

	public void FillItemUnit()
	{
		CpUnit1.DataSource = null;
		int iD = (from x in Items.GetUnits()
			where x.ID > 0
			select x).FirstOrDefault().ID;
		CpUnit1.DataSource = (from x in Items.GetUnits()
			where x.ID > 0
			select x).ToList();
		CpUnit1.DisplayMember = "Name";
		CpUnit1.ValueMember = "ID";
		CpUnit1.SelectedValue = iD;
	}

	public void FillItemUnit2()
	{
		CbUnit2.DataSource = null;
		CbUnit2.DataSource = Items.GetUnits().ToList();
		CbUnit2.DisplayMember = "Name";
		CbUnit2.ValueMember = "ID";
		CbUnit2.SelectedValue = 0;
	}

	public void FillItemUnit3()
	{
		CbUnit3.DataSource = null;
		CbUnit3.DataSource = Items.GetUnits().ToList();
		CbUnit3.DisplayMember = "Name";
		CbUnit3.ValueMember = "ID";
		CbUnit3.SelectedValue = 0;
	}

	public void FillItemType()
	{
		CbItemtype.DataSource = Items.GetItemType();
		CbItemtype.DisplayMember = "TypeName";
		CbItemtype.ValueMember = "SN";
	}

	public void GetStores()
	{
		CBStore.DataSource = db.Account_Stores.Where((Account_Stores x) => x.BranchID == (int?)LoginDetails.BranchID).ToList();
		CBStore.DisplayMember = "Store_Name";
		CBStore.ValueMember = "ID";
		comboPrint.SelectedValue = 1;
	}

	public void FillItemCat()
	{
		CBCat.DataSource = null;
		CBCat.DataSource = Items.GetCats();
		CBCat.DisplayMember = "Name";
		CBCat.ValueMember = "ID";
		CBCat.SelectedValue = 1;
	}

	public void FillItemClass()
	{
		CBClass.DataSource = null;
		CBClass.DataSource = Items.GetItemCalass();
		CBClass.DisplayMember = "Name";
		CBClass.ValueMember = "ID";
	}

	public void FillItemCompany()
	{
		CBCompany.DataSource = null;
		CBCompany.DataSource = Items.GetItemCompany();
		CBCompany.DisplayMember = "Name";
		CBCompany.ValueMember = "ID";
	}

	public void FillItemGuarantee()
	{
		CBGuarantee.DataSource = null;
		CBGuarantee.DataSource = Items.GetGuarantee();
		CBGuarantee.DisplayMember = "Name";
		CBGuarantee.ValueMember = "ID";
	}

	public void FillItemCountry()
	{
		CBCountry.DataSource = null;
		CBCountry.DataSource = Items.GetCountry();
		CBCountry.DisplayMember = "Name";
		CBCountry.ValueMember = "ID";
	}

	private void NewFrm()
	{
		ClearControls();
		TxtItemCode.Focus();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		BtnPrint.Enabled = false;
		CpUnit1.Enabled = true;
		CbUnit2.Enabled = true;
		datatable.Rows.Clear();
		dtCurrentTable.Rows.Clear();
		gridControl1.Enabled = true;
		gridView1.OptionsBehavior.Editable = false;
		DataRow dataRow = null;
		dataRow = datatable.NewRow();
		dataRow["كود_الصنف"] = string.Empty;
		dataRow["أسم الصنف"] = string.Empty;
		dataRow["الوحدة"] = string.Empty;
		dataRow["الكمية"] = 0;
		dataRow["ظ….التكلفة"] = 0.0;
		dataRow["الاجمالى"] = 0.0;
		dataRow["أسم_الوحدة"] = "الصغرى";
		dataRow["المخزن"] = Pur.GetStoreName();
		dataRow["رقم المخزن"] = Pur.GetStoreID();
		dataRow["تم"] = 0;
		datatable.Rows.Add(dataRow);
		TapPrice.SelectedTabPage = xtraTabPage1;
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 8))
		{
			int num = 0;
			TapPrice.SelectedTabPage = xtraTabPage5;
			int unit = Convert.ToInt32(CpUnit1.SelectedValue);
			int unit2 = Convert.ToInt32(CbUnit2.SelectedValue);
			int unit3 = Convert.ToInt32(CbUnit3.SelectedValue);
			int itemType = Convert.ToInt32(CbItemtype.SelectedValue);
			int classID = Convert.ToInt32(CBClass.SelectedValue);
			int companyID = Convert.ToInt32(CBCompany.SelectedValue);
			int catID = Convert.ToInt32(CBCat.SelectedValue);
			int guaranteeID = Convert.ToInt32(CBGuarantee.SelectedValue);
			int countryID = Convert.ToInt32(CBCountry.SelectedValue);
			bool isVat = CHVat.Checked;
			decimal value = NVat.Value;
			decimal value2 = NTobacco.Value;
			decimal numOffer = Convert.ToDecimal(TxtNumOffer.Value);
			decimal priceOffer = Convert.ToDecimal(TxtPriceOffer.Value);
			decimal price1unit = Math.Round(NPrice1unit1.Value, DecimalNum);
			decimal price2unit = Math.Round(NPrice2unit1.Value, DecimalNum);
			decimal price3unit = Math.Round(NPrice3unit1.Value, DecimalNum);
			decimal value3 = nlastpriceunit1.Value;
			decimal value4 = NPriceExportunit1.Value;
			decimal value5 = NAverg.Value;
			int requestunit = Convert.ToInt32(nRequestunit1.Value);
			string smallBarcode = TxtBarcodeSmall1.Text;
			string smallBarcode2 = TxtBarcodeSmall2.Text;
			string smallBarcode3 = TxtBarcodeSmall3.Text;
			decimal price1unit2 = Math.Round(NPrice1unit2.Value, DecimalNum);
			decimal price2unit2 = Math.Round(NPrice2unit2.Value, DecimalNum);
			decimal price3unit2 = Math.Round(NPrice3unit2.Value, DecimalNum);
			decimal value6 = nlastpriceunit2.Value;
			decimal value7 = NPriceExportunit2.Value;
			decimal value8 = NPriceMowazaunit2.Value;
			int requestunit2 = Convert.ToInt32(nRequestunit2.Value);
			decimal value9 = nConvertUnit2.Value;
			string mediumBarcode = TxtBarcodeMedium1.Text;
			string mediumBarcode2 = TxtBarcodeMedium2.Text;
			string mediumBarcode3 = TxtBarcodeMedium3.Text;
			decimal price1unit3 = Math.Round(NPrice1unit3.Value, DecimalNum);
			decimal price2unit3 = Math.Round(NPrice2unit3.Value, DecimalNum);
			decimal price3unit3 = Math.Round(NPrice3unit3.Value, DecimalNum);
			decimal value10 = nlastpriceunit3.Value;
			decimal value11 = NPriceExportunit3.Value;
			decimal value12 = NPriceMowazaunit3.Value;
			int requestunit3 = Convert.ToInt32(nRequestunit3.Value);
			string bigBarcode = TxtBarcodeBig1.Text;
			string bigBarcode2 = TxtBarcodeBig2.Text;
			string bigBarcode3 = TxtBarcodeBig3.Text;
			decimal value13 = nConvertUnit3.Value;
			int unitTafqitID = 1;
			if (RBUnitTafqit1.Checked)
			{
				unitTafqitID = 1;
			}
			if (RBUnitTafqit2.Checked)
			{
				unitTafqitID = 2;
			}
			if (RBUnitTafqit3.Checked)
			{
				unitTafqitID = 3;
			}
			Item_Groups item_Groups = class_Group.GetAll().FirstOrDefault((Item_Groups s) => s.Name == CBCat.Text);
			if (item_Groups == null)
			{
				MessageBox.Show("لم يتم إختيار المجموعات");
				return;
			}
			if (string.IsNullOrWhiteSpace(TxtItemCode.Text) && MessageBox.Show("هل تريد إنشاء باركود جديد؟", "إنشاء الباركود", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				long num2 = 0L;
				try
				{
					num2 = db.Item_Items.Select((Item_Items s) => s.ItemId).DefaultIfEmpty(0).Max();
					num2++;
				}
				catch
				{
					num2++;
				}
				TxtItemCode.Text = num2.ToString();
			}
			if (TxtItemCode.Text != string.Empty && (TxtItemName.Text != string.Empty || !string.IsNullOrWhiteSpace(TxtitemnameEn.Text)))
			{
				if (TxtBarcodeSmall1.Text != string.Empty)
				{
					int num3 = Items.CheckBarcode(TxtBarcodeSmall1.Text);
					if (num3 > 0)
					{
						MessageBox.Show("لا يمكن التكرار طŒ رقم الباركود ( " + TxtBarcodeSmall1.Text + " ) مسجل لدينا من قبل من فضلك اعد المحاولة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3);
						TxtBarcodeSmall1.Focus();
						return;
					}
				}
				if (TxtBarcodeMedium1.Text != string.Empty)
				{
					int num4 = Items.CheckBarcode(TxtBarcodeMedium1.Text);
					if (num4 > 0)
					{
						MessageBox.Show("لا يمكن التكرار طŒ رقم الباركود ( " + TxtBarcodeMedium1.Text + " ) مسجل لدينا من قبل من فضلك اعد المحاولة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3);
						TxtBarcodeMedium1.Focus();
						return;
					}
				}
				if (TxtBarcodeBig1.Text != string.Empty)
				{
					int num5 = Items.CheckBarcode(TxtBarcodeBig1.Text);
					if (num5 > 0)
					{
						MessageBox.Show("لا يمكن التكرار طŒ رقم الباركود ( " + TxtBarcodeBig1.Text + " ) مسجل لدينا من قبل من فضلك اعد المحاولة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3);
						TxtBarcodeBig1.Focus();
						return;
					}
				}
				if (Convert.ToInt32(CbItemtype.SelectedValue) == 3)
				{
					FiterDatatableItemsUpdate();
					if (datatable.Rows.Count == 0)
					{
						MessageBox.Show("عفوا، هذا الصنف نوعه مكون \r\n لابد من ادخال صنف واحد علي الاقل ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						insertnewrow();
						TapPrice.SelectedTabPage = xtraTabPage4;
						gridControl1.Focus();
						return;
					}
				}
				int num6 = CheckSmallUnitValue();
				if (num6 <= 0)
				{
					return;
				}
				int num7 = CheckMedeUnitValue();
				if (num7 <= 0)
				{
					return;
				}
				int num8 = CheckbigUnitValue();
				if (num8 <= 0)
				{
					return;
				}
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtItemName.Text + " ) الي الاصناف", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					string empty = string.Empty;
					empty = ((pictureEdit1.Image != null) ? ImageToBase64(pictureEdit1.Image, ImageFormat.Png) : string.Empty);
					num = Items.InsertNewItem(TxtItemCode.Text, TxtItemName.Text, TxtitemnameEn.Text, catID, classID, companyID, itemType, guaranteeID, countryID, unit, smallBarcode, smallBarcode2, smallBarcode3, unit2, value9, mediumBarcode, mediumBarcode2, mediumBarcode3, unit3, value13, bigBarcode, bigBarcode2, bigBarcode3, price1unit, price2unit, price3unit, value3, value4, value5, requestunit, TxtLocaton1.Text, price1unit2, price2unit2, price3unit2, value6, value7, value8, requestunit2, TxtLocaton2.Text, price1unit3, price2unit3, price3unit3, value10, value11, value12, requestunit3, TxtLocaton3.Text, empty, isVat, value, TxtNotes.Text, CBISOffer.Checked, numOffer, priceOffer, NDiscItem.Value, Npharmacist.Value, NDoctor.Value, CHISItemMizan.Checked, unitTafqitID, Convert.ToInt32(CBStore.SelectedValue), NQty.Value, value2, TxtVatCode.SelectedValue.ToString(), TxtExemptionReason.SelectedValue.ToString());
					if (num > 0)
					{
						int num9 = Convert.ToInt32(CbItemtype.SelectedValue);
						if (num9 == 3)
						{
							FiterDatatableItems();
							if (dtCurrentTable.Rows.Count > 0)
							{
								Pur.InsertItems_Complant(dtCurrentTable, TxtItemCode.Text);
							}
						}
						LblItmID.Text = num.ToString();
						MessageBox.Show("تم حفظ بيانات  الاصناف ( " + TxtItemName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						BtnNew.Enabled = true;
						BtnPrint.Enabled = true;
						TapPrice.SelectedTabPage = xtraTabPage1;
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات كود الصنف بنفس الاسم ( " + TxtItemCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtItemCode.Focus();
					break;
				}
			}
			else if (TxtItemCode.Text == string.Empty)
			{
				MessageBox.Show("عفوا، من فضلك ادخل كود الصنف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				TxtItemCode.Focus();
			}
			else if (TxtItemName.Text == string.Empty)
			{
				MessageBox.Show("عفوا، من فضلك ادخل اسم الصنف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				TxtItemName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 8))
		{
			if (BtnEdit.Text == "تعديل Edit")
			{
				BtnEdit.Text = "حفظ Save";
				CpUnit1.Enabled = true;
				CbUnit2.Enabled = true;
				return;
			}
			BtnEdit.Text = "تعديل Edit";
			CpUnit1.Enabled = false;
			CbUnit2.Enabled = false;
			int num = 0;
			int unit = Convert.ToInt32(CpUnit1.SelectedValue);
			int unit2 = Convert.ToInt32(CbUnit2.SelectedValue);
			int unit3 = Convert.ToInt32(CbUnit3.SelectedValue);
			int itemType = Convert.ToInt32(CbItemtype.SelectedValue);
			int classID = Convert.ToInt32(CBClass.SelectedValue);
			int companyID = Convert.ToInt32(CBCompany.SelectedValue);
			int catID = Convert.ToInt32(CBCat.SelectedValue);
			int guaranteeID = Convert.ToInt32(CBGuarantee.SelectedValue);
			int countryID = Convert.ToInt32(CBCountry.SelectedValue);
			bool isVat = CHVat.Checked;
			decimal value = NVat.Value;
			decimal value2 = NTobacco.Value;
			decimal numOffer = Convert.ToDecimal(TxtNumOffer.Value);
			decimal priceOffer = Convert.ToDecimal(TxtPriceOffer.Value);
			decimal price1unit = Math.Round(NPrice1unit1.Value, DecimalNum);
			decimal price2unit = Math.Round(NPrice2unit1.Value, DecimalNum);
			decimal price3unit = Math.Round(NPrice3unit1.Value, DecimalNum);
			decimal value3 = nlastpriceunit1.Value;
			decimal value4 = NPriceExportunit1.Value;
			decimal value5 = NAverg.Value;
			int requestunit = Convert.ToInt32(nRequestunit1.Value);
			string smallBarcode = TxtBarcodeSmall1.Text;
			string smallBarcode2 = TxtBarcodeSmall2.Text;
			string smallBarcode3 = TxtBarcodeSmall3.Text;
			decimal price1unit2 = Math.Round(NPrice1unit2.Value, DecimalNum);
			decimal price2unit2 = Math.Round(NPrice2unit2.Value, DecimalNum);
			decimal price3unit2 = Math.Round(NPrice3unit2.Value, DecimalNum);
			decimal value6 = nlastpriceunit2.Value;
			decimal value7 = NPriceExportunit2.Value;
			decimal value8 = NPriceMowazaunit2.Value;
			int requestunit2 = Convert.ToInt32(nRequestunit2.Value);
			decimal value9 = nConvertUnit2.Value;
			string mediumBarcode = TxtBarcodeMedium1.Text;
			string mediumBarcode2 = TxtBarcodeMedium2.Text;
			string mediumBarcode3 = TxtBarcodeMedium3.Text;
			decimal price1unit3 = Math.Round(NPrice1unit3.Value, DecimalNum);
			decimal price2unit3 = Math.Round(NPrice2unit3.Value, DecimalNum);
			decimal price3unit3 = Math.Round(NPrice3unit3.Value, DecimalNum);
			decimal value10 = nlastpriceunit3.Value;
			decimal value11 = NPriceExportunit3.Value;
			decimal value12 = NPriceMowazaunit3.Value;
			int requestunit3 = Convert.ToInt32(nRequestunit3.Value);
			string bigBarcode = TxtBarcodeBig1.Text;
			string bigBarcode2 = TxtBarcodeBig2.Text;
			string bigBarcode3 = TxtBarcodeBig3.Text;
			decimal value13 = nConvertUnit3.Value;
			int unitTafqitID = 1;
			if (RBUnitTafqit1.Checked)
			{
				unitTafqitID = 1;
			}
			if (RBUnitTafqit2.Checked)
			{
				unitTafqitID = 2;
			}
			if (RBUnitTafqit3.Checked)
			{
				unitTafqitID = 3;
			}
			if (TxtItemCode.Text != string.Empty && TxtItemName.Text != string.Empty)
			{
				if (Convert.ToInt32(CbItemtype.SelectedValue) == 3)
				{
					FiterDatatableItemsUpdate();
					if (datatable.Rows.Count == 0)
					{
						MessageBox.Show("عفوا، هذا الصنف نوعه مكون \r\n لابد من ادخال صنف واحد علي الاقل ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						insertnewrow();
						TapPrice.SelectedTabPage = xtraTabPage4;
						gridControl1.Focus();
						return;
					}
				}
				int num2 = CheckSmallUnitValue();
				if (num2 <= 0)
				{
					return;
				}
				int num3 = CheckMedeUnitValue();
				if (num3 <= 0)
				{
					return;
				}
				int num4 = CheckbigUnitValue();
				if (num4 <= 0)
				{
					return;
				}
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtItemName.Text + " ) الي الاصناف", "تأكيد التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					string empty = string.Empty;
					empty = ((pictureEdit1.Image != null) ? ImageToBase64(pictureEdit1.Image, ImageFormat.Png) : string.Empty);
					string vatCode = TxtVatCode.SelectedValue?.ToString() ?? "";
					string exemptionReason = TxtExemptionReason.SelectedValue?.ToString() ?? "";
					num = Items.EditItem(Convert.ToInt32(LblItmID.Text), TxtItemCode.Text, TxtItemName.Text, TxtitemnameEn.Text, catID, classID, companyID, itemType, guaranteeID, countryID, unit, smallBarcode, smallBarcode2, smallBarcode3, unit2, value9, mediumBarcode, mediumBarcode2, mediumBarcode3, unit3, value13, bigBarcode, bigBarcode2, bigBarcode3, price1unit, price2unit, price3unit, value3, value4, value5, requestunit, TxtLocaton1.Text, price1unit2, price2unit2, price3unit2, value6, value7, value8, requestunit2, TxtLocaton2.Text, price1unit3, price2unit3, price3unit3, value10, value11, value12, requestunit3, TxtLocaton3.Text, empty, isVat, value, TxtNotes.Text, CBISOffer.Checked, numOffer, priceOffer, NDiscItem.Value, Npharmacist.Value, NDoctor.Value, CHISItemMizan.Checked, unitTafqitID, Convert.ToInt32(CBStore.SelectedValue), NQty.Value, value2, vatCode, exemptionReason);
					if (num > 0)
					{
						FiterDatatableItems();
						Items.deleteItemsComplanyByItemId(TxtItemCode.Text);
						Pur.InsertItems_Complant(dtCurrentTable, TxtItemCode.Text);
						MessageBox.Show("تم تعديل بيانات الاصناف ( " + TxtItemName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						BtnNew.Enabled = true;
						BtnPrint.Enabled = true;
						TapPrice.SelectedTabPage = xtraTabPage1;
					}
					else
					{
						MessageBox.Show("تم تعديل بيانات كود الصنف بنفس الاسم ( " + TxtItemCode.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				}
				case DialogResult.No:
					TxtItemCode.Focus();
					break;
				}
			}
			else if (TxtItemCode.Text == string.Empty)
			{
				MessageBox.Show("عفوا، من فضلك ادخل كود الصنف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				TxtItemCode.Focus();
			}
			else if (TxtItemName.Text == string.Empty)
			{
				MessageBox.Show("عفوا، من فضلك ادخل اسم الصنف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				TxtItemName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 8))
		{
			int num = 0;
			if (TxtItemCode.Text != string.Empty)
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtItemName.Text + " ) الي الاصناف", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = Items.DeleteItem(TxtItemCode.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات الاصناف ( " + TxtItemName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						BtnNew.Enabled = true;
						BtnPrint.Enabled = false;
						ClearControls();
						TapPrice.SelectedTabPage = xtraTabPage1;
					}
					else
					{
						MessageBox.Show("عفوا، لايمكن حذف هذا الصنف ( " + TxtItemCode.Text + " ) لوجود حركات له في قاعدة البيانات", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtItemCode.Focus();
					break;
				}
			}
			else if (TxtItemCode.Text == string.Empty)
			{
				MessageBox.Show("عفوا، من فضلك ادخل كود الصنف", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				TxtItemCode.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void Print()
	{
		string nameServer = LoginDetails.nameServer;
		string databaseServer = LoginDetails.databaseServer;
		string userNameServer = LoginDetails.userNameServer;
		string passServer = LoginDetails.passServer;
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + nameServer + ";Initial Catalog=" + databaseServer + ";Persist Security Info=True;User ID=" + userNameServer + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["BarcodeItem"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Select_BarCode", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@ItemCode", TxtItemCode.Text);
		sqlDataAdapter.Fill(dataSetALL.BarcodeItem);
		sqlConnection.Close();
		StiReport stiReport = new StiReport();
		stiReport.Load(Application.StartupPath + "\\Reports\\RptBarcode.mrt");
		stiReport.RegData(dataSetALL.BarcodeItem);
		stiReport.Dictionary.Synchronize();
		stiReport.Compile();
		stiReport.Render();
		stiReport.Print();
	}

	public void ClearControls()
	{
		LblItmID.Text = "0";
		BindDropDownList();
		TxtItemCode.Text = string.Empty;
		TxtItemName.Text = string.Empty;
		TxtitemnameEn.Text = string.Empty;
		TxtNotes.Text = string.Empty;
		TxtLocaton1.Text = string.Empty;
		TxtLocaton2.Text = string.Empty;
		TxtLocaton3.Text = string.Empty;
		CHVat.Checked = true;
		NPrice1unit1.Value = 0m;
		NPrice1unit2.Value = 0m;
		NPrice1unit3.Value = 0m;
		NPrice2unit1.Value = 0m;
		NPrice2unit2.Value = 0m;
		NPrice2unit3.Value = 0m;
		NPrice3unit1.Value = 0m;
		NPrice3unit2.Value = 0m;
		NPrice3unit3.Value = 0m;
		NQty.Value = 0m;
		TxtPerUnit1Price1.Value = 0m;
		TxtPerUnit1Price2.Value = 0m;
		TxtPerUnit1Price3.Value = 0m;
		TxtPerUnit2Price1.Value = 0m;
		TxtPerUnit2Price2.Value = 0m;
		TxtPerUnit2Price3.Value = 0m;
		TxtPerUnit3Price1.Value = 0m;
		TxtPerUnit3Price2.Value = 0m;
		TxtPerUnit3Price3.Value = 0m;
		nlastpriceunit1.Value = 0m;
		nlastpriceunit2.Value = 0m;
		nlastpriceunit3.Value = 0m;
		NPriceExportunit1.Value = 0m;
		NPriceExportunit2.Value = 0m;
		NPriceExportunit3.Value = 0m;
		NAverg.Value = 0m;
		NPriceMowazaunit2.Value = 0m;
		NPriceMowazaunit3.Value = 0m;
		nRequestunit1.Value = 0m;
		nRequestunit2.Value = 0m;
		nRequestunit3.Value = 0m;
		TxtBarcodeSmall1.Text = string.Empty;
		TxtBarcodeSmall2.Text = string.Empty;
		TxtBarcodeSmall3.Text = string.Empty;
		TxtBarcodeMedium1.Text = string.Empty;
		TxtBarcodeMedium2.Text = string.Empty;
		TxtBarcodeMedium3.Text = string.Empty;
		TxtBarcodeBig1.Text = string.Empty;
		TxtBarcodeBig2.Text = string.Empty;
		TxtBarcodeBig3.Text = string.Empty;
		nConvertUnit2.Minimum = 0m;
		nConvertUnit3.Minimum = 0m;
		nConvertUnit2.Value = 0m;
		nConvertUnit3.Value = 0m;
		NVat.Value = LoginDetails.PerVat;
		NTobacco.Value = 0m;
		CBISOffer.Checked = false;
		TxtNumOffer.Enabled = false;
		TxtPriceOffer.Enabled = false;
		TxtNumOffer.Value = 0m;
		TxtPriceOffer.Value = 0m;
		CpUnit1.SelectedValue = 1;
		CbUnit2.SelectedValue = 0;
		CbUnit3.SelectedValue = 0;
		label80.Text = string.Empty;
		label31.Text = string.Empty;
		label81.Text = CpUnit1.SelectedText;
		LoginDetails.ItemcodeNewItem = null;
		CbUnit3.Enabled = false;
		pictureEdit1.Image = null;
		RBUnitTafqit1.Checked = true;
		RBUnitTafqit2.Checked = false;
		RBUnitTafqit3.Checked = false;
		nlastpriceunit1.Enabled = true;
	}

	public int CheckSmallUnitValue()
	{
		int result = 1;
		decimal value = NPrice1unit1.Value;
		decimal value2 = NPrice2unit1.Value;
		decimal value3 = NPrice3unit1.Value;
		decimal value4 = nlastpriceunit1.Value;
		if (NPrice2unit1.Value > 0m && value < value2)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة الصغري اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice2unit1.Focus();
			return result = 0;
		}
		if (NPrice3unit1.Value > 0m && value < value3)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الصغري اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice3unit1.Focus();
			return result = 0;
		}
		if (NPrice3unit1.Value > 0m && value2 < value3)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الصغري اكبر من سعر الجملة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice3unit1.Focus();
			return result = 0;
		}
		if (value4 > 0m && value4 > value)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون سعر القطاعي للوحدة الصغري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice1unit1.Focus();
			return result = 0;
		}
		if (NPrice2unit1.Value > 0m && value4 > value2)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة الصغري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice2unit1.Focus();
			return result = 0;
		}
		if (NPrice3unit1.Value > 0m && value4 > value3)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الصغري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
			NPrice3unit1.Focus();
			return result = 0;
		}
		return result;
	}

	public int CheckMedeUnitValue()
	{
		int result = 1;
		int num = Convert.ToInt32(CbUnit2.SelectedValue);
		if (num > 0)
		{
			decimal value = NPrice1unit1.Value;
			decimal value2 = NPrice1unit2.Value;
			decimal value3 = NPrice2unit2.Value;
			decimal value4 = NPrice3unit2.Value;
			decimal value5 = nlastpriceunit2.Value;
			if (NPrice2unit2.Value > 0m && value2 < value3)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة المتوسطة اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice2unit2.Focus();
				return result = 0;
			}
			if (NPrice3unit2.Value > 0m && value2 < value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة المتوسطة اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit2.Focus();
				return result = 0;
			}
			if (NPrice3unit2.Value > 0m && value3 < value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة المتوسطة اكبر من سعر الجملة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit2.Focus();
				return result = 0;
			}
			if (NPrice1unit2.Value > 0m && value5 > value2)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر القطاعي للوحدة المتوسطة اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice1unit2.Focus();
				return result = 0;
			}
			if (NPrice2unit2.Value > 0m && value5 > value3)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة المتوسطة اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice2unit2.Focus();
				return result = 0;
			}
			if (NPrice3unit2.Value > 0m && value5 > value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة المتوسطة اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit2.Focus();
				return result = 0;
			}
			if (value >= value2)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لا يمكن ان يكون سعر بيع القطاعي للوحدة الصغري اقل من او يساوي سعر بيع القطاعي للوحدة المتوسطة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice1unit2.Focus();
				return result = 0;
			}
		}
		return result;
	}

	public int CheckbigUnitValue()
	{
		int result = 1;
		int num = Convert.ToInt32(CbUnit3.SelectedValue);
		if (num > 0)
		{
			decimal value = NPrice1unit2.Value;
			decimal value2 = NPrice1unit3.Value;
			decimal value3 = NPrice2unit3.Value;
			decimal value4 = NPrice3unit3.Value;
			decimal value5 = nlastpriceunit3.Value;
			if (NPrice2unit3.Value > 0m && value2 < value3)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة الكبري اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice2unit3.Focus();
				return result = 0;
			}
			if (NPrice3unit3.Value > 0m && value2 < value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الكبري اكبر من السعر القطاعي", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit3.Focus();
				return result = 0;
			}
			if (NPrice3unit3.Value > 0m && value3 < value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الكبري اكبر من سعر الجملة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit3.Focus();
				return result = 0;
			}
			if (NPrice1unit3.Value > 0m && value5 > value2)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر القطاعي للوحدة الكبري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice1unit3.Focus();
				return result = 0;
			}
			if (NPrice2unit3.Value > 0m && value5 > value3)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون سعر الجملة للوحدة الكبري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice2unit3.Focus();
				return result = 0;
			}
			if (NPrice3unit3.Value > 0m && value5 > value4)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لايمكن ان يكون جملة الجملة للوحدة الكبري اقل من اخر تكلفة", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice3unit3.Focus();
				return result = 0;
			}
			if (value >= value2)
			{
				TapPrice.SelectedTabPage = xtraTabPage2;
				MessageBox.Show("لا يمكن ان يكون سعر بيع القطاعي للوحدة المتوسطة اقل من او يساوي سعر بيع القطاعي للوحدة الكبري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
				NPrice1unit3.Focus();
				return result = 0;
			}
		}
		return result;
	}

	private void TxtDigital()
	{
		numericUpDown1.DecimalPlaces = DecimalNum;
		nConvertUnit2.DecimalPlaces = DecimalNum;
		nConvertUnit3.DecimalPlaces = DecimalNum;
		NPrice1unit1.DecimalPlaces = DecimalNum;
		NPrice2unit1.DecimalPlaces = DecimalNum;
		NPrice3unit1.DecimalPlaces = DecimalNum;
		nlastpriceunit1.DecimalPlaces = DecimalNum;
		NPriceExportunit1.DecimalPlaces = DecimalNum;
		NAverg.DecimalPlaces = DecimalNum;
		nRequestunit1.DecimalPlaces = DecimalNum;
		NPrice1unit2.DecimalPlaces = DecimalNum;
		NPrice2unit2.DecimalPlaces = DecimalNum;
		NPrice3unit2.DecimalPlaces = DecimalNum;
		nlastpriceunit2.DecimalPlaces = DecimalNum;
		NPriceExportunit2.DecimalPlaces = DecimalNum;
		NPriceMowazaunit2.DecimalPlaces = DecimalNum;
		NPrice1unit3.DecimalPlaces = DecimalNum;
		NPrice2unit3.DecimalPlaces = DecimalNum;
		NPrice3unit3.DecimalPlaces = DecimalNum;
		nlastpriceunit3.DecimalPlaces = DecimalNum;
		NPriceExportunit3.DecimalPlaces = DecimalNum;
		NPriceMowazaunit3.DecimalPlaces = DecimalNum;
		TxtNumOffer.DecimalPlaces = DecimalNum;
		TxtPriceOffer.DecimalPlaces = DecimalNum;
		NVat.DecimalPlaces = DecimalNum;
		TxtPerUnit1Price1.DecimalPlaces = DecimalNum;
		TxtPerUnit1Price2.DecimalPlaces = DecimalNum;
		TxtPerUnit1Price3.DecimalPlaces = DecimalNum;
		TxtPerUnit2Price1.DecimalPlaces = DecimalNum;
		TxtPerUnit2Price2.DecimalPlaces = DecimalNum;
		TxtPerUnit2Price3.DecimalPlaces = DecimalNum;
		TxtPerUnit3Price1.DecimalPlaces = DecimalNum;
		TxtPerUnit3Price2.DecimalPlaces = DecimalNum;
		TxtPerUnit3Price3.DecimalPlaces = DecimalNum;
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
		CBCat.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		CBCat.AutoCompleteSource = AutoCompleteSource.ListItems;
		CBCompany.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		CBCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode.ToString() == "F3")
		{
			GetItemsControls();
		}
		else if (e.KeyCode.ToString() == "F4")
		{
			NewFrm();
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
		else if (e.KeyCode.ToString() == "F8" && TxtItemCode.Text != string.Empty)
		{
			GetItemsControlsByCode();
		}
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		NewFrm();
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

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnImage_Click_1(object sender, EventArgs e)
	{
		openFileDialog1.Title = "Choose Image";
		openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			Image image = new Bitmap(openFileDialog1.FileName);
			pictureEdit1.Image = image;
		}
	}

	private void BtnImage_Click(object sender, EventArgs e)
	{
		pictureEdit1.EditValue = null;
	}

	private void BtnPrint_Click(object sender, EventArgs e)
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 13))
		{
			if (TxtItemCode.Text != string.Empty)
			{
				LoginDetails.ItemCode = TxtItemCode.Text;
				FrmPrintBarcode frmPrintBarcode = new FrmPrintBarcode();
				frmPrintBarcode.ShowDialog();
				LoginDetails.ItemCode = string.Empty;
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void BtnSearch_Click(object sender, EventArgs e)
	{
		FrmSearchByBarcode frmSearchByBarcode = new FrmSearchByBarcode();
		frmSearchByBarcode.ShowDialog();
		if (frmSearchByBarcode.BracodeItem != string.Empty)
		{
			TxtItemCode.Text = frmSearchByBarcode.BracodeItem;
			GetItemsControlsByCode();
		}
	}

	private void TxtItemCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		GetItemsControls();
	}

	public int RandomNumber(int min, int max)
	{
		Random random = new Random();
		return random.Next(min, max);
	}

	private void CbUnit2_SelectionChangeCommitted(object sender, EventArgs e)
	{
		int num = Convert.ToInt32(CbUnit2.SelectedValue);
		if (num > 0)
		{
			NPrice1unit2.Enabled = true;
			NPrice2unit2.Enabled = true;
			NPrice3unit2.Enabled = true;
			nConvertUnit2.Enabled = true;
			nlastpriceunit2.Enabled = true;
			nRequestunit2.Enabled = true;
			TxtBarcodeMedium1.Enabled = true;
			CbUnit3.Enabled = true;
			TxtPerUnit1Price1.Enabled = true;
			TxtPerUnit1Price2.Enabled = true;
			TxtPerUnit1Price3.Enabled = true;
			TxtPerUnit2Price1.Enabled = true;
			TxtPerUnit2Price2.Enabled = true;
			TxtPerUnit2Price3.Enabled = true;
			TxtPerUnit3Price1.Enabled = true;
			TxtPerUnit3Price2.Enabled = true;
			TxtPerUnit3Price3.Enabled = true;
			FillItemUnit3();
			decimal value = nlastpriceunit1.Value;
			decimal num2 = Convert.ToDecimal(nConvertUnit2.Value);
			nlastpriceunit2.Value = value * num2;
			string text = CbUnit2.Text;
			string text2 = CpUnit1.Text;
			label80.Text = string.Empty + text + " = " + num2 + " " + text2 + string.Empty;
		}
		else
		{
			NPrice1unit2.Enabled = false;
			NPrice2unit2.Enabled = false;
			NPrice3unit2.Enabled = false;
			nConvertUnit2.Enabled = false;
			nlastpriceunit2.Enabled = false;
			nRequestunit2.Enabled = false;
			TxtBarcodeMedium1.Enabled = false;
			CbUnit3.Enabled = false;
			NPrice1unit3.Enabled = false;
			NPrice2unit3.Enabled = false;
			nConvertUnit3.Enabled = false;
			nlastpriceunit3.Enabled = false;
			nRequestunit3.Enabled = false;
			TxtBarcodeBig1.Enabled = false;
			TxtPerUnit1Price1.Enabled = false;
			TxtPerUnit1Price2.Enabled = false;
			TxtPerUnit1Price3.Enabled = false;
			TxtPerUnit2Price1.Enabled = false;
			TxtPerUnit2Price2.Enabled = false;
			TxtPerUnit2Price3.Enabled = false;
			TxtPerUnit3Price1.Enabled = false;
			TxtPerUnit3Price2.Enabled = false;
			TxtPerUnit3Price3.Enabled = false;
			nlastpriceunit2.Value = 0m;
			FillItemUnit3();
			label80.Text = string.Empty;
		}
	}

	private void CbUnit3_SelectionChangeCommitted(object sender, EventArgs e)
	{
	}

	private void nConvertUnit2_ValueChanged(object sender, EventArgs e)
	{
		decimal value = nlastpriceunit1.Value;
		decimal num = Convert.ToDecimal(nConvertUnit2.Value);
		nlastpriceunit2.Value = value * num;
		string text = CbUnit2.Text;
		string text2 = CpUnit1.Text;
		label80.Text = string.Empty + text + " = " + Math.Round(num, 2) + " " + text2 + string.Empty;
		int num2 = Convert.ToInt32(CbUnit3.SelectedValue);
		if (num2 > 0)
		{
			decimal value2 = nlastpriceunit2.Value;
			decimal num3 = Convert.ToDecimal(nConvertUnit3.Value);
			nlastpriceunit3.Value = value2 * num3;
			string text3 = CbUnit2.Text;
			string text4 = CbUnit3.Text;
			string text5 = CbUnit3.Text;
			string text6 = CbUnit2.Text;
			decimal num4 = Convert.ToDecimal(nConvertUnit3.Value);
			label31.Text = string.Empty + text5 + " = " + num3 + " " + text6 + " = " + num * num4 + " " + text2 + string.Empty;
		}
	}

	private void nlastpriceunit2_ValueChanged(object sender, EventArgs e)
	{
		decimal value = nlastpriceunit2.Value;
		if (value > 0m)
		{
			decimal num = Convert.ToDecimal(nConvertUnit2.Value);
			nlastpriceunit1.Value = Math.Round(value / num, DecimalNum);
		}
	}

	private void nlastpriceunit1_ValueChanged(object sender, EventArgs e)
	{
		if (!_isUpdating)
		{
			_isUpdating = true;
			int num = Convert.ToInt32(CbUnit2.SelectedValue);
			if (num > 0)
			{
				decimal value = nlastpriceunit1.Value;
				decimal num2 = Convert.ToDecimal(nConvertUnit2.Value);
				nlastpriceunit2.Value = Math.Round(value * num2, DecimalNum);
			}
			int num3 = Convert.ToInt32(CbUnit3.SelectedValue);
			if (num3 > 0)
			{
				decimal value2 = nlastpriceunit2.Value;
				decimal num4 = Convert.ToDecimal(nConvertUnit3.Value);
				nlastpriceunit3.Value = Math.Round(value2 * num4, DecimalNum);
			}
			_isUpdating = false;
		}
	}

	private void nConvertUnit3_ValueChanged(object sender, EventArgs e)
	{
		decimal value = nlastpriceunit2.Value;
		decimal num = Convert.ToDecimal(nConvertUnit3.Value);
		string text = CbUnit3.Text;
		string text2 = CbUnit2.Text;
		string text3 = CpUnit1.Text;
		decimal num2 = Convert.ToDecimal(nConvertUnit2.Value);
		label31.Text = string.Empty + text + " = " + Math.Round(num, 2) + " " + text2 + " = " + Math.Round(num * num2, 2) + " " + text3 + string.Empty;
	}

	private void CpUnit1_SelectedIndexChanged(object sender, EventArgs e)
	{
		NPrice1unit1.Value = 0m;
		NPrice2unit1.Value = 0m;
		NPrice3unit1.Value = 0m;
		nlastpriceunit1.Value = 0m;
		nRequestunit1.Value = 0m;
		TxtBarcodeSmall1.Text = string.Empty;
		label81.Text = CpUnit1.Text;
		CbUnit2.Enabled = true;
		int num = Convert.ToInt32(CbItemtype.SelectedValue);
		if (num == 3)
		{
			GetTotalCost();
		}
	}

	private void CbUnit2_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(CbUnit2.SelectedValue) == 0)
			{
				NPrice1unit2.Value = 0m;
				NPrice2unit2.Value = 0m;
				NPrice3unit2.Value = 0m;
				NPriceExportunit2.Value = 0m;
				NPriceExportunit3.Value = 0m;
				NPriceMowazaunit2.Value = 0m;
				NPriceMowazaunit3.Value = 0m;
				NPriceExportunit2.Enabled = false;
				NPriceExportunit3.Enabled = false;
				NPriceMowazaunit2.Enabled = false;
				NPriceMowazaunit3.Enabled = false;
				nlastpriceunit2.Value = 0m;
				nRequestunit2.Value = 0m;
				nConvertUnit2.Minimum = Convert.ToDecimal("0");
				nConvertUnit2.Value = 0m;
				TxtBarcodeMedium1.Text = string.Empty;
				TxtBarcodeMedium2.Text = string.Empty;
				TxtBarcodeMedium3.Text = string.Empty;
				label80.Text = string.Empty;
				NPrice1unit2.Enabled = false;
				NPrice2unit2.Enabled = false;
				NPrice3unit2.Enabled = false;
				nlastpriceunit2.Enabled = false;
				nRequestunit2.Enabled = false;
				nConvertUnit2.Enabled = false;
				TxtBarcodeMedium1.Enabled = false;
				TxtBarcodeMedium2.Enabled = false;
				TxtBarcodeMedium3.Enabled = false;
				NPrice1unit3.Value = 0m;
				NPrice2unit3.Value = 0m;
				NPrice3unit3.Value = 0m;
				nlastpriceunit3.Value = 0m;
				nRequestunit3.Value = 0m;
				nConvertUnit3.Minimum = Convert.ToDecimal("0");
				nConvertUnit3.Value = 0m;
				TxtBarcodeBig1.Text = string.Empty;
				TxtBarcodeBig2.Text = string.Empty;
				TxtBarcodeBig3.Text = string.Empty;
				label31.Text = string.Empty;
				CbUnit3.Enabled = false;
				NPrice1unit3.Enabled = false;
				NPrice2unit3.Enabled = false;
				NPrice3unit3.Enabled = false;
				nlastpriceunit3.Enabled = false;
				nRequestunit3.Enabled = false;
				nConvertUnit3.Enabled = false;
				TxtBarcodeBig1.Enabled = false;
				TxtBarcodeBig2.Enabled = false;
				TxtBarcodeBig3.Enabled = false;
				CbUnit3.Enabled = false;
				CbUnit3.SelectedValue = 0;
				TxtLocaton2.Enabled = false;
				TxtPerUnit2Price1.Enabled = false;
				TxtPerUnit2Price2.Enabled = false;
				TxtPerUnit2Price3.Enabled = false;
				TxtPerUnit3Price1.Enabled = false;
				TxtPerUnit3Price2.Enabled = false;
				TxtPerUnit3Price3.Enabled = false;
				TxtPerUnit2Price1.Value = 0m;
				TxtPerUnit2Price2.Value = 0m;
				TxtPerUnit2Price3.Value = 0m;
				TxtPerUnit3Price1.Value = 0m;
				TxtPerUnit3Price2.Value = 0m;
				TxtPerUnit3Price3.Value = 0m;
			}
			else
			{
				NPrice1unit2.Value = 0m;
				NPrice2unit2.Value = 0m;
				NPrice3unit2.Value = 0m;
				NPriceExportunit2.Value = 0m;
				NPriceExportunit3.Value = 0m;
				NPriceMowazaunit2.Value = 0m;
				NPriceMowazaunit3.Value = 0m;
				NPriceExportunit2.Enabled = true;
				NPriceExportunit3.Enabled = false;
				NPriceMowazaunit2.Enabled = true;
				NPriceMowazaunit3.Enabled = false;
				nlastpriceunit2.Value = 0m;
				nRequestunit2.Value = 0m;
				nConvertUnit2.Minimum = Convert.ToDecimal("1.1");
				nConvertUnit2.Value = 2m;
				TxtBarcodeMedium1.Text = string.Empty;
				TxtBarcodeMedium2.Text = string.Empty;
				TxtBarcodeMedium3.Text = string.Empty;
				TxtBarcodeMedium1.Enabled = true;
				TxtBarcodeMedium2.Enabled = true;
				TxtBarcodeMedium3.Enabled = true;
				CbUnit2.Enabled = true;
				decimal value = nlastpriceunit1.Value;
				decimal num = Convert.ToDecimal(nConvertUnit2.Value);
				nlastpriceunit2.Value = value * num;
				string text = CbUnit2.Text;
				string text2 = CpUnit1.Text;
				label80.Text = " " + text + " = " + num + " " + text2 + " ";
				NPrice1unit2.Enabled = true;
				NPrice2unit2.Enabled = true;
				NPrice3unit2.Enabled = true;
				nlastpriceunit2.Enabled = true;
				nRequestunit2.Enabled = true;
				nConvertUnit2.Enabled = true;
				NPrice1unit2.Enabled = true;
				NPrice2unit2.Enabled = true;
				NPrice3unit2.Enabled = true;
				CbUnit3.Enabled = true;
				TxtLocaton2.Enabled = true;
				TxtPerUnit2Price1.Enabled = true;
				TxtPerUnit2Price2.Enabled = true;
				TxtPerUnit2Price3.Enabled = true;
				TxtPerUnit3Price1.Enabled = true;
				TxtPerUnit3Price2.Enabled = true;
				TxtPerUnit3Price3.Enabled = true;
				TxtPerUnit2Price1.Value = 0m;
				TxtPerUnit2Price2.Value = 0m;
				TxtPerUnit2Price3.Value = 0m;
				TxtPerUnit3Price1.Value = 0m;
				TxtPerUnit3Price2.Value = 0m;
				TxtPerUnit3Price3.Value = 0m;
			}
			int num2 = Convert.ToInt32(CbItemtype.SelectedValue);
			if (num2 == 3)
			{
				GetTotalCost();
			}
		}
		catch (Exception)
		{
		}
	}

	private void CbUnit3_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(CbUnit3.SelectedValue) == 0)
			{
				NPrice1unit3.Value = 0m;
				NPrice2unit3.Value = 0m;
				NPrice3unit3.Value = 0m;
				TxtPerUnit3Price1.Value = 0m;
				TxtPerUnit3Price2.Value = 0m;
				TxtPerUnit3Price3.Value = 0m;
				nlastpriceunit3.Value = 0m;
				nRequestunit3.Value = 0m;
				NPriceExportunit3.Value = 0m;
				NPriceMowazaunit3.Value = 0m;
				NPriceExportunit3.Enabled = false;
				NPriceMowazaunit3.Enabled = false;
				nConvertUnit3.Minimum = Convert.ToDecimal("0");
				nConvertUnit3.Value = 0m;
				TxtBarcodeBig1.Text = string.Empty;
				TxtBarcodeBig2.Text = string.Empty;
				TxtBarcodeBig3.Text = string.Empty;
				NPrice1unit3.Enabled = false;
				NPrice2unit3.Enabled = false;
				NPrice3unit3.Enabled = false;
				nlastpriceunit3.Enabled = false;
				nRequestunit3.Enabled = false;
				nConvertUnit3.Enabled = false;
				TxtBarcodeBig1.Enabled = false;
				TxtBarcodeBig2.Enabled = false;
				TxtBarcodeBig3.Enabled = false;
				label31.Text = string.Empty;
				TxtLocaton3.Enabled = false;
				TxtPerUnit3Price1.Enabled = false;
				TxtPerUnit3Price2.Enabled = false;
				TxtPerUnit3Price3.Enabled = false;
			}
			else
			{
				decimal num = Convert.ToDecimal(nConvertUnit2.Value);
				string text = CbUnit3.Text;
				string text2 = CbUnit2.Text;
				string text3 = CpUnit1.Text;
				decimal num2 = Convert.ToDecimal(nConvertUnit2.Value);
				decimal num3 = Convert.ToDecimal(nConvertUnit3.Value);
				label31.Text = string.Empty + text + " = " + num3 + " " + text2 + " = " + num * num2 + " " + text3 + string.Empty;
				NPrice1unit3.Value = 0m;
				NPrice2unit3.Value = 0m;
				NPrice3unit3.Value = 0m;
				nRequestunit3.Value = 0m;
				NPriceExportunit3.Value = 0m;
				NPriceMowazaunit3.Value = 0m;
				TxtPerUnit3Price1.Value = 0m;
				TxtPerUnit3Price2.Value = 0m;
				TxtPerUnit3Price3.Value = 0m;
				NPriceExportunit3.Enabled = true;
				NPriceMowazaunit3.Enabled = true;
				nConvertUnit3.Minimum = Convert.ToDecimal("1.1");
				nConvertUnit3.Value = 2m;
				TxtBarcodeBig1.Text = string.Empty;
				TxtBarcodeBig2.Text = string.Empty;
				TxtBarcodeBig3.Text = string.Empty;
				decimal num4 = Math.Round(nlastpriceunit2.Value, DecimalNum);
				nlastpriceunit3.Value = Math.Round(num4 * num, DecimalNum);
				CbUnit3.Enabled = true;
				NPrice1unit3.Enabled = true;
				NPrice2unit3.Enabled = true;
				NPrice3unit3.Enabled = true;
				nlastpriceunit3.Enabled = true;
				nRequestunit3.Enabled = true;
				nConvertUnit3.Enabled = true;
				TxtBarcodeBig1.Enabled = true;
				TxtBarcodeBig2.Enabled = true;
				TxtBarcodeBig3.Enabled = true;
				CbUnit3.Enabled = true;
				TxtLocaton3.Enabled = true;
				TxtPerUnit3Price1.Enabled = true;
				TxtPerUnit3Price2.Enabled = true;
				TxtPerUnit3Price3.Enabled = true;
			}
			int num5 = Convert.ToInt32(CbItemtype.SelectedValue);
			if (num5 == 3)
			{
				GetTotalCost();
			}
		}
		catch (Exception)
		{
		}
	}

	private void nlastpriceunit3_ValueChanged(object sender, EventArgs e)
	{
		decimal value = nlastpriceunit3.Value;
		if (value > 0m)
		{
			int num = Convert.ToInt32(nConvertUnit3.Value);
			nlastpriceunit2.Value = value / (decimal)num;
		}
	}

	private void TxtBarcodeunit1_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(1, 9999998));
			if (Items.CheckBarcode(barcode, 1) == 0)
			{
				TxtBarcodeSmall1.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeSmall2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(1, 9999998));
			if (Items.CheckBarcode(barcode, 1) == 0)
			{
				TxtBarcodeSmall2.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeSmall3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(1, 9999998));
			if (Items.CheckBarcode(barcode, 1) == 0)
			{
				TxtBarcodeSmall3.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeunit2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(9999999, 99999999));
			if (Items.CheckBarcode(barcode, 2) == 0)
			{
				TxtBarcodeMedium1.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeMedium2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(9999999, 99999999));
			if (Items.CheckBarcode(barcode, 2) == 0)
			{
				TxtBarcodeMedium2.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeMedium3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 9999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(9999999, 99999999));
			if (Items.CheckBarcode(barcode, 2) == 0)
			{
				TxtBarcodeMedium3.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeunit3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 99999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(99999999, 999999999));
			if (Items.CheckBarcode(barcode, DecimalNum) == 0)
			{
				TxtBarcodeBig1.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeBig2_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 99999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(99999999, 999999999));
			if (Items.CheckBarcode(barcode, DecimalNum) == 0)
			{
				TxtBarcodeBig2.Text = barcode;
				break;
			}
			num++;
		}
	}

	private void TxtBarcodeBig3_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		int num;
		for (num = 0; num < 99999998; num++)
		{
			string barcode = Convert.ToString(RandomNumber(99999999, 999999999));
			if (Items.CheckBarcode(barcode, DecimalNum) == 0)
			{
				TxtBarcodeBig3.Text = barcode;
				break;
			}
			num++;
		}
	}

	public void GetItemsControls()
	{
		try
		{
			if (TxtitemnameEn.Text != string.Empty)
			{
				LoginDetails.ItemSearch = TxtitemnameEn.Text;
			}
			if (TxtItemName.Text != string.Empty)
			{
				LoginDetails.ItemSearch = TxtItemName.Text;
			}
			if (TxtItemCode.Text != string.Empty)
			{
				LoginDetails.ItemSearch = TxtItemCode.Text;
			}
			else if (TxtItemCode.Text == string.Empty && TxtItemName.Text == string.Empty && TxtitemnameEn.Text == string.Empty)
			{
				LoginDetails.ItemSearch = string.Empty;
			}
			LoginDetails.ISCheckSearchItem = true;
			FrmSearchItems1 frmSearchItems = new FrmSearchItems1();
			frmSearchItems.ShowDialog();
			if (LoginDetails.ItemSearchID <= 0)
			{
				return;
			}
			foreach (Item_Items i in Items.GetItemsByitemid(LoginDetails.ItemSearchID))
			{
				LblItmID.Text = i.ItemId.ToString();
				NQty.Value = 0m;
				ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)i.ItemId && x.StoreID == (int?)LoginDetails.StoreID).FirstOrDefault();
				if (itemQuantity != null)
				{
					NQty.Value = Convert.ToDecimal(itemQuantity.OpeningBalance);
					Convert.ToInt32(itemQuantity.StoreID);
					if (true)
					{
						CBStore.SelectedValue = Convert.ToInt32(itemQuantity.StoreID);
					}
				}
				TxtItemCode.Text = i.Item_code;
				TxtItemName.Text = i.item_Name;
				TxtitemnameEn.Text = i.item_Name_English;
				CbItemtype.SelectedValue = Convert.ToInt32(i.item_Type);
				CBClass.SelectedValue = Convert.ToInt32(i.Class_ID);
				CBCat.SelectedValue = Convert.ToInt32(i.Category_ID);
				CBCompany.SelectedValue = Convert.ToInt32(i.CompanyID);
				CBGuarantee.SelectedValue = Convert.ToInt32(i.GuaranteeID);
				CBCountry.SelectedValue = Convert.ToInt32(i.CountryID);
				CpUnit1.SelectedValue = Convert.ToInt32(i.UnitSmall);
				TxtBarcodeSmall1.Text = i.SmallBarCode1;
				TxtBarcodeSmall2.Text = i.SmallBarCode2;
				TxtBarcodeSmall3.Text = i.SmallBarCode3;
				CbUnit2.SelectedValue = Convert.ToInt32(i.UnitMedium);
				TxtBarcodeMedium1.Text = i.MediumBarCode1;
				TxtBarcodeMedium2.Text = i.MediumBarCode2;
				TxtBarcodeMedium3.Text = i.MediumBarCode3;
				nConvertUnit2.Value = Convert.ToDecimal(i.ConvertMediumUnit);
				CbUnit3.SelectedValue = Convert.ToInt32(i.UnitLarge);
				TxtBarcodeBig1.Text = i.BigBarCode1;
				TxtBarcodeBig2.Text = i.BigBarCode2;
				TxtBarcodeBig3.Text = i.BigBarCode3;
				nConvertUnit3.Value = Convert.ToDecimal(i.ConvertBigUnit);
				TxtLocaton1.Text = i.LocationSmall;
				TxtLocaton2.Text = i.LocationMedium;
				TxtLocaton3.Text = i.LocationLarge;
				NPrice1unit1.Value = Convert.ToDecimal(i.SellPriceSmall);
				NPrice2unit1.Value = Convert.ToDecimal(i.SellPriceSmall2);
				NPrice3unit1.Value = Convert.ToDecimal(i.SellPriceSmall3);
				nlastpriceunit1.Value = Convert.ToDecimal(i.LastCost);
				NPriceExportunit1.Value = Convert.ToDecimal(i.PriceExportunit1);
				NAverg.Value = Convert.ToDecimal(i.Average_cost);
				nRequestunit1.Value = Convert.ToDecimal(i.SmallUnitQuantity);
				NPrice1unit2.Value = Convert.ToDecimal(i.SellPriceMedium);
				NPrice2unit2.Value = Convert.ToDecimal(i.SellPriceMedium2);
				NPrice3unit2.Value = Convert.ToDecimal(i.SellPriceMedium3);
				nlastpriceunit2.Value = Convert.ToDecimal(i.LastCost2);
				NPriceExportunit2.Value = Convert.ToDecimal(i.PriceExportunit2);
				NPriceMowazaunit2.Value = Convert.ToDecimal(i.PriceMowazaunit2);
				nRequestunit2.Value = Convert.ToDecimal(i.SmallUnitQuantity2);
				NPrice1unit3.Value = Convert.ToDecimal(i.SellpriceLarge);
				NPrice2unit3.Value = Convert.ToDecimal(i.SellpriceLarge2);
				NPrice3unit3.Value = Convert.ToDecimal(i.SellpriceLarge3);
				nlastpriceunit3.Value = Convert.ToDecimal(i.LastCost3);
				NPriceExportunit3.Value = Convert.ToDecimal(i.PriceExportunit3);
				NPriceMowazaunit3.Value = Convert.ToDecimal(i.PriceMowazaunit3);
				nRequestunit3.Value = Convert.ToDecimal(i.SmallUnitQuantity3);
				CHVat.Checked = Convert.ToBoolean(i.Is_Tax);
				NVat.Value = Convert.ToDecimal(i.Tax_Value);
				CBISOffer.Checked = Convert.ToBoolean(i.IsOffer);
				TxtNumOffer.Value = Convert.ToDecimal(i.NumOffer);
				TxtPriceOffer.Value = Convert.ToDecimal(i.PriceOffer);
				TxtNotes.Text = i.Note;
				CHISItemMizan.Checked = Convert.ToBoolean(i.Is_Item_mizan);
				comboPrint.SelectedValue = Convert.ToInt32(i.PrinterID);
				NTobacco.Value = Convert.ToDecimal(i.Tax_Tobacco);
				TxtVatCode.SelectedValue = i.VatCode ?? "";
				TxtExemptionReason.SelectedValue = i.TaxExemptionReasonCode ?? "";
				if (i.UnitTafqitID == 1)
				{
					RBUnitTafqit1.Checked = true;
					RBUnitTafqit2.Checked = false;
					RBUnitTafqit3.Checked = false;
				}
				if (i.UnitTafqitID == 2)
				{
					RBUnitTafqit1.Checked = false;
					RBUnitTafqit2.Checked = true;
					RBUnitTafqit3.Checked = false;
				}
				if (i.UnitTafqitID == 3)
				{
					RBUnitTafqit1.Checked = false;
					RBUnitTafqit2.Checked = false;
					RBUnitTafqit3.Checked = true;
				}
				if (Convert.ToInt32(CbUnit2.SelectedValue) == 0)
				{
					try
					{
						if (Convert.ToInt32(CbUnit2.SelectedValue) == 0)
						{
							NPrice1unit2.Value = 0m;
							NPrice2unit2.Value = 0m;
							NPrice3unit2.Value = 0m;
							nlastpriceunit2.Value = 0m;
							nRequestunit2.Value = 0m;
							nConvertUnit2.Minimum = Convert.ToDecimal("0");
							nConvertUnit2.Value = 0m;
							TxtBarcodeMedium1.Text = string.Empty;
							label80.Text = string.Empty;
							NPrice1unit2.Enabled = false;
							NPrice2unit2.Enabled = false;
							NPrice3unit2.Enabled = false;
							nlastpriceunit2.Enabled = false;
							nRequestunit2.Enabled = false;
							nConvertUnit2.Enabled = false;
							TxtBarcodeMedium1.Enabled = false;
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nlastpriceunit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("0");
							nConvertUnit3.Value = 0m;
							TxtBarcodeBig1.Text = string.Empty;
							label31.Text = string.Empty;
							CbUnit3.Enabled = false;
							NPrice1unit3.Enabled = false;
							NPrice2unit3.Enabled = false;
							NPrice3unit3.Enabled = false;
							nlastpriceunit3.Enabled = false;
							nRequestunit3.Enabled = false;
							nConvertUnit3.Enabled = false;
							TxtBarcodeBig1.Enabled = false;
							CbUnit3.Enabled = false;
							CbUnit3.SelectedValue = 0;
							TxtLocaton2.Enabled = false;
							TxtPerUnit2Price1.Value = 0m;
							TxtPerUnit2Price2.Value = 0m;
							TxtPerUnit2Price3.Value = 0m;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
							TxtPerUnit2Price1.Enabled = false;
							TxtPerUnit2Price2.Enabled = false;
							TxtPerUnit2Price3.Enabled = false;
							TxtPerUnit3Price1.Enabled = false;
							TxtPerUnit3Price2.Enabled = false;
							TxtPerUnit3Price3.Enabled = false;
						}
						else
						{
							NPrice1unit2.Value = 0m;
							NPrice2unit2.Value = 0m;
							NPrice3unit2.Value = 0m;
							nlastpriceunit2.Value = 0m;
							nRequestunit2.Value = 0m;
							nConvertUnit2.Minimum = Convert.ToDecimal("1.1");
							nConvertUnit2.Value = 2m;
							TxtBarcodeMedium1.Text = string.Empty;
							CbUnit2.Enabled = true;
							decimal value = nlastpriceunit1.Value;
							decimal num = Convert.ToDecimal(nConvertUnit2.Value);
							nlastpriceunit2.Value = value * num;
							string text = CbUnit2.Text;
							string text2 = CpUnit1.Text;
							label80.Text = " " + text + " = " + Math.Round(num, 2) + " " + text2 + " ";
							NPrice1unit2.Enabled = true;
							NPrice2unit2.Enabled = true;
							NPrice3unit2.Enabled = true;
							nlastpriceunit2.Enabled = true;
							nRequestunit2.Enabled = true;
							nConvertUnit2.Enabled = true;
							TxtBarcodeMedium1.Enabled = true;
							CbUnit3.Enabled = true;
							TxtLocaton2.Enabled = true;
							TxtPerUnit2Price1.Enabled = true;
							TxtPerUnit2Price2.Enabled = true;
							TxtPerUnit2Price3.Enabled = true;
							TxtPerUnit3Price1.Enabled = true;
							TxtPerUnit3Price2.Enabled = true;
							TxtPerUnit3Price3.Enabled = true;
							TxtPerUnit2Price1.Value = 0m;
							TxtPerUnit2Price2.Value = 0m;
							TxtPerUnit2Price3.Value = 0m;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
						}
						int num2 = Convert.ToInt32(CbItemtype.SelectedValue);
						if (num2 != 3)
						{
						}
					}
					catch (Exception)
					{
					}
				}
				else if (Convert.ToInt32(CbUnit3.SelectedValue) == 0)
				{
					try
					{
						if (Convert.ToInt32(CbUnit3.SelectedValue) == 0)
						{
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nlastpriceunit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("0");
							nConvertUnit3.Value = 0m;
							TxtBarcodeBig1.Text = string.Empty;
							NPrice1unit3.Enabled = false;
							NPrice2unit3.Enabled = false;
							NPrice3unit3.Enabled = false;
							nlastpriceunit3.Enabled = false;
							nRequestunit3.Enabled = false;
							nConvertUnit3.Enabled = false;
							TxtBarcodeBig1.Enabled = false;
							label31.Text = string.Empty;
							TxtLocaton3.Enabled = false;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
							TxtPerUnit3Price1.Enabled = false;
							TxtPerUnit3Price2.Enabled = false;
							TxtPerUnit3Price3.Enabled = false;
						}
						else
						{
							decimal value2 = nlastpriceunit2.Value;
							decimal num3 = Convert.ToDecimal(nConvertUnit2.Value);
							nlastpriceunit3.Value = value2 * num3;
							string text3 = CbUnit3.Text;
							string text4 = CbUnit2.Text;
							string text5 = CpUnit1.Text;
							decimal num4 = Convert.ToDecimal(nConvertUnit2.Value);
							decimal d = Convert.ToDecimal(nConvertUnit3.Value);
							label31.Text = string.Empty + text3 + " = " + Math.Round(d, 2) + " " + text4 + " = " + Math.Round(num3 * num4, 2) + " " + text5 + string.Empty;
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("1.1");
							nConvertUnit3.Value = 2m;
							TxtBarcodeBig1.Text = string.Empty;
							CbUnit3.Enabled = true;
							NPrice1unit3.Enabled = true;
							NPrice2unit3.Enabled = true;
							NPrice3unit3.Enabled = true;
							nlastpriceunit3.Enabled = true;
							nRequestunit3.Enabled = true;
							nConvertUnit3.Enabled = true;
							TxtBarcodeBig1.Enabled = true;
							CbUnit3.Enabled = true;
							TxtLocaton3.Enabled = true;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
							TxtPerUnit3Price1.Enabled = true;
							TxtPerUnit3Price2.Enabled = true;
							TxtPerUnit3Price3.Enabled = true;
						}
						int num5 = Convert.ToInt32(CbItemtype.SelectedValue);
						if (num5 == 3)
						{
							GetTotalCost();
						}
					}
					catch (Exception)
					{
					}
				}
				try
				{
					if (i.Image == null || i.Image == string.Empty)
					{
						pictureEdit1.EditValue = null;
					}
					else
					{
						pictureEdit1.EditValue = Base64ToImage(i.Image);
					}
				}
				catch (Exception)
				{
					pictureEdit1.EditValue = null;
				}
				int num6 = Items.IsItemInDatabase(i.Item_code);
				if (num6 > 0)
				{
					nlastpriceunit1.Enabled = true;
					nlastpriceunit2.Enabled = false;
					nlastpriceunit3.Enabled = false;
					CpUnit1.Enabled = false;
					CbUnit2.Enabled = false;
					CbUnit3.Enabled = false;
					nConvertUnit2.Enabled = false;
					nConvertUnit3.Enabled = false;
					gridView1.OptionsBehavior.Editable = false;
				}
				else
				{
					CpUnit1.Enabled = true;
					nlastpriceunit1.Enabled = true;
					gridView1.OptionsBehavior.Editable = true;
				}
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnNew.Enabled = true;
				BtnPrint.Enabled = true;
			}
			int item_Type = Convert.ToInt32(CbItemtype.SelectedValue);
			FillGridviewEdit(LoginDetails.ItemSearchID, item_Type);
		}
		catch (Exception)
		{
		}
	}

	public void GetItemsControlsByCode()
	{
		try
		{
			List<Item_Items> itemsByitemCode = Items.GetItemsByitemCode(TxtItemCode.Text);
			if (itemsByitemCode == null)
			{
				return;
			}
			foreach (Item_Items i in itemsByitemCode)
			{
				LblItmID.Text = i.ItemId.ToString();
				NQty.Value = 0m;
				ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)i.ItemId && x.StoreID == (int?)LoginDetails.StoreID).FirstOrDefault();
				if (itemQuantity != null)
				{
					NQty.Value = Convert.ToDecimal(itemQuantity.OpeningBalance);
					Convert.ToInt32(itemQuantity.StoreID);
					if (true)
					{
						CBStore.SelectedValue = Convert.ToInt32(itemQuantity.StoreID);
					}
				}
				TxtItemCode.Text = i.Item_code;
				TxtItemName.Text = i.item_Name;
				TxtitemnameEn.Text = i.item_Name_English;
				CbItemtype.SelectedValue = Convert.ToInt32(i.item_Type);
				CBClass.SelectedValue = Convert.ToInt32(i.Class_ID);
				CBCat.SelectedValue = Convert.ToInt32(i.Category_ID);
				CBCompany.SelectedValue = Convert.ToInt32(i.CompanyID);
				CBGuarantee.SelectedValue = Convert.ToInt32(i.GuaranteeID);
				CBCountry.SelectedValue = Convert.ToInt32(i.CountryID);
				CpUnit1.SelectedValue = Convert.ToInt32(i.UnitSmall);
				TxtBarcodeSmall1.Text = i.SmallBarCode1;
				TxtBarcodeSmall2.Text = i.SmallBarCode2;
				TxtBarcodeSmall3.Text = i.SmallBarCode3;
				CbUnit2.SelectedValue = Convert.ToInt32(i.UnitMedium);
				TxtBarcodeMedium1.Text = i.MediumBarCode1;
				TxtBarcodeMedium2.Text = i.MediumBarCode2;
				TxtBarcodeMedium3.Text = i.MediumBarCode3;
				nConvertUnit2.Value = Convert.ToDecimal(i.ConvertMediumUnit);
				CbUnit3.SelectedValue = Convert.ToInt32(i.UnitLarge);
				TxtBarcodeBig1.Text = i.BigBarCode1;
				TxtBarcodeBig2.Text = i.BigBarCode2;
				TxtBarcodeBig3.Text = i.BigBarCode3;
				nConvertUnit3.Value = Convert.ToDecimal(i.ConvertBigUnit);
				TxtLocaton1.Text = i.LocationSmall;
				TxtLocaton2.Text = i.LocationMedium;
				TxtLocaton3.Text = i.LocationLarge;
				NPrice1unit1.Value = Convert.ToDecimal(i.SellPriceSmall);
				NPrice2unit1.Value = Convert.ToDecimal(i.SellPriceSmall2);
				NPrice3unit1.Value = Convert.ToDecimal(i.SellPriceSmall3);
				nlastpriceunit1.Value = Convert.ToDecimal(i.LastCost);
				NPriceExportunit1.Value = Convert.ToDecimal(i.PriceExportunit1);
				NAverg.Value = Convert.ToDecimal(i.Average_cost);
				nRequestunit1.Value = Convert.ToDecimal(i.SmallUnitQuantity);
				NPrice1unit2.Value = Convert.ToDecimal(i.SellPriceMedium);
				NPrice2unit2.Value = Convert.ToDecimal(i.SellPriceMedium2);
				NPrice3unit2.Value = Convert.ToDecimal(i.SellPriceMedium3);
				nlastpriceunit2.Value = Convert.ToDecimal(i.LastCost2);
				NPriceExportunit2.Value = Convert.ToDecimal(i.PriceExportunit2);
				NPriceMowazaunit2.Value = Convert.ToDecimal(i.PriceMowazaunit2);
				nRequestunit2.Value = Convert.ToDecimal(i.SmallUnitQuantity2);
				NPrice1unit3.Value = Convert.ToDecimal(i.SellpriceLarge);
				NPrice2unit3.Value = Convert.ToDecimal(i.SellpriceLarge2);
				NPrice3unit3.Value = Convert.ToDecimal(i.SellpriceLarge3);
				nlastpriceunit3.Value = Convert.ToDecimal(i.LastCost3);
				NPriceExportunit3.Value = Convert.ToDecimal(i.PriceExportunit3);
				NPriceMowazaunit3.Value = Convert.ToDecimal(i.PriceMowazaunit3);
				nRequestunit3.Value = Convert.ToDecimal(i.SmallUnitQuantity3);
				CHVat.Checked = Convert.ToBoolean(i.Is_Tax);
				NVat.Value = LoginDetails.PerVat;
				CBISOffer.Checked = Convert.ToBoolean(i.IsOffer);
				TxtNumOffer.Value = Convert.ToDecimal(i.NumOffer);
				TxtPriceOffer.Value = Convert.ToDecimal(i.PriceOffer);
				TxtNotes.Text = i.Note;
				CHISItemMizan.Checked = Convert.ToBoolean(i.Is_Item_mizan);
				comboPrint.SelectedValue = Convert.ToInt32(i.PrinterID);
				NTobacco.Value = Convert.ToDecimal(i.Tax_Tobacco);
				if (i.UnitTafqitID == 1)
				{
					RBUnitTafqit1.Checked = true;
					RBUnitTafqit2.Checked = false;
					RBUnitTafqit3.Checked = false;
				}
				if (i.UnitTafqitID == 2)
				{
					RBUnitTafqit1.Checked = false;
					RBUnitTafqit2.Checked = true;
					RBUnitTafqit3.Checked = false;
				}
				if (i.UnitTafqitID == 3)
				{
					RBUnitTafqit1.Checked = false;
					RBUnitTafqit2.Checked = false;
					RBUnitTafqit3.Checked = true;
				}
				if (Convert.ToInt32(CbUnit2.SelectedValue) == 0)
				{
					try
					{
						if (Convert.ToInt32(CbUnit2.SelectedValue) == 0)
						{
							NPrice1unit2.Value = 0m;
							NPrice2unit2.Value = 0m;
							NPrice3unit2.Value = 0m;
							nlastpriceunit2.Value = 0m;
							nRequestunit2.Value = 0m;
							nConvertUnit2.Minimum = Convert.ToDecimal("0");
							nConvertUnit2.Value = 0m;
							TxtBarcodeMedium1.Text = string.Empty;
							label80.Text = string.Empty;
							NPrice1unit2.Enabled = false;
							NPrice2unit2.Enabled = false;
							NPrice3unit2.Enabled = false;
							nlastpriceunit2.Enabled = false;
							nRequestunit2.Enabled = false;
							nConvertUnit2.Enabled = false;
							TxtBarcodeMedium1.Enabled = false;
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nlastpriceunit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("0");
							nConvertUnit3.Value = 0m;
							TxtBarcodeBig1.Text = string.Empty;
							label31.Text = string.Empty;
							CbUnit3.Enabled = false;
							NPrice1unit3.Enabled = false;
							NPrice2unit3.Enabled = false;
							NPrice3unit3.Enabled = false;
							nlastpriceunit3.Enabled = false;
							nRequestunit3.Enabled = false;
							nConvertUnit3.Enabled = false;
							TxtBarcodeBig1.Enabled = false;
							CbUnit3.Enabled = false;
							CbUnit3.SelectedValue = 0;
							TxtLocaton2.Enabled = false;
							TxtPerUnit2Price1.Value = 0m;
							TxtPerUnit2Price2.Value = 0m;
							TxtPerUnit2Price3.Value = 0m;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
							TxtPerUnit2Price1.Enabled = false;
							TxtPerUnit2Price2.Enabled = false;
							TxtPerUnit2Price3.Enabled = false;
							TxtPerUnit3Price1.Enabled = false;
							TxtPerUnit3Price2.Enabled = false;
							TxtPerUnit3Price3.Enabled = false;
						}
						else
						{
							NPrice1unit2.Value = 0m;
							NPrice2unit2.Value = 0m;
							NPrice3unit2.Value = 0m;
							nlastpriceunit2.Value = 0m;
							nRequestunit2.Value = 0m;
							nConvertUnit2.Minimum = Convert.ToDecimal("1.1");
							nConvertUnit2.Value = 2m;
							TxtBarcodeMedium1.Text = string.Empty;
							CbUnit2.Enabled = true;
							decimal value = nlastpriceunit1.Value;
							decimal num = Convert.ToDecimal(nConvertUnit2.Value);
							nlastpriceunit2.Value = value * num;
							string text = CbUnit2.Text;
							string text2 = CpUnit1.Text;
							label80.Text = " " + text + " = " + Math.Round(num, 2) + " " + text2 + " ";
							NPrice1unit2.Enabled = true;
							NPrice2unit2.Enabled = true;
							NPrice3unit2.Enabled = true;
							nlastpriceunit2.Enabled = true;
							nRequestunit2.Enabled = true;
							nConvertUnit2.Enabled = true;
							TxtBarcodeMedium1.Enabled = true;
							CbUnit3.Enabled = true;
							TxtLocaton2.Enabled = true;
							TxtPerUnit2Price1.Value = 0m;
							TxtPerUnit2Price2.Value = 0m;
							TxtPerUnit2Price3.Value = 0m;
							TxtPerUnit3Price1.Value = 0m;
							TxtPerUnit3Price2.Value = 0m;
							TxtPerUnit3Price3.Value = 0m;
							TxtPerUnit2Price1.Enabled = true;
							TxtPerUnit2Price2.Enabled = true;
							TxtPerUnit2Price3.Enabled = true;
							TxtPerUnit3Price1.Enabled = true;
							TxtPerUnit3Price2.Enabled = true;
							TxtPerUnit3Price3.Enabled = true;
						}
						int num2 = Convert.ToInt32(CbItemtype.SelectedValue);
						if (num2 == 3)
						{
							GetTotalCost();
						}
					}
					catch (Exception)
					{
					}
				}
				else if (Convert.ToInt32(CbUnit3.SelectedValue) == 0)
				{
					try
					{
						if (Convert.ToInt32(CbUnit3.SelectedValue) == 0)
						{
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nlastpriceunit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("0");
							nConvertUnit3.Value = 0m;
							TxtBarcodeBig1.Text = string.Empty;
							NPrice1unit3.Enabled = false;
							NPrice2unit3.Enabled = false;
							NPrice3unit3.Enabled = false;
							nlastpriceunit3.Enabled = false;
							nRequestunit3.Enabled = false;
							nConvertUnit3.Enabled = false;
							TxtBarcodeBig1.Enabled = false;
							label31.Text = string.Empty;
							TxtLocaton3.Enabled = false;
						}
						else
						{
							decimal value2 = nlastpriceunit2.Value;
							decimal num3 = Convert.ToDecimal(nConvertUnit2.Value);
							nlastpriceunit3.Value = value2 * num3;
							string text3 = CbUnit3.Text;
							string text4 = CbUnit2.Text;
							string text5 = CpUnit1.Text;
							decimal num4 = Convert.ToDecimal(nConvertUnit2.Value);
							decimal num5 = Convert.ToDecimal(nConvertUnit3.Value);
							label31.Text = string.Empty + text3 + " = " + num5 + " " + text4 + " = " + num3 * num4 + " " + text5 + string.Empty;
							NPrice1unit3.Value = 0m;
							NPrice2unit3.Value = 0m;
							NPrice3unit3.Value = 0m;
							nRequestunit3.Value = 0m;
							nConvertUnit3.Minimum = Convert.ToDecimal("1.1");
							nConvertUnit3.Value = 2m;
							TxtBarcodeBig1.Text = string.Empty;
							CbUnit3.Enabled = true;
							NPrice1unit3.Enabled = true;
							NPrice2unit3.Enabled = true;
							NPrice3unit3.Enabled = true;
							nlastpriceunit3.Enabled = true;
							nRequestunit3.Enabled = true;
							nConvertUnit3.Enabled = true;
							TxtBarcodeBig1.Enabled = true;
							CbUnit3.Enabled = true;
							TxtLocaton3.Enabled = true;
						}
						int num6 = Convert.ToInt32(CbItemtype.SelectedValue);
						if (num6 != 3)
						{
						}
					}
					catch (Exception)
					{
					}
				}
				try
				{
					if (i.Image == null || i.Image == string.Empty)
					{
						pictureEdit1.EditValue = null;
					}
					else
					{
						pictureEdit1.EditValue = Base64ToImage(i.Image);
					}
				}
				catch (Exception)
				{
					pictureEdit1.EditValue = null;
				}
				int num7 = Items.IsItemInDatabase(i.Item_code);
				if (num7 > 0)
				{
					nlastpriceunit1.Enabled = true;
					nlastpriceunit2.Enabled = false;
					nlastpriceunit3.Enabled = false;
					CpUnit1.Enabled = false;
					CbUnit2.Enabled = false;
					CbUnit3.Enabled = false;
					nConvertUnit2.Enabled = false;
					nConvertUnit3.Enabled = false;
					gridView1.OptionsBehavior.Editable = false;
				}
				else
				{
					CpUnit1.Enabled = true;
					nlastpriceunit1.Enabled = true;
					gridView1.OptionsBehavior.Editable = true;
				}
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				BtnNew.Enabled = true;
				BtnPrint.Enabled = true;
			}
			int item_Type = Convert.ToInt32(CbItemtype.SelectedValue);
			FillGridviewEdit(LoginDetails.ItemSearchID, item_Type);
		}
		catch (Exception)
		{
		}
	}

	public void GetItmAnotherFrm()
	{
		if (LoginDetails.ItemSearchID <= 0)
		{
			return;
		}
		foreach (Item_Items i in Items.GetItemsByitemid(LoginDetails.ItemSearchID))
		{
			LblItmID.Text = i.ItemId.ToString();
			NQty.Value = 0m;
			ItemQuantity itemQuantity = db.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)i.ItemId && x.StoreID == (int?)LoginDetails.StoreID).FirstOrDefault();
			if (itemQuantity != null)
			{
				NQty.Value = Convert.ToDecimal(itemQuantity.OpeningBalance);
				Convert.ToInt32(itemQuantity.StoreID);
				if (true)
				{
					CBStore.SelectedValue = Convert.ToInt32(itemQuantity.StoreID);
				}
			}
			TxtItemCode.Text = i.Item_code;
			TxtItemName.Text = i.item_Name;
			TxtitemnameEn.Text = i.item_Name_English;
			CbItemtype.SelectedValue = Convert.ToInt32(i.item_Type);
			TxtVatCode.SelectedValue = i.VatCode ?? "";
			TxtExemptionReason.SelectedValue = i.TaxExemptionReasonCode ?? "";
			CBClass.SelectedValue = Convert.ToInt32(i.Class_ID);
			CBCat.SelectedValue = Convert.ToInt32(i.Category_ID);
			CBCompany.SelectedValue = Convert.ToInt32(i.CompanyID);
			CBGuarantee.SelectedValue = Convert.ToInt32(i.GuaranteeID);
			CBCountry.SelectedValue = Convert.ToInt32(i.CountryID);
			CpUnit1.SelectedValue = Convert.ToInt32(i.UnitSmall);
			TxtBarcodeSmall1.Text = i.SmallBarCode1;
			TxtBarcodeSmall2.Text = i.SmallBarCode2;
			TxtBarcodeSmall3.Text = i.SmallBarCode3;
			CbUnit2.SelectedValue = Convert.ToInt32(i.UnitMedium);
			TxtBarcodeMedium1.Text = i.MediumBarCode1;
			TxtBarcodeMedium2.Text = i.MediumBarCode2;
			TxtBarcodeMedium3.Text = i.MediumBarCode3;
			nConvertUnit2.Value = Convert.ToDecimal(i.ConvertMediumUnit);
			CbUnit3.SelectedValue = Convert.ToInt32(i.UnitLarge);
			TxtBarcodeBig1.Text = i.BigBarCode1;
			TxtBarcodeBig2.Text = i.BigBarCode2;
			TxtBarcodeBig3.Text = i.BigBarCode3;
			nConvertUnit3.Value = Convert.ToDecimal(i.ConvertBigUnit);
			TxtLocaton1.Text = i.LocationSmall;
			TxtLocaton2.Text = i.LocationMedium;
			TxtLocaton3.Text = i.LocationLarge;
			NPrice1unit1.Value = Convert.ToDecimal(i.SellPriceSmall);
			NPrice2unit1.Value = Convert.ToDecimal(i.SellPriceSmall2);
			NPrice3unit1.Value = Convert.ToDecimal(i.SellPriceSmall3);
			nlastpriceunit1.Value = Convert.ToDecimal(i.LastCost);
			NPriceExportunit1.Value = Convert.ToDecimal(i.PriceExportunit1);
			NAverg.Value = Convert.ToDecimal(i.Average_cost);
			nRequestunit1.Value = Convert.ToDecimal(i.SmallUnitQuantity);
			NPrice1unit2.Value = Convert.ToDecimal(i.SellPriceMedium);
			NPrice2unit2.Value = Convert.ToDecimal(i.SellPriceMedium2);
			NPrice3unit2.Value = Convert.ToDecimal(i.SellPriceMedium3);
			nlastpriceunit2.Value = Convert.ToDecimal(i.LastCost2);
			NPriceExportunit2.Value = Convert.ToDecimal(i.PriceExportunit2);
			NPriceMowazaunit2.Value = Convert.ToDecimal(i.PriceMowazaunit2);
			nRequestunit2.Value = Convert.ToDecimal(i.SmallUnitQuantity2);
			NPrice1unit3.Value = Convert.ToDecimal(i.SellpriceLarge);
			NPrice2unit3.Value = Convert.ToDecimal(i.SellpriceLarge2);
			NPrice3unit3.Value = Convert.ToDecimal(i.SellpriceLarge3);
			nlastpriceunit3.Value = Convert.ToDecimal(i.LastCost3);
			NPriceExportunit3.Value = Convert.ToDecimal(i.PriceExportunit3);
			NPriceMowazaunit3.Value = Convert.ToDecimal(i.PriceMowazaunit3);
			nRequestunit3.Value = Convert.ToDecimal(i.SmallUnitQuantity3);
			CHVat.Checked = Convert.ToBoolean(i.Is_Tax);
			NVat.Value = LoginDetails.PerVat;
			CBISOffer.Checked = Convert.ToBoolean(i.IsOffer);
			TxtNumOffer.Value = Convert.ToDecimal(i.NumOffer);
			TxtPriceOffer.Value = Convert.ToDecimal(i.PriceOffer);
			TxtNotes.Text = i.Note;
			CHISItemMizan.Checked = Convert.ToBoolean(i.Is_Item_mizan);
			int num = Items.IsItemInDatabase(i.Item_code);
			comboPrint.SelectedValue = Convert.ToInt32(i.PrinterID);
			NTobacco.Value = Convert.ToDecimal(i.Tax_Tobacco);
			if (i.UnitTafqitID == 1)
			{
				RBUnitTafqit1.Checked = true;
				RBUnitTafqit2.Checked = false;
				RBUnitTafqit3.Checked = false;
			}
			if (i.UnitTafqitID == 2)
			{
				RBUnitTafqit1.Checked = false;
				RBUnitTafqit2.Checked = true;
				RBUnitTafqit3.Checked = false;
			}
			if (i.UnitTafqitID == 3)
			{
				RBUnitTafqit1.Checked = false;
				RBUnitTafqit2.Checked = false;
				RBUnitTafqit3.Checked = true;
			}
			nlastpriceunit1.Enabled = true;
			nlastpriceunit2.Enabled = true;
			nlastpriceunit3.Enabled = true;
			nConvertUnit2.Enabled = true;
			nConvertUnit3.Enabled = true;
			gridView1.OptionsBehavior.Editable = true;
			try
			{
				if (i.Image == null || i.Image == string.Empty)
				{
					pictureEdit1.EditValue = null;
				}
				else
				{
					pictureEdit1.EditValue = Base64ToImage(i.Image);
				}
			}
			catch (Exception)
			{
				pictureEdit1.EditValue = null;
			}
			BtnSave.Enabled = false;
			BtnEdit.Enabled = true;
			BtnDelete.Enabled = true;
			BtnNew.Enabled = true;
			BtnPrint.Enabled = true;
		}
		int item_Type = Convert.ToInt32(CbItemtype.SelectedValue);
		FillGridviewEdit(LoginDetails.ItemSearchID, item_Type);
	}

	public void SearchItems()
	{
		try
		{
			LoginDetails.ItemSearch = CodeItm;
			LoginDetails.ISCheckSearchItem = false;
			LoginDetails.ISPurches = true;
			FrmSearchItems1 frmSearchItems = new FrmSearchItems1();
			frmSearchItems.ShowDialog();
			if (LoginDetails.ItemSearchAll2.Count > 0)
			{
				foreach (string item in LoginDetails.ItemSearchAll2)
				{
					string code = item.ToString();
					Item_Items item_Items = Pur.GetItems(code).FirstOrDefault();
					if (item_Items != null)
					{
						DataRow dataRow = null;
						dataRow = datatable.NewRow();
						dataRow["كود_الصنف"] = item_Items.Item_code;
						dataRow["أسم الصنف"] = item_Items.item_Name;
						dataRow["الوحدة"] = item_Items.Item_Unit.Name;
						dataRow["الكمية"] = 1;
						dataRow["المخزن"] = Pur.GetStoreName();
						dataRow["رقم المخزن"] = Pur.GetStoreID();
						dataRow["أسم_الوحدة"] = "الصغرى";
						dataRow["ظ….التكلفة"] = item_Items.SellPriceSmall;
						dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item_Items.SellPriceSmall * (decimal?)Convert.ToInt32(1)), 2);
						object obj = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
						int num = Convert.ToInt32(obj.ToString());
						dataRow["ظ…"] = num + 1;
						datatable.Rows.Add(dataRow);
					}
					IsShow = 1;
				}
				CheckFfistRow();
				insertnewrow();
				GetTotalCost();
				ArrangeDatatable();
				frmSearchItems.Close();
				frmSearchItems.Hide();
				LoginDetails.ItemSearchAll2.Clear();
				LoginDetails.ISPurches = false;
			}
			frmSearchItems.Close();
			frmSearchItems.Hide();
			LoginDetails.ItemSearchAll2.Clear();
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			GetTotalCost();
		}
		catch (Exception)
		{
			LoginDetails.ISCheckSearchItem = true;
			LoginDetails.ISPurches = false;
			NewFrm();
		}
	}

	public void FillGridviewEdit(int ItemId, int item_Type)
	{
		if (item_Type == 3)
		{
			datatable.Rows.Clear();
			List<Item_ItemComponent> allItemsComp = Items.GetAllItemsComp(ItemId);
			int num = 1;
			foreach (Item_ItemComponent item in allItemsComp)
			{
				DataRow dataRow = null;
				int itemID = Convert.ToInt32(item.ItemID_Complant);
				Item_Items item_Items = Pur.GetItemsByitemid(itemID).FirstOrDefault();
				dataRow = datatable.NewRow();
				dataRow["كود_الصنف"] = item_Items.Item_code;
				dataRow["أسم الصنف"] = item_Items.item_Name;
				if (item.ItemUnitType == "الصغرى")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "المتوسطة")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				if (item.ItemUnitType == "الكبرى")
				{
					dataRow["الوحدة"] = db.Item_Unit.Where((Item_Unit x) => (int?)x.ID == item.ItemUnitID).FirstOrDefault().Name;
				}
				dataRow["أسم_الوحدة"] = item.ItemUnitType;
				dataRow["رقم المخزن"] = item.StoreID;
				dataRow["المخزن"] = (from x in db.Account_Stores
					where (int?)x.ID == item.StoreID
					select x.Store_Name).FirstOrDefault();
				dataRow["الكمية"] = item.Amount;
				dataRow["ظ….التكلفة"] = item.SmallUnitPrice;
				dataRow["الاجمالى"] = Math.Round(Convert.ToDecimal(item.TotalPrice), DecimalNum);
				dataRow["مسح"] = 0;
				datatable.Rows.Add(dataRow);
				num++;
			}
			insertnewrow();
			gridControl1.DataSource = datatable;
			gridView1.OptionsBehavior.Editable = true;
			ArrangeDatatable();
		}
		else
		{
			datatable.Rows.Clear();
		}
	}

	private void TxtItemCode_ButtonClick(object sender, EventArgs e)
	{
		GetItemsControls();
	}

	private void CBISOffer_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void CbItemtype_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void CbItemtype_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			int num = Convert.ToInt32(CbItemtype.SelectedValue);
			if (num == 3)
			{
				xtraTabPage4.PageVisible = true;
				datatable.Rows.Clear();
				dtCurrentTable.Rows.Clear();
				gridControl1.Enabled = true;
				gridView1.OptionsBehavior.Editable = true;
				DataRow dataRow = null;
				dataRow = datatable.NewRow();
				dataRow["كود_الصنف"] = string.Empty;
				dataRow["أسم الصنف"] = string.Empty;
				dataRow["الوحدة"] = string.Empty;
				dataRow["الكمية"] = 0;
				dataRow["ظ….التكلفة"] = 0.0;
				dataRow["الاجمالى"] = 0.0;
				dataRow["تم"] = 0;
				dataRow["أسم_الوحدة"] = "الصغرى";
				dataRow["المخزن"] = Pur.GetStoreName();
				dataRow["رقم المخزن"] = Pur.GetStoreID();
				datatable.Rows.Add(dataRow);
				nlastpriceunit1.Value = 0m;
				GetTotalCost();
				nlastpriceunit1.Enabled = true;
				nlastpriceunit2.Enabled = false;
				nlastpriceunit3.Enabled = false;
			}
			else
			{
				xtraTabPage4.PageVisible = false;
				datatable.Rows.Clear();
				dtCurrentTable.Rows.Clear();
				gridControl1.Enabled = true;
				gridView1.OptionsBehavior.Editable = false;
				DataRow dataRow2 = null;
				dataRow2 = datatable.NewRow();
				dataRow2["كود_الصنف"] = string.Empty;
				dataRow2["أسم الصنف"] = string.Empty;
				dataRow2["الوحدة"] = string.Empty;
				dataRow2["الكمية"] = 0;
				dataRow2["ظ….التكلفة"] = 0.0;
				dataRow2["الاجمالى"] = 0.0;
				dataRow2["تم"] = 0;
				dataRow2["أسم_الوحدة"] = "الصغرى";
				dataRow2["المخزن"] = Pur.GetStoreName();
				dataRow2["رقم المخزن"] = Pur.GetStoreID();
				datatable.Rows.Add(dataRow2);
				nlastpriceunit1.Value = 0m;
				nlastpriceunit2.Value = 0m;
				nlastpriceunit3.Value = 0m;
				nlastpriceunit1.Enabled = true;
				nlastpriceunit2.Enabled = true;
				nlastpriceunit3.Enabled = true;
			}
		}
		catch (Exception)
		{
		}
	}

	public void GetTotalCost()
	{
		object summaryValue = gridView1.Columns["الاجمالى"].SummaryItem.SummaryValue;
		if (summaryValue != null)
		{
			nlastpriceunit1.Text = Convert.ToString(summaryValue);
			nlastpriceunit1.Enabled = true;
			nlastpriceunit2.Enabled = false;
			nlastpriceunit3.Enabled = false;
		}
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void CBType_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			CbItemtype.Focus();
		}
	}

	private void TxtItemName_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtItemName.Focus();
		}
	}

	private void TxtitemnameEn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtitemnameEn.Focus();
		}
	}

	private void TxtNote_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TxtNotes.Focus();
		}
	}

	private void CpUnit1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TapPrice.SelectedTabPage = xtraTabPage1;
			CpUnit1.Focus();
		}
	}

	private void CbUnit2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (Convert.ToInt32(CbUnit2.SelectedValue) > 0)
			{
				nConvertUnit2.Focus();
				return;
			}
			TapPrice.SelectedTabPage = xtraTabPage2;
			NPrice1unit1.Focus();
		}
	}

	private void CbUnit3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (Convert.ToInt32(CbUnit3.SelectedValue) > 0)
			{
				nConvertUnit3.Focus();
				return;
			}
			TapPrice.SelectedTabPage = xtraTabPage2;
			NPrice1unit1.Focus();
		}
	}

	private void NPrice1unit1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TapPrice.SelectedTabPage = xtraTabPage2;
			NPrice1unit1.Focus();
		}
	}

	private void nRequestunit1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (Convert.ToInt32(CbUnit2.SelectedValue) > 0)
			{
				NPrice1unit2.Focus();
				return;
			}
			TapPrice.SelectedTabPage = xtraTabPage5;
			CbItemtype.Focus();
		}
	}

	private void nRequestunit2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (Convert.ToInt32(CbUnit3.SelectedValue) > 0)
			{
				NPrice1unit3.Focus();
				return;
			}
			TapPrice.SelectedTabPage = xtraTabPage5;
			CbItemtype.Focus();
		}
	}

	private void nRequestunit3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TapPrice.SelectedTabPage = xtraTabPage5;
			CbItemtype.Focus();
		}
	}

	private void pictureEdit1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
	}

	private void CHVat_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TapPrice.SelectedTabPage = xtraTabPage3;
			CHVat.Focus();
		}
	}

	private void NVat_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TapPrice.SelectedTabPage = xtraTabPage5;
			BtnSave.Focus();
		}
	}

	private void textBox_AR_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
	}

	private void textBox_EN_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("en"));
	}

	public static InputLanguage GetInputLanguageByName(string inputName)
	{
		foreach (InputLanguage installedInputLanguage in InputLanguage.InstalledInputLanguages)
		{
			if (installedInputLanguage.Culture.EnglishName.ToLower().StartsWith(inputName))
			{
				return installedInputLanguage;
			}
		}
		return null;
	}

	private void SetKeyboardLayout(InputLanguage layout)
	{
		InputLanguage.CurrentInputLanguage = layout;
	}

	private void BtnCalcVatSmall1_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice1unit1.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatSmall2_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice2unit1.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatSmall3_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice3unit1.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void CBISOffer_CheckedChanged_1(object sender, EventArgs e)
	{
		if (CBISOffer.Checked)
		{
			TxtNumOffer.Enabled = true;
			TxtPriceOffer.Enabled = true;
			TxtNumOffer.Value = 0m;
			TxtPriceOffer.Value = 0m;
		}
		else
		{
			TxtNumOffer.Enabled = false;
			TxtPriceOffer.Enabled = false;
			TxtNumOffer.Value = 0m;
			TxtPriceOffer.Value = 0m;
		}
	}

	private void BtnCalcVatMedium1_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice1unit2.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatMedium2_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice2unit2.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatMedium3_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice3unit2.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatLarg1_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice1unit3.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatLarg2_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice2unit3.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnCalcVatLarg3_Click(object sender, EventArgs e)
	{
		FrmVAT frmVAT = new FrmVAT();
		frmVAT.ShowDialog();
		if (frmVAT.ISFrmClosing)
		{
			NPrice3unit3.Value = Convert.ToDecimal(frmVAT.TxtNeed.Value);
		}
	}

	private void BtnImage_Click_2(object sender, EventArgs e)
	{
		pictureEdit1.Image = null;
	}

	private void FrmItems_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing)
		{
			DialogResult dialogResult = MessageBox.Show("هل انت متأكد من اغلاق شاشة كرت الصنف", "اغلاق كرت الصنف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			if (dialogResult == DialogResult.Yes)
			{
				LoginDetails.ItemSearchID = 0;
			}
			else
			{
				e.Cancel = true;
			}
		}
	}

	private void TxtPerUnit1Price1_ValueChanged(object sender, EventArgs e)
	{
		perPrice1(TxtPerUnit1Price1.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit1Price2_ValueChanged(object sender, EventArgs e)
	{
		perPrice2(TxtPerUnit1Price2.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit1Price3_ValueChanged(object sender, EventArgs e)
	{
		perPrice3(TxtPerUnit1Price3.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit2Price1_ValueChanged(object sender, EventArgs e)
	{
		perPrice1(TxtPerUnit2Price1.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit2Price2_ValueChanged(object sender, EventArgs e)
	{
		perPrice2(TxtPerUnit2Price2.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit2Price3_ValueChanged(object sender, EventArgs e)
	{
		perPrice3(TxtPerUnit2Price3.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit3Price1_ValueChanged(object sender, EventArgs e)
	{
		perPrice1(TxtPerUnit3Price1.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit3Price2_ValueChanged(object sender, EventArgs e)
	{
		perPrice2(TxtPerUnit3Price2.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void TxtPerUnit3Price3_ValueChanged(object sender, EventArgs e)
	{
		perPrice3(TxtPerUnit3Price3.Value, nlastpriceunit1.Value, nlastpriceunit2.Value, nlastpriceunit3.Value);
	}

	private void perPrice1(decimal PerUnit1, decimal nlastpriceunit1, decimal nlastpriceunit2, decimal nlastpriceunit3)
	{
		if (!(nlastpriceunit1 > 0m))
		{
			return;
		}
		NPrice1unit1.Value = PerUnit1 * nlastpriceunit1 / 100m + nlastpriceunit1;
		TxtPerUnit1Price1.Value = PerUnit1;
		if (nlastpriceunit2 > 0m)
		{
			NPrice1unit2.Value = PerUnit1 * nlastpriceunit2 / 100m + nlastpriceunit2;
			TxtPerUnit2Price1.Value = PerUnit1;
			if (nlastpriceunit3 > 0m)
			{
				TxtPerUnit3Price1.Value = PerUnit1;
				NPrice1unit3.Value = PerUnit1 * nlastpriceunit3 / 100m + nlastpriceunit3;
			}
		}
	}

	private void perPrice2(decimal PerUnit2, decimal nlastpriceunit1, decimal nlastpriceunit2, decimal nlastpriceunit3)
	{
		if (!(nlastpriceunit1 > 0m))
		{
			return;
		}
		NPrice2unit1.Value = PerUnit2 * nlastpriceunit1 / 100m + nlastpriceunit1;
		TxtPerUnit1Price2.Value = PerUnit2;
		if (nlastpriceunit2 > 0m)
		{
			NPrice2unit2.Value = PerUnit2 * nlastpriceunit2 / 100m + nlastpriceunit2;
			TxtPerUnit2Price2.Value = PerUnit2;
			if (nlastpriceunit3 > 0m)
			{
				TxtPerUnit3Price2.Value = PerUnit2;
				NPrice2unit3.Value = PerUnit2 * nlastpriceunit3 / 100m + nlastpriceunit3;
			}
		}
	}

	private void BtnGroupR_Click(object sender, EventArgs e)
	{
		FillItemCat();
	}

	private void BtnClassR_Click(object sender, EventArgs e)
	{
		FillItemClass();
	}

	private void BtnContryR_Click(object sender, EventArgs e)
	{
		FillItemCountry();
	}

	private void BtnCompanyR_Click(object sender, EventArgs e)
	{
		FillItemCompany();
	}

	private void BtnGuanartyR_Click(object sender, EventArgs e)
	{
		FillItemGuarantee();
	}

	private void BtnGroupP_Click(object sender, EventArgs e)
	{
		FrmGroupsRes frmGroupsRes = new FrmGroupsRes();
		frmGroupsRes.ShowDialog();
		FillItemCat();
	}

	private void BtnClassP_Click(object sender, EventArgs e)
	{
		FrmClass frmClass = new FrmClass();
		frmClass.ShowDialog();
		FillItemClass();
	}

	private void BtnContryP_Click(object sender, EventArgs e)
	{
		FrmCountry frmCountry = new FrmCountry();
		frmCountry.ShowDialog();
		FillItemCountry();
	}

	private void BtnCompanyP_Click(object sender, EventArgs e)
	{
		FrmCompany frmCompany = new FrmCompany();
		frmCompany.ShowDialog();
		FillItemCompany();
	}

	private void BtnGuanartyP_Click(object sender, EventArgs e)
	{
		FrmGuarantee frmGuarantee = new FrmGuarantee();
		frmGuarantee.ShowDialog();
		FillItemGuarantee();
	}

	private void CBStore_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			GTSdbEntities gTSdbEntities = new GTSdbEntities();
			int STID = Convert.ToInt32(CBStore.SelectedValue);
			int ItmID = Convert.ToInt32(LblItmID.Text);
			ItemQuantity itemQuantity = gTSdbEntities.ItemQuantities.Where((ItemQuantity x) => x.ItemID == (int?)ItmID && x.StoreID == (int?)STID).FirstOrDefault();
			if (itemQuantity != null)
			{
				NQty.Value = Convert.ToDecimal(itemQuantity.OpeningBalance);
				Convert.ToInt32(itemQuantity.StoreID);
				if (true)
				{
					CBStore.SelectedValue = Convert.ToInt32(itemQuantity.StoreID);
				}
			}
			else
			{
				NQty.Value = 0m;
			}
		}
		catch (Exception)
		{
		}
	}

	private void BtnUnit1R_Click(object sender, EventArgs e)
	{
		FillItemUnit();
	}

	private void BtnUnit2R_Click(object sender, EventArgs e)
	{
		FillItemUnit2();
	}

	private void BtnUnit3R_Click(object sender, EventArgs e)
	{
		FillItemUnit3();
	}

	private void BtnUnit1P_Click(object sender, EventArgs e)
	{
		FrmUnit frmUnit = new FrmUnit();
		frmUnit.ShowDialog();
		FillItemUnit();
	}

	private void BtnUnit2P_Click(object sender, EventArgs e)
	{
		FrmUnit frmUnit = new FrmUnit();
		frmUnit.ShowDialog();
		FillItemUnit2();
	}

	private void BtnUnit3P_Click(object sender, EventArgs e)
	{
		FrmUnit frmUnit = new FrmUnit();
		frmUnit.ShowDialog();
		FillItemUnit3();
	}

	private void nlastpriceunit1_KeyUp(object sender, KeyEventArgs e)
	{
		if (LblItmID.Text == "" || LblItmID.Text == "0")
		{
			NAverg.Text = nlastpriceunit1.Text;
		}
	}

	private void perPrice3(decimal PerUnit3, decimal nlastpriceunit1, decimal nlastpriceunit2, decimal nlastpriceunit3)
	{
		if (!(nlastpriceunit1 > 0m))
		{
			return;
		}
		NPrice3unit1.Value = PerUnit3 * nlastpriceunit1 / 100m + nlastpriceunit1;
		TxtPerUnit1Price3.Value = PerUnit3;
		if (nlastpriceunit2 > 0m)
		{
			NPrice3unit2.Value = PerUnit3 * nlastpriceunit2 / 100m + nlastpriceunit2;
			TxtPerUnit2Price3.Value = PerUnit3;
			if (nlastpriceunit3 > 0m)
			{
				TxtPerUnit3Price3.Value = PerUnit3;
				NPrice3unit3.Value = PerUnit3 * nlastpriceunit3 / 100m + nlastpriceunit3;
			}
		}
	}

	private void TxtVatCode_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (LoginDetails.IsElectronicInvoice && TxtVatCode.SelectedItem is InvoicType invoicType)
		{
			switch (invoicType.ValueVat)
			{
			case "E":
				CHVat.Checked = false;
				NVat.Text = "0.00";
				NVat.ReadOnly = true;
				break;
			case "S":
				CHVat.Checked = true;
				NVat.Text = "15.00";
				NVat.ReadOnly = false;
				break;
			case "Z":
				CHVat.Checked = true;
				NVat.Text = "0.00";
				NVat.ReadOnly = true;
				break;
			case "O":
				CHVat.Checked = false;
				NVat.Text = "0.00";
				NVat.ReadOnly = true;
				break;
			}
		}
		ElctorncSeeting();
	}

	private void NVat_Leave(object sender, EventArgs e)
	{
		if (!LoginDetails.IsElectronicInvoice || !(TxtVatCode.SelectedItem is InvoicType { ValueVat: "S" }))
		{
			return;
		}
		if (decimal.TryParse(NVat.Text, out var result))
		{
			if (result != 15m && result != 5m)
			{
				MessageBox.Show("الرجاء إدخال 15 أو 5 فقط");
				NVat.Text = "15.00";
			}
		}
		else
		{
			MessageBox.Show("الرجاء إدخال رقم صحيح");
			NVat.Text = "15.00";
		}
	}

	private void CHVat_CheckedChanged(object sender, EventArgs e)
	{
		if (!LoginDetails.IsElectronicInvoice)
		{
			return;
		}
		if (CHVat.Checked)
		{
			if (TxtVatCode.Text == " E    (معفي من الضريبة) " || TxtVatCode.Text == " O   (عكس ضريبي (Reverse Charge))")
			{
				CHVat.Checked = false;
			}
		}
		else if (TxtVatCode.Text == " S    (خاضع للضريبة بنسبة قياسية 5% أو 15% فقط) " || TxtVatCode.Text == " Z    (خاضع لضريبة بنسبة صفر)")
		{
			CHVat.Checked = true;
		}
	}

	private void ElctorncSeeting()
	{
		if (LoginDetails.IsElectronicInvoice)
		{
			if (TxtVatCode.Text == " E    (معفي من الضريبة) " || TxtVatCode.Text == " O   (عكس ضريبي (Reverse Charge))")
			{
				TxtExemptionReason.Enabled = true;
			}
			else
			{
				TxtExemptionReason.Enabled = false;
			}
		}
	}

	private void BindElctorncVat()
	{
		if (!LoginDetails.IsElectronicInvoice)
		{
			GropElctronc.Visible = false;
		}
		List<InvoicType> dataSource = new List<InvoicType>
		{
			new InvoicType
			{
				TextVat = " E    (معفي من الضريبة) ",
				ValueVat = "E"
			},
			new InvoicType
			{
				TextVat = " S    (خاضع للضريبة بنسبة قياسية 5% أو 15% فقط) ",
				ValueVat = "S"
			},
			new InvoicType
			{
				TextVat = " Z    (خاضع لضريبة بنسبة صفر)",
				ValueVat = "Z"
			},
			new InvoicType
			{
				TextVat = " O   (عكس ضريبي (Reverse Charge))",
				ValueVat = "O"
			}
		};
		TxtVatCode.DisplayMember = "TextVat";
		TxtVatCode.ValueMember = "ValueVat";
		TxtVatCode.DataSource = dataSource;
		TxtVatCode.DropDownStyle = ComboBoxStyle.DropDownList;
		List<InvoicType> dataSource2 = new List<InvoicType>
		{
			new InvoicType
			{
				TextExemptionReason = "الخدمات المالية",
				ValueExemptionReason = "VATEX-SA-29"
			},
			new InvoicType
			{
				TextExemptionReason = " عقد تأمين على الحياة ",
				ValueExemptionReason = "VATEX-SA-29-7"
			},
			new InvoicType
			{
				TextExemptionReason = "التوريدات العقارية المعفاة من الضريبة",
				ValueExemptionReason = "VATEX-SA-30"
			},
			new InvoicType
			{
				TextExemptionReason = "التوريدات الخاضعة للضريبة",
				ValueExemptionReason = "S"
			},
			new InvoicType
			{
				TextExemptionReason = "صادرات السلع من المملكة",
				ValueExemptionReason = "VATEX-SA-32"
			},
			new InvoicType
			{
				TextExemptionReason = "صادرات الخدمات من المملكة ",
				ValueExemptionReason = "VATEX-SA-33"
			},
			new InvoicType
			{
				TextExemptionReason = "النقل الدولي  للسلع ",
				ValueExemptionReason = "VATEX-SA-34-1"
			},
			new InvoicType
			{
				TextExemptionReason = "ط¨ الدولي النقل ",
				ValueExemptionReason = "VATEX-SA-34-2"
			},
			new InvoicType
			{
				TextExemptionReason = "  الخدمات المرتبطة مباشرة أو عرضي\u064bط§ بتوريد النقل الدولي للركاب",
				ValueExemptionReason = "VATEX-SA-34-3"
			},
			new InvoicType
			{
				TextExemptionReason = "توريد وسائل النقل المؤهلة ",
				ValueExemptionReason = "VATEX-SA-34-4"
			},
			new InvoicType
			{
				TextExemptionReason = "  الخدمات ذات الصلة بنقل السلع أو الركاب، وفق\u064bط§ للتعريف الوارد بالمادة الخامسة والعشرين من الالئحة التنفيذية لنظام ضريبة القيامة المضافة",
				ValueExemptionReason = "VATEX-SA-34-5"
			},
			new InvoicType
			{
				TextExemptionReason = "ط© والمعدات ط§  ",
				ValueExemptionReason = "VATEX-SA-35"
			},
			new InvoicType
			{
				TextExemptionReason = " ط© المعاد ",
				ValueExemptionReason = "VATEX-SA-36"
			},
			new InvoicType
			{
				TextExemptionReason = " ظ† الخاصة التعليمية الخدمات ",
				ValueExemptionReason = "VATEX-SA-EDU"
			},
			new InvoicType
			{
				TextExemptionReason = " ظ† الخاصة الصحية الخدمات  ",
				ValueExemptionReason = "VATEX-SA-HEA"
			},
			new InvoicType
			{
				TextExemptionReason = "  ط© العسكرية السلع توريد ",
				ValueExemptionReason = "VATEX-SA-MLTRY"
			},
			new InvoicType
			{
				TextExemptionReason = "  السبب يتم تزويده من قبل المكلف على أساس كل حالة على حدة",
				ValueExemptionReason = "VATEX-SA-OOS"
			}
		};
		TxtExemptionReason.DisplayMember = "TextExemptionReason";
		TxtExemptionReason.ValueMember = "ValueExemptionReason";
		TxtExemptionReason.DataSource = dataSource2;
		TxtExemptionReason.DropDownStyle = ComboBoxStyle.DropDownList;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmItems));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled5 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled6 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled7 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled8 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled9 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled10 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled11 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions12 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled12 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions13 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled13 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions14 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance14 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered14 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed14 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled14 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions15 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance15 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered15 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed15 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled15 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions16 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance16 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered16 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed16 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled16 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions17 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance17 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered17 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed17 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled17 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions18 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance18 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered18 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed18 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled18 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance19 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered19 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed19 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled19 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions19 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance20 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered20 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed20 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled20 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions imageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance21 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered21 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed21 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled21 = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LblItmID = new System.Windows.Forms.Label();
		this.TxtitemnameEn = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtItemName = new DevExpress.XtraEditors.ButtonEdit();
		this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtItemCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label5 = new System.Windows.Forms.Label();
		this.label65 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.CHVat = new System.Windows.Forms.CheckBox();
		this.label49 = new System.Windows.Forms.Label();
		this.NVat = new System.Windows.Forms.NumericUpDown();
		this.CBCompany = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtNotes = new System.Windows.Forms.TextBox();
		this.CBClass = new System.Windows.Forms.ComboBox();
		this.CbItemtype = new System.Windows.Forms.ComboBox();
		this.label37 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnPrint = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
		this.TapPrice = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.RBUnitDefualtSell3 = new System.Windows.Forms.RadioButton();
		this.RBUnitDefualtSell2 = new System.Windows.Forms.RadioButton();
		this.RBUnitDefualtSell1 = new System.Windows.Forms.RadioButton();
		this.BtnUnit3P = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit3R = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit2P = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit2R = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit1P = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit1R = new DevExpress.XtraEditors.SimpleButton();
		this.TxtBarcodeBig3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label42 = new System.Windows.Forms.Label();
		this.TxtBarcodeMedium3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label38 = new System.Windows.Forms.Label();
		this.TxtBarcodeSmall3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label40 = new System.Windows.Forms.Label();
		this.CbUnit2 = new System.Windows.Forms.ComboBox();
		this.CbUnit3 = new System.Windows.Forms.ComboBox();
		this.CpUnit1 = new System.Windows.Forms.ComboBox();
		this.TxtBarcodeBig1 = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtBarcodeMedium1 = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtBarcodeSmall1 = new DevExpress.XtraEditors.ButtonEdit();
		this.nConvertUnit3 = new System.Windows.Forms.NumericUpDown();
		this.label28 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.nConvertUnit2 = new System.Windows.Forms.NumericUpDown();
		this.label27 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.label35 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.RBUnitTafqit3 = new System.Windows.Forms.RadioButton();
		this.RBUnitTafqit2 = new System.Windows.Forms.RadioButton();
		this.RBUnitTafqit1 = new System.Windows.Forms.RadioButton();
		this.TxtBarcodeBig2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label43 = new System.Windows.Forms.Label();
		this.TxtBarcodeMedium2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label41 = new System.Windows.Forms.Label();
		this.TxtBarcodeSmall2 = new DevExpress.XtraEditors.ButtonEdit();
		this.label39 = new System.Windows.Forms.Label();
		this.TxtLocaton2 = new DevExpress.XtraEditors.ButtonEdit();
		this.TxtLocaton3 = new DevExpress.XtraEditors.ButtonEdit();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtLocaton1 = new DevExpress.XtraEditors.ButtonEdit();
		this.label50 = new System.Windows.Forms.Label();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl6 = new DevExpress.XtraEditors.SeparatorControl();
		this.label31 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.label67 = new System.Windows.Forms.Label();
		this.label73 = new System.Windows.Forms.Label();
		this.label75 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.label70 = new System.Windows.Forms.Label();
		this.label72 = new System.Windows.Forms.Label();
		this.BtnCalcVatLarg3 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatLarg2 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatLarg1 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatMedium3 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatMedium2 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatMedium1 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatSmall2 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatSmall3 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCalcVatSmall1 = new DevExpress.XtraEditors.SimpleButton();
		this.CBStore = new System.Windows.Forms.ComboBox();
		this.label74 = new System.Windows.Forms.Label();
		this.label71 = new System.Windows.Forms.Label();
		this.label69 = new System.Windows.Forms.Label();
		this.label66 = new System.Windows.Forms.Label();
		this.NQty = new System.Windows.Forms.NumericUpDown();
		this.label23 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.NPrice3unit3 = new System.Windows.Forms.NumericUpDown();
		this.NPrice3unit2 = new System.Windows.Forms.NumericUpDown();
		this.label16 = new System.Windows.Forms.Label();
		this.NPrice3unit1 = new System.Windows.Forms.NumericUpDown();
		this.NPrice2unit3 = new System.Windows.Forms.NumericUpDown();
		this.NPrice1unit3 = new System.Windows.Forms.NumericUpDown();
		this.NPrice2unit2 = new System.Windows.Forms.NumericUpDown();
		this.NPrice1unit2 = new System.Windows.Forms.NumericUpDown();
		this.NPrice2unit1 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit3Price3 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price3 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit1Price3 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit3Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit1Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit3Price1 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price1 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit1Price1 = new System.Windows.Forms.NumericUpDown();
		this.NPrice1unit1 = new System.Windows.Forms.NumericUpDown();
		this.label20 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label52 = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label44 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.nRequestunit1 = new System.Windows.Forms.NumericUpDown();
		this.label21 = new System.Windows.Forms.Label();
		this.NPriceMowazaunit3 = new System.Windows.Forms.NumericUpDown();
		this.NPriceExportunit3 = new System.Windows.Forms.NumericUpDown();
		this.NAverg = new System.Windows.Forms.NumericUpDown();
		this.label53 = new System.Windows.Forms.Label();
		this.NPriceExportunit1 = new System.Windows.Forms.NumericUpDown();
		this.label54 = new System.Windows.Forms.Label();
		this.NPriceMowazaunit2 = new System.Windows.Forms.NumericUpDown();
		this.NPriceExportunit2 = new System.Windows.Forms.NumericUpDown();
		this.separatorControl5 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
		this.nlastpriceunit3 = new System.Windows.Forms.NumericUpDown();
		this.nlastpriceunit2 = new System.Windows.Forms.NumericUpDown();
		this.nlastpriceunit1 = new System.Windows.Forms.NumericUpDown();
		this.label19 = new System.Windows.Forms.Label();
		this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
		this.comboPrint = new System.Windows.Forms.ComboBox();
		this.label56 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.nRequestunit3 = new System.Windows.Forms.NumericUpDown();
		this.nRequestunit2 = new System.Windows.Forms.NumericUpDown();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.separatorControl8 = new DevExpress.XtraEditors.SeparatorControl();
		this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
		this.GropElctronc = new System.Windows.Forms.GroupBox();
		this.label79 = new System.Windows.Forms.Label();
		this.TxtExemptionReason = new System.Windows.Forms.ComboBox();
		this.label78 = new System.Windows.Forms.Label();
		this.TxtVatCode = new System.Windows.Forms.ComboBox();
		this.label77 = new System.Windows.Forms.Label();
		this.label76 = new System.Windows.Forms.Label();
		this.NTobacco = new System.Windows.Forms.NumericUpDown();
		this.label68 = new System.Windows.Forms.Label();
		this.BtnGuanartyP = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCompanyP = new DevExpress.XtraEditors.SimpleButton();
		this.CHISItemMizan = new System.Windows.Forms.CheckBox();
		this.BtnContryP = new DevExpress.XtraEditors.SimpleButton();
		this.BtnClassP = new DevExpress.XtraEditors.SimpleButton();
		this.BtnGroupP = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.CBISOffer = new System.Windows.Forms.CheckBox();
		this.TxtNumOffer = new System.Windows.Forms.NumericUpDown();
		this.TxtPriceOffer = new System.Windows.Forms.NumericUpDown();
		this.label46 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.BtnGuanartyR = new DevExpress.XtraEditors.SimpleButton();
		this.BtnCompanyR = new DevExpress.XtraEditors.SimpleButton();
		this.BtnContryR = new DevExpress.XtraEditors.SimpleButton();
		this.BtnClassR = new DevExpress.XtraEditors.SimpleButton();
		this.BtnGroupR = new DevExpress.XtraEditors.SimpleButton();
		this.CBCat = new System.Windows.Forms.ComboBox();
		this.BtnImage = new DevExpress.XtraEditors.SimpleButton();
		this.label47 = new System.Windows.Forms.Label();
		this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
		this.CBGuarantee = new System.Windows.Forms.ComboBox();
		this.label58 = new System.Windows.Forms.Label();
		this.CBCountry = new System.Windows.Forms.ComboBox();
		this.label57 = new System.Windows.Forms.Label();
		this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.shearch = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
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
		this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
		this.label63 = new System.Windows.Forms.Label();
		this.Npharmacist = new System.Windows.Forms.NumericUpDown();
		this.label64 = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.NDoctor = new System.Windows.Forms.NumericUpDown();
		this.label62 = new System.Windows.Forms.Label();
		this.label60 = new System.Windows.Forms.Label();
		this.NDiscItem = new System.Windows.Forms.NumericUpDown();
		this.label59 = new System.Windows.Forms.Label();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
		this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
		this.label80 = new System.Windows.Forms.Label();
		this.label81 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtitemnameEn.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemName.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.buttonEdit1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NVat).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TapPrice).BeginInit();
		this.TapPrice.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton2.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton3.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).BeginInit();
		this.xtraTabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.NQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceMowazaunit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NAverg).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceMowazaunit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit1).BeginInit();
		this.xtraTabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl8).BeginInit();
		this.xtraTabPage5.SuspendLayout();
		this.GropElctronc.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.NTobacco).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumOffer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPriceOffer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureEdit1.Properties).BeginInit();
		this.xtraTabPage4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).BeginInit();
		this.xtraTabPage6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Npharmacist).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.NDiscItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add(this.LblItmID);
		this.groupBox1.Controls.Add(this.TxtitemnameEn);
		this.groupBox1.Controls.Add(this.TxtItemName);
		this.groupBox1.Controls.Add(this.buttonEdit1);
		this.groupBox1.Controls.Add(this.TxtItemCode);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label65);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.LblItmID, "LblItmID");
		this.LblItmID.Name = "LblItmID";
		resources.ApplyResources(this.TxtitemnameEn, "TxtitemnameEn");
		this.TxtitemnameEn.Name = "TxtitemnameEn";
		this.TxtitemnameEn.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtitemnameEn.Properties.Appearance.Font");
		this.TxtitemnameEn.Properties.Appearance.Options.UseFont = true;
		this.TxtitemnameEn.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtitemnameEn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtitemnameEn.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions, "editorButtonImageOptions1");
		this.TxtitemnameEn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtitemnameEn.Properties.Buttons"), resources.GetString("TxtitemnameEn.Properties.Buttons1"), (int)resources.GetObject("TxtitemnameEn.Properties.Buttons2"), (bool)resources.GetObject("TxtitemnameEn.Properties.Buttons3"), (bool)resources.GetObject("TxtitemnameEn.Properties.Buttons4"), (bool)resources.GetObject("TxtitemnameEn.Properties.Buttons5"), editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, resources.GetString("TxtitemnameEn.Properties.Buttons6"), resources.GetObject("TxtitemnameEn.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtitemnameEn.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtitemnameEn.Properties.Buttons9"))
		});
		this.TxtitemnameEn.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		this.TxtitemnameEn.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtitemnameEn.KeyDown += new System.Windows.Forms.KeyEventHandler(CpUnit1_KeyDown);
		resources.ApplyResources(this.TxtItemName, "TxtItemName");
		this.TxtItemName.Name = "TxtItemName";
		this.TxtItemName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtItemName.Properties.Appearance.Font");
		this.TxtItemName.Properties.Appearance.Options.UseFont = true;
		this.TxtItemName.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions2, "editorButtonImageOptions2");
		this.TxtItemName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtItemName.Properties.Buttons"), resources.GetString("TxtItemName.Properties.Buttons1"), (int)resources.GetObject("TxtItemName.Properties.Buttons2"), (bool)resources.GetObject("TxtItemName.Properties.Buttons3"), (bool)resources.GetObject("TxtItemName.Properties.Buttons4"), (bool)resources.GetObject("TxtItemName.Properties.Buttons5"), editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, resources.GetString("TxtItemName.Properties.Buttons6"), resources.GetObject("TxtItemName.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtItemName.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtItemName.Properties.Buttons9"))
		});
		this.TxtItemName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		this.TxtItemName.Enter += new System.EventHandler(textBox_AR_Enter);
		this.TxtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtitemnameEn_KeyDown);
		resources.ApplyResources(this.buttonEdit1, "buttonEdit1");
		this.buttonEdit1.Name = "buttonEdit1";
		this.buttonEdit1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("buttonEdit1.Properties.Appearance.Font");
		this.buttonEdit1.Properties.Appearance.Options.UseFont = true;
		this.buttonEdit1.Properties.Appearance.Options.UseTextOptions = true;
		this.buttonEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.buttonEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.buttonEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions3, "editorButtonImageOptions3");
		this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("buttonEdit1.Properties.Buttons"), resources.GetString("buttonEdit1.Properties.Buttons1"), (int)resources.GetObject("buttonEdit1.Properties.Buttons2"), (bool)resources.GetObject("buttonEdit1.Properties.Buttons3"), (bool)resources.GetObject("buttonEdit1.Properties.Buttons4"), (bool)resources.GetObject("buttonEdit1.Properties.Buttons5"), editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, resources.GetString("buttonEdit1.Properties.Buttons6"), resources.GetObject("buttonEdit1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("buttonEdit1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("buttonEdit1.Properties.Buttons9"))
		});
		this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		this.buttonEdit1.Enter += new System.EventHandler(textBox_EN_Enter);
		this.buttonEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtItemName_KeyDown);
		resources.ApplyResources(this.TxtItemCode, "TxtItemCode");
		this.TxtItemCode.Name = "TxtItemCode";
		this.TxtItemCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtItemCode.Properties.Appearance.Font");
		this.TxtItemCode.Properties.Appearance.Options.UseFont = true;
		this.TxtItemCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtItemCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtItemCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtItemCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions4, "editorButtonImageOptions4");
		this.TxtItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtItemCode.Properties.Buttons"), resources.GetString("TxtItemCode.Properties.Buttons1"), (int)resources.GetObject("TxtItemCode.Properties.Buttons2"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons3"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons4"), (bool)resources.GetObject("TxtItemCode.Properties.Buttons5"), editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, resources.GetString("TxtItemCode.Properties.Buttons6"), resources.GetObject("TxtItemCode.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtItemCode.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtItemCode.Properties.Buttons9"))
		});
		this.TxtItemCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtItemCode_ButtonClick);
		this.TxtItemCode.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(TxtItemName_KeyDown);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.label65, "label65");
		this.label65.Name = "label65";
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		resources.ApplyResources(this.CHVat, "CHVat");
		this.CHVat.Checked = true;
		this.CHVat.CheckState = System.Windows.Forms.CheckState.Checked;
		this.CHVat.Name = "CHVat";
		this.CHVat.UseVisualStyleBackColor = true;
		this.CHVat.CheckedChanged += new System.EventHandler(CHVat_CheckedChanged);
		this.CHVat.KeyDown += new System.Windows.Forms.KeyEventHandler(NVat_KeyDown);
		resources.ApplyResources(this.label49, "label49");
		this.label49.Name = "label49";
		resources.ApplyResources(this.NVat, "NVat");
		this.NVat.DecimalPlaces = 2;
		this.NVat.Maximum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.NVat.Name = "NVat";
		this.NVat.Value = new decimal(new int[4] { 15, 0, 0, 0 });
		this.NVat.KeyDown += new System.Windows.Forms.KeyEventHandler(NVat_KeyDown);
		this.NVat.Leave += new System.EventHandler(NVat_Leave);
		resources.ApplyResources(this.CBCompany, "CBCompany");
		this.CBCompany.FormattingEnabled = true;
		this.CBCompany.Name = "CBCompany";
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtNotes, "TxtNotes");
		this.TxtNotes.Name = "TxtNotes";
		this.TxtNotes.Enter += new System.EventHandler(textBox_AR_Enter);
		this.TxtNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(CpUnit1_KeyDown);
		resources.ApplyResources(this.CBClass, "CBClass");
		this.CBClass.FormattingEnabled = true;
		this.CBClass.Name = "CBClass";
		resources.ApplyResources(this.CbItemtype, "CbItemtype");
		this.CbItemtype.FormattingEnabled = true;
		this.CbItemtype.Name = "CbItemtype";
		this.CbItemtype.SelectedIndexChanged += new System.EventHandler(CbItemtype_SelectedIndexChanged);
		resources.ApplyResources(this.label37, "label37");
		this.label37.Name = "label37";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		resources.ApplyResources(this.BtnPrint, "BtnPrint");
		this.BtnPrint.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnPrint.ImageOptions.Image");
		this.BtnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnPrint.Name = "BtnPrint";
		this.BtnPrint.Click += new System.EventHandler(BtnPrint_Click);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Controls.Add(this.BtnEdit);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnPrint);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnSearch);
		this.groupBox2.Controls.Add(this.BtnDelete);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		resources.ApplyResources(this.BtnSearch, "BtnSearch");
		this.BtnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSearch.ImageOptions.Image");
		this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSearch.Name = "BtnSearch";
		this.BtnSearch.Click += new System.EventHandler(BtnSearch_Click);
		resources.ApplyResources(this.TapPrice, "TapPrice");
		this.TapPrice.Name = "TapPrice";
		this.TapPrice.SelectedTabPage = this.xtraTabPage1;
		this.TapPrice.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[6] { this.xtraTabPage1, this.xtraTabPage2, this.xtraTabPage3, this.xtraTabPage5, this.xtraTabPage4, this.xtraTabPage6 });
		resources.ApplyResources(this.xtraTabPage1, "xtraTabPage1");
		this.xtraTabPage1.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("xtraTabPage1.Appearance.Header.Font");
		this.xtraTabPage1.Appearance.Header.Image = (System.Drawing.Image)resources.GetObject("xtraTabPage1.Appearance.Header.Image");
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseImage = true;
		this.xtraTabPage1.Controls.Add(this.label81);
		this.xtraTabPage1.Controls.Add(this.label80);
		this.xtraTabPage1.Controls.Add(this.RBUnitDefualtSell3);
		this.xtraTabPage1.Controls.Add(this.RBUnitDefualtSell2);
		this.xtraTabPage1.Controls.Add(this.RBUnitDefualtSell1);
		this.xtraTabPage1.Controls.Add(this.BtnUnit3P);
		this.xtraTabPage1.Controls.Add(this.BtnUnit3R);
		this.xtraTabPage1.Controls.Add(this.BtnUnit2P);
		this.xtraTabPage1.Controls.Add(this.BtnUnit2R);
		this.xtraTabPage1.Controls.Add(this.BtnUnit1P);
		this.xtraTabPage1.Controls.Add(this.BtnUnit1R);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeBig3);
		this.xtraTabPage1.Controls.Add(this.label42);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeMedium3);
		this.xtraTabPage1.Controls.Add(this.label38);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeSmall3);
		this.xtraTabPage1.Controls.Add(this.label40);
		this.xtraTabPage1.Controls.Add(this.CbUnit2);
		this.xtraTabPage1.Controls.Add(this.CbUnit3);
		this.xtraTabPage1.Controls.Add(this.CpUnit1);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeBig1);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeMedium1);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeSmall1);
		this.xtraTabPage1.Controls.Add(this.nConvertUnit3);
		this.xtraTabPage1.Controls.Add(this.label28);
		this.xtraTabPage1.Controls.Add(this.label18);
		this.xtraTabPage1.Controls.Add(this.label22);
		this.xtraTabPage1.Controls.Add(this.nConvertUnit2);
		this.xtraTabPage1.Controls.Add(this.label27);
		this.xtraTabPage1.Controls.Add(this.label11);
		this.xtraTabPage1.Controls.Add(this.label12);
		this.xtraTabPage1.Controls.Add(this.numericUpDown1);
		this.xtraTabPage1.Controls.Add(this.label35);
		this.xtraTabPage1.Controls.Add(this.label34);
		this.xtraTabPage1.Controls.Add(this.label33);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit3);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit2);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit1);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeBig2);
		this.xtraTabPage1.Controls.Add(this.label43);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeMedium2);
		this.xtraTabPage1.Controls.Add(this.label41);
		this.xtraTabPage1.Controls.Add(this.TxtBarcodeSmall2);
		this.xtraTabPage1.Controls.Add(this.label39);
		this.xtraTabPage1.Controls.Add(this.TxtLocaton2);
		this.xtraTabPage1.Controls.Add(this.TxtLocaton3);
		this.xtraTabPage1.Controls.Add(this.label7);
		this.xtraTabPage1.Controls.Add(this.label8);
		this.xtraTabPage1.Controls.Add(this.TxtLocaton1);
		this.xtraTabPage1.Controls.Add(this.label50);
		this.xtraTabPage1.Controls.Add(this.separatorControl1);
		this.xtraTabPage1.Controls.Add(this.separatorControl3);
		this.xtraTabPage1.Controls.Add(this.separatorControl6);
		this.xtraTabPage1.Controls.Add(this.label31);
		this.xtraTabPage1.Controls.Add(this.label30);
		this.xtraTabPage1.Controls.Add(this.label36);
		this.xtraTabPage1.Name = "xtraTabPage1";
		resources.ApplyResources(this.RBUnitDefualtSell3, "RBUnitDefualtSell3");
		this.RBUnitDefualtSell3.Name = "RBUnitDefualtSell3";
		this.RBUnitDefualtSell3.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RBUnitDefualtSell2, "RBUnitDefualtSell2");
		this.RBUnitDefualtSell2.Name = "RBUnitDefualtSell2";
		this.RBUnitDefualtSell2.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RBUnitDefualtSell1, "RBUnitDefualtSell1");
		this.RBUnitDefualtSell1.Checked = true;
		this.RBUnitDefualtSell1.Name = "RBUnitDefualtSell1";
		this.RBUnitDefualtSell1.TabStop = true;
		this.RBUnitDefualtSell1.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.BtnUnit3P, "BtnUnit3P");
		this.BtnUnit3P.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit3P.ImageOptions.Image");
		this.BtnUnit3P.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit3P.Name = "BtnUnit3P";
		this.BtnUnit3P.Click += new System.EventHandler(BtnUnit3P_Click);
		resources.ApplyResources(this.BtnUnit3R, "BtnUnit3R");
		this.BtnUnit3R.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit3R.ImageOptions.Image");
		this.BtnUnit3R.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit3R.Name = "BtnUnit3R";
		this.BtnUnit3R.Click += new System.EventHandler(BtnUnit3R_Click);
		resources.ApplyResources(this.BtnUnit2P, "BtnUnit2P");
		this.BtnUnit2P.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit2P.ImageOptions.Image");
		this.BtnUnit2P.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit2P.Name = "BtnUnit2P";
		this.BtnUnit2P.Click += new System.EventHandler(BtnUnit2P_Click);
		resources.ApplyResources(this.BtnUnit2R, "BtnUnit2R");
		this.BtnUnit2R.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit2R.ImageOptions.Image");
		this.BtnUnit2R.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit2R.Name = "BtnUnit2R";
		this.BtnUnit2R.Click += new System.EventHandler(BtnUnit2R_Click);
		resources.ApplyResources(this.BtnUnit1P, "BtnUnit1P");
		this.BtnUnit1P.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit1P.ImageOptions.Image");
		this.BtnUnit1P.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit1P.Name = "BtnUnit1P";
		this.BtnUnit1P.Click += new System.EventHandler(BtnUnit1P_Click);
		resources.ApplyResources(this.BtnUnit1R, "BtnUnit1R");
		this.BtnUnit1R.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit1R.ImageOptions.Image");
		this.BtnUnit1R.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit1R.Name = "BtnUnit1R";
		this.BtnUnit1R.Click += new System.EventHandler(BtnUnit1R_Click);
		resources.ApplyResources(this.TxtBarcodeBig3, "TxtBarcodeBig3");
		this.TxtBarcodeBig3.Name = "TxtBarcodeBig3";
		this.TxtBarcodeBig3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeBig3.Properties.Appearance.Font");
		this.TxtBarcodeBig3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeBig3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeBig3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeBig3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeBig3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions5, "editorButtonImageOptions5");
		this.TxtBarcodeBig3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeBig3.Properties.Buttons"), resources.GetString("TxtBarcodeBig3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeBig3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeBig3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeBig3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeBig3.Properties.Buttons5"), editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance5, appearanceHovered5, appearancePressed5, appearanceDisabled5, resources.GetString("TxtBarcodeBig3.Properties.Buttons6"), resources.GetObject("TxtBarcodeBig3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeBig3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeBig3.Properties.Buttons9"))
		});
		this.TxtBarcodeBig3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeBig3_ButtonClick);
		resources.ApplyResources(this.label42, "label42");
		this.label42.Name = "label42";
		resources.ApplyResources(this.TxtBarcodeMedium3, "TxtBarcodeMedium3");
		this.TxtBarcodeMedium3.Name = "TxtBarcodeMedium3";
		this.TxtBarcodeMedium3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMedium3.Properties.Appearance.Font");
		this.TxtBarcodeMedium3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMedium3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMedium3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMedium3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMedium3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions6, "editorButtonImageOptions6");
		this.TxtBarcodeMedium3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons"), resources.GetString("TxtBarcodeMedium3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons5"), editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance6, appearanceHovered6, appearancePressed6, appearanceDisabled6, resources.GetString("TxtBarcodeMedium3.Properties.Buttons6"), resources.GetObject("TxtBarcodeMedium3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMedium3.Properties.Buttons9"))
		});
		this.TxtBarcodeMedium3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeMedium3_ButtonClick);
		resources.ApplyResources(this.label38, "label38");
		this.label38.Name = "label38";
		resources.ApplyResources(this.TxtBarcodeSmall3, "TxtBarcodeSmall3");
		this.TxtBarcodeSmall3.Name = "TxtBarcodeSmall3";
		this.TxtBarcodeSmall3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall3.Properties.Appearance.Font");
		this.TxtBarcodeSmall3.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions7, "editorButtonImageOptions7");
		this.TxtBarcodeSmall3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons"), resources.GetString("TxtBarcodeSmall3.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons5"), editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance7, appearanceHovered7, appearancePressed7, appearanceDisabled7, resources.GetString("TxtBarcodeSmall3.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall3.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeSmall3_ButtonClick);
		resources.ApplyResources(this.label40, "label40");
		this.label40.Name = "label40";
		resources.ApplyResources(this.CbUnit2, "CbUnit2");
		this.CbUnit2.FormattingEnabled = true;
		this.CbUnit2.Name = "CbUnit2";
		this.CbUnit2.SelectedIndexChanged += new System.EventHandler(CbUnit2_SelectedIndexChanged);
		resources.ApplyResources(this.CbUnit3, "CbUnit3");
		this.CbUnit3.FormattingEnabled = true;
		this.CbUnit3.Name = "CbUnit3";
		this.CbUnit3.SelectedIndexChanged += new System.EventHandler(CbUnit3_SelectedIndexChanged);
		resources.ApplyResources(this.CpUnit1, "CpUnit1");
		this.CpUnit1.FormattingEnabled = true;
		this.CpUnit1.Name = "CpUnit1";
		this.CpUnit1.SelectedIndexChanged += new System.EventHandler(CpUnit1_SelectedIndexChanged);
		resources.ApplyResources(this.TxtBarcodeBig1, "TxtBarcodeBig1");
		this.TxtBarcodeBig1.Name = "TxtBarcodeBig1";
		this.TxtBarcodeBig1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeBig1.Properties.Appearance.Font");
		this.TxtBarcodeBig1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeBig1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeBig1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeBig1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeBig1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions8, "editorButtonImageOptions8");
		this.TxtBarcodeBig1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeBig1.Properties.Buttons"), resources.GetString("TxtBarcodeBig1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeBig1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeBig1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeBig1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeBig1.Properties.Buttons5"), editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance8, appearanceHovered8, appearancePressed8, appearanceDisabled8, resources.GetString("TxtBarcodeBig1.Properties.Buttons6"), resources.GetObject("TxtBarcodeBig1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeBig1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeBig1.Properties.Buttons9"))
		});
		this.TxtBarcodeBig1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeunit3_ButtonClick);
		resources.ApplyResources(this.TxtBarcodeMedium1, "TxtBarcodeMedium1");
		this.TxtBarcodeMedium1.Name = "TxtBarcodeMedium1";
		this.TxtBarcodeMedium1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMedium1.Properties.Appearance.Font");
		this.TxtBarcodeMedium1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMedium1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMedium1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMedium1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMedium1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions9, "editorButtonImageOptions9");
		this.TxtBarcodeMedium1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons"), resources.GetString("TxtBarcodeMedium1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons5"), editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance9, appearanceHovered9, appearancePressed9, appearanceDisabled9, resources.GetString("TxtBarcodeMedium1.Properties.Buttons6"), resources.GetObject("TxtBarcodeMedium1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMedium1.Properties.Buttons9"))
		});
		this.TxtBarcodeMedium1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeunit2_ButtonClick);
		resources.ApplyResources(this.TxtBarcodeSmall1, "TxtBarcodeSmall1");
		this.TxtBarcodeSmall1.Name = "TxtBarcodeSmall1";
		this.TxtBarcodeSmall1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall1.Properties.Appearance.Font");
		this.TxtBarcodeSmall1.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions10, "editorButtonImageOptions10");
		this.TxtBarcodeSmall1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons"), resources.GetString("TxtBarcodeSmall1.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons5"), editorButtonImageOptions10, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance10, appearanceHovered10, appearancePressed10, appearanceDisabled10, resources.GetString("TxtBarcodeSmall1.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall1.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeunit1_ButtonClick);
		resources.ApplyResources(this.nConvertUnit3, "nConvertUnit3");
		this.nConvertUnit3.DecimalPlaces = 3;
		this.nConvertUnit3.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.nConvertUnit3.Name = "nConvertUnit3";
		this.nConvertUnit3.ValueChanged += new System.EventHandler(nConvertUnit3_ValueChanged);
		resources.ApplyResources(this.label28, "label28");
		this.label28.Name = "label28";
		resources.ApplyResources(this.label18, "label18");
		this.label18.Name = "label18";
		resources.ApplyResources(this.label22, "label22");
		this.label22.Name = "label22";
		resources.ApplyResources(this.nConvertUnit2, "nConvertUnit2");
		this.nConvertUnit2.DecimalPlaces = 3;
		this.nConvertUnit2.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.nConvertUnit2.Name = "nConvertUnit2";
		this.nConvertUnit2.ValueChanged += new System.EventHandler(nConvertUnit2_ValueChanged);
		resources.ApplyResources(this.label27, "label27");
		this.label27.Name = "label27";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
		this.numericUpDown1.DecimalPlaces = 3;
		this.numericUpDown1.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.label35, "label35");
		this.label35.Name = "label35";
		resources.ApplyResources(this.label34, "label34");
		this.label34.Name = "label34";
		resources.ApplyResources(this.label33, "label33");
		this.label33.Name = "label33";
		resources.ApplyResources(this.RBUnitTafqit3, "RBUnitTafqit3");
		this.RBUnitTafqit3.Name = "RBUnitTafqit3";
		this.RBUnitTafqit3.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RBUnitTafqit2, "RBUnitTafqit2");
		this.RBUnitTafqit2.Name = "RBUnitTafqit2";
		this.RBUnitTafqit2.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.RBUnitTafqit1, "RBUnitTafqit1");
		this.RBUnitTafqit1.Checked = true;
		this.RBUnitTafqit1.Name = "RBUnitTafqit1";
		this.RBUnitTafqit1.TabStop = true;
		this.RBUnitTafqit1.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.TxtBarcodeBig2, "TxtBarcodeBig2");
		this.TxtBarcodeBig2.Name = "TxtBarcodeBig2";
		this.TxtBarcodeBig2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeBig2.Properties.Appearance.Font");
		this.TxtBarcodeBig2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeBig2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeBig2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeBig2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeBig2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions11, "editorButtonImageOptions11");
		this.TxtBarcodeBig2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeBig2.Properties.Buttons"), resources.GetString("TxtBarcodeBig2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeBig2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeBig2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeBig2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeBig2.Properties.Buttons5"), editorButtonImageOptions11, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance11, appearanceHovered11, appearancePressed11, appearanceDisabled11, resources.GetString("TxtBarcodeBig2.Properties.Buttons6"), resources.GetObject("TxtBarcodeBig2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeBig2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeBig2.Properties.Buttons9"))
		});
		this.TxtBarcodeBig2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeBig2_ButtonClick);
		this.TxtBarcodeBig2.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtBarcodeBig2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label43, "label43");
		this.label43.Name = "label43";
		resources.ApplyResources(this.TxtBarcodeMedium2, "TxtBarcodeMedium2");
		this.TxtBarcodeMedium2.Name = "TxtBarcodeMedium2";
		this.TxtBarcodeMedium2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeMedium2.Properties.Appearance.Font");
		this.TxtBarcodeMedium2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeMedium2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeMedium2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeMedium2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeMedium2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions12, "editorButtonImageOptions12");
		this.TxtBarcodeMedium2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons"), resources.GetString("TxtBarcodeMedium2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons5"), editorButtonImageOptions12, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance12, appearanceHovered12, appearancePressed12, appearanceDisabled12, resources.GetString("TxtBarcodeMedium2.Properties.Buttons6"), resources.GetObject("TxtBarcodeMedium2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeMedium2.Properties.Buttons9"))
		});
		this.TxtBarcodeMedium2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeMedium2_ButtonClick);
		this.TxtBarcodeMedium2.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtBarcodeMedium2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label41, "label41");
		this.label41.Name = "label41";
		resources.ApplyResources(this.TxtBarcodeSmall2, "TxtBarcodeSmall2");
		this.TxtBarcodeSmall2.Name = "TxtBarcodeSmall2";
		this.TxtBarcodeSmall2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtBarcodeSmall2.Properties.Appearance.Font");
		this.TxtBarcodeSmall2.Properties.Appearance.Options.UseFont = true;
		this.TxtBarcodeSmall2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBarcodeSmall2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBarcodeSmall2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBarcodeSmall2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions13, "editorButtonImageOptions13");
		this.TxtBarcodeSmall2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons"), resources.GetString("TxtBarcodeSmall2.Properties.Buttons1"), (int)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons2"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons3"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons4"), (bool)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons5"), editorButtonImageOptions13, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance13, appearanceHovered13, appearancePressed13, appearanceDisabled13, resources.GetString("TxtBarcodeSmall2.Properties.Buttons6"), resources.GetObject("TxtBarcodeSmall2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtBarcodeSmall2.Properties.Buttons9"))
		});
		this.TxtBarcodeSmall2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBarcodeSmall2_ButtonClick);
		this.TxtBarcodeSmall2.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtBarcodeSmall2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label39, "label39");
		this.label39.Name = "label39";
		resources.ApplyResources(this.TxtLocaton2, "TxtLocaton2");
		this.TxtLocaton2.Name = "TxtLocaton2";
		this.TxtLocaton2.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLocaton2.Properties.Appearance.Font");
		this.TxtLocaton2.Properties.Appearance.Options.UseFont = true;
		this.TxtLocaton2.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocaton2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocaton2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtLocaton2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions14, "editorButtonImageOptions14");
		this.TxtLocaton2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtLocaton2.Properties.Buttons"), resources.GetString("TxtLocaton2.Properties.Buttons1"), (int)resources.GetObject("TxtLocaton2.Properties.Buttons2"), (bool)resources.GetObject("TxtLocaton2.Properties.Buttons3"), (bool)resources.GetObject("TxtLocaton2.Properties.Buttons4"), (bool)resources.GetObject("TxtLocaton2.Properties.Buttons5"), editorButtonImageOptions14, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance14, appearanceHovered14, appearancePressed14, appearanceDisabled14, resources.GetString("TxtLocaton2.Properties.Buttons6"), resources.GetObject("TxtLocaton2.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtLocaton2.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtLocaton2.Properties.Buttons9"))
		});
		this.TxtLocaton2.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtLocaton2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtLocaton3, "TxtLocaton3");
		this.TxtLocaton3.Name = "TxtLocaton3";
		this.TxtLocaton3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLocaton3.Properties.Appearance.Font");
		this.TxtLocaton3.Properties.Appearance.Options.UseFont = true;
		this.TxtLocaton3.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocaton3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocaton3.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtLocaton3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions15, "editorButtonImageOptions15");
		this.TxtLocaton3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtLocaton3.Properties.Buttons"), resources.GetString("TxtLocaton3.Properties.Buttons1"), (int)resources.GetObject("TxtLocaton3.Properties.Buttons2"), (bool)resources.GetObject("TxtLocaton3.Properties.Buttons3"), (bool)resources.GetObject("TxtLocaton3.Properties.Buttons4"), (bool)resources.GetObject("TxtLocaton3.Properties.Buttons5"), editorButtonImageOptions15, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance15, appearanceHovered15, appearancePressed15, appearanceDisabled15, resources.GetString("TxtLocaton3.Properties.Buttons6"), resources.GetObject("TxtLocaton3.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtLocaton3.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtLocaton3.Properties.Buttons9"))
		});
		this.TxtLocaton3.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtLocaton3.KeyDown += new System.Windows.Forms.KeyEventHandler(NPrice1unit1_KeyDown);
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtLocaton1, "TxtLocaton1");
		this.TxtLocaton1.Name = "TxtLocaton1";
		this.TxtLocaton1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtLocaton1.Properties.Appearance.Font");
		this.TxtLocaton1.Properties.Appearance.Options.UseFont = true;
		this.TxtLocaton1.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtLocaton1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtLocaton1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtLocaton1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		resources.ApplyResources(editorButtonImageOptions16, "editorButtonImageOptions16");
		this.TxtLocaton1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("TxtLocaton1.Properties.Buttons"), resources.GetString("TxtLocaton1.Properties.Buttons1"), (int)resources.GetObject("TxtLocaton1.Properties.Buttons2"), (bool)resources.GetObject("TxtLocaton1.Properties.Buttons3"), (bool)resources.GetObject("TxtLocaton1.Properties.Buttons4"), (bool)resources.GetObject("TxtLocaton1.Properties.Buttons5"), editorButtonImageOptions16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance16, appearanceHovered16, appearancePressed16, appearanceDisabled16, resources.GetString("TxtLocaton1.Properties.Buttons6"), resources.GetObject("TxtLocaton1.Properties.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("TxtLocaton1.Properties.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("TxtLocaton1.Properties.Buttons9"))
		});
		this.TxtLocaton1.Enter += new System.EventHandler(textBox_EN_Enter);
		this.TxtLocaton1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label50, "label50");
		this.label50.Name = "label50";
		resources.ApplyResources(this.separatorControl1, "separatorControl1");
		this.separatorControl1.LineThickness = 1;
		this.separatorControl1.Name = "separatorControl1";
		resources.ApplyResources(this.separatorControl3, "separatorControl3");
		this.separatorControl3.LineThickness = 1;
		this.separatorControl3.Name = "separatorControl3";
		resources.ApplyResources(this.separatorControl6, "separatorControl6");
		this.separatorControl6.LineThickness = 1;
		this.separatorControl6.Name = "separatorControl6";
		resources.ApplyResources(this.label31, "label31");
		this.label31.Name = "label31";
		resources.ApplyResources(this.label30, "label30");
		this.label30.Name = "label30";
		resources.ApplyResources(this.label36, "label36");
		this.label36.Name = "label36";
		resources.ApplyResources(this.xtraTabPage2, "xtraTabPage2");
		this.xtraTabPage2.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("xtraTabPage2.Appearance.Header.Font");
		this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage2.Controls.Add(this.label67);
		this.xtraTabPage2.Controls.Add(this.label73);
		this.xtraTabPage2.Controls.Add(this.label75);
		this.xtraTabPage2.Controls.Add(this.label48);
		this.xtraTabPage2.Controls.Add(this.label70);
		this.xtraTabPage2.Controls.Add(this.label72);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatLarg3);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatLarg2);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatLarg1);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatMedium3);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatMedium2);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatMedium1);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatSmall2);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatSmall3);
		this.xtraTabPage2.Controls.Add(this.BtnCalcVatSmall1);
		this.xtraTabPage2.Controls.Add(this.CBStore);
		this.xtraTabPage2.Controls.Add(this.label74);
		this.xtraTabPage2.Controls.Add(this.label71);
		this.xtraTabPage2.Controls.Add(this.label69);
		this.xtraTabPage2.Controls.Add(this.label66);
		this.xtraTabPage2.Controls.Add(this.NQty);
		this.xtraTabPage2.Controls.Add(this.label23);
		this.xtraTabPage2.Controls.Add(this.label25);
		this.xtraTabPage2.Controls.Add(this.label29);
		this.xtraTabPage2.Controls.Add(this.label10);
		this.xtraTabPage2.Controls.Add(this.label14);
		this.xtraTabPage2.Controls.Add(this.label17);
		this.xtraTabPage2.Controls.Add(this.NPrice3unit3);
		this.xtraTabPage2.Controls.Add(this.NPrice3unit2);
		this.xtraTabPage2.Controls.Add(this.label16);
		this.xtraTabPage2.Controls.Add(this.NPrice3unit1);
		this.xtraTabPage2.Controls.Add(this.NPrice2unit3);
		this.xtraTabPage2.Controls.Add(this.NPrice1unit3);
		this.xtraTabPage2.Controls.Add(this.NPrice2unit2);
		this.xtraTabPage2.Controls.Add(this.NPrice1unit2);
		this.xtraTabPage2.Controls.Add(this.NPrice2unit1);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price3);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price3);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price3);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price1);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price1);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price1);
		this.xtraTabPage2.Controls.Add(this.NPrice1unit1);
		this.xtraTabPage2.Controls.Add(this.label20);
		this.xtraTabPage2.Controls.Add(this.label32);
		this.xtraTabPage2.Controls.Add(this.label24);
		this.xtraTabPage2.Controls.Add(this.label52);
		this.xtraTabPage2.Controls.Add(this.label55);
		this.xtraTabPage2.Controls.Add(this.label13);
		this.xtraTabPage2.Controls.Add(this.label44);
		this.xtraTabPage2.Controls.Add(this.label51);
		this.xtraTabPage2.Controls.Add(this.nRequestunit1);
		this.xtraTabPage2.Controls.Add(this.label21);
		this.xtraTabPage2.Controls.Add(this.NPriceMowazaunit3);
		this.xtraTabPage2.Controls.Add(this.NPriceExportunit3);
		this.xtraTabPage2.Controls.Add(this.NAverg);
		this.xtraTabPage2.Controls.Add(this.label53);
		this.xtraTabPage2.Controls.Add(this.NPriceExportunit1);
		this.xtraTabPage2.Controls.Add(this.label54);
		this.xtraTabPage2.Controls.Add(this.NPriceMowazaunit2);
		this.xtraTabPage2.Controls.Add(this.NPriceExportunit2);
		this.xtraTabPage2.Controls.Add(this.separatorControl5);
		this.xtraTabPage2.Controls.Add(this.separatorControl2);
		this.xtraTabPage2.Controls.Add(this.separatorControl4);
		this.xtraTabPage2.Controls.Add(this.nlastpriceunit3);
		this.xtraTabPage2.Controls.Add(this.nlastpriceunit2);
		this.xtraTabPage2.Controls.Add(this.nlastpriceunit1);
		this.xtraTabPage2.Controls.Add(this.label19);
		this.xtraTabPage2.Name = "xtraTabPage2";
		resources.ApplyResources(this.label67, "label67");
		this.label67.Name = "label67";
		resources.ApplyResources(this.label73, "label73");
		this.label73.Name = "label73";
		resources.ApplyResources(this.label75, "label75");
		this.label75.Name = "label75";
		resources.ApplyResources(this.label48, "label48");
		this.label48.Name = "label48";
		resources.ApplyResources(this.label70, "label70");
		this.label70.Name = "label70";
		resources.ApplyResources(this.label72, "label72");
		this.label72.Name = "label72";
		resources.ApplyResources(this.BtnCalcVatLarg3, "BtnCalcVatLarg3");
		this.BtnCalcVatLarg3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatLarg3.ImageOptions.Image");
		this.BtnCalcVatLarg3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatLarg3.Name = "BtnCalcVatLarg3";
		this.BtnCalcVatLarg3.Click += new System.EventHandler(BtnCalcVatLarg3_Click);
		resources.ApplyResources(this.BtnCalcVatLarg2, "BtnCalcVatLarg2");
		this.BtnCalcVatLarg2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatLarg2.ImageOptions.Image");
		this.BtnCalcVatLarg2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatLarg2.Name = "BtnCalcVatLarg2";
		this.BtnCalcVatLarg2.Click += new System.EventHandler(BtnCalcVatLarg2_Click);
		resources.ApplyResources(this.BtnCalcVatLarg1, "BtnCalcVatLarg1");
		this.BtnCalcVatLarg1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatLarg1.ImageOptions.Image");
		this.BtnCalcVatLarg1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatLarg1.Name = "BtnCalcVatLarg1";
		this.BtnCalcVatLarg1.Click += new System.EventHandler(BtnCalcVatLarg1_Click);
		resources.ApplyResources(this.BtnCalcVatMedium3, "BtnCalcVatMedium3");
		this.BtnCalcVatMedium3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatMedium3.ImageOptions.Image");
		this.BtnCalcVatMedium3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatMedium3.Name = "BtnCalcVatMedium3";
		this.BtnCalcVatMedium3.Click += new System.EventHandler(BtnCalcVatMedium3_Click);
		resources.ApplyResources(this.BtnCalcVatMedium2, "BtnCalcVatMedium2");
		this.BtnCalcVatMedium2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatMedium2.ImageOptions.Image");
		this.BtnCalcVatMedium2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatMedium2.Name = "BtnCalcVatMedium2";
		this.BtnCalcVatMedium2.Click += new System.EventHandler(BtnCalcVatMedium2_Click);
		resources.ApplyResources(this.BtnCalcVatMedium1, "BtnCalcVatMedium1");
		this.BtnCalcVatMedium1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatMedium1.ImageOptions.Image");
		this.BtnCalcVatMedium1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatMedium1.Name = "BtnCalcVatMedium1";
		this.BtnCalcVatMedium1.Click += new System.EventHandler(BtnCalcVatMedium1_Click);
		resources.ApplyResources(this.BtnCalcVatSmall2, "BtnCalcVatSmall2");
		this.BtnCalcVatSmall2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatSmall2.ImageOptions.Image");
		this.BtnCalcVatSmall2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatSmall2.Name = "BtnCalcVatSmall2";
		this.BtnCalcVatSmall2.Click += new System.EventHandler(BtnCalcVatSmall2_Click);
		resources.ApplyResources(this.BtnCalcVatSmall3, "BtnCalcVatSmall3");
		this.BtnCalcVatSmall3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatSmall3.ImageOptions.Image");
		this.BtnCalcVatSmall3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatSmall3.Name = "BtnCalcVatSmall3";
		this.BtnCalcVatSmall3.Click += new System.EventHandler(BtnCalcVatSmall3_Click);
		resources.ApplyResources(this.BtnCalcVatSmall1, "BtnCalcVatSmall1");
		this.BtnCalcVatSmall1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCalcVatSmall1.ImageOptions.Image");
		this.BtnCalcVatSmall1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCalcVatSmall1.Name = "BtnCalcVatSmall1";
		this.BtnCalcVatSmall1.Click += new System.EventHandler(BtnCalcVatSmall1_Click);
		resources.ApplyResources(this.CBStore, "CBStore");
		this.CBStore.FormattingEnabled = true;
		this.CBStore.Name = "CBStore";
		this.CBStore.SelectedIndexChanged += new System.EventHandler(CBStore_SelectedIndexChanged);
		resources.ApplyResources(this.label74, "label74");
		this.label74.Name = "label74";
		resources.ApplyResources(this.label71, "label71");
		this.label71.Name = "label71";
		resources.ApplyResources(this.label69, "label69");
		this.label69.Name = "label69";
		resources.ApplyResources(this.label66, "label66");
		this.label66.Name = "label66";
		resources.ApplyResources(this.NQty, "NQty");
		this.NQty.DecimalPlaces = 3;
		this.NQty.ForeColor = System.Drawing.Color.Blue;
		this.NQty.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NQty.Maximum = new decimal(new int[4] { 1410065407, 2, 0, 0 });
		this.NQty.Minimum = new decimal(new int[4] { 1215752191, 23, 0, -2147483648 });
		this.NQty.Name = "NQty";
		resources.ApplyResources(this.label23, "label23");
		this.label23.Name = "label23";
		resources.ApplyResources(this.label25, "label25");
		this.label25.Name = "label25";
		resources.ApplyResources(this.label29, "label29");
		this.label29.Name = "label29";
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.label14, "label14");
		this.label14.Name = "label14";
		resources.ApplyResources(this.label17, "label17");
		this.label17.Name = "label17";
		resources.ApplyResources(this.NPrice3unit3, "NPrice3unit3");
		this.NPrice3unit3.DecimalPlaces = 3;
		this.NPrice3unit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice3unit3.Maximum = new decimal(new int[4] { 1410065407, 2, 0, 0 });
		this.NPrice3unit3.Name = "NPrice3unit3";
		resources.ApplyResources(this.NPrice3unit2, "NPrice3unit2");
		this.NPrice3unit2.DecimalPlaces = 3;
		this.NPrice3unit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice3unit2.Maximum = new decimal(new int[4] { 1410065407, 2, 0, 0 });
		this.NPrice3unit2.Name = "NPrice3unit2";
		resources.ApplyResources(this.label16, "label16");
		this.label16.Name = "label16";
		resources.ApplyResources(this.NPrice3unit1, "NPrice3unit1");
		this.NPrice3unit1.DecimalPlaces = 3;
		this.NPrice3unit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice3unit1.Maximum = new decimal(new int[4] { 1410065407, 2, 0, 0 });
		this.NPrice3unit1.Name = "NPrice3unit1";
		resources.ApplyResources(this.NPrice2unit3, "NPrice2unit3");
		this.NPrice2unit3.DecimalPlaces = 3;
		this.NPrice2unit3.Maximum = new decimal(new int[4] { 1215752191, 23, 0, 0 });
		this.NPrice2unit3.Name = "NPrice2unit3";
		resources.ApplyResources(this.NPrice1unit3, "NPrice1unit3");
		this.NPrice1unit3.DecimalPlaces = 3;
		this.NPrice1unit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit3.Maximum = new decimal(new int[4] { 1215752191, 23, 0, 0 });
		this.NPrice1unit3.Name = "NPrice1unit3";
		resources.ApplyResources(this.NPrice2unit2, "NPrice2unit2");
		this.NPrice2unit2.DecimalPlaces = 3;
		this.NPrice2unit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice2unit2.Maximum = new decimal(new int[4] { 1215752191, 23, 0, 0 });
		this.NPrice2unit2.Name = "NPrice2unit2";
		resources.ApplyResources(this.NPrice1unit2, "NPrice1unit2");
		this.NPrice1unit2.DecimalPlaces = 3;
		this.NPrice1unit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit2.Maximum = new decimal(new int[4] { 1215752191, 23, 0, 0 });
		this.NPrice1unit2.Name = "NPrice1unit2";
		resources.ApplyResources(this.NPrice2unit1, "NPrice2unit1");
		this.NPrice2unit1.DecimalPlaces = 3;
		this.NPrice2unit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice2unit1.Maximum = new decimal(new int[4] { 1410065407, 2, 0, 0 });
		this.NPrice2unit1.Name = "NPrice2unit1";
		resources.ApplyResources(this.TxtPerUnit3Price3, "TxtPerUnit3Price3");
		this.TxtPerUnit3Price3.DecimalPlaces = 2;
		this.TxtPerUnit3Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price3.Name = "TxtPerUnit3Price3";
		this.TxtPerUnit3Price3.ValueChanged += new System.EventHandler(TxtPerUnit3Price3_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit2Price3, "TxtPerUnit2Price3");
		this.TxtPerUnit2Price3.DecimalPlaces = 2;
		this.TxtPerUnit2Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price3.Name = "TxtPerUnit2Price3";
		this.TxtPerUnit2Price3.ValueChanged += new System.EventHandler(TxtPerUnit2Price3_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit1Price3, "TxtPerUnit1Price3");
		this.TxtPerUnit1Price3.DecimalPlaces = 2;
		this.TxtPerUnit1Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price3.Name = "TxtPerUnit1Price3";
		this.TxtPerUnit1Price3.ValueChanged += new System.EventHandler(TxtPerUnit1Price3_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit3Price2, "TxtPerUnit3Price2");
		this.TxtPerUnit3Price2.DecimalPlaces = 2;
		this.TxtPerUnit3Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price2.Name = "TxtPerUnit3Price2";
		this.TxtPerUnit3Price2.ValueChanged += new System.EventHandler(TxtPerUnit3Price2_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit2Price2, "TxtPerUnit2Price2");
		this.TxtPerUnit2Price2.DecimalPlaces = 2;
		this.TxtPerUnit2Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price2.Name = "TxtPerUnit2Price2";
		this.TxtPerUnit2Price2.ValueChanged += new System.EventHandler(TxtPerUnit2Price2_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit1Price2, "TxtPerUnit1Price2");
		this.TxtPerUnit1Price2.DecimalPlaces = 2;
		this.TxtPerUnit1Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price2.Name = "TxtPerUnit1Price2";
		this.TxtPerUnit1Price2.ValueChanged += new System.EventHandler(TxtPerUnit1Price2_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit3Price1, "TxtPerUnit3Price1");
		this.TxtPerUnit3Price1.DecimalPlaces = 2;
		this.TxtPerUnit3Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price1.Name = "TxtPerUnit3Price1";
		this.TxtPerUnit3Price1.ValueChanged += new System.EventHandler(TxtPerUnit3Price1_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit2Price1, "TxtPerUnit2Price1");
		this.TxtPerUnit2Price1.DecimalPlaces = 2;
		this.TxtPerUnit2Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price1.Name = "TxtPerUnit2Price1";
		this.TxtPerUnit2Price1.ValueChanged += new System.EventHandler(TxtPerUnit2Price1_ValueChanged);
		resources.ApplyResources(this.TxtPerUnit1Price1, "TxtPerUnit1Price1");
		this.TxtPerUnit1Price1.DecimalPlaces = 2;
		this.TxtPerUnit1Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price1.Name = "TxtPerUnit1Price1";
		this.TxtPerUnit1Price1.ValueChanged += new System.EventHandler(TxtPerUnit1Price1_ValueChanged);
		resources.ApplyResources(this.NPrice1unit1, "NPrice1unit1");
		this.NPrice1unit1.DecimalPlaces = 3;
		this.NPrice1unit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPrice1unit1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.NPrice1unit1.Name = "NPrice1unit1";
		resources.ApplyResources(this.label20, "label20");
		this.label20.Name = "label20";
		resources.ApplyResources(this.label32, "label32");
		this.label32.Name = "label32";
		resources.ApplyResources(this.label24, "label24");
		this.label24.Name = "label24";
		resources.ApplyResources(this.label52, "label52");
		this.label52.Name = "label52";
		resources.ApplyResources(this.label55, "label55");
		this.label55.Name = "label55";
		resources.ApplyResources(this.label13, "label13");
		this.label13.Name = "label13";
		resources.ApplyResources(this.label44, "label44");
		this.label44.Name = "label44";
		resources.ApplyResources(this.label51, "label51");
		this.label51.Name = "label51";
		resources.ApplyResources(this.nRequestunit1, "nRequestunit1");
		this.nRequestunit1.DecimalPlaces = 3;
		this.nRequestunit1.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.nRequestunit1.Name = "nRequestunit1";
		this.nRequestunit1.KeyDown += new System.Windows.Forms.KeyEventHandler(nRequestunit1_KeyDown);
		resources.ApplyResources(this.label21, "label21");
		this.label21.Name = "label21";
		resources.ApplyResources(this.NPriceMowazaunit3, "NPriceMowazaunit3");
		this.NPriceMowazaunit3.DecimalPlaces = 3;
		this.NPriceMowazaunit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPriceMowazaunit3.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NPriceMowazaunit3.Name = "NPriceMowazaunit3";
		this.NPriceMowazaunit3.KeyDown += new System.Windows.Forms.KeyEventHandler(nRequestunit3_KeyDown);
		resources.ApplyResources(this.NPriceExportunit3, "NPriceExportunit3");
		this.NPriceExportunit3.DecimalPlaces = 3;
		this.NPriceExportunit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPriceExportunit3.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NPriceExportunit3.Name = "NPriceExportunit3";
		this.NPriceExportunit3.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.NAverg, "NAverg");
		this.NAverg.DecimalPlaces = 3;
		this.NAverg.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NAverg.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NAverg.Minimum = new decimal(new int[4] { 1316134911, 2328, 0, -2147483648 });
		this.NAverg.Name = "NAverg";
		this.NAverg.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label53, "label53");
		this.label53.Name = "label53";
		resources.ApplyResources(this.NPriceExportunit1, "NPriceExportunit1");
		this.NPriceExportunit1.DecimalPlaces = 3;
		this.NPriceExportunit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPriceExportunit1.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NPriceExportunit1.Name = "NPriceExportunit1";
		this.NPriceExportunit1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label54, "label54");
		this.label54.Name = "label54";
		resources.ApplyResources(this.NPriceMowazaunit2, "NPriceMowazaunit2");
		this.NPriceMowazaunit2.DecimalPlaces = 3;
		this.NPriceMowazaunit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPriceMowazaunit2.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NPriceMowazaunit2.Name = "NPriceMowazaunit2";
		this.NPriceMowazaunit2.KeyDown += new System.Windows.Forms.KeyEventHandler(nRequestunit2_KeyDown);
		resources.ApplyResources(this.NPriceExportunit2, "NPriceExportunit2");
		this.NPriceExportunit2.DecimalPlaces = 3;
		this.NPriceExportunit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.NPriceExportunit2.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.NPriceExportunit2.Name = "NPriceExportunit2";
		this.NPriceExportunit2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.separatorControl5, "separatorControl5");
		this.separatorControl5.LineThickness = 1;
		this.separatorControl5.Name = "separatorControl5";
		resources.ApplyResources(this.separatorControl2, "separatorControl2");
		this.separatorControl2.LineThickness = 1;
		this.separatorControl2.Name = "separatorControl2";
		resources.ApplyResources(this.separatorControl4, "separatorControl4");
		this.separatorControl4.LineThickness = 1;
		this.separatorControl4.Name = "separatorControl4";
		resources.ApplyResources(this.nlastpriceunit3, "nlastpriceunit3");
		this.nlastpriceunit3.DecimalPlaces = 3;
		this.nlastpriceunit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nlastpriceunit3.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.nlastpriceunit3.Name = "nlastpriceunit3";
		this.nlastpriceunit3.ValueChanged += new System.EventHandler(nlastpriceunit3_ValueChanged);
		this.nlastpriceunit3.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.nlastpriceunit2, "nlastpriceunit2");
		this.nlastpriceunit2.DecimalPlaces = 3;
		this.nlastpriceunit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nlastpriceunit2.Maximum = new decimal(new int[4] { 1316134911, 2328, 0, 0 });
		this.nlastpriceunit2.Name = "nlastpriceunit2";
		this.nlastpriceunit2.ValueChanged += new System.EventHandler(nlastpriceunit2_ValueChanged);
		this.nlastpriceunit2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.nlastpriceunit1, "nlastpriceunit1");
		this.nlastpriceunit1.DecimalPlaces = 3;
		this.nlastpriceunit1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nlastpriceunit1.Maximum = new decimal(new int[4] { -727379969, 232, 0, 0 });
		this.nlastpriceunit1.Name = "nlastpriceunit1";
		this.nlastpriceunit1.ValueChanged += new System.EventHandler(nlastpriceunit1_ValueChanged);
		this.nlastpriceunit1.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.nlastpriceunit1.KeyUp += new System.Windows.Forms.KeyEventHandler(nlastpriceunit1_KeyUp);
		resources.ApplyResources(this.label19, "label19");
		this.label19.Name = "label19";
		resources.ApplyResources(this.xtraTabPage3, "xtraTabPage3");
		this.xtraTabPage3.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("xtraTabPage3.Appearance.Header.Font");
		this.xtraTabPage3.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage3.Controls.Add(this.comboPrint);
		this.xtraTabPage3.Controls.Add(this.label56);
		this.xtraTabPage3.Controls.Add(this.label26);
		this.xtraTabPage3.Controls.Add(this.label15);
		this.xtraTabPage3.Controls.Add(this.nRequestunit3);
		this.xtraTabPage3.Controls.Add(this.nRequestunit2);
		this.xtraTabPage3.Controls.Add(this.checkBox8);
		this.xtraTabPage3.Controls.Add(this.TxtNotes);
		this.xtraTabPage3.Controls.Add(this.checkBox7);
		this.xtraTabPage3.Controls.Add(this.checkBox6);
		this.xtraTabPage3.Controls.Add(this.checkBox5);
		this.xtraTabPage3.Controls.Add(this.label37);
		this.xtraTabPage3.Controls.Add(this.checkBox4);
		this.xtraTabPage3.Controls.Add(this.checkBox3);
		this.xtraTabPage3.Controls.Add(this.checkBox2);
		this.xtraTabPage3.Controls.Add(this.separatorControl8);
		this.xtraTabPage3.Name = "xtraTabPage3";
		this.xtraTabPage3.PageVisible = false;
		resources.ApplyResources(this.comboPrint, "comboPrint");
		this.comboPrint.FormattingEnabled = true;
		this.comboPrint.Name = "comboPrint";
		resources.ApplyResources(this.label56, "label56");
		this.label56.Name = "label56";
		resources.ApplyResources(this.label26, "label26");
		this.label26.Name = "label26";
		resources.ApplyResources(this.label15, "label15");
		this.label15.Name = "label15";
		resources.ApplyResources(this.nRequestunit3, "nRequestunit3");
		this.nRequestunit3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nRequestunit3.Maximum = new decimal(new int[4] { 99999999, 0, 0, 0 });
		this.nRequestunit3.Name = "nRequestunit3";
		resources.ApplyResources(this.nRequestunit2, "nRequestunit2");
		this.nRequestunit2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nRequestunit2.Maximum = new decimal(new int[4] { 9999999, 0, 0, 0 });
		this.nRequestunit2.Name = "nRequestunit2";
		resources.ApplyResources(this.checkBox8, "checkBox8");
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox7, "checkBox7");
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox6, "checkBox6");
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox5, "checkBox5");
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox4, "checkBox4");
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox3, "checkBox3");
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.checkBox2, "checkBox2");
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.separatorControl8, "separatorControl8");
		this.separatorControl8.LineColor = System.Drawing.Color.RoyalBlue;
		this.separatorControl8.LineThickness = 1;
		this.separatorControl8.Name = "separatorControl8";
		resources.ApplyResources(this.xtraTabPage5, "xtraTabPage5");
		this.xtraTabPage5.Controls.Add(this.GropElctronc);
		this.xtraTabPage5.Controls.Add(this.label77);
		this.xtraTabPage5.Controls.Add(this.label76);
		this.xtraTabPage5.Controls.Add(this.NTobacco);
		this.xtraTabPage5.Controls.Add(this.label68);
		this.xtraTabPage5.Controls.Add(this.BtnGuanartyP);
		this.xtraTabPage5.Controls.Add(this.BtnCompanyP);
		this.xtraTabPage5.Controls.Add(this.CHISItemMizan);
		this.xtraTabPage5.Controls.Add(this.BtnContryP);
		this.xtraTabPage5.Controls.Add(this.BtnClassP);
		this.xtraTabPage5.Controls.Add(this.BtnGroupP);
		this.xtraTabPage5.Controls.Add(this.groupBox3);
		this.xtraTabPage5.Controls.Add(this.BtnGuanartyR);
		this.xtraTabPage5.Controls.Add(this.BtnCompanyR);
		this.xtraTabPage5.Controls.Add(this.BtnContryR);
		this.xtraTabPage5.Controls.Add(this.BtnClassR);
		this.xtraTabPage5.Controls.Add(this.BtnGroupR);
		this.xtraTabPage5.Controls.Add(this.CBCat);
		this.xtraTabPage5.Controls.Add(this.BtnImage);
		this.xtraTabPage5.Controls.Add(this.label47);
		this.xtraTabPage5.Controls.Add(this.pictureEdit1);
		this.xtraTabPage5.Controls.Add(this.CBGuarantee);
		this.xtraTabPage5.Controls.Add(this.label58);
		this.xtraTabPage5.Controls.Add(this.CBCountry);
		this.xtraTabPage5.Controls.Add(this.label57);
		this.xtraTabPage5.Controls.Add(this.CbItemtype);
		this.xtraTabPage5.Controls.Add(this.CBCompany);
		this.xtraTabPage5.Controls.Add(this.CHVat);
		this.xtraTabPage5.Controls.Add(this.label2);
		this.xtraTabPage5.Controls.Add(this.label49);
		this.xtraTabPage5.Controls.Add(this.label9);
		this.xtraTabPage5.Controls.Add(this.label6);
		this.xtraTabPage5.Controls.Add(this.NVat);
		this.xtraTabPage5.Controls.Add(this.CBClass);
		this.xtraTabPage5.Controls.Add(this.label3);
		this.xtraTabPage5.Name = "xtraTabPage5";
		resources.ApplyResources(this.GropElctronc, "GropElctronc");
		this.GropElctronc.Controls.Add(this.label79);
		this.GropElctronc.Controls.Add(this.TxtExemptionReason);
		this.GropElctronc.Controls.Add(this.label78);
		this.GropElctronc.Controls.Add(this.TxtVatCode);
		this.GropElctronc.Name = "GropElctronc";
		this.GropElctronc.TabStop = false;
		resources.ApplyResources(this.label79, "label79");
		this.label79.Name = "label79";
		resources.ApplyResources(this.TxtExemptionReason, "TxtExemptionReason");
		this.TxtExemptionReason.FormattingEnabled = true;
		this.TxtExemptionReason.Name = "TxtExemptionReason";
		resources.ApplyResources(this.label78, "label78");
		this.label78.Name = "label78";
		resources.ApplyResources(this.TxtVatCode, "TxtVatCode");
		this.TxtVatCode.FormattingEnabled = true;
		this.TxtVatCode.Name = "TxtVatCode";
		this.TxtVatCode.SelectedIndexChanged += new System.EventHandler(TxtVatCode_SelectedIndexChanged);
		resources.ApplyResources(this.label77, "label77");
		this.label77.Name = "label77";
		resources.ApplyResources(this.label76, "label76");
		this.label76.Name = "label76";
		resources.ApplyResources(this.NTobacco, "NTobacco");
		this.NTobacco.DecimalPlaces = 2;
		this.NTobacco.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.NTobacco.Name = "NTobacco";
		resources.ApplyResources(this.label68, "label68");
		this.label68.Name = "label68";
		resources.ApplyResources(this.BtnGuanartyP, "BtnGuanartyP");
		this.BtnGuanartyP.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGuanartyP.ImageOptions.Image");
		this.BtnGuanartyP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGuanartyP.Name = "BtnGuanartyP";
		this.BtnGuanartyP.Click += new System.EventHandler(BtnGuanartyP_Click);
		resources.ApplyResources(this.BtnCompanyP, "BtnCompanyP");
		this.BtnCompanyP.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCompanyP.ImageOptions.Image");
		this.BtnCompanyP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCompanyP.Name = "BtnCompanyP";
		this.BtnCompanyP.Click += new System.EventHandler(BtnCompanyP_Click);
		resources.ApplyResources(this.CHISItemMizan, "CHISItemMizan");
		this.CHISItemMizan.Name = "CHISItemMizan";
		this.CHISItemMizan.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.BtnContryP, "BtnContryP");
		this.BtnContryP.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContryP.ImageOptions.Image");
		this.BtnContryP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContryP.Name = "BtnContryP";
		this.BtnContryP.Click += new System.EventHandler(BtnContryP_Click);
		resources.ApplyResources(this.BtnClassP, "BtnClassP");
		this.BtnClassP.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnClassP.ImageOptions.Image");
		this.BtnClassP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClassP.Name = "BtnClassP";
		this.BtnClassP.Click += new System.EventHandler(BtnClassP_Click);
		resources.ApplyResources(this.BtnGroupP, "BtnGroupP");
		this.BtnGroupP.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGroupP.ImageOptions.Image");
		this.BtnGroupP.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGroupP.Name = "BtnGroupP";
		this.BtnGroupP.Click += new System.EventHandler(BtnGroupP_Click);
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Controls.Add(this.CBISOffer);
		this.groupBox3.Controls.Add(this.TxtNumOffer);
		this.groupBox3.Controls.Add(this.TxtPriceOffer);
		this.groupBox3.Controls.Add(this.label46);
		this.groupBox3.Controls.Add(this.label45);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		resources.ApplyResources(this.CBISOffer, "CBISOffer");
		this.CBISOffer.Name = "CBISOffer";
		this.CBISOffer.UseVisualStyleBackColor = true;
		this.CBISOffer.CheckedChanged += new System.EventHandler(CBISOffer_CheckedChanged_1);
		resources.ApplyResources(this.TxtNumOffer, "TxtNumOffer");
		this.TxtNumOffer.DecimalPlaces = 2;
		this.TxtNumOffer.Maximum = new decimal(new int[4] { 99999, 0, 0, 0 });
		this.TxtNumOffer.Name = "TxtNumOffer";
		resources.ApplyResources(this.TxtPriceOffer, "TxtPriceOffer");
		this.TxtPriceOffer.DecimalPlaces = 2;
		this.TxtPriceOffer.Maximum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.TxtPriceOffer.Name = "TxtPriceOffer";
		resources.ApplyResources(this.label46, "label46");
		this.label46.Name = "label46";
		resources.ApplyResources(this.label45, "label45");
		this.label45.Name = "label45";
		resources.ApplyResources(this.BtnGuanartyR, "BtnGuanartyR");
		this.BtnGuanartyR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGuanartyR.ImageOptions.Image");
		this.BtnGuanartyR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGuanartyR.Name = "BtnGuanartyR";
		this.BtnGuanartyR.Click += new System.EventHandler(BtnGuanartyR_Click);
		resources.ApplyResources(this.BtnCompanyR, "BtnCompanyR");
		this.BtnCompanyR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCompanyR.ImageOptions.Image");
		this.BtnCompanyR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCompanyR.Name = "BtnCompanyR";
		this.BtnCompanyR.Click += new System.EventHandler(BtnCompanyR_Click);
		resources.ApplyResources(this.BtnContryR, "BtnContryR");
		this.BtnContryR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnContryR.ImageOptions.Image");
		this.BtnContryR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnContryR.Name = "BtnContryR";
		this.BtnContryR.Click += new System.EventHandler(BtnContryR_Click);
		resources.ApplyResources(this.BtnClassR, "BtnClassR");
		this.BtnClassR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnClassR.ImageOptions.Image");
		this.BtnClassR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnClassR.Name = "BtnClassR";
		this.BtnClassR.Click += new System.EventHandler(BtnClassR_Click);
		resources.ApplyResources(this.BtnGroupR, "BtnGroupR");
		this.BtnGroupR.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnGroupR.ImageOptions.Image");
		this.BtnGroupR.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnGroupR.Name = "BtnGroupR";
		this.BtnGroupR.Click += new System.EventHandler(BtnGroupR_Click);
		resources.ApplyResources(this.CBCat, "CBCat");
		this.CBCat.FormattingEnabled = true;
		this.CBCat.Name = "CBCat";
		resources.ApplyResources(this.BtnImage, "BtnImage");
		this.BtnImage.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnImage.ImageOptions.Image");
		this.BtnImage.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnImage.Name = "BtnImage";
		this.BtnImage.Click += new System.EventHandler(BtnImage_Click_2);
		resources.ApplyResources(this.label47, "label47");
		this.label47.Name = "label47";
		resources.ApplyResources(this.pictureEdit1, "pictureEdit1");
		this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
		this.pictureEdit1.Name = "pictureEdit1";
		this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
		this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
		this.pictureEdit1.Click += new System.EventHandler(BtnImage_Click_1);
		resources.ApplyResources(this.CBGuarantee, "CBGuarantee");
		this.CBGuarantee.FormattingEnabled = true;
		this.CBGuarantee.Name = "CBGuarantee";
		resources.ApplyResources(this.label58, "label58");
		this.label58.Name = "label58";
		resources.ApplyResources(this.CBCountry, "CBCountry");
		this.CBCountry.FormattingEnabled = true;
		this.CBCountry.Name = "CBCountry";
		resources.ApplyResources(this.label57, "label57");
		this.label57.Name = "label57";
		resources.ApplyResources(this.xtraTabPage4, "xtraTabPage4");
		this.xtraTabPage4.Appearance.Header.Font = (System.Drawing.Font)resources.GetObject("xtraTabPage4.Appearance.Header.Font");
		this.xtraTabPage4.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage4.Controls.Add(this.gridControl1);
		this.xtraTabPage4.Name = "xtraTabPage4";
		this.xtraTabPage4.PageVisible = false;
		resources.ApplyResources(this.gridControl1, "gridControl1");
		this.gridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleDescription");
		this.gridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleName");
		this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip");
		this.gridControl1.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gridControl1.EmbeddedNavigator.Anchor");
		this.gridControl1.EmbeddedNavigator.BackgroundImage = (System.Drawing.Image)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImage");
		this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout");
		this.gridControl1.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode");
		this.gridControl1.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl1.EmbeddedNavigator.Margin");
		this.gridControl1.EmbeddedNavigator.MaximumSize = (System.Drawing.Size)resources.GetObject("gridControl1.EmbeddedNavigator.MaximumSize");
		this.gridControl1.EmbeddedNavigator.TextLocation = (DevExpress.XtraEditors.NavigatorButtonsTextLocation)resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation");
		this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
		this.gridControl1.EmbeddedNavigator.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType");
		this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[9] { this.repositoryItemButtonEdit4, this.repositoryItemButtonEdit6, this.repositoryItemTextEdit4, this.repositoryItemTextEdit5, this.repositoryItemDateEdit2, this.repositoryItemTextEdit6, this.repositoryItemPictureEdit2, this.repositoryItemButtonEdit5, this.repositoryItemButtonEdit7 });
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2] { this.gridView1, this.cardView1 });
		this.gridView1.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridView1.Appearance.HeaderPanel.Font");
		this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
		resources.ApplyResources(this.gridView1, "gridView1");
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[12]
		{
			this.gridColumn1, this.gridColumn2, this.shearch, this.gridColumn19, this.gridColumn4, this.gridColumn20, this.gridColumn21, this.gridColumn3, this.gridColumn6, this.gridColumn7,
			this.gridColumn8, this.gridColumn9
		});
		this.gridView1.DetailHeight = 431;
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridView1.OptionsNavigation.AutoFocusNewRow = true;
		this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
		this.gridView1.OptionsPrint.EnableAppearanceOddRow = true;
		this.gridView1.OptionsPrint.PrintFilterInfo = true;
		this.gridView1.OptionsPrint.PrintPreview = true;
		this.gridView1.OptionsView.RowAutoHeight = true;
		this.gridView1.OptionsView.ShowFooter = true;
		this.gridView1.OptionsView.ShowGroupPanel = false;
		resources.ApplyResources(this.gridColumn1, "gridColumn1");
		this.gridColumn1.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceCell.Font");
		this.gridColumn1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn1.AppearanceHeader.Font");
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.ColumnEdit = this.repositoryItemButtonEdit7;
		this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn1.FieldName = "كود_الصنف";
		this.gridColumn1.MinWidth = 23;
		this.gridColumn1.Name = "gridColumn1";
		resources.ApplyResources(this.repositoryItemButtonEdit7, "repositoryItemButtonEdit7");
		resources.ApplyResources(editorButtonImageOptions17, "editorButtonImageOptions17");
		this.repositoryItemButtonEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit7.Buttons"), resources.GetString("repositoryItemButtonEdit7.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit7.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit7.Buttons5"), editorButtonImageOptions17, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance17, appearanceHovered17, appearancePressed17, appearanceDisabled17, resources.GetString("repositoryItemButtonEdit7.Buttons6"), resources.GetObject("repositoryItemButtonEdit7.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit7.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit7.Buttons9"))
		});
		this.repositoryItemButtonEdit7.Name = "repositoryItemButtonEdit7";
		this.repositoryItemButtonEdit7.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit7_ButtonClick);
		resources.ApplyResources(this.gridColumn2, "gridColumn2");
		this.gridColumn2.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceCell.Font");
		this.gridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn2.AppearanceHeader.Font");
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.FieldName = "أسم الصنف";
		this.gridColumn2.MinWidth = 23;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.shearch, "shearch");
		this.shearch.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceCell.Font");
		this.shearch.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.shearch.AppearanceCell.Options.UseFont = true;
		this.shearch.AppearanceCell.Options.UseForeColor = true;
		this.shearch.AppearanceCell.Options.UseTextOptions = true;
		this.shearch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("shearch.AppearanceHeader.Font");
		this.shearch.AppearanceHeader.Options.UseFont = true;
		this.shearch.AppearanceHeader.Options.UseTextOptions = true;
		this.shearch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.shearch.ColumnEdit = this.repositoryItemButtonEdit4;
		this.shearch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.FieldName = "الوحدة";
		this.shearch.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
		this.shearch.MinWidth = 23;
		this.shearch.Name = "shearch";
		this.shearch.OptionsColumn.ReadOnly = true;
		this.repositoryItemButtonEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
		editorButtonImageOptions18.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions18.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit4.Buttons"), resources.GetString("repositoryItemButtonEdit4.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit4.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit4.Buttons5"), editorButtonImageOptions18, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance18, appearanceHovered18, appearancePressed18, appearanceDisabled18, resources.GetString("repositoryItemButtonEdit4.Buttons6"), resources.GetObject("repositoryItemButtonEdit4.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit4.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit4.Buttons9"))
		});
		this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
		this.repositoryItemButtonEdit4.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed_1);
		resources.ApplyResources(this.gridColumn19, "gridColumn19");
		this.gridColumn19.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn19.AppearanceHeader.Font");
		this.gridColumn19.AppearanceHeader.Options.UseFont = true;
		this.gridColumn19.FieldName = "أسم_الوحدة";
		this.gridColumn19.MinWidth = 23;
		this.gridColumn19.Name = "gridColumn19";
		this.gridColumn19.OptionsColumn.AllowEdit = false;
		this.gridColumn19.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn4, "gridColumn4");
		this.gridColumn4.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceCell.Font");
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn4.AppearanceHeader.Font");
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit5;
		this.gridColumn4.FieldName = "المخزن";
		this.gridColumn4.MinWidth = 23;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.repositoryItemButtonEdit5, "repositoryItemButtonEdit5");
		this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit5.Buttons"))
		});
		this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
		this.repositoryItemButtonEdit5.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit3_ButtonClick);
		resources.ApplyResources(this.gridColumn20, "gridColumn20");
		this.gridColumn20.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceCell.Font");
		this.gridColumn20.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn20.AppearanceCell.Options.UseFont = true;
		this.gridColumn20.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn20.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn20.AppearanceHeader.Font");
		this.gridColumn20.AppearanceHeader.Options.UseFont = true;
		this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.FieldName = "الكمية";
		this.gridColumn20.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
		this.gridColumn20.MinWidth = 23;
		this.gridColumn20.Name = "gridColumn20";
		this.gridColumn20.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn21, "gridColumn21");
		this.gridColumn21.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceCell.Font");
		this.gridColumn21.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn21.AppearanceCell.Options.UseFont = true;
		this.gridColumn21.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn21.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn21.AppearanceHeader.Font");
		this.gridColumn21.AppearanceHeader.Options.UseFont = true;
		this.gridColumn21.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn21.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.gridColumn21.FieldName = "ظ….التكلفة";
		this.gridColumn21.MinWidth = 23;
		this.gridColumn21.Name = "gridColumn21";
		this.gridColumn21.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn3, "gridColumn3");
		this.gridColumn3.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceCell.Font");
		this.gridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.Black;
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn3.AppearanceHeader.Font");
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.FieldName = "الاجمالى";
		this.gridColumn3.MinWidth = 23;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn6, "gridColumn6");
		this.gridColumn6.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn6.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceCell.Font");
		this.gridColumn6.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn6.AppearanceCell.FontStyleDelta");
		this.gridColumn6.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn6.AppearanceHeader.Font");
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.FieldName = "الضربية";
		this.gridColumn6.MinWidth = 23;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.ReadOnly = true;
		this.gridColumn6.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn7, "gridColumn7");
		this.gridColumn7.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn7.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceCell.Font");
		this.gridColumn7.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn7.AppearanceHeader.Font");
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.FieldName = "إجمالى المبلغ";
		this.gridColumn7.MinWidth = 23;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.ReadOnly = true;
		this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
		resources.ApplyResources(this.gridColumn8, "gridColumn8");
		this.gridColumn8.AppearanceCell.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.gridColumn8.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceCell.Font");
		this.gridColumn8.AppearanceCell.FontStyleDelta = (System.Drawing.FontStyle)resources.GetObject("gridColumn8.AppearanceCell.FontStyleDelta");
		this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.White;
		this.gridColumn8.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn8.AppearanceHeader.Font");
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit6;
		this.gridColumn8.FieldName = "حذف";
		this.gridColumn8.MinWidth = 23;
		this.gridColumn8.Name = "gridColumn8";
		this.repositoryItemButtonEdit6.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit6, "repositoryItemButtonEdit6");
		this.repositoryItemButtonEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit6.Buttons"), resources.GetString("repositoryItemButtonEdit6.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit6.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit6.Buttons5"), imageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance19, appearanceHovered19, appearancePressed19, appearanceDisabled19, resources.GetString("repositoryItemButtonEdit6.Buttons6"), resources.GetObject("repositoryItemButtonEdit6.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit6.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit6.Buttons9"))
		});
		this.repositoryItemButtonEdit6.Name = "repositoryItemButtonEdit6";
		this.repositoryItemButtonEdit6.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit6.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonClick);
		resources.ApplyResources(this.gridColumn9, "gridColumn9");
		this.gridColumn9.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
		this.gridColumn9.AppearanceCell.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceCell.Font");
		this.gridColumn9.AppearanceCell.Options.UseBackColor = true;
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = (System.Drawing.Font)resources.GetObject("gridColumn9.AppearanceHeader.Font");
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.FieldName = "ظ…";
		this.gridColumn9.MinWidth = 23;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.OptionsColumn.AllowEdit = false;
		resources.ApplyResources(this.repositoryItemTextEdit4, "repositoryItemTextEdit4");
		this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
		resources.ApplyResources(this.repositoryItemTextEdit5, "repositoryItemTextEdit5");
		this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
		resources.ApplyResources(this.repositoryItemDateEdit2, "repositoryItemDateEdit2");
		this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit2.Buttons"))
		});
		this.repositoryItemDateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit2.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
		resources.ApplyResources(this.repositoryItemTextEdit6, "repositoryItemTextEdit6");
		this.repositoryItemTextEdit6.Name = "repositoryItemTextEdit6";
		this.repositoryItemPictureEdit2.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit2.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit2.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit2.Name = "repositoryItemPictureEdit2";
		this.repositoryItemPictureEdit2.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		resources.ApplyResources(this.cardView1, "cardView1");
		this.cardView1.CardWidth = 232;
		this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.gridColumn10, this.gridColumn11, this.gridColumn12, this.gridColumn13, this.gridColumn14, this.gridColumn15, this.gridColumn16, this.gridColumn17, this.gridColumn18 });
		this.cardView1.DetailHeight = 431;
		this.cardView1.GridControl = this.gridControl1;
		this.cardView1.Name = "cardView1";
		this.cardView1.OptionsPrint.PrintFilterInfo = true;
		this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
		resources.ApplyResources(this.gridColumn10, "gridColumn10");
		this.gridColumn10.MinWidth = 23;
		this.gridColumn10.Name = "gridColumn10";
		resources.ApplyResources(this.gridColumn11, "gridColumn11");
		this.gridColumn11.MinWidth = 23;
		this.gridColumn11.Name = "gridColumn11";
		resources.ApplyResources(this.gridColumn12, "gridColumn12");
		this.gridColumn12.MinWidth = 23;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.ReadOnly = true;
		resources.ApplyResources(this.gridColumn13, "gridColumn13");
		this.gridColumn13.MinWidth = 23;
		this.gridColumn13.Name = "gridColumn13";
		resources.ApplyResources(this.gridColumn14, "gridColumn14");
		this.gridColumn14.MinWidth = 23;
		this.gridColumn14.Name = "gridColumn14";
		resources.ApplyResources(this.gridColumn15, "gridColumn15");
		this.gridColumn15.MinWidth = 23;
		this.gridColumn15.Name = "gridColumn15";
		resources.ApplyResources(this.gridColumn16, "gridColumn16");
		this.gridColumn16.MinWidth = 23;
		this.gridColumn16.Name = "gridColumn16";
		resources.ApplyResources(this.gridColumn17, "gridColumn17");
		this.gridColumn17.MinWidth = 23;
		this.gridColumn17.Name = "gridColumn17";
		resources.ApplyResources(this.gridColumn18, "gridColumn18");
		this.gridColumn18.MinWidth = 23;
		this.gridColumn18.Name = "gridColumn18";
		resources.ApplyResources(this.xtraTabPage6, "xtraTabPage6");
		this.xtraTabPage6.Controls.Add(this.label63);
		this.xtraTabPage6.Controls.Add(this.Npharmacist);
		this.xtraTabPage6.Controls.Add(this.label64);
		this.xtraTabPage6.Controls.Add(this.label61);
		this.xtraTabPage6.Controls.Add(this.NDoctor);
		this.xtraTabPage6.Controls.Add(this.label62);
		this.xtraTabPage6.Controls.Add(this.label60);
		this.xtraTabPage6.Controls.Add(this.NDiscItem);
		this.xtraTabPage6.Controls.Add(this.label59);
		this.xtraTabPage6.Name = "xtraTabPage6";
		this.xtraTabPage6.PageVisible = false;
		resources.ApplyResources(this.label63, "label63");
		this.label63.Name = "label63";
		resources.ApplyResources(this.Npharmacist, "Npharmacist");
		this.Npharmacist.DecimalPlaces = 2;
		this.Npharmacist.Maximum = new decimal(new int[4] { 99, 0, 0, 0 });
		this.Npharmacist.Name = "Npharmacist";
		resources.ApplyResources(this.label64, "label64");
		this.label64.Name = "label64";
		resources.ApplyResources(this.label61, "label61");
		this.label61.Name = "label61";
		resources.ApplyResources(this.NDoctor, "NDoctor");
		this.NDoctor.DecimalPlaces = 2;
		this.NDoctor.Maximum = new decimal(new int[4] { 99, 0, 0, 0 });
		this.NDoctor.Name = "NDoctor";
		resources.ApplyResources(this.label62, "label62");
		this.label62.Name = "label62";
		resources.ApplyResources(this.label60, "label60");
		this.label60.Name = "label60";
		resources.ApplyResources(this.NDiscItem, "NDiscItem");
		this.NDiscItem.DecimalPlaces = 2;
		this.NDiscItem.Maximum = new decimal(new int[4] { 99, 0, 0, 0 });
		this.NDiscItem.Name = "NDiscItem";
		resources.ApplyResources(this.label59, "label59");
		this.label59.Name = "label59";
		this.repositoryItemButtonEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
		editorButtonImageOptions19.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
		editorButtonImageOptions19.ImageUri.Uri = "Zoom100";
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit1.Buttons"), resources.GetString("repositoryItemButtonEdit1.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit1.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit1.Buttons5"), editorButtonImageOptions19, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance20, appearanceHovered20, appearancePressed20, appearanceDisabled20, resources.GetString("repositoryItemButtonEdit1.Buttons6"), resources.GetObject("repositoryItemButtonEdit1.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit1.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit1.Buttons9"))
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit2.Appearance.Options.UseImage = true;
		resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit2.Buttons"), resources.GetString("repositoryItemButtonEdit2.Buttons1"), (int)resources.GetObject("repositoryItemButtonEdit2.Buttons2"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons3"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons4"), (bool)resources.GetObject("repositoryItemButtonEdit2.Buttons5"), imageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance21, appearanceHovered21, appearancePressed21, appearanceDisabled21, resources.GetString("repositoryItemButtonEdit2.Buttons6"), resources.GetObject("repositoryItemButtonEdit2.Buttons7"), (DevExpress.Utils.SuperToolTip)resources.GetObject("repositoryItemButtonEdit2.Buttons8"), (DevExpress.Utils.ToolTipAnchor)resources.GetObject("repositoryItemButtonEdit2.Buttons9"))
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
		this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
		resources.ApplyResources(this.repositoryItemTextEdit2, "repositoryItemTextEdit2");
		this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
		resources.ApplyResources(this.repositoryItemDateEdit1, "repositoryItemDateEdit1");
		this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.Buttons"))
		});
		this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemDateEdit1.CalendarTimeProperties.Buttons"))
		});
		this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
		resources.ApplyResources(this.repositoryItemTextEdit3, "repositoryItemTextEdit3");
		this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
		this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceDisabled.Options.UseImage = true;
		this.repositoryItemPictureEdit1.AppearanceReadOnly.Options.UseImage = true;
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
		resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
		this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repositoryItemButtonEdit3.Buttons"))
		});
		this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
		this.openFileDialog1.FileName = "openFileDialog1";
		resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
		resources.ApplyResources(this.label80, "label80");
		this.label80.Name = "label80";
		resources.ApplyResources(this.label81, "label81");
		this.label81.Name = "label81";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.TapPrice);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmItems";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmItems_FormClosing);
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtitemnameEn.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemName.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.buttonEdit1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtItemCode.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NVat).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TapPrice).EndInit();
		this.TapPrice.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeBig2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeMedium2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBarcodeSmall2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton2.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton3.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtLocaton1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).EndInit();
		this.xtraTabPage2.ResumeLayout(false);
		this.xtraTabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.NQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice3unit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice2unit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPrice1unit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceMowazaunit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NAverg).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceMowazaunit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NPriceExportunit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nlastpriceunit1).EndInit();
		this.xtraTabPage3.ResumeLayout(false);
		this.xtraTabPage3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nRequestunit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl8).EndInit();
		this.xtraTabPage5.ResumeLayout(false);
		this.xtraTabPage5.PerformLayout();
		this.GropElctronc.ResumeLayout(false);
		this.GropElctronc.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.NTobacco).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNumOffer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPriceOffer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureEdit1.Properties).EndInit();
		this.xtraTabPage4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cardView1).EndInit();
		this.xtraTabPage6.ResumeLayout(false);
		this.xtraTabPage6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Npharmacist).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.NDiscItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1.CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemDateEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTextEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.behaviorManager1).EndInit();
		base.ResumeLayout(false);
	}
}


