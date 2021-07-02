using Eshop.DataAccess.Data;
using Eshop.DataAccess.Repository.IRepository;

namespace Eshop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            ApplicationUser = new ApplicationUserRepository(_dbContext);
            StoredProcedureCall = new StoredProcedureCall(_dbContext);
            OrderDetails = new OrderDetailsRepository(_dbContext);
            OrderHeader = new OrderHeaderRepository(_dbContext);
            ShoppingCart = new ShoppingCartRepository(_dbContext);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IStoredProcedureCall StoredProcedureCall { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
