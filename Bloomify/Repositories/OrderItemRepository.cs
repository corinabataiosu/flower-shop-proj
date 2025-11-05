using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderID(int orderID)
        {
            return _context.OrderItems.Where(oi => oi.orderID == orderID).ToList();
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            Create(orderItem);
        }
        public void DeleteOrderItem(OrderItem orderItem)
        {
            Delete(orderItem);
        }
    }
}
