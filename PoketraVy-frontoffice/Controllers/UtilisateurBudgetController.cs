using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System.Collections.Generic;

namespace PoketraVy_frontoffice.Controllers
{
    public class UtilisateurBudgetController : Controller
    {
        private readonly UtilisateurBudgetRepository _utilisateurBudgetRepository;

        public UtilisateurBudgetController(UtilisateurBudgetRepository utilisateurBudgetRepository)
        {
            _utilisateurBudgetRepository = utilisateurBudgetRepository;
        }

        // GET: UtilisateurBudget
        public IActionResult Index()
        {
            IEnumerable<UtilisateurBudget> utilisateurBudgets = _utilisateurBudgetRepository.GetAll();
            return View(utilisateurBudgets);
        }

        // GET: UtilisateurBudget/Details/5
        public IActionResult Details(int id)
        {
            UtilisateurBudget utilisateurBudget = _utilisateurBudgetRepository.GetById(id);
            if (utilisateurBudget == null)
            {
                return NotFound();
            }
            return View(utilisateurBudget);
        }

        // GET: UtilisateurBudget/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UtilisateurBudget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UtilisateurBudget utilisateurBudget)
        {
            if (ModelState.IsValid)
            {
                _utilisateurBudgetRepository.Add(utilisateurBudget);
                return RedirectToAction("UserDepenses", "Utilisateur");
            }
            return View(utilisateurBudget);
        }

        // GET: UtilisateurBudget/Edit/5
        public IActionResult Edit(int id)
        {
            UtilisateurBudget utilisateurBudget = _utilisateurBudgetRepository.GetById(id);
            if (utilisateurBudget == null)
            {
                return NotFound();
            }
            return View(utilisateurBudget);
        }

        // POST: UtilisateurBudget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UtilisateurBudget utilisateurBudget)
        {
            if (id != utilisateurBudget.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _utilisateurBudgetRepository.Update(utilisateurBudget);
                return RedirectToAction(nameof(Index));
            }
            return View(utilisateurBudget);
        }

        // GET: UtilisateurBudget/Delete/5
        public IActionResult Delete(int id)
        {
            UtilisateurBudget utilisateurBudget = _utilisateurBudgetRepository.GetById(id);
            if (utilisateurBudget == null)
            {
                return NotFound();
            }
            return View(utilisateurBudget);
        }

        // POST: UtilisateurBudget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _utilisateurBudgetRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
