using Eshop.DataAccess.Repository.IRepository;
using Eshop.Models;
using Eshop.Models.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            CategoryVM categoryVM = new CategoryVM()
            {
                Categories = await _unitOfWork.Category.GetAllAsync()
            };

            var count = categoryVM.Categories.Count();
            categoryVM.Categories = categoryVM.Categories.OrderBy(p => p.Name)
                .Skip((productPage - 1) * 2).Take(2).ToList();

            categoryVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = 2,
                TotalItem = count,
                urlParam = "/Admin/Category/Index?productPage=:"
            };

            return View(categoryVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                //create
                return View(category);
            }
            //edit
            category = await _unitOfWork.Category.GetAsync(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    await _unitOfWork.Category.AddAsync(category);

                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        #region Ajax API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _unitOfWork.Category.GetAllAsync();
            return Json(new { data = allCategories });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Category.GetAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Error deleting Category";
                return Json(new { success = false, message = "Error while deleting" });
            }

            await _unitOfWork.Category.RemoveAsync(category);
            _unitOfWork.Save();

            TempData["Success"] = "Category successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}