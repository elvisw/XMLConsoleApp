using OrderXMLConsoleApp.Models;
using System.Xml.Serialization;

namespace OrderXMLConsoleApp
{
    public class XMLReadAndWrite
    {
        public static void CreatePO(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            var serializer = new XmlSerializer(typeof(PurchaseOrder));
            var writer = new StreamWriter(filename);
            var po = new PurchaseOrder();

            // Creates an address to ship and bill to.
            var billAddress = new Address
            {
                Name = "Teresa Atkinson",
                Line1 = "1 Main St.",
                City = "AnyTown",
                State = "WA",
                Zip = "00000"
            };
            // Sets ShipTo and BillTo to the same addressee.
            po.ShipTo = billAddress;
            po.OrderDate = DateTime.Now;

            // Creates an OrderedItem.
            var i1 = new OrderedItem
            {
                ItemName = "Widget S",
                Description = "Small widget",
                UnitPrice = 5.23m,
                Quantity = 3
            };

            // Inserts the item into the array.
            var items = new List<OrderedItem> { i1 };
            po.OrderedItems = items;
            // Calculate the total cost.
            po.ShipCost = 12.51m;
            // Serializes the purchase order, and closes the TextWriter.
            serializer.Serialize(writer, po);
            writer.Close();
        }

        public static void ReadPO(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to be deserialized.
            var serializer = new XmlSerializer(typeof(PurchaseOrder));

            // If the XML document has been altered with unknown
            // nodes or attributes, handles them with the
            // UnknownNode and UnknownAttribute events.
            serializer.UnknownNode+= new
            XmlNodeEventHandler(Serializer_UnknownNode);
            serializer.UnknownAttribute+= new
            XmlAttributeEventHandler(Serializer_UnknownAttribute);

            // A FileStream is needed to read the XML document.
            var fs = new FileStream(filename, FileMode.Open);

            // Declares an object variable of the type to be deserialized.
            // Uses the Deserialize method to restore the object's state
            // with data from the XML document. */
            var po = (PurchaseOrder?)serializer.Deserialize(fs);

            if (po != null)
            {
                // Reads the order date.
                Console.WriteLine($"OrderDate: {po.OrderDate:D}");

                // Reads the shipping address.
                Address shipTo = po.ShipTo;
                ReadAddress(shipTo, "Ship To:");
                // Reads the list of ordered items.
                var items = po.OrderedItems;

                if (items != null)
                {
                    Console.WriteLine("Items to be shipped:");
                    foreach (OrderedItem oi in items)
                    {
                        Console.WriteLine("\t"+
                        oi.ItemName + "\t" +
                        oi.Description + "\t" +
                        oi.UnitPrice + "\t" +
                        oi.Quantity + "\t" +
                        oi.LineTotal);
                    }
                }
                else
                {
                    Console.WriteLine("There is no item in this order.");
                }

                // Reads the subtotal, shipping cost, and total cost.
                Console.WriteLine(
                "\n\t\t\t\t\t Subtotal\t" + po.SubTotal +
                "\n\t\t\t\t\t Shipping\t" + po.ShipCost +
                "\n\t\t\t\t\t Total\t\t" + po.TotalCost
                );
            }
            else
            {
                Console.WriteLine($"XML File({filename}) has no PurchaseOrder.");
            }

        }

        protected static void ReadAddress(Address a, string label)
        {
            // Reads the fields of the Address.
            Console.WriteLine(label);
            Console.Write("\t"+
            a.Name +"\n\t" +
            a.Line1 +"\n\t" +
            a.City +"\t" +
            a.State +"\n\t" +
            a.Zip +"\n");
        }

        protected static void Serializer_UnknownNode
        (object? sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" +   e.Name + "\t" + e.Text);
        }

        protected static void Serializer_UnknownAttribute
        (object? sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}