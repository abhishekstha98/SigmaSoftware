using System.ComponentModel.DataAnnotations;
using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewScoreController : ControllerBase
    {
        private readonly IInterviewScoreService _scoreService;
        private readonly ICandidateService _candidateService;

        public InterviewScoreController(IInterviewScoreService scoreService, ICandidateService candidateService)
        {
            _scoreService = scoreService;
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScores()
        {
            var scores = await _scoreService.GetAllScoresAsync();
            return Ok(scores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScoreById(int id)
        {
            var score = await _scoreService.GetScoreByIdAsync(id);
            if (score == null)
                return NotFound();

            return Ok(score);
        }

        [HttpGet("candidate/email/{email}")]
        public async Task<IActionResult> GetScoresByCandidateEmail(string email)
        {
            try
            {
                // Validate email format
                if (!IsValidEmail(email))
                    return BadRequest(new { Message = "Invalid email format." });

                var candidate = await _candidateService.SearchCandidatesAsync(email);
                if (candidate.FirstOrDefault() == null)
                    return NotFound(new { Message = "Candidate not found." });

                var scores = await _scoreService.GetScoresByCandidateIdAsync(candidate.FirstOrDefault().Id);
                if (!scores.Any())
                    return NotFound(new { Message = "No scores found for the specified candidate." });

                return Ok(scores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving scores.", Details = ex.Message });
            }
        }


        private bool IsValidEmail(string email)
        {
            var emailValidator = new EmailAddressAttribute();
            return emailValidator.IsValid(email);
        }


        [HttpPost]
        public async Task<IActionResult> AddScore([FromBody] InterviewScore score, string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(email))
            {
                if (!IsValidEmail(email))
                    return BadRequest(new { Message = "Invalid email format." });
                var candidate = await _candidateService.SearchCandidatesAsync(email);
                if (candidate == null)
                    return NotFound(new { Message = "Candidate with the provided email not found." });

                score.CandidateId = candidate.FirstOrDefault().Id;
            }

            if (score.CandidateId == 0)
                return BadRequest(new { Message = "CandidateId is required when email is not provided." });

            await _scoreService.AddScoreAsync(score);
            return CreatedAtAction(nameof(GetScoreById), new { id = score.Id }, score);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScore(int id, [FromBody] InterviewScore score)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingScore = await _scoreService.GetScoreByIdAsync(id);
            if (existingScore == null)
                return NotFound();

            score.Id = id;
            await _scoreService.UpdateScoreAsync(score);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var existingScore = await _scoreService.GetScoreByIdAsync(id);
            if (existingScore == null)
                return NotFound();

            await _scoreService.DeleteScoreAsync(id);
            return NoContent();
        }
    }

}
