using System;
using System.Collections.Generic;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class EnvironmentVariableEqualsTest : EnvironmentVariableTest
    {
        [MandatoryField]
        public string ExpectedValue{ get; set; }

        public override void Run()
        {
            AssertState.Equal(ExpectedValue, Environment.GetEnvironmentVariable(Variable));
        }

        public override List<Test> CreateExamples()
        {
                return new List<Test>
                           {
                               new EnvironmentVariableEqualsTest
                                   {
                                       TestName = "Machine Name Example",
                                       Variable = "COMPUTERNAME",
                                       ExpectedValue = "APPSVR01"
                                   }
                           };
        }
    }
}