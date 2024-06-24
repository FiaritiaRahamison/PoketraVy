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

namespace PoketraVy_backoffice.Pages.Budgets
{
    public class DetailsModel : PageModel
    {
        private readonly IBudgetService _service;
        
        [BindProperty]
        public List<UtilisateurBudget> UtilisateurBudgets { get; set; }

        public Budget Budget { get; set; }


        public DetailsModel(IBudgetService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Budget = await _service.GetBudget((int)id);
            if (Budget == null)
            {
                return NotFound();
            }

            // Check if UtilisateurBudget is null before converting to List
            UtilisateurBudgets = Budget.UtilisateurBudget?.ToList() ?? new List<UtilisateurBudget>();

            return Page();
        }
    }
}
