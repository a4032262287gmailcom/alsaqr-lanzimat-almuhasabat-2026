using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GTSErpSystem.BLL.Security;
using GTSErpSystem.DataSetRep;
using Stimulsoft.Report;

namespace GTSErpSystem.BLL.Reports;

public class Class_MovementItem
{
	private CheckPrvlg CheckPermession = new CheckPrvlg();

	public void MovementItemActive(int ItemID)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			if (ItemID <= 0)
			{
				return;
			}
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
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

	public void MovementItemNormal(int ItemID)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			if (ItemID <= 0)
			{
				return;
			}
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
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

	public void MovementItemActiveAllBranches(int ItemID)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			if (ItemID <= 0)
			{
				return;
			}
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
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

	public void MovementItemNormalAllBranches(int ItemID)
	{
		if (CheckPermession.CheckUserPage(LoginDetails.GroupID, 102))
		{
			if (ItemID <= 0)
			{
				return;
			}
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + LoginDetails.nameServer + ";Initial Catalog=" + LoginDetails.databaseServer + ";Persist Security Info=True;User ID=" + LoginDetails.userNameServer + ";Password=" + LoginDetails.passServer);
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


