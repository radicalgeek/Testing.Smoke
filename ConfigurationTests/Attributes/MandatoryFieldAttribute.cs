using System;
using System.Xml.Serialization;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Attributes
{
    [XmlRoot(Namespace = "ConfigurationTests")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class MandatoryFieldAttribute : Attribute
    {
    }
}