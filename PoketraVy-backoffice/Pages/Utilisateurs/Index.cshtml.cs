using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;
using PoketraVy_backoffice.services;

namespace PoketraVy_backoffice.Pages.Utilisateurs
{
    public class IndexModel : PageModel
    {
        private readonly IUtilisateurService _service;

        public IndexModel(IUtilisateurService service)
        {
            _service = service;
        }

        public IList<Utilisateur> Utilisateur { get;set; }

        public async Task OnGetAsync()
        {
            Utilisateur = await _service.GetUtilisateurs();
        }
    }
}
