using kursa4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Главная страница личного кабинета
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return RedirectToAction("Login");

            return View(user);
        }

        // Заказы
        public async Task<IActionResult> Orders()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login");

            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return RedirectToAction("Login");

            return View(user.Orders.ToList());
        }

        // Корзина
        public async Task<IActionResult> Cart()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null)
                return RedirectToAction("Login");

            var user = await _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Laptop)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user?.Cart?.CartItems == null)
                return View(new List<CartItem>());

            return View(user.Cart.CartItems.ToList());
        }

        // Регистрация (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Регистрация (POST)
        [HttpPost]
        public async Task<IActionResult> Register(User model, string confirmPassword)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != confirmPassword)
            {
                ModelState.AddModelError("Password", "Пароли не совпадают");
                return View(model);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
                return View(model);
            }

            model.Role = "User";

            // Создание корзины вместе с пользователем
            model.Cart = new Cart
            {
                CartItems = new List<CartItem>() // можно опустить, EF сам инициализирует
            };

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

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
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
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
