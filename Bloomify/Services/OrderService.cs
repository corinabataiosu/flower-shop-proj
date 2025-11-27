using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;
using Bloomify.Repositories;
using NuGet.Protocol.Core.Types;

namespace Bloomify.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public OrderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public IEnumerable<Order> GetOrdersByUserID(int userID)
        {
            return _repositoryWrapper.OrderRepository.GetOrdersByUserID(userID);
        }

        public void CreateOrder(Order order)
        {
            _repositoryWrapper.OrderRepository.Create(order);
            _repositoryWrapper.Save();
        }

        public Order GetOrderById(int orderId)
        {
            return _repositoryWrapper.OrderRepository.GetOrderByOrderID(orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _repositoryWrapper.OrderRepository.GetAllOrders();
        }

        public void MarkOrderAsReceived(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                order.Status = "delivered";
                _repositoryWrapper.OrderRepository.Update(order);
                _repositoryWrapper.Save();
            }
        }
        public void UpdateOrder(Order order)
        {
            _repositoryWrapper.OrderRepository.Update(order);
            _repositoryWrapper.Save();
        }
    }
}
