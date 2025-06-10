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
}
