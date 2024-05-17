using System.Diagnostics;
using BookingSite.Model;
using Microsoft.AspNetCore.Mvc;
using BookingSite.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private BookingDatabaseContext _context;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = new BookingDatabaseContext();
    }

    public async Task<IActionResult> Index()
    {
        var listingiContext = await _context.Listingis.Include(u => u.Neprimicnina).ToListAsync();
        
        return View(listingiContext);
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
