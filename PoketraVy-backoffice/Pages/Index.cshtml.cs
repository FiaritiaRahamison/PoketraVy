using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
                var user = _context.Utilisateurs.SingleOrDefault(u => u.Username == Input.Username && u.Password == Input.Password);

                if (user != null)
                {
                    // Authentification réussie
                    HttpContext.Session.SetInt32("UserId", user.ID);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("UserRole", user.Role.ToString());

                    return RedirectToPage("/Utilisateurs/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
                }
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");
            // Redirect to the home page
            return RedirectToPage("/Index");
        }
    }
}
