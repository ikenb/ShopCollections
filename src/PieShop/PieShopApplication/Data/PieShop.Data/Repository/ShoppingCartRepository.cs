using Microsoft.EntityFrameworkCore;
using PieShop.Core.Models;
using PieShop.Data.Repository.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace PieShop.Data.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public string ShoppingCartId { get; set; }

        public ShoppingCartRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }


        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCartItem =
                    _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.Id == pie.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1

                };

                _applicationDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _applicationDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.Id == pie.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _applicationDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _applicationDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _applicationDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _applicationDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }


    }
}
