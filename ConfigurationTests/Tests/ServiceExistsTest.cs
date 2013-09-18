using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class ServiceExistsTest : Test
    {
        [Description("Name of service")]
        public string ServiceName { get; set; }

        public override void Run()
        {
            if ((new ServiceController(ServiceName)) == null) throw new AssertionException(string.Format("Service with name [{0}] was not found", ServiceName));
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test> { new ServiceExistsTest { ServiceName = "Bob" } };
        }
    }
}