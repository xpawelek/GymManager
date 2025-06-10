using System;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Admin;
using GymManager.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class AdminMessageServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly AdminMessageService _svc;

    public AdminMessageServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        // seed required data
        _ctx.Members.Add(new Member { Id = 1, FirstName = "A", LastName = "B", Email = "a@b.com", UserId = Guid.NewGuid().ToString() });
        _ctx.Trainers.Add(new Trainer { Id = 1, FirstName = "T", LastName = "R", Email = "t@r.com", UserId = Guid.NewGuid().ToString() });

        _ctx.Messages.Add(new Message
        {
            Id = 1,
            MessageContent = "Test Message",
            MemberId = 1,
            TrainerId = 1,
            SentByMember = true,
            Date = DateTime.UtcNow
        });

        _ctx.SaveChanges();

        var mapper = new AdminMessageMapper();
        var logger = NullLogger<AdminMessageService>.Instance;
        var accessor = new HttpContextAccessor();
        _svc = new AdminMessageService(_ctx, mapper, accessor, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }
}
