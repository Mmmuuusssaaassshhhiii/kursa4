using kursa4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int LaptopId, int Rating, string Comment)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            TempData["Error"] = "Чтобы оставить отзыв, нужно авторизоваться.";
            return RedirectToAction("Details", "Laptops", new { id = LaptopId });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null)
        {
            TempData["Error"] = "Пользователь не найден.";
            return RedirectToAction("Details", "Laptops", new { id = LaptopId });
        }

        var review = new Review
        {
            LaptopId = LaptopId,
            UserId = user.Id,
            Content = Comment,
            Rating = Rating,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Отзыв добавлен!";
        return RedirectToAction("Details", "Laptops", new { id = LaptopId });
    }
}