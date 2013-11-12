using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class SettingsClassTests
    {

        private SettingsClassTest test;
        [TestInitialize]
        public void MyTestInitialize()
        {
            test = new SettingsClassTest();
        }

        [TestMethod]
        public void SettingsClassTestGetApplicationStringValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "StringSetting";
            test.ExpectedValue = "SettingValue";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestGetApplicationBoolValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "BoolSetting";
            test.ExpectedValue = "True";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestGetApplicationByteValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "ByteSetting";
            test.ExpectedValue = "123";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestGetApplicationCharValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "CharSetting";
            test.ExpectedValue = "x";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestGetUserStringValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "UserStringSetting";
            test.ExpectedValue = "ValueOfSetting";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestGetApplicationConnectionStringValue()
        {
            test.ClassName = "Settings";
            test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
            test.SettingName = "ConnectionStringSetting";
            test.ExpectedValue = "This is my Application connection string";
            test.Run();
        }

        [TestMethod]
        public void SettingsClassTestAssemblyNotFound()
        {
            try
            {
                test.AssemblyPath = "c:\\myfile.dll";
                test.Run();
                Assert.Fail("AssertionException not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("File Not Found: c:\\myfile.dll", ex.Message);
            }
        }

        [TestMethod]
        public void SettingsClassTestClassNotFound()
        {
            try
            {
                test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
                test.ClassName = "Bread";
                test.Run();
                Assert.Fail("AssertionException not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("Class Not Found: Bread", ex.Message);
            }
        }

        [TestMethod]
        public void SettingsClassTestClassNotSettingsClass()
        {
            try
            {
                test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
                test.ClassName = "SettingsClassTests";
                test.Run();
                Assert.Fail("AssertionException not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("Class Does Not Implement ApplicationSettingsBase: SettingsClassTests", ex.Message);
            }
        }

        [TestMethod]
        public void SettingsClassTestPropertyNotFound()
        {
            try
            {
                test.ClassName = "Settings";
                test.AssemblyPath = "ConfigurationTests.Tests.Unit.dll";
                test.SettingName = "Butter";
                test.Run();
                Assert.Fail("AssertionException not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("Setting Not Found: Butter", ex.Message);
            }
        }
    }
}