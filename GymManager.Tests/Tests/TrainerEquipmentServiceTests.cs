using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class TrainerEquipmentServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly TrainerEquipmentService _svc;

    public TrainerEquipmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.Equipments.AddRange(
            new Equipment { Id = 1, Name = "Kettlebell" },
            new Equipment { Id = 2, Name = "Rowing Machine" }
        );
        _ctx.SaveChanges();

        var mapper = new TrainerEquipmentMapper();
        var logger = NullLogger<TrainerEquipmentService>.Instance;
        _svc = new TrainerEquipmentService(_ctx, mapper, logger);
    }

    public void Dispose() => _ctx.Dispose();

    [Fact]
    public async Task GetAllAsync_returns_all_equipment()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_returns_existing_equipment()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Name.Should().Be("Kettlebell");
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_for_missing_id()
    {
        var result = await _svc.GetByIdAsync(999);
        result.Should().BeNull();
    }
}
