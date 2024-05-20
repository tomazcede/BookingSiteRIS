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

    public async Task<IActionResult> Index(int id, Boolean reserved_err = false, Boolean reserved_succ = false)
    {
        if(reserved_err)
            TempData["msg"] = "<script>alert('Invalid date interval, check the other reservations');</script>";
        if(reserved_succ)
            TempData["msg"] = "<script>alert('Reservation added successfully');</script>";
        var listing = await _context.Listingis.Include(u => u.Neprimicnina).Include(u => u.Rezervacijes)
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
    
    public async Task<IActionResult> BookListing(int id, string from_date, string till_date)
    {
        if(!SignedIn())
            return RedirectToAction("Login", "Uporabniki");
        
        var reservations = await _context.Rezervacijes.Where(d => d.ListingId == id).ToListAsync();
        DateTime from = DateTime.Parse(from_date);
        DateTime till = DateTime.Parse(till_date);

        foreach (var reservation in reservations)
        {
            if (reservation.DatumOd <= from && reservation.DatumDo >= from)
                return RedirectToAction("Index", new { id = id, reserved_err = true });
            if (reservation.DatumOd <= till && reservation.DatumDo >= till)
                return RedirectToAction("Index", new { id = id, reserved_err = true });
        }

        Rezervacije r = new Rezervacije();
        r.ListingId = id;
        r.UporabnikId = _context.Uporabnikis.FirstOrDefault(d => d.Email == HttpContext.User.Identity.Name).UporabnikId;
        r.DatumOd = from;
        r.DatumDo = till;
        r.RezervacijaId = _context.Rezervacijes.Count() + 1;

        _context.Rezervacijes.Add(r);
        _context.SaveChanges();
        
        return RedirectToAction("Index", new { id = id, reserved_succ=true });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    private bool SignedIn()
    {
        if(HttpContext.User.Identity.Name != null){
            return true;
        }
        return false;
    }
}
