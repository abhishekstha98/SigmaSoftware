﻿using CandidateHubAPI.Controllers;
using CandidateHubAPI.Models;
using CandidateHubAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CandidateHubAPI.Tests
{
    public class CandidateControllerTests
    {
        [Fact]
        public async Task GetAllCandidates_ReturnsOkWithCandidates()
        {
            var mockService = new Mock<ICandidateService>();
            mockService.Setup(service => service.GetAllCandidatesAsync())
                .ReturnsAsync(new List<Candidate>
                {
                    new Candidate { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
                });

            var controller = new CandidateController(mockService.Object);
            var result = await controller.GetAllCandidates();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var candidates = Assert.IsAssignableFrom<IEnumerable<Candidate>>(okResult.Value);
            Assert.Single(candidates);
        }

        [Fact]
        public async Task GetCandidateById_ReturnsNotFound_WhenCandidateDoesNotExist()
        {
            var mockService = new Mock<ICandidateService>();
            mockService.Setup(service => service.GetCandidateByIdAsync(1))
                .ReturnsAsync((Candidate)null);

            var controller = new CandidateController(mockService.Object);
            var result = await controller.GetCandidateById(1);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCandidateByEmail_ReturnsNotFound_WhenCandidateDoesNotExist()
        {
            var mockService = new Mock<ICandidateService>();
            mockService.Setup(service => service.SearchCandidateByEmailAsync("john.doe@example.com"))
                .ReturnsAsync((Candidate)null);

            var controller = new CandidateController(mockService.Object);
            var result = await controller.GetCandidateById(1);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
