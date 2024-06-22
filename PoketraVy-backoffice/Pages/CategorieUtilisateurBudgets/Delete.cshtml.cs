using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Pages.CategorieUtilisateurBudgets
{
    public class DeleteModel : PageModel
    {
        private readonly PoketraVy_backoffice.Data.PoketraVy_backofficeContext _context;

        public DeleteModel(PoketraVy_backoffice.Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategorieUtilisateurBudget CategorieUtilisateurBudget { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategorieUtilisateurBudget = await _context.CategorieUtilisateurBudget.FirstOrDefaultAsync(m => m.ID == id);

            if (CategorieUtilisateurBudget == null)
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

            CategorieUtilisateurBudget = await _context.CategorieUtilisateurBudget.FindAsync(id);

            if (CategorieUtilisateurBudget != null)
            {
                _context.CategorieUtilisateurBudget.Remove(CategorieUtilisateurBudget);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
