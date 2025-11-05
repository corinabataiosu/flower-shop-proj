using Bloomify.Services.Interfaces;
using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ProductService(IRepositoryWrapper repository)
        {
            _repositoryWrapper = repository;
        }
        public List<Product> GetAllProducts()
        {
            return _repositoryWrapper.ProductRepository.GetAllWithCategoryAndProvider();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _repositoryWrapper.ProductRepository
                .FindByCondition(p => p.CategoryID == categoryId)
                .Include(p => p.Categories)
                .Include(p => p.Providers)
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return _repositoryWrapper.ProductRepository
                .FindByCondition(p => p.ProductID == id)
                .Include(p => p.Categories)
                .Include(p => p.Providers)
                .FirstOrDefault();
        }
        public void CreateProduct(Product product)
        {
            _repositoryWrapper.ProductRepository.Create(product);
            _repositoryWrapper.Save();
        }
        public void UpdateProduct(Product product)
        {
            _repositoryWrapper.ProductRepository.Update(product);
            _repositoryWrapper.Save();
        }
        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _repositoryWrapper.ProductRepository.Delete(product);
                _repositoryWrapper.Save();
            }
        }

        public bool Exists(int id)
        {
            return _repositoryWrapper.ProductRepository.FindByCondition(p => p.ProductID == id).Any();
        }
    }
}
