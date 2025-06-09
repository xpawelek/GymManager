using System.Security.Claims;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/members")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MemberController : ControllerBase
    {
        private readonly AdminMemberService _admin;
        private readonly MemberSelfService _self;
        private readonly TrainerMemberService _trainer;
        private readonly ILogger<MemberController> _logger;

        public MemberController(
            AdminMemberService admin,
            MemberSelfService self,
            TrainerMemberService trainer,
            ILogger<MemberController> logger)
        {
            _admin = admin;
            _self = self;
            _trainer = trainer;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;
        private int Id => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

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
                _logger.LogError(ex, "Error while retrieving all members (Admin).");
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
                _logger.LogError(ex, "Error while retrieving member by ID (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /*
        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Receptionist}")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateMemberDto dto)
        {
            try
            {
                var r = await _admin.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByIdAdmin), new { id = r.Id }, r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a member (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        */

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAdmin(int id, [FromBody] UpdateMemberDto dto)
        {
            try
            {
                return (await _admin.PatchAsync(id, dto)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating member (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                return (await _admin.DeleteAsync(id)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting member (Admin).");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> GetSelf()
        {
            try
            {
                var dto = await _self.GetOwnAsync();
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving self member profile.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> UpdateSelf([FromBody] UpdateSelfMemberDto dto)
        {
            try
            {
                return (await _self.UpdateOwnAsync(dto)) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating self member profile.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("assigned")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAssigned()
        {
            try
            {
                return Ok(await _trainer.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving assigned members.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("assigned/{id}")]
        [Authorize(Roles = RoleConstants.Trainer)]
        public async Task<IActionResult> GetAssignedById(int id)
        {
            try
            {
                var dto = await _trainer.GetByIdAsync(id);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving assigned member by ID.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
