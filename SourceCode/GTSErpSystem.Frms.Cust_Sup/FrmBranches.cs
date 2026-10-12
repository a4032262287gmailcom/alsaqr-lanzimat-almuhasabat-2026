using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Cust_Sup.Search;

namespace GTSErpSystem.Frms.Cust_Sup;

public class FrmBranches : XtraForm
{
	private Class_Branches BRN = new Class_Branches();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public int GrpID;

	public bool IsCustomer;

	public bool IsSupplier;

	private IContainer components = null;

	private GroupBox groupBox1;

	private ButtonEdit TxtAccount;

	private Label label12;

	private Label label11;

	private TextBox TxtNote;

	private Label label10;

	private TextBox TxtPhone;

	private Label label9;

	private TextBox TxtFax;

	private Label label8;

	private TextBox TxtAddress;

	private Label label5;

	private TextBox TxtName;

	private ButtonEdit TxtCode;

	private Label label1;

	private SimpleButton BtnEdit;

	private SimpleButton BtnSave;

	private SimpleButton BtnNew;

	private SimpleButton BtnDelete;

	private SimpleButton BtnExit;

	public FrmBranches()
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
		Clear();
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		TxtName.Focus();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave, BtnEdit, BtnDelete);
		}
	}

	private void Frms_Load(object sender, EventArgs e)
	{
		base.KeyPreview = true;
	}

	private void Frms_KeyDown(object sender, KeyEventArgs e)
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

	private void BtnNew_Click(object sender, EventArgs e)
	{
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
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

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TxtName_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		Clear();
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ControlSave_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			BtnSave.Focus();
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		SetKeyboardLayout(GetInputLanguageByName("ar"));
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

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void TxtNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void TxtCode_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchBranch frmSearchBranch = new FrmSearchBranch();
			frmSearchBranch.ShowDialog();
			if (frmSearchBranch.CustCode > 0)
			{
				int FrmID = frmSearchBranch.CustCode;
				Account_Branch account_Branch = (from x in BRN.GetAll()
					where x.ID == FrmID
					select x).FirstOrDefault();
				TxtCode.Text = FrmID.ToString();
				TxtName.Text = account_Branch.Name;
				TxtPhone.Text = account_Branch.Phone;
				TxtFax.Text = account_Branch.Fax;
				TxtAccount.Text = account_Branch.AccountNo.ToString();
				TxtAddress.Text = account_Branch.Address;
				TxtNote.Text = account_Branch.Note;
				BtnSave.Enabled = false;
				BtnEdit.Enabled = true;
				BtnDelete.Enabled = true;
				IsEnableFalse();
			}
			else
			{
				TxtName.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
			Clear();
		}
	}

	private void Clear()
	{
		TxtCode.Text = "";
		TxtName.Text = "";
		TxtPhone.Text = "";
		TxtFax.Text = "";
		TxtAccount.Text = "";
		TxtAddress.Text = "";
		TxtNote.Text = "";
		BtnSave.Enabled = true;
		BtnEdit.Enabled = false;
		BtnDelete.Enabled = false;
		IsEnableTrue();
		TxtName.Focus();
	}

	private void Save()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 2))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من اضافة ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
				{
					num = BRN.Insert(out var Code, out var AccountNo, TxtName.Text, TxtPhone.Text, TxtFax.Text, TxtAddress.Text, TxtNote.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الفروع  ( " + TxtName.Text + " ) بنجاح", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						TxtCode.Text = Code.ToString();
						TxtAccount.Text = AccountNo.ToString();
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
					}
					else
					{
						MessageBox.Show(" تم حفظ بيانات الفروع بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				}
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم الفروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Edit()
	{
		if (CheckPermession.CheckUserPageEventEdit(LoginDetails.GroupID, 2))
		{
			if (!TxtName.Enabled)
			{
				IsEnableTrue();
				BtnEdit.Text = "حفظ F6";
				return;
			}
			int num = 0;
			if (TxtName.Text != "" && TxtCode.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من تعديل ( " + TxtName.Text + " ) الي قاعدة البيانات", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = BRN.Update(Convert.ToInt32(TxtCode.Text), TxtName.Text, TxtPhone.Text, TxtFax.Text, TxtAddress.Text, TxtNote.Text);
					if (num > 0)
					{
						MessageBox.Show("تم حفظ بيانات الفروع ( " + TxtName.Text + " ) بنجاح", "نجاح التعديل", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
						BtnSave.Enabled = false;
						BtnEdit.Enabled = true;
						BtnDelete.Enabled = true;
						IsEnableFalse();
					}
					else
					{
						MessageBox.Show("تم حفظ بيانات الفروع بنفس الاسم ( " + TxtName.Text + " ) لا يمكن التكرار", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else if (TxtName.Text == "")
			{
				MessageBox.Show(" عفوا، ادخل اسم الفروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void Delete()
	{
		if (CheckPermession.CheckUserPageEventDelete(LoginDetails.GroupID, 2))
		{
			int num = 0;
			if (TxtName.Text != "")
			{
				switch (MessageBox.Show("هل أنت متأكد من حذف ( " + TxtName.Text + " ) من قاعدة البيانات", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
				case DialogResult.Yes:
					num = BRN.Delete(Convert.ToInt32(TxtCode.Text));
					if (num > 0)
					{
						MessageBox.Show("تم حذف بيانات الفروع ( " + TxtName.Text + " ) بنجاح", "نجاح الحذف", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						BtnSave.Enabled = true;
						BtnEdit.Enabled = false;
						BtnDelete.Enabled = false;
						Clear();
					}
					else
					{
						MessageBox.Show("لا يمكن حذف ( " + TxtName.Text + " ) لوجود عمليات جارية بهذا الفروع", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					break;
				case DialogResult.No:
					TxtName.Focus();
					break;
				}
			}
			else
			{
				MessageBox.Show("عفوا، اختر اسم الفروع ثم حاول مرة اخري", "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
				TxtName.Focus();
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	private void IsEnableFalse()
	{
		TxtAddress.Enabled = false;
		TxtPhone.Enabled = false;
		TxtName.Enabled = false;
		TxtNote.Enabled = false;
		TxtPhone.Enabled = false;
		TxtFax.Enabled = false;
		BtnEdit.Text = "تعديل F6";
	}

	private void IsEnableTrue()
	{
		TxtAddress.Enabled = true;
		TxtPhone.Enabled = true;
		TxtName.Enabled = true;
		TxtNote.Enabled = true;
		TxtPhone.Enabled = true;
		TxtFax.Enabled = true;
		BtnEdit.Text = "تعديل F6";
		TxtName.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.Cust_Sup.FrmBranches));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TxtAccount = new DevExpress.XtraEditors.ButtonEdit();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtNote = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.TxtPhone = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtFax = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.TxtAddress = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.TxtName = new System.Windows.Forms.TextBox();
		this.TxtCode = new DevExpress.XtraEditors.ButtonEdit();
		this.label1 = new System.Windows.Forms.Label();
		this.BtnEdit = new DevExpress.XtraEditors.SimpleButton();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
		this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.TxtAccount);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.TxtNote);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.TxtPhone);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtFax);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.TxtAddress);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.TxtName);
		this.groupBox1.Controls.Add(this.TxtCode);
		this.groupBox1.Controls.Add(this.label1);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.TxtAccount, "TxtAccount");
		this.TxtAccount.Name = "TxtAccount";
		this.TxtAccount.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 255, 210);
		this.TxtAccount.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtAccount.Properties.Appearance.Font");
		this.TxtAccount.Properties.Appearance.Options.UseBackColor = true;
		this.TxtAccount.Properties.Appearance.Options.UseFont = true;
		this.TxtAccount.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtAccount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtAccount.Properties.ReadOnly = true;
		this.TxtAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label12, "label12");
		this.label12.Name = "label12";
		resources.ApplyResources(this.label11, "label11");
		this.label11.Name = "label11";
		resources.ApplyResources(this.TxtNote, "TxtNote");
		this.TxtNote.Name = "TxtNote";
		this.TxtNote.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtNote.KeyDown += new System.Windows.Forms.KeyEventHandler(ControlSave_KeyDown);
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.TxtPhone, "TxtPhone");
		this.TxtPhone.Name = "TxtPhone";
		this.TxtPhone.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.TxtFax, "TxtFax");
		this.TxtFax.Name = "TxtFax";
		this.TxtFax.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.TxtAddress, "TxtAddress");
		this.TxtAddress.Name = "TxtAddress";
		this.TxtAddress.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.TxtName, "TxtName");
		this.TxtName.Name = "TxtName";
		this.TxtName.Enter += new System.EventHandler(textBox1_Enter);
		this.TxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.TxtCode, "TxtCode");
		this.TxtCode.Name = "TxtCode";
		this.TxtCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 255, 210);
		this.TxtCode.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("TxtCode.Properties.Appearance.Font");
		this.TxtCode.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCode.Properties.Appearance.Options.UseFont = true;
		this.TxtCode.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.TxtCode.Properties.ReadOnly = true;
		this.TxtCode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCode_ButtonClick);
		this.TxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		this.BtnEdit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnEdit.Appearance.Font");
		this.BtnEdit.Appearance.Options.UseFont = true;
		this.BtnEdit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnEdit.ImageOptions.Image");
		this.BtnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnEdit, "BtnEdit");
		this.BtnEdit.Name = "BtnEdit";
		this.BtnEdit.Click += new System.EventHandler(BtnEdit_Click);
		this.BtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnSave.Appearance.Font");
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnSave, "BtnSave");
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnNew.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnNew.Appearance.Font");
		this.BtnNew.Appearance.Options.UseFont = true;
		this.BtnNew.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnNew.ImageOptions.Image");
		this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnNew, "BtnNew");
		this.BtnNew.Name = "BtnNew";
		this.BtnNew.Click += new System.EventHandler(BtnNew_Click);
		this.BtnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnDelete.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnDelete.Appearance.Font");
		this.BtnDelete.Appearance.Options.UseFont = true;
		this.BtnDelete.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnDelete.ImageOptions.Image");
		this.BtnDelete.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnDelete, "BtnDelete");
		this.BtnDelete.Name = "BtnDelete";
		this.BtnDelete.Click += new System.EventHandler(BtnDelete_Click);
		this.BtnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnExit.Appearance.Font = (System.Drawing.Font)resources.GetObject("BtnExit.Appearance.Font");
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		resources.ApplyResources(this.BtnExit, "BtnExit");
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.BtnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.BtnEdit);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnNew);
		base.Controls.Add(this.BtnDelete);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmBranches";
		base.Load += new System.EventHandler(Frms_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Frms_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtAccount.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCode.Properties).EndInit();
		base.ResumeLayout(false);
	}
}


