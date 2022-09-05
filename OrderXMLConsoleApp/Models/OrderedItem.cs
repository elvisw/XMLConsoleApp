namespace OrderXMLConsoleApp.Models
{
    public class OrderedItem
    {
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get => UnitPrice * Quantity; }
    }
}