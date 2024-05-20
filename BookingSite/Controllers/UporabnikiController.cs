using System.Diagnostics;
using BookingSite.Model;
using Microsoft.AspNetCore.Mvc;
using BookingSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace BookingSite.Controllers;

public class UporabnikiController : Controller
{
    private BookingDatabaseContext _context;

    public UporabnikiController(ILogger<HomeController> logger)
    {
        _context = new BookingDatabaseContext();
    }

    public IActionResult Index()
    {
        if (!SignedIn())
            return RedirectToAction("Login");

        var user = _context.Uporabnikis.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
        return View(user);
    }
    public IActionResult Login(Boolean failed = false)
    {
        if (failed) {
            TempData["msg"] = "<script>window.addEventListener('load', function () {  alert(\"Login failed\")})</script>";
        }
        return View();
    }
    
    public async Task<IActionResult> AuthUser(string email, string password)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Encoding.ASCII.GetBytes("sol"),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        Uporabniki search;
        
        try
        {
            search = _context.Uporabnikis.Include(u => u.TipUporabnika).First(u => u.Email == email && u.Geslo == password);
        }
        catch (Exception e)
        {
            return RedirectToAction("Login", new { failed = true});
        }
    
        if(search == null)
            return RedirectToAction("Login", new { failed = true});

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, search.Email),
            new Claim("UserID", search.UporabnikId.ToString()),
            new Claim(ClaimTypes.Role, search.TipUporabnika.Naziv),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };


        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);
        
        return RedirectToAction("Index");
    }
    public async void Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Redirect("/home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    private bool UserExists(int id)
    {
        return (_context.Uporabnikis?.Any(e => e.UporabnikId == id)).GetValueOrDefault();
    }

    private bool SignedIn()
    {
        if(HttpContext.User.Identity.Name != null){
            return true;
        }
        return false;
    }
}
