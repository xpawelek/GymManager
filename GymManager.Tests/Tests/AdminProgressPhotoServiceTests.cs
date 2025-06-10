using System;
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

public class AdminProgressPhotoServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly AdminProgressPhotoService _svc;

    public AdminProgressPhotoServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        // Seed sample data
        _ctx.ProgressPhotos.Add(new ProgressPhoto
        {
            Id = 1,
            MemberId = 1,
            Comment = "Progress",
            IsPublic = true,
            Date = DateTime.Today
        });

        _ctx.ProgressPhotos.Add(new ProgressPhoto
        {
            Id = 2,
            MemberId = 2,
            Comment = "Private",
            IsPublic = false,
            Date = DateTime.Today
        });

        _ctx.SaveChanges();

        var mapper = new AdminProgessPhotoMapper();
        var logger = NullLogger<AdminProgressPhotoService>.Instance;
        _svc = new AdminProgressPhotoService(_ctx, mapper, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_all_photos()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_returns_correct_photo()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Comment.Should().Be("Progress");
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_for_invalid_id()
    {
        var result = await _svc.GetByIdAsync(999);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllPublic_returns_only_public_photos()
    {
        var result = await _svc.GetAllPublic();
        result.Should().HaveCount(1);
        result[0].IsPublic.Should().BeTrue();
    }

    [Fact]
    public async Task PatchAsync_updates_comment()
    {
        var dto = new UpdateProgressPhotoDto
        {
            Comment = "Updated!"
        };

        var success = await _svc.PatchAsync(1, dto);
        success.Should().BeTrue();

        var updated = await _svc.GetByIdAsync(1);
        updated!.Comment.Should().Be("Updated!");
    }

    [Fact]
    public async Task PatchAsync_returns_false_for_invalid_id()
    {
        var dto = new UpdateProgressPhotoDto { Comment = "Should not work" };
        var result = await _svc.PatchAsync(999, dto);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_deletes_existing_photo()
    {
        var success = await _svc.DeleteAsync(2);
        success.Should().BeTrue();

        var all = await _svc.GetAllAsync();
        all.Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteAsync_returns_false_for_invalid_id()
    {
        var result = await _svc.DeleteAsync(999);
        result.Should().BeFalse();
    }
}
