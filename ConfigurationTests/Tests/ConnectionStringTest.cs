using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{

    public class ConnectionStringTest : ConfigurationTest
    {
        private List<ConnectionStringSetting> _stringSettings;
        public ConnectionStringTest()
        {
            _stringSettings = new List<ConnectionStringSetting>();
        }
        public class ConnectionStringSetting
        {
            public string SettingName { get; set; }
            public string ExpectedValue { get; set; }
            public override string ToString()
            {
                return SettingName;
            }
        }


        [MandatoryField]
        public string ConnectionStringName { get; set; }
        [MandatoryField]
        public List<ConnectionStringSetting> StringSettings
        {
            get { return _stringSettings; }
            set { _stringSettings = value;}
        }
        public bool CheckConnectivity { get; set; }

        public override void Run()
        {
            var connectionStringSettings = GetConnectionStringSettings();

            var connectionString = GetConnectionString(connectionStringSettings);

            CheckAssertions(connectionString);
        }

        protected void CheckAssertions(string connectionString)
        {
            DbConnectionStringBuilder dbConnection = new DbConnectionStringBuilder { ConnectionString = connectionString };

            foreach (ConnectionStringSetting setting in StringSettings)
            {
                if (dbConnection.ContainsKey(setting.SettingName))
                {
                    AssertState.Equal(setting.ExpectedValue, dbConnection[setting.SettingName].ToString());
                }
                else
                {
                    throw new AssertionException(string.Format("Connection String setting [{0}] not found", setting.SettingName));
                }
            }

            if (dbConnection.Keys != null)
                AssertState.Equal(StringSettings.Count, dbConnection.Keys.Count);
            else
            {
                throw new AssertionException("No StringSetting values were found");
            }

            if (CheckConnectivity)
                using (SqlConnection sqlConnection = (new SqlConnection(connectionString)))
                    sqlConnection.Open();
        }

        protected static string GetConnectionString(ConnectionStringSettings connectionStringSettings)
        {
            return connectionStringSettings.ConnectionString;
        }

        protected ConnectionStringSettings GetConnectionStringSettings()
        {
            ConnectionStringsSection connectionStrings = (ConnectionStringsSection)GetConfig().GetSection("connectionStrings");
            ConnectionStringSettings connectionStringSettings = connectionStrings.ConnectionStrings[ConnectionStringName];

            if (connectionStringSettings == null)
                throw new AssertionException(string.Format("Connection String [{0}] not found", ConnectionStringName));
            return connectionStringSettings;
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                             {
                                 
                                 new ConnectionStringTest
                                     {
                                         TestName = "Example config connectionstring",
                                         Path = @"D:\Folder\Path\bin",
                                         Filename = "Example.exe.config",
                                         ConnectionStringName = "PrimaryConnectionString",
                                         StringSettings =
                                           new List<ConnectionStringSetting>
                                               {new ConnectionStringSetting{SettingName = "Initial Catalog", ExpectedValue= "AmberLive"}},
                                         CheckConnectivity = true
                                     },
                                 new ConnectionStringTest
                                     {
                                         TestName = "Example web.config connectionstring",
                                         Path = @"D:\Folder\Path",
                                         Filename = "web.config",
                                         StringSettings =
                                           new List<ConnectionStringSetting>
                                               {new ConnectionStringSetting{SettingName="Data Source", ExpectedValue = @"UKDEVSQLWR02\SQL2008"}},
                                         CheckConnectivity = false
                                     }
                             };
        }
    }
}