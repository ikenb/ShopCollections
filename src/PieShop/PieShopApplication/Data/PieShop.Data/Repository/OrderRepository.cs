using PieShop.Core.Models;
using PieShop.Data.Repository.Intefaces;
using PieShop.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace PieShop.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderRepository(ApplicationDbContext applicationDbContext, IShoppingCartRepository shoppingCartRepository)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCartRepository.ShoppingCartItems;
            order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.Id,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _applicationDbContext.Orders.Add(order);

            _applicationDbContext.SaveChanges();
        }
    }
}
