using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetReviewsForProduct(int productID);
        void AddReview(Review review);
        Review GetReviewById(int reviewId);
        void UpdateReview(Review review);
    }
}
