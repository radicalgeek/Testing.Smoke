using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Tests;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests
{
    public class ConfigurationTestSuite
    {
        public string Name { get; set; }
        public string Description {get;set;}

        private List<Test> tests;
        public List<Test> Tests
        {
            get
            {
                return tests??(tests=new List<Test>());
            }
            set{
                tests = value;
            }
        }

        public void CreateExampleData()
        {
            Name = "Example";
            Description = "This is an Example Configuration Test Suite to illustrate the usage of the various Test types.";

            Tests = new List<Test>();

            var testsTypes = typeof(Test).Assembly.GetTypes()
                            .Where(type => type.IsSubclassOf(typeof(Test)) && !type.IsAbstract);

            foreach (var type in testsTypes)
            {
                object instance = Activator.CreateInstance(type);
                var method = type.GetMethod("CreateExamples");
                var examples = (List<Test>)((MethodInfo)method).Invoke(instance, null);
                Tests.AddRange(examples);
            }
        }
    }
}
