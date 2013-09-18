using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using Microsoft.JScript;
using Microsoft.VisualBasic;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    [HiddenFromUi]
    public class CustomTest : Test
    {
        public enum Language
        {
            VB,
            CSharp,
            JScript
        }

        [MandatoryField]
        public string Code { get; set; }
        [MandatoryField]
        public Language CodeLanguage { get; set; }

        public string StartClass { get; set; }
        [MandatoryField]
        public string StartMethod { get; set; }

        private readonly Dictionary<Language, CodeDomProvider> codeDomProviders =
            new Dictionary<Language, CodeDomProvider>{
            {Language.CSharp, new CSharpCodeProvider()},
            {Language.VB, new VBCodeProvider()},
            {Language.JScript, new JScriptCodeProvider()},
        };

        private Type type;

        private Type CompiledType
        {
            get
            {
                if (type == null)
                {
                    CompilerResults results = codeDomProviders[CodeLanguage].CompileAssemblyFromSource(new CompilerParameters(),
                                                                                        new[] { Code });
                    type = results.CompiledAssembly.GetType(StartClass);
                }
                return type;
            }
        }

        [MandatoryField]
        public object ExpectedResult { get; set; }

        public override void Run()
        {
            object actual = CompiledType.GetMethod(StartMethod).Invoke(Activator.CreateInstance(CompiledType), null);
            AssertState.Equal(ExpectedResult, actual);
        }

        public override List<Test> CreateExamples()
        {
            // Do not expose an example of this, since it's dangerous and not for general consumption.
            return new List<Test>();
            //                             {
            //                                 new CustomTest
            //                                {
            //                                    CodeLanguage = Language.CSharp,
            //                                    Code="namespace Blah { class Test { public bool Run() { return true; } } }",
            //                                    ExpectedResult = false,
            //                                    StartClass = "Blah.Test",
            //                                    StartMethod = "Run",
            //                                    TestName = "CustomTest"
            //                                }
            //                             };
        }
    }
}