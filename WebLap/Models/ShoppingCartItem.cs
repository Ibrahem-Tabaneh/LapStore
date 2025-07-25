namespace WebLap.Models
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImg { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
    }
}
