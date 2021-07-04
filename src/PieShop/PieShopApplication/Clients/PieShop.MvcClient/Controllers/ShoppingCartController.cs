using Microsoft.AspNetCore.Mvc;
using PieShop.Core.Models;
using PieShop.Core.Models.ViewModels;
using PieShop.Data.Repository.Intefaces;
using PieShop.Data.Repository.Interfaces;
using System.Linq;

namespace PieShop.MvcClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _pieRepository = pieRepository;
            _shoppingCart = new ShoppingCart();//TODO: find a better to inject this dependency

            _shoppingCartRepository = shoppingCartRepository;
        }

        public ViewResult Index()
        {
            var items = _shoppingCartRepository.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCartRepository.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.Id == pieId);

            if (selectedPie != null)
            {
                _shoppingCartRepository.AddToCart(selectedPie, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.Id == pieId);

            if (selectedPie != null)
            {
                _shoppingCartRepository.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }
    }
}
