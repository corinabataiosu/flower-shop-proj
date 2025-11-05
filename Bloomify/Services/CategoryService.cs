using Bloomify.Repositories;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;
using Bloomify.Models;

namespace Bloomify.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CategoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            // Cast CategoryRepository to the appropriate interface type
            var categoryRepository = _repositoryWrapper.CategoryRepository as ICategoryRepository;
            if (categoryRepository == null)
            {
                throw new InvalidCastException("CategoryRepository is not of type ICategoryRepository.");
            }

            // Use the FindAll method from the casted repository
            return categoryRepository.FindAll().ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            // Cast CategoryRepository to the appropriate interface type
            var categoryRepository = _repositoryWrapper.CategoryRepository as ICategoryRepository;
            if (categoryRepository == null)
            {
                throw new InvalidCastException("CategoryRepository is not of type ICategoryRepository.");
            }
            // Use the FindByCondition method from the casted repository
            return categoryRepository
                .FindByCondition(c => c.CategoryID == categoryId)
                .FirstOrDefault();
        }

        public void CreateCategory(Category category)
        {
            // Cast CategoryRepository to the appropriate interface type
            var categoryRepository = _repositoryWrapper.CategoryRepository as ICategoryRepository;
            if (categoryRepository == null)
            {
                throw new InvalidCastException("CategoryRepository is not of type ICategoryRepository.");
            }
            // Use the Create method from the casted repository
            categoryRepository.Create(category);
            _repositoryWrapper.Save();
        }

        public void UpdateCategory(Category category)
        {
            // Cast CategoryRepository to the appropriate interface type
            var categoryRepository = _repositoryWrapper.CategoryRepository as ICategoryRepository;
            if (categoryRepository == null)
            {
                throw new InvalidCastException("CategoryRepository is not of type ICategoryRepository.");
            }
            // Use the Update method from the casted repository
            categoryRepository.Update(category);
            _repositoryWrapper.Save();
        }

        public void DeleteCategory(int categoryId)
        {
            // Cast CategoryRepository to the appropriate interface type
            var categoryRepository = _repositoryWrapper.CategoryRepository as ICategoryRepository;
            if (categoryRepository == null)
            {
                throw new InvalidCastException("CategoryRepository is not of type ICategoryRepository.");
            }
            // Use the Delete method from the casted repository
            var category = categoryRepository.FindByCondition(c => c.CategoryID == categoryId).FirstOrDefault();
            if (category != null)
            {
                categoryRepository.Delete(category);
                _repositoryWrapper.Save();
            }
        }
    }
}
