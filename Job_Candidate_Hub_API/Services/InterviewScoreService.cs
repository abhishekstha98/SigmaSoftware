using CandidateHubAPI.Models;
using CandidateHubAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Services
{
    public class InterviewScoreService : IInterviewScoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InterviewScoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<InterviewScore>> GetAllScoresAsync()
        {
            return await _unitOfWork.Repository<InterviewScore>().ListAllAsync();
        }

        public async Task<InterviewScore> GetScoreByIdAsync(int id)
        {
            return await _unitOfWork.Repository<InterviewScore>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<InterviewScore>> GetScoresByCandidateIdAsync(int candidateId)
        {
            return await _unitOfWork.Repository<InterviewScore>()
                .GetQueryable()
                .Where(score => score.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task AddScoreAsync(InterviewScore score)
        {
            score.TotalScore = score.TechnicalScore + score.CommunicationScore + score.ProblemSolvingScore;

            await _unitOfWork.Repository<InterviewScore>().AddAsync(score);
            await _unitOfWork.SaveAsync();
        }


        public async Task UpdateScoreAsync(InterviewScore score)
        {
            await _unitOfWork.Repository<InterviewScore>().UpdateAsync(score);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteScoreAsync(int id)
        {
            var score = await _unitOfWork.Repository<InterviewScore>().GetByIdAsync(id);
            if (score != null)
            {
                await _unitOfWork.Repository<InterviewScore>().DeleteAsync(score);
                await _unitOfWork.SaveAsync();
            }
        }
    }

}
