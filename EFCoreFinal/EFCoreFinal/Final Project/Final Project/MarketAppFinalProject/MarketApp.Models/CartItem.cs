namespace MarketApp.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProdId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
