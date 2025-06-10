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

public class AdminMemberServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly AdminMemberService _svc;

    public AdminMemberServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.Members.AddRange(
            new Member { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new Member { Id = 2, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com" }
        );
        _ctx.SaveChanges();

        var mapper = new AdminMemberMapper();
        var logger = NullLogger<AdminMemberService>.Instance;
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

}
