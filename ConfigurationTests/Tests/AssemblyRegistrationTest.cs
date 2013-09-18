using System.Collections.Generic;
using System.IO;
using System.Reflection;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class AssemblyRegistrationTest : Test
    {
        [MandatoryField]
        public string DllName { get; set; }

        public override void Run()
        {
            try
            {
                Assembly.Load(DllName);
            }
            catch (FileNotFoundException)
            {
                throw new AssertionException(string.Format("{0} was not found to be registered in the GAC", DllName));
            }
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                        {
                            new AssemblyRegistrationTest
                                {
                                    DllName = "TMS_HyperString.dll",
                                    TestName = "Assembly Registration Example"
                                }
                        };
        }
    }
}
