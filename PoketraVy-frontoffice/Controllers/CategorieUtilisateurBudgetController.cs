using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace PoketraVy_frontoffice.Controllers
{
    public class CategorieUtilisateurBudgetController : Controller
    {
        private readonly CategorieUtilisateurBudgetRepository _categorieUtilisateurBudgetRepository;
        private readonly BudgetRepository _budgetRepository;

        public CategorieUtilisateurBudgetController(CategorieUtilisateurBudgetRepository categorieUtilisateurBudgetRepository,
            BudgetRepository budgetRepository)
        {
            _categorieUtilisateurBudgetRepository = categorieUtilisateurBudgetRepository;
            _budgetRepository = budgetRepository;
        }

        // GET: CategorieUtilisateurBudget
        public IActionResult Index()
        {
            IEnumerable<CategorieUtilisateurBudget> categorieUtilisateurBudgets = _categorieUtilisateurBudgetRepository.GetAll();
            return View(categorieUtilisateurBudgets);
        }

        // GET: CategorieUtilisateurBudget/Details/5
        public IActionResult Details(int id)
        {
            CategorieUtilisateurBudget categorieUtilisateurBudget = _categorieUtilisateurBudgetRepository.GetById(id);
            if (categorieUtilisateurBudget == null)
            {
                return NotFound();
            }
            return View(categorieUtilisateurBudget);
        }

        // GET: CategorieUtilisateurBudget/Create
        public IActionResult Create()
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var activeBudgets = _budgetRepository.GetActiveBudgetsByIdUser(userId);

            var viewModel = new CategorieUtilisateurBudgetCreationViewModel
            {
                ActiveBudgets = (List<Budget>)activeBudgets
            };

            return View(viewModel);
        }

        // POST: CategorieUtilisateurBudget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategorieUtilisateurBudget categorieUtilisateurBudget)
        {
            if (ModelState.IsValid)
            {
                _categorieUtilisateurBudgetRepository.Add(categorieUtilisateurBudget);
                return RedirectToAction(nameof(Index));
            }
            return View(categorieUtilisateurBudget);
        }

        // GET: CategorieUtilisateurBudget/Edit/5
        public IActionResult Edit(int id)
        {
            CategorieUtilisateurBudget categorieUtilisateurBudget = _categorieUtilisateurBudgetRepository.GetById(id);
            if (categorieUtilisateurBudget == null)
            {
                return NotFound();
            }
            return View(categorieUtilisateurBudget);
        }

        // POST: CategorieUtilisateurBudget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategorieUtilisateurBudget categorieUtilisateurBudget)
        {
            if (id != categorieUtilisateurBudget.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _categorieUtilisateurBudgetRepository.Update(categorieUtilisateurBudget);
                return RedirectToAction(nameof(Index));
            }
            return View(categorieUtilisateurBudget);
        }

        // GET: CategorieUtilisateurBudget/Delete/5
        public IActionResult Delete(int id)
        {
            CategorieUtilisateurBudget categorieUtilisateurBudget = _categorieUtilisateurBudgetRepository.GetById(id);
            if (categorieUtilisateurBudget == null)
            {
                return NotFound();
            }
            return View(categorieUtilisateurBudget);
        }

        // POST: CategorieUtilisateurBudget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categorieUtilisateurBudgetRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}