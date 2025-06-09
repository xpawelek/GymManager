using System.Security.Claims;
using System.Text.Json;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Shared.DTOs.Admin.CreateMembershipDto;
using MemberCreateDto = GymManager.Shared.DTOs.Member.CreateSelfMembershipDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/memberships")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipController : ControllerBase
    {
        private readonly AdminMembershipService _admin;
        private readonly MemberSelfMembershipService _member;
        private readonly ILogger<MembershipController> _logger;

        public MembershipController(
            AdminMembershipService admin,
            MemberSelfMembershipService member,
            ILogger<MembershipController> logger)
        {
            _admin = admin;
            _member = member;
            _logger = logger;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                switch (Role)
                {
                    case RoleConstants.Admin:
                        return Ok(await _admin.GetAllAsync());

                    case RoleConstants.Member:
                        var own = await _member.GetOwnAsync();
                        return own == null ? NotFound() : Ok(own);

                    default:
                        return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving memberships.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{memberId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByMemberId(int memberId)
        {
            try
            {
                var dto = await _admin.GetByMemberIdAsync(memberId);
                return dto == null ? NotFound() : Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving membership by member ID.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("has-or-had")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> HasOrHadAny()
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdStr == null)
                    return Unauthorized();

                var has = await _member.HasOrHadAnyMembershipAsync();
                return Ok(has);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking membership history.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member)]
        public async Task<IActionResult> Create([FromBody] object raw)
        {
            try
            {
                switch (Role)
                {
                    case RoleConstants.Admin:
                        {
                            var dto = JsonSerializer
                                .Deserialize<AdminCreateDto>(raw.ToString()!)!;
                            var r = await _admin.CreateAsync(dto);
                            return CreatedAtAction(nameof(GetByMemberId), new { id = r.Id }, r);
                        }
                    case RoleConstants.Member:
                        {
                            var dto = JsonSerializer
                                .Deserialize<MemberCreateDto>(raw.ToString()!)!;
                            var r = await _member.CreateSelfMembership(dto);
                            return CreatedAtAction(nameof(GetAll), null, r);
                        }
                    default:
                        return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating membership.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("admin/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAsAdmin(int id, [FromBody] GymManager.Shared.DTOs.Admin.UpdateMembershipDto dto)
        {
            try
            {
                return await _admin.PatchAsync(id, dto) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating membership as admin.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> PatchOwn([FromBody] UpdateMembershipDto dto)
        {
            try
            {
                return await _member.UpdateOwnAsync(dto) ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating own membership.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _admin.DeleteAsync(id)
                    ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting membership.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
