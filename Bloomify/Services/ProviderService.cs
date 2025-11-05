using Bloomify.Services.Interfaces;
using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Repositories;


namespace Bloomify.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ProviderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IEnumerable<Provider> GetAllProviders()
        {
            // Cast ProviderRepository to the appropriate interface type
            var providerRepository = _repositoryWrapper.ProviderRepository as IProviderRepository;
            if (providerRepository == null)
            {
                throw new InvalidCastException("ProviderRepository is not of type IProviderRepository.");
            }
            // Use the FindAll method from the casted repository
            return providerRepository.FindAll().ToList();
        }
        // Implement other methods as needed
        public Provider GetProviderById(int providerId)
        {
            throw new NotImplementedException();
        }
        public void CreateProvider(Provider provider)
        {
            throw new NotImplementedException();
        }
        public void UpdateProvider(Provider provider)
        {
            throw new NotImplementedException();
        }
        public void DeleteProvider(int providerId)
        {
            throw new NotImplementedException();
        }
    }
}
