using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
        public void CreateReview(Review review)
        {
            Create(review);
        }
        public void DeleteReview(Review review)
        {
            Delete(review);
        }

        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews.Where(r => r.ProductID == productId).ToList();
        }

    }
}
