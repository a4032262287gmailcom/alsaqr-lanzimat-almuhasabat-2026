using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using GTSErpSystem.BLL.Items;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.FrmsMain;
using Excel = Microsoft.Office.Interop.Excel;

namespace GTSErpSystem.Frms.Items;

public class FrmImportExcel : XtraForm
{
	private Class_Items Items = new Class_Items();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private Class_Purchases Pur = new Class_Purchases();

	private Class_Items items = new Class_Items();

	private GTSdbEntities db = new GTSdbEntities();

	private DataTable datatable = new DataTable();

	private DataTable dtCurrentTable = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private ButtonEdit TxtPathFile;

	private SimpleButton BtnExit;

	private SimpleButton BtnLoad;

	private SimpleButton BtnNew;

	private Label label2;

	private Label label1;

	private GroupBox groupBox2;

	private DataGridView dataGridView1;

	private System.Windows.Forms.ComboBox comboBox1;

	private SimpleButton BtnDownload;

	private SimpleButton BtnSave;

	private GroupBox groupBox3;

	private System.Windows.Forms.ComboBox CBGuarantee;

	private Label label58;

	private System.Windows.Forms.ComboBox CBCountry;

	private Label label57;

	private System.Windows.Forms.ComboBox CbItemtype;

	private System.Windows.Forms.ComboBox CBCompany;

	private Label label3;

	private Label label9;

	private Label label6;

	private NumericUpDown NVat;

	private System.Windows.Forms.ComboBox CBClass;

	private Label label4;

	private System.Windows.Forms.ComboBox CBCat;

	private SimpleButton BtnVat;

	private Label label5;

	private XtraTabControl TapPrice;

	private XtraTabPage xtraTabPage1;

	private System.Windows.Forms.ComboBox CbUnit2;

	private System.Windows.Forms.ComboBox CbUnit3;

	private System.Windows.Forms.ComboBox CbUnit1;

	private NumericUpDown nConvertUnit3;

	private Label label28;

	private Label LblUnit3;

	private NumericUpDown nConvertUnit2;

	private Label label27;

	private Label LblUnit2;

	private NumericUpDown numericUpDown1;

	private Label LblUnit;

	private Label LblUnit1;

	private RadioButton RBUnitTafqit3;

	private RadioButton RBUnitTafqit2;

	private RadioButton RBUnitTafqit1;

	private SeparatorControl separatorControl1;

	private SeparatorControl separatorControl3;

	private Label label31;

	private Label label30;

	private Label label36;

	private XtraTabPage xtraTabPage2;

	private Label label16;

	private NumericUpDown TxtPerUnit3Price3;

	private NumericUpDown TxtPerUnit2Price3;

	private NumericUpDown TxtPerUnit1Price3;

	private NumericUpDown TxtPerUnit3Price2;

	private NumericUpDown TxtPerUnit2Price2;

	private NumericUpDown TxtPerUnit1Price2;

	private NumericUpDown TxtPerUnit3Price1;

	private NumericUpDown TxtPerUnit2Price1;

	private NumericUpDown TxtPerUnit1Price1;

	private Label label20;

	private Label label32;

	private SeparatorControl separatorControl4;

	private Label label10;

	private Label label18;

	private SimpleButton BtnCreateCode;

	private SimpleButton BtnUnit1Price;

	private Label label8;

	private Label label7;

	private Label label17;

	private Label label15;

	private SeparatorControl separatorControl2;

	private CheckBox CKUnit3;

	private CheckBox CKUnit2;

	private CheckBox CKUnit1;

	private SimpleButton BtnUnit3Qty;

	private SimpleButton BtnUnit3;

	private SimpleButton BtnUnit2Qty;

	private SimpleButton BtnUnit2;

	private SimpleButton BtnUnit1Qty;

	private SimpleButton BtnUnit1;

	private SimpleButton BtnUnit3Price;

	private SimpleButton BtnUnit2Price;

	public FrmImportExcel()
	{
		InitializeComponent();
		FullDropDown();
		SetInitialRow();
	}

	public static DataTable GetDataTableExcel(string strFileName, string sheet)
	{
		string selectCommandText = "SELECT * FROM [" + sheet + "$]";
		DataSet dataSet = new DataSet();
		try
		{
			string selectConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=Excel 12.0;";
			using OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, selectConnectionString);
			oleDbDataAdapter.Fill(dataSet);
		}
		catch
		{
			string selectConnectionString2 = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";";
			using OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter(selectCommandText, selectConnectionString2);
			oleDbDataAdapter2.Fill(dataSet);
		}
		return dataSet.Tables[0];
	}

	private void BtnLoad_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(TxtPathFile.Text))
		{
			if (!string.IsNullOrWhiteSpace(comboBox1.Text))
			{
				GetDataTableExcel(TxtPathFile.Text, comboBox1.Text);
				dataGridView1.DataSource = GetDataTableExcel(TxtPathFile.Text, comboBox1.Text);
				int num = 0;
				num = ((db.Item_Items.Count() > 0) ? db.Item_Items.Max((Item_Items s) => s.ItemId) : 0);
				num++;
				for (int num2 = 0; num2 < dataGridView1.Rows.Count - 1; num2++)
				{
					dataGridView1.Rows[num2].Cells[0].Value = num;
					num++;
				}
			}
			else
			{
				MessageBox.Show("إدخل إسم الشيت");
			}
		}
		else
		{
			MessageBox.Show("إدخل إمتداد الملف");
			TxtPathFile_ButtonClick(null, null);
		}
	}

	private void TxtPathFile_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		string text = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + TxtPathFile.Text + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";";
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.InitialDirectory = "c:\\";
		openFileDialog.Filter = "Excel files (*.xls)|*.xls|Excel files (*.xlsx)|*.xlsx";
		openFileDialog.FilterIndex = 2;
		openFileDialog.RestoreDirectory = true;
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		TxtPathFile.Text = openFileDialog.FileName;
		foreach (string excelSheetName in GetExcelSheetNames(TxtPathFile.Text))
		{
			comboBox1.Items.Add(excelSheetName);
		}
		comboBox1.SelectedIndex = 0;
	}

	public static IEnumerable<string> GetExcelSheetNames(string excelFile)
	{
		Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			Excel.Workbook workbook = application.Workbooks.Open(excelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
		string[] array = new string[workbook.Worksheets.Count];
		int num = 0;
			foreach (Excel.Worksheet worksheet in workbook.Worksheets)
		{
			array[num] = worksheet.Name;
			num++;
		}
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return array;
	}

	private void BtnNew_Click(object sender, EventArgs e)
	{
		dataGridView1.DataSource = null;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnDownload_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "Excel files (*.xls)|*.xls|Excel files (*.xlsx)|*.xlsx";
		saveFileDialog.Title = "Download Excel File";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.FileName = "items.xlsx";
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			string sourceFileName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Download\\items.xlsx";
			string fileName = saveFileDialog.FileName;
			File.Copy(sourceFileName, fileName, overwrite: true);
			MessageBox.Show("تم التحميل");
		}
	}

	private void FullDropDown()
	{
		CbItemtype.DataSource = Items.GetItemType();
		CbItemtype.DisplayMember = "TypeName";
		CbItemtype.ValueMember = "SN";
		CBCat.DataSource = Items.GetCats();
		CBCat.DisplayMember = "Name";
		CBCat.ValueMember = "ID";
		CBClass.DataSource = Items.GetItemCalass();
		CBClass.DisplayMember = "Name";
		CBClass.ValueMember = "ID";
		CBCompany.DataSource = Items.GetItemCompany();
		CBCompany.DisplayMember = "Name";
		CBCompany.ValueMember = "ID";
		CBGuarantee.DataSource = Items.GetGuarantee();
		CBGuarantee.DisplayMember = "Name";
		CBGuarantee.ValueMember = "ID";
		CBCountry.DataSource = Items.GetCountry();
		CBCountry.DisplayMember = "Name";
		CBCountry.ValueMember = "ID";
		CbUnit1.DataSource = Items.GetUnits();
		CbUnit1.DisplayMember = "Name";
		CbUnit1.ValueMember = "ID";
		CbUnit2.DataSource = Items.GetUnits().ToList();
		CbUnit2.DisplayMember = "Name";
		CbUnit2.ValueMember = "ID";
		CbUnit3.DataSource = Items.GetUnits().ToList();
		CbUnit3.DisplayMember = "Name";
		CbUnit3.ValueMember = "ID";
		NVat.Value = LoginDetails.PerVat;
	}

	private void CbItemtype_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[5].Value = CbItemtype.SelectedValue;
		}
	}

	private void CBCat_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[6].Value = CBCat.SelectedValue;
		}
	}

	private void CBClass_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[7].Value = CBClass.SelectedValue;
		}
	}

	private void CBCountry_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[10].Value = CBCountry.SelectedValue;
		}
	}

	private void CBCompany_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[8].Value = CBCompany.SelectedValue;
		}
	}

	private void CBGuarantee_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			dataGridView1.Rows[i].Cells[9].Value = CBGuarantee.SelectedValue;
		}
	}

	private void BtnVat_Click(object sender, EventArgs e)
	{
		try
		{
			if (BtnVat.Text == "تفعيل الضربية")
			{
				BtnVat.Text = "إلغاء الضربية";
				for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
				{
					dataGridView1.Rows[i].Cells[53].Value = true;
					dataGridView1.Rows[i].Cells[54].Value = NVat.Value;
				}
			}
			else
			{
				BtnVat.Text = "تفعيل الضربية";
				for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
				{
					dataGridView1.Rows[j].Cells[53].Value = false;
					dataGridView1.Rows[j].Cells[54].Value = 0;
				}
			}
		}
		catch
		{
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("يرجى التحدث للدعم الفنى لمراجعة البيانات", "تنبية", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
		{
			FrmPassExcel frmPassExcel = new FrmPassExcel();
			frmPassExcel.ShowDialog();
			if (!frmPassExcel.ISOK)
			{
				return;
			}
			try
			{
				for (int i = 0; i < dataGridView1.Rows.Count && dataGridView1.Rows[i].Cells[0].Value != null; i++)
				{
					string value = dataGridView1.Rows[i].Cells[0].Value.ToString();
					string text = dataGridView1.Rows[i].Cells[1].Value.ToString();
					string text2 = dataGridView1.Rows[i].Cells[2].Value.ToString();
					string text3 = dataGridView1.Rows[i].Cells[3].Value.ToString();
					string text4 = dataGridView1.Rows[i].Cells[4].Value.ToString();
					string value2 = dataGridView1.Rows[i].Cells[5].Value.ToString();
					string value3 = dataGridView1.Rows[i].Cells[6].Value.ToString();
					string value4 = dataGridView1.Rows[i].Cells[7].Value.ToString();
					string value5 = dataGridView1.Rows[i].Cells[8].Value.ToString();
					string value6 = dataGridView1.Rows[i].Cells[9].Value.ToString();
					string value7 = dataGridView1.Rows[i].Cells[10].Value.ToString();
					string value8 = dataGridView1.Rows[i].Cells[11].Value.ToString();
					string value9 = dataGridView1.Rows[i].Cells[12].Value.ToString();
					decimal num = 0m;
					if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[13].Value = 0;
					}
					num = Convert.ToDecimal(dataGridView1.Rows[i].Cells[13].Value);
					string text5 = dataGridView1.Rows[i].Cells[14].Value.ToString();
					string text6 = dataGridView1.Rows[i].Cells[15].Value.ToString();
					string text7 = dataGridView1.Rows[i].Cells[16].Value.ToString();
					string text8 = dataGridView1.Rows[i].Cells[17].Value.ToString();
					decimal num2 = 0m;
					if (dataGridView1.Rows[i].Cells[18].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[18].Value = 0;
					}
					num2 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[18].Value);
					decimal num3 = 0m;
					if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[19].Value = 0;
					}
					num3 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[19].Value);
					decimal num4 = 0m;
					if (dataGridView1.Rows[i].Cells[20].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[20].Value = 0;
					}
					num4 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[20].Value);
					decimal num5 = 0m;
					if (dataGridView1.Rows[i].Cells[21].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[21].Value = 0;
					}
					num5 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[21].Value);
					decimal num6 = 0m;
					if (dataGridView1.Rows[i].Cells[22].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[22].Value = 0;
					}
					num6 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[22].Value);
					decimal num7 = 0m;
					if (dataGridView1.Rows[i].Cells[23].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[23].Value = 0;
					}
					num7 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[23].Value);
					decimal num8 = 0m;
					if (dataGridView1.Rows[i].Cells[24].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[24].Value = 0;
					}
					num8 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[24].Value);
					decimal num9 = 0m;
					if (dataGridView1.Rows[i].Cells[25].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[25].Value = 0;
					}
					num9 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[25].Value);
					string text9 = dataGridView1.Rows[i].Cells[26].Value.ToString();
					string text10 = dataGridView1.Rows[i].Cells[27].Value.ToString();
					string text11 = dataGridView1.Rows[i].Cells[28].Value.ToString();
					string text12 = dataGridView1.Rows[i].Cells[29].Value.ToString();
					decimal num10 = 0m;
					if (dataGridView1.Rows[i].Cells[30].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[30].Value = 0;
					}
					num10 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[30].Value);
					decimal num11 = 0m;
					if (dataGridView1.Rows[i].Cells[31].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[31].Value = 0;
					}
					num11 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[31].Value);
					decimal num12 = 0m;
					if (dataGridView1.Rows[i].Cells[32].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[32].Value = 0;
					}
					num12 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[32].Value);
					decimal num13 = 0m;
					if (dataGridView1.Rows[i].Cells[33].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[33].Value = 0;
					}
					num13 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[33].Value);
					decimal num14 = 0m;
					if (dataGridView1.Rows[i].Cells[34].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[34].Value = 0;
					}
					num14 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[34].Value);
					decimal num15 = 0m;
					if (dataGridView1.Rows[i].Cells[35].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[35].Value = 0;
					}
					num15 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[35].Value);
					decimal num16 = 0m;
					if (dataGridView1.Rows[i].Cells[36].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[36].Value = 0;
					}
					num16 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[36].Value);
					decimal num17 = 0m;
					if (dataGridView1.Rows[i].Cells[37].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[37].Value = 0;
					}
					num17 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[37].Value);
					string text13 = dataGridView1.Rows[i].Cells[38].Value.ToString();
					string text14 = dataGridView1.Rows[i].Cells[39].Value.ToString();
					string text15 = dataGridView1.Rows[i].Cells[40].Value.ToString();
					string text16 = dataGridView1.Rows[i].Cells[41].Value.ToString();
					decimal num18 = 0m;
					if (dataGridView1.Rows[i].Cells[42].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[42].Value = 0;
					}
					num18 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[42].Value);
					decimal num19 = 0m;
					if (dataGridView1.Rows[i].Cells[43].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[43].Value = 0;
					}
					num19 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[43].Value);
					decimal num20 = 0m;
					if (dataGridView1.Rows[i].Cells[44].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[44].Value = 0;
					}
					num20 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[44].Value);
					decimal num21 = 0m;
					if (dataGridView1.Rows[i].Cells[45].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[45].Value = 0;
					}
					num21 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[45].Value);
					decimal num22 = 0m;
					if (dataGridView1.Rows[i].Cells[46].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[46].Value = 0;
					}
					num22 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[46].Value);
					decimal num23 = 0m;
					if (dataGridView1.Rows[i].Cells[47].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[47].Value = 0;
					}
					num23 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[47].Value);
					decimal num24 = 0m;
					if (dataGridView1.Rows[i].Cells[48].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[48].Value = 0;
					}
					num24 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[48].Value);
					decimal num25 = 0m;
					if (dataGridView1.Rows[i].Cells[49].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[49].Value = 0;
					}
					num25 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[49].Value);
					decimal num26 = 0m;
					if (dataGridView1.Rows[i].Cells[50].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[50].Value = 0;
					}
					num26 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[50].Value);
					decimal num27 = 0m;
					if (dataGridView1.Rows[i].Cells[51].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[51].Value = 0;
					}
					num27 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[51].Value);
					decimal num28 = 0m;
					if (dataGridView1.Rows[i].Cells[52].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[52].Value = 0;
					}
					num28 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[52].Value);
					bool flag = false;
					if (dataGridView1.Rows[i].Cells[53].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[53].Value = false;
					}
					flag = Convert.ToBoolean(dataGridView1.Rows[i].Cells[53].Value);
					decimal num29 = 0m;
					if (dataGridView1.Rows[i].Cells[54].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[54].Value = 0;
					}
					num29 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[54].Value);
					decimal num30 = 0m;
					if (dataGridView1.Rows[i].Cells[55].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[55].Value = 0;
					}
					num30 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[55].Value);
					string text17 = dataGridView1.Rows[i].Cells[56].Value.ToString();
					int num31 = 0;
					if (dataGridView1.Rows[i].Cells[57].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[57].Value = LoginDetails.UserID;
					}
					num31 = Convert.ToInt32(dataGridView1.Rows[i].Cells[57].Value.ToString());
					int num32 = 0;
					if (dataGridView1.Rows[i].Cells[58].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[58].Value = LoginDetails.BranchID;
					}
					num32 = Convert.ToInt32(dataGridView1.Rows[i].Cells[58].Value.ToString());
					string text18 = dataGridView1.Rows[i].Cells[59].Value.ToString();
					if (dataGridView1.Rows[i].Cells[60].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[60].Value = DateTime.Now;
					}
					object value10 = (dataGridView1.Rows[i].Cells[60].Value = DateTime.Now);
					DateTime dateTime = Convert.ToDateTime(value10);
					int num33 = 0;
					if (dataGridView1.Rows[i].Cells[61].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[61].Value = LoginDetails.UserID;
					}
					num33 = Convert.ToInt32(dataGridView1.Rows[i].Cells[61].Value.ToString());
					int num34 = 0;
					if (dataGridView1.Rows[i].Cells[62].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[62].Value = LoginDetails.BranchID;
					}
					num34 = Convert.ToInt32(dataGridView1.Rows[i].Cells[62].Value.ToString());
					string text19 = dataGridView1.Rows[i].Cells[63].Value.ToString();
					if (dataGridView1.Rows[i].Cells[64].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[64].Value = DateTime.Now;
					}
					value10 = (dataGridView1.Rows[i].Cells[64].Value = DateTime.Now);
					DateTime dateTime2 = Convert.ToDateTime(value10);
					int num35 = 0;
					if (dataGridView1.Rows[i].Cells[65].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[65].Value = 1;
					}
					num35 = Convert.ToInt32(dataGridView1.Rows[i].Cells[65].Value);
					decimal num36 = 0m;
					if (dataGridView1.Rows[i].Cells[66].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[66].Value = 0;
					}
					num36 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[66].Value);
					decimal num37 = 0m;
					if (dataGridView1.Rows[i].Cells[67].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[67].Value = 0;
					}
					num37 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[67].Value);
					decimal num38 = 0m;
					if (dataGridView1.Rows[i].Cells[68].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[68].Value = 0;
					}
					num38 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[68].Value);
					int num39 = 0;
					if (dataGridView1.Rows[i].Cells[69].Value.ToString() == "")
					{
						dataGridView1.Rows[i].Cells[69].Value = 0;
					}
					num39 = Convert.ToInt32(dataGridView1.Rows[i].Cells[69].Value);
					int num40 = 0;
					num40 = (flag ? 1 : 0);
					datatable.Rows.Add(Convert.ToInt32(value), text, text2, text3, text4, Convert.ToInt32(value2), Convert.ToInt32(value3), Convert.ToInt32(value4), Convert.ToInt32(value5), Convert.ToInt32(value6), Convert.ToInt32(value7), Convert.ToInt32(value8), Convert.ToInt32(value9), Convert.ToInt32(num), text5, text6, text7, text8, num2, num3, num4, num5, num6, num7, num8, num9, text9, text10, text11, text12, num10, num11, num12, num13, num14, num15, num16, num17, text13, text14, text15, text16, num18, num19, num20, num21, num22, num23, num24, num25, Convert.ToInt32(num26), num27, num28, num40, num29, num30, text17, num31, num32, text18, dateTime, num33, num34, text19, dateTime2, num35, num36, num37, num38, num39, "", "");
				}
			}
			catch (Exception)
			{
				MessageBox.Show("من المحتمل أن هذا الملف ليس هو ملف تحميل الأصناف");
			}
			items.SaveItems(datatable);
			dataGridView1.DataSource = null;
			datatable.Rows.Clear();
		}
		else
		{
			MessageBox.Show("نأسف لعدم إكتمال الطلب");
		}
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void TxtPathFile_EditValueChanged(object sender, EventArgs e)
	{
	}

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void label35_Click(object sender, EventArgs e)
	{
	}

	private void groupBox4_Enter(object sender, EventArgs e)
	{
	}

	private void label12_Click(object sender, EventArgs e)
	{
	}

	private void BtnCreateCode_Click(object sender, EventArgs e)
	{
		long num = 0L;
		num = ((db.Item_Items.Count() > 0) ? db.Item_Items.Max((Item_Items s) => s.ItemId) : 0);
		num++;
		for (int num2 = 0; num2 < dataGridView1.Rows.Count - 1; num2++)
		{
			string value = LoginDetails.BranchID.ToString() + DateTime.Now.AddYears(-2000).Year + DateTime.Now.Day + num;
			if (dataGridView1.Rows[num2].Cells[1].Value.ToString() == "")
			{
				dataGridView1.Rows[num2].Cells[1].Value = Convert.ToDouble(value);
			}
			num++;
		}
	}

	private void CbUnit1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void CKUnit2_Click(object sender, EventArgs e)
	{
	}

	private void CKUnit2_CheckedChanged(object sender, EventArgs e)
	{
		if (CKUnit2.Checked)
		{
			BtnUnit2.Enabled = true;
			BtnUnit2Qty.Enabled = true;
			BtnUnit2Price.Enabled = true;
			CbUnit2.Enabled = true;
			nConvertUnit2.Enabled = true;
			RBUnitTafqit2.Enabled = true;
			TxtPerUnit2Price1.Enabled = true;
			TxtPerUnit2Price2.Enabled = true;
			TxtPerUnit2Price3.Enabled = true;
		}
		else
		{
			BtnUnit2.Enabled = false;
			BtnUnit2Qty.Enabled = false;
			BtnUnit2Price.Enabled = false;
			CbUnit2.Enabled = false;
			nConvertUnit2.Enabled = false;
			RBUnitTafqit2.Enabled = false;
			TxtPerUnit2Price1.Enabled = false;
			TxtPerUnit2Price2.Enabled = false;
			TxtPerUnit2Price3.Enabled = false;
		}
	}

	private void CKUnit3_CheckedChanged(object sender, EventArgs e)
	{
		if (CKUnit3.Checked)
		{
			BtnUnit3.Enabled = true;
			BtnUnit3Qty.Enabled = true;
			BtnUnit3Price.Enabled = true;
			CbUnit3.Enabled = true;
			nConvertUnit3.Enabled = true;
			RBUnitTafqit3.Enabled = true;
			TxtPerUnit3Price1.Enabled = true;
			TxtPerUnit3Price2.Enabled = true;
			TxtPerUnit3Price3.Enabled = true;
		}
		else
		{
			BtnUnit3.Enabled = false;
			BtnUnit3Qty.Enabled = false;
			BtnUnit3Price.Enabled = false;
			CbUnit3.Enabled = false;
			nConvertUnit3.Enabled = false;
			RBUnitTafqit3.Enabled = false;
			TxtPerUnit3Price1.Enabled = false;
			TxtPerUnit3Price2.Enabled = false;
			TxtPerUnit3Price3.Enabled = false;
		}
	}

	private void CKUnit1_CheckedChanged(object sender, EventArgs e)
	{
		if (CKUnit1.Checked)
		{
			BtnUnit1.Enabled = true;
			BtnUnit1Qty.Enabled = true;
			BtnUnit1Price.Enabled = true;
			CbUnit1.Enabled = true;
			numericUpDown1.Enabled = true;
			RBUnitTafqit1.Enabled = true;
			TxtPerUnit1Price1.Enabled = true;
			TxtPerUnit1Price2.Enabled = true;
			TxtPerUnit1Price3.Enabled = true;
		}
		else
		{
			BtnUnit1.Enabled = false;
			BtnUnit1Qty.Enabled = false;
			BtnUnit1Price.Enabled = false;
			CbUnit1.Enabled = false;
			numericUpDown1.Enabled = false;
			RBUnitTafqit1.Enabled = false;
			TxtPerUnit1Price1.Enabled = false;
			TxtPerUnit1Price2.Enabled = false;
			TxtPerUnit1Price3.Enabled = false;
		}
	}

	private void BtnEditGrid_Click(object sender, EventArgs e)
	{
	}

	private void BtnUnit1_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit1.Checked)
				{
					dataGridView1.Rows[i].Cells[11].Value = CbUnit1.SelectedValue;
					if (RBUnitTafqit1.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit1Qty_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit2.Checked)
				{
					dataGridView1.Rows[i].Cells[36].Value = numericUpDown1.Value;
					if (RBUnitTafqit3.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit2_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit2.Checked)
				{
					dataGridView1.Rows[i].Cells[12].Value = CbUnit2.SelectedValue;
					if (RBUnitTafqit2.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit3_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit3.Checked)
				{
					dataGridView1.Rows[i].Cells[13].Value = CbUnit3.SelectedValue;
					if (RBUnitTafqit3.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit3Qty_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit3.Checked)
				{
					dataGridView1.Rows[i].Cells[49].Value = nConvertUnit3.Value;
					if (RBUnitTafqit3.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit2Qty_Click(object sender, EventArgs e)
	{
		try
		{
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (CKUnit2.Checked)
				{
					dataGridView1.Rows[i].Cells[37].Value = nConvertUnit2.Value;
					if (RBUnitTafqit3.Checked)
					{
						dataGridView1.Rows[i].Cells[65].Value = true;
					}
					else
					{
						dataGridView1.Rows[i].Cells[65].Value = false;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void BtnUnit1Price_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
		{
			if (CKUnit1.Checked)
			{
			}
		}
	}

	public void SetInitialRow()
	{
		datatable.Columns.Add("ItemId", typeof(int));
		datatable.Columns.Add("Item_code", typeof(string));
		datatable.Columns.Add("item_Name", typeof(string));
		datatable.Columns.Add("item_Name_English", typeof(string));
		datatable.Columns.Add("Note", typeof(string));
		datatable.Columns.Add("item_Type", typeof(int));
		datatable.Columns.Add("Category_ID", typeof(int));
		datatable.Columns.Add("Class_ID", typeof(int));
		datatable.Columns.Add("CompanyID", typeof(int));
		datatable.Columns.Add("GuaranteeID", typeof(int));
		datatable.Columns.Add("CountryID", typeof(int));
		datatable.Columns.Add("UnitSmall", typeof(int));
		datatable.Columns.Add("UnitMedium", typeof(int));
		datatable.Columns.Add("UnitLarge", typeof(int));
		datatable.Columns.Add("SmallBarCode1", typeof(string));
		datatable.Columns.Add("SmallBarCode2", typeof(string));
		datatable.Columns.Add("SmallBarCode3", typeof(string));
		datatable.Columns.Add("LocationSmall", typeof(string));
		datatable.Columns.Add("SellPriceSmall", typeof(decimal));
		datatable.Columns.Add("SellPriceSmall2", typeof(decimal));
		datatable.Columns.Add("SellPriceSmall3", typeof(decimal));
		datatable.Columns.Add("LastCost", typeof(decimal));
		datatable.Columns.Add("Average_cost", typeof(decimal));
		datatable.Columns.Add("PriceExportunit1", typeof(decimal));
		datatable.Columns.Add("PriceMowazaunit1", typeof(decimal));
		datatable.Columns.Add("SmallUnitQuantity", typeof(decimal));
		datatable.Columns.Add("MediumBarCode1", typeof(string));
		datatable.Columns.Add("MediumBarCode2", typeof(string));
		datatable.Columns.Add("MediumBarCode3", typeof(string));
		datatable.Columns.Add("LocationMedium", typeof(string));
		datatable.Columns.Add("ConvertMediumUnit", typeof(decimal));
		datatable.Columns.Add("SellPriceMedium", typeof(decimal));
		datatable.Columns.Add("SellPriceMedium2", typeof(decimal));
		datatable.Columns.Add("SellPriceMedium3", typeof(decimal));
		datatable.Columns.Add("LastCost2", typeof(decimal));
		datatable.Columns.Add("PriceExportunit2", typeof(decimal));
		datatable.Columns.Add("PriceMowazaunit2", typeof(decimal));
		datatable.Columns.Add("SmallUnitQuantity2", typeof(decimal));
		datatable.Columns.Add("BigBarCode1", typeof(string));
		datatable.Columns.Add("BigBarCode2", typeof(string));
		datatable.Columns.Add("BigBarCode3", typeof(string));
		datatable.Columns.Add("LocationLarge", typeof(string));
		datatable.Columns.Add("ConvertBigUnit", typeof(decimal));
		datatable.Columns.Add("SellpriceLarge", typeof(decimal));
		datatable.Columns.Add("SellpriceLarge2", typeof(decimal));
		datatable.Columns.Add("SellpriceLarge3", typeof(decimal));
		datatable.Columns.Add("LastCost3", typeof(decimal));
		datatable.Columns.Add("PriceExportunit3", typeof(decimal));
		datatable.Columns.Add("PriceMowazaunit3", typeof(decimal));
		datatable.Columns.Add("SmallUnitQuantity3", typeof(decimal));
		datatable.Columns.Add("IsOffer", typeof(int));
		datatable.Columns.Add("NumOffer", typeof(decimal));
		datatable.Columns.Add("PriceOffer", typeof(decimal));
		datatable.Columns.Add("Is_Tax", typeof(int));
		datatable.Columns.Add("Tax_Value", typeof(decimal));
		datatable.Columns.Add("Is_Item_mizan", typeof(int));
		datatable.Columns.Add("Image", typeof(string));
		datatable.Columns.Add("UserID_Add", typeof(int));
		datatable.Columns.Add("UserBranch_Add", typeof(int));
		datatable.Columns.Add("UserMacAddress_Add", typeof(string));
		datatable.Columns.Add("UserDate_Add", typeof(DateTime));
		datatable.Columns.Add("UserID_Update", typeof(int));
		datatable.Columns.Add("UserBranch_Update", typeof(int));
		datatable.Columns.Add("UserMacAddress_Update", typeof(string));
		datatable.Columns.Add("UserDate_Update", typeof(DateTime));
		datatable.Columns.Add("UnitTafqitID", typeof(int));
		datatable.Columns.Add("DiscItemPer", typeof(decimal));
		datatable.Columns.Add("PharmPer", typeof(decimal));
		datatable.Columns.Add("DoctorPer", typeof(decimal));
		datatable.Columns.Add("PrinterID", typeof(int));
		datatable.Columns.Add("Code_Company", typeof(string));
		datatable.Columns.Add("BarcodeCountry", typeof(string));
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Items.FrmImportExcel));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.TxtPathFile = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnLoad = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDownload = new DevExpress.XtraEditors.SimpleButton();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.CBGuarantee = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label58 = new System.Windows.Forms.Label();
		this.CBCountry = new System.Windows.Forms.ComboBox();
		this.label57 = new System.Windows.Forms.Label();
		this.BtnVat = new DevExpress.XtraEditors.SimpleButton();
		this.CbItemtype = new System.Windows.Forms.ComboBox();
		this.CBCompany = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.NVat = new System.Windows.Forms.NumericUpDown();
		this.CBClass = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.CBCat = new System.Windows.Forms.ComboBox();
		this.TapPrice = new DevExpress.XtraTab.XtraTabControl();
		this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
		this.BtnUnit3Qty = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit3 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit2Qty = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit2 = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit1Qty = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit1 = new DevExpress.XtraEditors.SimpleButton();
		this.CKUnit3 = new System.Windows.Forms.CheckBox();
		this.CKUnit2 = new System.Windows.Forms.CheckBox();
		this.CKUnit1 = new System.Windows.Forms.CheckBox();
		this.CbUnit2 = new System.Windows.Forms.ComboBox();
		this.CbUnit3 = new System.Windows.Forms.ComboBox();
		this.CbUnit1 = new System.Windows.Forms.ComboBox();
		this.nConvertUnit3 = new System.Windows.Forms.NumericUpDown();
		this.label28 = new System.Windows.Forms.Label();
		this.LblUnit3 = new System.Windows.Forms.Label();
		this.nConvertUnit2 = new System.Windows.Forms.NumericUpDown();
		this.label27 = new System.Windows.Forms.Label();
		this.LblUnit2 = new System.Windows.Forms.Label();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.LblUnit = new System.Windows.Forms.Label();
		this.LblUnit1 = new System.Windows.Forms.Label();
		this.RBUnitTafqit3 = new System.Windows.Forms.RadioButton();
		this.RBUnitTafqit2 = new System.Windows.Forms.RadioButton();
		this.RBUnitTafqit1 = new System.Windows.Forms.RadioButton();
		this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
		this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
		this.label31 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
		this.label10 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.TxtPerUnit3Price3 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price3 = new System.Windows.Forms.NumericUpDown();
		this.BtnCreateCode = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit3Price = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit2Price = new DevExpress.XtraEditors.SimpleButton();
		this.BtnUnit1Price = new DevExpress.XtraEditors.SimpleButton();
		this.TxtPerUnit1Price3 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit3Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit1Price2 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit3Price1 = new System.Windows.Forms.NumericUpDown();
		this.TxtPerUnit2Price1 = new System.Windows.Forms.NumericUpDown();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtPerUnit1Price1 = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
		this.label32 = new System.Windows.Forms.Label();
		this.separatorControl4 = new DevExpress.XtraEditors.SeparatorControl();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPathFile.Properties).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.NVat).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TapPrice).BeginInit();
		this.TapPrice.SuspendLayout();
		this.xtraTabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).BeginInit();
		this.xtraTabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.comboBox1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.TxtPathFile);
		this.groupBox1.Location = new System.Drawing.Point(12, 11);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox1.Size = new System.Drawing.Size(425, 86);
		this.groupBox1.TabIndex = 45;
		this.groupBox1.TabStop = false;
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(6, 51);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(341, 24);
		this.comboBox1.TabIndex = 2;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(353, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(51, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Excel File";
		this.label2.Click += new System.EventHandler(label2_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(353, 56);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Sheet Name";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.TxtPathFile.Location = new System.Drawing.Point(6, 16);
		this.TxtPathFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPathFile.Name = "TxtPathFile";
		this.TxtPathFile.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TxtPathFile.Properties.Appearance.Options.UseFont = true;
		this.TxtPathFile.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtPathFile.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtPathFile.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtPathFile.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.TxtPathFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.TxtPathFile.Properties.ReadOnly = true;
		this.TxtPathFile.Size = new System.Drawing.Size(341, 26);
		this.TxtPathFile.TabIndex = 0;
		this.TxtPathFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtPathFile_ButtonClick);
		this.TxtPathFile.EditValueChanged += new System.EventHandler(TxtPathFile_EditValueChanged);
		this.BtnExit.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnExit.Appearance.Options.UseForeColor = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(12, 14);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(145, 32);
		this.BtnExit.TabIndex = 4;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnLoad.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnLoad.ImageOptions.Image");
		this.BtnLoad.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnLoad.Location = new System.Drawing.Point(161, 14);
		this.BtnLoad.Name = "BtnLoad";
		this.BtnLoad.Size = new System.Drawing.Size(119, 32);
		this.BtnLoad.TabIndex = 0;
		this.BtnLoad.Text = "تحميل الأصناف";
		this.BtnLoad.Click += new System.EventHandler(BtnLoad_Click);
		this.BtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnNew.Location = new System.Drawing.Point(285, 15);
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Size = new System.Drawing.Size(130, 67);
		this.BtnNew.TabIndex = 3;
		this.BtnNew.Text = "جديد";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.groupBox2.Controls.Add(this.BtnLoad);
		this.groupBox2.Controls.Add(this.BtnSave);
		this.groupBox2.Controls.Add(this.BtnDownload);
		this.groupBox2.Controls.Add(this.BtnNew);
		this.groupBox2.Controls.Add(this.BtnExit);
		this.groupBox2.Location = new System.Drawing.Point(12, 101);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox2.Size = new System.Drawing.Size(425, 111);
		this.groupBox2.TabIndex = 45;
		this.groupBox2.TabStop = false;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.Appearance.Options.UseForeColor = true;
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(12, 48);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(145, 34);
		this.BtnSave.TabIndex = 3;
		this.BtnSave.Text = "حفظ الأصناف فى البرنامج";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnDownload.Appearance.ForeColor = System.Drawing.Color.Green;
		this.BtnDownload.Appearance.Options.UseForeColor = true;
		this.BtnDownload.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDownload.ImageOptions.Image");
		this.BtnDownload.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnDownload.Location = new System.Drawing.Point(161, 50);
		this.BtnDownload.Name = "BtnDownload";
		this.BtnDownload.Size = new System.Drawing.Size(119, 32);
		this.BtnDownload.TabIndex = 3;
		this.BtnDownload.Text = "تحميل ملف الإكسل";
		this.BtnDownload.Click += new System.EventHandler(BtnDownload_Click);
		this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(12, 218);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.Size = new System.Drawing.Size(1145, 504);
		this.dataGridView1.TabIndex = 46;
		this.groupBox3.Controls.Add(this.CBGuarantee);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.label58);
		this.groupBox3.Controls.Add(this.CBCountry);
		this.groupBox3.Controls.Add(this.label57);
		this.groupBox3.Controls.Add(this.BtnVat);
		this.groupBox3.Controls.Add(this.CbItemtype);
		this.groupBox3.Controls.Add(this.CBCompany);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.NVat);
		this.groupBox3.Controls.Add(this.CBClass);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.CBCat);
		this.groupBox3.Location = new System.Drawing.Point(443, 11);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.groupBox3.Size = new System.Drawing.Size(285, 202);
		this.groupBox3.TabIndex = 45;
		this.groupBox3.TabStop = false;
		this.CBGuarantee.FormattingEnabled = true;
		this.CBGuarantee.Location = new System.Drawing.Point(6, 138);
		this.CBGuarantee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CBGuarantee.Name = "CBGuarantee";
		this.CBGuarantee.Size = new System.Drawing.Size(196, 21);
		this.CBGuarantee.TabIndex = 128;
		this.CBGuarantee.SelectedIndexChanged += new System.EventHandler(CBGuarantee_SelectedIndexChanged);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(208, 168);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 16);
		this.label5.TabIndex = 134;
		this.label5.Text = "الضريبة";
		this.label58.AutoSize = true;
		this.label58.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label58.Location = new System.Drawing.Point(208, 140);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(46, 16);
		this.label58.TabIndex = 134;
		this.label58.Text = "الضمان";
		this.CBCountry.FormattingEnabled = true;
		this.CBCountry.Location = new System.Drawing.Point(6, 90);
		this.CBCountry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CBCountry.Name = "CBCountry";
		this.CBCountry.Size = new System.Drawing.Size(196, 21);
		this.CBCountry.TabIndex = 125;
		this.CBCountry.SelectedIndexChanged += new System.EventHandler(CBCountry_SelectedIndexChanged);
		this.label57.AutoSize = true;
		this.label57.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label57.Location = new System.Drawing.Point(208, 92);
		this.label57.Name = "label57";
		this.label57.Size = new System.Drawing.Size(66, 16);
		this.label57.TabIndex = 133;
		this.label57.Text = "بلد المنشأ";
		this.BtnVat.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnVat.Appearance.Options.UseForeColor = true;
		this.BtnVat.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnVat.ImageOptions.Image");
		this.BtnVat.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnVat.Location = new System.Drawing.Point(6, 165);
		this.BtnVat.Name = "BtnVat";
		this.BtnVat.Size = new System.Drawing.Size(120, 22);
		this.BtnVat.TabIndex = 4;
		this.BtnVat.Text = "تفعيل الضربية";
		this.BtnVat.Click += new System.EventHandler(BtnVat_Click);
		this.CbItemtype.FormattingEnabled = true;
		this.CbItemtype.Location = new System.Drawing.Point(6, 16);
		this.CbItemtype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CbItemtype.Name = "CbItemtype";
		this.CbItemtype.Size = new System.Drawing.Size(196, 21);
		this.CbItemtype.TabIndex = 120;
		this.CbItemtype.SelectedIndexChanged += new System.EventHandler(CbItemtype_SelectedIndexChanged);
		this.CBCompany.FormattingEnabled = true;
		this.CBCompany.Location = new System.Drawing.Point(6, 114);
		this.CBCompany.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CBCompany.Name = "CBCompany";
		this.CBCompany.Size = new System.Drawing.Size(196, 21);
		this.CBCompany.TabIndex = 126;
		this.CBCompany.SelectedIndexChanged += new System.EventHandler(CBCompany_SelectedIndexChanged);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label3.Location = new System.Drawing.Point(208, 18);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 16);
		this.label3.TabIndex = 121;
		this.label3.Text = "نوع الصنف";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label9.Location = new System.Drawing.Point(208, 116);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(55, 16);
		this.label9.TabIndex = 130;
		this.label9.Text = "الشركات";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(208, 67);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(63, 16);
		this.label6.TabIndex = 127;
		this.label6.Text = "فئة الصنف";
		this.NVat.DecimalPlaces = 2;
		this.NVat.Font = new System.Drawing.Font("Tahoma", 9f);
		this.NVat.Location = new System.Drawing.Point(132, 165);
		this.NVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.NVat.Maximum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.NVat.Name = "NVat";
		this.NVat.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.NVat.Size = new System.Drawing.Size(70, 22);
		this.NVat.TabIndex = 129;
		this.NVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.CBClass.FormattingEnabled = true;
		this.CBClass.Location = new System.Drawing.Point(6, 65);
		this.CBClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CBClass.Name = "CBClass";
		this.CBClass.Size = new System.Drawing.Size(196, 21);
		this.CBClass.TabIndex = 124;
		this.CBClass.SelectedIndexChanged += new System.EventHandler(CBClass_SelectedIndexChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label4.Location = new System.Drawing.Point(208, 43);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 16);
		this.label4.TabIndex = 122;
		this.label4.Text = "المجموعات";
		this.CBCat.FormattingEnabled = true;
		this.CBCat.Location = new System.Drawing.Point(6, 41);
		this.CBCat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CBCat.Name = "CBCat";
		this.CBCat.Size = new System.Drawing.Size(196, 21);
		this.CBCat.TabIndex = 123;
		this.CBCat.SelectedIndexChanged += new System.EventHandler(CBCat_SelectedIndexChanged);
		this.TapPrice.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TapPrice.Location = new System.Drawing.Point(734, 16);
		this.TapPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TapPrice.MultiLine = DevExpress.Utils.DefaultBoolean.False;
		this.TapPrice.Name = "TapPrice";
		this.TapPrice.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Center;
		this.TapPrice.SelectedTabPage = this.xtraTabPage1;
		this.TapPrice.Size = new System.Drawing.Size(423, 196);
		this.TapPrice.TabIndex = 47;
		this.TapPrice.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[2] { this.xtraTabPage1, this.xtraTabPage2 });
		this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9f);
		this.xtraTabPage1.Appearance.Header.Image = (System.Drawing.Image)resources.GetObject("xtraTabPage1.Appearance.Header.Image");
		this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage1.Appearance.Header.Options.UseImage = true;
		this.xtraTabPage1.Controls.Add(this.BtnUnit3Qty);
		this.xtraTabPage1.Controls.Add(this.BtnUnit3);
		this.xtraTabPage1.Controls.Add(this.BtnUnit2Qty);
		this.xtraTabPage1.Controls.Add(this.BtnUnit2);
		this.xtraTabPage1.Controls.Add(this.BtnUnit1Qty);
		this.xtraTabPage1.Controls.Add(this.BtnUnit1);
		this.xtraTabPage1.Controls.Add(this.CKUnit3);
		this.xtraTabPage1.Controls.Add(this.CKUnit2);
		this.xtraTabPage1.Controls.Add(this.CKUnit1);
		this.xtraTabPage1.Controls.Add(this.CbUnit2);
		this.xtraTabPage1.Controls.Add(this.CbUnit3);
		this.xtraTabPage1.Controls.Add(this.CbUnit1);
		this.xtraTabPage1.Controls.Add(this.nConvertUnit3);
		this.xtraTabPage1.Controls.Add(this.label28);
		this.xtraTabPage1.Controls.Add(this.LblUnit3);
		this.xtraTabPage1.Controls.Add(this.nConvertUnit2);
		this.xtraTabPage1.Controls.Add(this.label27);
		this.xtraTabPage1.Controls.Add(this.LblUnit2);
		this.xtraTabPage1.Controls.Add(this.numericUpDown1);
		this.xtraTabPage1.Controls.Add(this.LblUnit);
		this.xtraTabPage1.Controls.Add(this.LblUnit1);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit3);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit2);
		this.xtraTabPage1.Controls.Add(this.RBUnitTafqit1);
		this.xtraTabPage1.Controls.Add(this.separatorControl1);
		this.xtraTabPage1.Controls.Add(this.separatorControl3);
		this.xtraTabPage1.Controls.Add(this.label31);
		this.xtraTabPage1.Controls.Add(this.label30);
		this.xtraTabPage1.Controls.Add(this.label36);
		this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.xtraTabPage1.Name = "xtraTabPage1";
		this.xtraTabPage1.Size = new System.Drawing.Size(421, 170);
		this.xtraTabPage1.Text = "الوحدات";
		this.BtnUnit3Qty.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit3Qty.Appearance.Options.UseForeColor = true;
		this.BtnUnit3Qty.Enabled = false;
		this.BtnUnit3Qty.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit3Qty.ImageOptions.Image");
		this.BtnUnit3Qty.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit3Qty.Location = new System.Drawing.Point(117, 141);
		this.BtnUnit3Qty.Name = "BtnUnit3Qty";
		this.BtnUnit3Qty.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit3Qty.TabIndex = 173;
		this.BtnUnit3Qty.Click += new System.EventHandler(BtnUnit3Qty_Click);
		this.BtnUnit3.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit3.Appearance.Options.UseForeColor = true;
		this.BtnUnit3.Enabled = false;
		this.BtnUnit3.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit3.ImageOptions.Image");
		this.BtnUnit3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit3.Location = new System.Drawing.Point(117, 118);
		this.BtnUnit3.Name = "BtnUnit3";
		this.BtnUnit3.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit3.TabIndex = 173;
		this.BtnUnit3.Click += new System.EventHandler(BtnUnit3_Click);
		this.BtnUnit2Qty.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit2Qty.Appearance.Options.UseForeColor = true;
		this.BtnUnit2Qty.Enabled = false;
		this.BtnUnit2Qty.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit2Qty.ImageOptions.Image");
		this.BtnUnit2Qty.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit2Qty.Location = new System.Drawing.Point(117, 87);
		this.BtnUnit2Qty.Name = "BtnUnit2Qty";
		this.BtnUnit2Qty.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit2Qty.TabIndex = 173;
		this.BtnUnit2Qty.Click += new System.EventHandler(BtnUnit2Qty_Click);
		this.BtnUnit2.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit2.Appearance.Options.UseForeColor = true;
		this.BtnUnit2.Enabled = false;
		this.BtnUnit2.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit2.ImageOptions.Image");
		this.BtnUnit2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit2.Location = new System.Drawing.Point(117, 64);
		this.BtnUnit2.Name = "BtnUnit2";
		this.BtnUnit2.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit2.TabIndex = 173;
		this.BtnUnit2.Click += new System.EventHandler(BtnUnit2_Click);
		this.BtnUnit1Qty.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit1Qty.Appearance.Options.UseForeColor = true;
		this.BtnUnit1Qty.Enabled = false;
		this.BtnUnit1Qty.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit1Qty.ImageOptions.Image");
		this.BtnUnit1Qty.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit1Qty.Location = new System.Drawing.Point(117, 33);
		this.BtnUnit1Qty.Name = "BtnUnit1Qty";
		this.BtnUnit1Qty.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit1Qty.TabIndex = 173;
		this.BtnUnit1Qty.Click += new System.EventHandler(BtnUnit1Qty_Click);
		this.BtnUnit1.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit1.Appearance.Options.UseForeColor = true;
		this.BtnUnit1.Enabled = false;
		this.BtnUnit1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit1.ImageOptions.Image");
		this.BtnUnit1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit1.Location = new System.Drawing.Point(117, 10);
		this.BtnUnit1.Name = "BtnUnit1";
		this.BtnUnit1.Size = new System.Drawing.Size(29, 21);
		this.BtnUnit1.TabIndex = 173;
		this.BtnUnit1.Click += new System.EventHandler(BtnUnit1_Click);
		this.CKUnit3.AutoSize = true;
		this.CKUnit3.Location = new System.Drawing.Point(394, 122);
		this.CKUnit3.Name = "CKUnit3";
		this.CKUnit3.Size = new System.Drawing.Size(15, 14);
		this.CKUnit3.TabIndex = 172;
		this.CKUnit3.UseVisualStyleBackColor = true;
		this.CKUnit3.CheckedChanged += new System.EventHandler(CKUnit3_CheckedChanged);
		this.CKUnit2.AutoSize = true;
		this.CKUnit2.Location = new System.Drawing.Point(394, 67);
		this.CKUnit2.Name = "CKUnit2";
		this.CKUnit2.Size = new System.Drawing.Size(15, 14);
		this.CKUnit2.TabIndex = 172;
		this.CKUnit2.UseVisualStyleBackColor = true;
		this.CKUnit2.CheckedChanged += new System.EventHandler(CKUnit2_CheckedChanged);
		this.CKUnit2.Click += new System.EventHandler(CKUnit2_Click);
		this.CKUnit1.AutoSize = true;
		this.CKUnit1.Location = new System.Drawing.Point(394, 14);
		this.CKUnit1.Name = "CKUnit1";
		this.CKUnit1.Size = new System.Drawing.Size(15, 14);
		this.CKUnit1.TabIndex = 172;
		this.CKUnit1.UseVisualStyleBackColor = true;
		this.CKUnit1.CheckedChanged += new System.EventHandler(CKUnit1_CheckedChanged);
		this.CbUnit2.Enabled = false;
		this.CbUnit2.FormattingEnabled = true;
		this.CbUnit2.Location = new System.Drawing.Point(151, 64);
		this.CbUnit2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CbUnit2.Name = "CbUnit2";
		this.CbUnit2.Size = new System.Drawing.Size(134, 21);
		this.CbUnit2.TabIndex = 7;
		this.CbUnit3.Enabled = false;
		this.CbUnit3.FormattingEnabled = true;
		this.CbUnit3.Location = new System.Drawing.Point(151, 118);
		this.CbUnit3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CbUnit3.Name = "CbUnit3";
		this.CbUnit3.Size = new System.Drawing.Size(134, 21);
		this.CbUnit3.TabIndex = 13;
		this.CbUnit1.Enabled = false;
		this.CbUnit1.FormattingEnabled = true;
		this.CbUnit1.Location = new System.Drawing.Point(151, 10);
		this.CbUnit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.CbUnit1.Name = "CbUnit1";
		this.CbUnit1.Size = new System.Drawing.Size(134, 21);
		this.CbUnit1.TabIndex = 1;
		this.CbUnit1.SelectedIndexChanged += new System.EventHandler(CbUnit1_SelectedIndexChanged);
		this.nConvertUnit3.DecimalPlaces = 3;
		this.nConvertUnit3.Enabled = false;
		this.nConvertUnit3.Font = new System.Drawing.Font("Tahoma", 9f);
		this.nConvertUnit3.Location = new System.Drawing.Point(151, 142);
		this.nConvertUnit3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.nConvertUnit3.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.nConvertUnit3.Name = "nConvertUnit3";
		this.nConvertUnit3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.nConvertUnit3.Size = new System.Drawing.Size(134, 22);
		this.nConvertUnit3.TabIndex = 14;
		this.nConvertUnit3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label28.Location = new System.Drawing.Point(300, 141);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(73, 16);
		this.label28.TabIndex = 142;
		this.label28.Text = "كمية الوحدة";
		this.LblUnit3.AutoSize = true;
		this.LblUnit3.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblUnit3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblUnit3.Location = new System.Drawing.Point(297, 120);
		this.LblUnit3.Name = "LblUnit3";
		this.LblUnit3.Size = new System.Drawing.Size(82, 16);
		this.LblUnit3.TabIndex = 140;
		this.LblUnit3.Text = "الوحدة الكبري";
		this.nConvertUnit2.DecimalPlaces = 3;
		this.nConvertUnit2.Enabled = false;
		this.nConvertUnit2.Font = new System.Drawing.Font("Tahoma", 9f);
		this.nConvertUnit2.Location = new System.Drawing.Point(151, 86);
		this.nConvertUnit2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.nConvertUnit2.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.nConvertUnit2.Name = "nConvertUnit2";
		this.nConvertUnit2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.nConvertUnit2.Size = new System.Drawing.Size(134, 22);
		this.nConvertUnit2.TabIndex = 8;
		this.nConvertUnit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label27.Location = new System.Drawing.Point(300, 88);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(67, 14);
		this.label27.TabIndex = 138;
		this.label27.Text = "كمية الوحدة";
		this.LblUnit2.AutoSize = true;
		this.LblUnit2.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.LblUnit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblUnit2.Location = new System.Drawing.Point(287, 67);
		this.LblUnit2.Name = "LblUnit2";
		this.LblUnit2.Size = new System.Drawing.Size(92, 14);
		this.LblUnit2.TabIndex = 136;
		this.LblUnit2.Text = "الوحدة المتوسطة";
		this.numericUpDown1.DecimalPlaces = 3;
		this.numericUpDown1.Enabled = false;
		this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.numericUpDown1.Location = new System.Drawing.Point(151, 33);
		this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.numericUpDown1.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.numericUpDown1.Size = new System.Drawing.Size(134, 22);
		this.numericUpDown1.TabIndex = 2;
		this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.numericUpDown1.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.LblUnit.AutoSize = true;
		this.LblUnit.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.LblUnit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblUnit.Location = new System.Drawing.Point(300, 35);
		this.LblUnit.Name = "LblUnit";
		this.LblUnit.Size = new System.Drawing.Size(67, 14);
		this.LblUnit.TabIndex = 134;
		this.LblUnit.Text = "كمية الوحدة";
		this.LblUnit1.AutoSize = true;
		this.LblUnit1.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.LblUnit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.LblUnit1.Location = new System.Drawing.Point(300, 11);
		this.LblUnit1.Name = "LblUnit1";
		this.LblUnit1.Size = new System.Drawing.Size(79, 14);
		this.LblUnit1.TabIndex = 132;
		this.LblUnit1.Text = "الوحدة الصغري";
		this.RBUnitTafqit3.AutoSize = true;
		this.RBUnitTafqit3.Enabled = false;
		this.RBUnitTafqit3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.RBUnitTafqit3.Location = new System.Drawing.Point(25, 127);
		this.RBUnitTafqit3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.RBUnitTafqit3.Name = "RBUnitTafqit3";
		this.RBUnitTafqit3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.RBUnitTafqit3.Size = new System.Drawing.Size(85, 17);
		this.RBUnitTafqit3.TabIndex = 171;
		this.RBUnitTafqit3.Text = "وحدة التفقيط";
		this.RBUnitTafqit3.UseVisualStyleBackColor = true;
		this.RBUnitTafqit2.AutoSize = true;
		this.RBUnitTafqit2.Enabled = false;
		this.RBUnitTafqit2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.RBUnitTafqit2.Location = new System.Drawing.Point(25, 78);
		this.RBUnitTafqit2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.RBUnitTafqit2.Name = "RBUnitTafqit2";
		this.RBUnitTafqit2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.RBUnitTafqit2.Size = new System.Drawing.Size(85, 17);
		this.RBUnitTafqit2.TabIndex = 170;
		this.RBUnitTafqit2.Text = "وحدة التفقيط";
		this.RBUnitTafqit2.UseVisualStyleBackColor = true;
		this.RBUnitTafqit1.AutoSize = true;
		this.RBUnitTafqit1.Checked = true;
		this.RBUnitTafqit1.Enabled = false;
		this.RBUnitTafqit1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.RBUnitTafqit1.Location = new System.Drawing.Point(25, 32);
		this.RBUnitTafqit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.RBUnitTafqit1.Name = "RBUnitTafqit1";
		this.RBUnitTafqit1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.RBUnitTafqit1.Size = new System.Drawing.Size(85, 17);
		this.RBUnitTafqit1.TabIndex = 169;
		this.RBUnitTafqit1.TabStop = true;
		this.RBUnitTafqit1.Text = "وحدة التفقيط";
		this.RBUnitTafqit1.UseVisualStyleBackColor = true;
		this.separatorControl1.LineThickness = 1;
		this.separatorControl1.Location = new System.Drawing.Point(3, 105);
		this.separatorControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.separatorControl1.Name = "separatorControl1";
		this.separatorControl1.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
		this.separatorControl1.Size = new System.Drawing.Size(373, 18);
		this.separatorControl1.TabIndex = 148;
		this.separatorControl3.LineThickness = 1;
		this.separatorControl3.Location = new System.Drawing.Point(3, 50);
		this.separatorControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.separatorControl3.Name = "separatorControl3";
		this.separatorControl3.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
		this.separatorControl3.Size = new System.Drawing.Size(373, 20);
		this.separatorControl3.TabIndex = 146;
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label31.Location = new System.Drawing.Point(101, 289);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(0, 16);
		this.label31.TabIndex = 125;
		this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label30.Location = new System.Drawing.Point(184, 163);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(0, 16);
		this.label30.TabIndex = 121;
		this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label36.AutoSize = true;
		this.label36.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label36.Location = new System.Drawing.Point(213, 29);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(0, 16);
		this.label36.TabIndex = 117;
		this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 9f);
		this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
		this.xtraTabPage2.Controls.Add(this.label10);
		this.xtraTabPage2.Controls.Add(this.label18);
		this.xtraTabPage2.Controls.Add(this.label16);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price3);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price3);
		this.xtraTabPage2.Controls.Add(this.BtnCreateCode);
		this.xtraTabPage2.Controls.Add(this.BtnUnit3Price);
		this.xtraTabPage2.Controls.Add(this.BtnUnit2Price);
		this.xtraTabPage2.Controls.Add(this.BtnUnit1Price);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price3);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price2);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit3Price1);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit2Price1);
		this.xtraTabPage2.Controls.Add(this.label8);
		this.xtraTabPage2.Controls.Add(this.TxtPerUnit1Price1);
		this.xtraTabPage2.Controls.Add(this.label7);
		this.xtraTabPage2.Controls.Add(this.label17);
		this.xtraTabPage2.Controls.Add(this.label15);
		this.xtraTabPage2.Controls.Add(this.label20);
		this.xtraTabPage2.Controls.Add(this.separatorControl2);
		this.xtraTabPage2.Controls.Add(this.label32);
		this.xtraTabPage2.Controls.Add(this.separatorControl4);
		this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.xtraTabPage2.Name = "xtraTabPage2";
		this.xtraTabPage2.Size = new System.Drawing.Size(421, 170);
		this.xtraTabPage2.Text = "الاسعار";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label10.Location = new System.Drawing.Point(117, 100);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(69, 14);
		this.label10.TabIndex = 116;
		this.label10.Text = "جملة الجملة";
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label18.Location = new System.Drawing.Point(117, 60);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(69, 14);
		this.label18.TabIndex = 116;
		this.label18.Text = "جملة الجملة";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label16.Location = new System.Drawing.Point(117, 18);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(69, 14);
		this.label16.TabIndex = 116;
		this.label16.Text = "جملة الجملة";
		this.TxtPerUnit3Price3.DecimalPlaces = 3;
		this.TxtPerUnit3Price3.Enabled = false;
		this.TxtPerUnit3Price3.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit3Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price3.Location = new System.Drawing.Point(50, 96);
		this.TxtPerUnit3Price3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit3Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price3.Name = "TxtPerUnit3Price3";
		this.TxtPerUnit3Price3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit3Price3.Size = new System.Drawing.Size(65, 22);
		this.TxtPerUnit3Price3.TabIndex = 18;
		this.TxtPerUnit3Price3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit2Price3.DecimalPlaces = 3;
		this.TxtPerUnit2Price3.Enabled = false;
		this.TxtPerUnit2Price3.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit2Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price3.Location = new System.Drawing.Point(50, 58);
		this.TxtPerUnit2Price3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit2Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price3.Name = "TxtPerUnit2Price3";
		this.TxtPerUnit2Price3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit2Price3.Size = new System.Drawing.Size(65, 22);
		this.TxtPerUnit2Price3.TabIndex = 18;
		this.TxtPerUnit2Price3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.BtnCreateCode.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnCreateCode.Appearance.Options.UseForeColor = true;
		this.BtnCreateCode.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnCreateCode.ImageOptions.Image");
		this.BtnCreateCode.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnCreateCode.Location = new System.Drawing.Point(187, 134);
		this.BtnCreateCode.Name = "BtnCreateCode";
		this.BtnCreateCode.Size = new System.Drawing.Size(179, 22);
		this.BtnCreateCode.TabIndex = 4;
		this.BtnCreateCode.Text = "إنشاء كود للصنف";
		this.BtnCreateCode.Click += new System.EventHandler(BtnCreateCode_Click);
		this.BtnUnit3Price.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit3Price.Appearance.Options.UseForeColor = true;
		this.BtnUnit3Price.Enabled = false;
		this.BtnUnit3Price.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit3Price.ImageOptions.Image");
		this.BtnUnit3Price.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit3Price.Location = new System.Drawing.Point(15, 95);
		this.BtnUnit3Price.Name = "BtnUnit3Price";
		this.BtnUnit3Price.Size = new System.Drawing.Size(29, 22);
		this.BtnUnit3Price.TabIndex = 4;
		this.BtnUnit3Price.Click += new System.EventHandler(BtnEditGrid_Click);
		this.BtnUnit2Price.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit2Price.Appearance.Options.UseForeColor = true;
		this.BtnUnit2Price.Enabled = false;
		this.BtnUnit2Price.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit2Price.ImageOptions.Image");
		this.BtnUnit2Price.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit2Price.Location = new System.Drawing.Point(15, 56);
		this.BtnUnit2Price.Name = "BtnUnit2Price";
		this.BtnUnit2Price.Size = new System.Drawing.Size(29, 22);
		this.BtnUnit2Price.TabIndex = 4;
		this.BtnUnit2Price.Click += new System.EventHandler(BtnEditGrid_Click);
		this.BtnUnit1Price.Appearance.ForeColor = System.Drawing.Color.Black;
		this.BtnUnit1Price.Appearance.Options.UseForeColor = true;
		this.BtnUnit1Price.Enabled = false;
		this.BtnUnit1Price.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnUnit1Price.ImageOptions.Image");
		this.BtnUnit1Price.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnUnit1Price.Location = new System.Drawing.Point(15, 14);
		this.BtnUnit1Price.Name = "BtnUnit1Price";
		this.BtnUnit1Price.Size = new System.Drawing.Size(29, 22);
		this.BtnUnit1Price.TabIndex = 4;
		this.BtnUnit1Price.Click += new System.EventHandler(BtnUnit1Price_Click);
		this.TxtPerUnit1Price3.DecimalPlaces = 3;
		this.TxtPerUnit1Price3.Enabled = false;
		this.TxtPerUnit1Price3.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit1Price3.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price3.Location = new System.Drawing.Point(50, 14);
		this.TxtPerUnit1Price3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit1Price3.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price3.Name = "TxtPerUnit1Price3";
		this.TxtPerUnit1Price3.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit1Price3.Size = new System.Drawing.Size(65, 22);
		this.TxtPerUnit1Price3.TabIndex = 18;
		this.TxtPerUnit1Price3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit3Price2.DecimalPlaces = 3;
		this.TxtPerUnit3Price2.Enabled = false;
		this.TxtPerUnit3Price2.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit3Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price2.Location = new System.Drawing.Point(187, 98);
		this.TxtPerUnit3Price2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit3Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price2.Name = "TxtPerUnit3Price2";
		this.TxtPerUnit3Price2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit3Price2.Size = new System.Drawing.Size(67, 22);
		this.TxtPerUnit3Price2.TabIndex = 18;
		this.TxtPerUnit3Price2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit2Price2.DecimalPlaces = 3;
		this.TxtPerUnit2Price2.Enabled = false;
		this.TxtPerUnit2Price2.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit2Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price2.Location = new System.Drawing.Point(187, 58);
		this.TxtPerUnit2Price2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit2Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price2.Name = "TxtPerUnit2Price2";
		this.TxtPerUnit2Price2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit2Price2.Size = new System.Drawing.Size(67, 22);
		this.TxtPerUnit2Price2.TabIndex = 18;
		this.TxtPerUnit2Price2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit1Price2.DecimalPlaces = 3;
		this.TxtPerUnit1Price2.Enabled = false;
		this.TxtPerUnit1Price2.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit1Price2.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price2.Location = new System.Drawing.Point(187, 14);
		this.TxtPerUnit1Price2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit1Price2.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price2.Name = "TxtPerUnit1Price2";
		this.TxtPerUnit1Price2.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit1Price2.Size = new System.Drawing.Size(67, 22);
		this.TxtPerUnit1Price2.TabIndex = 18;
		this.TxtPerUnit1Price2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit3Price1.DecimalPlaces = 3;
		this.TxtPerUnit3Price1.Enabled = false;
		this.TxtPerUnit3Price1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit3Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit3Price1.Location = new System.Drawing.Point(297, 96);
		this.TxtPerUnit3Price1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit3Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit3Price1.Name = "TxtPerUnit3Price1";
		this.TxtPerUnit3Price1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit3Price1.Size = new System.Drawing.Size(69, 22);
		this.TxtPerUnit3Price1.TabIndex = 18;
		this.TxtPerUnit3Price1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.TxtPerUnit2Price1.DecimalPlaces = 3;
		this.TxtPerUnit2Price1.Enabled = false;
		this.TxtPerUnit2Price1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit2Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit2Price1.Location = new System.Drawing.Point(297, 56);
		this.TxtPerUnit2Price1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit2Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit2Price1.Name = "TxtPerUnit2Price1";
		this.TxtPerUnit2Price1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit2Price1.Size = new System.Drawing.Size(69, 22);
		this.TxtPerUnit2Price1.TabIndex = 18;
		this.TxtPerUnit2Price1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(367, 98);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(50, 14);
		this.label8.TabIndex = 114;
		this.label8.Text = "القطاعي";
		this.TxtPerUnit1Price1.DecimalPlaces = 3;
		this.TxtPerUnit1Price1.Enabled = false;
		this.TxtPerUnit1Price1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.TxtPerUnit1Price1.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TxtPerUnit1Price1.Location = new System.Drawing.Point(297, 13);
		this.TxtPerUnit1Price1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.TxtPerUnit1Price1.Maximum = new decimal(new int[4] { 999999999, 0, 0, 0 });
		this.TxtPerUnit1Price1.Name = "TxtPerUnit1Price1";
		this.TxtPerUnit1Price1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.TxtPerUnit1Price1.Size = new System.Drawing.Size(69, 22);
		this.TxtPerUnit1Price1.TabIndex = 18;
		this.TxtPerUnit1Price1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label7.Location = new System.Drawing.Point(258, 99);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(39, 14);
		this.label7.TabIndex = 112;
		this.label7.Text = "الجملة";
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label17.Location = new System.Drawing.Point(367, 58);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(50, 14);
		this.label17.TabIndex = 114;
		this.label17.Text = "القطاعي";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label15.Location = new System.Drawing.Point(258, 59);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(39, 14);
		this.label15.TabIndex = 112;
		this.label15.Text = "الجملة";
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label20.Location = new System.Drawing.Point(367, 16);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(50, 14);
		this.label20.TabIndex = 114;
		this.label20.Text = "القطاعي";
		this.separatorControl2.LineThickness = 1;
		this.separatorControl2.Location = new System.Drawing.Point(40, 79);
		this.separatorControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.separatorControl2.Name = "separatorControl2";
		this.separatorControl2.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
		this.separatorControl2.Size = new System.Drawing.Size(369, 19);
		this.separatorControl2.TabIndex = 117;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Tahoma", 8.5f);
		this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label32.Location = new System.Drawing.Point(258, 17);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(39, 14);
		this.label32.TabIndex = 112;
		this.label32.Text = "الجملة";
		this.separatorControl4.LineThickness = 1;
		this.separatorControl4.Location = new System.Drawing.Point(40, 39);
		this.separatorControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		this.separatorControl4.Name = "separatorControl4";
		this.separatorControl4.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
		this.separatorControl4.Size = new System.Drawing.Size(369, 19);
		this.separatorControl4.TabIndex = 117;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1169, 733);
		base.Controls.Add(this.TapPrice);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmImportExcel";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Excel";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPathFile.Properties).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.NVat).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TapPrice).EndInit();
		this.TapPrice.ResumeLayout(false);
		this.xtraTabPage1.ResumeLayout(false);
		this.xtraTabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nConvertUnit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl3).EndInit();
		this.xtraTabPage2.ResumeLayout(false);
		this.xtraTabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit3Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit2Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtPerUnit1Price1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl4).EndInit();
		base.ResumeLayout(false);
	}
}


