using System.Xml;
using System.Xml.Serialization;

namespace OrderXMLConsoleApp.Models
{
    public class Address
    {
        // The XmlAttribute attribute instructs the XmlSerializer to serialize the
        // Name field as an XML attribute instead of an XML element (XML element is
        // the default behavior).
        [XmlAttribute]
        public string? Name { get; set; }
        public string? Line1 { get; set; }

        // Setting the IsNullable property to false instructs the
        // XmlSerializer that the XML attribute will not appear if
        // the City field is set to a null reference.
        [XmlElement(IsNullable = false)]
        public string City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
    }
}