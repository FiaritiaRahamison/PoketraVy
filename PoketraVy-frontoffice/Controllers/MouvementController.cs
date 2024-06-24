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
    public class MouvementController : Controller
    {
        private readonly MouvementRepository _mouvementRepository;

        public MouvementController(MouvementRepository mouvementRepository)
        {
            _mouvementRepository = mouvementRepository;
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
            return View();
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
            return View(mouvement);
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
