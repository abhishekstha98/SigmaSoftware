using CandidateHubAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Services
{
    public interface IInterviewScoreService
    {
        Task<IEnumerable<InterviewScore>> GetAllScoresAsync();
        Task<InterviewScore> GetScoreByIdAsync(int id);
        Task<IEnumerable<InterviewScore>> GetScoresByCandidateIdAsync(int candidateId);
        Task AddScoreAsync(InterviewScore score);
        Task UpdateScoreAsync(InterviewScore score);
        Task DeleteScoreAsync(int id);
    }

}
