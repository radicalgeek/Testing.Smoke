using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class HttpConnectionTestTest
    {
        [TestMethod]
        public void CheckHttpConnectionTest()
        {
            var httpConnTest = new HttpConnectionTest
            {
                TestName = "Http Connection Test",
                UrlToTest = "http://clearvision.dfguk.com/clearvision/",
                ExpectedResponse = "200"
            };

            httpConnTest.Run();
        }
    }
}
