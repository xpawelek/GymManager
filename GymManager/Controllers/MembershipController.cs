using System.Security.Claims;
using System.Text.Json;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Models.DTOs.Admin.CreateMembershipDto;
using MemberCreateDto = GymManager.Models.DTOs.Member.CreateSelfMembershipDto;

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

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _admin.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
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
                        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
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

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMembershipDto dto)
        {
            switch (Role)
            {
                case RoleConstants.Admin:
                    return await _admin.PatchAsync(id, dto)
                        ? NoContent() : NotFound();

                case RoleConstants.Member:
                    return await _member.UpdateOwnAsync(dto)
                        ? NoContent() : NotFound();

                default:
                    return Forbid();
            }
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
