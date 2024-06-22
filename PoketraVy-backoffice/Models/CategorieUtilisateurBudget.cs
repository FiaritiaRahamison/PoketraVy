using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Models
{
    public enum Categorie
    {
        Frais, Nourriture, Hygiène, Autres
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
    }
}
