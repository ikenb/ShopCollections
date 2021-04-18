using Product.API.Models;
using System.Collections.Generic;

namespace Product.API.Interfaces.Repository
{
    public interface IPieRepository
    {
        ICollection<Pie> GetPies();
        Pie GetPie(int pieId);
        bool UpdatePie(Pie pie);
        bool AddPie(Pie pie);
        bool DeletePie(Pie id);
        bool PieExists(int id);
        bool Save();
    }

}
