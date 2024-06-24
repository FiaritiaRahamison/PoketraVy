using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PoketraVy_frontoffice.Models
{
    public class CategorieUtilisateurBudgetCreationViewModel
    {
        public List<Budget> ActiveBudgets { get; set; }

        [Required(ErrorMessage = "Le champ 'IdUtilisateurBudget' est requis.")]
        public int IdUtilisateurBudget { get; set; }

        [Required(ErrorMessage = "Le champ 'Designation' est requis.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Le champ 'Categorie' est requis.")]
        public Categorie Categorie { get; set; }

        [Required(ErrorMessage = "Le champ 'Montant' est requis.")]
        [Range(0, double.MaxValue, ErrorMessage = "Le champ 'Montant' doit être supérieur ou égal à zéro.")]
        public double Montant { get; set; }

        [Required(ErrorMessage = "Le champ 'Daty' est requis.")]
        public DateTime Daty { get; set; }
    }
}
