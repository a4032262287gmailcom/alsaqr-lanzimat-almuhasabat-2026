using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Reports;

public class Class_Profit
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	public void GetProfitByGroup()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 71))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAvergForSore_ByGroup"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetTotalAvergForSore_ByGroup", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			sqlDataAdapter.Fill(dataSetALL.TableAvergForSore_ByGroup);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAvergForSore_ByGroup"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportStoreAvergByGroup.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportStoreAvergByGroup.mrt");
				}
				stiReport.RegData(dataSetALL.TableAvergForSore_ByGroup);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["CommercialRegister"] = LoginDetails.CommercialRegister;
				stiReport["Address"] = LoginDetails.Address;
				stiReport["Phone"] = LoginDetails.Phone;
				stiReport["Bank"] = LoginDetails.CompanyBank;
				stiReport["Title"] = "تقرير العملاء";
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

	public void GetProfitByItem()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 72))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAvergForSore_ByItem"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetTotalAvergForSore_ByItem", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			sqlDataAdapter.Fill(dataSetALL.TableAvergForSore_ByItem);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAvergForSore_ByItem"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportStoreAvergByItem.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportStoreAvergByItem.mrt");
				}
				stiReport.RegData(dataSetALL.TableAvergForSore_ByItem);
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

	public void GetProfitByItemScaffolds()
	{
		if (CheckPermession.CheckUserPageEventSave(LoginDetails.GroupID, 72))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableAvergForSore_ByItem"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetTotalAvergForSoreScaffolds_ByItem", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			sqlDataAdapter.Fill(dataSetALL.TableAvergForSore_ByItem);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableAvergForSore_ByItem"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportStoreAvergByItemScaffold.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportStoreAvergByItemScaffold.mrt");
				}
				stiReport.RegData(dataSetALL.TableAvergForSore_ByItem);
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
}


