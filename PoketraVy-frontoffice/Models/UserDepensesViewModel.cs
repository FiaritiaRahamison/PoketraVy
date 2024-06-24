using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public class UserDepensesViewModel
    {
        public List<UtilisateurBudget> UtilisateurBudgets { get; set; }
        public Dictionary<int, List<CategorieUtilisateurBudget>> CategorieUtilisateurBudgets { get; set; }
        public List<MouvementDetails> Mouvements { get; set; }
        public double depenses { get; set; }
        public double? totalMontant { get; set; }
    }
}
