using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class SettingsClassTest : Test
    {
        [MandatoryField]
        public string ClassName;
        [MandatoryField]
        public string AssemblyPath;
        [MandatoryField]
        public string SettingName;
        [MandatoryField]
        public string ExpectedValue;

        public override void Run()
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(AssemblyPath);
            }
            catch (FileNotFoundException)
            {
                throw new AssertionException(string.Format("File Not Found: {0}", AssemblyPath));
            }
            Type classType = assembly.GetTypes().FirstOrDefault(t => t.Name == ClassName);
            if (classType == null)
                throw new AssertionException(string.Format("Class Not Found: {0}", ClassName));
            if (!classType.IsSubclassOf(typeof(ApplicationSettingsBase)))
                throw new AssertionException(string.Format("Class Does Not Implement ApplicationSettingsBase: {0}", ClassName));

            PropertyInfo propertyInfo = classType.GetProperty(SettingName);
            if (propertyInfo == null)
                throw new AssertionException(string.Format("Setting Not Found: {0}", SettingName));
            object instance = Activator.CreateInstance(classType);
            object value = propertyInfo.GetValue(instance, null);
            AssertState.Equal(ExpectedValue, value.ToString());
        }

        public override List<Test> CreateExamples()
        {
            List<Test> examples = new List<Test>();
            examples.Add(new SettingsClassTest
                             {
                                 TestName = "Example Settings Class Test",
                                 AssemblyPath = "D:\\Services\\InstalledService\\bin\\MyAssembly.dll",
                                 ClassName = "Settings",
                                 SettingName = "OtherServiceURI",
                                 ExpectedValue = "http://UKPRODSERVICESERVER01/Services/Service.svc"
                             });
            return examples;
        }
    }
}