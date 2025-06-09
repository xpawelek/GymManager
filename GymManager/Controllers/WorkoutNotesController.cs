using GymManager.Shared.DTOs.Trainer;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/workout-notes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutNotesController : ControllerBase
    {
        private readonly AdminWorkoutNoteService _admin;
        private readonly MemberSelfWorkoutNoteService _member;
        private readonly TrainerSelfWorkoutNoteService _trainer;
        private readonly ILogger<WorkoutNotesController> _logger;

        public WorkoutNotesController(
            AdminWorkoutNoteService admin,
            MemberSelfWorkoutNoteService member,
            TrainerSelfWorkoutNoteService trainer,
            ILogger<WorkoutNotesController> logger)
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
                _logger.LogError(ex, "Error fetching all workout notes (admin).");
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
                _logger.LogError(ex, "Error fetching workout note by ID {Id} (admin).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetMyNotes()
        {
            try
            {
                return Ok(await _member.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching workout notes (member).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetMyById(int id)
        {
            try
            {
                var dto = await _member.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching workout note {Id} (member).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyAsTrainer()
        {
            try
            {
                return Ok(await _trainer.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching trainer's workout notes.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyByIdAsTrainer(int id)
        {
            try
            {
                var dto = await _trainer.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching workout note {Id} (trainer).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /*
        [HttpPost("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> CreateAsTrainer([FromBody] CreateSelfWorkoutNote dto)
        {
            var result = await _trainer.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetMyByIdAsTrainer),
                new { id = result.Id },
                result);
        }
        */

        [HttpPatch("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> PatchAsTrainer(int id, [FromBody] UpdateSelfWorkoutNoteDto dto)
        {
            try
            {
                var ok = await _trainer.PatchAsync(id, dto);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching workout note {Id} (trainer).", id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /*
        [HttpDelete("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> DeleteAsTrainer(int id)
            => (await _trainer.DeleteAsync(id)) ? NoContent() : NotFound();
        */
    }
}
