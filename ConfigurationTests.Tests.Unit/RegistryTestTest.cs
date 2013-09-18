using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class RegistryTestTest
    {
        /// <summary>
        /// This test uses a set of common registry settings that should be constant
        /// on any Windows machine. Though the proof of this will come when we first check this in,
        /// of course.
        /// </summary>

        [TestMethod]
        public void CheckRegistryKeySettings()
        {

            var rkt = new RegistryKeyTest();

            var expectedEntries = new List<RegistryKeyTest.RegistryEntry>
                        {
                            new RegistryKeyTest.RegistryEntry
                            {ValueName = "JobMinimumRetryDelay", ExpectedValue = 0x258},
                            new RegistryKeyTest.RegistryEntry
                            {ValueName = "JobInactivityTimeout", ExpectedValue = 0x0076a700}
                        };

            rkt.BaseKey = RegistryKeyTest.RegistryBaseKey.HKEY_LOCAL_MACHINE;
            rkt.Path = "Software/Microsoft/Windows/CurrentVersion/BITS";
            rkt.ExpectedEntries = expectedEntries;

            rkt.Run();

        }
    }
}
