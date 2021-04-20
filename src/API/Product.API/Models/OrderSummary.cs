using System.ComponentModel.DataAnnotations;

namespace Product.API.Models
{
    public class OrderSummary
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PieId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Pie Pie { get; set; }
        public Order Order { get; set; }
    }
}
