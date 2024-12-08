using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHubAPI.Controllers;
using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class CandidateControllerTests
{
    [Fact]
    public async Task GetAllCandidates_ReturnsOkResult()
    {
        var mockService = new Mock<ICandidateService>();
        mockService.Setup(service => service.GetAllCandidatesAsync()).ReturnsAsync(new List<Candidate>());
        var controller = new CandidateController(mockService.Object);

        var result = await controller.GetAllCandidates();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCandidateById_ReturnsNotFound_WhenCandidateDoesNotExist()
    {
        var mockService = new Mock<ICandidateService>();
        mockService.Setup(service => service.GetCandidateByIdAsync(It.IsAny<int>())).ReturnsAsync((Candidate)null);
        var controller = new CandidateController(mockService.Object);

        var result = await controller.GetCandidateById(1);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpsertCandidate_ReturnsOkResult()
    {
        var mockService = new Mock<ICandidateService>();
        var candidate = new Candidate { Id = 1, FirstName = "John", LastName = "Doe" };
        mockService.Setup(service => service.UpsertCandidateAsync(candidate)).ReturnsAsync(candidate);
        var controller = new CandidateController(mockService.Object);

        var result = await controller.UpsertCandidate(candidate);

        Assert.IsType<OkObjectResult>(result);
    }
}
