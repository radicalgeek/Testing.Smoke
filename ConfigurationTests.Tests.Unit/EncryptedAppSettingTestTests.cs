using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using ConfigurationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    /// <summary>
    /// Summary description for EncryptedAppSettingTestTests
    /// </summary>
    [TestClass]
    public class EncryptedAppSettingTestTests
    {
        [TestMethod]
        public void EncryptedAppSettingTestReadNoneNexusEncryptedValueTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.config", Path.GetFileName(location));
            test.Key = "EncryptedHello";
            test.ExpectedValue = "World";
            test.Run();
        }

        [TestMethod]
        public void EncryptedAppSettingTestReadNexusEncryptedValueTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.config", Path.GetFileName(location));
            test.Key = "NexusEncryptedHello";
            test.ExpectedValue = "World";
            test.Run();
        }

        [TestMethod]
        public void EncryptedAppSettingTestNoValueTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.config", Path.GetFileName(location));
            test.Key = "Hallo";
            test.ExpectedValue = "World";
            try
            {
                test.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("AppSetting with Key [Hallo] was not found", ex.Message);
            }
        }

        [TestMethod]
        public void EncryptedAppSettingTestNoMatchTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.config", Path.GetFileName(location));

            test.Key = "NexusEncryptedHello";
            test.ExpectedValue = "Everybody";
            try
            {
                test.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException<string> ex)
            {
                Assert.AreEqual("Expected [Everybody] Actual [World]", ex.Message);
            }
        }


        [TestMethod]
        public void EncryptedAppSettingTestFileNotFoundTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.confug", Path.GetFileName(location));
            test.Key = "NexusEncryptedHello";
            test.ExpectedValue = "Everybody";
            try
            {
                test.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("File Not Found"));
            }
        }

        [TestMethod]
        [DeploymentItem("EmptyFileTest.exe.config")]
        [DeploymentItem("EmptyFileTest.exe")]
        public void EncryptedAppSettingTestEmptyFileTest()
        {
            var test = new EncryptedAppSettingTest();
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = "EmptyFileTest.exe.config";
            test.Key = "NexusEncryptedHello";
            test.ExpectedValue = "Everybody";
            try
            {
                test.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Root element is missing."));
            }
        }

        [TestMethod]
        public void GivenCreateExampleMethodOneExampleIsCreated()
        {
            var test = new EncryptedAppSettingTest();
            var result = test.CreateExamples();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GivenCreateExampleMethodOneExampleIsCreatedWithTestNameExampleEncryptedAppSettingTest()
        {
            var test = new EncryptedAppSettingTest();
            var result = test.CreateExamples();
            Assert.AreEqual("Example Encrypted AppSetting Test", result.First().TestName);
        } 
    }
}
