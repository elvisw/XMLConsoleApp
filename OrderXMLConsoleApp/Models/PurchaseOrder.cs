using System.Xml.Serialization;

namespace OrderXMLConsoleApp.Models
{
    // The XmlRoot attribute allows you to set an alternate name
    // (PurchaseOrder) for the XML element and its namespace. By
    // default, the XmlSerializer uses the class name. The attribute
    // also allows you to set the XML namespace for the element. Lastly,
    // the attribute sets the IsNullable property, which specifies whether
    // the xsi:null attribute appears if the class instance is set to
    // a null reference.
    [XmlRoot("PurchaseOrder", Namespace = "http://www.cpandl.com",
    IsNullable = false)]
    public class PurchaseOrder
    {
        public Address ShipTo { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime OrderDate { get; set; }
        // The XmlArray attribute changes the XML element name
        // from the default of "OrderedItems" to "Items".
        [XmlArray("Items")]
        public List<OrderedItem>? OrderedItems { get; set; }
        public decimal SubTotal
        {
            get
            {
                if (OrderedItems != null)
                {
                    return OrderedItems.Sum(t => t.LineTotal);
                }
                else
                {
                    return 0;
                }
            }
        }
        public decimal ShipCost { get; set; }
        public decimal TotalCost { get => SubTotal + ShipCost; }
    }
}