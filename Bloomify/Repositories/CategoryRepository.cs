using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
