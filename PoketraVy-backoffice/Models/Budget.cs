using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Models
{
    public class Budget
    {
        public int ID { get; set; }
        public double Montant { get; set; }
        public DateTime Daty { get; set; }
        public DateTime DatyFinPrevisionnel { get; set; }
        public bool Etat { get; set; }
        public string Remarque { get; set; }

        public ICollection<UtilisateurBudget> UtilisateurBudget { get; set; }
    }
}
