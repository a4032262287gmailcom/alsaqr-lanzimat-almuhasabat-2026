using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Reports;

public class Class_Account_CustSupp
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	public void GetAllCustmer()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 51))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAccount_CusSupp"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Account_Cuss", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.TableAccount_CusSupp);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAccount_CusSupp"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "تقرير العملاء";
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "Customer Report";
				}
				stiReport.RegData(dataSetALL.TableAccount_CusSupp);
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
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void GetAllCustmerByTotal()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 51))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAccount_CusSupp"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Account_CussByTotal", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.TableAccount_CusSupp);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAccount_CusSupp"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "تقرير العملاء";
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "Customer Report";
				}
				stiReport.RegData(dataSetALL.TableAccount_CusSupp);
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
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void GetAllSupp()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 65))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAccount_CusSupp"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Account_Supp", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.TableAccount_CusSupp);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAccount_CusSupp"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "تقرير الموردين";
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCustSuppReport.mrt");
					stiReport["Title"] = "Supplier Report";
				}
				stiReport.RegData(dataSetALL.TableAccount_CusSupp);
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
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void GetMizanCusSupp()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 51))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAccount_CusSupp"].Clear();
			SqlCommand sqlCommand = new SqlCommand("Print_Account_MizanCustSupp", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataSetALL.TableAccount_CusSupp);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAccount_CusSupp"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\PrintCustSuppMizanReport.mrt");
					stiReport["Title"] = "تقرير ميزان العملاء والموردين";
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\PrintCustSuppMizanReport.mrt");
					stiReport["Title"] = "Customer Report";
				}
				stiReport.RegData(dataSetALL.TableAccount_CusSupp);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["@Date1"] = LoginDetails.CompanyBank;
				stiReport["@Date2"] = LoginDetails.CompanyBank;
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
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}


