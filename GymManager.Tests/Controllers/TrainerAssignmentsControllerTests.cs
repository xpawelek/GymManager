using Xunit;
using Moq;
using FluentAssertions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManager.Controllers;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Tests.Helpers;

namespace GymManager.Tests.Controllers;

public class TrainerAssignmentsControllerTests
{
    private readonly Mock<TrainerSelfTrainerAssignmentService> _trainerService = new();
    private readonly TrainerAssignmentsController _controller;

    public TrainerAssignmentsControllerTests()
    {
        _controller = new TrainerAssignmentsController(
            admin: null!,
            member: null!,
            trainer: _trainerService.Object,
            logger: TestHelper.GetLogger<TrainerAssignmentsController>()
        );

        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "42"),
            new Claim(ClaimTypes.Role, "Trainer")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public async Task GetMyAssignments_ShouldReturnOkWithList()
    {
        var mockList = new List<ReadSelfTrainerAssignmentDto>
        {
            new() { Id = 1, MemberId = 7, IsActive = true }
        };
        _trainerService.Setup(s => s.GetAllAsync()).ReturnsAsync(mockList);

        var result = await _controller.GetMyAssignments();

        result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(mockList);
    }

    [Fact]
    public async Task GetMyById_ShouldReturnNotFound_WhenNull()
    {
        _trainerService.Setup(s => s.GetByIdAsync(99))
                       .ReturnsAsync((ReadSelfTrainerAssignmentDto?)null);

        var result = await _controller.GetMyById(99);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetMyById_ShouldReturnOk_WhenExists()
    {
        var dto = new ReadSelfTrainerAssignmentDto
        {
            Id = 2,
            MemberId = 17,
            IsActive = true
        };

        _trainerService.Setup(s => s.GetByIdAsync(2)).ReturnsAsync(dto);

        var result = await _controller.GetMyById(2);

        result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(dto);
    }
}
