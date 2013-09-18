﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Tests;
using RadicalGeek.Testing.Smoke.TestConfiguration.Tests.Unit.Utils;
using TestConfiguration.Forms;

namespace RadicalGeek.Testing.Smoke.TestConfiguration.Tests.Unit
{
    [TestClass]
    public class TestEditorTest
    {
        private TestEditor_Accessor _TestEditorAccessor;
        public TestContext TestContext { private get; set; }

        [TestInitialize]
        public void MyTestInitialize()
        {
            _TestEditorAccessor = new TestEditor_Accessor();
        }

        [TestMethod]
        public void TestEditorConstructorTest()
        {
            var target = new TestEditor(false);
            Assert.IsNotNull(target);
        }
        [TestMethod]
        public void ConstructorToCreateTestFromTemplateTest()
        {
            bool fromTemplate = true;
            _TestEditorAccessor = new TestEditor_Accessor(fromTemplate);
            Assert.IsNotNull(_TestEditorAccessor._configurationTestSuite);
        }
        [TestMethod]
        public void ConstructorToNotCreateTestFromTemplateTest()
        {
            bool fromTemplate = false;
            _TestEditorAccessor = new TestEditor_Accessor(fromTemplate);
            Assert.IsNull(_TestEditorAccessor._configurationTestSuite);
        }
        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void CreateTestMenusTest()
        {
            _TestEditorAccessor.CreateTestMenus();
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void AddTestToListTest()
        {
            Test test = new FileExistsTest();
            _TestEditorAccessor.AddTestToList(test);
            Assert.AreEqual(1, _TestEditorAccessor.lstListOfTests.Items.Count);
        }

        [TestMethod]
        public void DefaultConstructorTest()
        {
            var target = new TestEditor();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void GetSelectedIndexFromListControlTest()
        {
            var listBox = new ListBox();
            listBox.Items.AddRange(new object[]{new FileExistsTest(), new FolderExistsTest(), new RegistryKeyTest()});      
            listBox.SelectedItem = listBox.Items[2];

            object sender = listBox;
            int[] expected = new int[] { 2 };
            int[] actual = TestEditor_Accessor.GetSelectedIndexFromListControl(sender);
            Assert.AreEqual(expected.Length, actual.Length); 
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void GetSelectedIndicesTest()
        {
            _TestEditorAccessor.lstListOfTests = TestUtil.ListBoxWithThreeItems;
            _TestEditorAccessor.lstListOfTests.SelectedItem = _TestEditorAccessor.lstListOfTests.Items[2];
            int[] expected = new int[] { 2 }; ;
            int[] actual = _TestEditorAccessor.GetSelectedIndices();
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        [Ignore]
        [DeploymentItem("TestConfiguration.exe")]
        public void GetSelectedIndicesBySenderListViewTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.lvwListOfTest.Items[2].Selected = true;

            
            int[] expected = new int[]{2};
            int[] actual = _TestEditorAccessor.GetSelectedIndicesBySender(_TestEditorAccessor.lvwListOfTest);
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void GetTestTypesTest()
        {
            var expected = TestUtil.TestTypes.ToList();
            var actual = TestEditor_Accessor.GetTestTypes();
            var actualTypes = actual.ToList();
            expected.ForEach(t => Assert.IsTrue(actualTypes.Contains(t)));
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void InitializeTestSuiteTest()
        {
            _TestEditorAccessor.InitializeTestSuite();
            Assert.IsNotNull(_TestEditorAccessor._configurationTestSuite);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void LoadTestsFromExamplesTest()
        {
            _TestEditorAccessor.LoadTestsFromExamples();
            Assert.IsNotNull(_TestEditorAccessor._configurationTestSuite);
            Assert.IsNotNull(_TestEditorAccessor._configurationTestSuite.Tests);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void MoveListItemsTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.lstListOfTests.Items.AddRange(TestUtil.ThreeTestObjects);
            _TestEditorAccessor.lstListOfTests.SelectedItem = _TestEditorAccessor.lstListOfTests.Items[1];
            _TestEditorAccessor.MoveListItems("up");
            Assert.AreEqual("FolderExistTest", (_TestEditorAccessor.lstListOfTests.Items[0] as Test).TestName);
            Assert.AreEqual("FileExistTest", (_TestEditorAccessor.lstListOfTests.Items[1] as Test).TestName);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void MoveSelectedItemsTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.lstListOfTests.Items.AddRange(TestUtil.ThreeTestObjects);
            int[] selectedIndices = new int[] { 1 };
            var moveType = TestEditor_Accessor.MoveType.Up;
            _TestEditorAccessor.MoveSelectedItems(selectedIndices, moveType);
            Assert.AreEqual("FolderExistTest", (_TestEditorAccessor.lstListOfTests.Items[0] as Test).TestName);
            Assert.AreEqual("FileExistTest", (_TestEditorAccessor.lstListOfTests.Items[1] as Test).TestName);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void RemoveAllTestsFromListTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.lstListOfTests.Items.AddRange(TestUtil.ThreeTestObjects);
            _TestEditorAccessor._configurationTestSuite = TestUtil.ConfigurationTestSuiteWithThreeTests;
            _TestEditorAccessor.RemoveAllTestsFromList();
            Assert.AreEqual(0, _TestEditorAccessor.lvwListOfTest.Items.Count);
            Assert.AreEqual(0, _TestEditorAccessor.lstListOfTests.Items.Count);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void RemoveTestsFromListTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.lstListOfTests.Items.AddRange(TestUtil.ThreeTestObjects);
            int[] selectedIndices = new int[] { 1 };
            _TestEditorAccessor.RemoveTestsFromList(selectedIndices);
            Assert.AreEqual(2, _TestEditorAccessor.lvwListOfTest.Items.Count);
            Assert.AreEqual(2, _TestEditorAccessor.lstListOfTests.Items.Count);
        }


        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void RunSelectedTestsTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            int[] selectedIndices = new int[]{1};
            _TestEditorAccessor.RunSelectedTests(selectedIndices);
            Assert.IsTrue(_TestEditorAccessor.lvwListOfTest.Items[1].Text != string.Empty);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void RunTestTest()
        {
            _TestEditorAccessor.lvwListOfTest.Items.AddRange(TestUtil.ThreeListViewItems);
            _TestEditorAccessor.RunTest();
            Assert.IsTrue(_TestEditorAccessor.lvwListOfTest.Items[0].Text != string.Empty);
            Assert.IsTrue(_TestEditorAccessor.lvwListOfTest.Items[1].Text != string.Empty);
            Assert.IsTrue(_TestEditorAccessor.lvwListOfTest.Items[2].Text != string.Empty);
        }
        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void ShowIdleStatusTest()
        {
            _TestEditorAccessor.ShowIdleStatus();
            Assert.AreEqual("Ready...", _TestEditorAccessor.tslStatus.Text);
        }
    
        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void ShowStatusTest()
        {
            const string status = "Counting...";
            _TestEditorAccessor.ShowStatus(status);
            Assert.AreEqual(status, _TestEditorAccessor.tslStatus.Text);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void RunTestFromListItemTest()
        {
            var item = TestUtil.ThreeListViewItems[0];
            TestEditor_Accessor.RunTestFromListItem(item);
            Assert.IsTrue(item.Text != string.Empty);
            Assert.IsTrue(item.SubItems[2].Text != string.Empty);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void TestMenu_ClickTest()
        {
            object sender = TestUtil.FileExistTestMenuItem;
            var e = new EventArgs();
            _TestEditorAccessor.TestMenu_Click(sender, e);
            Assert.AreEqual(1, _TestEditorAccessor.lstListOfTests.Items.Count);
        }

        [TestMethod]
        [DeploymentItem("TestConfiguration.exe")]
        public void WriteFileTest()
        {
            _TestEditorAccessor._configurationTestSuite = TestUtil.ConfigurationTestSuiteWithThreeTests;
            string fileName = Path.Combine(TestContext.DeploymentDirectory,string.Format("FILE{0:ddMMyyyyhhmmss}.xml",DateTime.Now));

            _TestEditorAccessor.WriteXmlFile(fileName);
            
            Assert.IsTrue(File.Exists(fileName));
        }

        [TestMethod()]
        [DeploymentItem("TestConfiguration.exe")]
        public void LoadTestsIfNotNullConfigNotInitializedTest()
        {
            _TestEditorAccessor.LoadTestsToList();
            Assert.AreEqual(0, _TestEditorAccessor.lstListOfTests.Items.Count);
        }

        [TestMethod()]
        [DeploymentItem("TestConfiguration.exe")]
        public void LoadTestsIfNotNullConfigInitializedTest()
        {
            _TestEditorAccessor._configurationTestSuite = TestUtil.ConfigurationTestSuiteWithThreeTests;
            _TestEditorAccessor.LoadTestsToList();
            Assert.AreEqual(3, _TestEditorAccessor.lstListOfTests.Items.Count);
        }
    }
}
