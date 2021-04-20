using Product.API.Models;

namespace Product.API.Interfaces.Repository
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }

}
