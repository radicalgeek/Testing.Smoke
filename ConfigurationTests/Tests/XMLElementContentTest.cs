using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using RadicalGeek.Common.Text;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public class XMLElementContentTest : XMLElementTestBase
    {
        [MandatoryField]
        public string ExpectedValue { get; set; }

        public override List<Test> CreateExamples()
        {
            return new List<Test>
                       {
                           new XMLElementContentTest
                               {
                                   TestName = "Example config expected value",
                                   Path = @".",
                                   Filename = "InstallationSmokeTest.exe.config",
                                   XmlPath = "/appSettings/add",
                                   ExpectedValue = ""
                               },
                       };
        }

        public override void Run()
        {
            if (!File.Exists(FullFilePath)) throw new AssertionException("File Not Found");
            XmlDocument doc = knownDocuments[FullFilePath];

            IEnumerable<XmlNode> nodeInstances = GetNodesWithName(XmlPath, doc);

            int nodeCount = nodeInstances.Count();
            if (nodeCount > MaximumOccurrences)
                throw new AssertionException(
                    string.Format("{1} instance{2} of {0} found where no more than {3} {4} expected", XmlPath,
                                  nodeCount, nodeCount.pl("", "s"), MaximumOccurrences,
                                  MaximumOccurrences.pl("was", "were")));

            if (nodeCount < MinimumOccurrences)
                throw new AssertionException(
                    string.Format("{1} instance{2} of {0} found where at least {3} {4} expected", XmlPath,
                                  nodeCount, nodeCount.pl("", "s"), MinimumOccurrences,
                                  MinimumOccurrences.pl("was", "were")));

            List<XmlNode> nodesWithMatchingAttributes = new List<XmlNode>();

                nodesWithMatchingAttributes.AddRange(nodeInstances);

            nodeInstances = nodeInstances.Where(n => n.InnerXml == ExpectedValue);

            int nodesCount = nodeInstances.Count();

            if (nodesCount == 0)
                throw new AssertionException(string.Format("No instances of {0} found with the expected content", XmlPath));
            if (nodesCount > 1)
                throw new AssertionException(string.Format("{0} instances of {1} found with the expected content where no more than 1 was expected", nodesCount, XmlPath));
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
    }
}