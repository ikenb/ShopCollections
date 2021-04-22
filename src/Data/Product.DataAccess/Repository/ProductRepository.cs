using Eshop.DataAccess.Data;
using Eshop.DataAccess.Repository.IRepository;
using Eshop.Models;
using System.Linq;

namespace Eshop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(Product product)
        {
            var dbProduct = _dbContext.Products.FirstOrDefault(s => s.Id == product.Id);
            if (dbProduct != null)
            {
                if (dbProduct.ImageUrl != null)
                {
                    dbProduct.ImageUrl = product.ImageUrl;
                }

                dbProduct.Price = product.Price;


                dbProduct.Description = product.Description;
                dbProduct.CategoryId = product.CategoryId;


            }
        }
    }
}
