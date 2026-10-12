using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GTSErpSystem.BLL.Main;

internal class ChangeConnection
{
	private Configuration conifg;

	public string DataBaseDefualt;

	public ChangeConnection()
	{
		conifg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
	}

	public string GetConnectionString(string Key)
	{
		return conifg.ConnectionStrings.ConnectionStrings[Key].ConnectionString;
	}

	public void SaveConnectionString(string key, string value)
	{
    if (string.IsNullOrWhiteSpace(key))
        throw new ArgumentException("Connection name is required.", nameof(key));

    if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Connection string is required.", nameof(value));

    if (value.TrimStart().StartsWith("metadata=", StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException(
            "Provide the SQL Server provider connection string, not an Entity Framework connection string.",
            nameof(value));

    System.Data.SqlClient.SqlConnectionStringBuilder sqlBuilder =
        new System.Data.SqlClient.SqlConnectionStringBuilder(value);

    sqlBuilder.MultipleActiveResultSets = true;
    sqlBuilder.ApplicationName = "EntityFramework";

    string connectionString =
        "metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;" +
        "provider=System.Data.SqlClient;provider connection string=\"" +
        sqlBuilder.ConnectionString + "\"";

    System.Configuration.Configuration configuration =
        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    System.Configuration.ConnectionStringsSection section =
        (System.Configuration.ConnectionStringsSection)
        configuration.GetSection("connectionStrings");

    if (section == null)
        throw new System.Configuration.ConfigurationErrorsException(
            "The connectionStrings section is missing.");

    System.Configuration.ConnectionStringSettings existing =
        section.ConnectionStrings[key];

    if (existing == null)
    {
        existing = new System.Configuration.ConnectionStringSettings(
            key, connectionString, "System.Data.EntityClient");

        section.ConnectionStrings.Add(existing);
    }
    else
    {
        existing.ConnectionString = connectionString;
        existing.ProviderName = "System.Data.EntityClient";
    }

    configuration.Save(ConfigurationSaveMode.Modified);
    ConfigurationManager.RefreshSection("connectionStrings");
}

	public int TestCon()
	{
    try
    {
        using (GTSdbEntities context = new GTSdbEntities())
        {
            System.Data.Common.DbConnection connection =
                context.Database.Connection;

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return 1;
        }
    }
    catch (Exception ex)
    {
        try
        {
            string logPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ConnectionTestError.txt");

            System.IO.File.AppendAllText(
                logPath,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                Environment.NewLine +
                ex.ToString() +
                Environment.NewLine +
                "------------------------------" +
                Environment.NewLine);
        }
        catch
        {
        }

        MessageBox.Show(
            "Database connection failed. See ConnectionTestError.txt for details.",
            "Database connection",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        return 0;
    }
}

	public string GetSqlConnectionFromFile()
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		try
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
			List<SaveConnectionForXml> list = new List<SaveConnectionForXml>();
			if (File.Exists(Application.StartupPath + "\\ERP.xml"))
			{
				FileStream fileStream = new FileStream(Application.StartupPath + "\\ERP.xml", FileMode.Open, FileAccess.Read);
				list = (List<SaveConnectionForXml>)xmlSerializer.Deserialize(fileStream);
				SaveConnectionForXml saveConnectionForXml = list[0];
				text = saveConnectionForXml._comboServer;
				text2 = Class_Encryption.DecryptString(saveConnectionForXml._TxtUser, "EF");
				text3 = Class_Encryption.DecryptString(saveConnectionForXml._TxtPassword, "EF");
				text4 = (DataBaseDefualt = saveConnectionForXml._comboDatabase);
				fileStream.Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "مستكشف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return "data source=" + text + ";initial catalog=" + text4 + ";persist security info=True;user id=" + text2 + ";password=" + text3 + ";";
	}
}


