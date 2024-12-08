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
        public async Task<IActionResult> SearchCandidates([FromQuery] string searchTerm)
        {
            try
            {
                var results = await _candidateService.SearchCandidatesAsync(searchTerm);

                if (!results.Any())
                    return NotFound(new { Message = "No candidates match the search term." });

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while searching for candidates.", Details = ex.Message });
            }
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


        [HttpPost("update-selection")]
        public async Task<IActionResult> UpdateSelectionStatus([FromQuery] string email, [FromQuery] bool isSelected)
        {
            var success = await _candidateService.UpdateSelectionStatusAsync(email, isSelected);
            if (!success) return NotFound(new { Message = "Candidate not found." });

            return Ok(new { Message = "Candidate selection status updated." });
        }
    }
}
