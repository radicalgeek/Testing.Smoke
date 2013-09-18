using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RadicalGeek.Testing.Smoke.ConfigurationTests;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Tests;

namespace RadicalGeek.Testing.Smoke.TestConfiguration.Tests.Unit.Utils
{
    public static class TestUtil
    {
        public static ListBox ListBoxWithThreeItems
        {
            get
            {
                var listBox = new ListBox();
                listBox.Items.AddRange(ThreeTestObjects);      
                return listBox;
            }
        }

        public static object[] ThreeTestObjects
        {
            get
            {
                return new object[] 
                { 
                      new FileExistsTest{TestName = "FileExistTest"}
                    , new FolderExistsTest{TestName = "FolderExistTest"}
                    , new RegistryKeyTest{TestName = "RegistryKeyTest"} 
                };
            }
        }
        public static ListViewItem[] ThreeListViewItems
        {
            get
            {
                return new[]
                {
                     new ListViewItem(new[]{"",((Test)ThreeTestObjects[0]).TestName,""}) {Tag = ThreeTestObjects[0]}
                    ,new ListViewItem(new[]{"",((Test)ThreeTestObjects[1]).TestName,""}) {Tag = ThreeTestObjects[1]}
                    ,new ListViewItem(new[]{"",((Test)ThreeTestObjects[2]).TestName,""}) {Tag = ThreeTestObjects[2]}
                };
            }
        }
        public static ListView ListViewWithThreeItems
        {
            get
            {
                var listview = new ListView();
                listview.Items.AddRange(ThreeListViewItems);
                return listview;
            }
        }
        public static ToolStripItem MenuItemInContextMenu
        {
            get
            {
                using (var contextMenu = new ContextMenuStrip())
                {
                    contextMenu.Items.AddRange(new ToolStripItem[] { new ToolStripMenuItem(), new ToolStripMenuItem() });
                    var listView = ListViewWithThreeItems;
                    listView.ContextMenuStrip = contextMenu;
                    return contextMenu.Items[0];
                }
            }
        }
        public static IEnumerable<Type> TestTypes
        {
            get
            {
                return typeof(Test).Assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(typeof(Test)) && !type.IsAbstract);
            }
        }
        public static IEnumerable<Test> ThreeTestItems
        {
            get
            {
                return new List<Test>
                {
                     new FileExistsTest{TestName = "FileExistTest"}
                    , new FolderExistsTest{TestName = "FolderExistTest"}
                    , new RegistryKeyTest{TestName = "RegistryKeyTest"} 
                };
            }
        }
        public static ToolStripMenuItem FileExistTestMenuItem
        {
            get
            {
                var type = typeof(FileExistsTest);
                return  new ToolStripMenuItem
                {
                    Text = type.Name,
                    Tag = type
                };
            }
        }
        public static ConfigurationTestSuite ConfigurationTestSuiteWithThreeTests
        {
            get
            {
                var test = new ConfigurationTestSuite
                                {
                                    Name = "New Tests",
                                    Description = "New Tests"
                                };
                
                test.Tests.AddRange(ThreeTestItems);
                return test;
            }
        }
        public static ConfigurationTestSuite ConfigurationTestSuiteWithAllExamples
        {
            get
            {
                var configurationTestSuite = new ConfigurationTestSuite();
                var testTypes = TestTypes;

                foreach (var type in testTypes)
                {
                    Test test = (Test)Activator.CreateInstance(type);
                    configurationTestSuite.Tests.AddRange(test.CreateExamples());
                }
                return configurationTestSuite;
            }
        }
    }
}
