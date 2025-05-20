using GymManager.Models.DTOs.Trainer;
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

        public TrainerProfileController(TrainerProfileService svc)
        {
            _svc = svc;
        }

        // GET /api/trainers/profile
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var dto = await _svc.GetMyProfileAsync();
            return dto == null ? NotFound() : Ok(dto);
        }

        // PATCH /api/trainers/profile
        [HttpPatch]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateSelfTrainerDto dto)
        {
            var ok = await _svc.UpdateMyProfileAsync(dto);
            return ok ? NoContent() : NotFound();
        }
    }
}
