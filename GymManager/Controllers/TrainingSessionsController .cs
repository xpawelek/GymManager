using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Models.DTOs.Admin.CreateTrainingSessionDto;
using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateTrainingSessionDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/training-sessions")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainingSessionsController : ControllerBase
    {
        private readonly AdminTrainingSessionService _admin;
        private readonly MemberTrainingSessionService _member;
        private readonly TrainerTrainingSessionService _trainer;

        public TrainingSessionsController(
            AdminTrainingSessionService admin,
            MemberTrainingSessionService member,
            TrainerTrainingSessionService trainer)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
            => Ok(await _admin.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            var dto = await _admin.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateDto dto)
        {
            var result = await _admin.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAdmin), new { id = result.Id }, result);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] AdminUpdateDto dto)
            => (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
            => (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();


        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAllMember()
            => Ok(await _member.GetAllAsync());

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            var dto = await _member.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }


        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAllTrainer()
            => Ok(await _trainer.GetAllAsync());

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetByIdTrainer(int id)
        {
            var dto = await _trainer.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }
    }
}
