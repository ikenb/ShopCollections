using Product.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.ApiServices.Interfaces
{
    public interface IPieApiService
    {
        Task<IEnumerable<Pie>> GetPies();
        Task<Pie> GetPie(string id);
        Task<Pie> CreatePie(Pie pie);
        Task<Pie> UpdatePie(Pie pie);
        Task DeletePie(int id);

    }
}
