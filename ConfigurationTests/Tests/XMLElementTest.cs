using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using RadicalGeek.Common.Text;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class XMLElementTest : XMLElementTestBase
    {
        private List<Attribute> expectedValues = new List<Attribute>();

        [MandatoryField]
        public List<Attribute> ExpectedValues
        {
            get { return expectedValues; }
            set { expectedValues = value; }
        }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                           {
                               new XMLElementTest
                                   {
                                       TestName = "Example config expected value",
                                       Path = @".",
                                       Filename = "InstallationSmokeTest.exe.config",
                                       XmlPath = "/appSettings/add",
                                       ExpectedValues =
                                           new List<Attribute>
                                               {
                                                   new Attribute {Name = "key", Value = "Setting"},
                                                   new Attribute
                                                       {
                                                           Name = "value",
                                                           Value = "SettingValue"
                                                       }
                                               }
                                   },
                               new XMLElementTest
                                   {
                                       TestName = "Example web.config expected value",
                                       Path = @".",
                                       Filename = "web.config",
                                       XmlPath = "/appSettings/add",
                                       ExpectedValues =
                                           new List<Attribute>
                                               {
                                                   new Attribute {Name = "key", Value = "Setting"},
                                                   new Attribute
                                                       {
                                                           Name = "value",
                                                           Value = "SettingValue"
                                                       }
                                               },
                                               MinimumOccurrences = 3
                                   },
                           };

        }

        public override void Run()
        {
            if (!File.Exists(FullFilePath)) throw new AssertionException("File Not Found");
            XmlDocument doc = knownDocuments[FullFilePath];

            List<XmlNode> nodeInstances = GetNodesWithName(XmlPath, doc);

            if (nodeInstances.Count > MaximumOccurrences)
                throw new AssertionException(
                    string.Format("{1} instance{2} of {0} found where no more than {3} {4} expected", XmlPath,
                                  nodeInstances.Count, nodeInstances.Count.pl("", "s"), MaximumOccurrences,
                                  MaximumOccurrences.pl("was", "were")));

            if (nodeInstances.Count < MinimumOccurrences)
                throw new AssertionException(
                    string.Format("{1} instance{2} of {0} found where at least {3} {4} expected", XmlPath,
                                  nodeInstances.Count, nodeInstances.Count.pl("", "s"), MinimumOccurrences,
                                  MinimumOccurrences.pl("was", "were")));

            List<int> matchingAttributeCounts = nodeInstances.Select(GetMatchingAttributesCount).ToList();

            if (ExpectedValues.Count > 0 && matchingAttributeCounts.Count(c => c == ExpectedValues.Count) == 0)
                throw new AssertionException(
                    string.Format("No instances of {0} found with all expected attributes matching (most was {1})",
                                  XmlPath,
                                  matchingAttributeCounts.Max()));
        }

        private static List<XmlNode> GetNodesWithName(string xmlPath, XmlNode doc, string path = "")
        {
            var result = new List<XmlNode>();
            foreach (XmlNode n in doc.ChildNodes)
            {
                string nodePath = string.Format("{0}/{1}", path, n.Name);
                if (nodePath.EndsWith(xmlPath)) result.Add(n);
                if (n.HasChildNodes) result.AddRange(GetNodesWithName(xmlPath, n, nodePath));
            }
            return result;
        }

        private int GetMatchingAttributesCount(XmlNode node)
        {
            if (node.Attributes != null)
                return
                    ExpectedValues.Count(
                        expectedValue => node.Attributes.GetNamedItem(expectedValue.Name).Value == expectedValue.Value);
            return 0;
        }

        #region Nested type: Attribute
        [DefaultProperty("Name")]
        public class Attribute
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }
        #endregion
    }
}
