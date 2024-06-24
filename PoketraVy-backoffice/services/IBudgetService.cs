using Microsoft.VisualBasic;
using PoketraVy_backoffice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.services
{
    public interface IBudgetService
    {
        Task<Budget> CreateObject(Budget budget,List<UtilisateurBudget> utilisateurBudgets);
        Task<Budget> GetBudget(int budgetId);
        Task<List<UtilisateurBudget>> GetUtilisateurBudgetsToCreate();
        Task<List<Budget>> GetBudgets(DateTime min, DateTime max);
    }
}
