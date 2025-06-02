using kursa4.Interfaces;
using kursa4.Models;
using Microsoft.AspNetCore.Mvc;

namespace kursa4.Controllers;

public class OrdersController : Controller
{
    private readonly IUsersOrders _orderRepo;
    private readonly IUserCart _cartRepo;
    private readonly IAllUsers _userRepo;

    public OrdersController(IUsersOrders orderRepo, IUserCart cartRepo, IAllUsers userRepo)
    {
        _orderRepo = orderRepo;
        _cartRepo = cartRepo;
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Login", "Account");

        var user = _userRepo.GetUserByEmail(email);
        if (user == null) return RedirectToAction("Login", "Account");

        var cartItems = _cartRepo.GetCartItems(user.Id)
            .Select(ci =>
            {
                ci.Laptop = _cartRepo.GetLaptopById(ci.LaptopId);
                return ci;
            }).ToList();

        var total = cartItems.Sum(item => (item.Laptop?.Price ?? 0) * item.Quantity);

        ViewBag.Total = total;
        ViewBag.CartItems = cartItems;

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm(string deliveryDate)
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Login", "Account");

        var user = _userRepo.GetUserByEmail(email);
        var cartItems = _cartRepo.GetCartItems(user.Id);

        if (!cartItems.Any())
        {
            TempData["Error"] = "Корзина пуста.";
            return RedirectToAction("Index", "Cart");
        }

        var order = new Order
        {
            UserId = user.Id,
            DeliveryDate = DateTime.TryParse(deliveryDate, out var dt) ? dt : null,
            OrderItems = cartItems.Select(ci => new OrderItem
            {
                LaptopId = ci.LaptopId,
                Quantity = ci.Quantity,
                Price = ci.Laptop?.Price ?? 0
            }).ToList()
        };

        _orderRepo.CreateOrder(order);
        _cartRepo.ClearCart(user.Id);

        return RedirectToAction("Success");
    }


    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }
}
