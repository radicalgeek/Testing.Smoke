using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using RadicalGeek.Common.Xml;
using RadicalGeek.Testing.Smoke.ConfigurationTests;

namespace TestConfiguration.Forms
{
    public partial class MDI : Form
    {
        #region Ctor
        public MDI()
        {
            InitializeComponent();
        } 
        #endregion

        #region Workers
        private static ConfigurationTestSuite GetConfigurationSuiteFromFile(string fileName)
        {
            ConfigurationTestSuite configurationTestSuite = null;
            try
            {
                string xml = File.ReadAllText(fileName,Encoding.Unicode);
                configurationTestSuite = xml.ToObject<ConfigurationTestSuite>();
            }
            catch
            {
                MessageBox.Show("Unable to open file. Please check file format is Unicode.", "Error While Loading File");
            }
            return configurationTestSuite;
        } 
        private void LaunchNewTestEditor(bool loadTemplates)
        {
            var editor = new TestEditor(loadTemplates) { MdiParent = this };
            editor.Show();
        }        
        private void LaunchNewTestEditor(ConfigurationTestSuite testSuite)
        {
            if (testSuite != null)
            {
                var editor = new TestEditor(testSuite) { MdiParent = this };
                editor.Show();
            }
        }      
        private void OpenConfigurationFile()
        {
            using (var dialog = new OpenFileDialog() { Filter = "XML Configuration File |*.xml" })
            {
                dialog.FileOk += (s, ce) =>
                {
                    if (!ce.Cancel)
                    {
                        var testSuite = GetConfigurationSuiteFromFile(dialog.FileName);
                        LaunchNewTestEditor(testSuite);
                    }
                };
                dialog.ShowDialog();
            }
        }

        #endregion

        #region Event Handlers
        private void mnuNewConfiguration_Click(object sender, EventArgs e)
        {
            LaunchNewTestEditor(false);
        }
        private void mnuNewConfigurationWithTemplate_Click(object sender, EventArgs e)
        {
            LaunchNewTestEditor(true);
        }
        private void mnuOpenConfigurationFile_Click(object sender, EventArgs e)
        {
            OpenConfigurationFile();
        }

        private void MDI_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            foreach (string s in fileNames)
            {
                var testSuite = GetConfigurationSuiteFromFile(s);
                LaunchNewTestEditor(((ConfigurationTestSuite)testSuite));
            }
        }
        private void MDI_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }
        #endregion

    }
}
