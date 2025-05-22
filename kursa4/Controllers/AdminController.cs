using kursa4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync();
        var orders = await _context.Orders.ToListAsync();
        var laptops = await _context.Laptops.ToListAsync();
        
        ViewBag.UserCount = users.Count;
        ViewBag.OrderCount = orders.Count;
        ViewBag.LaptopCount = laptops.Count;

        return View();
    }

    public async Task<IActionResult> Laptops()
    {
        var laptops = await _context.Laptops
            .Include(l => l.Category)
            .Include(l => l.Brand)
            .Include(l => l.CPU)
            .Include(l => l.GPU)
            .Include(l => l.RAM)
            .Include(l => l.Storage)
            .ToListAsync();
        
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        ViewBag.CPUs = await _context.CPUs.ToListAsync();
        ViewBag.GPUs = await _context.GPUs.ToListAsync();
        ViewBag.RAMs = await _context.RAMs.ToListAsync();
        ViewBag.Storages = await _context.Storages.ToListAsync();
        
        return View(laptops);
    }

    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();

        return View(users);
    }

    public async Task<IActionResult> Orders()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .ToListAsync();
        
        return View(orders);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddLaptop(Laptop laptop, IFormFile ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "laptops");
            Directory.CreateDirectory(uploadsFolder); // на случай если папки нет
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            laptop.ImageUrl = "/img/" + uniqueFileName;
        }   

        // сохранить ноутбук в базу данных
        _context.Laptops.Add(laptop);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}