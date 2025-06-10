using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Member;
using GymManager.Services.Member;
using GymManager.Shared.DTOs.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GymManager.Tests.Services;

public class MemberServiceRequestServiceTests
{
    private readonly DbContextOptions<GymDbContext> _dbOptions;

    public MemberServiceRequestServiceTests()
    {
        _dbOptions = new DbContextOptionsBuilder<GymDbContext>()
            .UseInMemoryDatabase(databaseName: "MemberServiceRequestTestDb")
            .Options;
    }

    [Fact]
    public async Task CreateAsync_ShouldAddServiceRequestToDatabase()
    {
        // Arrange
        await using var context = new GymDbContext(_dbOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var mapper = new MemberServiceRequestMapper();
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<MemberServiceRequestService>();
        var service = new MemberServiceRequestService(context, mapper, logger);

        var dto = new CreateServiceRequestDto
        {
            ServiceProblemTitle = "Sprzęt uszkodzony",
            ProblemNote = "Bieżnia nie działa poprawnie."
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.True(result);
        Assert.Single(context.ServiceRequests);
        var request = context.ServiceRequests.First();
        Assert.Equal("Sprzęt uszkodzony", request.ServiceProblemTitle);
        Assert.Equal("Bieżnia nie działa poprawnie.", request.ProblemNote);
    }
}
