using GymManager.Models.DTOs.Trainer;
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

        public WorkoutNotesController(
            AdminWorkoutNoteService admin,
            MemberSelfWorkoutNoteService member,
            TrainerSelfWorkoutNoteService trainer)
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

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetMyNotes()
            => Ok(await _member.GetAllAsync());

        [HttpGet("self/{id}")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetMyById(int id)
        {
            var dto = await _member.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyAsTrainer()
            => Ok(await _trainer.GetAllAsync());

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyByIdAsTrainer(int id)
        {
            var dto = await _trainer.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
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
            => (await _trainer.PatchAsync(id, dto)) ? NoContent() : NotFound();
        
        /*
        [HttpDelete("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> DeleteAsTrainer(int id)
            => (await _trainer.DeleteAsync(id)) ? NoContent() : NotFound();
            */
    }
}
