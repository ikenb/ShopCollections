using PieShop.Core.Models;
using PieShop.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace PieShop.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _applicationDbContext.Categories;
            }
        }
    }
}
