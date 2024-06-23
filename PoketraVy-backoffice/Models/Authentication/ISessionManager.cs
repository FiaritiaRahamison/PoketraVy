using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Models.Authentication
{
    public interface ISessionManager
    {
        void ClearSession();
    }
}
