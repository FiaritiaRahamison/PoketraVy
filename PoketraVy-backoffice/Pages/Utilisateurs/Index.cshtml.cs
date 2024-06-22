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
    public class IndexModel : PageModel
    {
        private readonly PoketraVy_backoffice.Data.PoketraVy_backofficeContext _context;

        public IndexModel(PoketraVy_backoffice.Data.PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        public IList<Utilisateur> Utilisateur { get;set; }

        public async Task OnGetAsync()
        {
            Utilisateur = await _context.Utilisateurs.ToListAsync();
        }
    }
}
