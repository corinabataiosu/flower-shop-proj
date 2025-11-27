using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Repositories
{
    public class OrderRepository :  RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public Order GetOrderByOrderID(int orderID)
        {
            return _context.Orders
                   .Include(o => o.OrderItems)
                       .ThenInclude(oi => oi.Products)
                   .Include(o => o.ShippingDetails)
                   .Include(o => o.Users)
                   .FirstOrDefault(o => o.OrderID == orderID);
        }

        public IEnumerable<Order> GetOrdersByUserID(int userID)
        {
            return _context.Orders.Where(o => o.userID == userID).ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Users)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Products)
                .Include(o => o.ShippingDetails)
                .ToList();
        }
    }
}
