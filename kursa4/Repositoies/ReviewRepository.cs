using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class ReviewRepository : IUsersReviews
{
    private readonly List<Review> _reviews = new List<Review>
    {
        new Review
        {
            Id = 1,
            UserId = 1,
            LaptopId = 1,
            Content = "Отличный ноутбук, всё летает!",
            Rating = 5,
            CreatedAt = DateTime.Now.AddDays(-3)
        },
        new Review
        {
            Id = 2,
            UserId = 2,
            LaptopId = 1,
            Content = "Хороший, но шумный вентилятор.",
            Rating = 4,
            CreatedAt = DateTime.Now.AddDays(-2)
        },
        new Review
        {
            Id = 3,
            UserId = 3,
            LaptopId = 2,
            Content = "Экран супер, но клавиатура могла быть лучше.",
            Rating = 3,
            CreatedAt = DateTime.Now.AddDays(-1)
        }
    };

    public void AddReview(Review review)
    {
        review.Id = _reviews.Max(r => r.Id) + 1;
        review.CreatedAt = DateTime.Now;
        _reviews.Add(review);
    }

    public void UpdateReview(Review review)
    {
        var existingReview = _reviews.FirstOrDefault(r => r.Id == review.Id);
        if (existingReview != null)
        {
            existingReview.Content = review.Content;
            existingReview.Rating = review.Rating;
        }
    }

    public void DeleteReview(int reviewId)
    {
        var review = _reviews.FirstOrDefault(r => r.Id == reviewId);
        if (review != null)
        {
            _reviews.Remove(review);
        }
    }

    public Review GetReviewById(int reviewId)
    {
        return _reviews.FirstOrDefault(r => r.Id == reviewId);
    }

    public IEnumerable<Review> GetReviewsByLaptopId(int laptopId)
    {
        return _reviews.Where(r => r.LaptopId == laptopId);
    }

    public IEnumerable<Review> GetReviewsByUserId(int userId)
    {
        return _reviews.Where(r => r.UserId == userId);
    }

    public double GetAverageRatingForLaptop(int laptopId)
    {
        var reviews = _reviews.Where(r => r.LaptopId == laptopId).ToList();
        if (reviews.Count == 0) return 0.0;
        return Math.Round(reviews.Average(r => r.Rating), 2);
    }

    public IEnumerable<Review> GetAllReviews()
    {
        return _reviews;
    }
}