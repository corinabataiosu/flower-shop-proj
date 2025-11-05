using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersByUserID(int userID);
        void CreateOrder(Order order);
        Order GetOrderById(int orderId);

    }
}
