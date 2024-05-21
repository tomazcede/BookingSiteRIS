using System.Diagnostics;
using BookingSite.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using BookingSite.Models;

namespace BookingSite.Controllers
{
    public class UporabnikiController : Controller
    {
        private readonly BookingDatabaseContext _context;

        public UporabnikiController(BookingDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!SignedIn())
                return RedirectToAction("Login");

            var user = _context.Uporabniki.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            return View(user);
        }

        public IActionResult Login(bool failed = false)
        {
            if (failed)
            {
                TempData["msg"] = "<script>window.addEventListener('load', function () { alert(\"Login failed\")})</script>";
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
                search = _context.Uporabniki.Include(u => u.TipUporabnika).First(u => u.Email == email && u.Geslo == hashed);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", new { failed = true });
            }

            if (search == null)
                return RedirectToAction("Login", new { failed = true });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, search.Email),
                new Claim("UserID", search.UporabnikId.ToString()),
                new Claim(ClaimTypes.Role, search.TipUporabnika.Naziv),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool UserExists(int id)
        {
            return (_context.Uporabniki?.Any(e => e.UporabnikId == id)).GetValueOrDefault();
        }

        private bool SignedIn()
        {
            return HttpContext.User.Identity.Name != null;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string firstName, string lastName)
        {

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes("sol"),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));


            var newUser = new Uporabniki
            {
                Email = email,
                Geslo = hashedPassword,
                Ime = firstName,
                Priimek = lastName,
                TipUporabnikaId = 1
            };

            _context.Uporabniki.Add(newUser);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newUser.Email),
                new Claim("UserID", newUser.UporabnikId.ToString()),
                new Claim(ClaimTypes.Role, "Stranka"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index");
        }
    }
}
