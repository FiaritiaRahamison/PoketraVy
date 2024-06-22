using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Pages.Utilisateurs
{
    public class CreateModel : PageModel
    {
        private readonly PoketraVy_backoffice.Data.PoketraVy_backofficeContext _context;

        public CreateModel(PoketraVy_backoffice.Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Utilisateurs.Add(Utilisateur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
