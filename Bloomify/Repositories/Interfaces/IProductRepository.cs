using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IProductRepository: IRepositoryBase<Product>
    {
        public List<Product> GetAllWithCategoryAndProvider();
        public void Delete(Product product);
        
        //void Delete(List<Product> product);
        // Add any additional methods specific to Product repository here
    }
}
