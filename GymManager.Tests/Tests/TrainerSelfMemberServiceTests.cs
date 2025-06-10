using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class TrainerMemberServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly TrainerMemberService _svc;

    public TrainerMemberServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.Members.AddRange(
            new Member { Id = 1, FirstName = "Tomasz", LastName = "Zieliński", Email = "tomasz@example.com" },
            new Member { Id = 2, FirstName = "Katarzyna", LastName = "Mazur", Email = "kasia@example.com" }
        );
        _ctx.SaveChanges();

        var mapper = new TrainerMemberMapper();
        var logger = NullLogger<TrainerMemberService>.Instance;
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }
}
