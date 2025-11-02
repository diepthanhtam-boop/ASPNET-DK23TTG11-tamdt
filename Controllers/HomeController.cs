using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaptopShopWebsite.Data;
using LaptopShopWebsite.Models;

namespace LaptopShopWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var featuredProducts = await _context.Products
            .Where(p => p.Featured)
            .Take(8)
            .ToListAsync();
            
        var categories = new[]
        {
            new { Name = "Gaming", Description = "Laptop chơi game cao cấp", Image = "https://images.pexels.com/photos/325876/pexels-photo-325876.jpeg", Count = await _context.Products.CountAsync(p => p.Category == "Gaming") },
            new { Name = "Business", Description = "Laptop doanh nghiệp", Image = "https://images.pexels.com/photos/577210/pexels-photo-577210.jpeg", Count = await _context.Products.CountAsync(p => p.Category == "Business") },
            new { Name = "Ultrabook", Description = "Laptop siêu mỏng nhẹ", Image = "https://images.pexels.com/photos/1029757/pexels-photo-1029757.jpeg", Count = await _context.Products.CountAsync(p => p.Category == "Ultrabook") },
            new { Name = "Professional", Description = "Laptop chuyên nghiệp", Image = "https://images.pexels.com/photos/1194713/pexels-photo-1194713.jpeg", Count = await _context.Products.CountAsync(p => p.Category == "Professional") }
        };
        
        ViewBag.Categories = categories;
        return View(featuredProducts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
