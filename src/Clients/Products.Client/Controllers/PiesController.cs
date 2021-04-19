using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.ApiServices.Interfaces;
using System.Threading.Tasks;

namespace Products.Client.Controllers
{
    public class PiesController : Controller
    {
        private readonly IPieApiService _pieApiServices;



        public PiesController(IPieApiService pieApiServices)
        {
            _pieApiServices = pieApiServices;
        }

        public async Task<ActionResult> Index()
        {
            var pies = await _pieApiServices.GetPies();

            return View(pies);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
