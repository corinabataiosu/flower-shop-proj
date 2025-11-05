using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository
    {
        public ProviderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
