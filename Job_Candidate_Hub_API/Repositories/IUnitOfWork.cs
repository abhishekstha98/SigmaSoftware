using System.Threading.Tasks;

namespace CandidateHubAPI.Repositories
{
    public interface IUnitOfWork
    {
        ICandidateRepository<T> Repository<T>() where T : class;
        Task SaveAsync();
    }
}
