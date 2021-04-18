using Product.API.Data;
using Product.API.Interfaces.Repository;
using Product.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Product.API.Repository
{
    public class PieRepository : IPieRepository
    {
        private readonly ProductContext _context;

        public PieRepository(ProductContext context)
        {
            _context = context;

        }
        public ICollection<Pie> GetPies()
        {

            return _context.Pies.OrderBy(a => a.Name).ToList();
        }

        public bool AddPie(Pie pie)
        {
            _context.Pies.Add(pie);

            return Save();
        }

        public bool DeletePie(Pie pie)
        {
            _context.Pies.Remove(pie);
            return Save();
        }

        public Pie GetPie(int pieId)
        {
            return _context.Pies.FirstOrDefault(p => p.Id == pieId);
        }

        public bool PieExists(int id)
        {
            return _context.Pies.Any(e => e.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePie(Pie pie)
        {
            _context.Pies.Update(pie);

            return Save();
        }
    }
}
