using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    public interface ITwice { }
    public class Cod : ITwice { }

    [TestClass]
    public class IoCResolutionTestTests
    {
        private IoCResolutionTest _IoCResolutionTest;
        public interface IFish { }
        public interface ITwice { }
        public class Cod : IFish { }
        public interface IComputer { }
        public class AppleMacintosh : IComputer { }

        [TestInitialize]
        public void MyTestInitialize()
        {
            _IoCResolutionTest = new IoCResolutionTest();            
        }

        [TestMethod]
        public void TestInterfaceNotFound()
        {
            _IoCResolutionTest.Interface = "IDave";
            try
            {
                _IoCResolutionTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual(ex.Message, "No interface found with the name IDave");
            }
        }

        [TestMethod]
        public void TestInterfaceDefinedTwice()
        {
            _IoCResolutionTest.Interface = "ITwice";
            try
            {
                _IoCResolutionTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual(ex.Message, "Interface ITwice defined multiple times");
            }
        }

        [TestMethod]
        public void TestExpectedTypeNotFound()
        {
            _IoCResolutionTest.Interface = "IFish";
            _IoCResolutionTest.ExpectedType = "Trout";
            try
            {
                _IoCResolutionTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual(ex.Message, "No Type found with the name Trout");
            }
        }

        [TestMethod]
        public void TestExpectedTypeDefinedTwice()
        {
            _IoCResolutionTest.Interface = "IFish";
            _IoCResolutionTest.ExpectedType = "Cod";
            try
            {
                _IoCResolutionTest.Run();
                throw new AssertFailedException("Expected exception was not thrown");
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual(ex.Message, "Type Cod defined multiple times");
            }
        }

        [TestMethod]
        public void TestSuccessfulIoCResolution()
        {
            _IoCResolutionTest.Interface = "IComputer";
            _IoCResolutionTest.ExpectedType = "AppleMacintosh";
            _IoCResolutionTest.Run();
        }
    }
}
