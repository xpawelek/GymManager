using System.Security.Claims;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminCreateDto = GymManager.Models.DTOs.Admin.CreateMembershipTypeDto;
using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateMembershipTypeDto;

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

        public MembershipTypesController(
            AdminMembershipTypeService admin,
            MemberMembershipTypeService member,
            TrainerMembershipTypeService trainer)
        {
            _admin = admin;
            _member = member;
            _trainer = trainer;
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
                    return Ok(await _member.GetAllAsync());
                case RoleConstants.Trainer:
                    return Ok(await _trainer.GetAllAsync());
                default:
                    return Forbid();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            switch (Role)
            {
                case RoleConstants.Admin:
                    {
                        var dto = await _admin.GetByIdAsync(id);
                        return dto == null ? NotFound() : Ok(dto);
                    }
                case RoleConstants.Member:
                    {
                        var dto = await _member.GetByIdAsync(id);
                        return dto == null ? NotFound() : Ok(dto);
                    }
                case RoleConstants.Trainer:
                    {
                        var dto = await _trainer.GetByIdAsync(id);
                        return dto == null ? NotFound() : Ok(dto);
                    }
                default:
                    return Forbid();
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Create([FromBody] AdminCreateDto dto)
        {
            var r = await _admin.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Patch(int id, [FromBody] AdminUpdateDto dto)
        {
            var ok = await _admin.PatchAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _admin.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
