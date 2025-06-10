using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Member;
using GymManager.Services.Member;
using GymManager.Shared.DTOs.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace GymManager.Tests.Services;

public class MemberProgressPhotoServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly MemberProgressPhotoService _svc;
    private readonly string _userId;

    public MemberProgressPhotoServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _userId = Guid.NewGuid().ToString();

        _ctx.Members.Add(new Member
        {
            Id = 1,
            UserId = _userId,
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com"
        });

        _ctx.ProgressPhotos.AddRange(
            new ProgressPhoto
            {
                Id = 1,
                MemberId = 1,
                Comment = "Visible",
                IsPublic = true,
                Date = DateTime.Today
            },
            new ProgressPhoto
            {
                Id = 2,
                MemberId = 1,
                Comment = "Private",
                IsPublic = false,
                Date = DateTime.Today
            });

        _ctx.SaveChanges();

        var accessor = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, _userId)
        }));
        accessor.Setup(a => a.HttpContext).Returns(httpContext);

        var mapper = new MemberProgressPhotoMapper();
        var logger = NullLogger<MemberProgressPhotoService>.Instance;
        _svc = new MemberProgressPhotoService(_ctx, mapper, accessor.Object, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_all_user_photos()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllPublic_returns_only_public()
    {
        var result = await _svc.GetAllPublic();
        result.Should().HaveCount(1);
        result[0].IsPublic.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_returns_only_if_owned()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Comment.Should().Be("Visible");
    }

    [Fact]
    public async Task CreateAsync_adds_new_photo()
    {
        var dto = new CreateProgressPhotoDto
        {
            Comment = "New Progress",
            IsPublic = true,
            ImagePath = "test.jpg"
        };

        var result = await _svc.CreateAsync(dto);
        result.Should().NotBeNull();
        result!.Comment.Should().Be("New Progress");

        var all = await _svc.GetAllAsync();
        all.Should().HaveCount(3);
    }

    [Fact]
    public async Task PatchAsync_edits_comment()
    {
        var dto = new UpdateProgressPhotoDto
        {
            Comment = "Edited"
        };

        var result = await _svc.PatchAsync(1, dto);
        result.Should().BeTrue();

        var updated = await _svc.GetByIdAsync(1);
        updated!.Comment.Should().Be("Edited");
    }

    [Fact]
    public async Task DeleteAsync_deletes_owned_photo()
    {
        var result = await _svc.DeleteAsync(2);
        result.Should().BeTrue();

        var all = await _svc.GetAllAsync();
        all.Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteAsync_fails_for_invalid_id()
    {
        var result = await _svc.DeleteAsync(999);
        result.Should().BeFalse();
    }
}
