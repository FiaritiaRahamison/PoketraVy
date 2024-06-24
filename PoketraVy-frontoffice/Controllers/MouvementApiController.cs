using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoketraVy_frontoffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MouvementApiController : ControllerBase
    {
        private readonly CategorieUtilisateurBudgetRepository _categorieUtilisateurBudgetRepositoryRepository;
        private readonly UtilisateurBudgetRepository _utilisateurBudgetRepositoryRepository;

        public MouvementApiController(CategorieUtilisateurBudgetRepository categorieUtilisateurBudgetRepositoryRepository,
            UtilisateurBudgetRepository utilisateurBudgetRepositoryRepository)
        {
            _categorieUtilisateurBudgetRepositoryRepository = categorieUtilisateurBudgetRepositoryRepository;
            _utilisateurBudgetRepositoryRepository = utilisateurBudgetRepositoryRepository;
        }

        [HttpGet("GetCategorieUtilisateurBudgetId")]
        public IActionResult GetCategorieUtilisateurBudgetId(int IdBudget, int Categorie)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var idCategorieUtilisateurBudget = _categorieUtilisateurBudgetRepositoryRepository.GetByIdUtilisateurBudgetAndCategorie(userId, IdBudget, Categorie);
            if (idCategorieUtilisateurBudget == 0)
            {
                return NotFound();
            }
            return Ok(idCategorieUtilisateurBudget);
        }

        [HttpGet("GetUtilisateurBudgetsId")]
        public IActionResult GetUtilisateurBudgetsId(int IdBudget)
        {
            int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            int idUtilisateurBudget = _utilisateurBudgetRepositoryRepository.GetUtilisateurBudgetId(userId, IdBudget);
            if (idUtilisateurBudget == 0)
            {
                return NotFound();
            }
            return Ok(idUtilisateurBudget);
        }

        [HttpGet("GetCategorieIdByName")]
        public IActionResult GetCategorieIdByName(string name)
        {
            if (Enum.TryParse(typeof(PoketraVy_frontoffice.Models.Categorie), name, out var categorie))
            {
                return Ok((int)categorie);
            }
            return NotFound();
        }
    }
}
