using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;
using Microsoft.VisualBasic;
using System.Linq;

namespace PoketraVy_backoffice.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly PoketraVy_backofficeContext _context;

        public BudgetRepository(PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        public async Task<Budget> Create(Budget entity)
        {
            await _context.Budgets.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Budgets.FindAsync(id);
            if (entity != null)
            {
                _context.Budgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Budget> Get(int id)
        {
            return await _context.Budgets
                .Include(b => b.UtilisateurBudget)
                .ThenInclude(ub => ub.Utilisateur)
                .FirstOrDefaultAsync(b => b.ID==id);
        }

        public async Task<List<Budget>> GetAll()
        {
            return await _context.Budgets.Include(b=>b.UtilisateurBudget).ToListAsync();
        }

        public async Task<List<Budget>> GetBudgetsAsync(DateTime min, DateTime max)
        {
            return await _context.Budgets
            .Where(b => b.Daty >= min && b.Daty <= max)
            .Include(b => b.UtilisateurBudget)
            .ThenInclude(ub => ub.Utilisateur)
            .ToListAsync();
        }

        public async Task<Budget> Update(Budget entity)
        {
            _context.Budgets.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
