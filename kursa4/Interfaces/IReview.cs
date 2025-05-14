using kursa4.Models;

namespace kursa4.Interfaces;

public interface IReview
{
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<Review> GetReviewByIdAsync(int id);
    Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId);
    Task<IEnumerable<Review>> GetReviewsByLaptopIdAsync(int laptopId);
    Task AddReviewAsync(Review review);
    Task UpdateReviewAsync(Review review);
    Task DeleteReviewAsync(int id);
}