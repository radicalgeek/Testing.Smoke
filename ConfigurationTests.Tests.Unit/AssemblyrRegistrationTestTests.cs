using ConfigurationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class AssemblyRegistrationTestTests
    {
        [TestMethod]
        public void CheckAssemblyRegistered()
        {
            var rkt = new AssemblyRegistrationTest();
            rkt.DllName = "mscorlib.dll";
            rkt.Run();
        }
        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void CheckAssemblyNotRegistered()
        {
            try
            {
                var rkt = new AssemblyRegistrationTest();
                rkt.DllName = "No such DLL";
                rkt.Run();
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual("No such DLL was not found to be registered in the GAC", ex.Message);
                throw;
            }
        }
    }
}
