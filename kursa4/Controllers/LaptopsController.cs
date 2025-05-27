using kursa4.Interfaces;
using kursa4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers;

public class LaptopsController : Controller
{
    
    private readonly ApplicationDbContext _context;
    private readonly IAllLaptops _allLaptops;
    private readonly ILaptopsCategory _laptopsCategory;
    private readonly ILaptopsBrand _laptopsBrand;
    private readonly ILaptopsCpu _laptopsCpu;
    private readonly ILaptopsGpu _laptopsGpu;
    private readonly ILaptopsRam _laptopsRam;
    private readonly ILaptopsStorage _laptopsStorage;

    public LaptopsController(
        ApplicationDbContext context,
        IAllLaptops iAllLaptops,
        ILaptopsCategory iLaptopsCategory,
        ILaptopsBrand iLaptopsBrand,
        ILaptopsCpu iLaptopCpu,
        ILaptopsGpu iLaptopsGpu,
        ILaptopsRam iLaptopsRam,
        ILaptopsStorage iLaptopsStorage)
    {
        _context = context;
        _allLaptops = iAllLaptops;
        _laptopsCategory = iLaptopsCategory;
        _laptopsBrand = iLaptopsBrand;
        _laptopsCpu = iLaptopCpu;
        _laptopsGpu = iLaptopsGpu;
        _laptopsRam = iLaptopsRam;
        _laptopsStorage = iLaptopsStorage;
    }

    public ViewResult ListLaptops()
    {
        ViewBag.Title = "Каталог";

        var viewModel = new LaptopsListViewModel
        {
            allLaptops = _allLaptops.Laptops,
            currCategory = "Ноутбуки"
        };

        ViewBag.Categories = _laptopsCategory.AllCategories;
        ViewBag.Brands = _laptopsBrand.allBrands;
        ViewBag.CPUs = _laptopsCpu.AllCPUs;
        ViewBag.GPUs = _laptopsGpu.AllGPUs;
        ViewBag.Rams = _laptopsRam.AllRams;
        ViewBag.Storages = _laptopsStorage.AllStorages;

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var laptop = _context.Laptops
            .Include(l => l.Category)
            .Include(l => l.Brand)
            .Include(l => l.CPU)
            .Include(l => l.GPU)
            .Include(l => l.RAM)
            .Include(l => l.Storage)
            .Include(l => l.Reviews)
            .ThenInclude(r => r.User)
            .FirstOrDefault(l => l.Id == id);

        if (laptop == null)
        {
            return NotFound();
        }
        
        return View(laptop);
    }
}