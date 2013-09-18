using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class ConnectionStringTestTests
    {
        [DeploymentItem("ConnectionStringTest.exe")]
        [DeploymentItem("ConnectionStringTest.exe.config")]
        [TestMethod]
        public void ReadSqlConnectionString()
        {
            var connectionStringSettings = new List<ConnectionStringTest.ConnectionStringSetting>
                        {   
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Data Source", ExpectedValue= "10.99.175.100"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Initial Catalog", ExpectedValue= "ExpressFinance"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "User ID", ExpectedValue= "WSUser"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "password", ExpectedValue= "grollyrabbit"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "MultipleActiveResultSets", ExpectedValue= "True"}
                        };

            var test = new ConnectionStringTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = "ConnectionStringTest.exe.config";
            test.ConnectionStringName = "Nexus";
            test.StringSettings = connectionStringSettings;
            test.CheckConnectivity = false;
            test.Run();
        }


        [DeploymentItem("ConnectionStringTest.exe")]
        [DeploymentItem("ConnectionStringTest.exe.config")]
        [TestMethod]
        public void ReadOLEDBConnectionString()
        {
            var connectionStringSettings = new List<ConnectionStringTest.ConnectionStringSetting>
                        {   
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Provider", ExpectedValue= "SQLOLEDB.1"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Initial Catalog", ExpectedValue= "moneymart"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Integrated Security", ExpectedValue= "SSPI"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Data Source", ExpectedValue= @"ukuatefcosql02\sql2008"}
                        };

            var test = new ConnectionStringTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = "ConnectionStringTest.exe.config";
            test.ConnectionStringName = "BounceProc";
            test.StringSettings = connectionStringSettings;
            test.CheckConnectivity = false;
            test.Run();
        }


        [DeploymentItem("EncryptedConnectionStringTest.exe")]
        [DeploymentItem("EncryptedConnectionStringTest.exe.config")]
        [TestMethod]
        public void ReadEncryptedSqlConnectionString()
        {
            var connectionStringSettings = new List<ConnectionStringTest.ConnectionStringSetting>
                        {
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Data Source", ExpectedValue= "10.99.175.100"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "Initial Catalog", ExpectedValue= "ExpressFinance"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "User ID", ExpectedValue= "WSUser"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "password", ExpectedValue= "W3bS3rv1c3"},
                            new ConnectionStringTest.ConnectionStringSetting{SettingName = "MultipleActiveResultSets", ExpectedValue= "True"}
                        };

            var test = new EncryptedConnectionStringTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = "EncryptedConnectionStringTest.exe.config";
            test.ConnectionStringName = "Nexus";
            test.StringSettings = connectionStringSettings;
            test.CheckConnectivity = false;
            test.Run();
        }


    }
}