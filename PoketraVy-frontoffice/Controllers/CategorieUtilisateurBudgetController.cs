using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System.Collections.Generic;

namespace PoketraVy_frontoffice.Controllers
{
    public class CategorieUtilisateurBudgetController : Controller
    {
        private readonly CategorieUtilisateurBudgetRepository _categorieUtilisateurBudgetRepository;

        public CategorieUtilisateurBudgetController(CategorieUtilisateurBudgetRepository categorieUtilisateurBudgetRepository)
        {
            _categorieUtilisateurBudgetRepository = categorieUtilisateurBudgetRepository;
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
            return View();
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