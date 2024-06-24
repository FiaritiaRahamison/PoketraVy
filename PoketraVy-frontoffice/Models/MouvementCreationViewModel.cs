using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Models
{
    public class MouvementCreationViewModel
    {
        public List<Budget> ActiveBudgets { get; set; }
        public List<Categorie> Categories { get; set; }
        public Mouvement Mouvement { get; set; }

        public int? IdBudget { get; set; }
    }
}
