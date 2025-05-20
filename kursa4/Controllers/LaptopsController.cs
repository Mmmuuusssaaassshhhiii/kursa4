using kursa4.Interfaces;
using kursa4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace kursa4.Controllers;

public class LaptopsController : Controller
{
    private readonly IAllLaptops _allLaptops;
    private readonly ILaptopsCategory _laptopsCategory;
    private readonly ILaptopsBrand _laptopsBrand;
    private readonly ILaptopsCpu _laptopsCpu;
    private readonly ILaptopsGpu _laptopsGpu;
    private readonly ILaptopsRam _laptopsRam;
    private readonly ILaptopsStorage _laptopsStorage;

    public LaptopsController(IAllLaptops iAllLaptops,
        ILaptopsCategory iLaptopsCategory,
        ILaptopsBrand iLaptopsBrand,
        ILaptopsCpu iLaptopCpu,
        ILaptopsGpu iLaptopsGpu,
        ILaptopsRam iLaptopsRam,
        ILaptopsStorage iLaptopsStorage)
    {
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
        ViewBag.Title = "Страница с ноутбуками";

        var viewModel = new LaptopsListViewModel
        {
            allLaptops = _allLaptops.Laptops,
            currCategory = "Ноутбуки"
        };

        ViewBag.Categories = _laptopsCategory.AllCategories;

        return View(viewModel);
    }
}