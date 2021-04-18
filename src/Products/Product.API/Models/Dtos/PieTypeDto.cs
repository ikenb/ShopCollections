using System.Collections.Generic;

namespace Product.API.Models.Dtos
{
    public class PieTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public List<Pie> Pies { get; set; }
    }
}
