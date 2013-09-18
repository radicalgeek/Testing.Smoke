using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests.Unit
{
    /// <summary>
    /// Summary description for ServiceEndpointTest
    /// </summary>
    [TestClass]
    public class ServiceEndpointTestTests
    {

        [DeploymentItem("ServiceEndpointTest.exe")]
        [DeploymentItem("ServiceEndpointTest.exe.config")]
        [TestMethod]
        public void CheckKnownServiceEndpoint()
        {
            var test = new ServiceEndpointTest { TestName = "Debt Service Endpoint" };
            string location = Assembly.GetExecutingAssembly().Location;
            test.Path = Path.GetDirectoryName(location);
            test.Filename = "ServiceEndpointTest.exe.config";
            test.ServiceName = "DebtService.DebtService";
            test.EndpointName = "DebtServiceEndPoint";
            test.ExpectedAddress = "net.msmq://ukuatefcoesb01/private/debtcollection.svc";
            test.ExpectedBinding = "netMsmqBinding";
            test.ExpectedBindingConfiguration = "netMsmq";
            test.ExpectedContract = "DebtService.IDebtService";
            test.CheckConnectivity = false;
            test.Run();


        }
    }
}
