using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Account;

public class Class_Print_DailyRestrictionsALL
{
	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	public void Print_DailyRestrictionsALL(int ReferenceCode, int TranTypeID)
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
		DataSetALL dataSetALL = new DataSetALL();
		dataSetALL.Tables["Account_DailyRestrictions_ALL"].Clear();
		SqlCommand sqlCommand = new SqlCommand("Print_Account_DailyRestrictions_ALL", sqlConnection);
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlCommand.Parameters.AddWithValue("@ReferenceCode", ReferenceCode);
		sqlCommand.Parameters.AddWithValue("@TranTypeID", TranTypeID);
		sqlCommand.Parameters.AddWithValue("@BranchID", Convert.ToInt32(LoginDetails.BranchID));
		sqlDataAdapter.Fill(dataSetALL.Account_DailyRestrictions_ALL);
		sqlConnection.Close();
		if (dataSetALL.Tables["Account_DailyRestrictions_ALL"].Rows.Count > 0)
		{
			StiReport stiReport = new StiReport();
			stiReport.Load(Application.StartupPath + "\\Reports\\ReportAllAccountDailyRestrictionsALL.mrt");
			stiReport.RegData(dataSetALL.Account_DailyRestrictions_ALL);
			stiReport.Dictionary.Synchronize();
			stiReport.Compile();
			stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
			stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
			stiReport["VatNumComp"] = LoginDetails.VatNum;
			stiReport.Render();
			stiReport.Show(dialogForm: true);
		}
		else
		{
			MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}
}


