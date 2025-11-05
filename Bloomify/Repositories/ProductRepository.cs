using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public List<Product> GetAllWithCategoryAndProvider()
        {
            return _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Providers)
                .ToList();
        }
    }
}
