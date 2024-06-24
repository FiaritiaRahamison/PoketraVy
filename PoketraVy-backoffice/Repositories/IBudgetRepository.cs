using Microsoft.VisualBasic;
using PoketraVy_backoffice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<List<Budget>> GetBudgetsAsync(DateTime min,DateTime max);
    }
}
