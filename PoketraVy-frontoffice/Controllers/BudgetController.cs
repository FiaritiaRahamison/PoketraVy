using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetRepository _budgetRepository;

        public BudgetController(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        // GET: BudgetController
        public ActionResult Index()
        {
            var budgets = _budgetRepository.GetAllBudgets();
            return View(budgets);
        }

        // GET: BudgetController/Details/5
        public ActionResult Details(int id)
        {
            var budget = _budgetRepository.GetById(id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: BudgetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BudgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Budget budget)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _budgetRepository.Add(budget);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(budget);
        }

        // GET: BudgetController/Edit/5
        public ActionResult Edit(int id)
        {
            var budget = _budgetRepository.GetById(id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: BudgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Budget budget)
        {
            try
            {
                _budgetRepository.Update(budget);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BudgetController/Delete/5
        public ActionResult Delete(int id)
        {
            var budget = _budgetRepository.GetById(id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: BudgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _budgetRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
