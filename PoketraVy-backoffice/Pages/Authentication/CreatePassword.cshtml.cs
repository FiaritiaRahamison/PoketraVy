using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PoketraVy_backoffice.Pages.Authentication
{
    public class CreatePasswordModel : PageModel
    {
        private readonly Data.PoketraVy_backofficeContext _context;

        public CreatePasswordModel(Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Input.NewPassword != Input.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Les mots de passe ne correspondent pas.");
                    return Page();
                }

                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToPage("/Index");
                }

                var user = await _context.Utilisateurs.FindAsync(userId);
                if (user != null)
                {
                    user.Password = Input.NewPassword;
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("UserId");

                    // Connecter l'utilisateur
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToPage("/Utilisateurs/Index");
                }
            }

            return Page();
        }
    }
}
