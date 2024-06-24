using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UtilisateurRepository _utilisateurRepository;

        public AuthenticationController(UtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Authentication");
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = _utilisateurRepository.GetByUsernameAndPassword(user.Username, user.Password);

                if (utilisateur != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utilisateur.Username),
                        new Claim(ClaimTypes.NameIdentifier, utilisateur.ID.ToString()),
                        new Claim(ClaimTypes.Role, utilisateur.Role.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View();
        }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }
    }
}
