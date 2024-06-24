using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Controllers
{
    public class UtilisateurController : Controller
    {

        private readonly UtilisateurRepository _utilisateurRepository;
        private readonly UtilisateurBudgetRepository _utilisateurBudgetRepository;
        private readonly CategorieUtilisateurBudgetRepository _categorieUtilisateurBudgetRepository;
        private readonly MouvementRepository _mouvementRepository;

        public UtilisateurController(UtilisateurRepository utilisateurRepository,
            UtilisateurBudgetRepository utilisateurBudgetRepository,
            CategorieUtilisateurBudgetRepository categorieUtilisateurBudgetRepository,
            MouvementRepository mouvementRepository)
        {
            _utilisateurRepository = utilisateurRepository;
            _utilisateurBudgetRepository = utilisateurBudgetRepository;
            _categorieUtilisateurBudgetRepository = categorieUtilisateurBudgetRepository;
            _mouvementRepository = mouvementRepository;
        }

        // GET: UtilisateurController
        public ActionResult Index()
        {
            var users = _utilisateurRepository.GetAllUtilisateurs();
            return View(users);
        }

        // GET: UtilisateurController/Details/5
        public ActionResult Details(int id)
        {
            var utilisateur = _utilisateurRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: UtilisateurController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilisateurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur utilisateur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _utilisateurRepository.Add(utilisateur);
                    return RedirectToAction(nameof(Index));
                }
                return View(utilisateur);
            }
            catch
            {
                return View(utilisateur);
            }
        }

        // GET: UtilisateurController/Edit/5
        public ActionResult Edit(int id)
        {
            var utilisateur = _utilisateurRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: UtilisateurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Utilisateur utilisateur)
        {
            try
            {
                _utilisateurRepository.Update(utilisateur);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilisateurController/Delete/5
        public ActionResult Delete(int id)
        {
            var utilisateur = _utilisateurRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: UtilisateurController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _utilisateurRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult UserBudgets()
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var utilisateurBudgets = _utilisateurBudgetRepository.GetUtilisateurBudgetsByUserId(userId);
            var categorieUtilisateurBudgets = new Dictionary<int, List<CategorieUtilisateurBudget>>();
            var depensesListe = _mouvementRepository.GetByIdUser(userId);

            var depenses = 0.0;



            foreach (var budget in utilisateurBudgets)
            {
                var categories = _categorieUtilisateurBudgetRepository.GetCategorieUtilisateurBudgetsByUtilisateurBudgetId(budget.ID);
                categorieUtilisateurBudgets[budget.ID] = categories.ToList();
            }

            foreach (var depense in depensesListe)
            {
                depenses += depense.montant;
            }

            var viewModel = new UserBudgetsViewModel
            {
                UtilisateurBudgets = utilisateurBudgets.ToList(),
                CategorieUtilisateurBudgets = categorieUtilisateurBudgets,
                depenses = depenses
            };

            return View(viewModel);
        }
    }
}
