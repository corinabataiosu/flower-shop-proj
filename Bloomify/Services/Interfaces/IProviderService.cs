using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetAllProviders();
        Provider GetProviderById(int providerId);
        void CreateProvider(Provider provider);
        void UpdateProvider(Provider provider);
        void DeleteProvider(int providerId);
    }
}
