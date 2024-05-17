using System.Diagnostics;
using BookingSite.Model;
using Microsoft.AspNetCore.Mvc;
using BookingSite.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSite.Controllers;

public class ListingController : Controller
{
    private readonly BookingDatabaseContext _context;

    public ListingController()
    {
        _context = new BookingDatabaseContext();
    }

    public async Task<IActionResult> Index(int id)
    {
        var listing = await _context.Listingis.Include(u => u.Neprimicnina)
            .FirstOrDefaultAsync(m => m.ListingId == id);
        if (listing == null)
        {
            return NotFound();
        }
        return View(listing);
    }
    
    public async Task<IActionResult> Search(string searchtext)
    {
        var listing = await _context.Listingis.Include(u => u.Neprimicnina).Where(u => u.Neprimicnina.Kraj.Contains(searchtext) || u.Neprimicnina.Drzava.Ime.Contains(searchtext))
            .ToListAsync();
        return View(listing);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
