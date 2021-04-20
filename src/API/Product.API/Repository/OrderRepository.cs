using Product.API.Data;
using Product.API.Interfaces.Repository;
using Product.API.Models;
using System;
using System.Collections.Generic;

namespace Product.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductContext _productContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(ProductContext productContext, ShoppingCart shoppingCart)
        {
            _productContext = productContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = (decimal)_shoppingCart.GetShoppingCartTotal();

            order.OrderSummaries = new List<OrderSummary>();


            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderSummary
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.Id,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderSummaries.Add(orderDetail);
            }

            _productContext.Orders.Add(order);

            _productContext.SaveChanges();
        }
    }
}
