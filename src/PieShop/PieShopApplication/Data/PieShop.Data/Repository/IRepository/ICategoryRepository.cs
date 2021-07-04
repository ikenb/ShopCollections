using PieShop.Core.Models;
using System.Collections.Generic;

namespace PieShop.Data.Repository.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
