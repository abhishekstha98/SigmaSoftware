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

            // Check if the data is already in the cache
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Candidate> candidates))
            {
                // Fetch data from the database
                candidates = await _unitOfWork.Repository<Candidate>().ListAllAsync();

                // Cache the data for 10 minutes
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
                // Add new candidate
                await _unitOfWork.Repository<Candidate>().AddAsync(candidate);
            }
            else
            {
                // Update existing candidate with provided values, retain old values for empty fields
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

            // Invalidate relevant cache entries
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

            // Invalidate cache entries
            _cache.Remove("AllCandidates");
        }

        public async Task ScheduleInterviewsAsync()
        {
            // Fetch all candidates whose SentEmail is false
            var candidates = await _unitOfWork.Repository<Candidate>().ListAllAsync();
            var candidatesToSchedule = new List<Candidate>();

            foreach (var candidate in candidates)
            {
                if (!candidate.SentEmail)
                {
                    // Schedule an interview time for tomorrow
                    candidate.InterviewTime = DateTime.Now.AddDays(1); // Example: Schedule for tomorrow
                    candidate.SentEmail = true; // Mark as email sent
                    candidatesToSchedule.Add(candidate);

                    // Simulate sending a dummy email
                    SendDummyEmail(candidate);
                }
            }

            // Save updates to the database
            if (candidatesToSchedule.Count > 0)
            {
                await _unitOfWork.SaveAsync();
            }
        }

        private void SendDummyEmail(Candidate candidate)
        {
            // Simulated dummy email sending
            Console.WriteLine($"Email sent to {candidate.Email}: Interview scheduled on {candidate.InterviewTime}");
        }
    }
}
