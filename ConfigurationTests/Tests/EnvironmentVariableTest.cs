using System.ComponentModel;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class EnvironmentVariableTest:Test
    {
        [MandatoryField]
        [Description("Name of variable")]
        public string Variable{ get; set; }
    }
}