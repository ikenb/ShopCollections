using Eshop.DataAccess.Data;
using Eshop.DataAccess.Repository.IRepository;
using Eshop.Models;
using System.Linq;

namespace Eshop.DataAccess.Repository
{
    public class CategoryRepository : RepositoryAsync<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(Category category)
        {
            var product = _dbContext.Categories.FirstOrDefault(s => s.Id == category.Id);

            if (product != null)
            {
                product.Name = category.Name;
                _dbContext.SaveChanges();

            }
        }
    }
}
