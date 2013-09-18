using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using RadicalGeek.Common.Boolean;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class FileExistsTest : FileTest
    {
        private bool _shouldExist = true;

        [DefaultValue(true)]
        [Description("True to check if file exist")]
        public bool ShouldExist
        {
            get{return _shouldExist;}
            set{_shouldExist = value;}
        }

        public override void Run()
        {
            AssertState.Equal(ShouldExist, File.Exists(FullFilePath), string.Format("File was {0}present", ShouldExist.IfTrue("not ")));
        }

        public override List<Test> CreateExamples()
        {
                return new List<Test>
                             {
                                 new FileExistsTest
                                     {
                                         TestName = "Example executable",
                                         Path = @"D:\Folder\Path",
                                         Filename = "Example.exe",
                                         ShouldExist = true
                                     },
                                 new FileExistsTest
                                     {
                                         TestName = "Example config",
                                         Path = @"D:\Folder\Path",
                                         Filename = "Example.exe.config",
                                         ShouldExist = true
                                     }
                             };
        }
    }
}
