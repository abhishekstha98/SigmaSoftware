using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null)
                return NotFound(new { Message = "Candidate not found" });

            return Ok(candidate);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { Message = "Email is required for search." });
            }

            var candidate = await _candidateService.SearchCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound(new { Message = "Candidate not found." });
            }

            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _candidateService.UpsertCandidateAsync(candidate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the request.", Details = ex.Message });
            }
        }

        [HttpPost("schedule-interviews")]
        public async Task<IActionResult> ScheduleInterviews()
        {
            try
            {
                await _candidateService.ScheduleInterviewsAsync();
                return Ok(new { Message = "Interviews scheduled and emails sent to candidates who haven't received emails." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while scheduling interviews.", Details = ex.Message });
            }
        }

    }
}
