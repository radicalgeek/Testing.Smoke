using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class XMLElementTestTests
    {
        private XMLElementTest _XMLElementTest;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _XMLElementTest = new XMLElementTest();
        }

        [TestMethod]
        public void XmlElementTestReadValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/appSettings/add";
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "key", Value = "Hello" });
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "value", Value = "World" });
            _XMLElementTest.Run();
        }

        [TestMethod]
        public void XmlElementTestOneInstanceTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/appSettings/add";
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "key", Value = "Hello" });
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "value", Value = "World" });
            _XMLElementTest.MaximumOccurrences = 1;
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("4 instances of /appSettings/add found where no more than 1 was expected", ex.Message);
            }
        }

        [TestMethod]
        public void XmlElementTestNoValueTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/appSettings/bob";
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "key", Value = "Hello" });
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "value", Value = "World" });
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("0 instances of /appSettings/bob found where at least 1 was expected", ex.Message);
            }
        }

        [TestMethod]
        public void XmlElementTestNoMatchTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.config", Path.GetFileName(location));
            _XMLElementTest.XmlPath = "/appSettings/add";
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "key", Value = "Hello" });
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "value", Value = "Everybody" });
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("No instances of /appSettings/add found with all expected attributes matching (most was 1)", ex.Message);
            }
        }


        [TestMethod]
        public void XmlElementTestFileNotFoundTest()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            _XMLElementTest.Path = Path.GetDirectoryName(location);
            _XMLElementTest.Filename = string.Format("{0}.confug", Path.GetFileName(location));
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "key", Value = "Hello" });
            _XMLElementTest.ExpectedValues.Add(new XMLElementTest.Attribute { Name = "value", Value = "Everybody" });
            try
            {
                _XMLElementTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("File Not Found", ex.Message);
            }
        }
    }
}


