using GymManager.Data;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Trainer;
using GymManager.Services.Trainer;
using GymManager.Shared.DTOs.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GymManager.Tests.Services
{
    public class TrainerServiceRequestServiceTests
    {
        private readonly TrainerServiceRequestService _service;
        private readonly GymDbContext _context;

        public TrainerServiceRequestServiceTests()
        {
            var options = new DbContextOptionsBuilder<GymDbContext>()
                .UseInMemoryDatabase(databaseName: "TrainerServiceRequestDb")
                .Options;
            _context = new GymDbContext(options);

            var mapper = new TrainerServiceRequestMapper();
            var logger = new LoggerFactory().CreateLogger<TrainerServiceRequestService>();

            _service = new TrainerServiceRequestService(_context, mapper, logger);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddRequest()
        {
            var dto = new CreateServiceRequestDto
            {
                ServiceProblemTitle = "Broken equipment",
                ProblemNote = "Treadmill #3 is not working"
            };

            var result = await _service.CreateAsync(dto);

            Assert.True(result);
            Assert.Single(_context.ServiceRequests);

            var saved = await _context.ServiceRequests.FirstAsync();
            Assert.Equal("Broken equipment", saved.ServiceProblemTitle);
            Assert.Equal("Treadmill #3 is not working", saved.ProblemNote);
        }
    }
}
