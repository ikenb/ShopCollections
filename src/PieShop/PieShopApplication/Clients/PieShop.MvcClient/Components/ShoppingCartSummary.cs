using Microsoft.AspNetCore.Mvc;
using PieShop.Core.Models;
using PieShop.Core.Models.ViewModels;
using PieShop.Data.Repository.Intefaces;

namespace PieShop.MvcClient.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCart = new ShoppingCart();//TODO: still to fix, get ShoppingCart via DI
        }
        public IViewComponentResult Invoke()
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
    }
}
