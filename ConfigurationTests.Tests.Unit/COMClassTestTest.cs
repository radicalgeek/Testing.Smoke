using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class COMClassTestTest
    {
        /// <summary>
        /// This test checks given a classname that it exists in the registry of a mchine
        /// </summary>

        [TestMethod]
        public void CheckDllIsRegistered()
        {
            var rkt = new COMClassTest();
            rkt.ClassName = "InternetExplorer.Application";
            rkt.DllName = "Some Internet Explorer DLL";
            rkt.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void CheckDllIsNotRegistered()
        {
            try
            {
                var rkt = new COMClassTest();
                rkt.ClassName = "FishFood";
                rkt.DllName = "No such DLL";
                rkt.Run();
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("No such DLL was not found to be registered", ex.Message);
                throw;
            }
        }
    }
}
