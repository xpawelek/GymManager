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

public class TrainerMessageServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly TrainerMessageService _svc;
    private readonly string _trainerUserId;

    public TrainerMessageServiceTests()
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
            FirstName = "Trainer",
            LastName = "Test",
            Email = "trainer@example.com",
            UserId = _trainerUserId
        });

        _ctx.Members.Add(new Member
        {
            Id = 1,
            FirstName = "Member",
            LastName = "Test",
            Email = "member@example.com",
            UserId = Guid.NewGuid().ToString()
        });

        _ctx.TrainerAssignments.Add(new TrainerAssignments
        {
            MemberId = 1,
            TrainerId = 1,
            IsActive = true
        });

        _ctx.SaveChanges();

        // Mock HttpContext with trainer claim
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, _trainerUserId)
        }));

        var accessor = new Mock<IHttpContextAccessor>();
        accessor.Setup(a => a.HttpContext).Returns(httpContext);

        var mapper = new TrainerSelfMessageMapper();
        var logger = NullLogger<TrainerMessageService>.Instance;
        _svc = new TrainerMessageService(_ctx, mapper, accessor.Object, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }
    
}
