using System.Collections.Generic;
using System.ComponentModel;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    [DefaultProperty("TestName")] 
    public abstract class Test
    {
        public abstract void Run();

        public abstract List<Test> CreateExamples();

        [MandatoryField]
        [Description("Name of test")]
        public string TestName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, TestName);
        }
    }
}