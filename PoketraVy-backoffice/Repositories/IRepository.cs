using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoketraVy_backoffice.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
