namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class EndpointTest : ConfigurationTest
    {
        public virtual string EndpointName { get; set; }
        public string ExpectedAddress { get; set; }
        public string ExpectedBehaviourConfiguration{ get; set; }
        public string ExpectedBinding{ get; set; }
        public string ExpectedBindingConfiguration{ get; set; }
        public string ExpectedContract{ get; set; }
        public string ExpectedEndpointConfiguration{ get; set; }
        public bool? CheckConnectivity{ get; set; }
        public int? ConnectionTimeout{ get; set; }
    }
}
