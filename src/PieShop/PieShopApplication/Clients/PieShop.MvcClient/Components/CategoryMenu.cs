using Microsoft.AspNetCore.Mvc;
using PieShop.Data.Repository.Interfaces;
using System.Linq;

namespace PieShop.MvcClient.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.Name);

            return View(categories);
        }
    }
}
