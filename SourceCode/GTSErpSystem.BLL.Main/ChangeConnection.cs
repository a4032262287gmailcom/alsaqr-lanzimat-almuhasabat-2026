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
		string path = Path.Combine(Application.StartupPath, "ERP.xml");
		if (!File.Exists(path))
			throw new FileNotFoundException("لم يتم العثور على ملف إعداد الاتصال ERP.xml.", path);

		XmlSerializer serializer = new XmlSerializer(typeof(List<SaveConnectionForXml>));
		List<SaveConnectionForXml> settings;
		using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			settings = (List<SaveConnectionForXml>)serializer.Deserialize(stream);
		}

		if (settings == null || settings.Count == 0 || settings[0] == null)
			throw new InvalidDataException("ملف ERP.xml لا يحتوي على إعدادات اتصال.");

		SaveConnectionForXml saved = settings[0];
		if (string.IsNullOrWhiteSpace(saved._comboServer) ||
			string.IsNullOrWhiteSpace(saved._comboDatabase))
			throw new InvalidDataException("ملف ERP.xml لا يحتوي على اسم خادم وقاعدة بيانات صالحين.");

		string userName = Class_Encryption.DecryptString(saved._TxtUser, "EF");
		string password = Class_Encryption.DecryptString(saved._TxtPassword, "EF");
		DataBaseDefualt = saved._comboDatabase;

		System.Data.SqlClient.SqlConnectionStringBuilder builder =
			new System.Data.SqlClient.SqlConnectionStringBuilder
			{
				DataSource = saved._comboServer.Trim(),
				InitialCatalog = saved._comboDatabase.Trim(),
				UserID = userName,
				Password = password,
				PersistSecurityInfo = true,
				MultipleActiveResultSets = true,
				ConnectTimeout = 5,
				Encrypt = false,
				TrustServerCertificate = true,
				ApplicationName = "EntityFramework"
			};

		return builder.ConnectionString;
	}
}


