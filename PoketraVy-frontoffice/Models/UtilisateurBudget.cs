using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PoketraVy_frontoffice.Models

{
    public class UtilisateurBudget
    {
        public int ID { get; set; }
        public int IdUtilisateur { get; set; }
        public int IdBudget { get; set; }

        public double? Montant { get; set; }
        public string Remarque { get; set; }

        public Utilisateur Utilisateur{ get; set; }
        public Budget Budget { get; set; }

    }
}
