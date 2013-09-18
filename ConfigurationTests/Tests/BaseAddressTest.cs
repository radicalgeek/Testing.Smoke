using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class BaseAddressTest : ConfigurationTest
    {
        [MandatoryField]
        public string ServiceName { get; set; }
        public string ExpectedBaseAddressValue { get; set; }        
    }
}