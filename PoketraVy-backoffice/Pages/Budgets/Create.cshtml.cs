using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;
using PoketraVy_backoffice.services;

namespace PoketraVy_backoffice.Pages.Budgets
{
    public class CreateModel : PageModel
    {
        private readonly IBudgetService _service;

        [BindProperty]
        public Budget Budget { get; set; }

        [BindProperty]
        public List<UtilisateurBudget> UtilisateurBudgets { get; set; }

        public CreateModel(IBudgetService service)
        {
            _service = service;
            
        }

        public async Task<IActionResult> OnGet()
        {
            UtilisateurBudgets = await _service.GetUtilisateurBudgetsToCreate();
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Budget.Etat = false;
            try{
                await _service.CreateObject(Budget, UtilisateurBudgets);
            }catch (LogicException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Console.WriteLine(ex.StackTrace);
                return Page();
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "une erreur s'est produite");
                Console.WriteLine(ex.StackTrace);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
