using CandidateHubAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Services
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task AddCandidateAsync(Candidate candidate);
        Task<Candidate> SearchCandidateByEmailAsync(string email);
        Task<Candidate> UpsertCandidateAsync(Candidate candidate);
        Task ScheduleInterviewsAsync();

    }
}
