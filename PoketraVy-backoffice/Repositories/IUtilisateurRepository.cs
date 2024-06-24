using PoketraVy_backoffice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Repositories
{
    public interface IUtilisateurRepository : IRepository<Utilisateur>
    {
        Task<List<Utilisateur>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
    }
}
