using System;
using System.Xml.Serialization;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes
{
    [XmlRoot(Namespace = "ConfigurationTests")]
    [AttributeUsage(AttributeTargets.Class)]
    public class HiddenFromUiAttribute : Attribute
    {
    }
}