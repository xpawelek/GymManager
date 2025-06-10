using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Trainer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace GymManager.Tests.Services;

public class TrainerProgressPhotoServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly TrainerProgressPhotoService _svc;
    private readonly string _trainerUserId;

    public TrainerProgressPhotoServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _trainerUserId = Guid.NewGuid().ToString();

        // Seed Trainer and Member
        _ctx.Trainers.Add(new Trainer
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Email = "trainer@example.com",
            UserId = _trainerUserId
        });

        _ctx.Members.Add(new Member
        {
            Id = 1,
            FirstName = "Patryk",
            LastName = "Kox",
            Email = "patryk@example.com",
            UserId = Guid.NewGuid().ToString()
        });

        _ctx.TrainerAssignments.Add(new TrainerAssignments
        {
            MemberId = 1,
            TrainerId = 1,
            IsActive = true
        });

        _ctx.ProgressPhotos.AddRange(
            new ProgressPhoto
            {
                Id = 1,
                MemberId = 1,
                Comment = "Visible to trainer",
                IsPublic = false,
                Date = DateTime.Today
            },
            new ProgressPhoto
            {
                Id = 2,
                MemberId = 1,
                Comment = "Public photo",
                IsPublic = true,
                Date = DateTime.Today
            },
            new ProgressPhoto
            {
                Id = 3,
                MemberId = 99,
                Comment = "Not visible",
                IsPublic = false,
                Date = DateTime.Today
            });

        _ctx.SaveChanges();

        var accessor = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, _trainerUserId)
        }));
        accessor.Setup(a => a.HttpContext).Returns(httpContext);

        var mapper = new TrainerProgressPhotoMapper();
        var logger = NullLogger<TrainerProgressPhotoService>.Instance;
        _svc = new TrainerProgressPhotoService(_ctx, mapper, accessor.Object, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_public_and_assigned()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllPublic_returns_only_public()
    {
        var result = await _svc.GetAllPublic();
        result.Should().ContainSingle();
        result[0].IsPublic.Should().BeTrue();
    }

    [Fact]
    public async Task GetAssignedMembersPhotosAsync_returns_only_assigned()
    {
        var result = await _svc.GetAssignedMembersPhotosAsync();
        result.Should().ContainSingle();
        result[0].Comment.Should().Be("Visible to trainer");
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_if_not_assigned()
    {
        var result = await _svc.GetByIdAsync(3);
        result.Should().BeNull(); // Not assigned
    }

    [Fact]
    public async Task GetByIdAsync_returns_if_assigned()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Comment.Should().Be("Visible to trainer");
    }
}
