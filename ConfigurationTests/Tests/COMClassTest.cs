using System;
using System.Collections.Generic;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class COMClassTest : Test
    {
        [MandatoryField]
        public string ClassName { get; set; }

        [MandatoryField]
        public string DllName { get; set; }

        public override void Run()
        {
            if (Type.GetTypeFromProgID(ClassName, false) == null)
                throw new AssertionException(string.Format("{0} was not found to be registered", DllName));
        }
        public override List<Test> CreateExamples()
        {
            return new List<Test>
                        {
                            new COMClassTest
                                {
                                    ClassName = "clLoggedInUser",
                                    DllName = "AppSecurityController.dll",
                                    TestName = "Class Name Example"
                                }
                        };
        }
    }
}
