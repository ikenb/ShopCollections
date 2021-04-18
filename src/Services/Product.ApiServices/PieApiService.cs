using Product.API.Models;
using Product.ApiServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.ApiServices
{
    public class PieApiService : IPieApiService
    {
        public Task<Pie> CreatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> GetPie(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Pie>> GetPies()
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> UpdatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }
    }
}
