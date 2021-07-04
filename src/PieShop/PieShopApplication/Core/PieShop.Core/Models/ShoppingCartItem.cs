namespace PieShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Pie Pie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
