using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        //IQueryable<Review> GetAllReviews();
        IEnumerable<Review> GetReviewsByProductId(int productId);
        //Review GetReviewById(int reviewId);
        void CreateReview(Review review);
        //void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
