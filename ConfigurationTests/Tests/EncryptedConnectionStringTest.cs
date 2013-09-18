using System.Collections.Generic;
using System.Configuration;
using RadicalGeek.Common.Text;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class EncryptedConnectionStringTest : ConnectionStringTest
    {
        private new static string GetConnectionString(ConnectionStringSettings connectionStringSettings)
        {            string connectionStringSettingsConnectionString = connectionStringSettings.ConnectionString;

            string connectionString = connectionStringSettingsConnectionString.Decrypt();

            return connectionString;
        }

        public override void Run()
        {
            var connectionStringSettings = GetConnectionStringSettings();

            var connectionString = GetConnectionString(connectionStringSettings);

            CheckAssertions(connectionString);
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                       {

                           new EncryptedConnectionStringTest
                               {
                                   TestName = "Example config connectionstring",
                                   Path = @"D:\Folder\Path\bin",
                                   Filename = "Example.exe.config",
                                   ConnectionStringName = "PrimaryConnectionString",
                                   StringSettings =
                                           new List<ConnectionStringSetting>
                                               {new ConnectionStringSetting{SettingName="MultipleActiveResultSets", ExpectedValue = "True"}},
                                   CheckConnectivity = true
                               }
                       };
        }
    }
}