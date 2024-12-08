using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Repositories
{
    public interface ICandidateRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByEmailAsync(string email);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
