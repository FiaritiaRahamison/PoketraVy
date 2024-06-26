﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PoketraVy_backoffice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.PoketraVy_backofficeContext _context;

        public IndexModel(Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = _context.Utilisateurs.SingleOrDefault(u => u.Username == Input.Username);

                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.Password))
                    {
                        // Rediriger vers une page de création de mot de passe
                        HttpContext.Session.SetInt32("UserId", user.ID);
                        return RedirectToPage("/Authentication/CreatePassword");
                    }
                    else if (user.Password == Input.Password)
                    {
                        // Authentification réussie
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                            new Claim(ClaimTypes.Role, user.Role.ToString())
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return RedirectToPage("/Budgets/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nom d'utilisateur incorrect.");
                    return Page();
                }
            }

            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }

    }
}
