using System.Collections.Generic;
using RadicalGeek.Common.Text;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class EncryptedAppSettingTest : AppSettingTest
    {
        public override void Run()
        {
            string elementValue = GetSettingElement();

            elementValue = elementValue.Decrypt();
            AssertState.Equal(ExpectedValue, elementValue, CaseSensitive);
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                       {
                           new EncryptedAppSettingTest
                               {
                                   TestName = "Example Encrypted AppSetting Test",
                                   Key="Password",
                                   ExpectedValue = "p@ssword1",
                                   Filename = "MyProgram.exe.config"
                               }
                       };
        }
    }
}
