using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Reports;

public class Class_ReportsALL
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	private string Server = LoginDetails.nameServer;

	private string dataServer = LoginDetails.databaseServer;

	private string userserver = LoginDetails.userNameServer;

	private string passServer = LoginDetails.passServer;

	public void GetReport_MovementItemActive(int ItemID, string ItemCode, string ItemName)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableMovement"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetReport_MovementItemActive", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@ItemID", ItemID);
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			sqlDataAdapter.Fill(dataSetALL.TableMovement);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableMovement"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportMovmentItem.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportMovmentItem.mrt");
				}
				stiReport.RegData(dataSetALL.TableMovement);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["ItemCode"] = ItemCode;
				stiReport["ItemName"] = ItemName;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}

	public void GetReport_MovementItemALL(int ItemID, string ItemCode, string ItemName)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + dataServer + ";Persist Security Info=True;User ID=" + userserver + ";Password=" + passServer);
			DataSetALL dataSetALL = new DataSetALL();
			dataSetALL.Tables["TableMovement"].Clear();
			SqlCommand sqlCommand = new SqlCommand("GetReport_MovementItem", sqlConnection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlCommand.Parameters.AddWithValue("@ItemID", ItemID);
			sqlCommand.Parameters.AddWithValue("@BranchID", LoginDetails.BranchID);
			sqlDataAdapter.Fill(dataSetALL.TableMovement);
			sqlConnection.Close();
			if (dataSetALL.Tables["TableMovement"].Rows.Count > 0)
			{
				StiReport stiReport = new StiReport();
				if (LoginDetails.Lang == "ar-EG")
				{
					stiReport.Load(Application.StartupPath + "\\Reports\\ReportMovmentItem.mrt");
				}
				else
				{
					stiReport.Load(Application.StartupPath + "\\ReportsEN\\ReportMovmentItem.mrt");
				}
				stiReport.RegData(dataSetALL.TableMovement);
				stiReport.Dictionary.Synchronize();
				stiReport.Compile();
				stiReport["CompanyAr"] = LoginDetails.CompanyNameAR;
				stiReport["CompanyEn"] = LoginDetails.CompanyNameEN;
				stiReport["VatNumComp"] = LoginDetails.VatNum;
				stiReport["ItemCode"] = ItemCode;
				stiReport["ItemName"] = ItemName;
				stiReport.Render();
				stiReport.Show(dialogForm: true);
			}
			else
			{
				MessageBox.Show("لا يوجد سجلات متاحة للطباعة", "مستكشف الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
			}
		}
		else
		{
			MessageBox.Show("عفوا، لايوجد لك صلاحية \r\n يرجي مراجعة مدير النظام لاعطائك صلاحية.", "مستكشف صلاحيات المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3, MessageBoxOptions.RightAlign);
		}
	}
}


