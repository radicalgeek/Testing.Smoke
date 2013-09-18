using ConfigurationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class PlatformTestTest
    {
        /// <summary>
        /// This test checks given a CPU platform and a dll that the dll matches the CPU platform passed in
        /// </summary>

        [TestMethod]
        public void CheckPlatformIsCorrect()
        {
            var rkt = new PlatformTest();
            rkt.ExpectedCpuArchitecture = CpuArchitectures.x86;
            rkt.AssemblyFilePath = @"C:\windows\Microsoft.NET\Framework\v2.0.50727\Accessibility.dll";
            rkt.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void CheckPlatformIsIncorrect()
        {
            try
            {
                var rkt = new PlatformTest();
                rkt.ExpectedCpuArchitecture = CpuArchitectures.x64;
                rkt.AssemblyFilePath = @"C:\windows\Microsoft.NET\Framework\v2.0.50727\Accessibility.dll";
                rkt.Run();
            }
            catch (AssertionException ex)
            {
                Assert.AreEqual(@"C:\windows\Microsoft.NET\Framework\v2.0.50727\Accessibility.dll was expected to be compiled for x64 but was actually compiled for x86", ex.Message);
                throw;
            }
        }
    }
}
