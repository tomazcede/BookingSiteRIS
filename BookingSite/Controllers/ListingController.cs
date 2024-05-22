using BookingSite.Model;
using BookingSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

public class ListingController : Controller
{
    private readonly BookingDatabaseContext _context;

    public ListingController()
    {
        _context = new BookingDatabaseContext();
    }

    public async Task<IActionResult> Index(int id, Boolean reserved_err = false, Boolean reserved_succ = false)
    {
        if (reserved_err)
            TempData["msg"] = "<script>window.addEventListener('load', function () {  alert(\"Invalid date interval, check the other reservations\")})</script>";
        if (reserved_succ)
            TempData["msg"] = "<script>window.addEventListener('load', function () {  alert(\"Reservation added successfully\")})</script>";

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
        r.UporabnikId = _context.Uporabniki.FirstOrDefault(d => d.Email == HttpContext.User.Identity.Name).UporabnikId;
        r.DatumOd = from;
        r.DatumDo = till;

        _context.Rezervacijes.Add(r);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { id = id, reserved_succ = true });
    }

    // GET: Listing/Create
    public IActionResult Create()
    {
        ViewBag.Neprimicnine = new SelectList(_context.Neprimicnines, "NepremicninaId", "Naslov");
        return View();
    }

    // POST: Listing/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DatumOd,DatumDo,NeprimicninaId,Opis,SlikaUrl")] Listingi listing)
    {
        if (ModelState.IsValid)
        {
            var neprimicninaExists = _context.Neprimicnines.Any(n => n.NepremicninaId == listing.NeprimicninaId);
            if (!neprimicninaExists)
            {
                ModelState.AddModelError("NeprimicninaId", "Invalid NeprimicninaId. The specified property does not exist.");
                ViewBag.Neprimicnine = new SelectList(_context.Neprimicnines, "NepremicninaId", "Naslov");
                return View(listing);
            }

            _context.Add(listing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        ViewBag.Neprimicnine = new SelectList(_context.Neprimicnines, "NepremicninaId", "Naslov");
        return View(listing);
    }

    // GET: Listing/List
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
