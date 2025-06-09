using kursa4.Models;

namespace kursa4.Interfaces;

public interface IUsersReviews
{
    
    //get reviews
    IEnumerable<Review> GetReviewsByLaptopId(int laptopId);
    IEnumerable<Review> GetReviewsByUserId(int userId);
    Review GetReviewById(int reviewId);
    
    //cre, ed, del
    void AddReview(Review review);
    void UpdateReview(Review review);
    void DeleteReview(int reviewId);
    IEnumerable<Review> GetAllReviews();
    
    //average rating
    double GetAverageRatingForLaptop(int laptopId);
    
}