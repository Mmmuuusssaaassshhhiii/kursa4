using kursa4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace kursa4.Controllers
{
    public class AccountController : Controller
    {
        // Временное хранилище пользователей
        private static readonly List<User> users = new List<User>();

        // Главная страница личного кабинета
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return RedirectToAction("Login");

            return View(user);
        }

        // Заказы
        public IActionResult Orders()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login");

            var orders = new List<Order>
            {
                new Order { Id = 123, OrderDate = DateTime.Now.AddDays(-5), Status = "доставлен" },
                new Order { Id = 124, OrderDate = DateTime.Now.AddDays(-2), Status = "в обработке" }
            };

            return View(orders);
        }

        // Корзина
        public IActionResult Cart()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login");

            var cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, Laptop = new Laptop { Model = "ASUS ROG Strix G18" }, Quantity = 1 },
                new CartItem { Id = 2, Laptop = new Laptop { Model = "MacBook Air M2" }, Quantity = 2 }
            };

            return View(cartItems);
        }

        // Регистрация (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Регистрация (POST)
        [HttpPost]
        public IActionResult Register(User model, string confirmPassword)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != confirmPassword)
            {
                ModelState.AddModelError("Password", "Пароли не совпадают");
                return View(model);
            }

            if (users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
                return View(model);
            }

            model.Role = "User"; // Назначение роли по умолчанию
            users.Add(model);

            return RedirectToAction("Login");
        }

        // Вход (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Вход (POST)
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Неверный email или пароль";
            return View();
        }

        // Выход
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
