using CandidateHubAPI.Data;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CandidateHubAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICandidateRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repository = new CandidateRepository<T>(_context);
                _repositories.TryAdd(type, repository);
            }

            return (ICandidateRepository<T>)_repositories[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
