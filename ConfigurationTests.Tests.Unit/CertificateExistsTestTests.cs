using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadicalGeek.Common.Security;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class CertificateExistsTestTests
    {
        [TestMethod]
        [ExpectedException(typeof(MultipleCertificatesException))]
        public void TestMultipleCertificatesFound()
        {
            var test = new CertificateExistsTest
            {
                CertificateStoreLocation = StoreLocation.LocalMachine,
                CertificateStoreName = StoreName.Root
            };
            test.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(CertificateNotFoundException))]
        public void TestNoCertificateFound()
        {
            var test = new CertificateExistsTest
            {
                CertificateStoreLocation = StoreLocation.LocalMachine,
                CertificateStoreName = StoreName.Root,
                SubjectName = "Bob"
            };
            test.Run();
        }
    }
}