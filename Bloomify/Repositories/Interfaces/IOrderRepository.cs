using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        // Define methods specific to Order repository here
        IEnumerable<Order> GetOrdersByUserID(int userID);
        Order GetOrderByOrderID(int orderID);
        IEnumerable<Order> GetAllOrders();
        // Task AddOrderAsync(Order order);
        // Task UpdateOrderAsync(Order order);
        // Task DeleteOrderAsync(int orderId);
    }
}
