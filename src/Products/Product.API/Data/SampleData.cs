using Product.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product.API.Data
{
    public class SampleData
    {

        public IEnumerable<PieType> AllTypes =>
           new List<PieType>
           {
                new PieType{Id=1, TypeName="Fruit" },
                new PieType{Id=2, TypeName="Cheese"},
                new PieType{Id=3, TypeName="Cream"}
           };

        public IEnumerable<Pie> AllPies =>
            new List<Pie>
            {
                new Pie {Id = 1, Name="Strawberry Pie", Price=15.95M, Description="Icing carrot", Type = AllTypes.ToList()[0]},
                new Pie {Id = 2, Name="Cheese cake", Price=18.95M, Description="Jelly-o cheesecake", Type = AllTypes.ToList()[1]},
                new Pie {Id = 3, Name="Rhubarb Pie", Price=15.95M, Description="Sweet roll marzipan marshmallow", Type = AllTypes.ToList()[0]},
                new Pie {Id = 4, Name="Pumpkin Pie", Price=12.95M, Description="Chocolate cake gingerbread tootsie", Type= AllTypes.ToList()[0]},
                new Pie {Id = 5, Name="Strawberry Pie", Price=15.95M, Description="Icing carrot", Type = AllTypes.ToList()[0]},
                new Pie {Id = 6, Name="Cheese cake", Price=18.95M, Description="Jelly-o cheesecake", Type = AllTypes.ToList()[1]},
                new Pie {Id = 7, Name="Rhubarb Pie", Price=15.95M, Description="Sweet roll marzipan marshmallow", Type = AllTypes.ToList()[0]},
                new Pie {Id = 8, Name="Pumpkin Pie", Price=12.95M, Description="Chocolate cake gingerbread tootsie", Type= AllTypes.ToList()[2]}
            };
    }
}
