using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Models
{
    public class PieType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Pie> Pies { get; set; }

    }
}
