using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHubAPI.Controllers;
using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class InterviewScoreControllerTests
{
    [Fact]
    public async Task GetAllScores_ReturnsOkResult()
    {
        var mockScoreService = new Mock<IInterviewScoreService>();
        mockScoreService.Setup(service => service.GetAllScoresAsync()).ReturnsAsync(new List<InterviewScore>());
        var mockCandidateService = new Mock<ICandidateService>();
        var controller = new InterviewScoreController(mockScoreService.Object, mockCandidateService.Object);

        var result = await controller.GetAllScores();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetScoreById_ReturnsNotFound_WhenScoreDoesNotExist()
    {
        var mockScoreService = new Mock<IInterviewScoreService>();
        mockScoreService.Setup(service => service.GetScoreByIdAsync(It.IsAny<int>())).ReturnsAsync((InterviewScore)null);
        var mockCandidateService = new Mock<ICandidateService>();
        var controller = new InterviewScoreController(mockScoreService.Object, mockCandidateService.Object);

        var result = await controller.GetScoreById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddScore_ReturnsCreatedAtActionResult()
    {
        var mockScoreService = new Mock<IInterviewScoreService>();
        var mockCandidateService = new Mock<ICandidateService>();
        var score = new InterviewScore { Id = 1, CandidateId = 1, TechnicalScore = 80, CommunicationScore = 85, ProblemSolvingScore = 90 };
        mockScoreService.Setup(service => service.AddScoreAsync(score)).Returns(Task.CompletedTask);
        var controller = new InterviewScoreController(mockScoreService.Object, mockCandidateService.Object);

        var result = await controller.AddScore(score, string.Empty);

        Assert.IsType<CreatedAtActionResult>(result);
    }
}
