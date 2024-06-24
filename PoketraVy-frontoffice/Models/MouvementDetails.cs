using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public class MouvementDetails
    {
        public Mouvement mouvement { get; set; }
        public int IdUtilisateurBudget { get; set; }
        public PoketraVy_frontoffice.Models.Categorie Categorie { get; set; }
    }
}
