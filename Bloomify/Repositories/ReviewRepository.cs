using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;
using Microsoft.EntityFrameworkCore;

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
            return _context.Reviews
                .Include(r => r.Users)
                .Where(r => r.ProductID == productId)
                .ToList();
        }

        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews
                .Include(r => r.Users)
                .FirstOrDefault(r => r.ReviewID == reviewId);
        }

        public void UpdateReview(Review review)
        {
            Update(review);
        }

        public IQueryable<Review> GetAllReviews()
        {
            return FindAll();
        }
    }
}
