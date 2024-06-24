using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PoketraVy_frontoffice.Controllers
{
    public class MouvementController : Controller
    {
        private readonly MouvementRepository _mouvementRepository;
        private readonly BudgetRepository _budgetRepository;

        public MouvementController(MouvementRepository mouvementRepository,
            BudgetRepository budgetRepository)
        {
            _mouvementRepository = mouvementRepository;
            _budgetRepository = budgetRepository;
        }

        // GET: MouvementController
        public ActionResult Index()
        {
            var mouvements = _mouvementRepository.GetAll();
            return View(mouvements);
        }

        // GET: MouvementController/Details/5
        public ActionResult Details(int id)
        {
            var mouvement = _mouvementRepository.GetById(id);
            if (mouvement == null)
            {
                return NotFound();
            }

            return View(mouvement);
        }

        // GET: MouvementController/Create
        public ActionResult Create()
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            ViewBag.Categories = Enum.GetValues(typeof(Categorie)).Cast<Categorie>().ToList();
            var budgetsValides = _budgetRepository.GetActiveBudgetsByIdUser(userId);
            var categories = Enum.GetValues(typeof(Categorie)).Cast<Categorie>().ToList();

            var viewModel = new MouvementCreationViewModel
            {
                ActiveBudgets = (List<Budget>)budgetsValides,

                Categories = categories,
                Mouvement = new Mouvement(),
                IdBudget = null
            };

            return View(viewModel);
        }

        // POST: MouvementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mouvement mouvement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _mouvementRepository.Add(mouvement);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return RedirectToAction("UserDepenses", "Utilisateur");
        }

        // GET: MouvementController/Edit/5
        public ActionResult Edit(int id)
        {
            var mouvement = _mouvementRepository.GetById(id);
            if (mouvement == null)
            {
                return NotFound();
            }

            return View(mouvement);
        }

        // POST: MouvementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Mouvement mouvement)
        {
            try
            {
                _mouvementRepository.Update(mouvement);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MouvementController/Delete/5
        public ActionResult Delete(int id)
        {
            var mouvement = _mouvementRepository.GetById(id);
            if (mouvement == null)
            {
                return NotFound();
            }

            return View(mouvement);
        }

        // POST: MouvementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _mouvementRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
