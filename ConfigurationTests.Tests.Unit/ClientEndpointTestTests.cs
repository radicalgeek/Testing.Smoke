using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    [TestClass]
    public class ClientEndpointTestTests
    {
        [TestMethod]
        public void CheckKnownEndpoint()
        {
            var test = new ClientEndpointTest { TestName = "ChargeService Endpoint" };
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = string.Format("{0}.config", Path.GetFileName(location));
            test.EndpointName="BasicHttpBinding_TMSPaymentEFTSoap";
            test.ExpectedAddress="http://ukuatnexcg1/wsChargeService/ChargeService.svc";
            test.ExpectedBinding="basicHttpBinding";
            test.ExpectedBindingConfiguration="BasicHttpBinding_TMSPaymentEFTSoap";
            test.ExpectedContract="ChargeGateway.TMSPaymentEFTSoap";
            test.CheckConnectivity=false;
            test.Run();
        }
    }
}
