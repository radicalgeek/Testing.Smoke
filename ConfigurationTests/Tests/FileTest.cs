using System.ComponentModel;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class FileTest : Test
    {
        private string _path = ".";

        [DefaultValue(".")]
        [Description("Directory of file")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        [MandatoryField]
        [Description("Name of file")]
        public string Filename { get; set; }

        protected string FullFilePath
        {
            get
            {
                return System.IO.Path.Combine(Path, Filename);
            }
        }
    }
}