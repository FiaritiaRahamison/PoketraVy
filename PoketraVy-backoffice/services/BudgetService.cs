using Microsoft.VisualBasic;
using PoketraVy_backoffice.Models;
using PoketraVy_backoffice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PoketraVy_backoffice.services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IRepository<UtilisateurBudget> _utilisateurBudgetRepository;
        private readonly IUtilisateurService _utilisateurService;
        public BudgetService(IBudgetRepository budgetRepository, IRepository<UtilisateurBudget> utilisateurBudgetRepository,IUtilisateurService utilisateurService)
        {
            _budgetRepository = budgetRepository;
            _utilisateurBudgetRepository = utilisateurBudgetRepository;
            _utilisateurService = utilisateurService;
        }

        protected void BudgetCreationControl(Budget budget,List<UtilisateurBudget> utilisateurBudgets) {
            if(budget == null)
            {
                throw new LogicException("Budget invalide");
            }
            double totalAmout = utilisateurBudgets.Sum(ub => ub.Montant);
            if(totalAmout > budget.Montant) {
                throw new LogicException("Dispatch invalide, montant du budget < aux dispatchs");
            }
        }
        
        public async Task<Budget> CreateObject(Budget newBudget, List<UtilisateurBudget> utilisateurBudgets)
        {
            Budget createdBudget = null;
            BudgetCreationControl(newBudget, utilisateurBudgets);   
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Create the Budget
                createdBudget = await _budgetRepository.Create(newBudget);

                // Create UtilisateurBudgets
                foreach (var utilisateurBudget in utilisateurBudgets)
                {
                    utilisateurBudget.IdBudget = createdBudget.ID;
                    utilisateurBudget.Budget = createdBudget;
                    utilisateurBudget.Utilisateur = await _utilisateurService.GetUtilisateur(utilisateurBudget.Utilisateur.ID);
                    utilisateurBudget.IdUtilisateur = utilisateurBudget.Utilisateur.ID;
                    await _utilisateurBudgetRepository.Create(utilisateurBudget);
                }

                // Commit the transaction
                transaction.Complete();
            }
            return createdBudget;
        }

        public async Task<Budget> GetBudget(int id)
        {
            return await _budgetRepository.Get(id);
        }

        public async Task<List<Budget>> GetBudgets(DateTime min, DateTime max)
        {
            var budgets = await _budgetRepository.GetAll();
            return budgets.Where(b => b.Daty >= min && b.Daty <= max).ToList();
        }

        public async Task CloturerBudget(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var budget = await _budgetRepository.Get(id);
                    if (budget != null)
                    {
                        budget.Etat = true;  // Assuming Etat is a string indicating the state
                        await _budgetRepository.Update(budget);
                        transaction.Complete();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<List<UtilisateurBudget>> GetUtilisateurBudgetsToCreate()
        {
            List<UtilisateurBudget> utilisateurBudgets = new List<UtilisateurBudget>();
            var users = await _utilisateurService.GetUtilisateurs();
            foreach ( var user in users )
            {
                var utilisateurBudget = new UtilisateurBudget();
                utilisateurBudget.Utilisateur = user;
                utilisateurBudget.IdUtilisateur = user.ID;
                utilisateurBudget.Remarque = "";
                utilisateurBudget.Montant = 0.0;
                utilisateurBudgets.Add(utilisateurBudget);
            }
            return utilisateurBudgets;
        }

    }
}
