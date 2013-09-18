using System.ComponentModel;
using System.Xml;
using RadicalGeek.Common.Collections;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public abstract class XMLElementTestBase : FileTest
    {
        protected int maximumOccurences = int.MaxValue;
        protected int minimumOccurences = 1;

        protected static readonly CacheList<string, XmlDocument> knownDocuments =
            new CacheList<string, XmlDocument>(s =>
                                                   {
                                                       var doc = new XmlDocument();
                                                       doc.Load(s);
                                                       return doc;
                                                   });

        [DefaultValue(int.MaxValue)]
        public int MaximumOccurrences
        {
            get { return maximumOccurences; }
            set { maximumOccurences = value; }
        }

        [DefaultValue(1)]
        public int MinimumOccurrences
        {
            get { return minimumOccurences; }
            set { minimumOccurences = value; }
        }

        [MandatoryField]
        public string XmlPath { get; set; }
    }
}