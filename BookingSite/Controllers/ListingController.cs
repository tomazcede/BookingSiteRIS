using System.Diagnostics;
using BookingSite.Model;
using BookingSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ListingController : Controller
{
    private readonly BookingDatabaseContext _context;

    public ListingController()
    {
        _context = new BookingDatabaseContext();
    }

    public async Task<IActionResult> Index(int id, bool reserved_err = false, bool reserved_succ = false)
    {
        if (reserved_err)
            TempData["msg"] = "<script>window.addEventListener('load', function () { alert(\"Invalid date interval, check the other reservations\") })</script>";
        if (reserved_succ)
            TempData["msg"] = "<script>window.addEventListener('load', function () { alert(\"Reservation added successfully\") })</script>";

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
        var listing = await _context.Listingis.Include(u => u.Neprimicnina)
            .Where(u => u.Neprimicnina.Kraj.Contains(searchtext) || u.Neprimicnina.Drzava.Ime.Contains(searchtext))
            .ToListAsync();

        return View(listing);
    }

    public async Task<IActionResult> BookListing(int id, string from_date, string till_date)
    {
        if (!SignedIn())
            return RedirectToAction("Login", "Uporabniki");

        var listing = await _context.Listingis.Include(l => l.Rezervacijes)
                                              .FirstOrDefaultAsync(l => l.ListingId == id);
        if (listing == null)
        {
            return NotFound();
        }

        DateTime from;
        DateTime till;
        if (!DateTime.TryParse(from_date, out from) || !DateTime.TryParse(till_date, out till))
        {
            TempData["msg"] = "<script>alert('Invalid date format. Please check your input.');</script>";
            return RedirectToAction("Index", new { id = id });
        }

        foreach (var reservation in listing.Rezervacijes)
        {
            if ((reservation.DatumOd <= from && reservation.DatumDo >= from) ||
                (reservation.DatumOd <= till && reservation.DatumDo >= till))
            {
                return RedirectToAction("Index", new { id = id, reserved_err = true });
            }
        }

        var user = await _context.Uporabniki.FirstOrDefaultAsync(u => u.Email == HttpContext.User.Identity.Name);
        if (user == null)
        {
            return RedirectToAction("Login", "Uporabniki");
        }

        var newReservation = new Rezervacije
        {
            ListingId = id,
            UporabnikId = user.UporabnikId,
            DatumOd = from,
            DatumDo = till
        };

        try
        {
            _context.Rezervacijes.Add(newReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = id, reserved_succ = true });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            TempData["msg"] = "<script>alert('Concurrency error occurred while saving. Please try again.');</script>";
            return RedirectToAction("Index", new { id = id });
        }
        catch (DbUpdateException ex)
        {
            TempData["msg"] = "<script>alert('An error occurred while saving the reservation. Please check your data and try again.');</script>";
            return RedirectToAction("Index", new { id = id });
        }
    }

    
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DatumOd,DatumDo,Neprimicnina,Opis,SlikaUrl")] Listingi listing)
    {
        if (ModelState.IsValid)
        {
            if (listing.Neprimicnina != null && !string.IsNullOrWhiteSpace(listing.Neprimicnina.Naslov))
            {
                
                var property = await _context.Neprimicnines.FirstOrDefaultAsync(p => p.Naslov == listing.Neprimicnina.Naslov);
                if (property == null)
                {
                    property = new Neprimicnine
                    {
                        Naslov = listing.Neprimicnina.Naslov,

                    };
                    _context.Neprimicnines.Add(property);
                    await _context.SaveChangesAsync();
                }

                listing.NeprimicninaId = property.NepremicninaId;
                listing.Neprimicnina = null;

                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
            {
                ModelState.AddModelError("Neprimicnina.Naslov", "The property address is required.");
            }
        }

        return View(listing);
    }

    public async Task<IActionResult> List()
    {
        var listings = await _context.Listingis.Include(u => u.Neprimicnina).ToListAsync();
        return View(listings);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool SignedIn()
    {
        return HttpContext.User.Identity.Name != null;
    }
}
