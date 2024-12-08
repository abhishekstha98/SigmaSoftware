using CandidateHubAPI.Models;
using CandidateHubAPI.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CandidateService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            const string cacheKey = "AllCandidates";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Candidate> candidates))
            {
                candidates = await _unitOfWork.Repository<Candidate>().ListAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, candidates, cacheOptions);
            }

            return candidates;
        }

        public async Task<Candidate> UpsertCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _unitOfWork.Repository<Candidate>().GetByEmailAsync(candidate.Email);

            if (existingCandidate == null)
            {
                await _unitOfWork.Repository<Candidate>().AddAsync(candidate);
            }
            else
            {
                existingCandidate.FirstName = !string.IsNullOrWhiteSpace(candidate.FirstName)
                    ? candidate.FirstName
                    : existingCandidate.FirstName;

                existingCandidate.LastName = !string.IsNullOrWhiteSpace(candidate.LastName)
                    ? candidate.LastName
                    : existingCandidate.LastName;

                existingCandidate.PhoneNumber = !string.IsNullOrWhiteSpace(candidate.PhoneNumber)
                    ? candidate.PhoneNumber
                    : existingCandidate.PhoneNumber;

                existingCandidate.CallTimeInterval = !string.IsNullOrWhiteSpace(candidate.CallTimeInterval)
                    ? candidate.CallTimeInterval
                    : existingCandidate.CallTimeInterval;

                existingCandidate.LinkedInUrl = !string.IsNullOrWhiteSpace(candidate.LinkedInUrl)
                    ? candidate.LinkedInUrl
                    : existingCandidate.LinkedInUrl;

                existingCandidate.GitHubUrl = !string.IsNullOrWhiteSpace(candidate.GitHubUrl)
                    ? candidate.GitHubUrl
                    : existingCandidate.GitHubUrl;

                existingCandidate.Comment = !string.IsNullOrWhiteSpace(candidate.Comment)
                    ? candidate.Comment
                    : existingCandidate.Comment;

                await _unitOfWork.Repository<Candidate>().UpdateAsync(existingCandidate);
            }

            await _unitOfWork.SaveAsync();

            _cache.Remove($"Candidate_{candidate.Email}");
            _cache.Remove("AllCandidates");

            return candidate;
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            var cacheKey = $"CandidateById_{id}";

            if (!_cache.TryGetValue(cacheKey, out Candidate candidate))
            {
                candidate = await _unitOfWork.Repository<Candidate>().GetByIdAsync(id);

                if (candidate != null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, candidate, cacheOptions);
                }
            }

            return candidate;
        }

        public async Task<Candidate> SearchCandidateByEmailAsync(string email)
        {
            var cacheKey = $"Candidate_{email}";

            if (!_cache.TryGetValue(cacheKey, out Candidate candidate))
            {
                candidate = await _unitOfWork.Repository<Candidate>().GetByEmailAsync(email);

                if (candidate != null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, candidate, cacheOptions);
                }
            }

            return candidate;
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
            await _unitOfWork.Repository<Candidate>().AddAsync(candidate);
            await _unitOfWork.SaveAsync();

            _cache.Remove("AllCandidates");
        }

        public async Task ScheduleInterviewsAsync()
        {
            var candidates = await _unitOfWork.Repository<Candidate>().ListAllAsync();
            var candidatesToSchedule = new List<Candidate>();

            foreach (var candidate in candidates)
            {
                if (!candidate.SentEmail)
                {
                    candidate.InterviewTime = DateTime.Now.AddDays(1); 
                    candidate.SentEmail = true;
                    candidatesToSchedule.Add(candidate);

                    SendDummyEmail(candidate);
                }
            }

            if (candidatesToSchedule.Count > 0)
            {
                await _unitOfWork.SaveAsync();
            }
        }

        private void SendDummyEmail(Candidate candidate)
        {
            Console.WriteLine($"Email sent to {candidate.Email}: Interview scheduled on {candidate.InterviewTime}");
        }

        public async Task<bool> UpdateSelectionStatusAsync(string email, bool isSelected)
        {
            var candidate = await _unitOfWork.Repository<Candidate>().GetByEmailAsync(email);
            if (candidate == null) return false;

            if (isSelected)
            {
                var onboardedCandidate = new OnboardedCandidate
                {
                    Email = candidate.Email,
                    FullName = $"{candidate.FirstName} {candidate.LastName}",
                    OnboardedDate = DateTime.Now
                };

                await _unitOfWork.Repository<OnboardedCandidate>().AddAsync(onboardedCandidate);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
