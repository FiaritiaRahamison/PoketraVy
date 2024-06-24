using Microsoft.EntityFrameworkCore;
using PoketraVy_backoffice.Data;
using PoketraVy_backoffice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace PoketraVy_backoffice.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly PoketraVy_backofficeContext _context;

        public UtilisateurRepository(PoketraVy_backofficeContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Utilisateurs.CountAsync();
        }

        public async Task<Utilisateur> Create(Utilisateur entity)
        {
            await _context.Utilisateurs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Utilisateurs.FindAsync(id);
            if (entity != null)
            {
                _context.Utilisateurs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Utilisateur> Get(int id)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(b => b.ID == id);
        }

        public async Task<List<Utilisateur>> GetAll()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<List<Utilisateur>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _context.Utilisateurs
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        }

        public async Task<Utilisateur> Update(Utilisateur entity)
        {
            _context.Utilisateurs.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
