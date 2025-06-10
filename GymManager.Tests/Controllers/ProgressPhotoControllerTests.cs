using Xunit;
using Moq;
using FluentAssertions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManager.Controllers;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Member;
using GymManager.Tests.Helpers;

namespace GymManager.Tests.Controllers;

public class ProgressPhotoControllerTests
{
    private readonly Mock<AdminProgressPhotoService> _admin = new();
    private readonly Mock<MemberProgressPhotoService> _member = new();
    private readonly Mock<TrainerProgressPhotoService> _trainer = new();
    private readonly ProgressPhotoController _controller;

    public ProgressPhotoControllerTests()
    {
        _controller = new ProgressPhotoController(
            _admin.Object,
            _member.Object,
            _trainer.Object,
            TestHelper.GetLogger<ProgressPhotoController>());

        // Sztuczne uwierzytelnienie użytkownika
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "2"),
            new Claim(ClaimTypes.Role, "Trainer")
        }, "mock"));
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    }

    [Fact]
    public async Task GetPublic_ShouldReturnOk()
    {
        _member.Setup(s => s.GetAllPublic()).ReturnsAsync(new List<ReadProgressPhotoDto>());
        var result = await _controller.GetPublic();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        var dto = new CreateProgressPhotoDto
        {
            Comment = "Test comment",
            IsPublic = true
        };

        _member.Setup(s => s.CreateAsync(dto))
               .ReturnsAsync(new ReadProgressPhotoDto { Id = 99 });

        var result = await _controller.Create(dto);

        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task UploadPhoto_ShouldReturnOk()
    {
        var file = new FormFile(Stream.Null, 0, 0, "photo", "test.jpg");

        var result = await _controller.UploadPhoto(file);

        result.Should().BeOfType<OkObjectResult>();
    }
}
