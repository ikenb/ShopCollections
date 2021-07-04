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
    }
}
