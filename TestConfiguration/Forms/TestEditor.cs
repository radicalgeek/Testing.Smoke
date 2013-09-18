﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigurationTests;
using System.Reflection;
using System.Configuration;
using RadicalGeek.Common.Xml;
using RadicalGeek.Testing.Smoke.ConfigurationTests;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Tests;
using RadicalGeek.Testing.Smoke.TestConfiguration;

namespace TestConfiguration.Forms
{
    public partial class TestEditor : Form
    {
        #region Enums
        private enum MoveType
        {
            Up = -1,
            Down = 1
        }
        #endregion

        #region Private Fields
        private ConfigurationTestSuite _configurationTestSuite;
        private string _filename;
        #endregion

        #region Ctor
        public TestEditor()
        {
            InitializeComponent();
        }
        public TestEditor(ConfigurationTestSuite configurationTestSuite)
            : this()
        {
            _configurationTestSuite = configurationTestSuite;
        }
        public TestEditor(bool fromTemplate)
            : this()
        {
            if (fromTemplate)
                LoadTestsFromExamples();
        }
        #endregion

        #region Workers

        private static IEnumerable<Type> GetTestTypes()
        {
            IEnumerable<Type> testsTypes = typeof(Test).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Test)) && !type.IsAbstract);
            return testsTypes;
        }
        private static IEnumerable<Type> GetPluginTestTypes()
        {
            string defaultPluginPath = Path.Combine(Application.StartupPath, "Plugins");
            List<string> pluginPaths = new List<string>();
            pluginPaths.Add(defaultPluginPath);
            string path = ConfigurationManager.AppSettings["CommaSeparatedPluginPaths"];
            pluginPaths.AddRange((path ?? string.Empty).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList());

            List<Type> testsTypes = new List<Type>();
            IEnumerable<string> paths = pluginPaths.Where(folder => folder.IsValidDirectory());
            foreach (Assembly assembly in paths.SelectMany(folder => Directory.GetFiles(folder).Select(Assembly.LoadFile)))
                testsTypes.AddRange(assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Test)) && !type.IsAbstract));
            return testsTypes;
        }
        private static IEnumerable<Type> GetAllTestTypes()
        {
            List<Type> testTypes = new List<Type>();
            testTypes.AddRange(GetTestTypes());
            testTypes.AddRange(GetPluginTestTypes());
            return testTypes;
        }
        private void LoadTestsFromExamples()
        {
            _configurationTestSuite = new ConfigurationTestSuite();
            IEnumerable<Type> testTypes = GetAllTestTypes();

            foreach (Test test in testTypes.OrderBy(c => c.Name).Select(type => Activator.CreateInstance(type) as Test))
                _configurationTestSuite.Tests.AddRange(test.CreateExamples());
        }
        private void CreateTestMenus()
        {
            foreach (Type type in GetAllTestTypes().OrderBy(c => c.Name))
                CreateTestMenuItem(type);
        }

        private void CreateTestMenuItem(Type type)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem
            {
                Text = type.Name,
                Tag = type
            };
            menuItem.Click += TestMenu_Click;
            tsbTests.DropDownItems.Add(menuItem);
        }
        private void AddTestToList(Test test)
        {
            lstListOfTests.Items.Add(test);
            ListViewItem listViewItem = new ListViewItem
            {
                ImageIndex = -1,
                Tag = test,
                Text = ""
            };
            listViewItem.SubItems.Add(test.ToString());
            listViewItem.SubItems.Add("");
            lvwListOfTest.Items.Add(listViewItem);

            lstListOfTests.SelectedItem = test;
            pgTestConfiguration.SelectedObject = test;

            UpdateUi();
        }
        private void UpdateUi()
        {
            int testCount = lstListOfTests.Items.Count;
            lblTotalTestCount.Text = string.Format(CultureInfo.CurrentUICulture, "{0} Test{1}", testCount, (testCount > 0 ? "s" : ""));
            string testFormat = lstListOfTests.SelectedItem != null ? " [{1}]" : "";
            Text = string.Format(CultureInfo.CurrentUICulture, "Test Configurations Editor - {0}" + testFormat, txtTestName.Text, lstListOfTests.SelectedItem ?? "");
        }
        private void UpdateSeletedItem()
        {
            lstListOfTests.BeginUpdate();
            Test item = lstListOfTests.SelectedItem as Test;
            int index = lstListOfTests.Items.IndexOf(lstListOfTests.SelectedItem);
            lstListOfTests.Items[index] = item;
            lstListOfTests.EndUpdate();
            UpdateUi();
        }
        private void RemoveTestsFromList(int[] selectedIndices)
        {
            for (int i = selectedIndices.GetLowerBound(0); i <= selectedIndices.GetUpperBound(0); i++)
            {
                lstListOfTests.Items.RemoveAt(selectedIndices[i]);
                lvwListOfTest.Items.RemoveAt(selectedIndices[i]);
            }
        }
        private void RemoveAllTestsFromList()
        {
            lstListOfTests.Items.Clear();
            lvwListOfTest.Items.Clear();
            _configurationTestSuite.Tests = null;
        }
        private void InitializeTestSuite()
        {
            _configurationTestSuite = new ConfigurationTestSuite
            {
                Name = txtTestName.Text,
                Description = string.Empty
            };
            _configurationTestSuite.Tests.AddRange(lstListOfTests.Items.Cast<Test>());
        }
        private void SaveCurrentTestFile()
        {
            InitializeTestSuite();
            if (_filename != string.Empty && File.Exists(_filename))
                WriteXmlFile(_filename);
            else
                SaveNewTestFile();
        }
        private void SaveNewTestFile()
        {
            if (_configurationTestSuite == null)
                InitializeTestSuite();

            using (SaveFileDialog dialog = new SaveFileDialog { Title = "Save Test Configuration File", Filter = "XML Configuration File | *.xml" })
            {
                dialog.FileOk += (s, e) =>
                {
                    if (!e.Cancel)
                    {
                        _filename = ((SaveFileDialog)s).FileName;
                        WriteXmlFile(_filename);
                    }
                };
                dialog.ShowDialog();
            }
        }
        private void WriteXmlFile(string fileName)
        {
            string xmlString = _configurationTestSuite.ToXmlString();
            File.WriteAllText(fileName, xmlString, Encoding.Unicode);
        }

        private void RunTest()
        {
            SwitchToTestRunView();
            foreach (ListViewItem item in lvwListOfTest.Items)
                RunTestFromListItem(item);
            ShowIdleStatus();
        }
        private void RunSelectedTests(int[] selectedIndices)
        {
            SwitchToTestRunView();
            for (int i = selectedIndices.GetLowerBound(0); i <= selectedIndices.GetUpperBound(0); i++)
                RunTestFromListItem(lvwListOfTest.Items[selectedIndices[i]]);
            ShowIdleStatus();
        }
        private void SwitchToTestRunView()
        {
            tabMain.SelectedTab = tpgTestRun;
            tabMain.Refresh();
            ShowStatus("Running Tests...");
        }
        private static void RunTestFromListItem(ListViewItem item)
        {
            Test test = item.Tag as Test;
            try
            {
                test.Run();
                item.Text = "Pass";
                item.ImageIndex = 0;
            }
            catch (Exception e)
            {
                item.Text = "Fail";
                item.ImageIndex = 1;
                item.SubItems[2].Text = string.Format(CultureInfo.CurrentUICulture, "{0} - {1}", e.Source, e.Message);
            }
        }
        private void MoveSelectedItems(int[] selectedIndices, MoveType moveType)
        {
            for (int i = selectedIndices.GetLowerBound(0); i <= selectedIndices.GetUpperBound(0); i++)
            {
                object item = lstListOfTests.Items[selectedIndices[i]];
                lstListOfTests.Items.RemoveAt(selectedIndices[i]);
                lstListOfTests.Items.Insert(selectedIndices[i] + ((int)moveType), item);
                lstListOfTests.SelectedItem = item;

                ListViewItem lstItem = lvwListOfTest.Items[selectedIndices[i]];
                lvwListOfTest.Items.RemoveAt(selectedIndices[i]);
                lvwListOfTest.Items.Insert(selectedIndices[i] + ((int)moveType), lstItem);
            }
        }
        private void MoveListItems(string action)
        {
            int[] selectedIndices = GetSelectedIndices();

            switch (action)
            {
                case "up":
                    if (selectedIndices[0] != 0)
                        MoveSelectedItems(selectedIndices, MoveType.Up);
                    break;
                case "down":
                    if (selectedIndices[selectedIndices.Length - 1] != lstListOfTests.Items.Count - 1)
                        MoveSelectedItems(selectedIndices, MoveType.Down);
                    break;
                default:
                    break;
            }
        }
        private int[] GetSelectedIndices()
        {
            return lstListOfTests.SelectedIndices.Cast<int>().ToArray();
        }
        private int[] GetSelectedIndicesBySender(object sender)
        {
            if (sender is Control)
                return GetSelectedIndexFromListControl(sender);
            if (sender is ToolStripItem)
            {
                ToolStripItem item = sender as ToolStripItem;
                if (item.Owner is ContextMenuStrip)
                    return GetSelectedIndicesBySender(((ContextMenuStrip)item.Owner).SourceControl);
            }
            return GetSelectedIndicesBySender(lstListOfTests);
        }
        private static int[] GetSelectedIndexFromListControl(object sender)
        {
            IList listOfIndexes;
            if (sender is ListBox)
                listOfIndexes = ((ListBox)sender).SelectedIndices;
            else
                listOfIndexes = new List<int>();

            if (sender is ListView)
                listOfIndexes = ((ListView)sender).SelectedIndices;

            return listOfIndexes.Cast<int>().ToArray();
        }
        private void ShowStatus(string status)
        {
            tslStatus.Text = status;
        }
        private void ShowIdleStatus()
        {
            ShowStatus("Ready...");
        }
        private void LoadTestsToList()
        {
            if (_configurationTestSuite != null)
                foreach (Test test in _configurationTestSuite.Tests)
                    AddTestToList(test);
        }
        #endregion

        #region Event Handlers
        private void TestEditor_Load(object sender, EventArgs e)
        {
            CreateTestMenus();
        }
        private void TestEditor_Shown(object sender, EventArgs e)
        {
            LoadTestsToList();
        }

        private void TestMenu_Click(object sender, EventArgs e)
        {
            Type senderType = ((ToolStripItem)sender).Tag as Type;
            Test test = Activator.CreateInstance(senderType) as Test;

            AddTestToList(test);
        }
        private void txtTestName_TextChanged(object sender, EventArgs e)
        {
            UpdateUi();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lstListOfTests.Items.Count > 0)
                if (MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            Close();
        }
        private void btnMoveItem_Click(object sender, EventArgs e)
        {
            string action = (sender as Control).Tag.ToString().ToLowerInvariant();
            MoveListItems(action);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentTestFile();
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveNewTestFile();
        }
        private void lstListOfTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgTestConfiguration.SelectedObject = lstListOfTests.SelectedItem as Test;
            UpdateUi();
        }
        private void lvwListOfTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwListOfTest.SelectedIndices.Count > 0)
                lstListOfTests.SelectedItem = lstListOfTests.Items[lvwListOfTest.SelectedIndices[0]];
        }
        private void pgTestConfiguration_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label != null)
                if (e.ChangedItem.Label == "TestName")
                    UpdateSeletedItem();
        }
        private void mnuRemoveTest_Click(object sender, EventArgs e)
        {
            int[] selectedIndices = GetSelectedIndicesBySender(sender);
            RemoveTestsFromList(selectedIndices);
        }
        private void mnuRemoveAllTests_Click(object sender, EventArgs e)
        {
            RemoveAllTestsFromList();
        }
        private void mnuMove_Click(object sender, EventArgs e)
        {
            string action = (sender as ToolStripMenuItem).Tag.ToString().ToLowerInvariant();
            MoveListItems(action);
        }
        private void mnuRunSelectedTest_Click(object sender, EventArgs e)
        {
            int[] selectedIndices = GetSelectedIndicesBySender(sender);
            RunSelectedTests(selectedIndices);
        }
        private void mnuRunAllTests_Click(object sender, EventArgs e)
        {
            RunTest();
        }
        private void mnuSaveAndRun_Click(object sender, EventArgs e)
        {
            SaveCurrentTestFile();
            RunTest();
        }
        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveNewTestFile();
        }
        #endregion
    }
}
