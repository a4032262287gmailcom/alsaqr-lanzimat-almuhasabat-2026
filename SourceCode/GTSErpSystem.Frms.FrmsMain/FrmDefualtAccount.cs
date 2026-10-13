using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using GTSErpSystem.BLL.Account;
using GTSErpSystem.BLL.Cust_Sup;
using GTSErpSystem.BLL.Main.Setting;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.Frms.Cust_Sup.Search;
using GTSErpSystem.Frms.FrmsMain.Search;

namespace GTSErpSystem.Frms.FrmsMain;

public class FrmDefualtAccount : XtraForm
{
	private DataTable datatable = new DataTable();

	private Class_DefualtAccount DFLTAccNT = new Class_DefualtAccount();

	private Class_Stores STR = new Class_Stores();

	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private IContainer components = null;

	private GroupBox groupBox1;

	private ButtonEdit TxtNameBranch;

	private Label label6;

	private Label LblBranches;

	private Label label18;

	private Label label19;

	private ButtonEdit TxtReturnOrderAjil;

	private Label label20;

	private Label label21;

	private ButtonEdit TxtReturnOrderCash;

	private Label LblOrderAjil;

	private Label label15;

	private ButtonEdit TxtOrderAjil;

	private Label LblOrderCash;

	private Label label17;

	private ButtonEdit TxtOrderCash;

	private Label LblSupplierCash;

	private Label label11;

	private ButtonEdit TxtSupplierCash;

	private Label LblCustomerCash;

	private Label label13;

	private ButtonEdit TxtCustomerCash;

	private Label LblZmmSuppliers;

	private Label label7;

	private ButtonEdit TxtZmmSuppliers;

	private Label LblZmmCustomer;

	private Label label9;

	private ButtonEdit TxtZmmCustomer;

	private Label LblStore2;

	private Label label4;

	private Label lblStoreMain;

	private Label label2;

	private SeparatorControl separatorControl6;

	private Label LblMasareef;

	private Label label23;

	private ButtonEdit TxtMasareef;

	private Label LblCloseRun;

	private Label label25;

	private ButtonEdit TxtCloseRun;

	private Label LblMainMoney;

	private Label label27;

	private ButtonEdit TxtMainMoney;

	private Label LblArbah77;

	private Label label29;

	private ButtonEdit TxtArbah77;

	private Label LblBankSraf;

	private Label label31;

	private ButtonEdit TxtBankSraf;

	private Label LblBoxCash;

	private Label label33;

	private ButtonEdit TxtBoxCash;

	private Label LblMainBank;

	private Label label35;

	private ButtonEdit TxtMainBank;

	private Label LblMainBox;

	private Label label37;

	private ButtonEdit TxtMainBox;

	private Label LblReturnOrderAjil;

	private Label LblReturnOrderCash;

	private Label LblGardMinus;

	private Label label45;

	private ButtonEdit TxtGardMinus;

	private Label LblGardPlus;

	private Label label47;

	private ButtonEdit TxtGardPlus;

	private Label LblOpenQuantity;

	private Label label49;

	private ButtonEdit TxtOpenQuantity;

	private Label LblGhramaOrder;

	private Label label51;

	private ButtonEdit TxtGhramaOrder;

	private Label LblVatNum;

	private Label label53;

	private ButtonEdit TxtVatNum;

	private Label LblCostOrder;

	private Label label55;

	private ButtonEdit TxtCostOrder;

	private Label LblMawadAwalyaa;

	private Label LblEntagTam;

	private Label label58;

	private Label label59;

	private ButtonEdit TxtMawadAwalyaa;

	private Label label60;

	private Label label61;

	private ButtonEdit TxtEntagTam;

	private Label LblRun;

	private Label label63;

	private ButtonEdit TxtRun;

	private Label LblZmmEmployee;

	private Label label65;

	private ButtonEdit TxtZmmEmployee;

	private SimpleButton BtnSave;

	private SimpleButton BtnExit;

	private System.Windows.Forms.ComboBox TxtStore2;

	private System.Windows.Forms.ComboBox TxtStoreMain;

	public FrmDefualtAccount()
	{
		InitializeComponent();
		BindFrm(Branch());
		BindStores();
		BindTxtBox();
		if (LoginDetails.CloseYear)
		{
			CloseYear.HideButtons(BtnSave);
		}
	}

	private int Branch()
	{
		return (from zz in DFLTAccNT.GetDefualtAccount()
			where zz.ID > 0
			select zz).FirstOrDefault().ID;
	}

	private void BindFrm(int Code)
	{
		Account_Branch Itm = (from zz in DFLTAccNT.GetBranches()
			where zz.ID == Code
			select zz).FirstOrDefault();
		Account_DefualtAccount account_DefualtAccount = (from zz in DFLTAccNT.GetDefualtAccount()
			where zz.ID == Itm.ID
			select zz).FirstOrDefault();
		TxtNameBranch.Text = Itm.Name;
		LblBranches.Text = Itm.ID.ToString();
	}

	private void BindTxtBox()
	{
		Account_DefualtAccount account_DefualtAccount = (from x in DFLTAccNT.GetDefualtAccount()
			where x.BranchID == Convert.ToInt32(LblBranches.Text)
			select x).FirstOrDefault();
		if (account_DefualtAccount != null)
		{
			TxtStoreMain.SelectedValue = account_DefualtAccount.Acc_StoreMain;
			TxtStoreMain.Text = STR.GetName(Convert.ToInt32(account_DefualtAccount.Acc_StoreMain));
			TxtStore2.SelectedValue = account_DefualtAccount.Acc_StoreWaiting;
			TxtStore2.Text = STR.GetName(Convert.ToInt32(account_DefualtAccount.Acc_StoreWaiting));
			LblCostOrder.Text = account_DefualtAccount.Acc_CostItemOrder.ToString();
			TxtCostOrder.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_CostItemOrder));
			LblVatNum.Text = account_DefualtAccount.Acc_VatNum.ToString();
			TxtVatNum.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_VatNum));
			LblBoxCash.Text = account_DefualtAccount.Acc_CashBox.ToString();
			TxtBoxCash.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_CashBox));
			LblOrderCash.Text = account_DefualtAccount.Acc_OrderCash.ToString();
			TxtOrderCash.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_OrderCash));
			LblOrderAjil.Text = account_DefualtAccount.Acc_OrderAjila.ToString();
			TxtOrderAjil.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_OrderAjila));
			LblReturnOrderCash.Text = account_DefualtAccount.Acc_OrderReturnCash.ToString();
			TxtReturnOrderCash.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_OrderReturnCash));
			LblBankSraf.Text = account_DefualtAccount.Acc_BankNumCachir.ToString();
			TxtBankSraf.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_BankNumCachir));
			LblCustomerCash.Text = account_DefualtAccount.Acc_CustomerCash.ToString();
			TxtCustomerCash.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_CustomerCash));
			LblSupplierCash.Text = account_DefualtAccount.Acc_supplierCash.ToString();
			TxtSupplierCash.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.Acc_supplierCash));
			LblEntagTam.Text = account_DefualtAccount.StoreEntagTam.ToString();
			TxtEntagTam.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.StoreEntagTam));
			LblMawadAwalyaa.Text = account_DefualtAccount.StoreMawadAwalyaa.ToString();
			TxtMawadAwalyaa.Text = DFLTAccNT.GetAccountName(Convert.ToInt32(account_DefualtAccount.StoreMawadAwalyaa));
		}
	}

	private void BindStores()
	{
		TxtStoreMain.DataSource = STR.GetAll();
		TxtStoreMain.DisplayMember = "Store_Name";
		TxtStoreMain.ValueMember = "ID";
		TxtStore2.DataSource = STR.GetAll();
		TxtStore2.DisplayMember = "Store_Name";
		TxtStore2.ValueMember = "ID";
	}

	private void Clear()
	{
		TxtStoreMain.Text = "";
		TxtStore2.Text = "";
		LblCostOrder.Text = "1";
		TxtCostOrder.Text = "";
		LblVatNum.Text = "1";
		TxtVatNum.Text = "";
		LblBoxCash.Text = "1";
		TxtBoxCash.Text = "";
		LblOrderCash.Text = "1";
		TxtOrderCash.Text = "";
		LblOrderAjil.Text = "1";
		TxtOrderAjil.Text = "";
		LblReturnOrderCash.Text = "1";
		TxtReturnOrderCash.Text = "";
		LblBankSraf.Text = "1";
		TxtBankSraf.Text = "";
		LblCustomerCash.Text = "1";
		TxtCustomerCash.Text = "";
		LblSupplierCash.Text = "1";
		TxtSupplierCash.Text = "";
		TxtArbah77.Text = "";
		TxtMainMoney.Text = "";
		TxtCloseRun.Text = "";
		TxtMasareef.Text = "";
		TxtZmmEmployee.Text = "";
		TxtRun.Text = "";
		TxtEntagTam.Text = "";
		TxtMawadAwalyaa.Text = "";
		TxtGhramaOrder.Text = "";
		TxtOpenQuantity.Text = "";
		TxtGardPlus.Text = "";
		TxtGardMinus.Text = "";
		TxtZmmCustomer.Text = "";
		TxtZmmSuppliers.Text = "";
		TxtReturnOrderAjil.Text = "";
	}

	private void TxtNameBranch_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchBranch frmSearchBranch = new FrmSearchBranch();
			frmSearchBranch.ShowDialog();
			if (frmSearchBranch.CustCode > 0)
			{
				Clear();
				LblBranches.Text = "0";
				int FrmID = frmSearchBranch.CustCode;
				Account_Branch account_Branch = (from x in DFLTAccNT.GetBranches()
					where x.ID == FrmID
					select x).FirstOrDefault();
				LblBranches.Text = FrmID.ToString();
				TxtNameBranch.Text = account_Branch.Name;
			}
			else
			{
				TxtNameBranch.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtZmmCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblZmmCustomer.Text = custCode.ToString();
				TxtZmmCustomer.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtZmmCustomer.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtZmmSuppliers_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblZmmSuppliers.Text = custCode.ToString();
				TxtZmmSuppliers.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtZmmSuppliers.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtCustomerCash_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblCustomerCash.Text = custCode.ToString();
				TxtCustomerCash.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtCustomerCash.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtSupplierCash_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblSupplierCash.Text = custCode.ToString();
				TxtSupplierCash.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtSupplierCash.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtOrderCash_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblOrderCash.Text = custCode.ToString();
				TxtOrderCash.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtOrderCash.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtOrderAjil_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblOrderAjil.Text = custCode.ToString();
				TxtOrderAjil.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtOrderAjil.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtReturnOrderCash_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblReturnOrderCash.Text = custCode.ToString();
				TxtReturnOrderCash.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtReturnOrderCash.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtReturnOrderAjil_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblReturnOrderAjil.Text = custCode.ToString();
				TxtReturnOrderAjil.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtReturnOrderAjil.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtMainBox_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblMainBox.Text = custCode.ToString();
				TxtMainBox.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtMainBox.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtMainBank_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblMainBank.Text = custCode.ToString();
				TxtMainBank.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtMainBank.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtBoxCash_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblBoxCash.Text = custCode.ToString();
				TxtBoxCash.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtBoxCash.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtBankSraf_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblBankSraf.Text = custCode.ToString();
				TxtBankSraf.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtBankSraf.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtArbah77_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblArbah77.Text = custCode.ToString();
				TxtArbah77.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtArbah77.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtMainMoney_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblMainMoney.Text = custCode.ToString();
				TxtMainMoney.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtMainMoney.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtCloseRun_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblCloseRun.Text = custCode.ToString();
				TxtCloseRun.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtCloseRun.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtMasareef_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblMasareef.Text = custCode.ToString();
				TxtMasareef.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtMasareef.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtZmmEmployee_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblZmmEmployee.Text = custCode.ToString();
				TxtZmmEmployee.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtZmmEmployee.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtRun_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblRun.Text = custCode.ToString();
				TxtRun.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtRun.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtEntagTam_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblEntagTam.Text = custCode.ToString();
				TxtEntagTam.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtEntagTam.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtMawadAwalyaa_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblMawadAwalyaa.Text = custCode.ToString();
				TxtMawadAwalyaa.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtMawadAwalyaa.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtCostOrder_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblCostOrder.Text = custCode.ToString();
				TxtCostOrder.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtCostOrder.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtVatNum_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblVatNum.Text = custCode.ToString();
				TxtVatNum.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtVatNum.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtGhramaOrder_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblGhramaOrder.Text = custCode.ToString();
				TxtGhramaOrder.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtGhramaOrder.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtOpenQuantity_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblOpenQuantity.Text = custCode.ToString();
				TxtOpenQuantity.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtOpenQuantity.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtGardPlus_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblGardPlus.Text = custCode.ToString();
				TxtGardPlus.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtGardPlus.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void TxtGardMinus_ButtonClick(object sender, ButtonPressedEventArgs e)
	{
		try
		{
			FrmSearchAccount frmSearchAccount = new FrmSearchAccount();
			frmSearchAccount.ShowDialog();
			if (frmSearchAccount.CustCode > 0)
			{
				int custCode = frmSearchAccount.CustCode;
				LblGardMinus.Text = custCode.ToString();
				TxtGardMinus.Text = frmSearchAccount.AccountName;
			}
			else
			{
				TxtGardMinus.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		if (!CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 39))
		{
			MessageBox.Show("عفوا، لا تملك صلاحية حفظ إعدادات الحسابات الافتراضية.",
				"مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK,
				MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
				MessageBoxOptions.RightAlign);
			return;
		}

		try
		{
			int branchID = Convert.ToInt32(LblBranches.Text);
			if (TxtStoreMain.SelectedValue == null || TxtStore2.SelectedValue == null)
				throw new InvalidOperationException("اختر المستودع الرئيسي ومستودع الحجز.");

			int storeMain = Convert.ToInt32(TxtStoreMain.SelectedValue);
			int storeWaiting = Convert.ToInt32(TxtStore2.SelectedValue);
			if (storeMain == storeWaiting)
				throw new InvalidOperationException("لا يمكن اختيار المستودع نفسه للمستودع الرئيسي ومستودع الحجز.");

			var defaults = new Account_DefualtAccount
			{
				BranchID = branchID,
				Acc_CostItemOrder = ReadAccountID(LblCostOrder, "تكلفة الطلب"),
				Acc_VatNum = ReadAccountID(LblVatNum, "ضريبة القيمة المضافة"),
				Acc_CashBox = ReadAccountID(LblBoxCash, "الصندوق النقدي"),
				Acc_OrderCash = ReadAccountID(LblOrderCash, "المبيعات النقدية"),
				Acc_OrderAjila = ReadAccountID(LblOrderAjil, "المبيعات الآجلة"),
				Acc_OrderReturnCash = ReadAccountID(LblReturnOrderCash, "مرتجعات المبيعات النقدية"),
				Acc_BankNumCachir = ReadAccountID(LblBankSraf, "حساب البنك"),
				Acc_CustomerCash = ReadAccountID(LblCustomerCash, "العملاء النقديون"),
				Acc_supplierCash = ReadAccountID(LblSupplierCash, "الموردون النقديون"),
				StoreEntagTam = ReadAccountID(LblEntagTam, "الإنتاج التام"),
				StoreMawadAwalyaa = ReadAccountID(LblMawadAwalyaa, "المواد الأولية")
			};

			int result = DFLTAccNT.UpdateBranchDefaults(
				branchID, defaults, storeMain, storeWaiting);
			if (result <= 0)
				throw new InvalidOperationException("لم يتم العثور على إعدادات الحسابات لهذا الفرع.");

			MessageBox.Show(
				"تم حفظ إعدادات الحسابات الافتراضية للفرع ( " + TxtNameBranch.Text + " ) بنجاح.",
				"نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
		}
		catch (Exception ex)
		{
			MessageBox.Show(
				"تعذر حفظ إعدادات الحسابات الافتراضية للفرع ( " +
				TxtNameBranch.Text + " )." + Environment.NewLine + ex.Message,
				"فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
		}
	}

	private static int? ReadAccountID(Label codeLabel, string accountName)
	{
		if (string.IsNullOrWhiteSpace(codeLabel.Text))
			return null;

		if (!int.TryParse(codeLabel.Text, out int accountID) || accountID <= 0)
			throw new InvalidOperationException("اختر حساباً صالحاً لـ" + accountName + ".");

		return accountID;
	}

	private void BtnExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CombName_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void Control_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void LblBranches_TextChanged(object sender, EventArgs e)
	{
		try
		{
			BindTxtBox();
		}
		catch (Exception)
		{
			Clear();
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
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions12 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions13 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions14 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions15 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions16 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions17 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions18 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTSErpSystem.Frms.FrmsMain.FrmDefualtAccount));
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions19 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions20 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions21 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions22 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions23 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions24 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions25 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions26 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions27 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.LblBranches = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.TxtNameBranch = new DevExpress.XtraEditors.ButtonEdit();
		this.separatorControl6 = new DevExpress.XtraEditors.SeparatorControl();
		this.lblStoreMain = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.LblStore2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.LblZmmSuppliers = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.TxtZmmSuppliers = new DevExpress.XtraEditors.ButtonEdit();
		this.LblZmmCustomer = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.TxtZmmCustomer = new DevExpress.XtraEditors.ButtonEdit();
		this.LblSupplierCash = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.TxtSupplierCash = new DevExpress.XtraEditors.ButtonEdit();
		this.LblCustomerCash = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.TxtCustomerCash = new DevExpress.XtraEditors.ButtonEdit();
		this.LblOrderAjil = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.TxtOrderAjil = new DevExpress.XtraEditors.ButtonEdit();
		this.LblOrderCash = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.TxtOrderCash = new DevExpress.XtraEditors.ButtonEdit();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.TxtReturnOrderAjil = new DevExpress.XtraEditors.ButtonEdit();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.TxtReturnOrderCash = new DevExpress.XtraEditors.ButtonEdit();
		this.LblMasareef = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.TxtMasareef = new DevExpress.XtraEditors.ButtonEdit();
		this.LblCloseRun = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.TxtCloseRun = new DevExpress.XtraEditors.ButtonEdit();
		this.LblMainMoney = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.TxtMainMoney = new DevExpress.XtraEditors.ButtonEdit();
		this.LblArbah77 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.TxtArbah77 = new DevExpress.XtraEditors.ButtonEdit();
		this.LblBankSraf = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.TxtBankSraf = new DevExpress.XtraEditors.ButtonEdit();
		this.LblBoxCash = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.TxtBoxCash = new DevExpress.XtraEditors.ButtonEdit();
		this.LblMainBank = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.TxtMainBank = new DevExpress.XtraEditors.ButtonEdit();
		this.LblMainBox = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.TxtMainBox = new DevExpress.XtraEditors.ButtonEdit();
		this.LblReturnOrderAjil = new System.Windows.Forms.Label();
		this.LblReturnOrderCash = new System.Windows.Forms.Label();
		this.LblGardMinus = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.TxtGardMinus = new DevExpress.XtraEditors.ButtonEdit();
		this.LblGardPlus = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.TxtGardPlus = new DevExpress.XtraEditors.ButtonEdit();
		this.LblOpenQuantity = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.TxtOpenQuantity = new DevExpress.XtraEditors.ButtonEdit();
		this.LblGhramaOrder = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.TxtGhramaOrder = new DevExpress.XtraEditors.ButtonEdit();
		this.LblVatNum = new System.Windows.Forms.Label();
		this.label53 = new System.Windows.Forms.Label();
		this.TxtVatNum = new DevExpress.XtraEditors.ButtonEdit();
		this.LblCostOrder = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.TxtCostOrder = new DevExpress.XtraEditors.ButtonEdit();
		this.LblMawadAwalyaa = new System.Windows.Forms.Label();
		this.LblEntagTam = new System.Windows.Forms.Label();
		this.label58 = new System.Windows.Forms.Label();
		this.label59 = new System.Windows.Forms.Label();
		this.TxtMawadAwalyaa = new DevExpress.XtraEditors.ButtonEdit();
		this.label60 = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.TxtEntagTam = new DevExpress.XtraEditors.ButtonEdit();
		this.LblRun = new System.Windows.Forms.Label();
		this.label63 = new System.Windows.Forms.Label();
		this.TxtRun = new DevExpress.XtraEditors.ButtonEdit();
		this.LblZmmEmployee = new System.Windows.Forms.Label();
		this.label65 = new System.Windows.Forms.Label();
		this.TxtZmmEmployee = new DevExpress.XtraEditors.ButtonEdit();
		this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
		this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
		this.TxtStoreMain = new System.Windows.Forms.ComboBox();
		this.TxtStore2 = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameBranch.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmSuppliers.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmCustomer.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSupplierCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustomerCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOrderAjil.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOrderCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturnOrderAjil.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturnOrderCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMasareef.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCloseRun.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainMoney.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtArbah77.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBankSraf.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBoxCash.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainBank.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainBox.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGardMinus.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGardPlus.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOpenQuantity.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGhramaOrder.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVatNum.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCostOrder.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMawadAwalyaa.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEntagTam.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRun.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmEmployee.Properties).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TxtStore2);
		this.groupBox1.Controls.Add(this.TxtStoreMain);
		this.groupBox1.Controls.Add(this.LblGardMinus);
		this.groupBox1.Controls.Add(this.label45);
		this.groupBox1.Controls.Add(this.TxtGardMinus);
		this.groupBox1.Controls.Add(this.LblGardPlus);
		this.groupBox1.Controls.Add(this.label47);
		this.groupBox1.Controls.Add(this.TxtGardPlus);
		this.groupBox1.Controls.Add(this.LblOpenQuantity);
		this.groupBox1.Controls.Add(this.label49);
		this.groupBox1.Controls.Add(this.TxtOpenQuantity);
		this.groupBox1.Controls.Add(this.LblGhramaOrder);
		this.groupBox1.Controls.Add(this.label51);
		this.groupBox1.Controls.Add(this.TxtGhramaOrder);
		this.groupBox1.Controls.Add(this.LblVatNum);
		this.groupBox1.Controls.Add(this.label53);
		this.groupBox1.Controls.Add(this.TxtVatNum);
		this.groupBox1.Controls.Add(this.LblCostOrder);
		this.groupBox1.Controls.Add(this.label55);
		this.groupBox1.Controls.Add(this.TxtCostOrder);
		this.groupBox1.Controls.Add(this.LblMawadAwalyaa);
		this.groupBox1.Controls.Add(this.LblEntagTam);
		this.groupBox1.Controls.Add(this.label58);
		this.groupBox1.Controls.Add(this.label59);
		this.groupBox1.Controls.Add(this.TxtMawadAwalyaa);
		this.groupBox1.Controls.Add(this.label60);
		this.groupBox1.Controls.Add(this.label61);
		this.groupBox1.Controls.Add(this.TxtEntagTam);
		this.groupBox1.Controls.Add(this.LblRun);
		this.groupBox1.Controls.Add(this.label63);
		this.groupBox1.Controls.Add(this.TxtRun);
		this.groupBox1.Controls.Add(this.LblZmmEmployee);
		this.groupBox1.Controls.Add(this.label65);
		this.groupBox1.Controls.Add(this.TxtZmmEmployee);
		this.groupBox1.Controls.Add(this.LblMasareef);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.TxtMasareef);
		this.groupBox1.Controls.Add(this.LblCloseRun);
		this.groupBox1.Controls.Add(this.label25);
		this.groupBox1.Controls.Add(this.TxtCloseRun);
		this.groupBox1.Controls.Add(this.LblMainMoney);
		this.groupBox1.Controls.Add(this.label27);
		this.groupBox1.Controls.Add(this.TxtMainMoney);
		this.groupBox1.Controls.Add(this.LblArbah77);
		this.groupBox1.Controls.Add(this.label29);
		this.groupBox1.Controls.Add(this.TxtArbah77);
		this.groupBox1.Controls.Add(this.LblBankSraf);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.TxtBankSraf);
		this.groupBox1.Controls.Add(this.LblBoxCash);
		this.groupBox1.Controls.Add(this.label33);
		this.groupBox1.Controls.Add(this.TxtBoxCash);
		this.groupBox1.Controls.Add(this.LblMainBank);
		this.groupBox1.Controls.Add(this.label35);
		this.groupBox1.Controls.Add(this.TxtMainBank);
		this.groupBox1.Controls.Add(this.LblMainBox);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.TxtMainBox);
		this.groupBox1.Controls.Add(this.LblReturnOrderAjil);
		this.groupBox1.Controls.Add(this.LblReturnOrderCash);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.TxtReturnOrderAjil);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.TxtReturnOrderCash);
		this.groupBox1.Controls.Add(this.LblOrderAjil);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.TxtOrderAjil);
		this.groupBox1.Controls.Add(this.LblOrderCash);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.TxtOrderCash);
		this.groupBox1.Controls.Add(this.LblSupplierCash);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.TxtSupplierCash);
		this.groupBox1.Controls.Add(this.LblCustomerCash);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.TxtCustomerCash);
		this.groupBox1.Controls.Add(this.LblZmmSuppliers);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.TxtZmmSuppliers);
		this.groupBox1.Controls.Add(this.LblZmmCustomer);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.TxtZmmCustomer);
		this.groupBox1.Controls.Add(this.LblStore2);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.lblStoreMain);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.separatorControl6);
		this.groupBox1.Controls.Add(this.LblBranches);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.TxtNameBranch);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1228, 648);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.LblBranches.AutoSize = true;
		this.LblBranches.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblBranches.Location = new System.Drawing.Point(1204, 29);
		this.LblBranches.Name = "LblBranches";
		this.LblBranches.Size = new System.Drawing.Size(18, 19);
		this.LblBranches.TabIndex = 178;
		this.LblBranches.Text = "1";
		this.LblBranches.TextChanged += new System.EventHandler(LblBranches_TextChanged);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label6.Location = new System.Drawing.Point(1145, 28);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(43, 19);
		this.label6.TabIndex = 173;
		this.label6.Text = "الفرع";
		this.TxtNameBranch.Location = new System.Drawing.Point(15, 22);
		this.TxtNameBranch.Name = "TxtNameBranch";
		this.TxtNameBranch.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtNameBranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13.5f);
		this.TxtNameBranch.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
		this.TxtNameBranch.Properties.Appearance.Options.UseBackColor = true;
		this.TxtNameBranch.Properties.Appearance.Options.UseFont = true;
		this.TxtNameBranch.Properties.Appearance.Options.UseForeColor = true;
		this.TxtNameBranch.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtNameBranch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtNameBranch.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtNameBranch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions26.Image");
		this.TxtNameBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtNameBranch.Properties.ReadOnly = true;
		this.TxtNameBranch.Size = new System.Drawing.Size(1124, 36);
		this.TxtNameBranch.TabIndex = 0;
		this.TxtNameBranch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtNameBranch_ButtonClick);
		this.TxtNameBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.separatorControl6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.separatorControl6.LineColor = System.Drawing.Color.RoyalBlue;
		this.separatorControl6.LineThickness = 1;
		this.separatorControl6.Location = new System.Drawing.Point(6, 59);
		this.separatorControl6.Name = "separatorControl6";
		this.separatorControl6.Size = new System.Drawing.Size(1222, 19);
		this.separatorControl6.TabIndex = 179;
		this.lblStoreMain.AutoSize = true;
		this.lblStoreMain.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.lblStoreMain.Location = new System.Drawing.Point(1204, 102);
		this.lblStoreMain.Name = "lblStoreMain";
		this.lblStoreMain.Size = new System.Drawing.Size(18, 19);
		this.lblStoreMain.TabIndex = 182;
		this.lblStoreMain.Text = "1";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label2.Location = new System.Drawing.Point(1052, 101);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(139, 19);
		this.label2.TabIndex = 181;
		this.label2.Text = "المستودع الرئيسي";
		this.LblStore2.AutoSize = true;
		this.LblStore2.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblStore2.Location = new System.Drawing.Point(590, 102);
		this.LblStore2.Name = "LblStore2";
		this.LblStore2.Size = new System.Drawing.Size(18, 19);
		this.LblStore2.TabIndex = 185;
		this.LblStore2.Text = "1";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label4.Location = new System.Drawing.Point(438, 102);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(107, 19);
		this.label4.TabIndex = 184;
		this.label4.Text = "مستودع الحجز";
		this.LblZmmSuppliers.AutoSize = true;
		this.LblZmmSuppliers.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblZmmSuppliers.Location = new System.Drawing.Point(590, 141);
		this.LblZmmSuppliers.Name = "LblZmmSuppliers";
		this.LblZmmSuppliers.Size = new System.Drawing.Size(18, 19);
		this.LblZmmSuppliers.TabIndex = 191;
		this.LblZmmSuppliers.Text = "1";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label7.Location = new System.Drawing.Point(438, 140);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(95, 19);
		this.label7.TabIndex = 190;
		this.label7.Text = "ذمم الموردين";
		this.TxtZmmSuppliers.Location = new System.Drawing.Point(15, 136);
		this.TxtZmmSuppliers.Name = "TxtZmmSuppliers";
		this.TxtZmmSuppliers.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtZmmSuppliers.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtZmmSuppliers.Properties.Appearance.Options.UseBackColor = true;
		this.TxtZmmSuppliers.Properties.Appearance.Options.UseFont = true;
		this.TxtZmmSuppliers.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtZmmSuppliers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtZmmSuppliers.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtZmmSuppliers.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions2.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions24.Image");
		this.TxtZmmSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions2, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtZmmSuppliers.Properties.ReadOnly = true;
		this.TxtZmmSuppliers.Size = new System.Drawing.Size(417, 30);
		this.TxtZmmSuppliers.TabIndex = 4;
		this.TxtZmmSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtZmmSuppliers_ButtonClick);
		this.TxtZmmSuppliers.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblZmmCustomer.AutoSize = true;
		this.LblZmmCustomer.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblZmmCustomer.Location = new System.Drawing.Point(1204, 141);
		this.LblZmmCustomer.Name = "LblZmmCustomer";
		this.LblZmmCustomer.Size = new System.Drawing.Size(18, 19);
		this.LblZmmCustomer.TabIndex = 188;
		this.LblZmmCustomer.Text = "1";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label9.Location = new System.Drawing.Point(1052, 140);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(84, 19);
		this.label9.TabIndex = 187;
		this.label9.Text = "ذمم العملاء";
		this.TxtZmmCustomer.Location = new System.Drawing.Point(629, 136);
		this.TxtZmmCustomer.Name = "TxtZmmCustomer";
		this.TxtZmmCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtZmmCustomer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtZmmCustomer.Properties.Appearance.Options.UseBackColor = true;
		this.TxtZmmCustomer.Properties.Appearance.Options.UseFont = true;
		this.TxtZmmCustomer.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtZmmCustomer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtZmmCustomer.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtZmmCustomer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions3.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions25.Image");
		this.TxtZmmCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions3, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtZmmCustomer.Properties.ReadOnly = true;
		this.TxtZmmCustomer.Size = new System.Drawing.Size(417, 30);
		this.TxtZmmCustomer.TabIndex = 3;
		this.TxtZmmCustomer.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtZmmCustomer_ButtonClick);
		this.TxtZmmCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblSupplierCash.AutoSize = true;
		this.LblSupplierCash.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblSupplierCash.Location = new System.Drawing.Point(590, 180);
		this.LblSupplierCash.Name = "LblSupplierCash";
		this.LblSupplierCash.Size = new System.Drawing.Size(18, 19);
		this.LblSupplierCash.TabIndex = 197;
		this.LblSupplierCash.Text = "1";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label11.Location = new System.Drawing.Point(438, 179);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(99, 19);
		this.label11.TabIndex = 196;
		this.label11.Text = "المورد النقدي";
		this.TxtSupplierCash.Location = new System.Drawing.Point(15, 175);
		this.TxtSupplierCash.Name = "TxtSupplierCash";
		this.TxtSupplierCash.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtSupplierCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtSupplierCash.Properties.Appearance.Options.UseBackColor = true;
		this.TxtSupplierCash.Properties.Appearance.Options.UseFont = true;
		this.TxtSupplierCash.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtSupplierCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtSupplierCash.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtSupplierCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions4.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions22.Image");
		this.TxtSupplierCash.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions4, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtSupplierCash.Properties.ReadOnly = true;
		this.TxtSupplierCash.Size = new System.Drawing.Size(417, 30);
		this.TxtSupplierCash.TabIndex = 6;
		this.TxtSupplierCash.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtSupplierCash_ButtonClick);
		this.TxtSupplierCash.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblCustomerCash.AutoSize = true;
		this.LblCustomerCash.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblCustomerCash.Location = new System.Drawing.Point(1204, 180);
		this.LblCustomerCash.Name = "LblCustomerCash";
		this.LblCustomerCash.Size = new System.Drawing.Size(18, 19);
		this.LblCustomerCash.TabIndex = 194;
		this.LblCustomerCash.Text = "1";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label13.Location = new System.Drawing.Point(1052, 179);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(105, 19);
		this.label13.TabIndex = 193;
		this.label13.Text = "العميل النقدي";
		this.TxtCustomerCash.Location = new System.Drawing.Point(629, 175);
		this.TxtCustomerCash.Name = "TxtCustomerCash";
		this.TxtCustomerCash.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCustomerCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCustomerCash.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCustomerCash.Properties.Appearance.Options.UseFont = true;
		this.TxtCustomerCash.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCustomerCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCustomerCash.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCustomerCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions5.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions23.Image");
		this.TxtCustomerCash.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions5, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtCustomerCash.Properties.ReadOnly = true;
		this.TxtCustomerCash.Size = new System.Drawing.Size(417, 30);
		this.TxtCustomerCash.TabIndex = 5;
		this.TxtCustomerCash.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCustomerCash_ButtonClick);
		this.TxtCustomerCash.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblOrderAjil.AutoSize = true;
		this.LblOrderAjil.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblOrderAjil.Location = new System.Drawing.Point(590, 219);
		this.LblOrderAjil.Name = "LblOrderAjil";
		this.LblOrderAjil.Size = new System.Drawing.Size(18, 19);
		this.LblOrderAjil.TabIndex = 203;
		this.LblOrderAjil.Text = "1";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label15.Location = new System.Drawing.Point(438, 218);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(111, 19);
		this.label15.TabIndex = 202;
		this.label15.Text = "المبيعات الاجلة";
		this.TxtOrderAjil.Location = new System.Drawing.Point(15, 214);
		this.TxtOrderAjil.Name = "TxtOrderAjil";
		this.TxtOrderAjil.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtOrderAjil.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderAjil.Properties.Appearance.Options.UseBackColor = true;
		this.TxtOrderAjil.Properties.Appearance.Options.UseFont = true;
		this.TxtOrderAjil.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtOrderAjil.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtOrderAjil.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtOrderAjil.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions6.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions20.Image");
		this.TxtOrderAjil.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions6, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtOrderAjil.Properties.ReadOnly = true;
		this.TxtOrderAjil.Size = new System.Drawing.Size(417, 30);
		this.TxtOrderAjil.TabIndex = 8;
		this.TxtOrderAjil.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtOrderAjil_ButtonClick);
		this.TxtOrderAjil.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblOrderCash.AutoSize = true;
		this.LblOrderCash.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblOrderCash.Location = new System.Drawing.Point(1204, 219);
		this.LblOrderCash.Name = "LblOrderCash";
		this.LblOrderCash.Size = new System.Drawing.Size(18, 19);
		this.LblOrderCash.TabIndex = 200;
		this.LblOrderCash.Text = "1";
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label17.Location = new System.Drawing.Point(1052, 218);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(116, 19);
		this.label17.TabIndex = 199;
		this.label17.Text = "المبيعات النقدية";
		this.TxtOrderCash.Location = new System.Drawing.Point(629, 214);
		this.TxtOrderCash.Name = "TxtOrderCash";
		this.TxtOrderCash.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtOrderCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOrderCash.Properties.Appearance.Options.UseBackColor = true;
		this.TxtOrderCash.Properties.Appearance.Options.UseFont = true;
		this.TxtOrderCash.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtOrderCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtOrderCash.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtOrderCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions7.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions21.Image");
		this.TxtOrderCash.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions7, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtOrderCash.Properties.ReadOnly = true;
		this.TxtOrderCash.Size = new System.Drawing.Size(417, 30);
		this.TxtOrderCash.TabIndex = 7;
		this.TxtOrderCash.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtOrderCash_ButtonClick);
		this.TxtOrderCash.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label18.Location = new System.Drawing.Point(590, 258);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(18, 19);
		this.label18.TabIndex = 209;
		this.label18.Text = "1";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label19.Location = new System.Drawing.Point(438, 257);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(130, 19);
		this.label19.TabIndex = 208;
		this.label19.Text = "ظ…. المبيعات الاجلة";
		this.TxtReturnOrderAjil.Location = new System.Drawing.Point(15, 253);
		this.TxtReturnOrderAjil.Name = "TxtReturnOrderAjil";
		this.TxtReturnOrderAjil.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtReturnOrderAjil.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReturnOrderAjil.Properties.Appearance.Options.UseBackColor = true;
		this.TxtReturnOrderAjil.Properties.Appearance.Options.UseFont = true;
		this.TxtReturnOrderAjil.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtReturnOrderAjil.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtReturnOrderAjil.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtReturnOrderAjil.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions8.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions18.Image");
		this.TxtReturnOrderAjil.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions8, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtReturnOrderAjil.Properties.ReadOnly = true;
		this.TxtReturnOrderAjil.Size = new System.Drawing.Size(417, 30);
		this.TxtReturnOrderAjil.TabIndex = 10;
		this.TxtReturnOrderAjil.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtReturnOrderAjil_ButtonClick);
		this.TxtReturnOrderAjil.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label20.Location = new System.Drawing.Point(1204, 258);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(18, 19);
		this.label20.TabIndex = 206;
		this.label20.Text = "1";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label21.Location = new System.Drawing.Point(1052, 257);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(135, 19);
		this.label21.TabIndex = 205;
		this.label21.Text = "ظ…. المبيعات النقدية";
		this.TxtReturnOrderCash.Location = new System.Drawing.Point(629, 253);
		this.TxtReturnOrderCash.Name = "TxtReturnOrderCash";
		this.TxtReturnOrderCash.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtReturnOrderCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtReturnOrderCash.Properties.Appearance.Options.UseBackColor = true;
		this.TxtReturnOrderCash.Properties.Appearance.Options.UseFont = true;
		this.TxtReturnOrderCash.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtReturnOrderCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtReturnOrderCash.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtReturnOrderCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions9.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions19.Image");
		this.TxtReturnOrderCash.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions9, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtReturnOrderCash.Properties.ReadOnly = true;
		this.TxtReturnOrderCash.Size = new System.Drawing.Size(417, 30);
		this.TxtReturnOrderCash.TabIndex = 9;
		this.TxtReturnOrderCash.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtReturnOrderCash_ButtonClick);
		this.TxtReturnOrderCash.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblMasareef.AutoSize = true;
		this.LblMasareef.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblMasareef.Location = new System.Drawing.Point(590, 415);
		this.LblMasareef.Name = "LblMasareef";
		this.LblMasareef.Size = new System.Drawing.Size(18, 19);
		this.LblMasareef.TabIndex = 239;
		this.LblMasareef.Text = "1";
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label23.Location = new System.Drawing.Point(438, 414);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(121, 19);
		this.label23.TabIndex = 238;
		this.label23.Text = "حساب المصاريف";
		this.TxtMasareef.Location = new System.Drawing.Point(15, 410);
		this.TxtMasareef.Name = "TxtMasareef";
		this.TxtMasareef.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtMasareef.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMasareef.Properties.Appearance.Options.UseBackColor = true;
		this.TxtMasareef.Properties.Appearance.Options.UseFont = true;
		this.TxtMasareef.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMasareef.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMasareef.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtMasareef.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions10.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions10.Image");
		this.TxtMasareef.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions10, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtMasareef.Properties.ReadOnly = true;
		this.TxtMasareef.Size = new System.Drawing.Size(417, 30);
		this.TxtMasareef.TabIndex = 18;
		this.TxtMasareef.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtMasareef_ButtonClick);
		this.TxtMasareef.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblCloseRun.AutoSize = true;
		this.LblCloseRun.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblCloseRun.Location = new System.Drawing.Point(1204, 415);
		this.LblCloseRun.Name = "LblCloseRun";
		this.LblCloseRun.Size = new System.Drawing.Size(18, 19);
		this.LblCloseRun.TabIndex = 236;
		this.LblCloseRun.Text = "1";
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label25.Location = new System.Drawing.Point(1052, 414);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(130, 19);
		this.label25.TabIndex = 235;
		this.label25.Text = "اقفال امر التشغيل";
		this.TxtCloseRun.Location = new System.Drawing.Point(629, 410);
		this.TxtCloseRun.Name = "TxtCloseRun";
		this.TxtCloseRun.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCloseRun.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCloseRun.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCloseRun.Properties.Appearance.Options.UseFont = true;
		this.TxtCloseRun.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCloseRun.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCloseRun.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCloseRun.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions11.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions11.Image");
		this.TxtCloseRun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions11, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtCloseRun.Properties.ReadOnly = true;
		this.TxtCloseRun.Size = new System.Drawing.Size(417, 30);
		this.TxtCloseRun.TabIndex = 17;
		this.TxtCloseRun.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCloseRun_ButtonClick);
		this.TxtCloseRun.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblMainMoney.AutoSize = true;
		this.LblMainMoney.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblMainMoney.Location = new System.Drawing.Point(590, 376);
		this.LblMainMoney.Name = "LblMainMoney";
		this.LblMainMoney.Size = new System.Drawing.Size(18, 19);
		this.LblMainMoney.TabIndex = 233;
		this.LblMainMoney.Text = "1";
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label27.Location = new System.Drawing.Point(438, 375);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(77, 19);
		this.label27.TabIndex = 232;
		this.label27.Text = "رأس المال";
		this.TxtMainMoney.Location = new System.Drawing.Point(15, 371);
		this.TxtMainMoney.Name = "TxtMainMoney";
		this.TxtMainMoney.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtMainMoney.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMainMoney.Properties.Appearance.Options.UseBackColor = true;
		this.TxtMainMoney.Properties.Appearance.Options.UseFont = true;
		this.TxtMainMoney.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMainMoney.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMainMoney.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtMainMoney.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions12.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions12.Image");
		this.TxtMainMoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions12, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtMainMoney.Properties.ReadOnly = true;
		this.TxtMainMoney.Size = new System.Drawing.Size(417, 30);
		this.TxtMainMoney.TabIndex = 16;
		this.TxtMainMoney.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtMainMoney_ButtonClick);
		this.TxtMainMoney.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblArbah77.AutoSize = true;
		this.LblArbah77.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblArbah77.Location = new System.Drawing.Point(1204, 376);
		this.LblArbah77.Name = "LblArbah77";
		this.LblArbah77.Size = new System.Drawing.Size(18, 19);
		this.LblArbah77.TabIndex = 230;
		this.LblArbah77.Text = "1";
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label29.Location = new System.Drawing.Point(1052, 375);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(113, 19);
		this.label29.TabIndex = 229;
		this.label29.Text = "الارباح والخسائر";
		this.TxtArbah77.Location = new System.Drawing.Point(629, 371);
		this.TxtArbah77.Name = "TxtArbah77";
		this.TxtArbah77.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtArbah77.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtArbah77.Properties.Appearance.Options.UseBackColor = true;
		this.TxtArbah77.Properties.Appearance.Options.UseFont = true;
		this.TxtArbah77.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtArbah77.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtArbah77.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtArbah77.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions13.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions13.Image");
		this.TxtArbah77.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions13, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtArbah77.Properties.ReadOnly = true;
		this.TxtArbah77.Size = new System.Drawing.Size(417, 30);
		this.TxtArbah77.TabIndex = 15;
		this.TxtArbah77.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtArbah77_ButtonClick);
		this.TxtArbah77.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblBankSraf.AutoSize = true;
		this.LblBankSraf.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblBankSraf.Location = new System.Drawing.Point(590, 337);
		this.LblBankSraf.Name = "LblBankSraf";
		this.LblBankSraf.Size = new System.Drawing.Size(18, 19);
		this.LblBankSraf.TabIndex = 227;
		this.LblBankSraf.Text = "1";
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label31.Location = new System.Drawing.Point(438, 336);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(95, 19);
		this.label31.TabIndex = 226;
		this.label31.Text = "الصراف الالي";
		this.TxtBankSraf.Location = new System.Drawing.Point(15, 332);
		this.TxtBankSraf.Name = "TxtBankSraf";
		this.TxtBankSraf.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBankSraf.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtBankSraf.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBankSraf.Properties.Appearance.Options.UseFont = true;
		this.TxtBankSraf.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBankSraf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBankSraf.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBankSraf.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions14.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions14.Image");
		this.TxtBankSraf.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions14, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtBankSraf.Properties.ReadOnly = true;
		this.TxtBankSraf.Size = new System.Drawing.Size(417, 30);
		this.TxtBankSraf.TabIndex = 14;
		this.TxtBankSraf.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBankSraf_ButtonClick);
		this.TxtBankSraf.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblBoxCash.AutoSize = true;
		this.LblBoxCash.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblBoxCash.Location = new System.Drawing.Point(1204, 337);
		this.LblBoxCash.Name = "LblBoxCash";
		this.LblBoxCash.Size = new System.Drawing.Size(18, 19);
		this.LblBoxCash.TabIndex = 224;
		this.LblBoxCash.Text = "1";
		this.label33.AutoSize = true;
		this.label33.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label33.Location = new System.Drawing.Point(1052, 336);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(117, 19);
		this.label33.TabIndex = 223;
		this.label33.Text = "الصندوق النقدي";
		this.TxtBoxCash.Location = new System.Drawing.Point(629, 332);
		this.TxtBoxCash.Name = "TxtBoxCash";
		this.TxtBoxCash.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtBoxCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtBoxCash.Properties.Appearance.Options.UseBackColor = true;
		this.TxtBoxCash.Properties.Appearance.Options.UseFont = true;
		this.TxtBoxCash.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtBoxCash.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtBoxCash.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtBoxCash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions15.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions15.Image");
		this.TxtBoxCash.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions15, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtBoxCash.Properties.ReadOnly = true;
		this.TxtBoxCash.Size = new System.Drawing.Size(417, 30);
		this.TxtBoxCash.TabIndex = 13;
		this.TxtBoxCash.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtBoxCash_ButtonClick);
		this.TxtBoxCash.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblMainBank.AutoSize = true;
		this.LblMainBank.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblMainBank.Location = new System.Drawing.Point(590, 298);
		this.LblMainBank.Name = "LblMainBank";
		this.LblMainBank.Size = new System.Drawing.Size(18, 19);
		this.LblMainBank.TabIndex = 221;
		this.LblMainBank.Text = "1";
		this.label35.AutoSize = true;
		this.label35.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label35.Location = new System.Drawing.Point(438, 297);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(49, 19);
		this.label35.TabIndex = 220;
		this.label35.Text = "البنوك";
		this.TxtMainBank.Location = new System.Drawing.Point(15, 293);
		this.TxtMainBank.Name = "TxtMainBank";
		this.TxtMainBank.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtMainBank.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMainBank.Properties.Appearance.Options.UseBackColor = true;
		this.TxtMainBank.Properties.Appearance.Options.UseFont = true;
		this.TxtMainBank.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMainBank.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMainBank.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtMainBank.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions16.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions16.Image");
		this.TxtMainBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions16, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtMainBank.Properties.ReadOnly = true;
		this.TxtMainBank.Size = new System.Drawing.Size(417, 30);
		this.TxtMainBank.TabIndex = 12;
		this.TxtMainBank.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtMainBank_ButtonClick);
		this.TxtMainBank.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblMainBox.AutoSize = true;
		this.LblMainBox.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblMainBox.Location = new System.Drawing.Point(1204, 298);
		this.LblMainBox.Name = "LblMainBox";
		this.LblMainBox.Size = new System.Drawing.Size(18, 19);
		this.LblMainBox.TabIndex = 218;
		this.LblMainBox.Text = "1";
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label37.Location = new System.Drawing.Point(1052, 297);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(71, 19);
		this.label37.TabIndex = 217;
		this.label37.Text = "الصندوق ";
		this.TxtMainBox.Location = new System.Drawing.Point(629, 293);
		this.TxtMainBox.Name = "TxtMainBox";
		this.TxtMainBox.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtMainBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMainBox.Properties.Appearance.Options.UseBackColor = true;
		this.TxtMainBox.Properties.Appearance.Options.UseFont = true;
		this.TxtMainBox.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMainBox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMainBox.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtMainBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions17.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions17.Image");
		this.TxtMainBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions17, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtMainBox.Properties.ReadOnly = true;
		this.TxtMainBox.Size = new System.Drawing.Size(417, 30);
		this.TxtMainBox.TabIndex = 11;
		this.TxtMainBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtMainBox_ButtonClick);
		this.TxtMainBox.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblReturnOrderAjil.AutoSize = true;
		this.LblReturnOrderAjil.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblReturnOrderAjil.Location = new System.Drawing.Point(590, 259);
		this.LblReturnOrderAjil.Name = "LblReturnOrderAjil";
		this.LblReturnOrderAjil.Size = new System.Drawing.Size(18, 19);
		this.LblReturnOrderAjil.TabIndex = 215;
		this.LblReturnOrderAjil.Text = "1";
		this.LblReturnOrderCash.AutoSize = true;
		this.LblReturnOrderCash.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblReturnOrderCash.Location = new System.Drawing.Point(1204, 259);
		this.LblReturnOrderCash.Name = "LblReturnOrderCash";
		this.LblReturnOrderCash.Size = new System.Drawing.Size(18, 19);
		this.LblReturnOrderCash.TabIndex = 212;
		this.LblReturnOrderCash.Text = "1";
		this.LblGardMinus.AutoSize = true;
		this.LblGardMinus.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblGardMinus.Location = new System.Drawing.Point(590, 611);
		this.LblGardMinus.Name = "LblGardMinus";
		this.LblGardMinus.Size = new System.Drawing.Size(18, 19);
		this.LblGardMinus.TabIndex = 271;
		this.LblGardMinus.Text = "1";
		this.label45.AutoSize = true;
		this.label45.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label45.Location = new System.Drawing.Point(438, 610);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(122, 19);
		this.label45.TabIndex = 270;
		this.label45.Text = "التسوية بالنقصان";
		this.TxtGardMinus.Location = new System.Drawing.Point(15, 606);
		this.TxtGardMinus.Name = "TxtGardMinus";
		this.TxtGardMinus.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtGardMinus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtGardMinus.Properties.Appearance.Options.UseBackColor = true;
		this.TxtGardMinus.Properties.Appearance.Options.UseFont = true;
		this.TxtGardMinus.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtGardMinus.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtGardMinus.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtGardMinus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions18.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions28.Image");
		this.TxtGardMinus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions18, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtGardMinus.Properties.ReadOnly = true;
		this.TxtGardMinus.Size = new System.Drawing.Size(417, 30);
		this.TxtGardMinus.TabIndex = 28;
		this.TxtGardMinus.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtGardMinus_ButtonClick);
		this.TxtGardMinus.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblGardPlus.AutoSize = true;
		this.LblGardPlus.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblGardPlus.Location = new System.Drawing.Point(1204, 611);
		this.LblGardPlus.Name = "LblGardPlus";
		this.LblGardPlus.Size = new System.Drawing.Size(18, 19);
		this.LblGardPlus.TabIndex = 268;
		this.LblGardPlus.Text = "1";
		this.label47.AutoSize = true;
		this.label47.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label47.Location = new System.Drawing.Point(1052, 610);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(111, 19);
		this.label47.TabIndex = 267;
		this.label47.Text = "التسوية بالزيادة";
		this.TxtGardPlus.Location = new System.Drawing.Point(629, 606);
		this.TxtGardPlus.Name = "TxtGardPlus";
		this.TxtGardPlus.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtGardPlus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtGardPlus.Properties.Appearance.Options.UseBackColor = true;
		this.TxtGardPlus.Properties.Appearance.Options.UseFont = true;
		this.TxtGardPlus.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtGardPlus.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtGardPlus.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtGardPlus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions19.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions1.Image");
		this.TxtGardPlus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions19, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtGardPlus.Properties.ReadOnly = true;
		this.TxtGardPlus.Size = new System.Drawing.Size(417, 30);
		this.TxtGardPlus.TabIndex = 27;
		this.TxtGardPlus.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtGardPlus_ButtonClick);
		this.TxtGardPlus.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblOpenQuantity.AutoSize = true;
		this.LblOpenQuantity.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblOpenQuantity.Location = new System.Drawing.Point(590, 572);
		this.LblOpenQuantity.Name = "LblOpenQuantity";
		this.LblOpenQuantity.Size = new System.Drawing.Size(18, 19);
		this.LblOpenQuantity.TabIndex = 265;
		this.LblOpenQuantity.Text = "1";
		this.label49.AutoSize = true;
		this.label49.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label49.Location = new System.Drawing.Point(438, 571);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(127, 19);
		this.label49.TabIndex = 264;
		this.label49.Text = "الارصدة الافتتاحية";
		this.TxtOpenQuantity.Location = new System.Drawing.Point(15, 567);
		this.TxtOpenQuantity.Name = "TxtOpenQuantity";
		this.TxtOpenQuantity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtOpenQuantity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtOpenQuantity.Properties.Appearance.Options.UseBackColor = true;
		this.TxtOpenQuantity.Properties.Appearance.Options.UseFont = true;
		this.TxtOpenQuantity.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtOpenQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtOpenQuantity.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtOpenQuantity.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions20.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions2.Image");
		this.TxtOpenQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions20, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtOpenQuantity.Properties.ReadOnly = true;
		this.TxtOpenQuantity.Size = new System.Drawing.Size(417, 30);
		this.TxtOpenQuantity.TabIndex = 26;
		this.TxtOpenQuantity.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtOpenQuantity_ButtonClick);
		this.TxtOpenQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblGhramaOrder.AutoSize = true;
		this.LblGhramaOrder.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblGhramaOrder.Location = new System.Drawing.Point(1204, 572);
		this.LblGhramaOrder.Name = "LblGhramaOrder";
		this.LblGhramaOrder.Size = new System.Drawing.Size(18, 19);
		this.LblGhramaOrder.TabIndex = 262;
		this.LblGhramaOrder.Text = "1";
		this.label51.AutoSize = true;
		this.label51.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label51.Location = new System.Drawing.Point(1052, 571);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(127, 19);
		this.label51.TabIndex = 261;
		this.label51.Text = "غرامة ظ…. المبيعات";
		this.TxtGhramaOrder.Location = new System.Drawing.Point(629, 567);
		this.TxtGhramaOrder.Name = "TxtGhramaOrder";
		this.TxtGhramaOrder.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtGhramaOrder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtGhramaOrder.Properties.Appearance.Options.UseBackColor = true;
		this.TxtGhramaOrder.Properties.Appearance.Options.UseFont = true;
		this.TxtGhramaOrder.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtGhramaOrder.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtGhramaOrder.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtGhramaOrder.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions21.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions3.Image");
		this.TxtGhramaOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions21, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtGhramaOrder.Properties.ReadOnly = true;
		this.TxtGhramaOrder.Size = new System.Drawing.Size(417, 30);
		this.TxtGhramaOrder.TabIndex = 25;
		this.TxtGhramaOrder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtGhramaOrder_ButtonClick);
		this.TxtGhramaOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblVatNum.AutoSize = true;
		this.LblVatNum.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblVatNum.Location = new System.Drawing.Point(590, 533);
		this.LblVatNum.Name = "LblVatNum";
		this.LblVatNum.Size = new System.Drawing.Size(18, 19);
		this.LblVatNum.TabIndex = 259;
		this.LblVatNum.Text = "1";
		this.label53.AutoSize = true;
		this.label53.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label53.Location = new System.Drawing.Point(438, 532);
		this.label53.Name = "label53";
		this.label53.Size = new System.Drawing.Size(151, 19);
		this.label53.TabIndex = 258;
		this.label53.Text = "ضريبة القيمة المضافة";
		this.TxtVatNum.Location = new System.Drawing.Point(15, 528);
		this.TxtVatNum.Name = "TxtVatNum";
		this.TxtVatNum.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtVatNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtVatNum.Properties.Appearance.Options.UseBackColor = true;
		this.TxtVatNum.Properties.Appearance.Options.UseFont = true;
		this.TxtVatNum.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtVatNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtVatNum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtVatNum.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions22.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions4.Image");
		this.TxtVatNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions22, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtVatNum.Properties.ReadOnly = true;
		this.TxtVatNum.Size = new System.Drawing.Size(417, 30);
		this.TxtVatNum.TabIndex = 24;
		this.TxtVatNum.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtVatNum_ButtonClick);
		this.TxtVatNum.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblCostOrder.AutoSize = true;
		this.LblCostOrder.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblCostOrder.Location = new System.Drawing.Point(1204, 533);
		this.LblCostOrder.Name = "LblCostOrder";
		this.LblCostOrder.Size = new System.Drawing.Size(18, 19);
		this.LblCostOrder.TabIndex = 256;
		this.LblCostOrder.Text = "1";
		this.label55.AutoSize = true;
		this.label55.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label55.Location = new System.Drawing.Point(1052, 532);
		this.label55.Name = "label55";
		this.label55.Size = new System.Drawing.Size(128, 19);
		this.label55.TabIndex = 255;
		this.label55.Text = "ط­/ تكلفة المبيعات";
		this.TxtCostOrder.EditValue = "";
		this.TxtCostOrder.Location = new System.Drawing.Point(629, 528);
		this.TxtCostOrder.Name = "TxtCostOrder";
		this.TxtCostOrder.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtCostOrder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtCostOrder.Properties.Appearance.Options.UseBackColor = true;
		this.TxtCostOrder.Properties.Appearance.Options.UseFont = true;
		this.TxtCostOrder.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtCostOrder.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtCostOrder.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtCostOrder.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions23.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions5.Image");
		this.TxtCostOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions23, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtCostOrder.Properties.ReadOnly = true;
		this.TxtCostOrder.Size = new System.Drawing.Size(417, 30);
		this.TxtCostOrder.TabIndex = 23;
		this.TxtCostOrder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtCostOrder_ButtonClick);
		this.TxtCostOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblMawadAwalyaa.AutoSize = true;
		this.LblMawadAwalyaa.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblMawadAwalyaa.Location = new System.Drawing.Point(590, 494);
		this.LblMawadAwalyaa.Name = "LblMawadAwalyaa";
		this.LblMawadAwalyaa.Size = new System.Drawing.Size(18, 19);
		this.LblMawadAwalyaa.TabIndex = 253;
		this.LblMawadAwalyaa.Text = "1";
		this.LblEntagTam.AutoSize = true;
		this.LblEntagTam.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblEntagTam.Location = new System.Drawing.Point(1204, 494);
		this.LblEntagTam.Name = "LblEntagTam";
		this.LblEntagTam.Size = new System.Drawing.Size(18, 19);
		this.LblEntagTam.TabIndex = 252;
		this.LblEntagTam.Text = "1";
		this.label58.AutoSize = true;
		this.label58.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label58.Location = new System.Drawing.Point(590, 493);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(18, 19);
		this.label58.TabIndex = 251;
		this.label58.Text = "1";
		this.label59.AutoSize = true;
		this.label59.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label59.Location = new System.Drawing.Point(438, 492);
		this.label59.Name = "label59";
		this.label59.Size = new System.Drawing.Size(141, 19);
		this.label59.TabIndex = 250;
		this.label59.Text = "مخزون المواد الاولية";
		this.TxtMawadAwalyaa.Location = new System.Drawing.Point(15, 488);
		this.TxtMawadAwalyaa.Name = "TxtMawadAwalyaa";
		this.TxtMawadAwalyaa.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtMawadAwalyaa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtMawadAwalyaa.Properties.Appearance.Options.UseBackColor = true;
		this.TxtMawadAwalyaa.Properties.Appearance.Options.UseFont = true;
		this.TxtMawadAwalyaa.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtMawadAwalyaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtMawadAwalyaa.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtMawadAwalyaa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions24.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions6.Image");
		this.TxtMawadAwalyaa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions24, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtMawadAwalyaa.Properties.ReadOnly = true;
		this.TxtMawadAwalyaa.Size = new System.Drawing.Size(417, 30);
		this.TxtMawadAwalyaa.TabIndex = 22;
		this.TxtMawadAwalyaa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtMawadAwalyaa_ButtonClick);
		this.TxtMawadAwalyaa.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.label60.AutoSize = true;
		this.label60.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label60.Location = new System.Drawing.Point(1204, 493);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(18, 19);
		this.label60.TabIndex = 248;
		this.label60.Text = "1";
		this.label61.AutoSize = true;
		this.label61.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label61.Location = new System.Drawing.Point(1052, 492);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(131, 19);
		this.label61.TabIndex = 247;
		this.label61.Text = "مخزون الانتاج التام";
		this.TxtEntagTam.Location = new System.Drawing.Point(629, 488);
		this.TxtEntagTam.Name = "TxtEntagTam";
		this.TxtEntagTam.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtEntagTam.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtEntagTam.Properties.Appearance.Options.UseBackColor = true;
		this.TxtEntagTam.Properties.Appearance.Options.UseFont = true;
		this.TxtEntagTam.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtEntagTam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtEntagTam.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtEntagTam.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions25.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions7.Image");
		this.TxtEntagTam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions25, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtEntagTam.Properties.ReadOnly = true;
		this.TxtEntagTam.Size = new System.Drawing.Size(417, 30);
		this.TxtEntagTam.TabIndex = 21;
		this.TxtEntagTam.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtEntagTam_ButtonClick);
		this.TxtEntagTam.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblRun.AutoSize = true;
		this.LblRun.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblRun.Location = new System.Drawing.Point(590, 454);
		this.LblRun.Name = "LblRun";
		this.LblRun.Size = new System.Drawing.Size(18, 19);
		this.LblRun.TabIndex = 245;
		this.LblRun.Text = "1";
		this.label63.AutoSize = true;
		this.label63.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label63.Location = new System.Drawing.Point(438, 453);
		this.label63.Name = "label63";
		this.label63.Size = new System.Drawing.Size(118, 19);
		this.label63.TabIndex = 244;
		this.label63.Text = "حساب التشغيل";
		this.TxtRun.Location = new System.Drawing.Point(15, 449);
		this.TxtRun.Name = "TxtRun";
		this.TxtRun.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtRun.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtRun.Properties.Appearance.Options.UseBackColor = true;
		this.TxtRun.Properties.Appearance.Options.UseFont = true;
		this.TxtRun.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtRun.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtRun.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtRun.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions26.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions8.Image");
		this.TxtRun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions26, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtRun.Properties.ReadOnly = true;
		this.TxtRun.Size = new System.Drawing.Size(417, 30);
		this.TxtRun.TabIndex = 20;
		this.TxtRun.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtRun_ButtonClick);
		this.TxtRun.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.LblZmmEmployee.AutoSize = true;
		this.LblZmmEmployee.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.LblZmmEmployee.Location = new System.Drawing.Point(1204, 454);
		this.LblZmmEmployee.Name = "LblZmmEmployee";
		this.LblZmmEmployee.Size = new System.Drawing.Size(18, 19);
		this.LblZmmEmployee.TabIndex = 242;
		this.LblZmmEmployee.Text = "1";
		this.label65.AutoSize = true;
		this.label65.Font = new System.Drawing.Font("Tahoma", 9.5f);
		this.label65.Location = new System.Drawing.Point(1052, 453);
		this.label65.Name = "label65";
		this.label65.Size = new System.Drawing.Size(103, 19);
		this.label65.TabIndex = 241;
		this.label65.Text = "ذمم الموظفين";
		this.TxtZmmEmployee.Location = new System.Drawing.Point(629, 449);
		this.TxtZmmEmployee.Name = "TxtZmmEmployee";
		this.TxtZmmEmployee.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
		this.TxtZmmEmployee.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.TxtZmmEmployee.Properties.Appearance.Options.UseBackColor = true;
		this.TxtZmmEmployee.Properties.Appearance.Options.UseFont = true;
		this.TxtZmmEmployee.Properties.Appearance.Options.UseTextOptions = true;
		this.TxtZmmEmployee.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.TxtZmmEmployee.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
		this.TxtZmmEmployee.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
		editorButtonImageOptions27.Image = (System.Drawing.Image)resources.GetObject("editorButtonImageOptions9.Image");
		this.TxtZmmEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions27, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null)
		});
		this.TxtZmmEmployee.Properties.ReadOnly = true;
		this.TxtZmmEmployee.Size = new System.Drawing.Size(417, 30);
		this.TxtZmmEmployee.TabIndex = 19;
		this.TxtZmmEmployee.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(TxtZmmEmployee_ButtonClick);
		this.TxtZmmEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnSave.Appearance.Options.UseFont = true;
		this.BtnSave.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnSave.ImageOptions.Image");
		this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnSave.Location = new System.Drawing.Point(954, 668);
		this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSave.Name = "BtnSave";
		this.BtnSave.Size = new System.Drawing.Size(125, 40);
		this.BtnSave.TabIndex = 177;
		this.BtnSave.Text = "حفظ F5";
		this.BtnSave.Click += new System.EventHandler(BtnSave_Click);
		this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.BtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 10f);
		this.BtnExit.Appearance.Options.UseFont = true;
		this.BtnExit.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("BtnExit.ImageOptions.Image");
		this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
		this.BtnExit.Location = new System.Drawing.Point(1100, 668);
		this.BtnExit.Margin = new System.Windows.Forms.Padding(4);
		this.BtnExit.Name = "BtnExit";
		this.BtnExit.Size = new System.Drawing.Size(125, 40);
		this.BtnExit.TabIndex = 178;
		this.BtnExit.Text = "خروج ";
		this.BtnExit.Click += new System.EventHandler(BtnExit_Click);
		this.TxtStoreMain.FormattingEnabled = true;
		this.TxtStoreMain.Location = new System.Drawing.Point(629, 101);
		this.TxtStoreMain.Name = "TxtStoreMain";
		this.TxtStoreMain.Size = new System.Drawing.Size(417, 24);
		this.TxtStoreMain.TabIndex = 1;
		this.TxtStoreMain.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtStoreMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		this.TxtStore2.FormattingEnabled = true;
		this.TxtStore2.Location = new System.Drawing.Point(15, 102);
		this.TxtStore2.Name = "TxtStore2";
		this.TxtStore2.Size = new System.Drawing.Size(417, 24);
		this.TxtStore2.TabIndex = 2;
		this.TxtStore2.KeyDown += new System.Windows.Forms.KeyEventHandler(Control_KeyDown);
		this.TxtStore2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CombName_KeyPress);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1252, 721);
		base.Controls.Add(this.BtnSave);
		base.Controls.Add(this.BtnExit);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.Name = "FrmDefualtAccount";
		this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.RightToLeftLayout = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "اعدادات الحسابات الافتراضية";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TxtNameBranch.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.separatorControl6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmSuppliers.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmCustomer.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtSupplierCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCustomerCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOrderAjil.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOrderCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturnOrderAjil.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtReturnOrderCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMasareef.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCloseRun.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainMoney.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtArbah77.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBankSraf.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtBoxCash.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainBank.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMainBox.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGardMinus.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGardPlus.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtOpenQuantity.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtGhramaOrder.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtVatNum.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtCostOrder.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtMawadAwalyaa.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtEntagTam.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtRun.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TxtZmmEmployee.Properties).EndInit();
		base.ResumeLayout(false);
	}
}
