using GymManager.Shared.DTOs.Trainer;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/trainers/profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Trainer)]
    public class TrainerProfileController : ControllerBase
    {
        private readonly TrainerProfileService _svc;
        private readonly ILogger<TrainerProfileController> _logger;

        public TrainerProfileController(TrainerProfileService svc, ILogger<TrainerProfileController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        // GET /api/trainers/profile
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            try
            {
                var dto = await _svc.GetMyProfileAsync();
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while retrieving trainer profile.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // PATCH /api/trainers/profile
        [HttpPatch]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateSelfTrainerDto dto)
        {
            try
            {
                var ok = await _svc.UpdateMyProfileAsync(dto);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while updating trainer profile.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
