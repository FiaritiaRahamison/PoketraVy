using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Pages.Utilisateurs
{
    public class DeleteModel : PageModel
    {
        private readonly PoketraVy_backoffice.Data.PoketraVy_backofficeContext _context;

        public DeleteModel(PoketraVy_backoffice.Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Utilisateur Utilisateur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(m => m.ID == id);

            if (Utilisateur == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (Utilisateur != null)
            {
                _context.Utilisateurs.Remove(Utilisateur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
