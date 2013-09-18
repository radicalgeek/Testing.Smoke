using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using RadicalGeek.Common.Boolean;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class FolderExistsTest : Test
    {
        private bool _shouldExist = true;

        [MandatoryField]
        [Description("Directory of file")]
        public string Path { get; set; }

        [DefaultValue(true)]
        [Description("True to check if file exist")]
        public bool ShouldExist 
        {
            get { return _shouldExist; }
            set { _shouldExist = value; }
        }

        public override void Run()
        {
            if (Directory.Exists(Path) != ShouldExist)
                throw new AssertionException(string.Format("Folder was {0}present", ShouldExist.IfTrue("not ")));
        }

        public override List<Test> CreateExamples()
        {
           return new List<Test>
                             {
                                 new FolderExistsTest{Path=@"c:\Windows\System32",TestName = "Check System32 folder"}
                             };
        }
    }
}