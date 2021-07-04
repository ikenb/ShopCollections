using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;
using PieShop.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Data.Repository
{
    public class PieRepository : IPieRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PieRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _applicationDbContext.Pies.Include(c => c.Category);
            }

        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _applicationDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _applicationDbContext.Pies.FirstOrDefault(p => p.Id == pieId);
        }
    }
}
