using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class ShippingDetailRepository : RepositoryBase<ShippingDetail>, IShippingDetailRepository
    {
        public ShippingDetailRepository(AppDbContext context) : base(context)
        {
        }

        public ShippingDetail GetShippingDetail(int orderID)
        {
            return _context.ShippingDetails
                .FirstOrDefault(sd => sd.OrderID == orderID);
        }
    }
}
