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
    
}
