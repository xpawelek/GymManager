using System;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class TrainerMembershipTypeServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly TrainerMembershipTypeService _svc;

    public TrainerMembershipTypeServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.MembershipTypes.AddRange(
            new MembershipType { Id = 1, Name = "Premium", DurationInDays = 60, IsVisible = true },
            new MembershipType { Id = 2, Name = "Hidden", DurationInDays = 45, IsVisible = false }
        );
        _ctx.SaveChanges();

        var mapper = new TrainerMembershipTypeMapper();
        var logger = NullLogger<TrainerMembershipTypeService>.Instance;
        _svc = new TrainerMembershipTypeService(_ctx, mapper, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_only_visible_types()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("Premium");
    }

    [Fact]
    public async Task GetByIdAsync_returns_specific_type()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Name.Should().Be("Premium");
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_for_invalid_id()
    {
        var result = await _svc.GetByIdAsync(999);
        result.Should().BeNull();
    }
}
