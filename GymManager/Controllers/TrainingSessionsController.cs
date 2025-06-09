using System.Security.Claims;
using GymManager.Exceptions;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateTrainingSessionDto;
using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateTrainingSessionDto;
using MemberCreateDto = GymManager.Shared.DTOs.Member.CreateTrainingSessionDto;
using MemberUpdateDto = GymManager.Shared.DTOs.Member.UpdateTrainingSessionDto;

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
        private readonly ILogger<TrainingSessionsController> _logger;

        public TrainingSessionsController(
            AdminTrainingSessionService admin,
            MemberTrainingSessionService member,
            TrainerTrainingSessionService trainer,
            ILogger<TrainingSessionsController> logger)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
                return Ok(await _admin.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all training sessions (admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int id)
        {
            try
            {
                var dto = await _admin.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching training session by ID {Id} (admin).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("member/{memberId}/personal")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetPersonalForMember(int memberId)
        {
            try
            {
                var result = await _admin.GetByMemberIdAsync(memberId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching personal sessions for member {MemberId}.", memberId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("trainer/{trainerId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetSessionForMember(int trainerId)
        {
            try
            {
                var result = await _admin.GetByTrainerIdAsync(trainerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sessions for trainer {TrainerId}.", trainerId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateDto dto)
        {
            try
            {
                var result = await _admin.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByIdAdmin), new { id = result.Id }, result);
            }
            catch (UserFacingException ex)
            {
                _logger.LogWarning("Validation failed during admin session creation: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating training session (admin).");
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> CreateMember([FromBody] MemberCreateDto dto)
        {
            try
            {
                var result = await _member.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByIdAdmin), new { id = result.Id }, result);
            }
            catch (UserFacingException ex)
            {
                _logger.LogWarning("Validation failed during member session creation: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating training session (member).");
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] AdminUpdateDto dto)
        {
            try
            {
                return await _admin.PatchAsync(id, dto) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching session {Id} (admin).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> PatchMember(int id, [FromBody] MemberUpdateDto dto)
        {
            try
            {
                return await _member.PatchAsync(id, dto) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching session {Id} (member).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                return await _admin.DeleteAsync(id) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting session {Id} (admin).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPublic()
        {
            try
            {
                return Ok(await _member.GetAllGroupAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting public training sessions.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAllMember()
        {
            try
            {
                return Ok(await _member.GetMemberAllPersonalAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting member's own training sessions.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetByIdMember(int id)
        {
            try
            {
                var dto = await _member.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting session {Id} (member).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> DeleteOwnMember(int id)
        {
            try
            {
                return await _admin.DeleteAsync(id) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting own session {Id} (member).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAllTrainer()
        {
            try
            {
                return Ok(await _trainer.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting trainer sessions.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetByIdTrainer(int id)
        {
            try
            {
                var dto = await _trainer.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting session {Id} (trainer).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
