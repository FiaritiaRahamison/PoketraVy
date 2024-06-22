using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;

namespace PoketraVy_backoffice.Pages.UtilisateurBudgets
{
    public class IndexModel : PageModel
    {
        private readonly PoketraVy_backoffice.Data.PoketraVy_backofficeContext _context;

        public IndexModel(PoketraVy_backoffice.Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        public IList<UtilisateurBudget> UtilisateurBudget { get;set; }

        public async Task OnGetAsync()
        {
            UtilisateurBudget = await _context.UtilisateurBudgets.ToListAsync();
        }
    }
}
