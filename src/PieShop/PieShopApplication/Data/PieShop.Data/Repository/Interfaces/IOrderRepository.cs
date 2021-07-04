using PieShop.Core.Models;

namespace PieShop.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
