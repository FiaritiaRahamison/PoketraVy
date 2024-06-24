using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public class Mouvement
    {
        public int ID { get; set; }
        public int IdCategorieUtilisateurBudget { get; set; }
        public string Designation { get; set; }
        public double montant { get; set; }
        public DateTime daty { get; set; }

        public CategorieUtilisateurBudget CategorieUtilisateurBudget { get; set; }
    }
}
