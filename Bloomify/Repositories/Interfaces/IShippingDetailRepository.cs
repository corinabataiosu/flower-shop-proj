using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IShippingDetailRepository
    {
        ShippingDetail GetShippingDetail(int orderID);
    }
}
