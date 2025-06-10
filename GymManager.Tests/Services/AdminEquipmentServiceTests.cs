using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GymManager.Data;
using GymManager.Models;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Admin;
using GymManager.Services.Admin;
using GymManager.Shared.DTOs.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace GymManager.Tests.Services;

public class AdminEquipmentServiceTests : IDisposable
{
    private readonly GymDbContext _ctx;
    private readonly AdminEquipmentService _svc;

    public AdminEquipmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _ctx = new GymDbContext(options);

        _ctx.Equipments.AddRange(
            new Equipment { Id = 1, Name = "Bench Press" },
            new Equipment { Id = 2, Name = "Squat Rack" }
        );
        _ctx.SaveChanges();

        var mapper = new AdminEquipmentMapper();
        var logger = NullLogger<AdminEquipmentService>.Instance;
        _svc = new AdminEquipmentService(_ctx, mapper, logger);
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_returns_all_equipment()
    {
        var result = await _svc.GetAllAsync();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByIdAsync_returns_existing_item()
    {
        var result = await _svc.GetByIdAsync(1);
        result.Should().NotBeNull();
        result!.Name.Should().Be("Bench Press");
    }

    [Fact]
    public async Task GetByIdAsync_returns_null_if_not_found()
    {
        var result = await _svc.GetByIdAsync(999);
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_adds_equipment()
    {
        var dto = new CreateEquipmentDto { Name = "Treadmill" };
        var result = await _svc.CreateAsync(dto);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Treadmill");

        var all = await _svc.GetAllAsync();
        all.Should().HaveCount(3);
    }

    [Fact]
    public async Task PatchAsync_updates_equipment()
    {
        var updateDto = new UpdateEquipmentDto { Name = "Deluxe Bench" };
        var success = await _svc.PatchAsync(1, updateDto);

        success.Should().BeTrue();
        var updated = await _svc.GetByIdAsync(1);
        updated!.Name.Should().Be("Deluxe Bench");
    }

    [Fact]
    public async Task PatchAsync_returns_false_for_missing_id()
    {
        var updateDto = new UpdateEquipmentDto { Name = "X" };
        var result = await _svc.PatchAsync(999, updateDto);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_removes_item()
    {
        var result = await _svc.DeleteAsync(2);
        result.Should().BeTrue();

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