using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class XMLElementContentTestTests
    {
        private XMLElementContentTest _XMLElementTest;

        [TestInitialize]
        public void MyTestIntialize()
        {
            _XMLElementTest = new XMLElementContentTest();            
        }

        [TestMethod]
        public void XmlContentTestReadValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/mySettings/xmlcontent";
            _XMLElementTest.ExpectedValue = "Hello World";
            _XMLElementTest.Run();
        }

        [TestMethod]
        public void XmlContentTestOneInstanceTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/mySettings/xmlcontent";
            _XMLElementTest.ExpectedValue = "Hello World";
            _XMLElementTest.MaximumOccurrences = 1;
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("2 instances of /mySettings/xmlcontent found where no more than 1 was expected", ex.Message);
            }
        }

        [TestMethod]
        public void XmlContentTestNoValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/mySettings/bob";
            _XMLElementTest.ExpectedValue = "Hello World";
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("0 instances of /mySettings/bob found where at least 1 was expected", ex.Message);
            }
        }

        [TestMethod]
        public void XmlContentTestNoMatchTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/mySettings/xmlcontent";
            _XMLElementTest.ExpectedValue = "Hello Bob";
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("No instances of /mySettings/xmlcontent found with the expected content", ex.Message);
            }
        }
    }
}