using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepositoryAsync<Category>
    {
        void Update(Category category);
    }
}
