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

        public List<Utilisateur> Utilisateurs { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }

        public int PageSize => 3;

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 3)
        {
            var totalCount = await _service.GetUtilisateursCountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = pageNumber;

            Utilisateurs = await _service.GetUtilisateursWithPaginationAsync(pageNumber, pageSize);
        }
    }
}
