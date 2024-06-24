using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public class CategorieUtilisateurBudgetDetails
    {
        public int ID { get; set; }
        public int IdUtilisateurBudget { get; set; }
        public string Designation { get; set; }
        public Categorie? Categorie { get; set; }
        public double Montant { get; set; }
        public DateTime Daty { get; set; }

        public UtilisateurBudget UtilisateurBudget { get; set; }
        public ICollection<Mouvement> Mouvement { get; set; }

    }
}
