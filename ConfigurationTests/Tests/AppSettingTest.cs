using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class AppSettingTest : ConfigurationTest
    {
        [MandatoryField]
        public string Key { get; set; }
        public string ExpectedValue { get; set; }

        [DefaultValue(false)]
        public bool CaseSensitive { get; set; }

        public override void Run()
        {
            string elementValue = GetSettingElement();
            AssertState.Equal(ExpectedValue, elementValue, CaseSensitive);
        }

        protected internal string GetSettingElement()
        {
            AppSettingsSection appSettingsSection = (AppSettingsSection)GetConfig().GetSection("appSettings");
            KeyValueConfigurationElement element = appSettingsSection.Settings[Key];
            if (element == null)
                throw new AssertionException(string.Format("AppSetting with Key [{0}] was not found", Key));
            return element.Value;
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                       {
                           new AppSettingTest
                               {
                                   TestName = "Example AppSetting Test",
                                   Key="UserId",
                                   ExpectedValue = "1234",
                                   Filename = "MyProgram.exe.config"
                               }
                       };
        }
    }
}