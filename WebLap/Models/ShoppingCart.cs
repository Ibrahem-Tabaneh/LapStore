namespace WebLap.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            LstItems=new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> LstItems { get; set; }
        public decimal Total  { get; set; }
    }
}
