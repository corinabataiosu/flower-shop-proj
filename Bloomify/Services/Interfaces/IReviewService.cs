using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetReviewsForProduct(int productID);
        void AddReview(Review review);
        List<Review> GetReviewsByProductId(int productId);
    }
}
