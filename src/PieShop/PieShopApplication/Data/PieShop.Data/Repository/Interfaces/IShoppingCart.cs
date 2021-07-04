using PieShop.Core.Models;
using System;
using System.Collections.Generic;

namespace PieShop.Data.Repository.Intefaces
{
    public interface IShoppingCartRepository
    {

        List<ShoppingCartItem> ShoppingCartItems { get; set; }
        void AddToCart(Pie pie, int amount);
        int RemoveFromCart(Pie pie);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
    }


}
