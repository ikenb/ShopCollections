using PieShop.Core.Models;
using System.Collections.Generic;

namespace PieShop.Data.Repository.IRepository
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
    }
}
