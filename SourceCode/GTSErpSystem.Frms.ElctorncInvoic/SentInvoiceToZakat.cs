using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.BLL.Orders;
using GTSErpSystem.Dto;
using GTSErpSystem.Helper;
using Janus.Windows.EditControls;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;

namespace GTSErpSystem.Frms.ElctorncInvoic;

public class SentInvoiceToZakat : XtraForm
{
	private DataTable datatable = new DataTable();

	private Class_ElctroncInvoice Zakat = new Class_ElctroncInvoice();

	private GTSdbEntities db = new GTSdbEntities();

	private string fileNameAR = Application.StartupPath + "\\Layout\\SentInvoiceAR.xml";

	private string fileNameEN = Application.StartupPath + "\\Layout\\SentInvoiceEN.xml";

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	private IContainer components = null;

	private GridControl gridControl1;

	private GridView gridView1;

	private GridColumn gridColumn1;

	private GridColumn gridColumn2;

	private GridColumn gridColumn3;

	private GridColumn gridColumn4;

	private GridColumn gridColumn5;

	private GridColumn gridColumn6;

	private GridColumn gridColumn7;

	private GridColumn gridColumn8;

	private GridColumn gridColumn9;

	private GridColumn gridColumn10;

	private UIGroupBox uiGroupBox2;

	private Label label20;

	private GroupBox groupBox1;

	private RepositoryItemButtonEdit repositoryItemButtonEdit2;

	private RepositoryItemButtonEdit repositoryItemButtonEdit1;

	private SimpleButton BtnSent;

	private CheckBox CKSelected;

	private GridColumn gridColumn11;

	private GridColumn gridColumn12;

	private Mode mode { get; set; }

	public SentInvoiceToZakat()
	{
		InitializeComponent();
		SetInitialRow();
		Bind();
	}

	private void SetInitialRow()
	{
		datatable.Columns.Add("SN", typeof(int));
		datatable.Columns.Add("Id", typeof(int));
		datatable.Columns.Add("OrderID", typeof(int));
		datatable.Columns.Add("IsValid", typeof(string));
		datatable.Columns.Add("Uploaded", typeof(string));
		datatable.Columns.Add("UploadedDate", typeof(string));
		datatable.Columns.Add("ErorrMessage", typeof(string));
		datatable.Columns.Add("FatoraType", typeof(string));
		datatable.Columns.Add("Date", typeof(string));
		datatable.Columns.Add("Select", typeof(bool));
		gridView1.RowCellStyle += gridView1_RowCellStyle;
		gridView1.CustomRowCellEdit += gridView1_CustomRowCellEdit;
	}

	private void Bind()
	{
		datatable.Clear();
		List<ResultElectronicInvoiceXml> list = ((!LoginDetails.ShowFatora) ? Zakat.GetAllZakat() : Zakat.GetAllZakatNotUploaded());
		foreach (ResultElectronicInvoiceXml item in list)
		{
			DataRow dataRow = datatable.NewRow();
			dataRow["Id"] = item.Id;
			dataRow["OrderID"] = item.OrderId;
			dataRow["IsValid"] = (item.IsValid ? "Valid" : "Not Valid");
			dataRow["Uploaded"] = (Convert.ToBoolean(item.Uploaded) ? "Success" : "Not Success");
			dataRow["UploadedDate"] = item.UploadedDate?.ToString("yyyy-MM-dd HH:mm:ss");
			dataRow["ErorrMessage"] = item.ErrorMessage ?? string.Empty;
			dataRow["Date"] = item.Created?.ToString("yyyy-MM-dd");
			dataRow["Select"] = false;
			dataRow["FatoraType"] = item.FatoraType ?? string.Empty;
			datatable.Rows.Add(dataRow);
			ArrangeDatatable();
		}
		gridControl1.DataSource = datatable;
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

	public async Task<bool> SentToSazakt(int id)
	{
		waitForm.Show();
		if (LoginDetails.ModeInvoice == "developer")
		{
			mode = Mode.developer;
		}
		else if (LoginDetails.ModeInvoice == "Simulation")
		{
			mode = Mode.Simulation;
		}
		else if (LoginDetails.ModeInvoice == "Production")
		{
			mode = Mode.Production;
		}
		bool isSuccess = false;
		ElectronicInvoice invoiceZakat = new ElectronicInvoice();
		ResultElectronicInvoiceXml res = db.ResultElectronicInvoiceXmls.FirstOrDefault((ResultElectronicInvoiceXml s) => s.Id == id);
		if (res?.IsValid ?? false)
		{
			ReturnSentInvoiceToZakat sentZakat = await invoiceZakat.SentInvoiceToZakat(GenerateResult(res), CSIDInfoDto(), res.InvoiceTypeName, mode);
			isSuccess = sentZakat.IsSuccess;
			if (isSuccess)
			{
				Zakat.UpdateZakatById(res.Id, res.QRCode);
			}
			else
			{
				string errorMessages = string.Join(Environment.NewLine, sentZakat.MessageErrors.SelectMany((string[] e) => e));
				MessageBox.Show(errorMessages, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		waitForm.Close();
		return isSuccess;
	}

	private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
	{
		if (e.Column.FieldName == "IsValid")
		{
			GridView gridView = sender as GridView;
			string text = gridView.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
			e.Appearance.ForeColor = Color.White;
			if (text == "Valid")
			{
				e.Appearance.BackColor = Color.SteelBlue;
			}
			else if (text == "Not Valid")
			{
				e.Appearance.BackColor = Color.DarkRed;
			}
		}
		else if (e.Column.FieldName == "Uploaded")
		{
			GridView gridView2 = sender as GridView;
			string text2 = gridView2.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
			e.Appearance.ForeColor = Color.White;
			if (text2 == "Success")
			{
				e.Appearance.BackColor = Color.SteelBlue;
			}
			else if (text2 == "Not Success")
			{
				e.Appearance.BackColor = Color.DarkRed;
			}
		}
		if (e.Column.FieldName == "FatoraType")
		{
			GridView gridView3 = sender as GridView;
			string text3 = gridView3.GetRowCellValue(e.RowHandle, e.Column)?.ToString();
			if (text3 == "مبيعات" || text3 == "مبيعات كاشير")
			{
				e.Appearance.ForeColor = Color.White;
				e.Appearance.BackColor = Color.SteelBlue;
			}
			else if (text3 == "اشعار دائن")
			{
				e.Appearance.ForeColor = Color.Black;
				e.Appearance.BackColor = Color.DarkSeaGreen;
			}
		}
	}

	private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
	{
		if (e.Column.FieldName == "Sent")
		{
			string text = gridView1.GetRowCellValue(e.RowHandle, "Uploaded")?.ToString();
			if (text == "Success")
			{
				gridView1.SetRowCellValue(e.RowHandle, "Select", "");
				RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
				repositoryItemButtonEdit.Buttons.Clear();
				repositoryItemButtonEdit.ReadOnly = true;
				repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
				e.RepositoryItem = repositoryItemButtonEdit;
			}
			else
			{
				e.RepositoryItem = repositoryItemButtonEdit1;
			}
		}
	}

	private async void BtnSent_Click(object sender, EventArgs e)
	{
		gridView1.CloseEditor();
		gridView1.UpdateCurrentRow();
		List<DataRow> selectedRows = (from row2 in datatable.AsEnumerable()
			where row2.Field<bool?>("Select") == true
			select row2).ToList();
		foreach (DataRow row in selectedRows)
		{
			int id = row.Field<int>("ID");
			if (await SentToSazakt(id))
			{
				datatable.Rows.Remove(row);
				gridView1.RefreshData();
			}
		}
	}

	private void CKSelected_CheckedChanged(object sender, EventArgs e)
	{
		if (CKSelected.Checked)
		{
			if (datatable != null)
			{
				foreach (DataRow row in datatable.Rows)
				{
					if (row["Uploaded"].ToString() == "Not Success")
					{
						row["Select"] = true;
					}
				}
			}
		}
		else if (datatable != null)
		{
			foreach (DataRow row2 in datatable.Rows)
			{
				if (row2["Uploaded"].ToString() == "Not Success")
				{
					row2["Select"] = false;
				}
			}
		}
		gridControl1.DataSource = datatable;
	}

	private void SentInvoiceToZakat_Load(object sender, EventArgs e)
	{
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
		gridView1.Columns["Id"].Visible = false;
		if (LoginDetails.IsElectronicInvoice)
		{
			gridView1.Columns["Delete"].Visible = false;
		}
		else
		{
			gridView1.Columns["Delete"].Visible = true;
		}
	}

	private void SentInvoiceToZakat_FormClosing(object sender, FormClosingEventArgs e)
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

	private async void repositoryItemButtonEdit1_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int rowHandle = gridView1.FocusedRowHandle;
		int rowIndex = gridView1.GetDataSourceRowIndex(rowHandle);
		DataRow row = gridView1.GetDataRow(rowIndex);
		if (row != null && row.ItemArray.Length != 0)
		{
			int id = Convert.ToInt32(row[1]?.ToString());
			if (await SentToSazakt(id))
			{
				gridView1.DeleteRow(rowHandle);
			}
		}
	}

	private void repositoryItemButtonEdit2_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		int num = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle) + 1;
		DataRow dataRow = null;
		if (!LoginDetails.IsElectronicInvoice)
		{
			foreach (DataRow row in datatable.Rows)
			{
				if (row["SN"].ToString() == Convert.ToString(num))
				{
					string text = row["OrderID"].ToString();
					switch (MessageBox.Show("هل أنت متأكد من حذف فاتورة المبيعات رقم  ( " + text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign))
					{
					case DialogResult.Yes:
						Zakat.DeleteZakatById(text);
						dataRow = row;
						MessageBox.Show("تم حذف الفاتوره بنجاح", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						goto end_IL_0103;
					case DialogResult.No:
						return;
					}
				}
				continue;
				end_IL_0103:
				break;
			}
		}
		else
		{
			MessageBox.Show("لا يمكن حذف الفاتوره", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
		if (dataRow != null)
		{
			datatable.Rows.Remove(dataRow);
		}
		gridControl1.DataSource = datatable;
		ArrangeDatatable();
		gridControl1.RefreshDataSource();
	}

	private CSIDInfoDto CSIDInfoDto()
	{
		return new CSIDInfoDto
		{
			CertPem = LoginDetails.PublicKey,
			CSR = LoginDetails.Crs,
			PrivateKey = LoginDetails.PrivateKey,
			Secretkey = LoginDetails.SecretKey
		};
	}

	private Result GenerateResult(ResultElectronicInvoiceXml resultElectronic)
	{
		return new Result
		{
			QRCode = resultElectronic.QRCode,
			AllowanceTotalAmount = resultElectronic.AllowanceTotalAmount,
			ChargeTotalAmount = resultElectronic.ChargeTotalAmount,
			EncodedInvoice = resultElectronic.EncodedInvoice,
			InvoiceHash = resultElectronic.InvoiceHash,
			IsValid = resultElectronic.IsValid,
			LineExtensionAmount = resultElectronic.LineExtensionAmount,
			SingedXMLFileNameFullPath = resultElectronic.SingedXMLFileNameFullPath,
			UUID = resultElectronic.UUID,
			TaxInclusiveAmount = resultElectronic.TaxInclusiveAmount,
			PayableAmount = resultElectronic.PayableAmount,
			TaxAmount = resultElectronic.TaxAmount,
			TaxExclusiveAmount = resultElectronic.TaxExclusiveAmount,
			ErrorMessage = resultElectronic.ErrorMessage,
			SingedXMLFileName = resultElectronic.SingedXMLFileName,
			SingedXML = resultElectronic.SingedXML,
			ResultedValue = resultElectronic.ResultedValue,
			PrepaidAmount = resultElectronic.PrepaidAmount,
			SingedXMLFileNameShortPath = resultElectronic.SingedXMLFileNameShortPath,
			PIH = resultElectronic.PIH,
			Operation = resultElectronic.Operation,
			NormalXMLFileNameShortPath = resultElectronic.NormalXMLFileNameShortPath,
			NormalXMLFileNameFullPath = resultElectronic.NormalXMLFileNameFullPath
		};
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.ElctorncInvoic.SentInvoiceToZakat));
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.gridControl1 = new DevExpress.XtraGrid.GridControl();
		this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
		this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
		this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.BtnSent = new DevExpress.XtraEditors.SimpleButton();
		this.CKSelected = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox2).BeginInit();
		this.uiGroupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.gridControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gridControl1.Location = new System.Drawing.Point(6, 15);
		this.gridControl1.MainView = this.gridView1;
		this.gridControl1.Name = "gridControl1";
		this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2] { this.repositoryItemButtonEdit1, this.repositoryItemButtonEdit2 });
		this.gridControl1.Size = new System.Drawing.Size(857, 431);
		this.gridControl1.TabIndex = 0;
		this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
		this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[12]
		{
			this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gridColumn4, this.gridColumn5, this.gridColumn6, this.gridColumn7, this.gridColumn8, this.gridColumn9, this.gridColumn10,
			this.gridColumn11, this.gridColumn12
		});
		this.gridView1.GridControl = this.gridControl1;
		this.gridView1.Name = "gridView1";
		this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceCell.Options.UseFont = true;
		this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn1.AppearanceHeader.Options.UseFont = true;
		this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn1.Caption = "ظ…";
		this.gridColumn1.FieldName = "SN";
		this.gridColumn1.MinWidth = 25;
		this.gridColumn1.Name = "gridColumn1";
		this.gridColumn1.OptionsColumn.AllowEdit = false;
		this.gridColumn1.OptionsColumn.AllowFocus = false;
		this.gridColumn1.Visible = true;
		this.gridColumn1.VisibleIndex = 0;
		this.gridColumn1.Width = 94;
		this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceCell.Options.UseFont = true;
		this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn2.AppearanceHeader.Options.UseFont = true;
		this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn2.Caption = "رقم الفاتوره";
		this.gridColumn2.FieldName = "OrderID";
		this.gridColumn2.MinWidth = 25;
		this.gridColumn2.Name = "gridColumn2";
		this.gridColumn2.OptionsColumn.AllowEdit = false;
		this.gridColumn2.OptionsColumn.AllowFocus = false;
		this.gridColumn2.Visible = true;
		this.gridColumn2.VisibleIndex = 1;
		this.gridColumn2.Width = 94;
		this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceCell.Options.UseFont = true;
		this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn3.AppearanceHeader.Options.UseFont = true;
		this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn3.Caption = "Is Valid";
		this.gridColumn3.FieldName = "IsValid";
		this.gridColumn3.MinWidth = 25;
		this.gridColumn3.Name = "gridColumn3";
		this.gridColumn3.OptionsColumn.AllowEdit = false;
		this.gridColumn3.OptionsColumn.AllowFocus = false;
		this.gridColumn3.Visible = true;
		this.gridColumn3.VisibleIndex = 2;
		this.gridColumn3.Width = 94;
		this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceCell.Options.UseFont = true;
		this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn4.AppearanceHeader.Options.UseFont = true;
		this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn4.Caption = "Uploaded";
		this.gridColumn4.FieldName = "Uploaded";
		this.gridColumn4.MinWidth = 25;
		this.gridColumn4.Name = "gridColumn4";
		this.gridColumn4.OptionsColumn.AllowEdit = false;
		this.gridColumn4.OptionsColumn.AllowFocus = false;
		this.gridColumn4.Visible = true;
		this.gridColumn4.VisibleIndex = 3;
		this.gridColumn4.Width = 94;
		this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceCell.Options.UseFont = true;
		this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn5.AppearanceHeader.Options.UseFont = true;
		this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn5.Caption = "تاريخ الرفع";
		this.gridColumn5.FieldName = "UploadedDate";
		this.gridColumn5.MinWidth = 25;
		this.gridColumn5.Name = "gridColumn5";
		this.gridColumn5.OptionsColumn.AllowEdit = false;
		this.gridColumn5.OptionsColumn.AllowFocus = false;
		this.gridColumn5.Visible = true;
		this.gridColumn5.VisibleIndex = 4;
		this.gridColumn5.Width = 94;
		this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceCell.Options.UseFont = true;
		this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn6.AppearanceHeader.Options.UseFont = true;
		this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn6.Caption = "رساله الخطأ";
		this.gridColumn6.FieldName = "ErorrMessage";
		this.gridColumn6.MinWidth = 25;
		this.gridColumn6.Name = "gridColumn6";
		this.gridColumn6.OptionsColumn.AllowEdit = false;
		this.gridColumn6.OptionsColumn.AllowFocus = false;
		this.gridColumn6.Visible = true;
		this.gridColumn6.VisibleIndex = 5;
		this.gridColumn6.Width = 94;
		this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceCell.Options.UseFont = true;
		this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn7.AppearanceHeader.Options.UseFont = true;
		this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn7.Caption = "التاريخ ";
		this.gridColumn7.FieldName = "Date";
		this.gridColumn7.MinWidth = 25;
		this.gridColumn7.Name = "gridColumn7";
		this.gridColumn7.OptionsColumn.AllowEdit = false;
		this.gridColumn7.OptionsColumn.AllowFocus = false;
		this.gridColumn7.Visible = true;
		this.gridColumn7.VisibleIndex = 6;
		this.gridColumn7.Width = 94;
		this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceCell.Options.UseFont = true;
		this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn8.AppearanceHeader.Options.UseFont = true;
		this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn8.Caption = "حذف";
		this.gridColumn8.ColumnEdit = this.repositoryItemButtonEdit2;
		this.gridColumn8.FieldName = "Delete";
		this.gridColumn8.MinWidth = 25;
		this.gridColumn8.Name = "gridColumn8";
		this.gridColumn8.Visible = true;
		this.gridColumn8.VisibleIndex = 7;
		this.gridColumn8.Width = 94;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
		this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit2_ButtonPressed);
		this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceCell.Options.UseFont = true;
		this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn9.AppearanceHeader.Options.UseFont = true;
		this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn9.Caption = "رفع";
		this.gridColumn9.ColumnEdit = this.repositoryItemButtonEdit1;
		this.gridColumn9.FieldName = "Sent";
		this.gridColumn9.MinWidth = 25;
		this.gridColumn9.Name = "gridColumn9";
		this.gridColumn9.Visible = true;
		this.gridColumn9.VisibleIndex = 8;
		this.gridColumn9.Width = 94;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)
		});
		this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
		this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
		this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonPressed);
		this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn10.AppearanceCell.Options.UseFont = true;
		this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn10.AppearanceHeader.Options.UseFont = true;
		this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn10.Caption = "تحديد";
		this.gridColumn10.FieldName = "Select";
		this.gridColumn10.MinWidth = 25;
		this.gridColumn10.Name = "gridColumn10";
		this.gridColumn10.Visible = true;
		this.gridColumn10.VisibleIndex = 9;
		this.gridColumn10.Width = 94;
		this.gridColumn11.Caption = "Id";
		this.gridColumn11.FieldName = "Id";
		this.gridColumn11.MinWidth = 25;
		this.gridColumn11.Name = "gridColumn11";
		this.gridColumn11.OptionsColumn.AllowEdit = false;
		this.gridColumn11.OptionsColumn.AllowFocus = false;
		this.gridColumn11.Visible = true;
		this.gridColumn11.VisibleIndex = 10;
		this.gridColumn11.Width = 94;
		this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn12.AppearanceCell.Options.UseFont = true;
		this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
		this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
		this.gridColumn12.AppearanceHeader.Options.UseFont = true;
		this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
		this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.gridColumn12.Caption = "نوع الفاتوره";
		this.gridColumn12.FieldName = "FatoraType";
		this.gridColumn12.MinWidth = 25;
		this.gridColumn12.Name = "gridColumn12";
		this.gridColumn12.OptionsColumn.AllowEdit = false;
		this.gridColumn12.OptionsColumn.AllowFocus = false;
		this.gridColumn12.Visible = true;
		this.gridColumn12.VisibleIndex = 11;
		this.gridColumn12.Width = 94;
		this.uiGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.uiGroupBox2.BackColor = System.Drawing.Color.SteelBlue;
		this.uiGroupBox2.Controls.Add(this.label20);
		this.uiGroupBox2.Location = new System.Drawing.Point(12, 19);
		this.uiGroupBox2.Name = "uiGroupBox2";
		this.uiGroupBox2.Size = new System.Drawing.Size(869, 35);
		this.uiGroupBox2.TabIndex = 238;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.Snow;
		this.label20.Location = new System.Drawing.Point(672, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(122, 24);
		this.label20.TabIndex = 1;
		this.label20.Text = "قائمه بالفواتير";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.gridControl1);
		this.groupBox1.Location = new System.Drawing.Point(12, 103);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(869, 452);
		this.groupBox1.TabIndex = 239;
		this.groupBox1.TabStop = false;
		this.BtnSent.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSent.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSent.Appearance.Options.UseFont = true;
		this.BtnSent.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSent.ImageOptions.Image");
		this.BtnSent.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSent.Location = new System.Drawing.Point(12, 61);
		this.BtnSent.Margin = new System.Windows.Forms.Padding(5);
		this.BtnSent.Name = "BtnSent";
		this.BtnSent.Size = new System.Drawing.Size(117, 42);
		this.BtnSent.TabIndex = 240;
		this.BtnSent.Text = "ارسال";
		this.BtnSent.Click += new System.EventHandler(BtnSent_Click);
		this.CKSelected.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CKSelected.AutoSize = true;
		this.CKSelected.Font = new System.Drawing.Font("Tahoma", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CKSelected.ForeColor = System.Drawing.Color.SteelBlue;
		this.CKSelected.Location = new System.Drawing.Point(731, 78);
		this.CKSelected.Name = "CKSelected";
		this.CKSelected.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.CKSelected.Size = new System.Drawing.Size(150, 25);
		this.CKSelected.TabIndex = 241;
		this.CKSelected.Text = "تحديد كل الفواتير";
		this.CKSelected.UseVisualStyleBackColor = true;
		this.CKSelected.CheckedChanged += new System.EventHandler(CKSelected_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(893, 567);
		base.Controls.Add(this.CKSelected);
		base.Controls.Add(this.BtnSent);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.uiGroupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "SentInvoiceToZakat";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		this.Text = "SentInvoiceToZakat";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SentInvoiceToZakat_FormClosing);
		base.Load += new System.EventHandler(SentInvoiceToZakat_Load);
		((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.uiGroupBox2).EndInit();
		this.uiGroupBox2.ResumeLayout(false);
		this.uiGroupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}


