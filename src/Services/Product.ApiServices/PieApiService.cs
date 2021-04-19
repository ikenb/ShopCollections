using Product.API.Models;
using Product.ApiServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.ApiServices
{
    public class PieApiService : IPieApiService
    {

        public async Task<IEnumerable<Pie>> GetPies()
        {
            var pieList = new List<Pie>();

            pieList.Add(new Pie { Id = 1, Name = "Strawberry Pie", Price = 16.95, Description = "Icing carrot", TypeId = 2 });

            pieList.Add(new Pie { Id = 2, Name = "Cheese cake", Price = 18.95, Description = "Jelly-o cheesecake", TypeId = 1 });
            pieList.Add(new Pie { Id = 3, Name = "Rhubarb Pie", Price = 17.9, Description = "Sweet roll marzipan marshmallow", TypeId = 1 });
            pieList.Add(new Pie { Id = 4, Name = "Pumpkin Pie", Price = 19.95, Description = "Chocolate cake gingerbread tootsie", TypeId = 2 });
            pieList.Add(new Pie { Id = 5, Name = "Strawberry Pie", Price = 15.95, Description = "Icing carrot", TypeId = 3 });
            pieList.Add(new Pie { Id = 6, Name = "Cheese cake", Price = 18.95, Description = "Jelly-o cheesecake", TypeId = 1 });
            pieList.Add(new Pie { Id = 7, Name = "Rhubarb Pie", Price = 15.95, Description = "Sweet roll marzipan marshmallow", TypeId = 2 });
            pieList.Add(new Pie { Id = 8, Name = "Pumpkin Pie", Price = 12.95, Description = "Chocolate cake gingerbread tootsie", TypeId = 2 });


            return await Task.FromResult(pieList);
        }

        public Task DeletePie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> GetPie(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> CreatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> UpdatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }
    }
}
