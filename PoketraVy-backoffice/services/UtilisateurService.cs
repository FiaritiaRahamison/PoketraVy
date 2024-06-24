using PoketraVy_backoffice.Models;
using PoketraVy_backoffice.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }

        public async Task<Utilisateur> GetUtilisateur(int id)
        {
            return await _utilisateurRepository.Get(id);
        }

        public async Task<List<Utilisateur>> GetUtilisateurs()
        {
            return await _utilisateurRepository.GetAll();
        }

        public async Task<int> GetUtilisateursCountAsync()
        {
            return await _utilisateurRepository.CountAsync();
        }

        public async Task<List<Utilisateur>> GetUtilisateursWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await _utilisateurRepository.GetWithPaginationAsync(pageNumber, pageSize);
        }

    }
}
