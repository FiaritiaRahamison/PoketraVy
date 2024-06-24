using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public enum Categorie
    {
        Frais = 0, Nourriture = 1, Hygiène = 2, Autres = 3
    }
    public class CategorieUtilisateurBudget
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
