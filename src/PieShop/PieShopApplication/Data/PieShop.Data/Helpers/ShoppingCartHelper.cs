using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Core.Models;
using PieShop.Data.Repository;
using System;

namespace PieShop.Data.Helpers
{
    public static class ShoppingCartHelper
    {
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }
    }
}
