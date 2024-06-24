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

        public MouvementApiController(CategorieUtilisateurBudgetRepository categorieUtilisateurBudgetRepositoryRepository)
        {
            _categorieUtilisateurBudgetRepositoryRepository = categorieUtilisateurBudgetRepositoryRepository;
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
