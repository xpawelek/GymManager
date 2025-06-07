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

        public MembershipController(
            AdminMembershipService admin,
            MemberSelfMembershipService member)
        {
            _admin = admin;
            _member = member;
        }

        private string Role => User.FindFirstValue(ClaimTypes.Role)!;

        [HttpGet]
        public async Task<IActionResult> GetAll()
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

        [HttpGet("{memberId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetByMemberId(int memberId)
        {
            var dto = await _admin.GetByMemberIdAsync(memberId);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("has-or-had")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> HasOrHadAny()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdStr == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdStr);
            var has = await _member.HasOrHadAnyMembershipAsync();
            return Ok(has);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member)]
        public async Task<IActionResult> Create([FromBody] object raw)
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

        [HttpPatch("admin/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PatchAsAdmin(int id, [FromBody] GymManager.Shared.DTOs.Admin.UpdateMembershipDto dto)
        {
            return await _admin.PatchAsync(id, dto) ? NoContent() : NotFound();
        }
        
        
        [HttpPatch("self")]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> PatchOwn([FromBody] UpdateMembershipDto dto)
        {
            return await _member.UpdateOwnAsync(dto) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _admin.DeleteAsync(id)
                ? NoContent() : NotFound();
        }
    }
}
