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

public class MemberSelfMembershipServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly MemberSelfMembershipService _svc;
    private readonly string _userId;

    public MemberSelfMembershipServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _userId = Guid.NewGuid().ToString();

        _ctx.Members.Add(new Member
        {
            Id = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "jan@example.com",
            UserId = _userId
        });

        _ctx.MembershipTypes.Add(new MembershipType
        {
            Id = 1,
            Name = "Standard",
            DurationInDays = 30,
            IsVisible = true
        });

        _ctx.SaveChanges();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, _userId)
        }));

        var httpContext = new DefaultHttpContext { User = user };
        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var mapper = new MemberSelfMembershipMapper();
        var logger = NullLogger<MemberSelfMembershipService>.Instance;

        _svc = new MemberSelfMembershipService(_ctx, mapper, accessorMock.Object, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }
}
