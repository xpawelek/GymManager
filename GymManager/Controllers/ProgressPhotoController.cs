using System.Security.Claims;
using System.Text.Json;
using GymManager.Models.Identity;
using GymManager.Services.Admin;
using GymManager.Services.Member;
using GymManager.Services.Trainer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AdminUpdateDto = GymManager.Models.DTOs.Admin.UpdateProgressPhotoDto;
using MemberCreateDto = GymManager.Models.DTOs.Member.CreateProgressPhotoDto;
using MemberUpdateDto = GymManager.Models.DTOs.Member.UpdateProgressPhotoDto;

namespace GymManager.Controllers
{
    [ApiController]
    [Route("api/progress-photos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProgressPhotoController : ControllerBase
    {
        private readonly AdminProgressPhotoService _admin;
        private readonly MemberProgressPhotoService _member;
        private readonly TrainerProgressPhotoService _trainer;

        public ProgressPhotoController(
            AdminProgressPhotoService admin,
            MemberProgressPhotoService member,
            TrainerProgressPhotoService trainer)
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
                    var a = await _admin.GetByIdAsync(id);
                    return a == null ? NotFound() : Ok(a);
                case RoleConstants.Member:
                    var m = await _member.GetByIdAsync(id);
                    return m == null ? NotFound() : Ok(m);
                case RoleConstants.Trainer:
                    var t = await _trainer.GetByIdAsync(id);
                    return t == null ? NotFound() : Ok(t);
                default:
                    return Forbid();
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Member)]
        public async Task<IActionResult> Create([FromBody] MemberCreateDto dto)
        {
            var r = await _member.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] object raw)
        {
            switch (Role)
            {
                case RoleConstants.Admin:
                    var aDto = JsonSerializer.Deserialize<AdminUpdateDto>(raw.ToString()!)!;
                    return await _admin.PatchAsync(id, aDto)
                        ? NoContent() : NotFound();
                case RoleConstants.Member:
                    var mDto = JsonSerializer.Deserialize<MemberUpdateDto>(raw.ToString()!)!;
                    return await _member.PatchAsync(id, mDto)
                        ? NoContent() : NotFound();
                default:
                    return Forbid();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (Role)
            {
                case RoleConstants.Admin:
                    return await _admin.DeleteAsync(id)
                        ? NoContent() : NotFound();
                case RoleConstants.Member:
                    return await _member.DeleteAsync(id)
                        ? NoContent() : NotFound();
                default:
                    return Forbid();
            }
        }
    }
}
