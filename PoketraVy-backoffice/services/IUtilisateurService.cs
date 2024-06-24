using PoketraVy_backoffice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.services
{
    public interface IUtilisateurService
    {
        Task<Utilisateur> GetUtilisateur(int id);
        Task<List<Utilisateur>> GetUtilisateurs();
        Task<List<Utilisateur>> GetUtilisateursWithPaginationAsync(int pageNumber, int pageSize);
        public Task<int> GetUtilisateursCountAsync();

    }
}
