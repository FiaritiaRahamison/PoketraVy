using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Models
{
    public class Utilisateur
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }
    }
}
