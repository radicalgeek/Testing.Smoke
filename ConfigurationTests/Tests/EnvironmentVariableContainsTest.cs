using System;
using System.Collections.Generic;
using System.ComponentModel;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class EnvironmentVariableContainsTest : EnvironmentVariableTest
    {
        [MandatoryField]
        public string ExpectedValue{ get; set; }

        [DefaultValue(false)]
        public bool CaseSensitive{ get; set; }

        public override void Run()
        {
            string s = Environment.GetEnvironmentVariable(Variable) ?? string.Empty;
            if (!CaseSensitive)
            {
                s = s.ToUpper();
                ExpectedValue = ExpectedValue.ToUpper();
            }
            if (!s.Contains(ExpectedValue))
                throw new AssertionException(string.Format("Environment variable {0} did not contain expected text [{1}]", Variable, ExpectedValue));
        }

        public override List<Test> CreateExamples()
        {
                return new List<Test>
                           {
                               new EnvironmentVariableContainsTest
                                   {
                                       TestName = "Path Example",
                                       Variable = "PATH",
                                       ExpectedValue = @"C:\Windows",
                                       CaseSensitive = false
                                   }
                           };
        }
    }
}