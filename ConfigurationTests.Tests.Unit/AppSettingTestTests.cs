using System.Configuration;
using System.IO;
using System.Reflection;
using ConfigurationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class AppSettingTestTests
    {
        private AppSettingTest _AppSettingTest;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _AppSettingTest = new AppSettingTest();
        }
        [TestMethod]
        public void AppSettingTestReadValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _AppSettingTest.Key = "Hello";
            _AppSettingTest.ExpectedValue = "World";
            _AppSettingTest.Run();
        }

        [TestMethod]
        public void AppSettingTestNoValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _AppSettingTest.Key = "Hallo";
            _AppSettingTest.ExpectedValue = "World";
            try
            {
                _AppSettingTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("AppSetting with Key [Hallo] was not found", ex.Message);
            }
        }

        [TestMethod]
        public void AppSettingTestNoMatchTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.config", Path.GetFileName(location));

            _AppSettingTest.Key = "Hello";
            _AppSettingTest.ExpectedValue = "Everybody";
            try
            {
                _AppSettingTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException<string> ex)
            {
                Assert.AreEqual("Expected [Everybody] Actual [World]", ex.Message);
            }
        }


        [TestMethod]
        public void AppSettingTestFileNotFoundTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.confug", Path.GetFileName(location));
            _AppSettingTest.Key = "Hello";
            _AppSettingTest.ExpectedValue = "Everybody";
            try
            {
                _AppSettingTest.Run();
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
        public void AppSettingTestEmptyFileTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = "EmptyFileTest.exe.config";
            _AppSettingTest.Key = "Hello";
            _AppSettingTest.ExpectedValue = "Everybody";
            try
            {
                _AppSettingTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Root element is missing."));
            }
        }

        [TestMethod]
        public void GivenAnAppSettingTheSettingShouldBeReturned()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _AppSettingTest.Key = "Hello";
            _AppSettingTest.ExpectedValue = "World";

            string result = _AppSettingTest.GetSettingElement();
            Assert.AreEqual(_AppSettingTest.ExpectedValue, result);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void GivenAnInvalidAppSettingAnAssertionExceptionShouldBeThrown()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _AppSettingTest.Path = Path.GetDirectoryName(location);
            _AppSettingTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _AppSettingTest.Key = "error";
            _AppSettingTest.ExpectedValue = "World";
            string result = _AppSettingTest.GetSettingElement();
        }
   
    }

}