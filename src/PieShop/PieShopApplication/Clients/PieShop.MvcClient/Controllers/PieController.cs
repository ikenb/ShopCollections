using Microsoft.AspNetCore.Mvc;
using PieShop.Core.Models;
using PieShop.Core.Models.ViewModels;
using PieShop.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.MvcClient.Controllers
{
    public class PieController : Controller
    {

        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //public ViewResult List()
        //{
        //    PiesListViewModel piesLisViewModel = new PiesListViewModel();
        //    piesLisViewModel.Pies = _pieRepository.AllPies;
        //    piesLisViewModel.CurrentCategory = "Cheese Cakes";

        //    return View(piesLisViewModel);
        //}
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.Id);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.Name == category)
                    .OrderBy(p => p.Id);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == category)?.Name;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);

            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
