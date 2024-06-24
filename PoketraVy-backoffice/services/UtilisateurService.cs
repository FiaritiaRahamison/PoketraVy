using PoketraVy_backoffice.Models;
using PoketraVy_backoffice.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IRepository<Utilisateur> _utilisateurRepository;

        public UtilisateurService(IRepository<Utilisateur> utilisateurRepository)
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
    }
}
