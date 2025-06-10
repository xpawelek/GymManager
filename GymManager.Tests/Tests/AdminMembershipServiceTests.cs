using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Admin;
using GymManager.Services.Admin;
using GymManager.Shared.DTOs.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class AdminMembershipServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly AdminMembershipService _svc;

    public AdminMembershipServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.Members.Add(new Member
        {
            Id = 1,
            FirstName = "Adam",
            LastName = "Nowak",
            Email = "adam@example.com",
            UserId = Guid.NewGuid().ToString()
        });

        _ctx.MembershipTypes.Add(new MembershipType
        {
            Id = 1,
            Name = "Basic",
            DurationInDays = 30
        });

        _ctx.Memberships.AddRange(
            new Membership
            {
                Id = 1,
                MemberId = 1,
                MembershipTypeId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30),
                IsActive = true
            },
            new Membership
            {
                Id = 2,
                MemberId = 1,
                MembershipTypeId = 1,
                StartDate = DateTime.Today.AddDays(-60),
                EndDate = DateTime.Today.AddDays(-30),
                IsActive = false
            }
        );

        _ctx.SaveChanges();

        var mapper = new AdminMembershipMapper();
        var logger = NullLogger<AdminMembershipService>.Instance;
        _svc = new AdminMembershipService(_ctx, mapper, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_all_memberships()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByMemberIdAsync_returns_active_membership()
    {
        var result = await _svc.GetByMemberIdAsync(1);
        result.Should().NotBeNull();
        result!.MemberId.Should().Be(1);
        result.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task GetByMemberIdAsync_returns_null_if_no_active_membership()
    {
        var m = await _ctx.Memberships.FirstOrDefaultAsync(m => m.Id == 1);
        m!.IsActive = false;
        await _ctx.SaveChangesAsync();

        var result = await _svc.GetByMemberIdAsync(1);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByMemberIdAsync_returns_null_for_invalid_member_id()
    {
        var result = await _svc.GetByMemberIdAsync(999);
        result.Should().BeNull();
    }
}
