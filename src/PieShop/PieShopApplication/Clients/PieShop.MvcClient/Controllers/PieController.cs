using Microsoft.AspNetCore.Mvc;
using PieShop.Core.Models.ViewModels;
using PieShop.Data.Repository.IRepository;

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

        public ViewResult List()
        {
            PiesListViewModel piesLisViewModel = new PiesListViewModel();
            piesLisViewModel.Pies = _pieRepository.AllPies;
            piesLisViewModel.CurrentCategory = "Cheese Cakes";

            return View(piesLisViewModel);
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
