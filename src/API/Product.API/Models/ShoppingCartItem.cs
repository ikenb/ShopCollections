using System.ComponentModel.DataAnnotations;

namespace Product.API.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Pie Pie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
