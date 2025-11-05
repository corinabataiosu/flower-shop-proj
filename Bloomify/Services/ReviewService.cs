using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ReviewService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IEnumerable<Review> GetReviewsForProduct(int productID)
        {
            return _repositoryWrapper.ReviewRepository.GetReviewsByProductId(productID);
        }
        public void AddReview(Review review)
        {
            _repositoryWrapper.ReviewRepository.CreateReview(review);
            _repositoryWrapper.Save();
        }

        public List<Review> GetReviewsByProductId(int productId)
        {
            return _repositoryWrapper.ReviewRepository
                .FindByCondition(r => r.ProductID == productId)
                .Include(r => r.Products) // ???????????????????????????????????????????
                .ToList();
        }
    }
}
