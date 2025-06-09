using System.Security.Claims;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateMembershipTypeDto;
using AdminUpdateDto = GymManager.Shared.DTOs.Admin.UpdateMembershipTypeDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/membership-types")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipTypesController : ControllerBase
    {
        private readonly AdminMembershipTypeService _admin;
        private readonly MemberMembershipTypeService _member;
        private readonly TrainerMembershipTypeService _trainer;
        private readonly ILogger<MembershipTypesController> _logger;

        public MembershipTypesController(
            AdminMembershipTypeService admin,
            MemberMembershipTypeService member,
            TrainerMembershipTypeService trainer,
            ILogger<MembershipTypesController> logger)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                object? result = Role switch
                {
                    RoleConstants.Admin => await _admin.GetAllAsync(),
                    RoleConstants.Member => await _member.GetAllAsync(),
                    RoleConstants.Trainer => await _trainer.GetAllAsync(),
                    _ => null
                };

                return result == null ? Forbid() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while fetching all membership types.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                object? dto = Role switch
                {
                    RoleConstants.Admin => await _admin.GetByIdAsync(id),
                    RoleConstants.Member => await _member.GetByIdAsync(id),
                    RoleConstants.Trainer => await _trainer.GetByIdAsync(id),
                    _ => null
                };

                return dto == null ? Forbid() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while fetching membership type by ID.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicMembershipType()
        {
            try
            {
                var membershipTypeList = await _member.GetAllAsync();
                return Ok(membershipTypeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while fetching public membership types.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] AdminCreateDto dto)
        {
            try
            {
                var r = await _admin.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while creating membership type.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Patch(int id, [FromBody] AdminUpdateDto dto)
        {
            try
            {
                var ok = await _admin.PatchAsync(id, dto);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while updating membership type.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ok = await _admin.DeleteAsync(id);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Time}] Error while deleting membership type.", DateTime.Now);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
