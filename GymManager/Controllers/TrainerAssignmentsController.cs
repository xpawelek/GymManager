using System.Security.Claims;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using GymManager.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateTrainerAssignmentDto;
using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateTrainerAssignmentsDto;
using MemberCreateDto = GymManager.Shared.DTOs.Member.CreateTrainerAssignmentDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/trainer-assignments")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainerAssignmentsController : ControllerBase
    {
        private readonly AdminTrainerAssignmentService _admin;
        private readonly MemberSelfTrainerAssignmentService _member;
        private readonly TrainerSelfTrainerAssignmentService _trainer;
        private readonly ILogger<TrainerAssignmentsController> _logger;

        public TrainerAssignmentsController(
            AdminTrainerAssignmentService admin,
            MemberSelfTrainerAssignmentService member,
            TrainerSelfTrainerAssignmentService trainer,
            ILogger<TrainerAssignmentsController> logger)
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
                _logger.LogError(ex, "[{Time}] Error while retrieving all trainer assignments.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{memberId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByIdAdmin(int memberId)
        {
            try
            {
                var dto = await _admin.GetByMemberIdAsync(memberId);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while retrieving trainer assignment by member ID {MemberId}.", DateTime.Now, memberId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetAllMember()
        {
            try
            {
                return Ok(await _member.GetOwnAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while retrieving self trainer assignments.", DateTime.Now);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while creating trainer assignment.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
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
                _logger.LogError(ex, "[{Time}] Error while updating trainer assignment {Id}.", DateTime.Now, id);
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
                _logger.LogError(ex, "[{Time}] Error while deleting trainer assignment {Id}.", DateTime.Now, id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> CreateSelf([FromBody] MemberCreateDto dto)
        {
            try
            {
                var id = await _member.CreateAsync(dto);
                return Created($"api/trainer-assignments/self/{id}", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while member creating trainer assignment.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyAssignments()
        {
            try
            {
                return Ok(await _trainer.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while trainer retrieving own assignments.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("me/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetMyById(int id)
        {
            try
            {
                var dto = await _trainer.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while trainer retrieving assignment by ID {Id}.", DateTime.Now, id);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("ever-assigned")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<ActionResult<bool>> HasEverBeenAssigned()
        {
            try
            {
                return Ok(await _member.HasEverBeenAssignedAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while checking if member was ever assigned.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("has-active")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<ActionResult<bool>> HasActiveAssignment()
        {
            try
            {
                return Ok(await _member.HasActiveAssignmentAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while checking if member has active assignment.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("for-member/{memberId}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetForMember(int memberId)
        {
            try
            {
                var messages = await _trainer.GetAllForMemberAsync(memberId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while retrieving assignments for member {MemberId}.", DateTime.Now, memberId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
