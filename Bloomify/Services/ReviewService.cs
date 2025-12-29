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

        public Review GetReviewById(int reviewId)
        {
            return _repositoryWrapper.ReviewRepository.GetReviewById(reviewId);
        }

        public void UpdateReview(Review review)
        {
            _repositoryWrapper.ReviewRepository.UpdateReview(review);
            _repositoryWrapper.Save();
        }
    }
}
